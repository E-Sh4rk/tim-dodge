using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class PlatformObject : GameObject
	{
		public PlatformObject(int x, int y, BlockObject.Ground state) :
		base(Load.MapTextureNature, new Sprite("Content.ground.natureXml.xml"), new Vector2(x,y))
		{
			Sprite.ChangeState((int)state);
			this.state = state;
		}

		public class SavePlatformObject
		{
			public int x;
			public int y;
			public BlockObject.Ground state;

			// default for saving
			public SavePlatformObject() { }

			public SavePlatformObject(int x, int y, BlockObject.Ground state)
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

		public int x
		{
			get { return (int)position.X; }
			set { position.X = value; }
		}

		public int y
		{
			get { return (int)position.Y; }
			set { position.Y = value; }
		}


		public BlockObject.Ground state
		{
			get { return (BlockObject.Ground)(Sprite.NowState()); }
			set { Sprite.ChangeState((int)value); }
		}

	}
}
