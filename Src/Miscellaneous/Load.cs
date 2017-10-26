using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public static class Load
	{
		// Sound
		public static Sound sounds { get; private set; }

		// Texture2D
		public static Texture2D BackgroundMenu { get; private set; }
		public static Texture2D BackgroundWinter { get; private set; }
		public static Texture2D HeartFull { get; private set; }
		public static Texture2D HeartSemi { get; private set; }
		public static Texture2D HeartEmpty { get; private set; }

		// Texture (sprites)
		public static Texture TimTexture { get; private set; }
		public static Texture BombTexture;
		public static Texture FireballTexture;
		public static Texture MapTexture;

		// SpriteFont
		public static SpriteFont FontMenu { get; private set; }
		public static SpriteFont FontTitleMenu { get; private set; }
		public static SpriteFont FontScore { get; private set; }
		public static SpriteFont FontTitleLevel { get; private set; }

		// Color
		public static Color ColorTextMenu { get; private set; }
		public static Color ColorHighlightSelection { get; private set; }
		public static Color ColorTitleMenu { get; private set; }

		// XML
		public static String PathHighScores { get; private set; }

		public static void LoadContent(ContentManager Content)
		{
			// Sound
			sounds = new Sound(new SoundEffect[] { Content.Load<SoundEffect>("sound/jump"),
				Content.Load<SoundEffect>("sound/explosion"),
				Content.Load<SoundEffect>("sound/damage"),
				Content.Load<SoundEffect>("sound/fire"),
				Content.Load<SoundEffect>("sound/menu"),
				Content.Load<SoundEffect>("sound/toogle"),
				Content.Load<SoundEffect>("sound/applause"),
				Content.Load<SoundEffect>("sound/ah")},
				   new SoundEffect[] { Content.Load<SoundEffect>("sound/cuphead") });

			// Texture2D
			BackgroundMenu = Content.Load<Texture2D>("background/Menu");
			BackgroundWinter = Content.Load<Texture2D>("background/winter");
			HeartFull = Content.Load<Texture2D>("life/full_heart");
			HeartSemi = Content.Load<Texture2D>("life/semi_heart");
			HeartEmpty = Content.Load<Texture2D>("life/empty_heart");

			// Texture (sprites)
			TimTexture = new Texture(Content.Load<Texture2D>("character/Tim"));
			BombTexture = new Texture(Content.Load<Texture2D>("objects/bomb"));
			FireballTexture = new Texture(Content.Load<Texture2D>("objects/fireball"));
			MapTexture = new Texture(Content.Load<Texture2D>("ground/ground"));

			// SpriteFont
			FontMenu = Content.Load<SpriteFont>("SpriteFonts/Menu");
			FontTitleMenu = Content.Load<SpriteFont>("SpriteFonts/TitleMenu");
			FontScore = Content.Load<SpriteFont>("SpriteFonts/Score");
			FontTitleLevel = Content.Load<SpriteFont>("SpriteFonts/Level");

			// Color
			ColorTextMenu = Color.White;
			ColorHighlightSelection = Color.GreenYellow;
			ColorTitleMenu = Color.LightBlue;

			// XML
			PathHighScores = "scores.xml";
		}

		public static List<BestScore> LoadHighScores()
		{
			try
			{
				return Serializer<List<BestScore>>.Load(PathHighScores);
			}
			catch
			{
				return new List<BestScore>();
			}
		}
	}
}
