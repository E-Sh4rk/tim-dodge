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
		}
		private Texture texture = null;

		public Enemies(Texture texture)
		{
			time = 0;
			this.texture = texture;
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
		}
	}
}
