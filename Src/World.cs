using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class World : GameObject
	{
		public World()
		{

		}

		public World(Color collisionColor)
		{
			_collisionColor = collisionColor;
		}

		public Color[] colorTab;

		private Color _collisionColor;
		public Color collisionColor
		{
			get { return _collisionColor; }
		}

	}
}
