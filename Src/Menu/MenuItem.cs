using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents a item in a menu.
	/// </summary>
	public class MenuItem : Item
	{
		public delegate void ItemSelected();
		public ItemSelected LaunchSelection { get; }
		public bool Selectable { get { return LaunchSelection != null; } }

		public MenuItem(String Text, ItemSelected function) : base(Text, Load.FontMenu, Load.ColorTextMenu)
		{
			LaunchSelection = function;
		}

		// For not selectable items
		public MenuItem(String Text, SpriteFont spriteFont, Color Color) : base(Text, spriteFont, Color)
		{
		}
	}
}
