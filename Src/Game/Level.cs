using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Level
	{
		public FallingObjects falling;
		public Map map;
		public Texture2D Background;
		private GameInstance game;

		// bool false by default
		public bool FireballActiv;
		public bool BombActiv;
		public float interval { get; }

		private int XPToEnd { get; } // Score to have to end the level
		private int ScoreBeginning;
		private int XP { get { return game.scoreTim.value - ScoreBeginning; } }

		public bool Beginning { get; private set; }
		private float time;
		private const float timeOfBeg = 2.0f;
		public bool EndOfLevel { get { return XP > XPToEnd && falling.FallingList.Count == 0; } }

		public Level(GameInstance game, Map map, Texture2D Background,int XPToEnd, float interval)
		{
			this.game = game;
			this.map = map;
			this.Background = Background;
			this.interval = interval;
			this.XPToEnd = XPToEnd;

			falling = new FallingObjects(game, this);
			map = new Map();
		}

		public void BeginLevel()
		{
			Beginning = true;
			ScoreBeginning = game.scoreTim.value;
			time = 0.0f;
		}

		private void InitiateLevel()
		{
			Beginning = false;
			falling = new FallingObjects(game, this);
		}

		public void Update(GameTime gt)
		{
			time += (float)gt.ElapsedGameTime.TotalSeconds;

			if (Beginning && time > timeOfBeg)
				InitiateLevel();

			if (!Beginning)
				if (XP > XPToEnd)
					falling.stopFalling = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);
		}
	}
}
