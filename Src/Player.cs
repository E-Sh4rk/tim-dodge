using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	public class Player : PhysicalObject
	{
		public Player(Texture t, Sprite s, Vector2 pos, GameInstance gi)
			: base(t, s, pos)
		{
			JumpImpulsion = new Vector2(0f, -250f);
			DashForceLeft = new Vector2(-1000f, 0f);
			DashForceRight = -DashForceLeft;
			this.map = gi.map;
			Mass = 50;
			gameInst = gi;
			s.ChangeDirection(Controller.Direction.RIGHT);
		}

		protected SoundEffect jump;
		protected Map map;

		protected Vector2 JumpImpulsion;
		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;

		protected GameInstance gameInst;

		protected float elapsed_since_last_jump = 0;
		const float min_time_between_jump = 0.25f;
		public bool CanJump()
		{
			return map.nearTheGround(this) && elapsed_since_last_jump >= min_time_between_jump;
		}

		public void Move(KeyboardState state, GameTime gameTime)
		{
			List<Controller.Direction> directions = Controller.GetDirections(state);

			elapsed_since_last_jump += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (directions.Exists(el => el == Controller.Direction.TOP))
			{
				if (CanJump())
				{
					gameInst.sounds.playSound(Sound.SoundName.jump);
					elapsed_since_last_jump = 0;
					map.magnetizeToGround(this);
					ApplyNewImpulsion(JumpImpulsion);
				}
			}

			if (directions.Exists(el => el == Controller.Direction.LEFT))
			{
				Sprite.ChangeDirection(Controller.Direction.LEFT);
				if (velocity.X >= -3)
					ApplyNewForce(DashForceLeft);
			}

			if (directions.Exists(el => el == Controller.Direction.RIGHT))
			{
				Sprite.ChangeDirection(Controller.Direction.RIGHT);
				if (velocity.X <= 3)
					ApplyNewForce(DashForceRight);
			}

			if (directions.Exists(el => el == Controller.Direction.BOTTOM))
			{
				// TODO : "S'accroupir
			}

			if (Math.Abs(Velocity.X) > 0.3)
				ChangeSpriteState(Sprite.State.Walk);
			else
				ChangeSpriteState(Sprite.State.Stay);
		}
	}
}
