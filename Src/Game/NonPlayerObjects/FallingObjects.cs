using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class FallingObjects
	{
		private Random random;
		private GameInstance game;

		public List<NonPlayerObjects> Falling
		{
			get;
			protected set;
		}


		public FallingObjects(GameInstance game)

		{
			time = 0;
			this.game = game;

			random = new Random();
			Falling = new List<NonPlayerObjects>();
		}

		const float interval = 0.10f;//0.25f;

		private float time;

		public void UpdateEnemies(GameTime gt)
		{
			time += (float)gt.ElapsedGameTime.TotalSeconds;

			while (time > interval)
			{
				if (random.Next(0, 5) == 0)
				{
					Sprite s = new Sprite("Content.objects.bomb.xml");
					int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
					NonPlayerObjects bomb = new Bomb(Load.BombTexture, s, new Vector2(X, -30));
					Rectangle r1 = new Rectangle(bomb.Position.ToPoint(), bomb.Size);
					Rectangle r2 = new Rectangle(game.player.Position.ToPoint(), game.player.Size);
					bomb.ApplyNewImpulsion(new Vector2(Collision.direction_between(r1, r2, false).X * 0.04f, 0));
					Falling.Add(bomb);
				}
				else
				{
					Sprite s = new Sprite("Content.objects.fireball.xml");
					int X = random.Next(0, TimGame.WINDOW_WIDTH - s.RectOfSprite().Size.X);
					NonPlayerObjects enemy = new Fireball(Load.FireballTexture, s, new Vector2(X, -30));
					Falling.Add(enemy);
				}
				time -= interval;
			}

			// Delete enemies that are out of bounds
			Falling.RemoveAll((e => e.IsOutOfBounds()));

			// Autodestruct ennemies on the ground 
			Falling.FindAll(game.Level.map.nearTheGround).ForEach((e => e.destructionMode(gt)));

			// Delete enemies on the ground
			int i = 0;
			while (i < Falling.Count)
			{
				NonPlayerObjects e = Falling[i];
				if (e.Dead)
				{
					Falling.Remove(e);
					game.player.Score.incr(10);
				}
				else
					i++;
			}
		}
	}
}
