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
		public Map(Texture2D Background, Texture MapTexture)
		{
			tileMap = new List<BlockObject>();

			//Object(Texture texture, int x, int y, Ground state)
			tileMap.Add(new BlockObject(MapTexture, 6, Map.numberTileY, BlockObject.Ground.RightDurt));
			tileMap.Add(new BlockObject(MapTexture, 10, Map.numberTileY, BlockObject.Ground.LeftDurt));
			tileMap.Add(new BlockObject(MapTexture, 6, Map.numberTileY - 1, BlockObject.Ground.RightGround));
			tileMap.Add(new BlockObject(MapTexture, 10, Map.numberTileY - 1, BlockObject.Ground.LeftGround));


			for (int i = 0; i < Map.numberTileX; i++)
			{
				if (i < 6 || i > 10)
				{
					tileMap.Add(new BlockObject(MapTexture, i, Map.numberTileY - 1, BlockObject.Ground.MiddleGround));
					tileMap.Add(new BlockObject(MapTexture, i, Map.numberTileY, BlockObject.Ground.MiddleDurt));
				}

			}
			//Serializer<List<Map.Block>>.Save(Load.PathLevels[0], tileMap);

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

		public void AddBlock(BlockObject bl)
		{
			RemoveBlock(bl.x, bl.y);
			tileMap.Add(bl);
		}

		public void RemoveBlock(int x, int y)
		{
			try
			{
				tileMap.Remove(tileMap.Find((BlockObject obj) => obj.x == x && obj.y == y));
			}
			catch { }
		}

		public const int numberTileY = TimGame.GAME_HEIGHT / 64;
		public const int numberTileX = TimGame.GAME_WIDTH / 64;

	}
}
