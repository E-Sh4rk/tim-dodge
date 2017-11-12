using System;
using System.Collections.Generic;

namespace tim_dodge
{
	public class Snapshot
	{
		public Snapshot(GameInstance game)
		{
			game_ptr = game;
		}

		public GameInstance game_ptr { get; }

		// Objects (immutable snapshots)
		public List<NonPlayerObjectSnapshot> walking_objects { get; protected set; }
		public List<NonPlayerObjectSnapshot> falling_objects { get; protected set; }
		public List<PlayerObjectSnapshot> player_objects { get; protected set; }

		// Map Dynamic Objects

		// Infos about Map/Level
		public int level_number { get; protected set; }
		public float level_time { get; protected set; }

		public virtual void RestoreGameState()
		{
			// Level infos
			game_ptr.Level.SetLevel(level_number);
			game_ptr.Level.Current.SetTime(level_time);

			// Objects
			game_ptr.Level.Current.falling.EnemiesList.Clear();
			game_ptr.Level.Current.walking.EnemiesList.Clear();

			foreach (ObjectSnapshot snap in player_objects)
				snap.RestoreModelState();
			foreach (ObjectSnapshot snap in walking_objects)
			{
				snap.RestoreModelState();
				game_ptr.Level.Current.walking.EnemiesList.Add((Monstar)snap.model_ptr);
			}
			foreach (ObjectSnapshot snap in falling_objects)
			{
				snap.RestoreModelState();
				game_ptr.Level.Current.falling.EnemiesList.Add((NonPlayerObject)snap.model_ptr);
			}
		}
		public virtual void CaptureGameState()
		{
			// Level infos
			level_number = game_ptr.Level.CurrentLevelNumber();
			level_time = game_ptr.Level.Current.GetTime();

			// Objects
			walking_objects = new List<NonPlayerObjectSnapshot>();
			falling_objects = new List<NonPlayerObjectSnapshot>();
			player_objects = new List<PlayerObjectSnapshot>();

			PlayerObjectSnapshot pos = new PlayerObjectSnapshot(game_ptr.player);
			pos.CaptureModelState();
			player_objects.Add(pos);
			foreach (NonPlayerObject npo in game_ptr.Level.Current.walking.EnemiesList)
			{
				NonPlayerObjectSnapshot s = new NonPlayerObjectSnapshot(npo);
				s.CaptureModelState();
				walking_objects.Add(s);
			}
			foreach (NonPlayerObject npo in game_ptr.Level.Current.falling.EnemiesList)
			{
				NonPlayerObjectSnapshot s = new NonPlayerObjectSnapshot(npo);
				s.CaptureModelState();
				falling_objects.Add(s);
			}
		}

	}
}
