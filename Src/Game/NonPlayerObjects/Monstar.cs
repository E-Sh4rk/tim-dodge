using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Monstar : NonPlayerObject
	{
		PhysicalMap m;

		public Monstar(Texture t, Sprite s, Vector2 p, PhysicalMap m, Controller.Direction dir): base(t,s,p)
		{
			Mass = 25;
			Damage = 1;
			this.m = m;
			Sprite.ChangeDirection(dir);
		}

		protected void autoDestruct(GameTime gameTime)
		{
			if (Ghost)
			{
				if (Sprite.NowFrame() >= Sprite.NumberOfFrames() - 1)
					Dead = true;
			}
		}

		public void Move(GameTime gameTime)
		{
			if (m.nearLeftWall(this))
				Sprite.ChangeDirection(Controller.Direction.RIGHT);
			if (m.nearRightWall(this))
				Sprite.ChangeDirection(Controller.Direction.LEFT);
			if (Sprite.Direction == Controller.Direction.LEFT)
			{
				if (velocity.X >= -1)
					ApplyNewForce(new Vector2(-500f, 0f));
			}
			if (Sprite.Direction == Controller.Direction.RIGHT)
			{
				if (velocity.X <= 1)
					ApplyNewForce(new Vector2(500f, 0f));
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
