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

		public GameInstance gi;
		public MapEditorInstance editor;

		private MenuManager Menu;

		public bool GameRunning { get { return gi != null;} }
		public bool MenuRunning { get { return Menu.MenuRunning;} }
		public bool EditorRunning { get { return editor != null && editor.focus; } }

		public GameManager(ContentManager Content, TimGame Application)
		{
			Load.LoadContent(Content);
			this.Application = Application;
			Menu = new MenuManager(this);
		}

		public virtual void Update(GameTime gameTime)
		{
			if (GameRunning)
			{
				bool allDead = true;
				foreach (Player p in gi.players)
				{
					if (!p.IsDead())
						allDead = false;
				}
				if (allDead)
				{
					gi.focus = false;
					Menu.LaunchGameOver();
				}
			}

			if (Menu.MenuRunning)
				Menu.Update();
			else if (EditorRunning)
			{
				if (Controller.KeyPressed(Keys.Escape))
				{
					editor.focus = false;
					Menu.LaunchSaveMap();
				}
				editor.Update(gameTime);
			}
			else if (GameRunning && gi.focus)
			{
				if (Controller.KeyPressed(Keys.Space) || Controller.KeyPressed(Keys.P) || Controller.KeyPressed(Keys.Escape))
				{
					gi.focus = false;
					Menu.LaunchPause();
				}
				gi.Update(gameTime);
			}
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			// Menu 
			spriteBatch.Draw(Load.BackgroundSun, new Rectangle(0, 0, TimGame.GAME_WIDTH, TimGame.GAME_HEIGHT), Color.White);

			if (GameRunning)
				gi.Draw(spriteBatch);

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
