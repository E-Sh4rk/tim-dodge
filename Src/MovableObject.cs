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

		public Sprite sprite
		{
			get;
			protected set;
		}

		public MovableObject()
		{
		}


		public MovableObject(Vector2 pos, Vector2 vel, Vector2 acc, Vector2 fric, Vector2 wei)
		{
			Position = pos;
			Acceleration = acc;
			Velocity = vel;
			Friction = fric;
			Weight = wei;
		}

		public int Sol = 538;


		public void UpdateMove()
		{
			Velocity += Acceleration + Weight;
			Position += Velocity;
			Acceleration.X = 0;
			Acceleration.Y = 0;
			Position += Velocity;
			Velocity *= Friction;

			if (Position.Y >= Sol)
			{
				Position.Y = Sol;
				Velocity.Y = 0;
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture.Image, Position, sprite.RectOfSprite(), Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), sprite.Effect, 0f);
		}



	}
}
