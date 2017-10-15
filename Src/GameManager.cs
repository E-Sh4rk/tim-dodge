using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class GameManager
	// To link menus and game instances
	{
		private ContentManager Content;
		private TimGame Application;
		private GameInstance game;
		private Texture2D Background;

		private InitialMenu InitialMenu;
		private PauseMenu PauseMenu;
		private bool GameRunning { get { return game != null; } }
		private bool PauseMode;

		public GameManager(ContentManager Content, TimGame Application)
		{
			this.Content = Content;
			this.Application = Application;
			Background = Content.Load<Texture2D>("background/winter");
			PauseMode = false;

			Texture2D BackgrouneMenu = Content.Load<Texture2D>("background/Menu");
			SpriteFont FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			Color ColorTextMenu = Color.White;
			Color ColorHighlightSelection = Color.Yellow;

			InitialMenu = new InitialMenu(BackgrouneMenu, this, FontMenu, ColorTextMenu, ColorHighlightSelection);
			PauseMenu = new PauseMenu(BackgrouneMenu, this, FontMenu, ColorTextMenu, ColorHighlightSelection);
		}

		public void NewGame()
		{
			if (game != null)
				game.sounds.playing.Stop();
			game = new GameInstance(Content);
			PauseMode = false;
		}

		public void Resume()
		{
			PauseMode = false;
		}

		public void Parameters()
		{
		}

		public void BestScores()
		{
		}

		public void Quit()
		{
			Application.Quit();	
		}

		private void LauchPause()
		{
			PauseMode = true;
		}

		public void Update(GameTime gameTime)
		{
			if (GameRunning && !PauseMode &&
				(Controller.KeyPressed(Keys.Space) || Controller.KeyPressed(Keys.P)))
				LauchPause();

			if (GameRunning && !PauseMode)
				game.Update(gameTime);
			else if (PauseMode)
				PauseMenu.Update();
			else
				InitialMenu.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);

			if (GameRunning)
			{
				game.Draw(spriteBatch);
				if (PauseMode)
					PauseMenu.Draw(spriteBatch);
			}
			else
				InitialMenu.Draw(spriteBatch);		

		}
	}
}
