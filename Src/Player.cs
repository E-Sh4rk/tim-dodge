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
			JumpSpeed = new Vector2(0, -15);
			DashForceLeft = new Vector2(-1.8f, 0);
			DashForceRight = -DashForceLeft;
			DashSpeedLeft = new Vector2(-1, 0);
			DashSpeedRight = -DashSpeedLeft;
			sprite = new Sprite();
		}

		protected Vector2 JumpSpeed;
		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;
		protected Vector2 DashSpeedLeft;
		protected Vector2 DashSpeedRight;

	
		public bool InJump()
		{
			// TODO : condition for being in jump 
			return (Position.Y <= 538);
		}

		public void Move(KeyboardState state)
		{
			GetDirection(state); // put the direction in direction

			switch (direction)
			{
				case Direction.TOP:
					if (!InJump())
					{
						Velocity += JumpSpeed;
					}
					break;

				case Direction.LEFT:
					Acceleration = DashForceLeft;
					Velocity += DashSpeedLeft;
					break;

				case Direction.RIGHT:
					Acceleration = DashForceRight;
					Velocity += DashSpeedRight;
					break;

				case Direction.BOTTOM:
					//Acceleration = DashForceLeft;
					//Velocity += DashSpeedLeft;
					break;

				case Direction.NONE:
					//
					break;
			}

		}
	}
}
