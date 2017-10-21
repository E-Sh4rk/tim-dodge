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
	public class GameManager
	// To link menus and game instances
	{
		public ContentManager Content { get; }
		public TimGame Application { get; }
		public GameInstance game;
		private Texture2D Background;
		private MenuManager Menu;

		public Texture2D BackgroundMenu { get; private set; }
		public SpriteFont FontMenu { get; private set; }
		public SpriteFont FontTitleMenu { get; private set; }

		public bool GameRunning { get { return game != null; } }

		public static Sound sounds { get; private set; }

		public GameManager(ContentManager Content, TimGame Application)
		{
			this.Content = Content;
			this.Application = Application;

			sounds = new Sound(new SoundEffect[] { Content.Load<SoundEffect>("sound/jump"),
				Content.Load<SoundEffect>("sound/explosion"),
				Content.Load<SoundEffect>("sound/damage"),
				Content.Load<SoundEffect>("sound/fire"),
				Content.Load<SoundEffect>("sound/menu"),
				Content.Load<SoundEffect>("sound/toogle"),
				Content.Load<SoundEffect>("sound/applause")},
			                   new SoundEffect[] { Content.Load<SoundEffect>("sound/cuphead") });

			Background = Content.Load<Texture2D>("background/winter");

			BackgroundMenu = Content.Load<Texture2D>("background/Menu");
			FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			FontTitleMenu = Content.Load<SpriteFont>("SpriteFonts/TitleMenu");                       

			Menu = new MenuManager(this);
		}

		public void Update(GameTime gameTime)
		{
			if (GameRunning && game.player.IsDead())
				Menu.LaunchGameOver();

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
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);

			if (GameRunning)
				game.Draw(spriteBatch);

			if (Menu.MenuRunning)
				Menu.Draw(spriteBatch);
		}
	}
}
