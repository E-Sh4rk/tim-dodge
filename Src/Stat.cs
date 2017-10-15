using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Stat : Item
	{
		public int value { get; private set; }
		private String Title;

		public void incr(int i)
		{
			value += i;
		}

		public void decr(int i)
		{
			value -= i;
		}

		public Stat(SpriteFont fontStat, String Title, int value) : base(Title + value, fontStat)
		{
			this.Title = Title;
			this.value = value;
		}

		public void Update()
		{
			Text = Title + value;
		}

	}
}
