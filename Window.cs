using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
    class Window
    {
        // Window template.
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

        // Main game field.
        public string GameField { get; set; }        
        public int Width { get; }
        public int Height { get; }
        
        public Window()
        {
            GameField = Field.ToString();
            Width = 12;
            Height = 22;
        }

        public Window(int Width, int Height)
        {
            if (Width < 6 || Height < 6) 
            {
                GameField = Field.ToString();
                Width = 12;
                Height = 22;
            }
            this.Width = Width;
            this.Height = Height;
            Field.Clear();

            Field.Append("╔");
            for (var i = 0; i < Width - 2; i++)
            {
                Field.Append("═");
            }
            Field.Append("╗\n");
            for (var j = 0; j < Height-2; j++)
            {
                Field.Append("║");
                for (var i = 0; i < Width - 2; i++)
                {
                    Field.Append(" ");
                }
                Field.Append("║\n");
            }
            Field.Append("╚");
            for (var i = 0; i < Width - 2; i++)
            {
                Field.Append("═");
            }
            Field.Append("╝\n");

            GameField = Field.ToString();            
        }

        public void ClearWindow()
        {
            Console.Clear();
            GameField = Field.ToString();
        }

        public void Display()
        {
            Console.WriteLine(GameField);
        }

        public void DrawSprite(int[][] coords, char[] signs) 
        {
            // Fills the game field with given signs[index] in given coords[index][(0 - x/ 1 - y)] (without frame).
            char[] chars = GameField.ToCharArray();
            
            for (var i = 0; i < signs.Length; i++) 
            {
                chars[(Width * (coords[i][1] + 1)) + coords[i][0] + coords[i][1] + 2] = signs[i];
            }

            GameField = new string(chars);
        }

        public char GetSign(int x, int y) => GameField[(Width * (y + 1)) + x + y + 2]; // Returns the sign in given coordinates (without frame).

        public string GetFrozenLine(int line) => Field.ToString().Substring((Width * (line + 1)) + line + 2, Width  - 2); // Returns the string of template of signs on given line (without frame).

        public string GetLine(int line) => GameField.Substring((Width * (line + 1)) + line + 2, Width - 2); // Returns the string of game field of signs on given line (without frame).

        public void AddFrozen(int[][] coords, char[] signs) 
        {
            // Does the same thing as DrawSprite(int[][] coords, char[] signs), but in template.
            char[] chars = Field.ToString().ToCharArray();

            for (var i = 0; i < signs.Length; i++)
            {
                chars[(Width * (coords[i][1] + 1)) + coords[i][0] + coords[i][1] + 2] = signs[i];
            }

            Field = new StringBuilder(new string(chars));
        }

        public void ClearFrozenLine(int line) 
        {
            // Deletes one line from template and moves all upper lines one level below.

            char[] chars = Field.ToString().ToCharArray();
            
            for (int line1 = line; line1 > 0; line1--) 
            {
                string pastLine = GetFrozenLine(line1 - 1);
                for (var i = 0; i < Width - 2; i++)
                {
                    chars[(Width * (line1 + 1)) + i + line1 + 2] = pastLine.ToCharArray()[i];
                }
            }

            // Following loop deletes the top level line.
            for (var i = 0; i < Width - 2; i++)
            {
                chars[Width + i + 2] = ' ';
            }

            Field = new StringBuilder(new string(chars));
        }
    }
}
