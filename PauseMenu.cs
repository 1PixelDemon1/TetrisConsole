using System;

namespace TetrisConsole
{
    class PauseMenu
    {
        public enum MenuTabs 
        {
            Continue,
            Exit            
        }
        // Symbols of the box.
        private char[] signs { get; set; }
        // Coordinates of the box.
        private int[][] coords { get; set; }
        // Currently chosen menu tab.
        public MenuTabs menuState { get; set; }

        public PauseMenu() 
        {
            menuState = MenuTabs.Continue;

            string[] tabs = Enum.GetNames(typeof(MenuTabs));

            // Box has paddings from the sides.
            // Two from the left and right.
            int width = Game.window.Width - 4;
            // And two from top and the bottom.
            int height = tabs.Length + 4;

            signs = new char[width * height];
            coords = new int[width * height][];

            Update();
        }

        public void ChangeTab(MenuTabs tab) 
        {
            menuState = tab;
            Update();
        }
        public void Display() 
        {
            Game.window.DrawSprite(coords, signs);
        }

        private void Update() 
        {            
            string[] tabs = Enum.GetNames(typeof(MenuTabs));
            
            int width = Game.window.Width - 4;            
            int height = tabs.Length + 4;

            // Defines top border.
            for (var i = 0; i < width; i++)
            {
                signs[i] = '^';
                coords[i] = new int[] { i + 1, 2 };
            }
            
            // Defines main content.
            for (var j = 1; j < height; j++)
            {
                signs[width * (j + 0)] = '^';
                signs[width * (j + 1) - 1] = '^';
                coords[width * (j + 0)] = new int[] { 1, j + 2 };
                coords[width * (j + 1) - 1] = new int[] { width, j + 2 };

                // Following code prints tab names.
                if ((j > 2) && (j < height - 1))
                {
                    string tab = String.Format($"{{0,{-width}}}", tabs[j - 3]);

                    // Current tab is highlighted.
                    if (tabs[j - 3] == menuState.ToString())                    
                        tab = tab.Insert(0, "*");                        
                    
                    for (var i = 1; i < width - 1; i++)
                    {
                        signs[width * (j + 0) + i] = tab[i - 1];
                        coords[width * (j + 0) + i] = new int[] { i + 1, j + 1 };
                    }
                }
                else
                {
                    for (var i = 1; i < width - 1; i++)
                    {
                        signs[(width * (j + 0)) + i] = ' ';
                        coords[(width * (j + 0)) + i] = new int[] { i + 1, j + 2 };
                    }
                }                      
            }
               
            //Defines bottom border.
            for (var i = 0; i < width - 1; i++)
            {
                signs[i + width * (height - 1)] = '^';
                coords[i + width * (height - 1)] = new int[] { i + 1, height + 1 };
            }
        }
    }
}
