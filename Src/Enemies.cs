using System;
using System.Collections.Generic;
using System.Timers;

using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Enemies
	{
		private Timer timer;
		private Random random;
		public List<Enemy> ListEnemies
		{
			get;
		}
		private Texture texture = null;

		public Enemies(Texture texture)
		{
			this.texture = texture;
			random = new Random();
			timer = new Timer();
			timer.Start();
			timer.Interval = 1000;
			timer.Elapsed += GenerateEnemy;
			ListEnemies = new List<Enemy>();
		}

		private void GenerateEnemy(Object source, System.Timers.ElapsedEventArgs e)
		{
			int X = random.Next(0, TimGame.WINDOW_WIDTH);
			Enemy enemy = new Enemy(texture, null, new Vector2(X, 0));
			ListEnemies.Add(enemy);
		}
	}
}
