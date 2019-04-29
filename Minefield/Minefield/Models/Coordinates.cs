using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Models
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
