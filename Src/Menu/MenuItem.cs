using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MenuItem : Item
	{
		public delegate void ItemSelected();
		public ItemSelected LaunchSelection;

		public MenuItem(String Text, Menu Menu, ItemSelected function) : base(Text, Menu.FontMenu, Menu.ColorTextMenu)
		{
			LaunchSelection = function;
		}

		// Item non selectionable, like Title
		public MenuItem(String Text, Menu Menu) : base(Text, Menu.FontTitle, Menu.ColorTitle)
		{
		}
	}
}
