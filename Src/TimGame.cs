using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class TimGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public const int WINDOW_WIDTH = 1280;
		public const int WINDOW_HEIGHT = 720;
		World world = null;
		Player player = null;
		Map map = null;
		List<Enemy> enemies = new List<Enemy>();

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
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			map = new Map();

			world = new World(new Texture(Content.Load<Texture2D>("background/winter")), null, new Vector2(0.0f, 0.0f));
			//world.colorTab = new Color[world.Texture.Width * world.Texture.Height];
			//world.Texture.GetData<Color>(world.colorTab);

			player = new Player(new Texture(Content.Load<Texture2D>("character/Tim")), new Sprite("Content.character.TimXml.xml"),
			                    map, new Vector2(500, 250));

			enemies.Add(new Enemy(new Texture(Content.Load<Texture2D>("objects/bomb")), null, new Vector2(500, 100)));
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

			player.Move(Keyboard.GetState(), gameTime);

			// All physical objects
			List<PhysicalObject> phys_obj = new List<PhysicalObject>();
			phys_obj.Add(player);
			phys_obj.AddRange(enemies);

			foreach (PhysicalObject po in phys_obj)
			{
				if (po.Sprite != null)
					po.Sprite.UpdateFrame(gameTime);
				po.UpdatePosition(phys_obj, map, gameTime);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			world.Draw(spriteBatch);
			player.Draw(spriteBatch);
			foreach (Enemy en in enemies)
				en.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
