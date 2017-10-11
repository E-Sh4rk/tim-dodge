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
		public PhysicalObject(Texture t, Sprite s, Vector2 p): base (t, s, p)
		{
			forces = new List<Vector2>();
			impulsions = new List<Vector2>();
			reciprocal_collisions = new SortedSet<int>();
		}

		// Position is part of GameObject
		protected Vector2 velocity;
		protected Vector2 friction;
		protected List<Vector2> forces;
		protected List<Vector2> impulsions;

		protected SortedSet<int> reciprocal_collisions; // To exclude for collisions because already computed

		public float Mass
		{
			get;
			protected set;
		}
		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		public void ApplyNewForce(Vector2 force)
		{
			forces.Add(force);
		}
		public void ApplyNewImpulsion(Vector2 imp, int reciprocal_id = -1)
		{
			impulsions.Add(imp);
			if (reciprocal_id >= 0)
				reciprocal_collisions.Add(reciprocal_id);
		}

		const float collision_factor = 1.0f;
		const float gravity = 9.81f;
		const float ground_friction = 10.0f;
		const float air_friction = 1.0f;
		const float pixels_per_meter = 250;
		public void UpdatePosition(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			if (Mass <= 0.000001f)
				return;
			double dt = gameTime.ElapsedGameTime.TotalSeconds;
			if (dt <= 0.000001f)
				return;

			// Compute gravity, friction...
			forces.Add(new Vector2(0.0f, gravity * Mass));
			// TODO: Improve friction
			if (map.onTheGround(this))
				forces.Add(new Vector2(-ground_friction * Mass * velocity.X, 0.0f));
			else
				forces.Add(velocity * (-air_friction) * Mass);


			// Compute collisions with other physical objects and, depending on the relative direction of the center of the sprite,
			// compute what resulting force in this direction need to be applied (depending on the difference of velocity in this direction and
			// the min mass of the two objects).
			// Add this resulting force to both objects involved.
			foreach (PhysicalObject o in objects)
			{
				if (o.ID == ID || reciprocal_collisions.Contains(o.ID))
					continue;
				Vector2? coll_opt = Collision.object_collision(this, o);
				if (coll_opt == null)
					continue;
				Vector2 coll = coll_opt.Value;
				Vector2 rel_velocity = velocity - o.velocity;
				float prod = coll.X * rel_velocity.X + coll.Y * rel_velocity.Y;
				// TODO: see for the sign, test and fix formulas
				float min_mass = Math.Min(o.Mass, Mass);
				float intensity = collision_factor * (min_mass * prod);
				impulsions.Add(coll*intensity);
				o.ApplyNewImpulsion(-coll * intensity, ID);
			}

			// Compute new acceleration, velocity and position by taking into account all the forces
			Vector2 sum = new Vector2(0.0f, 0.0f);
			foreach (Vector2 force in forces)
				sum += force;
			foreach (Vector2 imp in impulsions)
				sum += imp / (float)dt;
			Vector2 a = sum / Mass;
			position += (0.5f * a * (float)dt * (float)dt + velocity * (float)dt)*pixels_per_meter;
			velocity += a * (float)dt;

			// Compute collisions with the map (which has infinite mass), and adjust position
			map.adjustPositionAndVelocity(this);

			forces.Clear();
			impulsions.Clear();
			reciprocal_collisions.Clear();
		}
	}
}
