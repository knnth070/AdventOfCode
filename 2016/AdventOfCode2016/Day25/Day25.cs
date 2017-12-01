using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day25
{
    public class Day25
    {
        private Assembunny.Interpreter interpreter;

        public int RegisterA { get { return interpreter.RegisterA; } set { interpreter.RegisterA = value; } }
        public int RegisterB { get { return interpreter.RegisterB; } set { interpreter.RegisterB = value; } }
        public int RegisterC { get { return interpreter.RegisterC; } set { interpreter.RegisterC = value; } }
        public int RegisterD { get { return interpreter.RegisterD; } set { interpreter.RegisterD = value; } }

        public Day25(string[] instructions)
        {
            interpreter = new Assembunny.Interpreter(instructions);
        }
        public int Run()
        {
            string result;

            for (int i = 0; i < Int32.MaxValue; i++)
            {
                RegisterA = i;
                result = interpreter.Run();
                if (result == "010101010101" || result == "101010101010")
                    return i;
            }

            return 0;
        }
    }
}
