using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
		private Parameters ParamMenu;

		private bool GameRunning { get { return game != null; } }
		private bool PauseMode;
		private bool SetParam;

		public static Sound sounds { get; private set; }

		public GameManager(ContentManager Content, TimGame Application)
		{
			this.Content = Content;
			this.Application = Application;
			Background = Content.Load<Texture2D>("background/winter");
			PauseMode = SetParam = false;

			Texture2D BackgrouneMenu = Content.Load<Texture2D>("background/Menu");
			SpriteFont FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			Color ColorTextMenu = Color.White;
			Color ColorHighlightSelection = Color.Yellow;

			InitialMenu = new InitialMenu(BackgrouneMenu, this, FontMenu, ColorTextMenu, ColorHighlightSelection);
			PauseMenu = new PauseMenu(BackgrouneMenu, this, FontMenu, ColorTextMenu, ColorHighlightSelection);
			ParamMenu = new Parameters(BackgrouneMenu, this, FontMenu, ColorTextMenu, ColorHighlightSelection);

			sounds = new Sound(new SoundEffect[] { Content.Load<SoundEffect>("sound/jump"),
				Content.Load<SoundEffect>("sound/explosion")},
				   new SoundEffect[] { Content.Load<SoundEffect>("sound/cuphead") });
		}

		public void NewGame()
		{
			if (game == null)
				sounds.playMusic(Sound.MusicName.cuphead);
			else
			{
				sounds.music.Stop();
				sounds.playMusic(Sound.MusicName.cuphead);
			}
			
			game = new GameInstance(Content);
			PauseMode = false;
		}

		public void Resume()
		{
			PauseMode = false;
		}

		public void Parameters()
		{
			SetParam = true;
		}

		public void BackMenu()
		{
			SetParam = false;
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
			else if (SetParam)
				ParamMenu.Update();
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
				if (SetParam)
					ParamMenu.Draw(spriteBatch);
				else if (PauseMode)
					PauseMenu.Draw(spriteBatch);
			}
			else if (SetParam)
				ParamMenu.Draw(spriteBatch);
			else
				InitialMenu.Draw(spriteBatch);		

		}
	}
}
