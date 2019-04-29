using Minefield.Viewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Minefield
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainVM();

            MessagingCenter.Subscribe<MainVM, PlayerVM>(this, "Winner", (sender, Player) => {
                DisplayAlert("Winner!", String.Format("Congratulations! You win! Score: {0}, Lives Left: {1}", Player.Moves, Player.Lives), "ok");
            });

            MessagingCenter.Subscribe<MainVM>(this, "GameOver", (sender) => {
                DisplayAlert("Game Over", "You lost all your lives, game over :(", "ok");
            });
        }
    }
}
