using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	/// <summary>
	/// Manager the game interface: for example, switch between a game instance and a menu when needed. 
	/// </summary>
	public class GameManager
	// To link menus and game instances
	{
		public TimGame Application { get; }

		public GameInstance game;
		public MapEditorInstance editor;

		private MenuManager Menu;

		public bool GameRunning { get { return game != null;} }
		public bool MenuRunning { get { return Menu.MenuRunning;} }
		public bool EditorRunning { get { return editor != null && editor.focus; } }

		public GameManager(ContentManager Content, TimGame Application)
		{
			Load.LoadContent(Content);
			this.Application = Application;
			Menu = new MenuManager(this);
		}

		public void Update(GameTime gameTime)
		{
			if (GameRunning && game.player.IsDead())
			{
				game.UndoPoisons();
				game.focus = false;
				Menu.LaunchGameOver();
			}

			if (Menu.MenuRunning)
				Menu.Update();

			else if (EditorRunning)
			{
				if (Controller.KeyPressed(Keys.Escape))
				{
					editor.focus = false;
					//Menu.LaunchPause();
					Menu.LaunchSaveMap();
				}
				editor.Update(gameTime);
			}

			else if (GameRunning && game.focus)
			{
				if (Controller.KeyPressed(Keys.Space) || Controller.KeyPressed(Keys.P) || Controller.KeyPressed(Keys.Escape))
				{
					game.focus = false;
					Menu.LaunchPause();
				}
				game.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Menu 
			spriteBatch.Draw(Load.BackgroundSun, new Rectangle(0, 0, TimGame.GAME_WIDTH, TimGame.GAME_HEIGHT), Color.White);

			if (GameRunning)
				game.Draw(spriteBatch);

			if (Menu.MenuRunning)
			{
				if (!GameRunning)
					Menu.chooseMap.Draw(spriteBatch);
				Menu.Draw(spriteBatch);
			}

			if (EditorRunning)
				editor.Draw(spriteBatch);
		}
	}
}
