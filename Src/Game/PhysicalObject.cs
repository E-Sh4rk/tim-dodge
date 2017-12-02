using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Represents an object that is subject to the physical laws (forces, impulsions, collisions...).
	/// </summary>
	public class PhysicalObject : GameObject
	{
		public PhysicalObject(Texture t, Sprite s, Vector2 p): base (t, s, p)
		{
			Mass = 5;
			forces = new Vector2(0, 0);
			impulsions = new Vector2(0, 0);
			already_computed_collisions = new SortedSet<int>();
			collisions_impulsion = new Vector2(0, 0);
			Ghost = false;
		}

		// Position is part of GameObject
		protected Vector2 velocity;
		protected Vector2 forces;
		protected Vector2 impulsions;

		protected SortedSet<int> already_computed_collisions;
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
		/// <summary>
		/// Set Ghost to true if you want the object to stop interacting with the environment.
		/// </summary>
		public bool Ghost
		{
			get;
			set;
		}

		public void ApplyNewForce(Vector2 force)
		{
			forces += force;
		}
		public void ApplyNewImpulsion(Vector2 imp)
		{
			impulsions += imp;
		}

		public const float collision_factor = 1.25f;
		public const float gravity = 9.81f;//24.79f;//1.622f;//9.81f;
		public const float ground_friction = 10.0f;
		public const float air_friction = 1.0f;
		public const float pixels_per_meter = 250;

		protected virtual void ApplyCollision(Vector2 imp, PhysicalObject obj, float elapsed)
		{
			already_computed_collisions.Add(obj.ID);
			collisions_impulsion += imp;
		}

		public void ApplyForces(List<PhysicalObject> objects, Map map, float elapsed)
		{
			double dt = elapsed;

			if (!Ghost)
			{
				// Compute gravity, friction...
				ApplyNewForce(new Vector2(0.0f, gravity * Mass));
				// TODO: Improve friction
				if (map.pMap.nearTheGround(this))
					ApplyNewForce(velocity * (-ground_friction) * Mass);
				else
					ApplyNewForce(velocity * (-air_friction) * Mass);
			}

			// Compute new velocity by taking into account all the forces and impulsions
			velocity += (impulsions + forces * (float)dt)/Mass;
			forces = new Vector2(0, 0);
			impulsions = new Vector2(0, 0);
		}
		public void ApplyCollisions(List<PhysicalObject> objects, Map map, float elapsed)
		{
			double dt = elapsed;
			
			if (!Ghost)
			{
				// Compute collisions with other physical objects and, depending on the relative direction of the center of the sprite,
				// compute what resulting force in this direction need to be applied (depending on the difference of velocity in this direction and
				// the min mass of the two objects).
				foreach (PhysicalObject o in objects)
				{
					if (o.ID == ID || o.Ghost || already_computed_collisions.Contains(o.ID))
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
					ApplyCollision(coll * intensity, o, elapsed);
					o.ApplyCollision(-coll * intensity, this, elapsed);
				}
			}

			// Compute new velocity
			velocity += collisions_impulsion / Mass;
			already_computed_collisions.Clear();
			collisions_impulsion = new Vector2(0,0);
		}
		public virtual void UpdatePosition(List<PhysicalObject> objects, Map map, float elapsed)
		{
			float xref = map.pMap.getXReferential(this);
			double dt = elapsed;
			position += (velocity + new Vector2(xref, 0)) * (float)dt * pixels_per_meter;
			map.pMap.adjustPositionAndVelocity(this);
		}
	}
}
