using System;
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
            //window = new Window(Width, Height);
            window = new Window();
            Console.ForegroundColor = ConsoleColor.Green;            
            GameIteration();
        }


        public static string GetKey()         
        {
            // Returns the currently pressed key.
            string k = " ";
            while (Console.KeyAvailable)
            {
                // This loop clears the input stack.
                k = Console.ReadKey(true).Key.ToString();
            }
            return k;            
        }

        public static void checkLines() 
        {
            // Scans horizontal lines and deletes them from window template if they are filled.
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
            // Main game loop.

            ITetramino tet = new LLTetra(new int[] { (window.Width / 2) - 1, 0 });
            ITetramino nextTet = new STetra(new int[] { (window.Width / 2) - 1, 0 });
            string nextTetStr = "#";

            bool isDown = false;

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

                if (key == "S")
                {
                    isDown = true;
                }

                if (isDown) 
                {
                    tet.Step("DOWN");
                }
                isDown = !isDown;

                // Following section checks if the tetramino has collided the bottom/another tetramino.
                if (!tet.isActive) 
                {
                    Game.Speed += 5;
                    Game.Score += 50;

                    // Adding the fallen tetramino to window template.
                    window.AddFrozen(tet.coords, tet.signs);
                    
                    tet = nextTet;
                    switch (new Random().Next(0, 5)) 
                    {
                        case 0:
                            nextTet = new LITetra(new int[] { (window.Width / 2) - 1, 0 });
                            nextTetStr = "|";
                            break;
                        case 1:
                            nextTet = new TTetra(new int[] { (window.Width / 2) - 1, 0 });
                            nextTetStr = "T";
                            break;
                        case 2:
                            nextTet = new STetra(new int[] { (window.Width / 2) - 1, 0 });
                            nextTetStr = "#";
                            break;
                        case 3:
                            nextTet = new RLTetra(new int[] { (window.Width / 2) - 1, 0 });
                            nextTetStr = "L";
                            break;
                        case 4:
                            nextTet = new LLTetra(new int[] { (window.Width / 2) - 1, 0 });
                            nextTetStr = "¬";
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

                // If the top level line is filled with tetraminos.
                if (key == "Escape" || window.GetFrozenLine(0).Contains("@"))
                {
                    Game.Over = true;
                }
            }            
        }
    }
}
