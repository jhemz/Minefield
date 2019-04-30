using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minefield.Enums;
using Minefield.Viewmodels;

namespace Minefield_UnitTest
{
    [TestClass]
    public class MainVMTest
    {
        MainVM MainVM;
        [TestInitialize]
        public void Init()
        {
            //initialization here

            MainVM = new MainVM();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //cleanup here
        }

        [TestMethod]
        public void TestPlayerRunsOutOFLives()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();
            MainVM.Lives = 0;
            bool stillAlive = MainVM.LoseLife();
            Assert.AreEqual(false, stillAlive);

        }

        [TestMethod]
        public void TestPlayerCannotMoveOutOfBoardBounds_LEFT()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();

            bool canMove = MainVM.MovePlayer(Direction.Left);

            Assert.AreEqual(false, canMove);

        }

        [TestMethod]
        public void TestPlayerCannotMoveOutOfBoardBounds_RIGHT()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();
            MainVM.Player_X = 7;
            bool canMove = MainVM.MovePlayer(Direction.Right);

            Assert.AreEqual(false, canMove);

        }

        [TestMethod]
        public void TestPlayerCannotMoveOutOfBoardBounds_UP()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();

            bool canMove = MainVM.MovePlayer(Direction.Up);

            Assert.AreEqual(false, canMove);

        }

        [TestMethod]
        public void TestPlayerCannotMoveOutOfBoardBounds_DOWN()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();
            MainVM.Player_Y = 7;
            bool canMove = MainVM.MovePlayer(Direction.Down);

            Assert.AreEqual(false, canMove);

        }

        [TestMethod]
        public void TestTileContainsMine()
        {
            //test that game ends when lives are 0

            MainVM.NewEasyGameFunction();

            //add a mine to a coord we define
            MainVM.Game.Board[1, 1].ContainsMine = true;

            //detect it
            bool containsMine = MainVM.TileContainsMine(new Minefield.Models.Coordinates(1, 1));

            Assert.AreEqual(true, containsMine);

        }
    }
}
