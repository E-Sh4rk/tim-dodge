using System;
using System.Collections.Generic;

namespace tim_dodge
{
	public class Snapshot
	{
		// Objects (immutable snapshots)
		public List<ObjectSnapshot> objects_states;
		public List<GameObject> objects;

		// Level&map
		public LevelSnapshot lvl;

		public virtual void RestoreGameState(GameInstance game)
		{
			// Level infos
			lvl.RestoreLevelState(game);

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
			lvl = new LevelSnapshot();
			lvl.CaptureLevelState(game);

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
