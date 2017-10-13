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

		private Vector2 PositionScoreTim;
		private Vector2 PositionLifeTime;
		private int InitialLifeTim;

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

			sounds.playMusic(Sound.MusicName.cuphead);

			fontDisplay = Content.Load<SpriteFont>("SpriteFonts/Score");

			PositionScoreTim = new Vector2(30, 20);
			PositionLifeTime = new Vector2(30, 20 + fontDisplay.MeasureString("S").Y);

			InitialLifeTim = 100;

			Stat scoreTim = new Stat(fontDisplay, "Score : ", PositionScoreTim, Color.Black, 0);
			Stat lifeTim = new Stat(fontDisplay, "Life : ", PositionLifeTime, Color.Red, InitialLifeTim);
			player = new Player(new Texture(Content.Load<Texture2D>("character/Tim")), new Sprite("Content.character.TimXml.xml"),
			                    new Vector2(500, 250), lifeTim, scoreTim, this);

			enemies = new Enemies(new Texture(Content.Load<Texture2D>("objects/bomb")), "Content.objects.bomb.xml", this);
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
			player.Score.Draw(spriteBatch);
			player.Life.Draw(spriteBatch);
		}
	}
}
