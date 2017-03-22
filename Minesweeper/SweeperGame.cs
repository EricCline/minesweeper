using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Minesweeper
{

	public class SweeperGame: Game
	{
        public const int TileSize = 20;

        private Texture2D all_tiles;

        private GameController gc;

        private const int tile = 9;
        private const int mine_tile = 10;
        private const int flag_tile = 11;
        private const int win_mine_tile = 12;
        private const int win_clicked_tile = 13;

        private MouseState prev_mouse_state;

        private Dictionary<int, Rectangle> sprite_rects;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public SweeperGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
            this.IsMouseVisible = true;
            gc = new GameController(16, 40);

            sprite_rects = new Dictionary<int, Rectangle>();
            sprite_rects.Add(0, new Rectangle (60, 0, TileSize, TileSize));
            sprite_rects.Add(1, new Rectangle (80, 0, TileSize, TileSize));
            sprite_rects.Add(2, new Rectangle (100, 0, TileSize, TileSize));
            sprite_rects.Add(3, new Rectangle (120, 0, TileSize, TileSize));
            sprite_rects.Add(4, new Rectangle (140, 0, TileSize, TileSize));
            sprite_rects.Add(5, new Rectangle (160, 0, TileSize, TileSize));
            sprite_rects.Add(6, new Rectangle (180, 0, TileSize, TileSize));
            sprite_rects.Add(7, new Rectangle (200, 0, TileSize, TileSize));
            sprite_rects.Add(8, new Rectangle (220, 0, TileSize, TileSize));

            sprite_rects.Add(tile, new Rectangle (0, 0, TileSize, TileSize));
            sprite_rects.Add(mine_tile, new Rectangle (20, 0, TileSize, TileSize));
            sprite_rects.Add(flag_tile, new Rectangle (40, 0, TileSize, TileSize));
            sprite_rects.Add(win_mine_tile, new Rectangle (240, 0, TileSize, TileSize));
            sprite_rects.Add(win_clicked_tile, new Rectangle (260, 0, TileSize, TileSize));

			base.Initialize();
		}

		protected override void LoadContent()
		{
            spriteBatch = new SpriteBatch(GraphicsDevice);
            all_tiles = Content.Load<Texture2D>("Mines");
		}

		protected override void Update(GameTime gameTime)
		{
            MouseState mouse_state = Mouse.GetState();
            KeyboardState keyboard_state = Keyboard.GetState();
            if (!gc.GameBoard.IsClear && !gc.GameBoard.IsExploded)
            {
                if (mouse_state.LeftButton == ButtonState.Pressed && prev_mouse_state.LeftButton == ButtonState.Released)
                {
                    if (keyboard_state.IsKeyDown(Keys.LeftControl))
                    {
                        gc.Flag(mouse_state.X, mouse_state.Y);
                    }
                    else
                    {
                        gc.Click(mouse_state.X, mouse_state.Y);
                    }
                }
            }
            prev_mouse_state = mouse_state;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

            foreach(Tile t in gc.GameBoard.Tiles)
            {
                Rectangle draw_square = new Rectangle(0, 0, TileSize, TileSize);

                int x_pos = t.X * TileSize;
                int y_pos = t.Y * TileSize;

                if (t.Exploded) { draw_square = sprite_rects[mine_tile]; }
                else if (t.Flagged) { draw_square = sprite_rects[flag_tile]; }
                else if (t.Revealed) { draw_square = sprite_rects[t.AdjacentMines]; }
                else if (t.IsMine)
                {
                    if (gc.GameBoard.IsExploded)
                    {
                        draw_square = sprite_rects[mine_tile];
                    }
                    else if (gc.GameBoard.IsClear)
                    {
                        draw_square = sprite_rects[win_mine_tile];
                    }
                    else
                    {
                        draw_square = sprite_rects[tile];
                    }
                }
                else if (!t.Revealed) { draw_square = sprite_rects[tile]; }
                spriteBatch.Draw(all_tiles, new Rectangle(x_pos, y_pos, TileSize, TileSize), draw_square, Color.White);

            }
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
