using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/**
	 * An object that is subject to collisions and gravity.
	**/
	public class PhysicalObject : GameObject
	{
		public PhysicalObject()
		{
		}

		// Position is part of GameObject
		protected Vector2 Velocity;
		protected Vector2 Acceleration;
		protected Vector2 Friction;
		protected List<Vector2> forces;

		protected List<int> reciprocal_forces; // To exclude for collisions because already computed

		public int mass
		{
			get;
			protected set;
		}

		public void ApplyNewForce(Vector2 force, int reciprocal_id = -1)
		{
			// TODO
		}

		public void UpdatePosition()
		{
			// Compute collisions with other physical objects and, depending on the relative direction of the center of the sprite,
			// compute what resulting force in this direction need to be applied (depending on the difference of velocity in this direction and
			// the min mass of the two objects).
			// Add this resulting force to the both objects involved.
			// TODO

			// Compute new acceleration, velocity and position by taking into account all the forces
			// TODO

			// Compute collisions with the map (which has infinite mass), and apply the new resulting forces
			// TODO
		}
	}
}
