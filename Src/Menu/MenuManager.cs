using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace tim_dodge
{
	/// <summary>
	/// Manager the menu interface.
	/// </summary>
	public class MenuManager
	{
		private List<MenuWindow> CurrentMenu;
		private MenuWindow InitialMenu;
		private MenuWindow PauseMenu;
		private MenuWindow ParamMenu;
		private MenuItem choiceMusicItem;
		private MenuItem choiceSoundItem;
		private MenuItem choiceCharlieItem;
		private MenuWindow Gameover;
		private MenuWindow QuitMenu;
		private MenuWindow Highscores;
		private MenuItem MultiPlayer;

		private MenuWindow Congrats;
		private MenuItem EnterYourName;
		private const String messageYourName = "Enter your name";
		private KeyboardReader YourName;
		private int gameScore;
		private const int SizeHighscores = 5;

		private MenuWindow SaveEditor;
		private MenuItem EnterYourPath;
		private const String messageYourPath = "Enter a file name";
		private KeyboardReader YourPath;

		public ChooseMap chooseMap;

		public bool MenuRunning { get { return CurrentMenu.Count != 0; } }

		GameManager GameManager;

		public MenuManager(GameManager GameManager)
		{
			chooseMap = new ChooseMap();

			this.GameManager = GameManager;

			// Initialization of windows
			InitialMenu = new MenuWindow();
			PauseMenu = new MenuWindow();
			ParamMenu = new MenuWindow();
			Gameover = new MenuWindow();
			Congrats = new MenuWindow();
			SaveEditor = new MenuWindow();
			QuitMenu = new MenuWindow();
			Highscores = new MenuWindow();
			//MultiPlayer = new MenuItem();

			// Constructrion of menus
			Initialize(InitialMenu, "< Maps >", new List<MenuItem> {
				new MenuItem("New Game", NewGame),
				new MenuItem("New 2 Game", NewMultiGame),
				new MenuItem("Map Editor", NewEditor),
				new MenuItem("Parameters", Parameters),
				new MenuItem("Best Scores", BestScores),
				new MenuItem("Quit", Quit) }
					  );

			Initialize(PauseMenu, "Pause", new List<MenuItem> {
				new MenuItem("Resume", Resume),
				new MenuItem("New Game", NewGame),
				new MenuItem("Parameters", Parameters),
				new MenuItem("Best Scores", BestScores),
				new MenuItem("Quit", Quit) }
					  );

			if (Load.sounds.musicmute)
				choiceMusicItem = new MenuItem("Activate Music", ChoiceMusic);
			else
				choiceMusicItem = new MenuItem("Deactivate Music", ChoiceMusic);
			if (Load.sounds.sfxmute)
				choiceSoundItem = new MenuItem("   Activate Sound Effects   ", ChoiceSound);
			else
				choiceSoundItem = new MenuItem("Deactivate Sound Effects", ChoiceSound);
			/*
			try
			{
				if (GameManager.game.rotation)
					choiceCharlieItem = new MenuItem("Deactivate Option Charlie", ChoiceCharlie);
				else
					choiceCharlieItem = new MenuItem("Activate Option Charlie", ChoiceCharlie);
			}
			catch
			{
				choiceCharlieItem = new MenuItem("Activate Option Charlie", ChoiceCharlie);
			}*/

			Initialize(ParamMenu, "Parameters", new List<MenuItem> {
				choiceMusicItem,
				choiceSoundItem,
				//choiceCharlieItem,
				new MenuItem("Back to Menu", Previous) }
					  );

			Initialize(QuitMenu, "Quit the game ?", new List<MenuItem> {
				new MenuItem("No, I want to play more!", Previous),
				new MenuItem("Yes, leave me alone", Quit) }
					  );

			Initialize(Gameover, "Game Over", new List<MenuItem> {
				new MenuItem("Play Again", NewGame),
				new MenuItem("Back Menu", BackInitialMenu),
				new MenuItem("Quit the game", Quit) }
					  );

			YourName = new KeyboardReader("You beat a highscore".Length);
			EnterYourName = new MenuItem(messageYourName, Load.FontMenu, Load.ColorTextMenu); // Text updated by the update function
			Initialize(Congrats, "Congrats !", new List<MenuItem> {
				new MenuItem("You beat a highscore", Load.FontMenu, Load.ColorTextMenu),
				EnterYourName,
				new MenuItem("<Validate>", RecordHighscore) }
					  );
			Congrats.Opacity = 0.9f;
			Congrats.Position = new Vector2(Congrats.Position.X, Gameover.ListItems[1].source.Y);
			Congrats.AlignItems();

			YourPath = new KeyboardReader("Save your map".Length);
			EnterYourPath = new MenuItem(messageYourPath, Load.FontMenu, Load.ColorTextMenu); // Text updated by the update function
			Initialize(SaveEditor, "Saving !", new List<MenuItem> {
				new MenuItem("Save your map", Load.FontMenu, Load.ColorTextMenu),
				EnterYourPath,
				new MenuItem("<Validate>", RecordMap) }
					  );
			SaveEditor.Opacity = 0.9f;
			SaveEditor.Position = new Vector2(SaveEditor.Position.X, 100);//Gameover.ListItems[1].source.Y);
			SaveEditor.AlignItems();

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
				Load.sounds.playSound(Sound.SoundName.toogle);
				Previous();
			}
			else if (Controller.KeyPressed(Keys.Right))
			{
				if (!GameManager.GameRunning)
					chooseMap.RightMap();
			}
			else if (Controller.KeyPressed(Keys.Left))
			{
				if (!GameManager.GameRunning)
					chooseMap.LeftMap();
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

			else if (CurrentMenu.Last() == SaveEditor)
			{
				YourPath.Update();
				if (YourPath.Text == String.Empty)
				{
					EnterYourPath.Text = messageYourPath;
					SaveEditor.AlignItems();
				}
				else
				{
					EnterYourPath.Text = YourPath.Text;
					SaveEditor.AlignItems();
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
			GameManager.game = new GameInstance(chooseMap.currentMap);
			CurrentMenu = new List<MenuWindow>();
		}

		private void NewMultiGame()
		{
			GameManager.game = new GameInstance(chooseMap.currentMap);
			GameManager.game.players.Add(new Player(new Vector2(10,10), new Vector2(0,25), GameManager.game));
			CurrentMenu = new List<MenuWindow>();
		}

		private void NewEditor()
		{
			GameManager.editor = new MapEditorInstance();
			CurrentMenu = new List<MenuWindow>();
		}

		public void LaunchPause()
		{
			Load.sounds.playSound(Sound.SoundName.toogle);
			CurrentMenu.Add(PauseMenu);
		}

		private void Resume()
		{ 
			CurrentMenu = new List<MenuWindow>();
			if (GameManager.GameRunning)
				GameManager.game.focus = true;
			else
				GameManager.editor.focus = true;
		}

		private void Parameters() { CurrentMenu.Add(ParamMenu); }

		private void StartReplay(ChooseMap.Maps map, string filename)
		{
			GameManager.game = new GameInstance(map);
			GameManager.game.LoadReplay(filename);
			CurrentMenu = new List<MenuWindow>();
		}

		public void LaunchGameOver()
		{
			if (CurrentMenu.Count == 0)
			{
				CurrentMenu.Add(Gameover);
				gameScore = GameManager.game.GetGlobalScore();

				List<BestScore> highscores = Load.LoadHighScores();

				if (highscores.Count < SizeHighscores || gameScore > highscores.Last().score)
				{
					// an highscore is beaten
					Load.sounds.playSound(Sound.SoundName.applause);
					CurrentMenu.Add(Congrats);
					YourName.Text = String.Empty;
				}
			}
		}

		public void LaunchSaveMap()
		{
			CurrentMenu.Add(SaveEditor);
			YourPath.Text = String.Empty;
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
			if (Load.sounds.musicmute)
			{
				Load.sounds.resumeMusic();
				choiceMusicItem.Text = "Deactivate Music";
			}
			else
			{
				Load.sounds.pauseMusic();
				choiceMusicItem.Text = "Activate Music";
			}
		}

		private void ChoiceCharlie()
		{
			if (GameManager.game.rotation)
			{
				GameManager.game.rotation = false;
				choiceCharlieItem.Text = "Activate Option Charlie";
			}
			else
			{
				GameManager.game.rotation = true;
				choiceCharlieItem.Text = "Deactivate Option Charlie";
			}
					           
		}

		private void ChoiceSound()
		{
			if (Load.sounds.sfxmute)
			{
				Load.sounds.sfxmute = false;
				choiceSoundItem.Text = "Deactivate Sound Effects";
			}
			else
			{
				Load.sounds.sfxmute = true;
				choiceSoundItem.Text = "   Activate Sound Effects   ";
			}
		}

		private void ResetScores()
		{
			List<BestScore> emptyScores = new List<BestScore>();
			Serializer<List<BestScore>>.Save(Load.PathHighScores, emptyScores);
			try { Directory.Delete("replays", true); } catch { }
			Previous();
			BestScores();
		}

		private void BestScores()
		{
			List<BestScore> highscores = Load.LoadHighScores();

			List<MenuItem> ScoreItems = new List<MenuItem>();
			for (int i = 0; i < SizeHighscores; i++)
			{
				if (i >= highscores.Count)
					ScoreItems.Add(new MenuItem((i + 1).ToString() + ". ", Load.FontMenu, Load.ColorTextMenu));
				else
				{
					String name = highscores[i].name;
					String score = highscores[i].score.ToString();
					String filename = highscores[i].replay_filename;
					ChooseMap.Maps map = highscores[i].map;
					ScoreItems.Add(new MenuItem((i + 1).ToString() + ". " + name + " : " + score, /*Load.FontMenu, Load.ColorTextMenu,*/
					                            () => { StartReplay(map, filename);}));
				}
			}

			ScoreItems.Add(new MenuItem("Back Menu", Previous));
			ScoreItems.Add(new MenuItem("Reset", ResetScores));

			Initialize(Highscores, "Best Scores", ScoreItems);

			CurrentMenu.Add(Highscores);
		}

		private void RecordHighscore()
		{
			if (YourName.Text != String.Empty)
			{
				List<BestScore> highscores = Load.LoadHighScores();

				BestScore newScore = new BestScore();
				newScore.score = gameScore;
				newScore.name = YourName.Text;
				newScore.replay_filename = "replays/"+newScore.name+"-"+newScore.score+".xml";
				newScore.map = chooseMap.currentMap;
				GameManager.game.SaveReplay(newScore.replay_filename);
				highscores.Add(newScore);
				highscores.Sort((b1, b2) => BestScore.Compare(b1, b2));

				while (highscores.Count > SizeHighscores)
				{
					try { File.Delete(highscores.Last().replay_filename); } catch { }
					highscores.Remove(highscores.Last());
				}

				Serializer<List<BestScore>>.Save(Load.PathHighScores, highscores);

				CurrentMenu.Remove(CurrentMenu.Last());
				CurrentMenu.Add(InitialMenu);
			}
		}

		private void RecordMap()
		{
			if (YourPath.Text != String.Empty)
			{
				GameManager.editor.Save(YourPath.Text + ".xml");
				CurrentMenu.Remove(CurrentMenu.Last());
				CurrentMenu.Add(InitialMenu);
			}
		}

		private void Quit() { GameManager.Application.Quit(); }
	}
}
