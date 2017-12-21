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

		private List<BlockObject> tileMap;
		private List<MapPlatform> platforms;
		public Maps currentMap;

		public void loadTileMap(ChooseMap.Maps map)
		{
			/*List<BlockObject.SaveBlock> lst = new List<BlockObject.SaveBlock>();
			lst.Add(new BlockObject.SaveBlock());
			lst.Add(new BlockObject.SaveBlock());
			List<MapPlatform.SavePlatform> p = new List<MapPlatform.SavePlatform>();
			p.Add(new MapPlatform.SavePlatform());
			Serializer<Map.SaveMap>.Save("tmp.xml", new Map.SaveMap(lst, p));*/
			Map.SaveMap m = Serializer<Map.SaveMap>.Load(StringEnv(map));
			tileMap = m.tileMap.ConvertAll(BlockObject.LoadBlock);
			platforms = m.platforms.ConvertAll(MapPlatform.LoadPlatform);
		}

		public enum Maps
		{
			DuneMap = 0,
			StairMap = 1,
			WaterMap = 2,
			FlatMap = 3
		}

		public static int nbMaps = Enum.GetNames(typeof(Maps)).Length;

		private static List<string> EnvMaps = new List<string>
		{ "Content/environment/dune.xml",
			"Content/environment/staircase.xml",
			"Content/environment/water.xml",
			"Content/environment/flat.xml"};

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
			platforms.ForEach((MapPlatform obj) => obj.Draw(spriteBatch));
		}
	}
}
