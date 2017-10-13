using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Stat
	{
		public int value { get; private set; };
		private SpriteFont fontDisplay;
		private Vector2 Position;
		private Color Color;
		private String Text;

		public Stat(SpriteFont fontDisplay, String Text, Vector2 Position, Color Color, int value)
		{
			this.fontDisplay = fontDisplay;

			this.Text = Text;
			this.Position = Position;
			this.Color = Color;
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

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(fontDisplay, Text + value, Position, Color);
		}
	}
}
