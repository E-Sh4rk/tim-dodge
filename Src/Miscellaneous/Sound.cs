using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	public class Sound
	{
		public SoundEffectInstance music; // Music played

		public SoundEffect[] sounds;
		public SoundEffect[] musics;

		public bool sfxmute;

		public bool musicmute
		{
			get;
			private set;
		}

		private SoundEffectInstance musicNow;

		public Sound(SoundEffect[] sfx, SoundEffect[] musc)
		{
			sounds = sfx;
			musics = musc;

			sfxmute = false; // Mute sound effects by default
			musicmute = false; // Mute music by default

			try // Catch exceptions in case the computer can't play songs (example: Travis)
			{
				musicNow = musics[(int)MusicName.cuphead].CreateInstance();
				musicNow.Play();
				musicNow.Volume = 0.60f;
				musicNow.IsLooped = true;
			}
			catch { }
		}

		public enum SoundName
		{
			jump = 0,
			explosion = 1,
			dammage = 2,
			fire = 3,
			menu = 4,
			toogle = 5,
			applause = 6,
			ah = 7,
			coin = 8
		}


		public enum MusicName
		{
			cuphead = 0
		}


		public void playSound(SoundName son)
		{
			try // Catch exceptions in case the computer can't play songs (example: Travis)
			{
				if (!sfxmute)
					sounds[(int)son].Play();
			}
			catch { }
		}

		public void playMusic(MusicName mus)
		{
			try // Catch exceptions in case the computer can't play songs (example: Travis)
			{
				musicNow.Stop();
				musicNow = musics[(int)mus].CreateInstance();
				musicNow.Volume = 0.60f;
				musicNow.IsLooped = true;

				if (!musicmute)
				{
					musicNow.Play();
				}
			}
			catch { }
		}

		public void pauseMusic()
		{
			try // Catch exceptions in case the computer can't play songs (example: Travis)
			{
				if (!musicmute)
				{
					musicNow.Pause();
					musicmute = true;
				}
			}
			catch { }
		}

		public void resumeMusic()
		{
			try // Catch exceptions in case the computer can't play songs (example: Travis)
			{
				if (musicmute)
				{
					musicNow.Resume();
					musicmute = false;
				}
			}
			catch { }
		}


	}
}
