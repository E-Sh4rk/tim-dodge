using System;
using System.Collections.Generic;

namespace tim_dodge
{
	public class Snapshot
	{
		public Snapshot() { }

		// Objects (immutable snapshots)
		public List<ObjectSnapshot> objects_states { get; protected set; }
		public List<GameObject> objects { get; protected set; }

		// Map Dynamic Objects

		// Infos about Map/Level
		public int level_number { get; protected set; }
		public float level_time { get; protected set; }

		public virtual void RestoreGameState(GameInstance game)
		{
			// Level infos
			game.Level.SetLevel(level_number);
			game.Level.Current.SetTime(level_time);

			// Objects
			game.Level.Current.falling.EnemiesList.Clear();
			game.Level.Current.walking.EnemiesList.Clear();

			for (int i = 0; i < objects.Count; i++)
			{
				GameObject o = objects[i];
				ObjectSnapshot s = objects_states[i];
				s.RestoreModelState(o);
				if (o is Monstar)
					game.Level.Current.walking.EnemiesList.Add((Monstar)o);
				else if (o is NonPlayerObject)
					game.Level.Current.falling.EnemiesList.Add((NonPlayerObject)o);
			}
		}
		public virtual void CaptureGameState(GameInstance game)
		{
			// Level infos
			level_number = game.Level.CurrentLevelNumber();
			level_time = game.Level.Current.GetTime();

			// Objects
			objects_states = new List<ObjectSnapshot>();
			objects = new List<GameObject>();
			objects.Add(game.player);
			objects.AddRange(game.Level.Current.walking.EnemiesList);
			objects.AddRange(game.Level.Current.falling.EnemiesList);
			foreach (GameObject o in objects)
			{
				ObjectSnapshot s = null;
				if (o is Player)
					s = new PlayerObjectSnapshot();
				if (o is NonPlayerObject)
					s = new NonPlayerObjectSnapshot();
				s.CaptureModelState(o);
				objects_states.Add(s);
			}
		}
	}
}
