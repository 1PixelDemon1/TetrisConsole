using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisConsole
{
    
    class Game
    {
        public static bool Over = false;
        public static int Speed = 100;
        public static Window window;
        public static int Score = 0;
        public static void Main()
        {
            window = new Window();                
            ConsoleColor newForeColor = ConsoleColor.Blue;          
            Console.ForegroundColor = newForeColor;
            GameIteration();
        }


        public static string GetKey()         
        {
            string k = " ";
            while (Console.KeyAvailable)
            {
                k = Console.ReadKey(true).Key.ToString();
            }
            return k;            
        }

        public static void checkLines() 
        {
            for (var line = 0; line < window.Height - 2; line++) 
            {
                bool flag = true;
                for (var i = 0; i < window.Width - 2; i++)
                {
                    if (window.GetFrozenLine(line).Contains(" ")) 
                    {
                        flag = false;
                        break;                           
                    }
                    
                }
                if (flag) 
                {
                    Game.Score += 200;
                    window.ClearFrozenLine(line);                    
                }
            }           
        }

        public static void GameIteration() 
        {            
            ITetramino tet = new TTetra(new int[] { window.Width/2, 0 });
            ITetramino nextTet = new STetra(new int[] { window.Width / 2, 0 });
            string nextTetStr = "#";
            
            while (!Game.Over)
            {
                window.ClearWindow();
                
                string key = Game.GetKey();                 

                if (key == "Spacebar")
                {
                    tet.Rotate();
                }

                if (key == "D") 
                {
                    tet.Step("RIGHT");
                }
                
                if (key == "A")
                {
                    tet.Step("LEFT");
                }
                
                tet.Step("DOWN");
                if (!tet.isActive) 
                {
                    Game.Speed += 5;
                    window.AddFrozen(tet.coords, tet.signs);
                    Game.Score += 50;
                    tet = nextTet;
                    switch (new Random().Next(0, 3)) 
                    {
                        case 0:
                            nextTet = new LITetra(new int[] { window.Width / 2, 0 });
                            nextTetStr = "|";
                            break;
                        case 1:
                            nextTet = new TTetra(new int[] { window.Width / 2, 0 });
                            nextTetStr = "T";
                            break;
                        case 2:
                            nextTet = new STetra(new int[] { window.Width / 2, 0 });
                            nextTetStr = "#";
                            break;
                        default:
                            break;
                    }
                }


                tet.Display();
                Game.checkLines();
                window.Display();

                Console.WriteLine($"Players`s score is: {Score} points");
                Console.WriteLine($"Next Tet is: {nextTetStr}");
                
                Thread.Sleep(10000/Game.Speed);                        
                
                if (key == "Escape") 
                {
                    Game.Over = true;
                }
            }            
        }
    }
}
