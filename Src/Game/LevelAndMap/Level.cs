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
		private Color scoreColor;

		public bool FireballActiv;
		public bool BombActiv;
		public float interval { get; }
		private float TimeToEnd { get; }

		private const float timeOfBeg = 0.8f;

		private float Time;

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
			walking = new WalkingObjects(game);
		}

		public void SetTime(float time) { this.Time = time }

		public bool Beginning { get { return Time > timeOfBeg; } }
		public bool EndOfLevel { get { return Time > TimeToEnd && falling.EnemiesList.Count == 0; } }

		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);
		}

		public void BeginLevel()
		{
			game.UndoPoisons();
			game.scoreTim.Color = scoreColor;
			Time = 0.0f;
			map.gMap.changeTexture(MapTexture);
			map.gMap.changeBackground(Background);
		}

		public void Update(float elapsed)
		{
			Time += elapsed;
		}
	}
}
