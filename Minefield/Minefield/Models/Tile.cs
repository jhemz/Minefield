using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Models
{
    public class Tile
    {
        //Model for a tile

        public bool ContainsMine { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
