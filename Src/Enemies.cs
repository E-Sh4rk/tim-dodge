using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Enemies
	{
		private Random random;
		private GameInstance game;

		public List<Enemy> ListEnemies
		{
			get;
			protected set;
		}
		private Texture texture;

		public Enemies(Texture texture, GameInstance game)
		{
			time = 0;
			this.texture = texture;
			this.game = game;
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

			// Delete enemies on the ground
			for (int i = 0; i < ListEnemies.Count; i++)
			{
				Enemy en = ListEnemies[i];
				if (game.map.nearTheGround(en))
				{
					ListEnemies.Remove(en);
					game.score.incr(10);
				}
			}
		}
	}
}
