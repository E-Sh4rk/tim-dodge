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
		private Map map;

		public List<Monstar> EnemiesList
		{
			get;
			protected set;
		}

		public WalkingObjects(GameInstance game, Level Level)
		{
			this.game = game;
			map = Level.map;
			EnemiesList = new List<Monstar>();
			random = new Random();
			interval = 5;
		}

		private float time;
		private float interval;

		public void Update(float elapsed)
		{
			time += elapsed;

			while (time > interval)
			{
				Sprite s = new Sprite("Content.character.MonstarXml.xml");
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
				Monstar m = new Monstar(Load.MonstarTexture, s, vec, map.pMap, dir);
				EnemiesList.Add(m);
				time -= interval;
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
