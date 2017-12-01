using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Day8
{
    public class Day8
    {
        private int[,] pixels;
        private string[] lines;

        public Day8(string[] lines)
        {
            this.pixels = new int[50, 6];
            this.lines = lines;
        }

        public void Parse()
        {
            foreach (var line in lines)
            {
                var tokens = line.Split(' ');

                if (tokens[0] == "rect")
                    ParseRect(tokens);
                if (tokens[0] == "rotate" &&
                    tokens[1] == "row")
                    ParseRotateRow(tokens);
                if (tokens[0] == "rotate" &&
                    tokens[1] == "column")
                    ParseRotateColumn(tokens);

            }
        }

        private void ParseRect(string[] tokens)
        {
            var dimensions = tokens[1].Split('x');

            int rows, columns;

            int.TryParse(dimensions[0], out columns);
            int.TryParse(dimensions[1], out rows);

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    pixels[x, y] = 1;
        }

        private void ParseRotateRow(string[] tokens)
        {
            int row, count;

            int.TryParse(tokens[2].Split('=')[1], out row);
            int.TryParse(tokens[4], out count);

            for (int i = 0; i < count; i++)
            {
                int t = pixels[49, row];
                for (int j = 49; j > 0; j--)
                    pixels[j, row] = pixels[j - 1, row];
                pixels[0, row] = t;
            }

        }

        private void ParseRotateColumn(string[] tokens)
        {
            int column, count;

            int.TryParse(tokens[2].Split('=')[1], out column);
            int.TryParse(tokens[4], out count);

            for (int i = 0; i < count; i++)
            {
                int t = pixels[column, 5];
                for (int j = 5; j > 0; j--)
                    pixels[column, j] = pixels[column, j - 1];
                pixels[column, 0] = t;
            }
        }

        public void ShowDisplay()
        {
            string row;

            for (int y = 0; y < 6; y++)
            {
                row = "";

                for (int x = 0; x < 50; x++)
                    row += (pixels[x, y] == 1) ? "#" : ".";

                Console.WriteLine($"{row}");
            }
        }

        public int GetPixelCount()
        {
            return pixels
                    .Cast<int>()
                    .Aggregate((a, b) => a + b);
        }
    }
}
