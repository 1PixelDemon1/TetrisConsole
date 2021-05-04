using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
    class LITetra : ITetramino
    {
        public int[][] coords { get; set; }
        public char[] signs { get; set; }
        public bool isActive { get; set; }
        public int rotationState { get; set; }

        public LITetra(int[] coords)
        {
            //CENTER IS MIDDLE PIXEL
            this.coords = new int[4][];
            this.coords[0] = coords;                                                //1             1
            this.coords[1] = new int[] { coords[0], coords[1] + 1 };                //2             2
            this.coords[2] = new int[] { coords[0], coords[1] + 2 };                //3             3
            this.coords[3] = new int[] { coords[0], coords[1] + 3 };                //4             4

            this.signs = new char[4];
            this.signs[0] = '@';
            this.signs[1] = '@';
            this.signs[2] = '@';
            this.signs[3] = '@';

            isActive = true;
            rotationState = 0;
        }
        public bool Collision(int[][] coords)
        {
            foreach (int[] xy in coords)
            {
                if (Game.window.GetSign(xy[0], xy[1]) != ' ')
                    return true;
            }

            return false;
        }

        public void Display()
        {
            Game.window.DrawSprite(coords, signs);
        }

        public void Rotate()
        {
            switch (rotationState)
            {
                case 0:
                    rotationState = 1;
                    coords[0] = new int[] { coords[0][0] + 1, coords[0][1] + 2 };
                    coords[1] = new int[] { coords[1][0], coords[1][1] + 1 };
                    coords[2] = new int[] { coords[2][0] - 1, coords[2][1] };
                    coords[3] = new int[] { coords[3][0] - 2, coords[3][1] - 1 };
                    if (Collision(coords)) 
                    {
                        coords[0] = new int[] { coords[0][0] - 1, coords[0][1]};
                        coords[1] = new int[] { coords[1][0] - 1, coords[1][1]};
                        coords[2] = new int[] { coords[2][0] - 1, coords[2][1]};
                        coords[3] = new int[] { coords[3][0] - 1, coords[3][1]};
                    }
                    if (Collision(coords))
                    {
                        coords[0] = new int[] { coords[0][0] + 3, coords[0][1] };
                        coords[1] = new int[] { coords[1][0] + 3, coords[1][1] };
                        coords[2] = new int[] { coords[2][0] + 3, coords[2][1] };
                        coords[3] = new int[] { coords[3][0] + 3, coords[3][1] };
                    }

                    break;
                case 1:
                    rotationState = 2;
                    coords[0] = new int[] { coords[0][0] - 2, coords[0][1] + 1 };
                    coords[1] = new int[] { coords[1][0] - 1, coords[1][1] };
                    coords[2] = new int[] { coords[2][0], coords[2][1] - 1};
                    coords[3] = new int[] { coords[3][0] + 1, coords[3][1] - 2 };
                    break;
                case 2:
                    rotationState = 3;
                    coords[0] = new int[] { coords[0][0] - 1, coords[0][1] - 2 };
                    coords[1] = new int[] { coords[1][0], coords[1][1] - 1 };
                    coords[2] = new int[] { coords[2][0] + 1, coords[2][1] };
                    coords[3] = new int[] { coords[3][0] + 2, coords[3][1] + 1 };

                    if (Collision(coords))
                    {
                        coords[0] = new int[] { coords[0][0] + 1, coords[0][1] };
                        coords[1] = new int[] { coords[1][0] + 1, coords[1][1] };
                        coords[2] = new int[] { coords[2][0] + 1, coords[2][1] };
                        coords[3] = new int[] { coords[3][0] + 1, coords[3][1] };
                    }
                    if (Collision(coords))
                    {
                        coords[0] = new int[] { coords[0][0] - 3, coords[0][1] };
                        coords[1] = new int[] { coords[1][0] - 3, coords[1][1] };
                        coords[2] = new int[] { coords[2][0] - 3, coords[2][1] };
                        coords[3] = new int[] { coords[3][0] - 3, coords[3][1] };
                    }
                    break;
                case 3:
                    rotationState = 0;
                    coords[0] = new int[] { coords[0][0] + 2, coords[0][1] - 1 };
                    coords[1] = new int[] { coords[1][0] + 1, coords[1][1] };
                    coords[2] = new int[] { coords[2][0], coords[2][1] + 1 };
                    coords[3] = new int[] { coords[3][0] - 1, coords[3][1] + 2 };
                    break;
                default:
                    break;
            }
        }

        public void Step(string directions)
        {
            int[][] newCoords = new int[4][];

            if (directions.Contains("DOWN"))
            {
                newCoords[0] = new int[] { coords[0][0], coords[0][1] + 1 };
                newCoords[1] = new int[] { coords[1][0], coords[1][1] + 1 };
                newCoords[2] = new int[] { coords[2][0], coords[2][1] + 1 };
                newCoords[3] = new int[] { coords[3][0], coords[3][1] + 1 };

                if (!Collision(newCoords))
                    this.coords = newCoords;
                else
                    isActive = false;
            }

            if (directions.Contains("RIGHT"))
            {
                newCoords[0] = new int[] { coords[0][0] + 1, coords[0][1] };
                newCoords[1] = new int[] { coords[1][0] + 1, coords[1][1] };
                newCoords[2] = new int[] { coords[2][0] + 1, coords[2][1] };
                newCoords[3] = new int[] { coords[3][0] + 1, coords[3][1] };

                if (!Collision(newCoords))
                    this.coords = newCoords;
            }

            if (directions.Contains("LEFT"))
            {
                newCoords[0] = new int[] { coords[0][0] - 1, coords[0][1] };
                newCoords[1] = new int[] { coords[1][0] - 1, coords[1][1] };
                newCoords[2] = new int[] { coords[2][0] - 1, coords[2][1] };
                newCoords[3] = new int[] { coords[3][0] - 1, coords[3][1] };

                if (!Collision(newCoords))
                    this.coords = newCoords;
            }
        }
    }
}
