using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tim_dodge;

namespace tim_tests
{
	public class SimulatedGameManager : GameManager
	{
		SimulatedGameInstance sgi;

		public SimulatedGameManager(ContentManager Content, TimGame Application, bool hard):
		base(Content, Application)
		{
			sgi = new SimulatedGameInstance(hard, this);
		}

		public override void Update(GameTime gameTime)
		{
			sgi.Update(gameTime);
		}

		public bool HasDied()
		{
			return sgi.HasDied();
		}

		public override void Draw(SpriteBatch spriteBatch) { }
	}
	public class SimulatedGameInstance : tim_dodge.GameInstance
	{
		public SimulatedGameInstance(bool hard, GameManager gm) :
		base(tim_dodge.ChooseMap.Maps.FlatMap, 1, gm)
		{
			if (hard)
			{
				// Test level 5 with 0.5 hearts
				foreach(tim_dodge.Player p in players)
					p.Life.decr(p.Life.value - 1);
				Level.LevelUp();
				Level.LevelUp();
				Level.LevelUp();
				Level.LevelUp();
			}
			else
			{
				// Test level 1 with all hearts
			}
		}

		private bool alreadyDead = false;

		public new void Update(GameTime gameTime)
		{
			if (!alreadyDead)
			{
				bool isDead = true;
				foreach (tim_dodge.Player p in players)
				{
					if (!p.IsDead())
						isDead = false;
				}
				if (isDead)
					alreadyDead = true;
				base.Update(gameTime);
			}
		}

		public new void Draw(SpriteBatch spriteBatch) { }

		public bool HasDied()
		{
			return alreadyDead;
		}
	}

	public class SimulatedGame : TimGame
	{
		SimulatedGameManager Game;

		public SimulatedGame()
		{
		}

		public void initializeHardLevel()
		{
			Game = new SimulatedGameManager(Content, this, true);
		}
		public void initializeEasyLevel()
		{
			Game = new SimulatedGameManager(Content, this, false);
		}

		public bool isGameTerminated()
		{
			return Game == null;
		}

		public Texture2D textureFromStream(Stream stream)
		{
			return Texture2D.FromStream(graphics.GraphicsDevice, stream);
		}

		protected override void Update(GameTime gameTime)
		{
			if (Game != null)
			{
				// Continue game until a death occurs
				if (Game.HasDied())
					Game = null;
				else
					Game.Update(gameTime);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) { }
	}
}
