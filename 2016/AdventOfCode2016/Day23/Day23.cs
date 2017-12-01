using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day23
{
    public class Day23
    {
        private Assembunny.Interpreter interpreter;

        public int RegisterA { get { return interpreter.RegisterA; } set { interpreter.RegisterA = value; } }
        public int RegisterB { get { return interpreter.RegisterB; } set { interpreter.RegisterB = value; } }
        public int RegisterC { get { return interpreter.RegisterC; } set { interpreter.RegisterC = value; } }
        public int RegisterD { get { return interpreter.RegisterD; } set { interpreter.RegisterD = value; } }

        public Day23(string[] instructions)
        {
            interpreter = new Assembunny.Interpreter(instructions);
        }

        public void Run()
        {
            interpreter.Run();
        }
    }
}
