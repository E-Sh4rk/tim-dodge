
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
		public Map(Texture2D Background, Texture MapTexture, ChooseMap.Maps MapLoad)
		{
			loadTileMap(ChooseMap.StringEnv(MapLoad));
			gMap = new GraphicalMap(Background, MapTexture, tileMap, platforms);
			pMap = new PhysicalMap(gMap.tileMap, platforms);
		}

		public GraphicalMap gMap;
		public PhysicalMap pMap;
		public List<BlockObject> tileMap;
		public List<MapPlatform> platforms;

        /// <summary>
        /// Serializable class for saving a map
        /// </summary>
		public class SaveMap
		{
			public SaveMap() { }
			public SaveMap(List<BlockObject.SaveBlock> tm, List<MapPlatform.SavePlatform> p) { tileMap = tm; platforms = p; }
			public List<BlockObject.SaveBlock> tileMap;
			public List<MapPlatform.SavePlatform> platforms;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			gMap.Draw(spriteBatch);
		}

		public void changeTexture(Texture NewMapTexture)
		{
			gMap.changeTexture(NewMapTexture);
		}

        /// <summary>
        /// Load a saved map and create a real game map with it 
        /// </summary>
        /// <param name="name">Path of saved map file</param>
		public void loadTileMap(string name)
		{
			SaveMap sm = Serializer<SaveMap>.Load(name);
			tileMap = sm.tileMap.ConvertAll(BlockObject.LoadBlock);
			platforms = sm.platforms.ConvertAll(MapPlatform.LoadPlatform);
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
        /// <summary>
        /// Indicate whether moving platform is in collision with
        /// an other platform or a screen edge
        /// </summary>
        /// <param name="pl">A moving platform</param>
        /// <returns></returns>
		bool platformInCollision(MapPlatform pl)
		{
			foreach (MapPlatform mp in platforms)
			{
				if (!mp.Equals(pl))
					foreach (PlatformObject po in mp.objs)
						if (pl.Intersect(new Rectangle((int)po.x, (int)po.y, po.w, po.h)))
							return true;
			}
			foreach (BlockObject o in tileMap)
			{
				if (pl.Intersect(new Rectangle((int)o.Position.X, (int)o.Position.Y, o.w, o.h)))
					return true;
			}
			Rectangle[] screenEdges = new Rectangle[4];
			screenEdges[0] = new Rectangle(0,-1,TimGame.GAME_WIDTH,1);
			screenEdges[1] = new Rectangle(-1, 0, 1, TimGame.GAME_HEIGHT);
			screenEdges[2] = new Rectangle(0, TimGame.GAME_HEIGHT, TimGame.GAME_WIDTH, 1);
			screenEdges[3] = new Rectangle(TimGame.GAME_WIDTH, 0, 1, TimGame.GAME_HEIGHT);
			foreach (Rectangle r in screenEdges)
			{
				if (pl.Intersect(r))
					return true;
			}
			return false;
		}
		public void Update(float elapsed)
		{
			foreach (MapPlatform p in platforms)
			{
				p.Move(elapsed);
				if (platformInCollision(p))
				{
					p.ChangeDirection();
					p.Move(elapsed);
				}
			}
			pMap.Update();
		}

		public const int numberTileY = TimGame.GAME_HEIGHT / 64;
		public const int numberTileX = TimGame.GAME_WIDTH / 64;

	}
}
