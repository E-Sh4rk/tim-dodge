using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Stat : Item
	{
		public int value { get; private set; }
		private String Title;

		public Stat(SpriteFont fontStat, Color Color, String Title, int value) : base(Title + value, fontStat, Color)
		{
			this.Title = Title;
			this.value = value;
		}

		public void incr(int i)
		{
			value += i;
		}

		public void decr(int i)
		{
			value -= i;
		}

		public void Update()
		{
			Text = Title + value;
		}

	}
}
