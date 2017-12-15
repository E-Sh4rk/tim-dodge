using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	/// <summary>
	/// Some utility functions to handle user's inputs.
	/// </summary>
	public static class Controller
	{
		public enum Direction
		{
			LEFT = 0,
			RIGHT = 1,
			TOP = 2,
			BOTTOM = 3
		}

		public static List<Direction> GetDirectionsPlayer1(KeyboardState state)
		{
			List<Direction> directions = new List<Direction>();

			if (state.IsKeyDown(Keys.Left))
				directions.Add(Direction.LEFT);

			if (state.IsKeyDown(Keys.Down))
				directions.Add(Direction.BOTTOM);

			if (state.IsKeyDown(Keys.Right))
				directions.Add(Direction.RIGHT);

			if (state.IsKeyDown(Keys.Up))
				directions.Add(Direction.TOP);

			return directions;
		}
		public static List<Direction> GetDirectionsPlayer2(KeyboardState state)
		{
			List<Direction> directions = new List<Direction>();

			if (state.IsKeyDown(Keys.Q))
				directions.Add(Direction.LEFT);

			if (state.IsKeyDown(Keys.S))
				directions.Add(Direction.BOTTOM);

			if (state.IsKeyDown(Keys.D))
				directions.Add(Direction.RIGHT);

			if (state.IsKeyDown(Keys.Z))
				directions.Add(Direction.TOP);

			return directions;
		}

		public static bool RewindKeyDown(KeyboardState state)
		{
            return state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
        }

		public static bool KeyPressed(Keys key)
		{	// Return True iff the Key key just has been pressed
			return TimGame.previousKeyState.IsKeyUp(key) && TimGame.currentKeyState.IsKeyDown(key);
		}

	}
}
