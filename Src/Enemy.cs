using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Enemy: PhysicalObject
	{
		public Enemy(Texture t, Sprite s, Vector2 p): base(t,s,p)
		{
			Mass = 5;
		}
	}
}
