using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Bonus which fall from the sky and add bonus score to the player.
	/// </summary>
	public class Coin : NonPlayerObject
	{
		public Coin(Vector2 p) : base(Load.CoinTexture, new Sprite("Content.objects.coin.xml"), p)
		{
			Mass = 7;
			Bonus = 250;
		}

        /// <summary>
        /// Wait a certain amount of time before "destroying" this object
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        protected void autoDestruct(float elapsed)
		{
			if (Damaged)
			{
				wait_before_die -= elapsed;
				if (wait_before_die <= 0f)
					Dead = true;
			}
		}

		protected override void ApplyCollision(Vector2 imp, PhysicalObject obj, float elapsed)
		{
			base.ApplyCollision(imp, obj, elapsed);
			Damaged = true;
			wait_before_die = 0f;
		}

		public override void UpdatePosition(List<PhysicalObject> objects, Map map, float elapsed)
		{
			base.UpdatePosition(objects, map, elapsed);
			autoDestruct(elapsed);
		}

		float wait_before_die = 1.0f;

		public override void TouchPlayer()
		{
			Load.sounds.playSound(Sound.SoundName.coin);
		}
	}
}
