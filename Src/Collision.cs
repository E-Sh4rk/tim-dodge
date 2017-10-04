using System;
using Microsoft.Xna.Framework;
namespace tim_dodge
{
	public static class Collision
	{
		public enum Direction
		{
			NONE = -1,
			LEFT = 0,
			RIGHT = 1,
			TOP = 2,
			BOTTOM = 3
		}

		private static Color GetColorAt(GameObject gameObject, World world)
		{
			// TODO Colision
			Color color = world.collisionColor;

			return color;
		}

		public static bool Collided(GameObject gameObject, World world)
		{
			// TODO : collision
			bool b = false;
			Color color = GetColorAt(gameObject, world);

			if (color != world.collisionColor)
				b = false;
			else
				b = true;

			return b;
		}

	}
}
