using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Enemies
	{
		private Random random;
		public List<Enemy> ListEnemies
		{
			get;
			protected set;
		}
		private Texture texture = null;
		private Map map = null;

		public Enemies(Texture texture, Map map)
		{
			time = 0;
			this.texture = texture;
			this.map = map;
			random = new Random();
			ListEnemies = new List<Enemy>();
		}

		const int interval = 1;

		private float time;
		public void UpdateEnemies(GameTime gt)
		{
			time += (float)gt.ElapsedGameTime.TotalSeconds;

			while (time > interval)
			{
				int X = random.Next(0, TimGame.WINDOW_WIDTH);
				Enemy enemy = new Enemy(texture, null, new Vector2(X, -30));
				ListEnemies.Add(enemy);
				time -= interval;
			}

			// Delete enemies that are out of bounds
			ListEnemies.RemoveAll((e => e.IsOutOfBounds()));
		}
	}
}
