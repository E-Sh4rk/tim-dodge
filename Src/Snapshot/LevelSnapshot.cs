using System;
namespace tim_dodge
{
	public class LevelSnapshot
	{
		// Map Dynamic Objects

		// Infos about Map/Level
		public int level_number { get; protected set; }
		public float level_time { get; protected set; }

		public virtual void RestoreLevelState(GameInstance game)
		{
			// Level infos
			game.Level.SetLevel(level_number);
			game.Level.Current.SetTime(level_time);
		}
		public virtual void CaptureLevelState(GameInstance game)
		{
			// Level infos
			level_number = game.Level.CurrentLevelNumber();
			level_time = game.Level.Current.GetTime();
		}
	}
}
