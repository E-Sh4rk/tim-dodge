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

		Texture bomb_texture;
		public Enemies(GameInstance game)
		{
			time = 0;
			this.game = game;
			bomb_texture = game.LoadTexture("objects/bomb");

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
				Sprite s = new Sprite("Content.objects.bomb.xml");
				int X = random.Next(0, TimGame.WINDOW_WIDTH-s.RectOfSprite().Size.X);
				Enemy enemy = new Bomb(bomb_texture, s, new Vector2(X, -30), game);
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
					game.player.Score.incr(10);
				}
			}

		}
	}
}
