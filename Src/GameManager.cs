using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class GameManager
	// To link menus and game instances
	{
		private GameInstance newGame;

		public GameManager(ContentManager Content)
		{
			newGame = new GameInstance(Content);
		}


		public void Update(GameTime gameTime)
		{
			newGame.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			newGame.Draw(spriteBatch);
		}

	}
}
