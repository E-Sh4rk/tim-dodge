using System;
namespace tim_dodge
{
	public class Replay : Snapshot
	{
		public Replay(GameInstance game) : base(game)
		{
		}

		// Infos about Map/Level

		// Infos about player name, date, etc

		public void ExportToFile(string file)
		{

		}
		public void ImportFromFile(string file)
		{

		}

		public override void RestoreGameState()
		{
			base.RestoreGameState();
		}
		public override void CaptureGameState()
		{
			base.CaptureGameState();
		}
	}
}
