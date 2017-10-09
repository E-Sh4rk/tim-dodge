using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class GameObject
	{
		public Texture Texture;
		public Sprite Sprite;
		public Vector2 Position;

		public GameObject()
		{
			Texture = new Texture();
			Sprite = null;
		}

		public Vector2 Size
		{
			get
			{
				if (Sprite != null)
					return new Vector2(Sprite.RectOfSprite().Size.X, Sprite.RectOfSprite().Size.Y);
				if (Texture == null)
					return new Vector2(0,0);
				return new Vector2(Texture.Image.Width, Texture.Image.Height);
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
				spriteBatch.Draw(Texture.Image, Position, new Rectangle(TexturePosition, Size.ToPoint()), Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
			else
				spriteBatch.Draw(Texture.Image, Position, Color.White);
		}

	}
}
