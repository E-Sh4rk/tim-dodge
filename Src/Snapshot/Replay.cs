using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using System.Linq;

namespace tim_dodge
{
	public sealed class ReferenceEqualityComparer : IEqualityComparer, IEqualityComparer<object>
	{
		public static ReferenceEqualityComparer Default { get; } = new ReferenceEqualityComparer();

		public new bool Equals(object x, object y) => ReferenceEquals(x, y);
		public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
	}
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

		// Conversion functions
		public static Replay FromSnapshotList(List<Snapshot> snaps)
		{
			// Retrieve all objects
			HashSet<GameObject> objects = new HashSet<GameObject>(ReferenceEqualityComparer.Default);
			foreach (Snapshot s in snaps)
			{
				foreach (GameObject o in s.objects)
					objects.Add(o);
			}
			GameObject[] objs = objects.ToArray();
			Dictionary<GameObject,int> dictionary = new Dictionary<GameObject, int>(ReferenceEqualityComparer.Default);
			for (int i = 0; i < objs.Length; i++)
				dictionary.Add(objs[i],i);

			// Convert them
			GameObjectBuilder[] builders = new GameObjectBuilder[objs.Length];
			for (int i = 0; i < objs.Length; i++)
			{
				builders[i] = new GameObjectBuilder();
				builders[i].type = objs[i].GetType();
			}

			// Compute ssnapshots
			List<SSnapshot> sss = new List<SSnapshot>();
			foreach (Snapshot s in snaps)
			{
				SSnapshot ss = new SSnapshot();
				ss.lvl = s.lvl;
				ss.objects_states = s.objects_states;
				ss.objects_ids = new List<int>();
				foreach (GameObject o in s.objects)
					ss.objects_ids.Add(dictionary[o]);
				sss.Add(ss);
			}

			return null;
		}
		public List<Snapshot> ToSnapshotList()
		{
			// TODO
			return null;
		}

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
