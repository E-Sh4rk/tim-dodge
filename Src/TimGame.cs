using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
    /*! \mainpage Index Page
     *
     * \section intro_sec Introduction
     *
     * Hello, and welcome to the Team Dodge ! If you are here, it is because you want to take part of the Team, or you 
     * want to evaluate us. This documentation is here to help you in your noble quest.
     *
     * \section composition_sec General composition of the project
     * 
     * The class which leads the project is the class GameManager. It is the class which gathers menus, game instances.
     * The renderer is in an annex file Renderer. The project is composed of different thematic folders. 
     * 
     * \section menu Menu
     * 
     * The menu is managed in the class MenuManager. It is a stack of MenuItem with the name CurrentMenu. The Menu displayed
     * is the menu at the top of the stack.
     *
     * \section physics Physical environment
     *
     * Each object that is part of the physical environment must inherits the PhysicalObject class.
     * It implements methods to compute the new velocity of the object by taking into account the forces and collisions with other physical objects.
     * It also implements a method UpdatePosition that computes the new position of the object given its velocity,
     * and handle the collisions with the map (which is not a physical object, and so is treated separately).
     * 
     * The collisions with the map are computed thanks to the class PhysicalMap. It represents the physical aspect as a list of walls of different types
     * (roofs, grounds, left and right walls). Each object touching a wall is translated to the corresponding side of the wall.
     * All these walls are computed from the graphical map (composed of blocks), once, when the map loads.
     * 
     * \section serial Saving maps and replay
     * 
     * For saving maps and replay, serialization is used. Small transition classes/structures are used in order to have an efficient serialization/deserialization. 
     * For instance, see SaveBlock and SaveMap classes.
     * 
     * For replay, we save snapshots of all objects at each frame for the entire game. This is not really heavy in space.
     * This is also usefull to be able to go back in the past.
     * 
     * 
     */

    /// <summary>
    /// This is just a MonoGame requirement. See GameManager and GameInstance for more interesting stuff.
    /// </summary>
    public class TimGame : Game
	{
		protected GraphicsDeviceManager graphics;
		protected Renderer renderer;
		protected SpriteBatch spriteBatch;

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
