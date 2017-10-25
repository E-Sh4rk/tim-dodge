using System;
namespace tim_dodge
{
	public class Level
	{
		public FallingObjects falling;
		public Map map;

		public Level(GameInstance game)
		{
			map = new Map();
			falling = new FallingObjects(game);
		}

	}
}
