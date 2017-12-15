using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which fall from the sky and poison the player (turn the screen).
	/// </summary>
	public class FirePoison : Fireball
	{
		public FirePoison(Vector2 p): base(p)
		{
			Damage = 1;
			color = Color.DarkViolet;
			poisonState = PoisonState.Rotation;
		}
	}
}
