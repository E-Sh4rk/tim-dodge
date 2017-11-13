using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class ChooseMap
	{
		public ChooseMap()
		{
			currentMap = Maps.DuneMap;
			loadTileMap(currentMap);
		}

		List<BlockObject> tileMap;
		public Maps currentMap;

		public void loadTileMap(ChooseMap.Maps map)
		{
			tileMap = Serializer<List<BlockObject.SaveBlock>>.Load(StringEnv(map)).ConvertAll(
				(BlockObject.SaveBlock input) => BlockObject.LoadBlock(input));
		}

		public enum Maps
		{
			DuneMap = 0,
			FlatMap = 1,
			WaterMap = 2
		}

		public static int nbMaps = 3;

		private static List<string> EnvMaps = new List<string>
		{ "Content/environment/dune.xml",
			"Content/environment/flat.xml",
			"Content/environment/water.xml"};

		public static String StringEnv(Maps env)
		{
			return EnvMaps[(int)env];
		}

		public void RightMap()
		{
			currentMap = (Maps)(((int)currentMap + 1) % nbMaps);
			loadTileMap(currentMap);
		}

		public void LeftMap()
		{
			currentMap = (Maps)(((int)currentMap + nbMaps - 1) % nbMaps);
			loadTileMap(currentMap);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			tileMap.ForEach((BlockObject obj) => obj.Draw(spriteBatch));
		}
	}
}
