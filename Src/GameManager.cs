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
		private ContentManager Content;
		private TimGame Application;
		private GameInstance game;
		private Texture2D Background;

		public Texture2D BackgroundMenu { get; }
		public SpriteFont FontMenu { get; }
		public Color ColorTextMenu { get; }
		public Color ColorHighlightSelection { get; }
		public SpriteFont FontTitleMenu { get; }
		public Color ColorTitleMenu { get; }

		private List<Menu> CurrentMenu;
		private InitialMenu InitialMenu;
		private PauseMenu PauseMenu;
		private Parameters ParamMenu;
		private GameOver Gameover;
		private QuitMenu QuitMenu;

		public bool GameRunning { get { return game != null; } }
		private bool MenuRunning { get { return CurrentMenu.Count != 0; } }

		public static Sound sounds { get; private set; }

		public GameManager(ContentManager Content, TimGame Application)
		{
			sounds = new Sound(new SoundEffect[] { Content.Load<SoundEffect>("sound/jump"),
				Content.Load<SoundEffect>("sound/explosion"),
				Content.Load<SoundEffect>("sound/damage")},
			                   new SoundEffect[] { Content.Load<SoundEffect>("sound/cuphead") });

			this.Content = Content;
			this.Application = Application;
			Background = Content.Load<Texture2D>("background/winter");

			// Default parameters for menus
			BackgroundMenu = Content.Load<Texture2D>("background/Menu");
			FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			ColorTextMenu = Color.White;
			ColorHighlightSelection = Color.Yellow;
			FontTitleMenu = Content.Load<SpriteFont>("SpriteFonts/TitleMenu");
			ColorTitleMenu = Color.DarkBlue;

			InitialMenu = new InitialMenu(this);
			PauseMenu = new PauseMenu(this);
			ParamMenu = new Parameters(this);
			Gameover = new GameOver(this);
			QuitMenu = new QuitMenu(this);

			CurrentMenu = new List<Menu>();
			CurrentMenu.Add(InitialMenu);
		}

		public void NewGame()
		{
			game = new GameInstance(Content);
			CurrentMenu = new List<Menu>();
		}

		private void LauchPause() { CurrentMenu.Add(PauseMenu); }

		public void Resume() { CurrentMenu = new List<Menu>(); }

		public void Parameters() { CurrentMenu.Add(ParamMenu); }

		public void LaunchGameOver() {
			if (CurrentMenu.Count == 0)
				CurrentMenu.Add(Gameover); 
		}

		public void BackInitialMenu() { CurrentMenu = new List<Menu> { InitialMenu }; }

		public void Previous() 
		{
			if (CurrentMenu.Count > 1)
				CurrentMenu.Remove(CurrentMenu.Last());
			else
				CurrentMenu.Add(QuitMenu);
		}
		 
		public void BestScores()
		{
		}

		public void Quit() { Application.Quit(); }

		public void Update(GameTime gameTime)
		{
			if (GameRunning && game.player.IsDead)
				LaunchGameOver();

			if (MenuRunning)
			{
				if (Controller.KeyPressed(Keys.Escape))
					Previous();
				CurrentMenu.Last().Update();
			}

			else if (GameRunning)
			{
				if (Controller.KeyPressed(Keys.Space) || Controller.KeyPressed(Keys.P) || Controller.KeyPressed(Keys.Escape))
					LauchPause();
				game.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);

			if (GameRunning)
				game.Draw(spriteBatch);

			if (MenuRunning)
				CurrentMenu.Last().Draw(spriteBatch);
		}
	}
}
