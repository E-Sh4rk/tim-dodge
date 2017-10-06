using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class ControlableObject : MovableObject
	{
		public ControlableObject()
		{
		}

		public ControlableObject(Vector2 pos, Vector2 vel, Vector2 acc, Vector2 fric, Vector2 wei)
			: base(pos, vel, acc, fric, wei)
		{

		}

		public enum Direction
		{
			NONE = -1,
			LEFT = 0,
			RIGHT = 1,
			TOP = 2,
			BOTTOM = 3
		}

		public Direction direction
		{
			get;
			protected set;
		}

		public void GetDirection(KeyboardState state)
		{
			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
			{
				direction = Direction.TOP;
			}

			else if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
			{
				direction = Direction.LEFT;
			}

			else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
			{
				direction = Direction.BOTTOM;
			}

			else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
			{
				direction = Direction.RIGHT;
			}

			else
			{
				direction = Direction.NONE;
			}

		}


	}
}
