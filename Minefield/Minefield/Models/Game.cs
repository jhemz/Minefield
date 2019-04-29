using Minefield.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Models
{
    //Model for the Game

    public class Game
    {
        public Difficulty Difficulty { get; set; }

        public List<Tile> Board { get; set; }

        public int BoardWidth { get; set; }

        public int BoardHeight { get; set; }
    }
}
