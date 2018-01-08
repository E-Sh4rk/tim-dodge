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
		GameInstance g;

		public Monstar(Vector2 p, GameInstance g, Controller.Direction dir):
		base(Load.MonstarTexture,new Sprite("Content.character.MonstarXml.xml"),p)
		{
			Mass = 25;
			Damage = 1;
			this.g = g;
			Sprite.ChangeDirection(dir);
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

		public void Move(float elapsed)
		{
			if (g.Level.Current.map.pMap.nearLeftWall(this))
				Sprite.ChangeDirection(Controller.Direction.RIGHT);
			if (g.Level.Current.map.pMap.nearRightWall(this))
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

		protected override void ApplyCollision(Vector2 imp, PhysicalObject obj, float elapsed)
		{
			base.ApplyCollision(imp, obj, elapsed);
			if (obj is Monstar)
			{
				Controller.Direction other_dir = Controller.Direction.LEFT;
				if (Sprite.Direction == other_dir)
					other_dir = Controller.Direction.RIGHT;
				Sprite.ChangeDirection(other_dir);
			}
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
