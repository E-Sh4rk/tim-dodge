using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	public class GameInstance
	{
		public Player player;
		private Enemies enemies;
		public Map map { get; }

		public Stat score;
		private Vector2 PositionScore;

		public Stat life;
		private Vector2 PositionLife;
		private int InitialLife;

		private SpriteFont fontDisplay;
		private ContentManager Content;

		public Sound sounds
		{
			get;
			private set;
		}

		public GameInstance(ContentManager Content)
		{
			map = new Map();
			this.Content = Content;

			sounds = new Sound(new SoundEffect[] { Content.Load<SoundEffect>("sound/jump"),
				Content.Load<SoundEffect>("sound/explosion")},
							   new SoundEffect[] { Content.Load<SoundEffect>("sound/cuphead")});
			
			player = new Player(new Texture(Content.Load<Texture2D>("character/Tim")), new Sprite("Content.character.TimXml.xml"),
			                    new Vector2(500, 250), this);

			sounds.playMusic(Sound.MusicName.cuphead);
			
			enemies = new Enemies(new Texture(Content.Load<Texture2D>("objects/bomb")), this);

			fontDisplay = Content.Load<SpriteFont>("SpriteFonts/Score");

			InitialLife = 100;

			PositionScore = new Vector2(30, 20);
			PositionLife = new Vector2(30, 20 + fontDisplay.MeasureString("S").Y);

			score = new Stat(fontDisplay, "Score : ", PositionScore, Color.Black, 0);
			life = new Stat(fontDisplay, "Life : ", PositionLife, Color.Red, InitialLife);
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
			score.Draw(spriteBatch);
			life.Draw(spriteBatch);
		}
	}
}
