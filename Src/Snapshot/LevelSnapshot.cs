using System;
using System.Xml.Serialization;

namespace tim_dodge
{
    /// <summary>
    /// Serializable structure that can capture and restore all information relative to the current level of a game (level number, progression...).
    /// </summary>
	[Serializable]
	public class LevelSnapshot
	{
		[Serializable]
		public class PlatformSnapshot
		{
			[Serializable]
			public class PlatformObjectSnapshot
			{
				public float x;
				public float y;
			}
			public PlatformObjectSnapshot[] objs;
			public float x_velocity = 0;
			public float y_velocity = 0;
		}
		// Map Dynamic Objects
		public PlatformSnapshot[] platforms;

		// Infos about Map/Level
		public int level_number;
		public float level_time;

		public virtual void RestoreLevelState(GameInstance game)
		{
			// Map dynamic objects
			if (game.Level.map.platforms.Count == platforms.Length)
			{
				int i = 0;
				foreach (MapPlatform mp in game.Level.map.platforms)
				{
					mp.x_velocity = platforms[i].x_velocity;
					mp.y_velocity = platforms[i].y_velocity;
					if (mp.objs.Length == platforms[i].objs.Length)
					{
						for (int j = 0; j < mp.objs.Length; j++)
						{
							mp.objs[j].x = platforms[i].objs[j].x;
							mp.objs[j].y = platforms[i].objs[j].y;
						}
					}
					i++;
				}
			}
			// Level infos
			game.Level.SetLevel(level_number);
			game.Level.Current.SetTime(level_time);
		}
		public virtual void CaptureLevelState(GameInstance game)
		{
			// Map dynamic objects
			platforms = new PlatformSnapshot[game.Level.map.platforms.Count];
			int i = 0;
			foreach (MapPlatform mp in game.Level.map.platforms)
			{
				PlatformSnapshot ps = new PlatformSnapshot();
				ps.x_velocity = mp.x_velocity;
				ps.y_velocity = mp.y_velocity;
				ps.objs = new PlatformSnapshot.PlatformObjectSnapshot[mp.objs.Length];
				for (int j = 0; j < ps.objs.Length; j++)
				{
					ps.objs[j] = new PlatformSnapshot.PlatformObjectSnapshot();
					ps.objs[j].x = mp.objs[j].x;
					ps.objs[j].y = mp.objs[j].y;
				}
				platforms[i] = ps;
				i++;
			}
			// Level infos
			level_number = game.Level.CurrentLevelNumber();
			level_time = game.Level.Current.GetTime();
		}
	}
}
