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
			MenuItem resume = new MenuItem("Resume", this, GameManager.Resume);

			List<MenuItem> PauseItems = new List<MenuItem>();
			PauseItems.Add(resume);
			PauseItems.AddRange(ListItems);

			ListItems = PauseItems;

			ConstructMenu();
		}
	}
}
