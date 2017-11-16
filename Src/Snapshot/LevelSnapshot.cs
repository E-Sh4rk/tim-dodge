using System;
using System.Xml.Serialization;

namespace tim_dodge
{
	public class LevelSnapshot
	{
		// Map Dynamic Objects

		// Infos about Map/Level
		public int level_number;
		public float level_time;
		public int score;

		public virtual void RestoreLevelState(GameInstance game)
		{
			// Level infos
			game.Level.SetLevel(level_number);
			game.Level.Current.SetTime(level_time);
			game.player.Score.set(score);
		}
		public virtual void CaptureLevelState(GameInstance game)
		{
			// Level infos
			level_number = game.Level.CurrentLevelNumber();
			level_time = game.Level.Current.GetTime();
			score = game.player.Score.value;
		}
	}
}
