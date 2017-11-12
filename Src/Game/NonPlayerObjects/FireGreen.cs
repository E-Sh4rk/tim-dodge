using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which fall from the sky and poison the player (turn the screen).
	/// </summary>
	public class FireGreen : Fireball
	{
		public FireGreen(Texture t, Sprite s, Vector2 p) : base(t, s, p)
		{
			Damage = 0;
			color = Color.DarkGreen;
			poisonState = PoisonState.Horizontal;
		}
	}
}
