using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class BlockObject : GameObject
	{
		public BlockObject(int x, int y, Ground state) : 
		base(Load.MapTextureNature, new Sprite("Content.ground.natureXml.xml"), Vector2.Zero)
		{
			this.state = state;
			this.x = x;
			this.y = y;
		}

		public class SaveBlock
		{
			public int x;
			public int y;
			public Ground state;

			// default for saving
			public SaveBlock() { }

			public SaveBlock(int x, int y, Ground state)
			{
				this.x = x;
				this.y = y;
				this.state = state;
			}
		}

		public SaveBlock CreateSave()
		{
			return new SaveBlock(_x, _y, state);
		}

		public static BlockObject LoadBlock(SaveBlock sbl)
		{
			return new BlockObject(sbl.x, sbl.y, sbl.state);
		}

		private int _x;
		private int _y;

		public int x
		{
			get { return _x; }
			set { _x = value; position.X = value * w; }
		}

		public int y
		{
			get { return _y; }
			set { _y = value; position.Y = value * h; }
		}


		public Ground state
		{
			get { return (Ground)(Sprite.NowState());}
			set { Sprite.ChangeState((int)value);
				h = Sprite.RectOfSprite().Height;
				w = Sprite.RectOfSprite().Width;}
		}

		public int h;
		public int w;

		public enum Ground
		{
			LeftGround = 0,
			MiddleGround = 1,
			RightGround = 2,
			LeftDurt = 3,
			MiddleDurt = 4,
			RightDurt = 5,
			RightEGround = 6,
			BottomRightDurt = 7,
			BottomDurt = 8,
			BottomLeftDurt = 9,
			LeftEGround = 10,
			BottomLeft2Durt = 11,
			LeftPlatform = 12,
			MiddlePlatform = 13,
			RightPlatform = 14,
			BottomRight2Durt = 15,
			UpWater = 16,
			MiddleWater = 17,
		}
	}
}
