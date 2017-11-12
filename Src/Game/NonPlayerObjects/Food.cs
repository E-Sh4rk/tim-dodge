using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Bonus which fall from the sky and add one half-heart to the player.
	/// </summary>
	public class Food : NonPlayerObject
	{
		public Food(Vector2 p) : base(Load.FoodTexture, new Sprite("Content.objects.food.xml"), p)
		{
			Mass = 5;
			Bonus = 10;
			Life = 1;
			Sprite.ChangeState(14);
		}

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
			Load.sounds.playSound(Sound.SoundName.food);
		}
	}
}
