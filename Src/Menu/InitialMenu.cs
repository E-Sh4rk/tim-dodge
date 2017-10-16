using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class InitialMenu : Menu
	{
		private MenuItem newGame;
		private MenuItem parameters;
		private MenuItem bestScores;
		private MenuItem quit;

		public InitialMenu(Texture2D BackgroundPicture, GameManager GameManager,
		                   SpriteFont FontMenu, Color ColorTextMenu, Color ColorHighlightSelection)
			: base(BackgroundPicture, ColorHighlightSelection)
		{
			newGame = new MenuItem("New Game", FontMenu, ColorTextMenu);
			parameters = new MenuItem("Parameters", FontMenu, ColorTextMenu);
			bestScores = new MenuItem("Best Scores", FontMenu, ColorTextMenu);
			quit = new MenuItem("Quit", FontMenu, ColorTextMenu);

			ListItems.Add(newGame);
			ListItems.Add(parameters);
			ListItems.Add(bestScores);
			ListItems.Add(quit);

			newGame.LaunchSelection += GameManager.NewGame;
			parameters.LaunchSelection += GameManager.Parameters;
			bestScores.LaunchSelection += GameManager.BestScores;
			quit.LaunchSelection += GameManager.Quit;

			ConstructMenu();
		}

	}
}
