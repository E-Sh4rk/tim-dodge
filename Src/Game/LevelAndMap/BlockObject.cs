using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class BlockObject : GameObject
	{

		public BlockObject(Texture texture, int x, int y, Ground state) : 
		base(texture, new Sprite("Content.ground.natureXml.xml"), Vector2.Zero)
		{
			Sprite.ChangeState((int)state);
			h = Sprite.RectOfSprite().Height;
			w = Sprite.RectOfSprite().Width;
			this.x = x;
			this.y = y;
			this.state = state;
		}

		private int _x;
		private int _y;

		public int x
		{
			get { return _x; }
			set { _x = value; position.X = value * h; }
		}

		public int y
		{
			get { return _y; }
			set { _y = value; position.Y = value * w; }
		}


		public Ground state;

		public float h;
		public float w;

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
