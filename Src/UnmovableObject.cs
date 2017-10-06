using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class UnmovableObject : GameObject
	{
		public UnmovableObject()
		{
		}

		public UnmovableObject(Vector2 pos)
		{
			Position = pos;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Color.White);
		}

	}
}
