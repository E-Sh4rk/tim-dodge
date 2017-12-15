using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace tim_dodge
{
	/// <summary>
	/// Some utility functions to play sound.
	/// </summary>
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

		public bool rewindmode
		{
			get;
			private set;
		}

		private SoundEffectInstance musicNow;
        private SoundEffectInstance[] musicLoaded;

		public Sound(SoundEffect[] sfx, SoundEffect[] musc)
		{
			sounds = sfx;
			musics = musc;
            musicLoaded = new SoundEffectInstance[3];

			sfxmute = false; // Mute sound effects by default
			musicmute = false; // Mute music by default
			rewindmode = false; // No rewind by default

			try // Catch exceptions (in case the computer can't play songs (example: Travis)
			{
                for (int i = 0; i < 3; i++)
                {
                    musicLoaded[i] = musics[i].CreateInstance();
                    musicLoaded[i].Volume = 0.60f;
                    musicLoaded[i].IsLooped = true;
                }

                musicNow = musicLoaded[(int)MusicName.menu];
                musicNow.Resume();
            }
			catch { }
		}

        public void realStopMusic(MusicName song)
        {
            int i = (int)song;
            musicLoaded[i].Stop();
            musicLoaded[i].Dispose();
            musicLoaded[i] = musics[i].CreateInstance();
            musicLoaded[i].Volume = 0.60f;
            musicLoaded[i].IsLooped = true;
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
			coin = 8,
			food = 9,
		}


		public enum MusicName
		{
			cuphead = 0,
			dj = 1,
            menu = 2
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
				musicNow.Pause();
                musicNow = musicLoaded[(int)mus];

				if (!musicmute)
				{
					musicNow.Resume();
				}
			}
			catch { }
		}

		public void playRewind()
		{
			try
			{
				if (!musicmute)
				{
					pauseMusic();
					rewindmode = true;
                    musicLoaded[(int)MusicName.dj].Play();
				}
			}
			catch { }
		}
		public void stopRewind()
		{
			try
			{
				if (rewindmode)
				{
                    musicLoaded[(int)MusicName.dj].Stop();
					resumeMusic();
					rewindmode = false;
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

        public void newGameSong()
        {
            try // Catch exceptions in case the computer can't play songs (example: Travis)
            {
                if (!musicmute)
                {
                    realStopMusic(MusicName.cuphead);
                    musicNow.Pause();
                    musicLoaded[(int)MusicName.cuphead].Resume();
               
                    musicNow = musicLoaded[(int)MusicName.cuphead];
                }
            }
            catch { }
        }

	}
}
