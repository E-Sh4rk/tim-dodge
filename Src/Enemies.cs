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
		private Map map;

		public Enemies(Texture texture, Map map)
		{
			time = 0;
			this.texture = texture;
			random = new Random();
			ListEnemies = new List<Enemy>();
			this.map = map;
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

			for (int i = 0; i < ListEnemies.Count; i++)
			{
				Enemy en = ListEnemies[i];
				if (map.nearTheGround(en))
					ListEnemies.Remove(en);
			}
		}
	}
}
