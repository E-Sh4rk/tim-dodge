using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents a basic game object, that is characterized by a texture/sprite and a position.
	/// </summary>
	public class GameObject
	{
		private static int next_id = 1;

		private int id = 0;
		public int ID { get { return id; } }

		public static float time_multiplicator = 1.0f;
		protected bool insensible_to_time_modif = false;

		public Texture Texture
		{
			get;
			protected set;
		}

		public Sprite Sprite
		{
			get;
		}

		protected Color color = Color.White; 

		protected Vector2 position = new Vector2(0.0f, 0.0f);
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public GameObject(Texture texture, Sprite sprite, Vector2 pos)
		{
			Texture = texture;
			Sprite = sprite;
			Position = pos;
			id = next_id;
			next_id++;
		}

		public Point Size
		{
			get
			{
				if (Sprite != null)
					return new Point(Sprite.RectOfSprite().Size.X, Sprite.RectOfSprite().Size.Y);
				if (Texture == null)
					return new Point(0,0);
				return new Point(Texture.Image.Width, Texture.Image.Height);
			}
		}
		public Point TexturePosition
		{
			get
			{
				if (Sprite == null)
					return new Point(0, 0);
				return new Point(Sprite.RectOfSprite().X, Sprite.RectOfSprite().Y);
			}
		}

		private void adjustSpritePosition(Point size, Point new_size)
		{
			if (size.X != new_size.X)
			{
				if (Sprite.Direction == Controller.Direction.RIGHT)
					position.X += size.X - new_size.X;
				else if (Sprite.Direction == Controller.Direction.LEFT) { }
				else
					position.X += (size.X - new_size.X) / 2;
			}

			if (size.Y != new_size.Y)
			{
				if (Sprite.Direction == Controller.Direction.TOP)
					position.Y += (size.Y - new_size.Y) / 2;
				else
					position.Y += size.Y - new_size.Y;
			}
		}
		public virtual void UpdateSprite(GameTime gt)
		{
			if (Sprite != null)
			{
				double dt = gt.ElapsedGameTime.TotalSeconds;
				if (!insensible_to_time_modif)
					dt *= time_multiplicator;
				Point size = Size;
				Sprite.UpdateFrame(dt);
				Point new_size = Size;
				adjustSpritePosition(size, new_size);
			}
		}
		public virtual void ChangeSpriteState(int state)
		{
			if (Sprite != null)
			{
				Point size = Size;
				Sprite.ChangeState(state);
				Point new_size = Size;
				adjustSpritePosition(size, new_size);
			}
		}

		public bool IsOutOfBounds()
		{
			if (Position.X > TimGame.WINDOW_WIDTH || Position.X < -Size.X)
				return true;
			if (Position.Y > TimGame.WINDOW_HEIGHT || Position.Y < -Size.Y)
				return true;
			return false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Sprite != null)
				spriteBatch.Draw(Texture.Image, position, new Rectangle(TexturePosition, Size), color, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
			else
				spriteBatch.Draw(Texture.Image, position, color);
		}

	}
}
