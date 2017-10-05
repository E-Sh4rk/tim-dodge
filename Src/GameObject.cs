using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class GameObject
	{
		public World world;
		public Vector2 Position;
		public Texture2D Texture;

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
			spriteBatch.Draw(Texture, Position, Source, Color.White);
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
					case Collision.Direction.RIGHT:
						frameIndex = nextFrame(frameIndex);

						break;
				}

				time = 0f;
			}


			Source = new Rectangle(
				//((int)frameIndex-1) * (frameWidth+16) + 30,
				testTim[((int)frameIndex)%22],
				38,
				frameWidth,
				frameHeight);
		}

		public int[] testTim = {30, 126, 218, 314, 410, 501, 597, 693, 789, 889, 986, 1082, 1173, 1266, 1359, 1452, 1547, 1642, 1739, 1838, 1933, 2032};

		// Rectangle permettant de définir la zone de l'image à afficher
		public Rectangle Source;
		// Durée depuis laquelle l'image est à l'écran
		public float time;
		// Durée de visibilité d'une image
		public float frameTime = 0.1f;
		// Indice de l'image en cours

		public enum framesIndex
		{
			R1 = 1, R2 = 2, R3 = 3, R4 = 4, R5 = 5,
			R6 = 6, R7 = 7, R8 = 8, R9 = 9, R10 = 10,
			R11 = 11, R12 = 12, R13 = 13, R14 = 14,
			R15 = 15, R16 = 16, R17 = 17, R18 = 18,
			R19 = 19, R20 = 20, R21 = 21, R22 = 22
		}


		public framesIndex nextFrame(framesIndex frame)
		{
			int iframe = (int)frame;

			if (iframe < (int)framesIndex.R22)
			{
				return (framesIndex)(iframe + 1);
			}

			else if (iframe == (int)framesIndex.R22)
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
