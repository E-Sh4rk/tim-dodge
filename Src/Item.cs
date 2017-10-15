using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Item
	{
		public Rectangle source;
		public Color Color;

		protected String Text;
		private SpriteFont fontItem;
		private Texture2D Image;
		private Vector2 Size;
		private Vector2 position;
		private Vector2 origin;

		public Vector2 Position 
		{
			get { return position; }
			set
			{
				position = value;
				source.X = (int)(position.X + origin.X);
				source.Y = (int)(position.Y + origin.X);

			}
		}

		public Vector2 Origin
		{
			get { return origin; }
			set
			{
				origin = value;
				source.X = (int)(position.X + origin.X);
				source.Y = (int)(position.Y + origin.X);
			}

		}

		public Item(String Text, SpriteFont fontItem)
		{
			this.Text = Text;
			this.fontItem = fontItem;
			Size = fontItem.MeasureString(Text);
			DefaultConstruct();
		}

		public Item(Texture2D Image, Vector2 Size)
		{
			this.Image = Image;
			this.Size = Size;
			DefaultConstruct();
		}

		private void DefaultConstruct()
		{
			Color = Color.White;
			position = Vector2.Zero;
			origin = Vector2.Zero;
			source = new Rectangle((int)(position.X + origin.X),
			                       (int)(position.Y + origin.X),
			                       (int)(Size.X),
			                       (int)(Size.Y));
		}

		public void unsetColor()
		{
			Color = Color.White;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Text != null && fontItem != null)
			{
				spriteBatch.DrawString(fontItem, Text, Position + Origin, Color);
			}

			else if (Image != null)
			{
				spriteBatch.Draw(Image, source, Color);
				                 
			}
				
		}
	}
}
