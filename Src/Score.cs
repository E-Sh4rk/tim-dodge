using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Score
	{
		private int value;
		private SpriteFont fontScore;
		private Color Color;
		private Vector2 Position;
		private String Text;

		public Score(SpriteFont fontScore)
		{
			this.fontScore = fontScore;
			Text = "Score : ";
			Color = Color.Yellow;
			Position = new Vector2(TimGame.WINDOW_WIDTH - 20, 50);
		}

		public void incr(int i)
		{
			value += i;
		}

		public void decr(int i)
		{
			value -= 1;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(fontScore, Text + value, Position - fontScore.MeasureString(Text + value), Color);
		}
	}
}
