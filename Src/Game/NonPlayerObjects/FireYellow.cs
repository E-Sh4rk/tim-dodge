using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	/// <summary>
	/// Ennemie which fall from the sky and poison the player (flip horizontally the screen).
	/// </summary>
	public class FireYellow : Fireball
	{
		public FireYellow(Texture t, Sprite s, Vector2 p) : base(t, s, p)
		{
			Damage = 0;
			color = Color.DeepPink;
			poisonState = PoisonState.Vertical;
		}
	}
}
