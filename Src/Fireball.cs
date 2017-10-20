using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Fireball : Enemy
	{
		public Fireball(Texture t, Sprite s, Vector2 p, GameInstance gi): base(t,s,p,gi)
		{
			Mass = 5;
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
			// Destroy the fireball if collision with something else
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
				GameManager.sounds.playSound(Sound.SoundName.explosion);
				ChangeSpriteState(3);
				Ghost = true;
				Velocity = new Vector2(0, 0);
			}
		}
	}
}
