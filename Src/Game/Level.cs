using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Level : GameObject
	{
		public FallingObjects falling;
		public Map map;

		public Level(GameInstance game) : base(Load.MapTexture, new Sprite("Content.ground.groundXml.xml"), new Vector2(0, 0))
		{
			map = new Map();
			falling = new FallingObjects(game);
			nowTest = Ground.MiddleGround;
			tileMap = new List<Block>();
			for (int i = 3; i < numberTileX - 4; i++)//numberTileX; i++)
			{
				tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY - 1, Ground.MiddleGround));
			}
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 2, numberTileY - 1, Ground.LeftEGround));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 1, numberTileY - 1, Ground.BottomLeftDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 1, Ground.MiddleDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 1, numberTileY - 2, Ground.RightGround));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 2, Ground.MiddleGround));

			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 4, numberTileY - 1, Ground.RightEGround));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 1, Ground.BottomRightDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 1, Ground.MiddleDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 1, Ground.MiddleDurt));

			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 2, Ground.LeftDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 2, Ground.MiddleDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 2, Ground.MiddleDurt));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 3, Ground.LeftGround));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 3, Ground.MiddleGround));
			tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 3, Ground.MiddleGround));
			//tileMap.Add()
			//tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 2, Ground.Bo));
			//tileMap = new List<Block> { new Block(Sprite.RectOfSprite().Height , Sprite.RectOfSprite().Width, 0, numberTileY-1, Ground.LeftEGround),
		}

		private Ground nowTest;

		public const int numberTileY = 12;
		public const int numberTileX = 22;

		public enum Ground
		{
			LeftGround = 0,
			MiddleGround = 1,
			RightGround = 2,
			LeftDurt = 3,
			MiddleDurt = 4,
			RightDurt = 5,
			RightEGround = 6,
			BottomRightDurt = 7,
			BottomDurt = 8,
			BottomLeftDurt = 9,
			LeftEGround = 10,
			BottomLeft2Durt = 11,
			LeftPlatform = 12,
			MiddlePlatform = 13,
			RightPlatform = 14,
			BottomRight2Durt = 15
		}

		public class Block
		{
			private int _x;
			private int _y;

			public int x
			{
				get { return _x;}
				set { _x = value; _position.X = value * h; }
			}

			public int y
			{
				get { return _y; }
				set { _y = value; _position.Y = value * w; }
			}
			private Vector2 _position;

			public Vector2 position
			{
				get { return _position;}
			}

			public Ground state;

			public float h;
			public float w;

			public Block(float h, float w, int x, int y, Ground state)
			{
				this.h = h;
				this.w = w;
				this.x = x;
				this.y = y;
				this.state = state; 
			}


		}

		public List<Level.Block> tileMap;

		public new void Draw(SpriteBatch spriteBatch)
		{
			Sprite.ChangeState((int)nowTest);
			tileMap.ForEach((Block obj) => DrawBlock(spriteBatch, obj));
		}


		public void DrawBlock(SpriteBatch spriteBatch, Block bl)
		{
			Sprite.ChangeState((int)bl.state);
			spriteBatch.Draw(Texture.Image, bl.position, new Rectangle(TexturePosition, Size), color, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
		}

	}
}
