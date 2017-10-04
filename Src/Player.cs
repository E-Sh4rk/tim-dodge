using System;
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

		public void Move(KeyboardState state)
		{
			if (state.IsKeyDown(Keys.Z))
			{
				/*direction = Collision.Direction.TOP;

				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.TOP)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.Y -= 1;
					}
				}
				*/
				Position.Y -= 2;
			}
			if (state.IsKeyDown(Keys.Q))
			{
				/*direction = Collision.Direction.LEFT;

				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.LEFT)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.X -= 1;
					}
				}
				*/
				Position.X -= 2;
			}
			if (state.IsKeyDown(Keys.S))
			{
				/*direction = Collision.Direction.BOTTOM;

				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.BOTTOM)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.Y += 1;
					}
				}
				*/
				Position.Y += 2;
			}
			if (state.IsKeyDown(Keys.D))
			{
				/*direction = Collision.Direction.RIGHT;

				if (!Collision.Collided(this, world))
				{
					if (collidedDirection != Collision.Direction.RIGHT)
					{
						collidedDirection = Collision.Direction.NONE;
						Position.X += 1;
					}
				}
				*/
				Position.X += 1;
			}
		}

		private Collision.Direction _collidedDirection;
		public Collision.Direction collidedDirection
		{
			get { return _collidedDirection; }
			set { _collidedDirection = value; }
		}

	}
}
