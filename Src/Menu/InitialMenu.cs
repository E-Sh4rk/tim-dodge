using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class InitialMenu : Menu
	{
		public InitialMenu(GameManager GameManager)
			: base(GameManager)
		{
			MenuItem newGame = new MenuItem("New Game", this, GameManager.NewGame);
			MenuItem parameters = new MenuItem("Parameters", this, GameManager.Parameters);
			MenuItem bestScores = new MenuItem("Best Scores", this, GameManager.BestScores);
			MenuItem quit = new MenuItem("Quit", this, GameManager.Quit);

			ListItems.Add(newGame);
			ListItems.Add(parameters);
			ListItems.Add(bestScores);
			ListItems.Add(quit);

			ConstructMenu();
		}

	}
}
