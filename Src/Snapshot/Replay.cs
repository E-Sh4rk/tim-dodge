using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using System.Linq;
using System.IO;

namespace tim_dodge
{
	public sealed class ReferenceEqualityComparer : IEqualityComparer, IEqualityComparer<object>
	{
		public static ReferenceEqualityComparer Default { get; } = new ReferenceEqualityComparer();

		public new bool Equals(object x, object y) => ReferenceEquals(x, y);
		public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
	}
	[Serializable]
	public class Replay
	{
		[Serializable]
		public class SSnapshot
		{
			// Objects (immutable snapshots)
			public List<ObjectSnapshot> objects_states;
			public List<int> objects_ids;

			// Level&map
			public LevelSnapshot lvl;
		}

		[Serializable]
		public class GameObjectBuilder
		{
			//public Type type; // Not serializable...
			public string type_str;

			public GameObject BuildObject(GameInstance g, int nb_players)
			{
				GameObject o = null;
				Vector2 p = new Vector2(0,0);

				Type type = Type.GetType(type_str);
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
				if (type == typeof(Player))
					o = new Player(p, g.GetNewScorePosition(nb_players), g.GetNewFuelPosition(nb_players), g);

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
				//builders[i].type = objs[i].GetType();
				builders[i].type_str = objs[i].GetType().AssemblyQualifiedName;
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

			Replay rep = new Replay();
			rep.objects = builders;
			rep.snapshots = sss;

			return rep;
		}
		public List<Snapshot> ToSnapshotList(GameInstance g)
		{
			// Instanciation of objects
			GameObject[] instances = new GameObject[objects.Length];
			int nb_players = 0;
			for (int i = 0; i < objects.Length; i++)
			{
				instances[i] = objects[i].BuildObject(g, nb_players);
				if (instances[i] == null)
					System.Diagnostics.Debug.Assert(false);
				if (instances[i] is Player)
					nb_players++;
			}

			// Conversion of the list
			List<Snapshot> res = new List<Snapshot>();
			foreach (SSnapshot ss in snapshots)
			{
				Snapshot s = new Snapshot();
				s.lvl = ss.lvl;
				s.objects_states = ss.objects_states;
				s.objects = new List<GameObject>();
				foreach (int id in ss.objects_ids)
					s.objects.Add(instances[id]);
				res.Add(s);
			}

			return res;
		}

		// Export and import functions
		public void ExportToFile(string file)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(file));
			//Serializer<Replay>.Save(file, this);
			BinarySerializer.Save(file, this);
		}
		public static Replay ImportFromFile(string file)
		{
			//return Serializer<Replay>.Load(file);
			return (Replay)BinarySerializer.Load(file);
		}
	}
}
