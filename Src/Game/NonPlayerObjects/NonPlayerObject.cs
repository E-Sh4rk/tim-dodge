using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Base class for every non playable object (typically, enemies and bonuses).
	/// </summary>
	public class NonPlayerObject: PhysicalObject
	{
		public NonPlayerObject(Texture t, Sprite s, Vector2 p): base(t,s,p)
		{
			Mass = 5;
			Damage = 0;
			Bonus = 0;
			Life = 0;
			Dead = false;
		}

		public int Damage
		{
			get;
			protected set;
		}

		public int Life
		{
			get;
			protected set;
		}

		public int Bonus
		{
			get;
			protected set;
		}

		public virtual void destructionMode(GameTime gt)
		{
			Dead = true;
		}

		public bool Dead
		{
			get;
			protected set;
		}

		public virtual void TouchPlayer()
		{
			Load.sounds.playSound(Sound.SoundName.explosion);
		}

	}
}
