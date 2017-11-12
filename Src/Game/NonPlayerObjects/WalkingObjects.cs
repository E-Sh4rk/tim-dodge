using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Manager for all walking objects (typically, enemies).
	/// </summary>
	public class WalkingObjects
	{
		private Random random;
		private GameInstance game;

		private const float interval = 5;

		public List<Monstar> EnemiesList
		{
			get;
			protected set;
		}

		public WalkingObjects(GameInstance game)
		{
			this.game = game;
			EnemiesList = new List<Monstar>();
			random = new Random();
		}

		private float time;

		public void Update(float elapsed)
		{
			if (!game.Level.Current.StopFalling)
			{
				time += elapsed;
				while (time > interval)
				{
					Controller.Direction dir;
					Vector2 vec = new Vector2(0f, 0f);
					if (random.Next(0, 2) == 0) // p = 1/2 to be on the right or on the left
					{
						dir = Controller.Direction.LEFT;
						vec.X = TimGame.GAME_WIDTH;
					}
					else
					{
						dir = Controller.Direction.RIGHT;
						vec.X = 0f;
					}
					Monstar m = new Monstar(vec, game, dir);
					EnemiesList.Add(m);
					time -= interval;
				}
			}

			// Delete enemies that are out of bounds
			EnemiesList.RemoveAll((e => e.IsOutOfBounds()));

			// Moving
			foreach (Monstar m in EnemiesList)
			{
				m.Move(elapsed);
			}

			// Delete enemies that are dead
			EnemiesList.RemoveAll((e => e.Dead));
		}
	}
}
