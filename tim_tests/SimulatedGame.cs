using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_tests
{
	public class SimulatedGameInstance : tim_dodge.GameInstance
	{
		public SimulatedGameInstance(bool hard) : base(tim_dodge.ChooseMap.Maps.FlatMap)
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

		public bool HasDied()
		{
			return alreadyDead;
		}
	}

	public class SimulatedGame : Game
	{
		GraphicsDeviceManager gdm = null;
		SimulatedGameInstance Game;
		SpriteBatch spriteBatch;

		public SimulatedGame()
		{
			gdm = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			gdm.PreferredBackBufferWidth = tim_dodge.TimGame.GAME_WIDTH;
			gdm.PreferredBackBufferHeight = tim_dodge.TimGame.GAME_HEIGHT;
		}

		public void initializeHardLevel()
		{
			Game = new SimulatedGameInstance(true);
		}
		public void initializeEasyLevel()
		{
			Game = new SimulatedGameInstance(false);
		}

		public bool isGameTerminated()
		{
			return Game == null;
		}

		public Texture2D textureFromStream(Stream stream)
		{
			return Texture2D.FromStream(gdm.GraphicsDevice, stream);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			tim_dodge.Load.LoadContent(Content);
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
	}
}
