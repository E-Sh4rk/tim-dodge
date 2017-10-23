using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class MenuManager
	{
		public Texture2D BackgroundMenu { get; }
		public SpriteFont FontMenu { get; }
		public Color ColorTextMenu { get; }
		public Color ColorHighlightSelection { get; }
		public SpriteFont FontTitleMenu { get; }
		public Color ColorTitleMenu { get; }
	
		private List<MenuWindow> CurrentMenu;
		private MenuWindow InitialMenu;
		private MenuWindow PauseMenu;
		private MenuWindow ParamMenu;
		private MenuItem choiceMusicItem;
		private MenuItem choiceSoundItem;
		private MenuWindow Gameover;
		private MenuWindow QuitMenu;
		private MenuWindow Highscores;

		private MenuWindow Congrats;
		private MenuItem EnterYourName;
		private const String messageYourName = "Enter your name";
		private KeyboardReader YourName;
		private const String pathHighScores = "scores.xml";
		private int gameScore;
		private const int SizeHighscores = 5;

		public bool MenuRunning { get { return CurrentMenu.Count != 0; } }

		GameManager GameManager;

		public MenuManager(GameManager GameManager)
		{
			this.GameManager = GameManager;

			// Default parameters for menus
			BackgroundMenu = GameManager.BackgroundMenu;
			FontMenu = GameManager.FontMenu;
			ColorTextMenu = Color.White;
			ColorHighlightSelection = Color.GreenYellow;
			FontTitleMenu = GameManager.FontTitleMenu;
			ColorTitleMenu = Color.LightBlue;

			// Initialization of windows
			InitialMenu = new MenuWindow(this);
			PauseMenu = new MenuWindow(this);
			ParamMenu = new MenuWindow(this);
			Gameover = new MenuWindow(this);
			Congrats = new MenuWindow(this);
			QuitMenu = new MenuWindow(this);
			Highscores = new MenuWindow(this);

			// Constructrion of menus
			Initialize(InitialMenu, "Menu", new List<MenuItem> {
				new MenuItem("New Game", this, NewGame),
				new MenuItem("Parameters", this, Parameters),
				new MenuItem("Best Scores", this, BestScores),
				new MenuItem("Quit", this, Quit) }
			          );

			Initialize(PauseMenu, "Pause", new List<MenuItem> {
				new MenuItem("Resume", this, Resume),
				new MenuItem("New Game", this, NewGame),
				new MenuItem("Parameters", this, Parameters),
				new MenuItem("Best Scores", this, BestScores),
				new MenuItem("Quit", this, Quit) }
					  );

			if (GameManager.sounds.musicmute)
				choiceMusicItem = new MenuItem("Activate Music", this, ChoiceMusic);
			else
				choiceMusicItem = new MenuItem("Deactivate Music", this, ChoiceMusic);
			if (GameManager.sounds.sfxmute)
				choiceSoundItem = new MenuItem("   Activate Sound Effects   ", this, ChoiceSound);
			else
				choiceSoundItem = new MenuItem("Deactivate Sound Effects", this, ChoiceSound);
			Initialize(ParamMenu, "Parameters", new List<MenuItem> {
				choiceMusicItem,
				choiceSoundItem,
				new MenuItem("Back to Menu", this, Previous) }
					  );

			Initialize(QuitMenu, "Quit the game ?", new List<MenuItem> {
				new MenuItem("No, I want to play more!", this, Previous),
				new MenuItem("Yes, leave me alone", this, Quit) }
					  );

			Initialize(Gameover, "Game Over", new List<MenuItem> {
				new MenuItem("Play Again", this, NewGame),
				new MenuItem("Back Menu", this, BackInitialMenu),
				new MenuItem("Quit the game", this, Quit) }
					  );

			Congrats.ColorTitle = Color.Green;
			YourName = new KeyboardReader("You beat an highscore".Length);
			EnterYourName = new MenuItem(messageYourName, FontMenu, ColorTextMenu); // Text updated by the update function
			Initialize(Congrats, "Congrats !", new List<MenuItem> {
				new MenuItem("You beat an highscore", FontMenu, ColorTextMenu),
				EnterYourName,
				new MenuItem("<Validate>", this, RecordHighscore) }
					  );
			Congrats.Opacity = 0.9f;
			Congrats.Position = new Vector2(Congrats.Position.X, Gameover.ListItems[1].source.Y);
			Congrats.AlignItems();

			// First Menu appearing
			CurrentMenu = new List<MenuWindow>();
			CurrentMenu.Add(InitialMenu);
		}

		private void Initialize(MenuWindow mw, String Title, List<MenuItem> items)
		{
			mw.Title = Title;
			mw.ListItems = items;
			mw.ConstructMenu();
		}

		public void Update()
		{
			if (Controller.KeyPressed(Keys.Escape))
			{
				GameManager.sounds.playSound(Sound.SoundName.toogle);
				Previous();
			}

			if (CurrentMenu.Last() == Congrats)
			{
				YourName.Update();
				if (YourName.Text == String.Empty)
				{
					EnterYourName.Text = messageYourName;
					Congrats.AlignItems();
				}
				else
				{
					EnterYourName.Text = YourName.Text;
					Congrats.AlignItems();
				}
			}

			CurrentMenu.Last().Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (CurrentMenu.Last() == Congrats)
				Gameover.Draw(spriteBatch);
			CurrentMenu.Last().Draw(spriteBatch);
		}



		// Menu functions

		private void NewGame()
		{
			GameManager.game = new GameInstance(GameManager.Content);
			CurrentMenu = new List<MenuWindow>();
		}

		public void LaunchPause() 
		{
			GameManager.sounds.playSound(Sound.SoundName.toogle);
			CurrentMenu.Add(PauseMenu);
		}

		private void Resume() { CurrentMenu = new List<MenuWindow>(); }

		private void Parameters() { CurrentMenu.Add(ParamMenu); }

		public void LaunchGameOver()
		{
			if (CurrentMenu.Count == 0)
			{
				CurrentMenu.Add(Gameover);
				gameScore = GameManager.game.player.Score.value;

				List<BestScore> highscores;
				try
				{
					highscores = Serializer<List<BestScore>>.Load(pathHighScores);
				}
				catch
				{
					highscores = new List<BestScore>();
				}

				if (highscores.Count < SizeHighscores || gameScore > highscores.Last().score)
				{
					// an highscore is beaten
					GameManager.sounds.playSound(Sound.SoundName.applause);
					CurrentMenu.Add(Congrats);
					YourName.Text = String.Empty;
				}
			}
		}

		private void BackInitialMenu() { CurrentMenu = new List<MenuWindow> { InitialMenu }; }

		private void Previous()
		{
			if (CurrentMenu.Count > 1)
				CurrentMenu.Remove(CurrentMenu.Last());
			else
				CurrentMenu.Add(QuitMenu);
		}

		private void ChoiceMusic()
		{
			if (GameManager.sounds.musicmute)
			{
				GameManager.sounds.resumeMusic();
				choiceMusicItem.setText("Deactivate Music");
			}
			else
			{
				GameManager.sounds.pauseMusic();
				choiceMusicItem.setText("Activate Music");
			}
		}

		private void ChoiceSound()
		{
			if (GameManager.sounds.sfxmute)
			{
				GameManager.sounds.sfxmute = false;
				choiceSoundItem.setText("Deactivate Sound Effects");
			}
			else
			{
				GameManager.sounds.sfxmute = true;
				choiceSoundItem.setText("   Activate Sound Effects   ");
			}
		}

		private void BestScores()
		{
			List<BestScore> highscores;

			try
			{
				highscores = Serializer<List<BestScore>>.Load(pathHighScores);
			}
			catch
			{
				highscores = new List<BestScore>();
			}

			List<MenuItem> ScoreItems = new List<MenuItem>();
			for (int i = 0; i < SizeHighscores; i++)
			{
				if (i >= highscores.Count)
					ScoreItems.Add(new MenuItem((i + 1).ToString() + ". ", FontMenu, ColorTextMenu));
				else
				{
					String name = highscores[i].name;
					int score = highscores[i].score;
					ScoreItems.Add(new MenuItem((i + 1).ToString() + ". " + name + " : " + score, FontMenu, ColorTextMenu));
				}
			}

			ScoreItems.Add(new MenuItem("Back Menu", this, Previous));

			Initialize(Highscores, "Best Scores", ScoreItems);
			CurrentMenu.Add(Highscores);
		}

		private void RecordHighscore()
		{
			List<BestScore> highscores;

			try
			{
				highscores = Serializer<List<BestScore>>.Load(pathHighScores);
			}
			catch
			{
				highscores = new List<BestScore>();
			}

			BestScore newScore = new BestScore();
			newScore.score = gameScore;
			newScore.name = YourName.Text;
			highscores.Add(newScore);
			highscores.Sort((b1, b2) => BestScore.Compare(b1, b2));

			while (highscores.Count > SizeHighscores)
				highscores.Remove(highscores.Last());

			Serializer<List<BestScore>>.Save(pathHighScores, highscores);

			CurrentMenu.Remove(CurrentMenu.Last());
		}

		private void Quit() { GameManager.Application.Quit(); }
	}
}
