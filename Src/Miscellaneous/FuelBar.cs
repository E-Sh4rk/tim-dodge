using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace tim_dodge
{
	/// <summary>
	/// Create a bar
	/// Usefull for time changes etc
	/// </summary>
	public class FuelBar
	{
		private Texture2D fuelDraw;
		public Vector2 position;

		protected SpriteFont fontItem;
		public Color ColorText;
		private Color ColorFull;
		public string sfuel = "Fuel : ";

		int w;
		int h;
		public float percentage
		{
			get;
			private set;
		}

		public void decr(float i)
		{
			percentage -= i;
		}

		public bool isFull { get { return percentage >= 100f; } }
		public bool isEmpty { get { return percentage <= 0f; } }

		public GameManager gameManager;

		public FuelBar(Vector2 pos, GameManager gm, Color Color)
		{
			fontItem = Load.FontScore;
			this.ColorText = Color;
			position = pos;
			gameManager = gm;
			percentage = 50; // Initial value of the fuel
			w = 100;
			h = 20;
			fuelDraw = new Texture2D(gameManager.Application.GraphicsDevice, w, h);
		}

		private float alpha = 1.0f;
		private float interv = 0.05f;
		private bool croiss = false;
		private float Alpha()
		{
			if (alpha <= 0.0f)
			{
				alpha += interv;
				croiss = true;
			}
			else if (alpha >= 1.0f)
			{
				alpha -= interv;
				croiss = false;
			}
			else if (croiss)
				alpha += interv;
			else
				alpha -= interv;
			return alpha;
		}

		public void Update()
		{
			if (percentage < 100)
				percentage += 0.1f;
			ColorFull = Color.Red * Alpha();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Color[] color = new Color[w*h];//set the color to the amount of pixels in the textures

			for (int i = 0; i < h; i++)//loop through all the colors setting them to whatever values we want
			{
				for (int j = 0; j < w; j++)
				{
					int k = i * w + j;
					if (i <= 1 || j <= 1 || i >= h - 2 || j >= w - 2)
						color[k] = Color.Black;
					else if (j <= (int)w * percentage / 100f)
						if (!isFull)
							color[k] = Color.Blue;
						else
							color[k] = ColorFull;
				}

			}

			fuelDraw.SetData(color);

			spriteBatch.DrawString(fontItem, sfuel, position, ColorText);
			Vector2 size = fontItem.MeasureString(sfuel);
			spriteBatch.Draw(fuelDraw, new Rectangle((int)(position.X + size.X), (int)position.Y+7, w, h), Color.White);
		}

	}
}