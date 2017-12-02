using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class PlatformObject : GameObject
	{
		public PlatformObject(float x, float y, BlockObject.Ground state) :
		base(Load.MapTextureNature, new Sprite("Content.ground.natureXml.xml"), new Vector2(x,y))
		{
			this.state = state;
			this.x = x;
			this.y = y;
		}

		public int h;
		public int w;

		public class SavePlatformObject
		{
			public float x;
			public float y;
			public BlockObject.Ground state;

			// default for saving
			public SavePlatformObject() { }

			public SavePlatformObject(float x, float y, BlockObject.Ground state)
			{
				this.x = x;
				this.y = y;
				this.state = state;
			}
		}

		public SavePlatformObject CreateSave()
		{
			return new SavePlatformObject(x, y, state);
		}

		public static PlatformObject LoadBlock(SavePlatformObject sbl)
		{
			return new PlatformObject(sbl.x, sbl.y, sbl.state);
		}

		public float x
		{
			get { return position.X; }
			set { position.X = value; }
		}

		public float y
		{
			get { return position.Y; }
			set { position.Y = value; }
		}

		public BlockObject.Ground state
		{
			get { return (BlockObject.Ground)(Sprite.NowState()); }
			set
			{
				Sprite.ChangeState((int)value);
				h = Sprite.RectOfSprite().Height;
				w = Sprite.RectOfSprite().Width;
			}
		}

	}
}
