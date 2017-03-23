using System;
using System.Drawing;

namespace Minesweeper
{
    public class Tile
    {
        public int X, Y;
        public bool IsMine;
        public bool Flagged;
        public bool Revealed;
        public int AdjacentMines;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
            IsMine = false;
            Flagged = false;
            Revealed = false;
            AdjacentMines = 0;
        }

        public bool Exploded
        {
            get { return IsMine && Revealed; }
        }

        public void Print()
        {
            Console.WriteLine(
                String.Format(
                    "X: {0} Y: {1}  IsMine: {2} Revealed: {3}",
                    X, Y, IsMine, Revealed
                )
            );
        }

        public bool Contains(int mouse_x, int mouse_y)
        {
            Rectangle bounding_box = new Rectangle(
                    X * SweeperGame.TileSize,
                    Y * SweeperGame.TileSize,
                    SweeperGame.TileSize, SweeperGame.TileSize
            );
            return bounding_box.Contains(mouse_x, mouse_y);
        }
    }
}
