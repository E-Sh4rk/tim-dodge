using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents the graphical aspect of a map. Used to display the map.
	/// </summary>
	public class GraphicalMap
	{
		public GraphicalMap(Texture2D Background, Texture MapTexture, List<BlockObject> tileMap)
		{
			this.tileMap = tileMap;

			this.MapTexture = MapTexture;
			this.Background = Background;
		}

		public Texture MapTexture;

		public Texture2D Background;

		public List<BlockObject> tileMap;

		public void changeTexture(Texture NewMapTexture)
		{
			MapTexture = NewMapTexture;
			tileMap.ForEach((BlockObject obj) => obj.ChangeTexture(NewMapTexture));
		}

		public void changeBackground(Texture2D NewBackground)
		{
			Background = NewBackground;
		}


		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, new Rectangle(0,0,TimGame.GAME_WIDTH, TimGame.GAME_HEIGHT), Color.White);
			tileMap.ForEach((BlockObject obj) => obj.Draw(spriteBatch));
		}
	}
}
