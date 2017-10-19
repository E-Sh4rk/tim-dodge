using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Parameters : Menu
	{
		private MenuItem activateSoundEffect;
		private MenuItem deactivateSoundEffect;
		private MenuItem activateMusic;
		private MenuItem deactivateMusic;

		private GameManager GameManager;

		public Parameters(GameManager GameManager)
			: base(GameManager)
		{
			this.GameManager = GameManager;

			activateMusic = new MenuItem("Activate Music", this, activMusic);
			deactivateMusic = new MenuItem("Deactivate Music", this, deactivMusic);
			activateSoundEffect = new MenuItem("   Activate Sound Effects   ", this, activSoundEffect);
			deactivateSoundEffect = new MenuItem("Deactivate Sound Effects", this, deactivSoundEffect);

			MenuItem backToMenu = new MenuItem("Back to Menu", this, GameManager.Previous);

			if (GameManager.sounds.musicmute)
				ListItems.Add(activateMusic);
			else
				ListItems.Add(deactivateMusic);
			
			if (GameManager.sounds.sfxmute)
				ListItems.Add(activateSoundEffect);
			else
				ListItems.Add(deactivateSoundEffect);
			
			ListItems.Add(backToMenu);

			ConstructMenu();
		}

		private void activMusic()
		{
			//if (GameManager.GameRunning)
			GameManager.sounds.resumeMusic();
			
			ListItems[ListItems.IndexOf(activateMusic)] = deactivateMusic;
			ConstructMenu();
		}

		private void deactivMusic()
		{
			//if (GameManager.GameRunning)
			GameManager.sounds.pauseMusic();
			
			ListItems[ListItems.IndexOf(deactivateMusic)] = activateMusic;
			ConstructMenu();
		}

		private void activSoundEffect()
		{
			GameManager.sounds.sfxmute = false;
			ListItems[ListItems.IndexOf(activateSoundEffect)] = deactivateSoundEffect;
			ConstructMenu();
		}

		private void deactivSoundEffect()
		{
			GameManager.sounds.sfxmute = true;
			ListItems[ListItems.IndexOf(deactivateSoundEffect)] = activateSoundEffect;
			ConstructMenu();
		}
	}
}
