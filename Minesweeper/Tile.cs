using System;

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
    }
}
