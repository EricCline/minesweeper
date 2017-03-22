using System;

namespace Minesweeper
{
    public class GameController
    {

        public Board GameBoard;
        private bool first_click;

        public GameController(int size, int mines)
        {
            GameBoard = new Board(size, mines);
            first_click = true;
        }

        public void CheckForEmptyReveals(Tile tile)
        {
            if (tile.AdjacentMines == 0)
            {
                foreach(Tile t in GameBoard.AdjacentTiles(tile))
                {
                    if (!t.Revealed)
                    {
                        t.Revealed = true;
                        CheckForEmptyReveals(t);
                    }
                }
            }
        }

        public void Flag(int mouse_x, int mouse_y)
        {
            if (GameBoard.ClickWithinGame(mouse_x, mouse_y))
            {
                Tile tile = GameBoard.TileFromCoordinates(mouse_x, mouse_y);
                tile.Flagged = !tile.Flagged;
            }
        }

        public void Click(int mouse_x, int mouse_y)
        {
            if (GameBoard.ClickWithinGame(mouse_x, mouse_y))
            {
                Tile tile = GameBoard.TileFromCoordinates(mouse_x, mouse_y);
                if (first_click && tile.IsMine)
                {
                    first_click = false;
                    tile.IsMine = false;
                    foreach(Tile t in GameBoard.Tiles)
                    {
                        if (!t.IsMine)
                        {
                            t.IsMine = true;
                            break;
                        }
                    }

                }
                tile.Revealed = true;
                if (GameBoard.IsExploded)
                {
                    Console.WriteLine("You Ded!");
                }
                else if (GameBoard.IsClear)
                {
                    Console.WriteLine("You Win!");
                }
                else
                {
                    CheckForEmptyReveals(tile);
                }
            }
        }
    }
}
