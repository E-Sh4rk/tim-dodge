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
		private Player player = null;

		public Enemies(Texture texture, Map map, Player player)
		{
			time = 0;
			this.texture = texture;
			this.map = map;
			this.player = player;
			random = new Random();
			ListEnemies = new List<Enemy>();
		}

		const float interval = 0.25f;

		private float time;

		public void UpdateEnemies(GameTime gt)
		{
			time += (float)gt.ElapsedGameTime.TotalSeconds;

			while (time > interval)
			{
				int X = random.Next(0, TimGame.WINDOW_WIDTH-texture.Image.Width);
				Enemy enemy = new Enemy(texture, null, new Vector2(X, -30));
				Rectangle r1 = new Rectangle(enemy.Position.ToPoint(), enemy.Size);
				Rectangle r2 = new Rectangle(player.Position.ToPoint(), player.Size);
				enemy.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
				ListEnemies.Add(enemy);
				time -= interval;
			}

			// Delete enemies that are out of bounds
			ListEnemies.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			ListEnemies.FindAll(map.nearTheGround).ForEach((e => e.destructionMode(gt)));
			           
			// Delete enemies which are dead
			ListEnemies.RemoveAll(e => e.Dead);
		}
	}
}
