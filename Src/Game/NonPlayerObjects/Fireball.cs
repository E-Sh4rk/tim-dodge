using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which fall from the sky and burn the player.
	/// </summary>
	public class Fireball : NonPlayerObject
	{
		public Fireball(Vector2 p): base(Load.FireballTexture,new Sprite("Content.objects.fireball.xml"),p)
		{
			Mass = 5;
			Damage = 1;
		}

        /// <summary>
        /// When the end animation is finished, "destroy" this object
        /// </summary>
        /// <param name="elapsed">Useless here</param>
        protected void autoDestruct(float elapsed)
		{
			if (Ghost)
			{
				if (Sprite.NowFrame() >= Sprite.NumberOfFrames() - 1)
					Dead = true;
			}
		}

		protected override void ApplyCollision(Vector2 imp, PhysicalObject obj, float elapsed)
		{
			base.ApplyCollision(imp, obj, elapsed);
			Damaged = true;
		}

		public override void UpdatePosition(List<PhysicalObject> objects, Map map, float elapsed)
		{
			if (Damaged)
				destructionMode(elapsed);
			base.UpdatePosition(objects, map, elapsed);
			autoDestruct(elapsed);
		}

        /// <summary>
        /// Used to have the end animation of this object
        /// </summary>
        /// <param name="elapsed">Useless here</param>
        protected void destructionMode(float elapsed)
		{
			if (!Ghost)
			{
				//GameManager.sounds.playSound(Sound.SoundName.explosion);
				ChangeSpriteState(3);
				Ghost = true;
				Velocity = new Vector2(0, 0);
			}
		}

		public override void TouchPlayer()
		{
			Load.sounds.playSound(Sound.SoundName.fire);
		}
	}
}
