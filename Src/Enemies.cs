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

		const float interval = 0.25f;

		private float time;

		public void UpdateEnemies(GameTime gt)
		{
			time += (float)gt.ElapsedGameTime.TotalSeconds;

			while (time > interval)
			{
				int X = random.Next(0, TimGame.WINDOW_WIDTH-texture.Image.Width);
				Enemy enemy = new Bomb(texture, null, new Vector2(X, -30), game);
				Rectangle r1 = new Rectangle(enemy.Position.ToPoint(), enemy.Size);
				Rectangle r2 = new Rectangle(game.player.Position.ToPoint(), game.player.Size);
				enemy.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
				ListEnemies.Add(enemy);
				time -= interval;
			}

			// Delete enemies that are out of bounds
			ListEnemies.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			ListEnemies.FindAll(game.map.nearTheGround).ForEach((e => e.destructionMode(gt)));

			// Delete enemies on the ground
			for (int i = 0; i < ListEnemies.Count; i++)
			{
				Enemy e = ListEnemies[i];
				if (e.Dead)
				{
					ListEnemies.Remove(e);
					game.score.incr(10);
				}
			}

		}
	}
}
