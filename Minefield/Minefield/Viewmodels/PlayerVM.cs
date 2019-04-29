using Minefield.Enums;
using Minefield.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Viewmodels
{
    public class PlayerVM : VM_Base<Player>
    {

        //Viewmodel for the player

        public PlayerVM(Difficulty difficulty)
        {
            //set model
            Model = new Player();

            //set coordinates
            Coordinates = new Coordinates(0, 0);

            //set lives
            switch (difficulty)
            {
                case Difficulty.easy:
                    Lives = 9;
                    break;
                case Difficulty.medium:
                    Lives = 6;
                    break;
                case Difficulty.hard:
                    Lives = 3;
                    break;
            }

            //set moves
            Moves = 0;
        }

        public int Moves
        {
            get
            {
                return Model.Moves;
            }
            set
            {
                Model.Moves = value;
                OnPropertyChanged("Moves");
            }
        }

        public int Lives
        {
            get
            {
                return Model.Lives;
            }
            set
            {
                Model.Lives = value;
                OnPropertyChanged("Lives");
            }
        }

        public Coordinates Coordinates
        {
            get
            {
                return Model.Coordinates;
            }
            set
            {
                Model.Coordinates = value;
                OnPropertyChanged("Coordinates");
            }
        }
    }
}
