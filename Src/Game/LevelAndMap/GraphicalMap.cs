using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents the graphical aspect of a map. Used to display the map.
	/// </summary>
	public class GraphicalMap : GameObject
	{
		public GraphicalMap(Texture2D Background, Texture MapTexture)
			: base(MapTexture, new Sprite("Content.ground.natureXml.xml"), new Vector2(0, 0))
		{
			tileMap = new List<Map.Block>();

			this.Background = Background;

			/*
			for (int i = 3; i < numberTileX - 4; i++)//numberTileX; i++)
			{
				if (i != 7 && i != 8 && i != 6 && i !=9)
					tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY - 1, Map.Ground.MiddleGround));
			}

			for (int i = 0; i < numberTileX ; i++)//numberTileX; i++)
			{
				if(i != 7 && i != 8 && i != 6 && i != 9)
					tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY , Map.Ground.MiddleDurt));
			}

			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 6, numberTileY, Map.Ground.RightDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 9, numberTileY, Map.Ground.LeftDurt));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 6, numberTileY-1, Map.Ground.RightGround));
			tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, 9, numberTileY - 1, Map.Ground.LeftGround));
			*/

			for (int i = 3; i < numberTileX - 4; i++)//numberTileX; i++)
			{
				tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY - 1, Map.Ground.MiddleGround));
			}

			for (int i = 0; i < numberTileX; i++)//numberTileX; i++)
			{
				tileMap.Add(new Map.Block(Sprite.RectOfSprite().Height, Sprite.RectOfSprite().Width, i, numberTileY, Map.Ground.MiddleDurt));
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

		public Texture2D Background;

		public List<Map.Block> tileMap;

		public void changeTexture(Texture NewMapTexture)
		{
			Texture = NewMapTexture;
		}

		public void changeBackground(Texture2D NewBackground)
		{
			Background = NewBackground;
		}


		public new void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Background, Vector2.Zero, Color.White);
			tileMap.ForEach((Map.Block obj) => DrawBlock(spriteBatch, obj));
		}


		public void DrawBlock(SpriteBatch spriteBatch, Map.Block bl)
		{
			Sprite.ChangeState((int)bl.state);
			spriteBatch.Draw(Texture.Image, bl.position, new Rectangle(TexturePosition, Size), color, 0f, new Vector2(0, 0), new Vector2(1, 1), Sprite.Effect, 0f);
		}

		public const int numberTileY = 11;
		public const int numberTileX = 20;

	}
}
