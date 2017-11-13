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
		public MapEditorInstance()
		{
			map = new Map(Load.BackgroundSun, Load.MapTextureNature, ChooseMap.Maps.DuneMap);
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

			if (mouse.LeftButton == ButtonState.Pressed || state.IsKeyDown(Keys.Enter))
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
			Serializer<List<BlockObject.SaveBlock>>.Save(path, map.tileMap.ConvertAll((BlockObject bl) => bl.CreateSave()));
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);
			mouseBlock.Draw(spriteBatch);
		}

	}
}