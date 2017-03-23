using System;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController g;

        [Test]
        public void FlagFlagsTile()
        {
            g = new GameController(2, 0);
            g.Flag(0, 0);
            g.GameBoard.Tiles[0, 0].Flagged.ShouldBeTrue();
        }

        [Test]
        public void ClickRevealsTile()
        {
            g = new GameController(2, 0);
            g.Click(10, 10);
            g.GameBoard.Tiles[0, 0].Revealed.ShouldBeTrue();
        }

        [Test]
        public void ClickDoesNothingIfFlagged()
        {
            g = new GameController(2, 0);
            g.GameBoard.Tiles[0, 0].Flagged = true;
            g.Click(10, 10);
            g.GameBoard.Tiles[0, 0].Revealed.ShouldBeFalse();
            g.GameBoard.Tiles[0, 0].Flagged.ShouldBeTrue();
        }

        [Test]
        public void ClickRelocatesMineIfFirstClick()
        {
            g = new GameController(2, 0);
            g.GameBoard.Tiles[1, 1].IsMine = true;
            g.Click(20, 20);
            g.GameBoard.Tiles[0, 0].IsMine.ShouldBeTrue();
            g.GameBoard.Tiles[1, 1].IsMine.ShouldBeFalse();
        }

        [Test]
        public void CheckForEmptyRevealsAllTilesAroundTile()
        {
            g = new GameController(3, 0);
            g.GameBoard.Tiles = new Tile[3, 3] {
                {new Tile(0, 0), new Tile(0, 1), new Tile(0, 2)},
                {new Tile(1, 0), new Tile(1, 1), new Tile(1, 2)},
                {new Tile(2, 0), new Tile(2, 1), new Tile(2, 2)}
            };
            g.GameBoard.Tiles[0, 0].AdjacentMines = 1;
            g.GameBoard.Tiles[0, 1].AdjacentMines = 1;
            g.GameBoard.Tiles[0, 2].AdjacentMines = 1;
            g.GameBoard.Tiles[1, 0].AdjacentMines = 1;
            g.GameBoard.Tiles[1, 1].Revealed = true;
            g.GameBoard.Tiles[1, 2].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 0].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 1].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 2].AdjacentMines = 1;
            g.CheckForEmptyReveals(g.GameBoard.Tiles[1, 1]);
            foreach(Tile t in g.GameBoard.Tiles)
            {
                t.Revealed.ShouldBeTrue();
            }

        }

        [Test]
        public void CheckForEmptyRevealsKeepsGoingUntilAdjacent()
        {
            g = new GameController(4, 0);
            g.GameBoard.Tiles = new Tile[4, 4] {
                {new Tile(0, 0), new Tile(0, 1), new Tile(0, 2), new Tile(0, 3)},
                {new Tile(1, 0), new Tile(1, 1), new Tile(1, 2), new Tile(1, 3)},
                {new Tile(2, 0), new Tile(2, 1), new Tile(2, 2), new Tile(2, 3)},
                {new Tile(3, 0), new Tile(3, 1), new Tile(3, 2), new Tile(3, 3)},
            };
            g.GameBoard.Tiles[0, 0].Revealed = true;
            g.GameBoard.Tiles[0, 1].AdjacentMines = 0;
            g.GameBoard.Tiles[0, 2].AdjacentMines = 0;
            g.GameBoard.Tiles[0, 3].AdjacentMines = 0;
            g.GameBoard.Tiles[1, 0].AdjacentMines = 0;
            g.GameBoard.Tiles[1, 1].AdjacentMines = 0;

            g.GameBoard.Tiles[1, 2].AdjacentMines = 1;
            g.GameBoard.Tiles[1, 3].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 0].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 1].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 2].AdjacentMines = 1;
            g.GameBoard.Tiles[2, 3].AdjacentMines = 1;
            g.GameBoard.Tiles[3, 0].AdjacentMines = 1;
            g.GameBoard.Tiles[3, 1].AdjacentMines = 1;
            g.GameBoard.Tiles[3, 2].AdjacentMines = 1;
            g.GameBoard.Tiles[3, 3].AdjacentMines = 1;
            g.CheckForEmptyReveals(g.GameBoard.Tiles[0, 0]);

            int expected_reveals = 11;
            int actual_reveals = 0;
            foreach(Tile t in g.GameBoard.Tiles)
            {
                if (t.Revealed)
                {
                    actual_reveals += 1;
                }
            }
            actual_reveals.ShouldEqual(expected_reveals);

        }
    }
}
