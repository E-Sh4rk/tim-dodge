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

		public Score(SpriteFont fontScore)
		{
			this.fontScore = fontScore;
			Color = Color.Black;
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
			string text = "Score : " + value;

			spriteBatch.DrawString(
				fontScore, text,
				new Vector2(TimGame.WINDOW_WIDTH - 20, 50) - fontScore.MeasureString(text),
				Color);
		}
	}
}
