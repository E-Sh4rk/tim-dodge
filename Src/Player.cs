using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Player : ControlableObject
	{
		public Player()
		{
		}

		public Player(Vector2 pos, Vector2 vel, Vector2 acc, Vector2 fric, Vector2 wei)
			: base(pos, vel, acc, fric, wei)
		{
			JumpSpeedUp = new Vector2(0, -25);
			JumpSpeedLeft = new Vector2(-5f, -22);
			JumpSpeedRight = JumpSpeedLeft;
			JumpSpeedRight.X = -JumpSpeedRight.X;
			DashForceLeft = new Vector2(-0.5f, 0);
			DashForceRight = -DashForceLeft;
			DashSpeedLeft = new Vector2(-0.5f, 0);
			DashSpeedRight = -DashSpeedLeft;
			sprite = new Sprite();
		}

		protected Vector2 JumpSpeedUp;
		protected Vector2 JumpSpeedLeft;
		protected Vector2 JumpSpeedRight;
		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;
		protected Vector2 DashSpeedLeft;
		protected Vector2 DashSpeedRight;

	
		public bool InJump()
		{
			// TODO : condition for being in jump 
			return (Position.Y < Sol);
		}

		public void Move(KeyboardState state)
		{
			GetDirection(state); // put the direction in direction

			if (direction.Exists(el => el == Direction.TOP))
			{
				if (!InJump())
				{
					if (direction.Exists(el => el == Direction.RIGHT))
						Velocity = JumpSpeedRight;
					else if (direction.Exists(el => el == Direction.LEFT))
						Velocity = JumpSpeedLeft;
					else
						Velocity = JumpSpeedUp;
				}
			}

			if (direction.Exists(el => el == Direction.LEFT))
			{
				Acceleration = DashForceLeft;
				Velocity += DashSpeedLeft;
			}

			if (direction.Exists(el => el == Direction.RIGHT))
			{
				Acceleration = DashForceRight;
				Velocity += DashSpeedRight;
			}

			if (direction.Exists(el => el == Direction.BOTTOM))
			{
				// TODO : "S'accroupir
			}

			if (Math.Abs(Velocity.X) > 0.5)
			{
				sprite.ChangeState(Sprite.State.Walk);
			}

			else
			{
				sprite.ChangeState(Sprite.State.Stay);
			}
				
		}
	}
}
