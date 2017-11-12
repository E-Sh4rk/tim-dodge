using System;
using System.Collections.Generic;
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
			poisonState = GameObject.PoisonState.Nothing;
			Dead = false;
		}

		public PoisonState poisonState;

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

		public bool Dead
		{
			get;
			protected set;
		}

		public bool Damaged
		{
			get;
			protected set;
		}

		public virtual void SetState(bool damaged, bool dead)
		{
			Damaged = damaged;
			Dead = dead;
		}

		public virtual void SufferDamage()
		{
			Damaged = true;
		}

		public virtual void TouchPlayer()
		{
			Load.sounds.playSound(Sound.SoundName.explosion);
		}

	}
}
