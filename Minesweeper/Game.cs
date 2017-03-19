using System;

public class Game
{

    public Board GameBoard;

    public Game(int size, int mines)
    {
        GameBoard = new Board(size, mines);
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

    public void Flag(int x, int y)
    {
        Tile tile = GameBoard.Tiles[x, y];
        tile.Flagged = true;
    }

    public void Click(int x, int y)
    {
        Tile tile = GameBoard.Tiles[x, y];
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
