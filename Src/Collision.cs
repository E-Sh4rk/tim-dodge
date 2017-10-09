using System;
using Microsoft.Xna.Framework;
namespace tim_dodge
{
	public static class Collision
	{
		/**
		 * If the intersection is not null, return a unit vector in the direction that the collision force would have.
		 **/
		public static Vector2? rect_collision(Rectangle r1, Rectangle r2)
		{
			if (r1.Intersects(r2))
			{
				Vector2 dir = r2.Center.ToVector2() - r1.Center.ToVector2();
				dir.Normalize();
				return dir;
			}
			return null;
		}
		public static bool sprite_collision(Sprite s1, Sprite s2)
		{
			// TODO
			return false;
		}
		public static Vector2? optimized_sprite_collision(Sprite s1, Sprite s2)
		{
			Vector2? rect_col = rect_collision(s1.RectOfSprite(), s2.RectOfSprite());
			if (rect_col == null)
				return null;
			if (sprite_collision(s1, s2))
				return rect_col;
			return null;
		}
	}
}
