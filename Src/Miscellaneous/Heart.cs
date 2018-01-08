using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace tim_dodge
{
	/// <summary>
	/// Manager the heart containers.
	/// </summary>
	public class Heart
	{
		private Texture2D[] heartTexture;

		public const int slotNumber = 4;
		private static Vector2 spacement = new Vector2(50,0);

		private Vector2 position = new Vector2(0, 0);

		protected SpriteFont fontItem;
		public Color Color;
		public string sfuel = "Heart : ";

		public int value { get; private set; }

		enum HeartState
		{
			full = 0,
			semi = 1,
			empty = 2
		}

		private HeartState[] Container;

		public Heart(Vector2 pos, Color Color)
		{
			fontItem = Load.FontScore;
			this.Color = Color;

			heartTexture = new Texture2D[3];
			heartTexture[(int)HeartState.full] = Load.HeartFull;
			heartTexture[(int)HeartState.semi] = Load.HeartSemi;
			heartTexture[(int)HeartState.empty] = Load.HeartEmpty;

			Container = new HeartState[slotNumber];

            value = slotNumber * 2;
			position = new Vector2(TimGame.GAME_WIDTH - ((slotNumber) * (spacement.X)), pos.Y);
		}

		public Heart(Vector2 pos, int initlife, Color color) : this(pos, color)
		{
			value = initlife;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Update
			for (int i = 0; i < slotNumber; i++)
			{
				if (value - 2 * i == 1)
					Container[i] = HeartState.semi;
				else if (value - 2 * i <= 0)
					Container[i] = HeartState.empty;
				else
					Container[i] = HeartState.full;
			}

			// Draw
			Vector2 size = fontItem.MeasureString(sfuel);
			size.Y = 0;
			spriteBatch.DrawString(fontItem, sfuel, position-size, Color);

			Vector2 pos = new Vector2(position.X, position.Y);
			for (int i = 0; i < slotNumber; i++)
			{
				spriteBatch.Draw(heartTexture[(int)Container[i]], pos, Color.White);
				pos += spacement;
			}
		}

		public void incr(int i)
		{
            if (i < 0)
                decr(-i);
            else
            {
                value += i;
                if (value > slotNumber * 2)
                    value = slotNumber * 2;
            }
		}

		public void decr(int i)
		{
            if (i < 0)
                incr(-i);

            else
            {
                value -= i;
                if (value < 0)
                    value = 0;
            }
		}

	}
}
