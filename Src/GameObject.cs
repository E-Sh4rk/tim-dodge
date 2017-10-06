using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class GameObject
	{
		public World world;
		public Vector2 Position;
		public Vector2 Acceleration;
		public Vector2 Velocity;
		public Vector2 Friction = new Vector2(1,0);

		public Texture2D Texture;
		SpriteEffects s = SpriteEffects.None;

		public GameObject()
		{
		}

		public GameObject(int totalAnimationFrames, int frameWidth, int frameHeight, World world)
		{
			_totalFrames = totalAnimationFrames;
			_frameWidth = frameWidth;
			_frameHeight = frameHeight;
			this.world = world;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Color.White);
		}

		public void DrawAnimation(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Source, Color.White, 0f, new Vector2(0,0), new Vector2(1,1), s, 0f);
		}

		public void UpdateFrame(GameTime gameTime)
		{
			time += (float)gameTime.ElapsedGameTime.TotalSeconds;

			while (time > frameTime)
			{
				switch (direction)
				{
					/*case Collision.Direction.LEFT:
						if (frameIndex == framesIndex.LEFT_1)
							frameIndex = framesIndex.LEFT_2;
						else
							frameIndex = framesIndex.LEFT_1;
						break;
						*/
					case Collision.Direction.NONE:
						frameIndex = nextFrame(frameIndex);

						break;

					case Collision.Direction.TOP:
						frameIndex = nextFrame(frameIndex);

						break;
						
					case Collision.Direction.RIGHT:
						frameIndex = nextFrame(frameIndex);
						s = SpriteEffects.None;

						break;
						
					case Collision.Direction.LEFT:
						frameIndex = nextFrame(frameIndex);
						s = SpriteEffects.FlipHorizontally;

						break;
				}

				time = 0f;
			}


			Source = new Rectangle(
				//((int)frameIndex-1) * (frameWidth+16) + 30,
				testTim[((int)frameIndex)%21],
				38,
				frameWidth+10,
				frameHeight);
			/*

			Source = new Rectangle(
				//((int)frameIndex-1) * (frameWidth+16) + 30,
				runTim[((int)frameIndex) % 9],
				222,
				118,
				140);
*/
		}

		public int[] testTim = {29, 125, 217, 313, 409, 501, 597, 693, 789, 889, 985, 
			            1081, 1173, 1266, 1359, 1452, 1545, 1641, 1737, 1836, 1932};

		public static int t = 1011;
		public int[] runTim = { 20, 156, 296, 424, 544, 660, 780, 900, 1011};//, t, t, t};
		
		public int[] testTime = {1452, 1545, 1641, 1737, 1836, 1932, 2031,
						0, 1177, 1275, 1359, 1452, 1546, 1641, 1737, 1836, 889, 985, 1081, 1173, 1266, 1359, };
		// Rectangle permettant de définir la zone de l'image à afficher
		public Rectangle Source;
		// Durée depuis laquelle l'image est à l'écran
		public float time;
		// Durée de visibilité d'une image
		public float frameTime = 0.02f;//0.07f;
		// Indice de l'image en cours

		public enum framesIndex
		{
			R1 = 1, R2 = 2, R3 = 3, R4 = 4, R5 = 5,
			R6 = 6, R7 = 7, R8 = 8, R9 = 9, R10 = 10,
			R11 = 11, R12 = 12, R13 = 13, R14 = 14,
			R15 = 15, R16 = 16, R17 = 17, R18 = 18,
			R19 = 19, R20 = 20, R21 = 21,

		}


		public framesIndex nextFrame(framesIndex frame)
		{
			int iframe = (int)frame;

			if (iframe < (int)framesIndex.R21)
			{
				return (framesIndex)(iframe + 1);
			}

			else if (iframe == (int)framesIndex.R21)
			{
				return framesIndex.R1;
			}

			else
			{
				// TODO : other cases
				return framesIndex.R1;
			}
		}


		public framesIndex frameIndex;

		private int _totalFrames;
		public int totalFrames
		{
			get { return _totalFrames; }
		}

		private int _frameWidth;

		public int frameWidth
		{
			get { return _frameWidth; }
		}
		private int _frameHeight;
		public int frameHeight
		{
			get { return _frameHeight; }
		}


		public Collision.Direction direction;
	}
}
