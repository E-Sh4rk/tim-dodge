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
			map = new Map(Load.BackgroundSun, Load.MapTextureNature);
			focus = true;
			mouseBlock = new BlockObject(Load.MapTextureNature, Map.numberTileX / 2, Map.numberTileY / 2, BlockObject.Ground.MiddleGround);
		}

		public BlockObject block;

		public Map map;

		public bool focus;

		private BlockObject mouseBlock;

		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();
			MouseState mouse = Mouse.GetState();

			mouseBlock.x = (int)(mouse.X*TimGame.general_scale/mouseBlock.w);
			mouseBlock.y = (int)(mouse.Y* TimGame.general_scale/mouseBlock.h);

			if (state.IsKeyDown(Keys.N))
			{
				var s = mouseBlock.Sprite.NowState();
				if (s + 1 == mouseBlock.Sprite.NumberOfState())
					s = -1;
				mouseBlock.ChangeSpriteState(s+1);
			}

			else if (state.IsKeyDown(Keys.P))
			{
				var s = mouseBlock.Sprite.NowState();
				if (s == 0)
					s = mouseBlock.Sprite.NumberOfState()-2;
				mouseBlock.ChangeSpriteState(s+1);
			}

			if (mouse.LeftButton == ButtonState.Pressed || state.IsKeyDown(Keys.Enter))
			{
				map.AddBlock(mouseBlock);
				mouseBlock = new BlockObject(Load.MapTextureNature, mouseBlock.x, mouseBlock.y, BlockObject.Ground.MiddleGround);
			}

			else if (mouse.RightButton == ButtonState.Pressed || state.IsKeyDown(Keys.Delete) || state.IsKeyDown(Keys.Back))
			{
				map.RemoveBlock(mouseBlock.x, mouseBlock.y);
			}
		}


		public void Draw(SpriteBatch spriteBatch)
		{
			map.Draw(spriteBatch);
			mouseBlock.Draw(spriteBatch);
		}

	}
}