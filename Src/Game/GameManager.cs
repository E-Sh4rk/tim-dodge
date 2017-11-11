using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	/// <summary>
	/// Manager the game interface: for example, switch between a game instance and a menu when needed. 
	/// </summary>
	public class GameManager
	// To link menus and game instances
	{
		public TimGame Application { get; }

		public GameInstance game;
		private MenuManager Menu;

		public bool GameRunning { get { return game != null; } }

		public bool rotation = false;

		public GameManager(ContentManager Content, TimGame Application)
		{
			Load.LoadContent(Content);
			this.Application = Application;
			Menu = new MenuManager(this);
		}

		public void Update(GameTime gameTime)
		{
			if (GameRunning && game.player.IsDead())
			{
				Menu.LaunchGameOver();
			}

			if (Menu.MenuRunning)
				Menu.Update();

			else if (GameRunning)
			{
				if (Controller.KeyPressed(Keys.Space) || Controller.KeyPressed(Keys.P) || Controller.KeyPressed(Keys.Escape))
					Menu.LaunchPause();
				game.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Menu 
			spriteBatch.Draw(Load.BackgroundSun, Vector2.Zero, Color.White);

			if (GameRunning)
				game.Draw(spriteBatch);

			if (Menu.MenuRunning)
				Menu.Draw(spriteBatch);
		}
	}
}
