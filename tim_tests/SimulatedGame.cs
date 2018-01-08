using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tim_dodge;

namespace tim_tests
{
	public class SimulatedGameManager : GameManager
	{
		SimulatedGameInstance sgi;

		public SimulatedGameManager(ContentManager Content, TimGame Application):
		base(Content, Application)
		{
			sgi = null;	
		}

		public void StartGameInstance(bool hard, int nbPlayers)
		{
			sgi = new SimulatedGameInstance(hard, this, nbPlayers);
		}

		public override void Update(GameTime gameTime)
		{
			if (sgi != null)
			{
				sgi.Update(gameTime);
				if (sgi.HasDied())
					sgi = null;
			}
		}

		public bool HasDied()
		{
			return sgi == null || sgi.HasDied();
		}

		public override void Draw(SpriteBatch spriteBatch) { }
	}
	public class SimulatedGameInstance : tim_dodge.GameInstance
	{
		public SimulatedGameInstance(bool hard, GameManager gm, int nbPlayers) :
		base(tim_dodge.ChooseMap.Maps.FlatMap, nbPlayers, gm)
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
		public SimulatedGameManager Game;

		public SimulatedGame()
		{
			
		}

		public void Init()
		{
			RunOneFrame();
			Game = new SimulatedGameManager(Content, this);
		}

		public void initializeHardLevel()
		{
			Game.StartGameInstance(true, 1);
		}
		public void initializeEasyLevel()
		{
			Game.StartGameInstance(false, 1);
		}

		public void initializeMultiPlayers()
		{
			Game.StartGameInstance(true, 2);
		}

		public bool isGameTerminated()
		{
			return Game.HasDied();
		}

		public Texture2D textureFromStream(Stream stream)
		{
			return Texture2D.FromStream(graphics.GraphicsDevice, stream);
		}

		protected override void Update(GameTime gameTime)
		{
			if (Game != null)
				Game.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) { }
	}
}
