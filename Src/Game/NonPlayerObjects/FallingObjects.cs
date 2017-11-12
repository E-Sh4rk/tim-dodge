﻿using System;
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
		private Map map;

		private bool FireballActiv;
		private bool BombActiv;
		private float interval;

		public bool stopFalling;

		public List<NonPlayerObject> FallingList
		{
			get;
			protected set;
		}

		public FallingObjects(GameInstance game, Level Level)
		{
			time = 0;
			this.game = game;
			map = Level.map;

			FireballActiv = Level.FireballActiv;
			BombActiv = Level.BombActiv;
			interval = Level.interval;

			stopFalling = false;

			random = new Random();
			FallingList = new List<NonPlayerObject>();
		}

		private float time;

		public void Update(float elapsed)
		{
			time += elapsed;

			if (!stopFalling)
			{
				while (time > interval)
				{
					if (BombActiv && random.Next(0, 5) == 0)
					{
						Sprite s = new Sprite("Content.objects.bomb.xml");
						int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
						NonPlayerObject bomb = new Bomb(Load.BombTexture, s, new Vector2(X, -30));
						Rectangle r1 = new Rectangle(bomb.Position.ToPoint(), bomb.Size);
						Rectangle r2 = new Rectangle(game.player.Position.ToPoint(), game.player.Size);
						bomb.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
						FallingList.Add(bomb);
					}
					else if (FireballActiv && random.Next(0, 5) != 0)
					{
						// a chance to have a poison
						if (random.Next(0, 6) == 0)
						{
							if (random.Next(0, 2) == 0)
							{
								Sprite s = new Sprite("Content.objects.fireball.xml");
								int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
								NonPlayerObject fireball = new FirePoison(Load.FireballTexture, s, new Vector2(X, -30));
								FallingList.Add(fireball);
							}
							else
							{
								Sprite s = new Sprite("Content.objects.fireball.xml");
								int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
								NonPlayerObject fireball = new FireGreen(Load.FireballTexture, s, new Vector2(X, -30));
								FallingList.Add(fireball);
							}

						}

						else // a regular fireball
						{
							Sprite s = new Sprite("Content.objects.fireball.xml");
							int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
							NonPlayerObject fireball = new Fireball(Load.FireballTexture, s, new Vector2(X, -30));
							FallingList.Add(fireball);
						}
					}
					else if (FireballActiv)
					{
						// a chance to have a cake
						if (random.Next(0, 5) == 0)
						{
							Sprite s = new Sprite("Content.objects.food.xml");
							s.ChangeState(14);//(22);
							int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
							NonPlayerObject food = new Food(Load.FoodTexture, s, new Vector2(X, -30));
							FallingList.Add(food);
						}

						else
						{
							Sprite s = new Sprite("Content.objects.coin.xml");
							int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
							NonPlayerObject coin = new Coin(Load.CoinTexture, s, new Vector2(X, -30));
							FallingList.Add(coin);
						}

					}
					time -= interval;
				}
			}

			// Delete enemies that are out of bounds
			FallingList.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			if (FallingList.Count != 0)
				FallingList.FindAll(map.pMap.nearTheGround).ForEach((e => e.SufferDamage()));

			// Delete enemies that are dead
			int i = 0;
			while (i < FallingList.Count)
			{
				NonPlayerObject e = FallingList[i];
				if (e.Dead)
				{
					FallingList.Remove(e);
					game.scoreTim.incr(10);
				}
				else
					i++;
			}
		}
	}
}
