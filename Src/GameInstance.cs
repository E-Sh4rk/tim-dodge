using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class GameInstance
	{
		private Player player = null;
		private Enemies enemies = null;
		private Map map = null;

		private ContentManager Content;

		public GameInstance(ContentManager Content)
		{
			this.Content = Content;
			LoadContent();
		}

		private void LoadContent()
		{
			map = new Map();

			player = new Player(new Texture(Content.Load<Texture2D>("character/Tim")), new Sprite("Content.character.TimXml.xml"),
					map, new Vector2(500, 250));

			enemies = new Enemies(new Texture(Content.Load<Texture2D>("objects/bomb")), map, player);
		}

		public void Update(GameTime gameTime)
		{
			player.Move(Keyboard.GetState(), gameTime);
			enemies.UpdateEnemies(gameTime);

			// All physical objects
			List<PhysicalObject> phys_obj = new List<PhysicalObject>();
			phys_obj.Add(player);
			phys_obj.AddRange(enemies.ListEnemies);

			foreach (PhysicalObject po in phys_obj)
				po.UpdateSprite(gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyForces(phys_obj, map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.ApplyCollisions(phys_obj, map, gameTime);
			foreach (PhysicalObject po in phys_obj)
				po.UpdatePosition(phys_obj, map, gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			player.Draw(spriteBatch);
			foreach (Enemy en in enemies.ListEnemies)
				en.Draw(spriteBatch);
		}
	}
}
