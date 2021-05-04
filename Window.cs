using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
    class Window
    {
        private StringBuilder Field = new StringBuilder("╔══════════╗\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "║          ║\n" +
                                                        "╚══════════╝\n");
        public string gameField { get; set; }        
        public int Width { get; }
        public int Height { get; }
        public Window()
        {
            gameField = Field.ToString();
            Width = 12;
            Height = 22;
        }

        public void ClearWindow()
        {
            Console.Clear();
            gameField = Field.ToString();
        }

        public void Display()
        {
            Console.WriteLine(gameField);
        }

        public void DrawSprite(int[][] coords, char[] signs) 
        {            
            char[] chars = gameField.ToCharArray();
            
            for (var i = 0; i < signs.Length; i++) 
            {
                chars[(Width * (coords[i][1] + 1)) + coords[i][0] + coords[i][1] + 2] = signs[i];
            }

            gameField = new string(chars);
        }

        public char GetSign(int x, int y) => gameField[(Width * (y + 1)) + x + y + 2];

        public string GetFrozenLine(int line) => Field.ToString().Substring((Width * (line + 1)) + line + 2, Width  - 2);

        public string GetLine(int line) => gameField.Substring((Width * (line + 1)) + line + 2, Width - 2);

        public void AddFrozen(int[][] coords, char[] signs) 
        {
            char[] chars = Field.ToString().ToCharArray();

            for (var i = 0; i < signs.Length; i++)
            {
                chars[(Width * (coords[i][1] + 1)) + coords[i][0] + coords[i][1] + 2] = signs[i];
            }

            Field = new StringBuilder(new string(chars));
        }

        public void ClearFrozenLine(int line) 
        {
            char[] chars = Field.ToString().ToCharArray();

            for (int line1 = line; line1 > 0; line1--) 
            {
                string pastLine = GetFrozenLine(line1 - 1);
                for (var i = 0; i < Width - 2; i++)
                {
                    chars[(Width * (line1 + 1)) + i + line1 + 2] = pastLine.ToCharArray()[i];
                }
            }

            for (var i = 0; i < Width - 2; i++)
            {
                chars[Width + i + 2] = ' ';
            }

            Field = new StringBuilder(new string(chars));
        }
    }
}
