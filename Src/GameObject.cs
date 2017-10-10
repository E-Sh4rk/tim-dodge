using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class GameObject
	{
		private static int next_id = 1;

		private int id = 0;
		public int ID { get { return id; } }

		public Texture Texture
		{
			get;
		}
		public Sprite Sprite
		{
			get;
		}
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

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Sprite != null)
				spriteBatch.Draw(Texture.Image, position, new Rectangle(TexturePosition, Size), Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
			else
				spriteBatch.Draw(Texture.Image, position, Color.White);
		}

	}
}
