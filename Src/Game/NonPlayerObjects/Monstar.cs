using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Monstar : NonPlayerObject
	{
		public Monstar(Texture t, Sprite s, Vector2 p): base(t,s,p)
		{
			Mass = 25;
			Damage = 1;
		}

		protected void autoDestruct(GameTime gameTime)
		{
			if (Ghost)
			{
				if (Sprite.NowFrame() >= Sprite.NumberOfFrames() - 1)
					Dead = true;
			}
		}

		protected bool damaged = false;
		protected override void ApplyCollision(Vector2 imp, int id, GameTime gt)
		{
			base.ApplyCollision(imp, id, gt);
			damaged = true;
		}

		public override void UpdatePosition(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			if (damaged)
				destructionMode(gameTime);
			base.UpdatePosition(objects, map, gameTime);
			autoDestruct(gameTime);
		}

		public override void destructionMode(GameTime gt)
		{
			if (!Ghost)
			{
				ChangeSpriteState(1);
				Ghost = true;
				Velocity = new Vector2(0, 0);
			}
		}

		public override void TouchPlayer() { }
	}
}
