using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class Board
    {
        public Tile[,] Tiles;

        private int _size;
        private int _mines;

        private Rectangle bounding_box;

        public Board(int size, int mines)
        {
            _size = size;
            _mines = mines;

            if (_mines >= _size * _size)
            {
                throw new System.ArgumentException(
                    String.Format("Cannot have as many mines({0}) as tiles({1}).", _mines, _size * _size)
                );
            }
            MakeTiles();
            PlaceMines();
            UpdateTileAdjacentMines();
            bounding_box = new Rectangle(0, 0, _size * 20, _size * 20);
        }

        public void PrintTiles()
        {
            Console.WriteLine("\n");
            foreach(Tile tile in Tiles)
            {
                Console.WriteLine(
                    String.Format(
                        "X: {0} Y: {1}  IsMine: {2} Revealed: {3}",
                        tile.X, tile.Y, tile.IsMine, tile.Revealed
                    )
                );
            }
        }

        public Tile[] TilesWithMines()
        {
            return Array.FindAll(Tiles.Cast<Tile>().ToArray(), t => t.IsMine);
        }

        public void UpdateTileAdjacentMines()
        {
            foreach(Tile tile in Tiles)
            {
                tile.AdjacentMines = AdjacentMines(tile);
            }
        }

        private void MakeTiles()
        {
            Tiles = new Tile[_size, _size];
            for(int x = 0; x < _size; x++)
            {
                for(int y = 0; y < _size; y++)
                {
                    Tiles[x, y] = new Tile(x, y);
                }
            }
        }

        private void PlaceMines()
        {
            Random rand = new Random();
            int x, y;
            for(int mine = 0; mine < _mines; mine++)
            {
                x = rand.Next(0, _size);
                y = rand.Next(0, _size);
                while(Tiles[x, y].IsMine)
                {
                    x = rand.Next(0, _size);
                    y = rand.Next(0, _size);
                }
                Tiles[x, y].IsMine = true;
            }
        }

        public int AdjacentMines(Tile tile)
        {
            int count = 0;
            foreach(Tile t in AdjacentTiles(tile))
            {
                if(t.IsMine)
                {
                    count += 1;
                }
            }
            return count;
        }

        public Tile[] AdjacentTiles(Tile tile)
        {
            int[] possibleX = possibleIndices(tile.X);
            int[] possibleY = possibleIndices(tile.Y);

            List<Tile> adj_tiles = new List<Tile>();
            foreach(int x in possibleX)
            {
                foreach(int y in possibleY)
                {
                    if ((x != tile.X || y != tile.Y))
                    {
                        adj_tiles.Add(Tiles[x, y]);
                    }
                }
            }
            return adj_tiles.ToArray();
        }

        public bool IsClear
        {
            get
            {
                foreach(Tile t in Tiles)
                {
                    if (!t.Revealed && !t.IsMine)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsExploded
        {
            get
            {
                foreach(Tile t in Tiles)
                {
                    if (t.Exploded)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ClickWithinGame(int mouse_x, int mouse_y)
        {
            return bounding_box.Contains(mouse_x, mouse_y);
        }


        public Tile TileFromCoordinates(int mouse_x, int mouse_y)
        {
            foreach(Tile t in Tiles)
            {
                if (t.Contains(mouse_x, mouse_y))
                {
                    return t;
                }
            }
            throw new Exception("Clicked outside game board!");
        }

        private int[] possibleIndices(int coordinate)
        {
            int[] possible;
            if (coordinate == 0)
            {
                possible = new int[2] {coordinate, coordinate + 1};
            } 
            else if (coordinate == _size - 1)
            {
                possible = new int[2] {coordinate, coordinate - 1};
            }
            else
            {
                possible = new int[3] {coordinate - 1, coordinate, coordinate + 1};
            }
            return possible;
        }
    }
}
