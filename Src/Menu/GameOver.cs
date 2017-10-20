using System;
using System.Collections.Generic;

namespace tim_dodge
{
	public class GameOver : Menu
	{
		public GameOver(GameManager GameManager)
			: base(GameManager)
		{
			Title = "Game Over !";

			MenuItem playAgain = new MenuItem("Play Again", this, GameManager.NewGame);
			MenuItem backInitialMenu = new MenuItem("Back Menu", this, GameManager.BackInitialMenu);
			MenuItem quit = new MenuItem("I don't like youre game...", this, GameManager.Quit);

			ListItems = new List<MenuItem> { playAgain, backInitialMenu, quit };

			ConstructMenu();
		}
	}
}
