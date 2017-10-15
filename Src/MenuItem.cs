using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MenuItem : Item
	{
		public delegate void ItemSelected();
		public event ItemSelected LaunchSelection;

		public MenuItem(String Text, SpriteFont fontItem, Color Color) : base(Text, fontItem, Color)
		{
		}

		public void LaunchSelecion()
		{
			LaunchSelection();
		}


	}
}
