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
		public Color Color;
		public string sfuel = "Fuel : ";

		int w;
		int h;
		public int percentage
		{
			get;
			private set;
		}

		public GameManager gameManager;

		public FuelBar(Vector2 pos, GameManager gm, Color Color)
		{
			fontItem = Load.FontScore;
			this.Color = Color;
			position = pos;
			gameManager = gm;
			percentage = 75;
			w = 100;
			h = 20;
			fuelDraw = new Texture2D(gameManager.Application.GraphicsDevice, w, h);
		}

		public void Update()
		{
			
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
						color[k] = Color.Blue;
				}

			}

			fuelDraw.SetData(color);

			spriteBatch.DrawString(fontItem, sfuel, position, Color);
			Vector2 size = fontItem.MeasureString(sfuel);
			spriteBatch.Draw(fuelDraw, new Rectangle((int)(position.X + size.X), (int)position.Y+7, w, h), Color.White);
		}

	}
}