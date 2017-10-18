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

		protected override void ApplyCollision(Vector2 imp, int id, GameTime gt)
		{
			base.ApplyCollision(imp, id, gt);
			Dead = true;
		}
	}
}
