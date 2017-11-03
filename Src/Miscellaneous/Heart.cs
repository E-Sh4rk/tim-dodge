using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace tim_dodge
{
	public class Heart
	{
		private Texture2D[] heartTexture;

		public const int slotNumber = 4;

		private static Vector2 spacement = new Vector2(50,0);
		private Vector2 position0 = new Vector2(TimGame.WINDOW_WIDTH - ((slotNumber)*(spacement.X) ), 20);

		public int value { get; private set; }

		enum HeartState
		{
			full = 0,
			semi = 1,
			empty = 2
		}

		private HeartState[] Container;

		public object MyProperty
		{
			get;
			private set;
		}
		public Heart(Texture2D full, Texture2D semi, Texture2D empty)
		{
			heartTexture = new Texture2D[3];
			heartTexture[(int)HeartState.full] = full;
			heartTexture[(int)HeartState.semi] = semi;
			heartTexture[(int)HeartState.empty] = empty;

			Container = new HeartState[slotNumber];

			value = slotNumber * 2;

		}

		public Heart(Texture2D full, Texture2D semi, Texture2D empty, int initlife)
		: this(full, semi, empty)
		{
			value = initlife;
		}

		public void Update()
		{
			for (int i = 0; i < slotNumber; i++)
			{
				if (value - 2 * i == 1)
					Container[i] = HeartState.semi;
				else if (value - 2 * i <= 0)
					Container[i] = HeartState.empty;
				else
					Container[i] = HeartState.full;                        
			}
		}


		public void Draw(SpriteBatch spriteBatch)
		{
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
