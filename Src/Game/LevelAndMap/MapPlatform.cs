using System;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MapPlatform
	{
		public PlatformObject[] objs { get; protected set; }
		public float x_velocity = 0;
		public float y_velocity = 0;

		public MapPlatform(PlatformObject[] objs, float xvel, float yvel)
		{
			this.objs = objs;
			x_velocity = xvel;
			y_velocity = yvel;
		}

		public class SavePlatform
		{
			public float x_offset = 0;
			public float y_offset = 0;
			public PlatformObject.SavePlatformObject[] platform;

			// default for saving
			public SavePlatform() { }

			public SavePlatform(float x, float y, PlatformObject.SavePlatformObject[] p)
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
			return new SavePlatform(x_velocity, y_velocity, bs);
		}

		public static MapPlatform LoadPlatform(SavePlatform sp)
		{
			PlatformObject[] objs = new PlatformObject[sp.platform.Length];
			for (int i = 0; i < objs.Length; i++)
				objs[i] = PlatformObject.LoadBlock(sp.platform[i]);
			return new MapPlatform(objs, sp.x_offset, sp.y_offset);
		}

		public void Move(float elapsed)
		{
			foreach (PlatformObject o in objs)
			{
				o.x += x_velocity*elapsed*PhysicalObject.pixels_per_meter;
				o.y += y_velocity*elapsed*PhysicalObject.pixels_per_meter;
			}
		}

		public void ChangeDirection()
		{
			x_velocity = -x_velocity;
			y_velocity = -y_velocity;
		}

		public bool Intersect(Rectangle r)
		{
			foreach (PlatformObject po in objs)
			{
				if (r.Intersects(new Rectangle((int)po.x, (int)po.y, po.w, po.h)))
					return true;
			}
			return false;
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
