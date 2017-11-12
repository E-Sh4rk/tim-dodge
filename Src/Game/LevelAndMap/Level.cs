using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents a level, characterized by a map, some parameters to generate enemies, some success conditions...
	/// </summary>
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

		private float TimeToEnd { get; }
		private int ScoreBeginning;

		public bool Beginning { get; private set; }
		private float Time;
		private const float timeOfBeg = 0.8f;
		public bool EndOfLevel { get { return Time > TimeToEnd && falling.EnemiesList.Count == 0; } }

		public Color scoreColor;

		public Level(GameInstance game, Map map, Texture2D Background, Texture MapTexture, int timeToEnd, float interval, Color scoreColor)
		{
			this.game = game;
			this.map = map;
			this.Background = Background;
			this.MapTexture = MapTexture;
			this.interval = interval;
			this.TimeToEnd = timeToEnd;
			this.scoreColor = scoreColor;

			falling = new FallingObjects(game, this);
			walking = new WalkingObjects(game, this);

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);
		}

		public void BeginLevel()
		{
			game.UndoPoisons();
			Beginning = true;
			game.scoreTim.Color = scoreColor;
			ScoreBeginning = game.scoreTim.value;
			Time = 0.0f;
			map.gMap.changeTexture(MapTexture);
			map.gMap.changeBackground(Background);
		}

		private void InitiateLevel()
		{
			Beginning = false;
			falling = new FallingObjects(game, this);
		}

		public void Update(float elapsed)
		{
			Time += elapsed;

			if (Beginning && Time > timeOfBeg)
				InitiateLevel();

			if (!Beginning)
				if (Time > TimeToEnd)
					falling.stopFalling = true;
		}

	}
}
