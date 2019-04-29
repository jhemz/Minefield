using Minefield.Enums;
using Minefield.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Minefield.Viewmodels
{
    public class MainVM : VM_Base
    {
        //Main Viewmodel

        public MainVM()
        {
            //link the commands and their functions
            UpCommand = new Command(() => UpFunction());
            DownCommand = new Command(() => DownFunction());
            LeftCommand = new Command(() => LeftFunction());
            RightCommand = new Command(() => RightFunction());
            NewEasyGameCommand = new Command(() => NewEasyGameFunction());
            NewMediumGameCommand = new Command(() => NewMediumGameFunction());
            NewHardGameCommand = new Command(() => NewHardGameFunction());

            //set that no game currently exists
            GameExists = false;
        }

        //the instance of the current game
        private GameVM _Game;
        public GameVM Game
        {
            get
            {
                return _Game;
            }
            set
            {
                _Game = value;
                OnPropertyChanged("Game");
            }
        }

        //instance of the player
        private PlayerVM _Player;
        public PlayerVM Player
        {
            get
            {
                return _Player;
            }
            set
            {
                _Player = value;
                OnPropertyChanged("Player");
            }
        }

        //players X coord
        public int Player_X
        {
            get
            {
                int _Player_X = 0;
                if (Player != null)
                {
                    _Player_X = Player.Coordinates.X;
                }
                return _Player_X;
            }
            set
            {
                if (Player != null)
                {
                    Player.Coordinates.X = value;
                    OnPropertyChanged("Player_X");
                }
            }
        }

        //players Y coord
        public int Player_Y
        {
            get
            {
                int Player_Y = 0;
                if (Player != null)
                {
                    Player_Y = Player.Coordinates.Y;
                }
                return Player_Y;
            }
            set
            {
                if (Player != null)
                {
                    Player.Coordinates.Y = value;
                    OnPropertyChanged("Player_Y");
                }
            }
        }

        //returns the players score, stored on the player model
        public int Score
        {
            get
            {
                int _Score  = 0;
                if(Player != null)
                {
                    _Score = Player.Moves;
                }
                return _Score;
            }
            set
            {
                if (Player != null)
                {
                    Player.Moves = value;
                    OnPropertyChanged("Score");
                }
               
            }
        }

        //returns the players lives left, stored on the player model
        public int Lives
        {
            get
            {
                int _Lives = 0;
                if (Player != null)
                {
                    _Lives = Player.Lives;
                }
                return _Lives;
            }
            set
            {
                if (Player != null)
                {
                    Player.Lives = value;
                    OnPropertyChanged("Lives");
                }

            }
        }

        //the visibilty for the new game menu
        public bool NoGame
        {
            get
            {
                return !GameExists;
            }
        }

        //the visibilty for the game board
        private bool _GameExists;
        public bool GameExists
        {
            get
            {
                return _GameExists;
            }
            set
            {
                _GameExists = value;
                OnPropertyChanged("GameExists");
                OnPropertyChanged("NoGame");
            }
        }

        //the visibilty for the game board
        private bool _ExplosionVisible;
        public bool ExplosionVisible
        {
            get
            {
                return _ExplosionVisible;
            }
            set
            {
                _ExplosionVisible = value;
                OnPropertyChanged("ExplosionVisible");
            }
        }

        #region functions

        //function to start a new game
        public void NewGame(Difficulty difficulty)
        {
            Game = new GameVM(difficulty);
            Player = new PlayerVM(difficulty);
            GameExists = true;
            Player_X = 0;
            Player_Y = 0;
            ExplosionVisible = false;
            OnPropertyChanged("Lives");
            OnPropertyChanged("Score");
        }

        public bool LoseLife()
        {
            bool stillAlive = false;

            if (Lives > 0)
            {
                //deduct a life
                Lives -= 1;

                //set still alive to true
                stillAlive = true;
            }

            return stillAlive;
        }

        public async Task ShowExplosion()
        {
            //show explosion for 500ms then hide, and lock ui
            ExplosionVisible = true;
            await Task.Run(() =>
             {
                 Thread.Sleep(500);
                 ExplosionVisible = false;
             });
        }

        public async Task MoveAsync(Direction direction)
        {
            //only allow movement if the explosion is not visible
            if (!ExplosionVisible)
            {
                bool moveResult = MovePlayer(direction);
                if (moveResult == false)  //could not move that direction
                {
                    //invalid move
                }
                else  //move was succesful
                {
                    //check to see if the tile the player is now on contains a mine
                    bool tileContainsMine = TileContainsMine(Player.Coordinates);

                    if (tileContainsMine) //tile had a mine
                    {
                        //boom!
                        await ShowExplosion();

                        //player now loses a life
                        bool StillAlive = LoseLife();
                        if (StillAlive == false)
                        {
                            //game over
                            MessagingCenter.Send<MainVM>(this, "GameOver");
                            GameExists = false;
                            
                        }
                        else //still alive, go back to start
                        {
                            Player_X = 0;
                            Player_Y = 0;
                        }
                    }
                    else //tile was empty
                    {
                        //add 1 point to move score
                        Score += 1;

                        if (Player_X == 7 && Player_Y == 7)
                        {
                            //Winner!
                            MessagingCenter.Send<MainVM, PlayerVM>(this, "Winner", Player);
                            GameExists = false;
                        }
                    }
                }
            }
        }

        public bool MovePlayer(Direction direction)
        {
            bool moveSuccess = false;
            switch (direction)
            {
                case Direction.Left:
                    if (Player.Coordinates.X > 0)
                    {
                        Player_X -= 1;
                        moveSuccess = true;
                    }
                    break;
                case Direction.Right:
                    if (Player.Coordinates.X < Game.BoardWidth - 1)
                    {
                        Player_X += 1;
                        moveSuccess = true;
                    }
                    break;
                case Direction.Up:
                    if (Player.Coordinates.Y > 0)
                    {
                        Player_Y -= 1;
                        moveSuccess = true;
                    }
                    break;
                case Direction.Down:
                    if (Player.Coordinates.Y < Game.BoardHeight - 1)
                    {
                        Player_Y += 1;
                        moveSuccess = true;
                    }
                    break;
            }
            return moveSuccess;
        }

        public bool TileContainsMine(Coordinates coordinates)
        {
            bool containsMine = false;

            if (Game.Board[coordinates.X, coordinates.Y].ContainsMine)
            {
                containsMine = true;
            }
            return containsMine;
        }
        #endregion

        #region commands

        //command for the NewEasyGame button
        public ICommand NewEasyGameCommand { get; private set; }
        //function for the NewEasyGame button
        public void NewEasyGameFunction()
        {
            NewGame(Difficulty.easy);
        }

        //command for the NewMediumGame button
        public ICommand NewMediumGameCommand { get; private set; }
        //function for the NewMediumGame button
        public void NewMediumGameFunction()
        {
            NewGame(Difficulty.medium);
        }

        //command for the NewHardGame button
        public ICommand NewHardGameCommand { get; private set; }
        //function for the NewHardGame button
        public void NewHardGameFunction()
        {
            NewGame(Difficulty.hard);
        }

        //command for the up button
        public ICommand UpCommand { get; private set; }
        //function for the up button
        public void UpFunction()
        {
            MoveAsync(Direction.Up);
        }

        //command for the down button
        public ICommand DownCommand { get; private set; }
        //function for the down button
        public void DownFunction()
        {
            MoveAsync(Direction.Down);
        }

        //command for the Left button
        public ICommand LeftCommand { get; private set; }
        //function for the Left button
        public void LeftFunction()
        {
            MoveAsync(Direction.Left);
        }

        //command for the Right button
        public ICommand RightCommand { get; private set; }
        //function for the Right button
        public void RightFunction()
        {
            MoveAsync(Direction.Right);
        }

        #endregion
    }
}
