using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Load the right level at the right moment.
	/// </summary>
	public class LevelManager
	{
		public Level Current { get; protected set; }
		private List<LevelDefinition> Levels;
		private Stat LevelNumber;
		private Item Title;
		GameInstance game;

		struct LevelDefinition
		{
			public LevelDefinition(Map m, Texture2D bck, Texture map_text, int time,
								  float interval, Color int_color, bool fireball, bool bomb)
			{
				this.map = m;
				this.background = bck;
				this.map_texture = map_text;
				this.time = time;
				this.interval = interval;
				this.interface_color = int_color;
				this.fireball_activ = fireball;
				this.bomb_activ = bomb;
			}

			public Map map;
			public Texture2D background;
			public Texture map_texture;
			public int time;
			public float interval;
			public Color interface_color;

			public bool fireball_activ;
			public bool bomb_activ;
		}

		public Map map;

		public LevelManager(GameInstance game)
		{
			this.game = game;
			LevelNumber = new Stat(Load.FontScore, Color.Red, "Level : ", 1);
			// level below score
			LevelNumber.Position = new Vector2(game.scoreTim.source.X, game.scoreTim.source.Bottom);

			Title = new Item("Level X", Load.FontTitleLevel, Color.Blue);
			Title.Position = new Vector2((TimGame.GAME_WIDTH - Title.Size.X) / 2,
										 (TimGame.GAME_HEIGHT - Title.Size.Y) / 2);

			map = new Map(Load.BackgroundSun, Load.MapTextureNature);

			LevelDefinition Level1 = new LevelDefinition(map, Load.BackgroundSun, Load.MapTextureNature,
														 10, 0.3f, Color.Black, true, false);
			LevelDefinition Level2 = new LevelDefinition(map, Load.BackgroundGreen, Load.MapTextureDesert,
														 15, 0.2f, Color.Black, true, false);
			LevelDefinition Level3 = new LevelDefinition(map, Load.BackgroundDark, Load.MapTextureGraveyard,
														 20, 0.15f, Color.White, true, false);
			LevelDefinition Level4 = new LevelDefinition(map, Load.BackgroundWinter, Load.MapTextureWinter,
														 25, 0.15f, Color.Black, true, true);
			LevelDefinition Level5 = new LevelDefinition(map, Load.BackgroundSun, Load.MapTextureNature,
														 50, 0.1f, Color.Black, true, true);

			Levels = new List<LevelDefinition> { Level1, Level2, Level3, Level4, Level5 };
			SetLevel(0);
		}

		public void SetLevel(int lvl)
		{
			if (lvl != LevelNumber.value && lvl < Levels.Count && lvl >= 0)
			{
				LevelNumber.set(lvl);
				Title.Text = "Level " + (LevelNumber.value + 1).ToString();

				LevelDefinition def = Levels[lvl];
				Current = new Level(game, def.map, def.background, def.map_texture, def.time, def.interval, def.interface_color);
				Current.FireballActiv = def.fireball_activ;
				Current.BombActiv = def.bomb_activ;
				Current.BeginLevel();
			}
		}

		public int CurrentLevelNumber() { return LevelNumber.value; }

		public void LevelUp()
		{
			SetLevel(LevelNumber.value + 1);
			map.changeTexture(Current.MapTexture);
		}

		public void Update(float elapsed)
		{
			Current.Update(elapsed);
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
