using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Bomb : Enemy
	{
		public Bomb(Texture t, Sprite s, Vector2 p): base(t,s,p)
		{
			Mass = 5;
			time = 0f;
			timeBeforeBoom = 0.8f;
		}

		public Rectangle[] rightWalls
		{
			get;
			protected set;
		}

		private float time;
		private float timeBeforeBoom;

		public void autoDestruct(GameTime gameTime)
		{
			time += (float)gameTime.ElapsedGameTime.TotalSeconds;
			color = Color.Red; 
			Dead |= time > timeBeforeBoom;
		}

		public override void destructionMode(GameTime gt)
		{
			autoDestruct(gt);
		}

	}
}
