using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class ControlableObject : MovableObject
	{

		public ControlableObject(Texture t, Sprite s, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 fric, Vector2 wei)
			: base(t, s, pos, vel, acc, fric, wei)
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

		public List<Direction> direction
		{
			get;
			protected set;
		}

		public void GetDirection(KeyboardState state)
		{
			var nokey = false;
			direction = new List<Direction>();

			if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
			{
				nokey = true;
				direction.Add(Direction.LEFT);
				Sprite.ChangeDirection(direction);
			}

			if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
			{
				nokey = true;
				direction.Add(Direction.BOTTOM);
				Sprite.ChangeDirection(direction);
			}

			if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
			{
				nokey = true;
				direction.Add(Direction.RIGHT);
				Sprite.ChangeDirection(direction);
			}

			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
			{
				nokey = true;
				direction.Add(Direction.TOP);
				Sprite.ChangeDirection(direction);
			}

			if(!nokey)
			{
				direction.Add(Direction.NONE);
			}
			// TODO RIGHT/UP, LEFT/UP

		}


	}
}
