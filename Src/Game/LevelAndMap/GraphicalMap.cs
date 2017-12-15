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
		public GraphicalMap(Texture2D Background, Texture MapTexture, List<BlockObject> tileMap, List<MapPlatform> platforms)
		{
			this.tileMap = tileMap;
			this.platforms = platforms;

			this.Background = Background;
			changeTexture(MapTexture);
		}

		public Texture MapTexture;

		public Texture2D Background;

		public List<BlockObject> tileMap;
		public List<MapPlatform> platforms;

		public void changeTexture(Texture NewMapTexture)
		{
			MapTexture = NewMapTexture;
			tileMap.ForEach((BlockObject obj) => obj.ChangeTexture(NewMapTexture));
			platforms.ForEach((MapPlatform obj) => obj.ChangeTexture(NewMapTexture));
		}

		public void changeBackground(Texture2D NewBackground)
		{
			Background = NewBackground;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Color c = Color.White;
			if (Background == Load.BackgroundDark)
				c *= 0.8f;
			spriteBatch.Draw(Background, new Rectangle(0,0,TimGame.GAME_WIDTH, TimGame.GAME_HEIGHT), c);
			tileMap.ForEach((BlockObject obj) => obj.Draw(spriteBatch));
			platforms.ForEach((MapPlatform obj) => obj.Draw(spriteBatch));
		}
	}
}
