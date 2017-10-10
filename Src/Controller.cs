using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public static class Controller
	{
		public enum Direction
		{
			LEFT = 0,
			RIGHT = 1,
			TOP = 2,
			BOTTOM = 3
		}

		public static List<Direction> GetDirections(KeyboardState state)
		{
			List<Direction> directions = new List<Direction>();

			if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
				directions.Add(Direction.LEFT);

			if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
				directions.Add(Direction.BOTTOM);

			if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
				directions.Add(Direction.RIGHT);

			if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
				directions.Add(Direction.TOP);

			return directions;
		}
	}
}
