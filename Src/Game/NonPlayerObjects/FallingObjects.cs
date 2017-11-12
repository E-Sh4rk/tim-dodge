using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Manager for all falling objects (typically, enemies).
	/// </summary>
	public class FallingObjects
	{
		private Random random;
		private GameInstance game;
		private Level level;

		public List<NonPlayerObject> EnemiesList
		{
			get;
			protected set;
		}

		public FallingObjects(GameInstance game, Level Level)
		{
			time = 0;
			this.game = game;
			this.level = Level;

			random = new Random();
			EnemiesList = new List<NonPlayerObject>();
		}

		private float time;

		public void Update(float elapsed)
		{
			if (!level.StopFalling)
			{
				time += elapsed;
				while (time > level.interval)
				{
					if (level.BombActiv && random.Next(0, 5) == 0)
					{
						NonPlayerObject bomb = new Bomb(new Vector2(0, -30));

						int X = random.Next(0, TimGame.GAME_WIDTH - bomb.Size.X);
						bomb.Position = new Vector2(X, bomb.Position.Y);

						Rectangle r1 = new Rectangle(bomb.Position.ToPoint(), bomb.Size);
						Rectangle r2 = new Rectangle(game.player.Position.ToPoint(), game.player.Size);
						bomb.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
						EnemiesList.Add(bomb);
					}
					else
					{
						NonPlayerObject enemy = null;
						if (level.FireballActiv && random.Next(0, 4) != 0)
						{
							// a chance to have a poison
							if (random.Next(0, 2) == 0)
							{
								int randF = random.Next(0, 3);
								if (randF == 0)
									enemy = new FirePoison(new Vector2(0, -30));
								else if (randF == 1)
									enemy = new FireYellow(new Vector2(0, -30));
								else
									enemy = new FireGreen(new Vector2(0, -30));
							}

							else // a regular fireball
								enemy = new Fireball(new Vector2(0, -30));
						}
						else if (level.FireballActiv)
						{
							// a chance to have a cake
							if (random.Next(0, 4) == 0)
								enemy = new Food(new Vector2(0, -30));
							else
								enemy = new Coin(new Vector2(0, -30));
						}
						int X = random.Next(0, TimGame.GAME_WIDTH - enemy.Size.X);
						enemy.Position = new Vector2(X, enemy.Position.Y);
						EnemiesList.Add(enemy);
					}
					time -= level.interval;
				}
			}

			// Delete enemies that are out of bounds
			EnemiesList.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			if (EnemiesList.Count != 0)
				EnemiesList.FindAll(level.map.pMap.nearTheGround).ForEach((e => e.SufferDamage()));

			// Delete enemies that are dead
			int i = 0;
			while (i < EnemiesList.Count)
			{
				NonPlayerObject e = EnemiesList[i];
				if (e.Dead)
				{
					EnemiesList.Remove(e);
					game.scoreTim.incr(10);
				}
				else
					i++;
			}
		}
	}
}
