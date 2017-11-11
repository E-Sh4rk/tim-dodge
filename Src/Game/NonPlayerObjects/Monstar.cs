using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which walk in the ground and hurt the player.
	/// </summary>
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

		protected override void ApplyCollision(Vector2 imp, PhysicalObject obj, GameTime gt)
		{
			base.ApplyCollision(imp, obj, gt);
			if (obj is Monstar)
			{
				Controller.Direction other_dir = Controller.Direction.LEFT;
				if (Sprite.Direction == other_dir)
					other_dir = Controller.Direction.RIGHT;
				Sprite.ChangeDirection(other_dir);
			}
		}

		public override void UpdatePosition(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			if (Damaged)
				destructionMode(gameTime);
			base.UpdatePosition(objects, map, gameTime);
			autoDestruct(gameTime);
		}

		protected void destructionMode(GameTime gt)
		{
			if (!Ghost)
			{
				ChangeSpriteState(1);
				Ghost = true;
				Velocity = new Vector2(0, 0);
			}
		}

		public override void TouchPlayer()
		{
			Load.sounds.playSound(Sound.SoundName.ah);
		}
	}
}
