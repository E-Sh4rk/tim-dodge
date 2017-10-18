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

			sfxmute = true; // Mute sound effects by default
			musicmute = true; // Mute music by default

			musicNow = musics[(int)MusicName.cuphead].CreateInstance();
			musicNow.Volume = 0.60f;
			musicNow.IsLooped = true;
		}

		public enum SoundName
		{
			jump = 0,
			explosion = 1,
			dammage = 2
		}


		public enum MusicName
		{
			cuphead = 0
		}


		public void playSound(SoundName son)
		{
			if (!sfxmute)
				sounds[(int)son].Play();
		}

		public void playMusic(MusicName mus)
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

		public void pauseMusic()
		{
			if (!musicmute)
			{
				musicNow.Pause();
				musicmute = true;
			}

		}

		public void resumeMusic()
		{
			if (musicmute)
			{
				musicNow.Resume();
				musicmute = false;
			}
		}


	}
}
