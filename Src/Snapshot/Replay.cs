using System;
using System.Collections.Generic;

namespace tim_dodge
{
	public class Replay
	{
		public class SSnapshot
		{
			// Objects (immutable snapshots)
			public List<ObjectSnapshot> objects_states { get; protected set; }
			public List<int> objects_ids { get; protected set; }

			// Level&map
			public LevelSnapshot lvl { get; protected set; }
		}

		public class GameObjectBuilder
		{
			public Type type { get; protected set; }

			public GameObject BuildObject(GameInstance g)
			{
				GameObject o = null;


				return o;
			}
		}

		public List<SSnapshot> snapshots;
		public GameObjectBuilder[] objects;
	}
}
