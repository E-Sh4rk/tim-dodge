using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Enemy: PhysicalObject
	{
		public Enemy(Texture t, Sprite s, Vector2 p, GameInstance gi): base(t,s,p)
		{
			Mass = 5;
			Damage = 1;
			Dead = false;
			gameInst = gi;
		}

		public int Damage
		{
			get;
			protected set;
		}

		protected GameInstance gameInst;

		public virtual void destructionMode(GameTime gt)
		{
			Dead = true;
		}

		public bool Dead
		{
			get;
			protected set;
		}

	}
}
