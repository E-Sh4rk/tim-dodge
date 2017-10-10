using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class MovableObject : GameObject
	{
		//protected Vector2 Position;
		protected Vector2 Velocity;
		protected Vector2 Acceleration;
		protected Vector2 Friction;
		protected Vector2 Weight;

		public MovableObject(Texture t, Sprite s, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 fric, Vector2 wei): base (t, s)
		{
			position = pos;
			Acceleration = acc;
			Velocity = vel;
			Friction = fric;
			Weight = wei;
		}

		public int Sol = 538;


		public void UpdateMove()
		{
			Velocity += Acceleration + Weight;
			position += Velocity;
			Acceleration.X = 0;
			Acceleration.Y = 0;
			position += Velocity;
			Velocity *= Friction;

			if (position.Y >= Sol)
			{
				position.Y = Sol;
				Velocity.Y = 0;
			}

		}

	}
}
