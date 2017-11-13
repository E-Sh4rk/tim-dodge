using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents a map, that is characterized by a background, some obstacles (ground, walls...) and the corresponding textures.
	/// </summary>
	public class Map
	{
		public Map(Texture2D Background, Texture MapTexture, string loadMapString)
		{
			loadTileMap(loadMapString);
			gMap = new GraphicalMap(Background, MapTexture, tileMap);
			pMap = new PhysicalMap(gMap.tileMap);
		}

		public GraphicalMap gMap;
		public PhysicalMap pMap;
		public List<BlockObject> tileMap;

		public void Draw(SpriteBatch spriteBatch)
		{
			gMap.Draw(spriteBatch);
		}

		public void changeTexture(Texture NewMapTexture)
		{
			gMap.changeTexture(NewMapTexture);
		}

		public void loadTileMap(String name)
		{
			tileMap = Serializer<List<BlockObject.SaveBlock>>.Load(name).ConvertAll(
				(BlockObject.SaveBlock input) => BlockObject.LoadBlock(input));
		}

		public void AddBlock(BlockObject bl)
		{
			RemoveBlock(bl.x, bl.y, true);
			tileMap.Add(bl);
		}

		public void RemoveBlock(int x, int y, bool testWater)
		{
			try
			{
				if (testWater)
					tileMap.Remove(tileMap.Find((BlockObject obj) => obj.state != BlockObject.Ground.MiddleWater &&
												obj.state != BlockObject.Ground.UpWater &&
												obj.x == x && obj.y == y));
				else
					tileMap.Remove(tileMap.Find((BlockObject obj) => obj.x == x && obj.y == y));
			}
			catch { }
		}

		public const int numberTileY = TimGame.GAME_HEIGHT / 64;
		public const int numberTileX = TimGame.GAME_WIDTH / 64;

	}
}
