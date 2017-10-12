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
			Mass = 5;
			forces = new Vector2(0, 0);
			impulsions = new Vector2(0, 0);
			precomputed_collisions = new SortedSet<int>();
			collisions_impulsion = new Vector2(0, 0);
		}

		// Position is part of GameObject
		protected Vector2 velocity;
		protected Vector2 friction;
		protected Vector2 forces;
		protected Vector2 impulsions;

		protected SortedSet<int> precomputed_collisions;
		protected Vector2 collisions_impulsion;

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
			forces += force;
		}
		public void ApplyNewImpulsion(Vector2 imp)
		{
			impulsions += imp;
		}

		const float collision_factor = 1.25f;
		const float gravity = 9.81f;
		const float ground_friction = 10.0f;
		const float air_friction = 1.0f;
		const float pixels_per_meter = 250;

		protected void AlreadyComputedCollision(Vector2 imp, int id)
		{
			precomputed_collisions.Add(id);
			collisions_impulsion += imp;
		}

		public void ApplyForces(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			double dt = gameTime.ElapsedGameTime.TotalSeconds;
			// Compute gravity, friction...
			ApplyNewForce(new Vector2(0.0f, gravity * Mass));
			// TODO: Improve friction
			if (map.nearTheGround(this))
				ApplyNewForce(velocity * (-ground_friction) * Mass);
			else
				ApplyNewForce(velocity * (-air_friction) * Mass);

			// Compute new velocity by taking into account all the forces and impulsions
			velocity += (impulsions + forces * (float)dt)/Mass;
			forces = new Vector2(0, 0);
			impulsions = new Vector2(0, 0);
		}
		public void ApplyCollisions(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			double dt = gameTime.ElapsedGameTime.TotalSeconds;
			// Compute collisions with other physical objects and, depending on the relative direction of the center of the sprite,
			// compute what resulting force in this direction need to be applied (depending on the difference of velocity in this direction and
			// the min mass of the two objects).
			foreach (PhysicalObject o in objects)
			{
				if (o.ID == ID || precomputed_collisions.Contains(o.ID))
					continue;
				Vector2? coll_opt = Collision.object_collision(this, o);
				if (coll_opt == null)
					continue;
				Vector2 coll = coll_opt.Value;
				Vector2 rel_velocity = velocity - o.velocity;
				float prod = coll.X * rel_velocity.X + coll.Y * rel_velocity.Y;
				if (prod <= 0)
					continue;
				float min_mass = Math.Min(o.Mass, Mass);
				float intensity = -collision_factor * (min_mass * prod);
				collisions_impulsion += coll * intensity;
				o.AlreadyComputedCollision(-coll * intensity, ID);
			}
			velocity += collisions_impulsion / Mass;
			precomputed_collisions.Clear();
			collisions_impulsion = new Vector2(0,0);
		}
		public void UpdatePosition(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * pixels_per_meter;
			map.adjustPositionAndVelocity(this);
		}
	}
}
