using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class LevelManager
	{
		public Level Current { get { return Levels[LevelNumber.value - 1]; } }
		private List<Level> Levels;
		private Stat LevelNumber;
		private Item Title;

		private Level Level1;
		private Level Level2;
		private Level Level3;
		private Level Level4;
		private Level Level5;

		public LevelManager(GameInstance game)
		{
			LevelNumber = new Stat(Load.FontScore, Color.Red, "Level : ", 1);
			// level below score
			LevelNumber.Position = new Vector2(game.scoreTim.source.X, game.scoreTim.source.Bottom);

			Title = new Item("Level 1", Load.FontTitleLevel, Color.Blue);
			Title.Position = new Vector2((TimGame.WINDOW_WIDTH - Title.Size.X) / 2,
										 (TimGame.WINDOW_HEIGHT - Title.Size.Y) / 2);

			Map map = new Map();

			Level1 = new Level(game, map, Load.BackgroundWinter, 100, 0.4f);
			Level1.FireballActiv = true;

			Level2 = new Level(game, map, Load.BackgroundWinter, 100, 0.2f);
			Level2.FireballActiv = true;

			Level3 = new Level(game, map, Load.BackgroundWinter, 600, 0.1f);
			Level3.FireballActiv = true;

			Level4 = new Level(game, map, Load.BackgroundWinter, 800, 0.2f);
			Level4.FireballActiv = true;
			Level4.BombActiv = true;

			Level5 = new Level(game, map, Load.BackgroundWinter, 10000, 0.1f);
			Level5.FireballActiv = true;
			Level5.BombActiv = true;

			Levels = new List<Level> { Level1, Level2, Level3, Level4, Level5 };
			Current.BeginLevel();
		}

		public void LevelUp()
		{
			if (LevelNumber.value < Levels.Count)
			{
				LevelNumber.incr(1);
				Title.Text = "Level " + LevelNumber.value.ToString();
				Current.BeginLevel();
			}
		}

		public void Update(GameTime gameTime)
		{
			Current.Update(gameTime);
			if (Current.EndOfLevel)
				LevelUp();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Current.Draw(spriteBatch);
			LevelNumber.Draw(spriteBatch);
			if (Current.Beginning)
				Title.Draw(spriteBatch);
		}
	}
}
