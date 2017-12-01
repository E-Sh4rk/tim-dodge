﻿using System;
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
			List<BlockObject.SaveBlock> lst = new List<BlockObject.SaveBlock>();
			lst.Add(new BlockObject.SaveBlock());
			lst.Add(new BlockObject.SaveBlock());
			Serializer<Map.SaveMap>.Save("tmp.xml", new Map.SaveMap(lst, new List<MapPlatform.SavePlatform>()));
			Map.SaveMap m = Serializer<Map.SaveMap>.Load(StringEnv(map));
			tileMap = m.tileMap.ConvertAll(BlockObject.LoadBlock);
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
