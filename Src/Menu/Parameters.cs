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
		private MenuItem backToMenu;


		public Parameters(Texture2D BackgroundPicture, GameManager GameManager,
						   SpriteFont FontMenu, Color ColorTextMenu, Color ColorHighlightSelection)
			: base(BackgroundPicture, ColorHighlightSelection)
		{
			activateSoundEffect = new MenuItem("   Activate Sound Effects   ", FontMenu, ColorTextMenu);
			deactivateSoundEffect = new MenuItem("Deactivate Sound Effects", FontMenu, ColorTextMenu);
			activateMusic = new MenuItem("Activate Music", FontMenu, ColorTextMenu);
			deactivateMusic = new MenuItem("Deactivate Music", FontMenu, ColorTextMenu);
			backToMenu = new MenuItem("Back to Menu", FontMenu, ColorTextMenu);

			activateSoundEffect.LaunchSelection += activSoundEffect;
			deactivateSoundEffect.LaunchSelection += deactivSoundEffect;
			activateMusic.LaunchSelection += activMusic;
			deactivateMusic.LaunchSelection += deactivMusic;
			backToMenu.LaunchSelection += GameManager.BackMenu;

			ListItems.Add(deactivateMusic);
			ListItems.Add(deactivateSoundEffect);
			ListItems.Add(backToMenu);

			ConstructMenu();
		}

		private void activMusic()
		{
			GameManager.sounds.music.Play();
			ListItems[ListItems.IndexOf(activateMusic)] = deactivateMusic;
			ConstructMenu();
		}

		private void deactivMusic()
		{
			GameManager.sounds.music.Stop();
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
