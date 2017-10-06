using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Player : GameObject
	{
		public Player()
		{

		}

		public Player(int totalAnimationFrames, int frameWidth, int frameHeight, World world)
			: base(totalAnimationFrames, frameWidth, frameHeight, world)
		{
			direction = Collision.Direction.RIGHT;
			frameIndex = framesIndex.R1;
			_collidedDirection = Collision.Direction.NONE;
		}


		public static bool noJump = true;
		public static bool noKey = true;
		public static bool possibleJump = true;

		public void Move(KeyboardState state)
		{
			noKey = true;
			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
			{
				direction = Collision.Direction.TOP;
				/*
				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.TOP)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.Y -= 1;
					}
				}
				*/
				if (possibleJump)
				{
					Velocity.Y = -jumpForce;
					possibleJump = false;
				}
				noKey = false;

			}

			if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
			{
				direction = Collision.Direction.LEFT;
				/*
				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.LEFT)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.X -= 1;
					}
				}
				*/
				Acceleration.X = -rightForceAcc;
				Velocity.X -= rightForceSpeed;
				noKey = false;
			}
			if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
			{
				direction = Collision.Direction.BOTTOM;
				/*
				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.BOTTOM)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.Y += 1;
					}
				}
				*/
				Velocity.Y = jumpForce;
				noKey = false;
			}
			if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
			{
				direction = Collision.Direction.RIGHT;
				/*
				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.RIGHT)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.X += 1;
					}
				}
				*/
				Acceleration.X = rightForceAcc;
				Velocity.X += rightForceSpeed;
				noKey = false;
			}

			if(noKey)
			{
				direction = Collision.Direction.NONE;
				Acceleration = new Vector2(0,0);
			}

		}

		private Collision.Direction _collidedDirection;
		public Collision.Direction collidedDirection
		{
			get { return _collidedDirection; }
			set { _collidedDirection = value; }
		}

		private float frictionX = 0.8f;
		private float frictionY = 1.0f;
		private int sol = 538;
		public Vector2 Gravity = new Vector2(0, 1);
		private int jumpForce = 15;
		private float rightForceAcc = 1.8f;
		private float rightForceSpeed = 1f;
		
		public void UpdateMove()
		{
			//Acceleration += Gravity;
			Velocity += Acceleration + Gravity;
			Acceleration.X = 0;
			Acceleration.Y = 0;
			Position += Velocity;
			Velocity.X *= frictionX;
			Velocity.Y *= frictionY;

			if (Position.Y >= sol)
			{
				possibleJump = true;
				Position.Y = sol;
				Velocity.Y = 0;
			}
			else
			{
				possibleJump = false;
			}

		}
	}
}
