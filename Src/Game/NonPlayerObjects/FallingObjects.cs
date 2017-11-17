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

		public List<NonPlayerObject> EnemiesList
		{
			get;
			protected set;
		}

		public FallingObjects(GameInstance game)
		{
			time = 0;
			this.game = game;

			random = new Random();
			EnemiesList = new List<NonPlayerObject>();
		}

		private float time;

		public void Update(float elapsed)
		{
			if (!game.Level.Current.StopFalling)
			{
				time += elapsed;
				while (time > game.Level.Current.interval)
				{
					if (game.Level.Current.BombActiv && random.Next(0, 5) == 0)
					{
						NonPlayerObject bomb = new Bomb(new Vector2(0, -30));

						int X = random.Next(0, TimGame.GAME_WIDTH - bomb.Size.X);
						bomb.Position = new Vector2(X, bomb.Position.Y);
						Player playerAimed = game.players[random.Next(0, game.players.Count)];

						Rectangle r1 = new Rectangle(bomb.Position.ToPoint(), bomb.Size);
						Rectangle r2 = new Rectangle(playerAimed.Position.ToPoint(), playerAimed.Size);
						bomb.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
						EnemiesList.Add(bomb);
					}
					else
					{
						NonPlayerObject enemy = null;
						if (game.Level.Current.FireballActiv && random.Next(0, 4) != 0)
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
						else if (game.Level.Current.FireballActiv)
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
					time -= game.Level.Current.interval;
				}
			}

			// Delete enemies that are out of bounds
			EnemiesList.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			if (EnemiesList.Count != 0)
				EnemiesList.FindAll(game.Level.Current.map.pMap.nearTheGround).ForEach((e => e.SufferDamage()));

			// Delete enemies that are dead
			int i = 0;
			while (i < EnemiesList.Count)
			{
				NonPlayerObject e = EnemiesList[i];
				if (e.Dead)
				{
					EnemiesList.Remove(e);
					game.AddToScores(10);
				}
				else
					i++;
			}
		}
	}
}
