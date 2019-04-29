using Minefield.Enums;
using Minefield.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Minefield.Viewmodels
{
    public class GameVM : VM_Base<Game>
    {
        //Viewmodel for the Game
        private Random rnd = new Random();

        public GameVM(Difficulty difficulty)
        {
            //create new model
            Model = new Game();

            //set game dimensions
            BoardWidth = 8;
            BoardHeight = 8;

            //set diffulty
            Difficulty = difficulty;

            //set board, based on difficulty
            Board = new TileVM[BoardWidth, BoardHeight];

            //We store the tiles as a list in the model. This is so that in future, if a save functionality is introduced, we can store the tile layout here
            Model.Board = new List<Tile>();
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    //use random number generator to decide if tile should contain a mine
                    bool containsMine = rnd.Next(1, (int)difficulty) == 2 ? true : false;

                    if(x == 7 && y == 7 || x == 0 && y == 0)
                    {
                        //we do not want to ever put a mine on the starting point or finish point, override the randomly generated if so
                        containsMine = false;
                    }

                    //create a model of the tile to store in the model
                    Tile tile = new Tile()
                    {
                        ContainsMine = containsMine,
                        Coordinates = new Coordinates(x, y)
                    };
                    Model.Board.Add(tile);

                    //create and set the TileVM in the board for the UI to access
                    Board[x, y] = new TileVM(x, y, containsMine);
                }
            }
        }
      
        public int BoardWidth
        {
            get
            {
                return Model.BoardWidth;
            }
            set
            {
                Model.BoardWidth = value;
                OnPropertyChanged("BoardWidth");
            }
        }
       
        public int BoardHeight
        {
            get
            {
                return Model.BoardHeight;
            }
            set
            {
                Model.BoardHeight = value;
                OnPropertyChanged("BoardHeight");
            }
        }

        public Difficulty Difficulty
        {
            get
            {
                return Model.Difficulty;
            }
            set
            {
                Model.Difficulty = value;
                OnPropertyChanged("Difficulty");
            }
        }

        private TileVM[,] _Board;
        public TileVM[,] Board
        {
            get
            {
                return _Board;
            }
            set
            {
                _Board = value;
                OnPropertyChanged("Board");
            }
        }



    }
}
