using System;
using System.Collections;
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
		public static bool sprite_collision(PhysicalObject o1, PhysicalObject o2)
		{
			// TODO
			return false;
		}
		public static Vector2? object_collision(PhysicalObject o1, PhysicalObject o2)
		{
			Vector2? rect_col = rect_collision(new Rectangle(o1.Position.ToPoint(), o1.Size.ToPoint()),
			                                  new Rectangle(o2.Position.ToPoint(), o2.Size.ToPoint()));
			if (rect_col == null)
				return null;
			if (sprite_collision(o1, o2))
				return rect_col;
			return null;
		}
	}
}
