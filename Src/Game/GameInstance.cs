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

		private Vector2 PositionScoreTim;

		private Heart heart;
		public Level Level { get; }

		public GameInstance()
		{
			Level = new Level(this);

			PositionScoreTim = new Vector2(30, 20);

			Stat scoreTim = new Stat(Load.FontScore, Color.Black, "Score : ", 0);
			scoreTim.Position = PositionScoreTim;

			heart = new Heart(Load.HeartFull, Load.HeartSemi, Load.HeartEmpty);
			
			player = new Player(new Vector2(500, 250), heart, scoreTim, this);
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();

			player.Move(state, gameTime);
			Level.falling.UpdateEnemies(gameTime);

			// All physical objects
			List<PhysicalObject> phys_obj = new List<PhysicalObject>();
			phys_obj.Add(player);
			phys_obj.AddRange(Level.falling.Falling);

			foreach (PhysicalObject po in phys_obj)
				po.UpdateSprite(gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyForces(phys_obj, Level.map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyCollisions(phys_obj, Level.map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.UpdatePosition(phys_obj, Level.map, gameTime);

			player.Score.Update();
			player.Life.Update();

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			player.Draw(spriteBatch);
			foreach (NonPlayerObject en in Level.falling.Falling)
				en.Draw(spriteBatch);
			player.Score.Draw(spriteBatch);
			player.Life.Draw(spriteBatch);

			heart.Draw(spriteBatch);
		}

	}
}
