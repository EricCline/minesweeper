using System;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private Board b;

        [SetUp]
        public void Init()
        {
            b = new Board(3, 0);
            b.Tiles = new Tile[3, 3] {{new Tile(0, 0), new Tile(0, 1), new Tile(0, 2)},
                                      {new Tile(1, 0), new Tile(1, 1), new Tile(1, 2)},
                                      {new Tile(2, 0), new Tile(2, 1), new Tile(2, 2)}};
        }

        [Test]
        public void MakesAppropriatelySizedBoard()
        {
            b.Tiles.Length.ShouldEqual(9);
        }

        [Test]
        public void SetsTheRightNumberOfMines()
        {
            b = new Board(5, 10);
            b.TilesWithMines().Length.ShouldEqual(10);
        }

        [Test]
        public void AdjacentMinesReturnsCorrectCount()
        {
            b.Tiles[0, 0].IsMine = true;
            b.Tiles[0, 2].IsMine = true;
            b.Tiles[1, 0].IsMine = true;
            b.Tiles[2, 0].IsMine = true;
            b.Tiles[2, 1].IsMine = true;
            b.AdjacentMines(b.Tiles[1, 1]).ShouldEqual(5);
            b.AdjacentMines(b.Tiles[1, 2]).ShouldEqual(2);
        }

        [Test]
        public void AdjacentTilesReturnsCorrectTiles()
        {
            Tile[] tiles = b.AdjacentTiles(b.Tiles[1, 1]);
            tiles.Length.ShouldEqual(8);
        }

        [Test]
        public void IsClearReturnsFalseWhenNonMineTilesLeftToFlip()
        {
            b.Tiles[0, 0].IsMine = true;
            b.IsClear.ShouldBeFalse();
        }

        [Test]
        public void IsClearReturnsTrueWhenAllNonMineTilesAreFlipped()
        {
            
            b.Tiles[0, 0].IsMine = true;
            foreach(Tile t in b.Tiles)
            {
                if (!t.IsMine)
                {
                    t.Revealed = true;
                }
            }
            b.IsClear.ShouldBeTrue();
        }

        [Test]
        public void IsExplodedReturnsTrueWhenExploded()
        {
            b.Tiles[0, 0].IsMine = true;
            b.Tiles[0, 0].Revealed = true;
            b.IsExploded.ShouldBeTrue();
        }

        [Test]
        public void ClickWithinGameReturnsTrueWhenClickWithinGame()
        {
            b.ClickWithinGame(30, 30).ShouldBeTrue();
        }

        [Test]
        public void ClickWithinGameReturnsFalseWhenClickOutsideGame()
        {
            b.ClickWithinGame(60, 60).ShouldBeFalse();
        }

        [Test]
        public void TileFromCoordinatesReturnsCorrectTile()
        {
            Tile t = b.TileFromCoordinates(50, 10);  // third tile in first row
            t.X.ShouldEqual(2);
            t.Y.ShouldEqual(0);
        }
    }
}
