using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which fall from the sky and poison the player (flip horizontally the screen).
	/// </summary>
	public class FireYellow : Fireball
	{
		public FireYellow(Vector2 p) : base(p)
		{
			Damage = 0;
			color = Color.DeepPink;
			poisonState = PoisonState.Vertical;
		}
	}
}
