using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class GraphicalMap : GameObject
	{
		public GraphicalMap()
			: base(Load.MapTexture, new Sprite("Content.ground.groundXml.xml"), new Vector2(0, 0))
		{
			tileMap = new List<Map.Block>();

			for (int i = 3; i < numberTileX - 4; i++)//numberTileX; i++)
			{
				tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY - 1, Map.Ground.MiddleGround));
			}

			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 2, numberTileY - 1, Map.Ground.LeftEGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 1, numberTileY - 1, Map.Ground.BottomLeftDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 1, Map.Ground.MiddleDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 1, numberTileY - 2, Map.Ground.RightGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 2, Map.Ground.MiddleGround));

			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 4, numberTileY - 1, Map.Ground.RightEGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 1, Map.Ground.BottomRightDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 1, Map.Ground.MiddleDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 1, Map.Ground.MiddleDurt));

			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 2, Map.Ground.LeftDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 2, Map.Ground.MiddleDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 2, Map.Ground.MiddleDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 3, numberTileY - 3, Map.Ground.LeftGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 2, numberTileY - 3, Map.Ground.MiddleGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, numberTileX - 1, numberTileY - 3, Map.Ground.MiddleGround));
			//tileMap.Add()
			//tileMap.Add(new Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 0, numberTileY - 2, Ground.Bo));
			//tileMap = new List<Block> { new Block(Sprite.RectOfSprite().Height , Sprite.RectOfSprite().Width, 0, numberTileY-1, Ground.LeftEGround),

		}


		public List<Map.Block> tileMap;


		public new void Draw(SpriteBatch spriteBatch)
		{
			tileMap.ForEach((Map.Block obj) => DrawBlock(spriteBatch, obj));
		}


		public void DrawBlock(SpriteBatch spriteBatch, Map.Block bl)
		{
			Sprite.ChangeState((int)bl.state);
			spriteBatch.Draw(Texture.Image, bl.position, new Rectangle(TexturePosition, Size), color, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
		}

		public const int numberTileY = 12;
		public const int numberTileX = 22;

	}
}
