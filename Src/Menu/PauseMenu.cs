using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class PauseMenu : InitialMenu
	{
		public MenuItem resume;

		public PauseMenu(Texture2D BackgroundPicture, GameManager GameManager,
		                 SpriteFont FontMenu, Color ColorTextMenu, Color ColorHighlightSelection)
			: base(BackgroundPicture, GameManager, FontMenu, ColorTextMenu, ColorHighlightSelection)
		{
			resume = new MenuItem("Resume", FontMenu, ColorTextMenu);
			resume.LaunchSelection += GameManager.Resume;

			List<MenuItem> PauseItems = new List<MenuItem>();
			PauseItems.Add(resume);
			PauseItems.AddRange(ListItems);

			ListItems = PauseItems;

			ConstructMenu();
		}
	}
}
