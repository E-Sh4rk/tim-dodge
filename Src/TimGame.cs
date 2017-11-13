using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	/// <summary>
	/// This is just a MonoGame requirement. See GameManager and GameInstance for more interesting stuff.
	/// </summary>
	public class TimGame : Game
	{
		GraphicsDeviceManager graphics;
		Renderer renderer;
		SpriteBatch spriteBatch;

		public const int WINDOW_WIDTH = 1280;
		public const int WINDOW_HEIGHT = 720;
		public const float general_scale = 1.5f;

		public const int GAME_WIDTH = (int)(WINDOW_WIDTH*general_scale);
		public const int GAME_HEIGHT = (int)(WINDOW_HEIGHT*general_scale);

		GameManager Game;

		public TimGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			renderer = new Renderer(graphics);
		}

		public void Quit()
		{
			this.Exit();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			//this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 250.0f);
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Game = new GameManager(Content, this);
		}

		public static KeyboardState previousKeyState;
		public static KeyboardState currentKeyState;

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			//graphics.PreferredBackBufferWidth = GAME_WIDTH;
			//graphics.PreferredBackBufferHeight = GAME_HEIGHT;

			// cf function KeyPressed in the class Controller
			previousKeyState = currentKeyState;
			currentKeyState = Keyboard.GetState();

			Game.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			renderer.Draw(spriteBatch, Game);
			base.Draw(gameTime);
		}
	}
}
