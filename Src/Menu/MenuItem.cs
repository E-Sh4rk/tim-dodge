using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MenuItem : Item
	{
		public delegate void ItemSelected();
		public ItemSelected LaunchSelection { get; }
		public bool reachable { get { return LaunchSelection != null; } }

		public MenuItem(String Text, MenuManager Menu, ItemSelected function) : base(Text, Menu.FontMenu, Menu.ColorTextMenu)
		{
			LaunchSelection = function;
		}

		// To completely redifine a MenuItem
		public MenuItem(String Text, SpriteFont spriteFont, Color Color) : base(Text, spriteFont, Color)
		{
		}
	}
}
