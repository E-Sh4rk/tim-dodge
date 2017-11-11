using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Renderer
	{
		RenderTarget2D renderTarget;
		readonly GraphicsDeviceManager graphics;
	 
		public Renderer(GraphicsDeviceManager graphics)
		{
			this.graphics = graphics;

			renderTarget = new RenderTarget2D(
				graphics.GraphicsDevice,
				graphics.PreferredBackBufferWidth,
				graphics.PreferredBackBufferHeight);
		}

		public void Draw(SpriteBatch spriteBatch, GameManager game)
		{
			Rend(spriteBatch, game);
			
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			float w = ((Texture2D)renderTarget).Width;
			float h = ((Texture2D)renderTarget).Height;

			Vector2 middle = new Vector2(w / 2.0f, h / 2.0f);

			float proportion = h / w;
			float angle = (float)(Math.PI/4);
			angle = 0;
			float scale = (float)(((Texture2D)renderTarget).Width * Math.Cos(angle) +
								   ((Texture2D)renderTarget).Height * Math.Sin(angle)) / ((Texture2D)renderTarget).Width;
			scale = 1;

			spriteBatch.Begin();
			spriteBatch.Draw((Texture2D)renderTarget, middle, null, Color.White,
			                 angle, middle,
			                 scale, SpriteEffects.None, 0); // (float)(Math.PI * 2)
			spriteBatch.End();

		}

		public void Rend(SpriteBatch spriteBatch, GameManager game)
		{
			// Set the device to the render target
			graphics.GraphicsDevice.SetRenderTarget(renderTarget);

			graphics.GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			game.Draw(spriteBatch);
			spriteBatch.End();

			// Reset the device to the back buffer
			graphics.GraphicsDevice.SetRenderTarget(null);
		}
	}
}
