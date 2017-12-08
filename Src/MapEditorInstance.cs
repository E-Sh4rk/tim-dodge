using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class MapEditorInstance 
	{

		GameManager gameManager;

		public MapEditorInstance(ChooseMap.Maps Maps, GameManager gameManager)
		{
			this.gameManager = gameManager;

			map = new Map(Load.BackgroundSun, Load.MapTextureNature, Maps);
			focus = true;
			mouseBlock = new BlockObject(Map.numberTileX / 2, Map.numberTileY / 2, BlockObject.Ground.MiddleGround);
		}

		public BlockObject block;
		public Map map;
		public bool focus;
		private BlockObject mouseBlock;

		const double time_before_rechange = 0.2f;
		protected double last_time_change = 0f;

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();
			MouseState mouse = Mouse.GetState();
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			last_time_change += elapsed;

			mouseBlock.x = (int)(mouse.X*TimGame.general_scale/mouseBlock.w);
			mouseBlock.y = (int)(mouse.Y* TimGame.general_scale/mouseBlock.h);

			// we can change
			if (last_time_change >= time_before_rechange)
			{
				if (state.IsKeyDown(Keys.N))
				{
					var s = mouseBlock.Sprite.NowState();
					if (s == mouseBlock.Sprite.NumberOfState() - 1)
						s = -1;
					mouseBlock.ChangeSpriteState(s + 1);
					mouseBlock.h = mouseBlock.Sprite.RectOfSprite().Height;
					mouseBlock.w = mouseBlock.Sprite.RectOfSprite().Width;
					last_time_change = 0f;
				}

				else if (state.IsKeyDown(Keys.P))
				{
					var s = mouseBlock.Sprite.NowState();
					if (s == 0)
						s = mouseBlock.Sprite.NumberOfState();
					mouseBlock.ChangeSpriteState(s - 1);
					mouseBlock.h = mouseBlock.Sprite.RectOfSprite().Height;
					mouseBlock.w = mouseBlock.Sprite.RectOfSprite().Width;
					last_time_change = 0f;
				}
			}

			if (mouse.LeftButton == ButtonState.Pressed || state.IsKeyDown(Keys.Space))
			{
				map.AddBlock(mouseBlock);
				mouseBlock = new BlockObject(mouseBlock.x, mouseBlock.y, mouseBlock.state);
			}

			else if (mouse.RightButton == ButtonState.Pressed || state.IsKeyDown(Keys.Delete) || state.IsKeyDown(Keys.Back))
			{
				map.RemoveBlock(mouseBlock.x, mouseBlock.y, false);
			}
		}

		public void Save(String path)
		{
			Map.SaveMap sm = new Map.SaveMap();
			sm.tileMap = map.tileMap.ConvertAll((BlockObject bl) => bl.CreateSave());
			sm.platforms = new List<MapPlatform.SavePlatform>();
			Serializer<Map.SaveMap>.Save(path, sm);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);

			int b = 2;
			Texture2D SingleTexture = new Texture2D(gameManager.Application.GraphicsDevice, mouseBlock.Size.X+2*b, mouseBlock.Size.Y+2*b, false, SurfaceFormat.Color);

			Color[] color = new Color[ (mouseBlock.Size.X+2*b) * (mouseBlock.Size.Y+2*b)];//set the color to the amount of pixels in the textures
			for (int i = 0; i < color.Length; i++)//loop through all the colors setting them to whatever values we want
			{
				color[i] = Color.Black;
			}

			SingleTexture.SetData(color);
			spriteBatch.Draw(SingleTexture, 
			                 new Rectangle((int)mouseBlock.Position.X-b, 
			                               (int)mouseBlock.Position.Y-b, mouseBlock.Size.X+2*b, 
			                               mouseBlock.Size.Y + 2*b), Color.Red);

			mouseBlock.Draw(spriteBatch);

			// TODO : Box pour la sélection de la tile en cours
			//Texture2D whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
			//whiteRectangle.SetData(new[] { Color.White });
			//System. blackPen = new Pen(Color.Black, 3);

		}

	}
}