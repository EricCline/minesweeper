using System;
using System.Linq;
using NUnit.Framework;
using Should;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class TileTests
    {
        private Tile t;

        [Test]
        public void ContainsReturnsTrueWhenPointInsideTile()
        {
            t = new Tile(0, 0);
            t.Contains(10, 10).ShouldBeTrue();
        }

        [Test]
        public void ContainsReturnsFalseWhenPointNotInsideTile()
        {
            t = new Tile(0, 0);
            t.Contains(20, 10).ShouldBeFalse();
        }
    }
}
