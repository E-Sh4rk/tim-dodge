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
		private Texture2D Background;
		private Menu InitialMenu;
		private bool MenuEnabled;
		private GameInstance game;
		private TimGame Application;

		public GameManager(ContentManager Content, TimGame Application)
		{
			this.Content = Content;
			this.Application = Application;
			Background = Content.Load<Texture2D>("background/winter");
			FillInitialMenu();
			MenuEnabled = true;
		}

		private void FillInitialMenu()
		{
			SpriteFont FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			Color ColorTextMenu = Color.White;
			Color ColorHighlightSelection = Color.Yellow;
			List<MenuItem> ListItems = new List<MenuItem>();

			MenuItem newGame = new MenuItem("New Game", FontMenu, ColorTextMenu);
			MenuItem parameters = new MenuItem("Parameters", FontMenu, ColorTextMenu);
			MenuItem bestScores = new MenuItem("Best Scores", FontMenu, ColorTextMenu);
			MenuItem quit = new MenuItem("Quit", FontMenu, ColorTextMenu);

			newGame.LaunchSelection += NewGame;
			parameters.LaunchSelection += Parameters;
			bestScores.LaunchSelection += BestScores;
			quit.LaunchSelection += Quit;

			ListItems.Add(newGame);
			ListItems.Add(parameters);
			ListItems.Add(bestScores);
			ListItems.Add(quit);

			InitialMenu = new Menu(Content.Load<Texture2D>("background/Menu"), ListItems, ColorHighlightSelection);
		}

		public void NewGame()
		{
			game = new GameInstance(Content);
			MenuEnabled = false;
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

		public void Update(GameTime gameTime)
		{
			if (MenuEnabled)
				InitialMenu.Update(Keyboard.GetState());
			else
				game.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);

			if (MenuEnabled)
				InitialMenu.Draw(spriteBatch);

			else
				game.Draw(spriteBatch);
		}
	}
}
