using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_tests
{
	public class SimulatedGameInstance : tim_dodge.GameInstance
	{
		public SimulatedGameInstance(bool hard) : base()
		{
			if (hard)
			{
				// Test level 5 with 0.5 hearts
				player.Life.decr(7);
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
			if (player.IsDead())
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
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			//	Exit();
#endif

			gdm.PreferredBackBufferWidth = tim_dodge.TimGame.WINDOW_WIDTH;
			gdm.PreferredBackBufferHeight = tim_dodge.TimGame.WINDOW_HEIGHT;

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

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			gdm.GraphicsDevice.Clear(Color.CornflowerBlue);

			// Simulation: no need to draw
			/*spriteBatch.Begin();
			if (Game != null)
				Game.Draw(spriteBatch);
			spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}
