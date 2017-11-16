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
		private Vector2 position0 = new Vector2(TimGame.GAME_WIDTH - ((slotNumber)*(spacement.X) ), 20);

		public int value { get; private set; }

		enum HeartState
		{
			full = 0,
			semi = 1,
			empty = 2
		}

		private HeartState[] Container;

		public Heart()
		{
			heartTexture = new Texture2D[3];
			heartTexture[(int)HeartState.full] = Load.HeartFull;
			heartTexture[(int)HeartState.semi] = Load.HeartSemi;
			heartTexture[(int)HeartState.empty] = Load.HeartEmpty;

			Container = new HeartState[slotNumber];

			value = slotNumber * 2;

		}

		public Heart(int initlife) : this()
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
			Vector2 position = new Vector2(position0.X, position0.Y);
			for (int i = 0; i < slotNumber; i++)
			{
				spriteBatch.Draw(heartTexture[(int)Container[i]], position, Color.White);
				position += spacement;
			}
		}

		public void incr(int i)
		{
			value += i;
			if (value > slotNumber * 2)
				value = slotNumber * 2;
		}

		public void decr(int i)
		{
			value -= i;
			if (value < 0)
				value = 0;
		}

	}
}
