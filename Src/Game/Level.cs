using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Level
	{
		public FallingObjects falling;
		public WalkingObjects walking;
		public Map map;
		public Texture2D Background;
		public Texture MapTexture;
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

		public Level(GameInstance game, Map map, Texture2D Background, Texture MapTexture, int XPToEnd, float interval)
		{
			this.game = game;
			this.map = map;
			this.Background = Background;
			this.MapTexture = MapTexture;
			this.interval = interval;
			this.XPToEnd = XPToEnd;

			falling = new FallingObjects(game, this);
			walking = new WalkingObjects(game, this);

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);
		}

		public void BeginLevel()
		{
			Beginning = true;
			ScoreBeginning = game.scoreTim.value;
			time = 0.0f;
			map.gMap.changeTexture(MapTexture);
			map.gMap.changeBackground(Background);
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

	}
}
