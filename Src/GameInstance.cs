using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class GameInstance
	{
		public Player player;
		public Enemies enemies;
		public Map map { get; }

		private Vector2 PositionScoreTim;
		private Vector2 PositionLifeTim;

		private SpriteFont fontDisplay;
		private ContentManager Content;

		public GameInstance(ContentManager Content)
		{
			map = new Map();
			this.Content = Content;

			fontDisplay = Content.Load<SpriteFont>("SpriteFonts/Score");

			PositionScoreTim = new Vector2(30, 20);
			PositionLifeTim = new Vector2(30, 20 + fontDisplay.MeasureString("S").Y);

			Stat scoreTim = new Stat(fontDisplay, Color.Black, "Score : ", 0);
			scoreTim.Position = PositionScoreTim;
			Stat lifeTim = new Stat(fontDisplay, Color.Red, "Life : ", 0);
			lifeTim.Position = PositionLifeTim;
			player = new Player(new Vector2(500, 250), lifeTim, scoreTim, this);

			enemies = new Enemies(this);
		}

		public Texture LoadTexture(string path)
		{
			return new Texture(Content.Load<Texture2D>(path));
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();

			player.Move(state, gameTime);
			enemies.UpdateEnemies(gameTime);

			// All physical objects
			List<PhysicalObject> phys_obj = new List<PhysicalObject>();
			phys_obj.Add(player);
			phys_obj.AddRange(enemies.ListEnemies);

			foreach (PhysicalObject po in phys_obj)
				po.UpdateSprite(gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyForces(phys_obj, map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyCollisions(phys_obj, map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.UpdatePosition(phys_obj, map, gameTime);

			player.Score.Update();
			player.Life.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			player.Draw(spriteBatch);
			foreach (Enemy en in enemies.ListEnemies)
				en.Draw(spriteBatch);
			player.Score.Draw(spriteBatch);
			player.Life.Draw(spriteBatch);
		}
	}
}
