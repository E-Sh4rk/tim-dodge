using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Replay
	{
		public class SSnapshot
		{
			// Objects (immutable snapshots)
			public List<ObjectSnapshot> objects_states;
			public List<int> objects_ids;

			// Level&map
			public LevelSnapshot lvl;
		}

		public class GameObjectBuilder
		{
			public Type type;

			public GameObject BuildObject(GameInstance g)
			{
				GameObject o = null;
				Vector2 p = new Vector2(0,0);

				if (type == typeof(Bomb))
					o = new Bomb(p);
				if (type == typeof(Coin))
					o = new Coin(p);
				if (type == typeof(Fireball))
					o = new Fireball(p);
				if (type == typeof(FireGreen))
					o = new FireGreen(p);
				if (type == typeof(FirePoison))
					o = new FirePoison(p);
				if (type == typeof(FireYellow))
					o = new FireYellow(p);
				if (type == typeof(Food))
					o = new Food(p);
				if (type == typeof(Monstar))
					o = new Monstar(p, g, Controller.Direction.RIGHT);
				//if (type == typeof(Player))

				return o;
			}
		}

		public List<SSnapshot> snapshots;
		public GameObjectBuilder[] objects;

		// Export and import functions
		public void ExportToFile(string file)
		{
			Serializer<Replay>.Save(file, this);
		}
		public static Replay ImportFromFile(string file)
		{
			return Serializer<Replay>.Load(file);
		}
	}
}
