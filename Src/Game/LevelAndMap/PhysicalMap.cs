using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Represents the physical aspect of a map. Used to compute the collisions with the map.
	/// </summary>
	public class PhysicalMap
	{
		Rectangle[] fixedRoofs;
		Rectangle[] allRoofs;
		public Rectangle[] roofs
		{
			get { return allRoofs; }
		}
		Rectangle[] fixedGround;
		Rectangle[] allGround;
		public Rectangle[] grounds
		{
			get { return allGround; }
		}
		Rectangle[] fixedLWalls;
		Rectangle[] allLWalls;
		public Rectangle[] leftWalls
		{
			get { return allLWalls; }
		}
		Rectangle[] fixedRWalls;
		Rectangle[] allRWalls;
		public Rectangle[] rightWalls
		{
			get { return allRWalls; }
		}

		List<MapPlatform> platforms;

		public PhysicalMap(List<BlockObject> tileMap, List<MapPlatform> platforms)
		{
			this.platforms = platforms;

			List<Rectangle>[] walls = new List<Rectangle>[4];
			for (int i = 0; i < 4; i++)
				walls[i] = new List<Rectangle>();

			tileMap.ForEach((BlockObject bl) =>
			{
				List<Rectangle>[] result = walls_of_ground(bl);
				for (int i = 0; i < 4; i++)
					walls[i].AddRange(result[i]);
			});

			walls[(int)Wall.left].Add(new Rectangle(-100, 0, 100, TimGame.GAME_HEIGHT));
			walls[(int)Wall.right].Add(new Rectangle(TimGame.GAME_WIDTH, 0, 100, TimGame.GAME_HEIGHT));
			walls[(int)Wall.roof].Add(new Rectangle(0, -100, TimGame.GAME_WIDTH, 100));

			fixedRoofs = walls[(int)Wall.roof].ToArray();
			fixedLWalls = walls[(int)Wall.left].ToArray();
			fixedRWalls = walls[(int)Wall.right].ToArray();
			fixedGround = walls[(int)Wall.bottom].ToArray();
		}

		public void Update()
		{
			List<Rectangle>[] walls = new List<Rectangle>[4];
			for (int i = 0; i < 4; i++)
				walls[i] = new List<Rectangle>();

			platforms.ForEach((MapPlatform bl) =>
			{
				foreach (PlatformObject po in bl.objs)
				{
					List<Rectangle>[] result = walls_of_ground(po);
					for (int i = 0; i < 4; i++)
						walls[i].AddRange(result[i]);
				}
			});

			walls[(int)Wall.roof].AddRange(fixedRoofs);
			walls[(int)Wall.left].AddRange(fixedLWalls);
			walls[(int)Wall.right].AddRange(fixedRWalls);
			walls[(int)Wall.bottom].AddRange(fixedGround);

			allRoofs = walls[(int)Wall.roof].ToArray();
			allLWalls = walls[(int)Wall.left].ToArray();
			allRWalls = walls[(int)Wall.right].ToArray();
			allGround = walls[(int)Wall.bottom].ToArray();
		}

		const int ground_detection_space = 1;
		public bool nearTheGround(PhysicalObject o)
		{
			Point pos = o.Position.ToPoint();
			pos.Y = pos.Y + ground_detection_space;
			Rectangle ro = new Rectangle(pos, o.Size);
			foreach (Rectangle r in grounds)
			{
				if (Collision.rect_collision(ro, r) != null)
					return true;
			}
			return false;
		}
		const int wall_detection_space = 1;
		public bool nearRightWall(PhysicalObject o)
		{
			Point pos = o.Position.ToPoint();
			pos.X = pos.X + ground_detection_space;
			Rectangle ro = new Rectangle(pos, o.Size);
			foreach (Rectangle r in rightWalls)
			{
				if (Collision.rect_collision(ro, r) != null)
					return true;
			}
			return false;
		}
		public bool nearLeftWall(PhysicalObject o)
		{
			Point pos = o.Position.ToPoint();
			pos.X = pos.X - ground_detection_space;
			Rectangle ro = new Rectangle(pos, o.Size);
			foreach (Rectangle r in leftWalls)
			{
				if (Collision.rect_collision(ro, r) != null)
					return true;
			}
			return false;
		}
		public void magnetizeToGround(PhysicalObject o)
		{
			Vector2 pos = o.Position;
			pos.Y = pos.Y + ground_detection_space;
			o.Position = pos;
			adjustPositionAndVelocity(o);
		}
		public void adjustPositionAndVelocity(PhysicalObject o)
		{
			Vector2 position = o.Position;
			Vector2 velocity = o.Velocity;
			foreach (Rectangle r in leftWalls)
			{
				if (Collision.rect_collision(new Rectangle(position.ToPoint(), o.Size), r) != null)
				{
					position.X = r.X + r.Size.X;
					velocity.X = Math.Max(velocity.X, 0);
				}
			}
			foreach (Rectangle r in rightWalls)
			{
				if (Collision.rect_collision(new Rectangle(position.ToPoint(), o.Size), r) != null)
				{
					position.X = r.X - o.Size.X;
					velocity.X = Math.Min(velocity.X, 0);
				}
			}
			foreach (Rectangle r in grounds)
			{
				if (Collision.rect_collision(new Rectangle(position.ToPoint(), o.Size), r) != null)
				{
					position.Y = r.Y - o.Size.Y;
					velocity.Y = Math.Min(velocity.Y, 0);
				}
			}
			foreach (Rectangle r in roofs)
			{
				if (Collision.rect_collision(new Rectangle(position.ToPoint(), o.Size), r) != null)
				{
					position.Y = r.Y + r.Size.Y;
					velocity.Y = Math.Max(velocity.Y, 0);
				}
			}
			o.Position = position;
			o.Velocity = velocity;
		}

		public enum Wall
		{
			roof = 0,
			bottom = 1,
			left = 2,
			right = 3
		}

		public List<Rectangle>[] walls_of_ground(PlatformObject po)
		{
			BlockObject bo = new BlockObject(0, 0, po.state);
			bo.Position = new Vector2(po.x, po.y);
			return walls_of_ground(bo);
		}
		public List<Rectangle>[] walls_of_ground(BlockObject bl)
		{
			BlockObject.Ground ground = bl.state;

			List<Rectangle>[] walls = new List<Rectangle>[4];

			for (int i = 0; i < 4; i++)
			{
				walls[i] = new List<Rectangle>();
			}

			List<BlockObject.Ground> rightsG = new List<BlockObject.Ground> { BlockObject.Ground.LeftGround, BlockObject.Ground.LeftDurt, BlockObject.Ground.BottomLeft2Durt, BlockObject.Ground.LeftPlatform };
			List<BlockObject.Ground> leftsG = new List<BlockObject.Ground> { BlockObject.Ground.RightGround, BlockObject.Ground.RightDurt, BlockObject.Ground.BottomRight2Durt, BlockObject.Ground.RightPlatform };
			List<BlockObject.Ground> bottomsG = new List<BlockObject.Ground> { BlockObject.Ground.LeftGround, BlockObject.Ground.MiddleGround, BlockObject.Ground.RightGround, BlockObject.Ground.RightEGround, BlockObject.Ground.LeftEGround, BlockObject.Ground.LeftPlatform, BlockObject.Ground.RightPlatform, BlockObject.Ground.MiddlePlatform};
			List<BlockObject.Ground> roofsG = new List<BlockObject.Ground> { BlockObject.Ground.BottomDurt, BlockObject.Ground.BottomLeft2Durt, BlockObject.Ground.BottomRight2Durt, BlockObject.Ground.LeftPlatform, BlockObject.Ground.RightPlatform, BlockObject.Ground.MiddlePlatform};

			const int pixelOffset = 10;
			const int magicBorder = 10;

			if (leftsG.Exists(e => e == ground))
				walls[(int)Wall.left].Add(new Rectangle((int)(bl.Position.X + 3*bl.w/4), (int)bl.Position.Y + pixelOffset, (int)(bl.w/4), (int)(bl.h - pixelOffset*2)));

			if (rightsG.Exists(e => e == ground))
				walls[(int)Wall.right].Add(new Rectangle((int)bl.Position.X, (int)bl.Position.Y + pixelOffset, (int)(bl.w / 4), (int)(bl.h - pixelOffset*2)));

			if (roofsG.Exists(e => e == ground))
				walls[(int)Wall.roof].Add(new Rectangle((int)bl.Position.X + magicBorder, (int)(bl.Position.Y + (bl.h / 2)), (int)(bl.w - 2*magicBorder) , (int)bl.h / 2));

			if (bottomsG.Exists(e => e == ground))
				walls[(int)Wall.bottom].Add(new Rectangle((int)bl.Position.X + magicBorder, (int)(bl.Position.Y), (int)(bl.w - 2*magicBorder), (int)bl.h / 2));

				
			return walls;
		}

		

	}
}

