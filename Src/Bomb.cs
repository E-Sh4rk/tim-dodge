using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class Bomb : Enemy
	{
		public Bomb(Texture t, Sprite s, Vector2 p, GameInstance gi): base(t,s,p,gi)
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
			if (time > timeBeforeBoom)
			{
				gameInst.sounds.playSound(Sound.SoundName.explosion);
				Dead = true;
			}
		}

		public override void destructionMode(GameTime gt)
		{
			ChangeSpriteState(1);
			autoDestruct(gt);
		}

	}
}
