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

		//public Vector2 PositionScoreTim { get; }
		public Stat scoreTim { get; }

		private Heart heart;
		public LevelManager Level { get; }

		public GameInstance()
		{
			Vector2 PositionScoreTim = new Vector2(30, 20);

			scoreTim = new Stat(Load.FontScore, Color.Black, "Score : ", 0);
			scoreTim.Position = PositionScoreTim;

			heart = new Heart(Load.HeartFull, Load.HeartSemi, Load.HeartEmpty);

			Level = new LevelManager(this);
			player = new Player(new Vector2(500, 250), heart, this);
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();

			Level.Update(gameTime);
			player.Move(state, gameTime);
			Level.Current.falling.Update(gameTime);

			// All physical objects
			List<PhysicalObject> phys_obj = new List<PhysicalObject>();
			phys_obj.Add(player);
			phys_obj.AddRange(Level.Current.falling.FallingList);

			foreach (PhysicalObject po in phys_obj)
				po.UpdateSprite(gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyForces(phys_obj, Level.Current.map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyCollisions(phys_obj, Level.Current.map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.UpdatePosition(phys_obj, Level.Current.map, gameTime);

			player.Life.Update();

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Level.Draw(spriteBatch);

			player.Draw(spriteBatch);
			foreach (NonPlayerObject en in Level.Current.falling.FallingList)
				en.Draw(spriteBatch);
			scoreTim.Draw(spriteBatch);
			player.Life.Draw(spriteBatch);

			heart.Draw(spriteBatch);

		}

	}
}
