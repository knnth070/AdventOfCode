using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            Day11();
        }

        private static void Day1()
        {
            var reader = new StreamReader(@"..\..\Day1\input.txt");
            string input = reader.ReadToEnd();

            var d1p1 = new Day1.Day1(input);
            var length = d1p1.GetShortestPathLength();

            Console.WriteLine($"Puzzle 1: {length}");

            var d1p2 = new Day1.Day1(input);
            var firstCrossing = d1p2.GetShortestPathLength(stopAtFirstCrossing: true);

            Console.WriteLine($"Puzzle 2: {firstCrossing}");
        }

        private static void Day2()
        {
            var reader = new StreamReader(@"..\..\Day2\input.txt");
            string input = reader.ReadToEnd();

            var d2p1 = new Day2.Day2Part1(input);
            var code = d2p1.GetBathroomCode();

            Console.WriteLine($"Puzzle 1: {code}");

            var d2p2 = new Day2.Day2Part2(input);
            code = d2p2.GetBathroomCode();

            Console.WriteLine($"Puzzle 2: {code}");
        }

        private static void Day3Puzzle1()
        {
            int validTriangles = 0;
            var reader = new StreamReader(@"..\..\Day3\input.txt");
            var d3p1 = new Day3.Day3();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (d3p1.IsValidTriangle(line))
                {
                    validTriangles++;
                }
            }

            Console.WriteLine($"Puzzle 1: {validTriangles}");
        }

        private static void Day3Puzzle2()
        {
            int validTriangles = 0;
            var reader = new StreamReader(@"..\..\Day3\input.txt");
            var d3p1 = new Day3.Day3();

            while (!reader.EndOfStream)
            {
                var lines = new StringBuilder[3];

                lines[0] = new StringBuilder();
                lines[1] = new StringBuilder();
                lines[2] = new StringBuilder();

                for (int i = 0; i < 3; i++)
                {
                    var line = reader.ReadLine();
                    var numbers = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x))
                        .ToArray();

                    lines[0].Append(numbers[0] + " ");
                    lines[1].Append(numbers[1] + " ");
                    lines[2].Append(numbers[2] + " ");
                }

                if (d3p1.IsValidTriangle(lines[0].ToString())) validTriangles++;
                if (d3p1.IsValidTriangle(lines[1].ToString())) validTriangles++;
                if (d3p1.IsValidTriangle(lines[2].ToString())) validTriangles++;
            }

            Console.WriteLine($"Puzzle 2: {validTriangles}");
        }

        private static void Day4()
        {
            int sumOfSectorIds = 0;
            var reader = new StreamReader(@"..\..\Day4\input.txt");
            Day4.Day4 d4p1;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                d4p1 = new Day4.Day4(line);

                if (d4p1.IsRealRoom())
                {
                    sumOfSectorIds += d4p1.GetSectorId();
                }

                if (d4p1.Decrypt().StartsWith("north"))
                {
                    Console.WriteLine($"{d4p1.Decrypt()} -> {d4p1.GetSectorId()}");
                }
            }

            Console.WriteLine($"Puzzle 1: {sumOfSectorIds}");
        }

        private static void Day5Puzzle1()
        {
            var d5p1 = new Day5.Day5Part1("cxdnnyjw");

            var password = d5p1.GetPassword(8);

            Console.WriteLine($"{password}");
        }

        private static void Day5Puzzle2()
        {
            var d5p2 = new Day5.Day5Part2("cxdnnyjw", 8);

            var password = d5p2.GetPassword(8);

            Console.WriteLine($"{password}");
        }

        private static void Day6Puzzle1()
        {
            var reader = new StreamReader(@"..\..\Day6\input.txt");
            var d6p1 = new Day6.Day6(reader.ReadToEnd());
            var mostFrequent = d6p1.GetRepeatedLetters(Day6.RepetitionCode.MostFrequent);
            var leastFrequent = d6p1.GetRepeatedLetters(Day6.RepetitionCode.LeastFrequent);

            Console.WriteLine($"Most frequent: {mostFrequent}");
            Console.WriteLine($"Least frequent: {leastFrequent}");
        }

        private static void Day7Puzzle1()
        {
            int validAddresses = 0;
            var reader = new StreamReader(@"..\..\Day7\input.txt");

            var d7p1 = new Day7.Day7Part1();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (d7p1.IPAddressIsAbba(line))
                {
                    validAddresses++;
                }
            }

            Console.WriteLine($"Puzzle 1: {validAddresses}");
        }

        private static void Day7Puzzle2()
        {
            int addressesSupportingSsl = 0;
            var reader = new StreamReader(@"..\..\Day7\input.txt");

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var d7p2 = new Day7.Day7Part2(line);

                if (d7p2.SupportsSsl())
                {
                    addressesSupportingSsl++;
                }
            }

            Console.WriteLine($"Puzzle 2: {addressesSupportingSsl}");
        }

        private static void Day8()
        {
            var lines = File.ReadAllLines(@"..\..\Day8\input.txt");

            var d8 = new Day8.Day8(lines);

            d8.Parse();
            d8.ShowDisplay();
            Console.WriteLine($"{d8.GetPixelCount()} pixels");
        }

        private static void Day9()
        {
            var lines = File.ReadAllLines(@"..\..\Day9\input.txt");

            var d9 = new Day9.Day9();

            var result = d9.Decompress(string.Join("", lines));
            Console.WriteLine($"Decompressed length = {result}");

            result = d9.Decompress(string.Join("", lines), recurse: true);
            Console.WriteLine($"Decompressed length v2 = {result}");
        }

        private static void Day10()
        {
            var lines = File.ReadAllLines(@"..\..\Day10\input.txt");

            var d10 = new Day10.Day10(lines);

            d10.Process();
        }

        private static void Day11()
        {
            var d11 = new Day11.Day11();
            var result = d11.Process();

            Console.WriteLine($"Distance = {result}");
        }

        private static void Day12()
        {
            var lines = File.ReadAllLines(@"..\..\Day12\input.txt");

            var d12 = new Day12.Day12(lines);

            d12.Run();
            Console.WriteLine($"{d12.RegisterA}");

            d12.RegisterA = 0; d12.RegisterB = 0; d12.RegisterC = 1; d12.RegisterD = 0;
            d12.Run();
            Console.WriteLine($"{d12.RegisterA}");
        }

        private static void Day13()
        {
            var d13 = new Day13.Day13(1350);

            Console.WriteLine($"Shortest distance = {d13.FindShortestPath(31, 39)}");
            Console.WriteLine($"Number of locations at distance 50 = {d13.FindNumberOfLocations(50)}");
        }

        private static void Day14()
        {
            var d14 = new Day14.Day14("jlmsuwbz", 2016);

            d14.CollectKeys();
        }

        private static void Day15()
        {
            int time;

            var discs = new Day15.Disc[]
            {
                new Day15.Disc { Positions = 5 , Current = 2 },
                new Day15.Disc { Positions = 13 , Current = 7 },
                new Day15.Disc { Positions = 17 , Current = 10 },
                new Day15.Disc { Positions = 3 , Current = 2 },
                new Day15.Disc { Positions = 19 , Current = 9 },
                new Day15.Disc { Positions = 7 , Current = 0 },
                new Day15.Disc { Positions = 11 , Current = 0 }
            };

            var p = new Day15.Day15(0, discs);
            time = p.FindTimeSlot();

            Console.WriteLine($"Capsule falls through all discs at time {time}");
        }

        private static void Day16()
        {
            var d16 = new Day16.Day16("10111011111001111");

            var result = d16.Checksum(d16.FillToLength(35651584));

            Console.WriteLine($"{result}");
        }

        private static void Day17()
        {
            var d17 = new Day17.Day17("udskfozm");

            Console.WriteLine($"Shortest path = {d17.ShortestPath(3, 3)}");
            Console.WriteLine($"Longest path = {d17.LongestPath(3, 3)}");
        }

        private static void Day18()
        {
            var lines = File.ReadAllLines(@"..\..\Day18\input.txt");
            var d18 = new Day18.Day18(lines[0]);

            Console.WriteLine($"Safe tiles 40 rows = {d18.CountSafeTiles(40)}");
            Console.WriteLine($"Safe tiles 400000 rows = {d18.CountSafeTiles(400000)}");
        }

        private static void Day19()
        {
            var number = 3018458;
            var d19 = new Day19.Day19();

            Console.WriteLine($"Remaining elf part 1 = {d19.RemainingElfPuzzle1(number)}");
            Console.WriteLine($"Remaining elf part 2 = {d19.FastRemainingElfPuzzle2(number)}");
        }

        private static void Day20()
        {
            var blacklist = File.ReadAllLines(@"..\..\Day20\input.txt");
            var d20 = new Day20.Day20(UInt32.MaxValue, blacklist);

            Console.WriteLine($"{d20.GetFirstValidAddress()}");
            Console.WriteLine($"{d20.NumberOfValidAddresses()}");
        }

        private static void Day21()
        {
            var instructions = File.ReadAllLines(@"..\..\Day21\input.txt");
            var d21 = new Day21.Day21(instructions);

            Console.WriteLine($"Scrambled password = {d21.Scramble("abcdefgh")}");
            Console.WriteLine($"Unscrambled password = {d21.BruteForcePassword("fbgdceah")}");
        }

        private static void Day22()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var input = File.ReadAllLines(@"..\..\Day22\input.txt");
            var d22 = new Day22.Day22(input);
            Console.WriteLine($"{d22.ViablePairs()} viable pairs");
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
            Console.WriteLine();
            d22.PrintMaze();
        }

        private static void Day23()
        {
            var input = File.ReadAllLines(@"..\..\Day23\input.txt");
            var d23 = new Day23.Day23(input);
            d23.RegisterA = 7;
            d23.Run();
            Console.WriteLine($"{d23.RegisterA}");
        }

        private static void Day24()
        {
            var input = File.ReadAllLines(@"..\..\Day24\input.txt");
            var d24 = new Day24.Day24(input);

            Console.WriteLine($"Shortest path = {d24.FindShortestPath()}");
            Console.WriteLine($"Shortest cycle = {d24.FindShortestPath(cycle: true)}");
        }

        private static void Day25()
        {
            var input = File.ReadAllLines(@"..\..\Day25\input.txt");
            var d25 = new Day25.Day25(input);
            Console.WriteLine($"Register A = {d25.Run()}");
        }
    }
}
