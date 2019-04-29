using Minefield.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield.Viewmodels
{
    public class TileVM : VM_Base<Tile>
    {
        //ViewmodelModel for a tile

        public TileVM(int x, int y, bool containsMine)
        {
            //set model
            Model = new Tile();

            //set coordinates
            Coordinates = new Coordinates(x, y);

            //set conaitnsMine
            ContainsMine = containsMine;
        }
        public bool ContainsMine
        {
            get
            {
                return Model.ContainsMine;
            }
            set
            {
                Model.ContainsMine = value;
                OnPropertyChanged("ContainsMine");
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
