
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class QuitMenu : Menu
	{
		public QuitMenu(GameManager GameManager)
			: base(GameManager)
		{
			Title = "Quit the game ?";

			MenuItem backToMenu = new MenuItem("No, I want to play more!", this, GameManager.BackMenu);
			MenuItem quit = new MenuItem("Yes, leave me alone", this, GameManager.Quit);

			ListItems.Add(backToMenu);
			ListItems.Add(quit);

			ConstructMenu();
		}

	}
}
