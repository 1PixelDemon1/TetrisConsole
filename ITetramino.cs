using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisConsole
{
    interface ITetramino
    {
        public int[][] coords { get; set; }

        public char[] signs { get; set; }

        public bool isActive { get; set; }

        public int rotationState { get; set; }

        public void Rotate();

        public void Step(string directions);

        public bool Collision(int[][] coords);

        public void Display();
    }
}
