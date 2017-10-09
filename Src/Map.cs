using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Map
	{
		public Rectangle[] roofs
		{
			get;
			protected set;
		}
		public Rectangle[] grounds
		{
			get;
			protected set;
		}
		public Rectangle[] leftWalls
		{
			get;
			protected set;
		}
		public Rectangle[] rightWalls
		{
			get;
			protected set;
		}

		public Map()
		{
			// TODO: Different maps system
			roofs = new Rectangle[] { };
			leftWalls = new Rectangle[] { };
			rightWalls = new Rectangle[] { };
			grounds = new Rectangle[] { new Rectangle(0, 538, TimGame.WINDOW_WIDTH, 100) };
		}
	}
}
