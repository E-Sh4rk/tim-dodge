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
		public ObjectSnapshot objects { get; protected set; }

		// Map Dynamic Objects

		// Score? Other special data for the player?

		public virtual void RestoreGameState()
		{

		}
		public virtual void CaptureGameState()
		{

		}

	}
}
