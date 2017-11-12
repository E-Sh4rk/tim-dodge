using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class FirePoison : Fireball
	{
		public FirePoison(Texture t, Sprite s, Vector2 p): base(t,s,p)
		{
			Damage = 0;
			color = Color.DarkViolet;
			Poisons.Add(PoisonState.Rotation);
		}
	}
}
