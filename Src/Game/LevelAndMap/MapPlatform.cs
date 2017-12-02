using System;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MapPlatform
	{
		public PlatformObject[] objs { get; protected set; }
		public int x_offset = 0;
		public int y_offset = 0;

		public MapPlatform(PlatformObject[] objs, int xoff, int yoff)
		{
			this.objs = objs;
			x_offset = xoff;
			y_offset = yoff;
		}

		public class SavePlatform
		{
			public int x_offset = 0;
			public int y_offset = 0;
			public PlatformObject.SavePlatformObject[] platform;

			// default for saving
			public SavePlatform() { }

			public SavePlatform(int x, int y, PlatformObject.SavePlatformObject[] p)
			{
				this.x_offset = x;
				this.y_offset = y;
				this.platform = p;
			}
		}

		public SavePlatform CreateSave()
		{
			PlatformObject.SavePlatformObject[] bs = new PlatformObject.SavePlatformObject[objs.Length];
			for (int i = 0; i < bs.Length; i++)
				bs[i] = objs[i].CreateSave();
			return new SavePlatform(x_offset, y_offset, bs);
		}

		public static MapPlatform LoadPlatform(SavePlatform sp)
		{
			PlatformObject[] objs = new PlatformObject[sp.platform.Length];
			for (int i = 0; i < objs.Length; i++)
				objs[i] = PlatformObject.LoadBlock(sp.platform[i]);
			return new MapPlatform(objs, sp.x_offset, sp.y_offset);
		}

		public void Move()
		{
			foreach (PlatformObject o in objs)
			{
				o.x += x_offset;
				o.y += y_offset;
			}
		}

		public void ChangeDirection()
		{
			x_offset = -x_offset;
			y_offset = -y_offset;
		}

		public void ChangeTexture(Texture newTexture)
		{
			foreach (PlatformObject po in objs)
				po.ChangeTexture(newTexture);
		}

		public void Draw(SpriteBatch sb)
		{
			foreach (PlatformObject po in objs)
				po.Draw(sb);
		}
	}
}
