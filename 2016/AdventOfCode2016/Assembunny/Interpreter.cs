using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Assembunny
{
    internal class Interpreter
    {
        private string[] instructions;
        private List<string> stackTrace;
        private int instructionPointer;

        public int RegisterA { get { return registers[0]; } set { registers[0] = value; } }
        public int RegisterB { get { return registers[1]; } set { registers[1] = value; } }
        public int RegisterC { get { return registers[2]; } set { registers[2] = value; } }
        public int RegisterD { get { return registers[3]; } set { registers[3] = value; } }

        private int[] registers;

        public Interpreter(string[] instructions)
        {
            this.instructions = instructions;
            registers = new int[4];
            stackTrace = new List<string>();
        }

        public string Run()
        {
            var result = new StringBuilder();

            try
            {
                for (instructionPointer = 0; instructionPointer < instructions.Length; instructionPointer++)
                {
                    stackTrace.Add($"{RegisterA}|{RegisterB}|{RegisterC}|{RegisterD}| [{instructionPointer}]: {instructions[instructionPointer]}");

                    var tokens = instructions[instructionPointer].Split(' ');

                    switch (tokens[0])
                    {
                        case "cpy":
                            Cpy(tokens[1], tokens[2]);
                            break;
                        case "inc":
                            Inc(tokens[1]);
                            break;
                        case "dec":
                            Dec(tokens[1]);
                            break;
                        case "jnz":
                            var offset = Jnz(tokens[1], tokens[2]);
                            instructionPointer += (offset - 1);
                            break;
                        case "tgl":
                            Tgl(tokens[1]);
                            break;
                        case "out":
                            result.Append(Out(tokens[1]).ToString());
                            if (result.Length >= 12)
                                return result.ToString();
                            break;
                        default:
                            throw new Exception($"invalid instruction '{tokens[0]}'");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                foreach (var item in stackTrace) Console.WriteLine($"{item}");
            }

            return "";
        }

        private void Cpy(string value, string name)
        {
            if (!int.TryParse(value, out int intValue))
            {
                var index = value.ToLower()[0] - 'a';
                intValue = registers[index];
            }

            var outIndex = name.ToLower()[0] - 'a';
            registers[outIndex] = intValue;
        }

        private void Inc(string name)
        {
            var index = name.ToLower()[0] - 'a';
            registers[index]++;
        }

        private void Dec(string name)
        {
            var index = name.ToLower()[0] - 'a';
            registers[index]--;
        }

        private int Jnz(string arg1, string arg2)
        {
            if (!int.TryParse(arg1, out int value))
            {
                var index = arg1.ToLower()[0] - 'a';
                value = registers[index];
            }

            if (value == 0)
                return 1;

            if (!int.TryParse(arg2, out int offset))
            {
                var index = arg2.ToLower()[0] - 'a';
                offset = registers[index];
            }

            return offset;
        }

        private void Tgl(string offset)
        {
            if (!int.TryParse(offset, out int value))
            {
                var index = offset.ToLower()[0] - 'a';
                value = registers[index];
            }

            if (instructionPointer + value >= instructions.Length)
                return;

            var instr = instructions[instructionPointer + value];
            var tokens = instr.Split(' ');

            switch (tokens.Length)
            {
                case 2:
                    if (tokens[0] == "inc")
                        instr = $"dec {tokens[1]}";
                    else
                        instr = $"inc {tokens[1]}";
                    break;
                case 3:
                    if (tokens[0] == "jnz")
                        instr = $"cpy {tokens[1]} {tokens[2]}";
                    else
                        instr = $"jnz {tokens[1]} {tokens[2]}";
                    break;
                default:
                    throw new InvalidOperationException($"{instr}");
            }

            instructions[instructionPointer + value] = instr;
        }

        private string Out(string register)
        {
            var index = register.ToLower()[0] - 'a';

            return $"{registers[index]}";
        }
    }
}
