using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day21
{
    public class Day21
    {
        private string password;
        private string[] instructions;

        public Day21(string[] instructions)
        {
            this.instructions = instructions;
        }

        public string Scramble(string password)
        {
            this.password = password;

            foreach (var instruction in instructions)
            {
                if (instruction.StartsWith("swap position"))
                    SwapPosition(instruction);

                if (instruction.StartsWith("swap letter"))
                    SwapLetter(instruction);

                if (instruction.StartsWith("reverse"))
                    Reverse(instruction);

                if (instruction.StartsWith("rotate left") || instruction.StartsWith("rotate right"))
                    Rotate(instruction);

                if (instruction.StartsWith("move"))
                    Move(instruction);

                if (instruction.StartsWith("rotate based"))
                    RotateBased(instruction);
            }

            return this.password;
        }

        private void SwapPosition(string instruction)
        {
            var tokens = instruction.Split(' ');
            char c;
            var p1 = Convert.ToInt32(tokens[2]);
            var p2 = Convert.ToInt32(tokens[5]);

            var tmp = password.ToCharArray();
            c = tmp[p1];
            tmp[p1] = tmp[p2];
            tmp[p2] = c;
            password = new string(tmp);
        }

        private void SwapLetter(string instruction)
        {
            var tokens = instruction.Split(' ');
            char c;
            var p1 = password.IndexOf(tokens[2]);
            var p2 = password.IndexOf(tokens[5]);

            var tmp = password.ToCharArray();
            c = tmp[p1];
            tmp[p1] = tmp[p2];
            tmp[p2] = c;
            password = new string(tmp);
        }

        private void Reverse(string instruction)
        {
            var tokens = instruction.Split(' ');
            var p1 = Convert.ToInt32(tokens[2]);
            var p2 = Convert.ToInt32(tokens[4]);

            var tmp = new string(password.Substring(p1, p2 - p1 + 1).Reverse().ToArray());

            password = password.Substring(0, p1) + tmp + password.Substring(p2 + 1, password.Length - p2 - 1);
        }

        private void Rotate(string instruction)
        {
            var tokens = instruction.Split(' ');
            var direction = tokens[1];
            var steps = Convert.ToInt32(tokens[2]);

            if (direction == "left")
                for (int i = 0; i < steps; i++)
                    password = password.Substring(1) + password[0];
            else
                for (int i = 0; i < steps; i++)
                    password = password[password.Length - 1] + password.Substring(0, password.Length - 1);
        }

        private void Move(string instruction)
        {
            var tokens = instruction.Split(' ');
            var from = Convert.ToInt32(tokens[2]);
            var to = Convert.ToInt32(tokens[5]);

            if (from < to)
                password = password.Substring(0, from) + password.Substring(from + 1, to - from) +
                    password[from] + password.Substring(to + 1, password.Length - to - 1);
            else
                password = password.Substring(0, to) + password[from] +
                    password.Substring(to).Replace(password[from].ToString(), "");
        }

        private void RotateBased(string instruction)
        {
            var tokens = instruction.Split(' ');
            var letter = tokens[6];
            var steps = 1 + password.IndexOf(letter) + (password.IndexOf(letter) >= 4 ? 1 : 0);

            for (int i = 0; i < steps; i++)
                password = password[password.Length - 1] + password.Substring(0, password.Length - 1);
        }

        public string BruteForcePassword(string scrambledPassword)
        {
            foreach (var password in AllPasswords("abcdefgh"))
                if (Scramble(password) == scrambledPassword)
                    return password;
            return "";
        }

        private static IEnumerable<string> AllPasswords(string input)
        {
            if (string.IsNullOrEmpty(input)) yield return "";

            foreach (var c in input)
                foreach (var item in AllPasswords(input.Replace($"{c}", "")))
                    yield return $"{c}{item}";
        }
    }
}
