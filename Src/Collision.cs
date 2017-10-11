using System;
using System.Collections;
using Microsoft.Xna.Framework;
namespace tim_dodge
{
	public static class Collision
	{
		const bool center_of_gravity_bottom = true;
		static Point center_of_gravity(Rectangle r)
		{
			Point center = r.Center;
			if (center_of_gravity_bottom)
				center.Y = r.Y+r.Height;
			return center;
		}
		/**
		 * If the intersection is not null, return a unit vector in the direction that the collision force would have.
		 **/
		public static Vector2? rect_collision(Rectangle r1, Rectangle r2)
		{
			if (r1.Intersects(r2))
			{
				Point dir = center_of_gravity(r2) - center_of_gravity(r1);
				if (dir.X == 0 && dir.Y == 0)
					return new Vector2(0, 0);
				Vector2 v = dir.ToVector2();
				v.Normalize();
				return v;
			}
			return null;
		}
		public static bool sprite_collision(PhysicalObject o1, PhysicalObject o2)
		{
			// Compute intersection rectangle
			Rectangle r1 = new Rectangle(o1.Position.ToPoint(), o1.Size);
			Rectangle r2 = new Rectangle(o2.Position.ToPoint(), o2.Size);
			Point intersection1 = new Point(Math.Max(r1.X, r2.X), Math.Max(r1.Y, r2.Y));
			Point intersection2 = new Point(Math.Min(r1.X+r1.Width, r2.X+r2.Width), Math.Min(r1.Y+r1.Height, r2.Y+r2.Height));
			Rectangle inter = new Rectangle(intersection1, intersection2-intersection1);
			// Compare texture masks in this rectangle
			Point offset1 = o1.TexturePosition - o1.Position.ToPoint();
			int width1 = o1.Texture.Image.Width;
			Point offset2 = o2.TexturePosition - o2.Position.ToPoint();
			int width2 = o2.Texture.Image.Width;
			for (int i = inter.X; i < inter.X + inter.Width; i++)
			{
				for (int j = inter.Y; j < inter.Y + inter.Height; j++)
				{
					Point pos1 = new Point(i, j) + offset1;
					Point pos2 = new Point(i, j) + offset2;
					if (o1.Texture.Mask[pos1.X + pos1.Y * width1] && o2.Texture.Mask[pos2.X + pos2.Y * width2])
						return true;
				}
			}
			return false;
		}
		public static Vector2? object_collision(PhysicalObject o1, PhysicalObject o2)
		{
			Vector2? rect_col = rect_collision(new Rectangle(o1.Position.ToPoint(), o1.Size),
			                                   new Rectangle(o2.Position.ToPoint(), o2.Size));
			if (rect_col == null)
				return null;
			if (sprite_collision(o1, o2))
				return rect_col;
			return null;
		}
	}
}
