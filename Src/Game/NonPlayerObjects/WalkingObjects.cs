using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class WalkingObjects
	{

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

			Sprite s = new Sprite("Content.character.MonstarXml.xml");
			Monstar m = new Monstar(Load.MonstarTexture, s, new Vector2(0f, 0f), map.pMap);

			EnemiesList.Add(m);
		}

		//private float time;

		public void Update(GameTime gt)
		{
			// Delete enemies that are out of bounds
			EnemiesList.RemoveAll((e => e.IsOutOfBounds()));

			// Moving
			foreach (Monstar m in EnemiesList)
			{
				m.Move(gt);
			}

			// Delete enemies that are dead
			EnemiesList.RemoveAll((e => e.Dead));

		}
	}
}
