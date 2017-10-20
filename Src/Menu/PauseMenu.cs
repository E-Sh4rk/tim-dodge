using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class PauseMenu : InitialMenu
	{
		public PauseMenu(GameManager GameManager)
			: base(GameManager)
		{
			Title = "Pause";

			MenuItem resume = new MenuItem("Resume", this, GameManager.Resume);

			// ListItems[0] contains the title of Initial Menu : to remove
			// Add at the head of the list the button "Resume"
			ListItems[0] = resume;

			ConstructMenu();
		}
	}
}
