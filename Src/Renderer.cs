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
				    TimGame.GAME_WIDTH,
				    TimGame.GAME_HEIGHT);
		}

		float angle = 0;
		float pas = -0.007f;
		const float max = (float) Math.PI / 16;	

		public void Draw(SpriteBatch spriteBatch, GameManager gm)
		{			
			Rend(spriteBatch, gm);
			
			graphics.GraphicsDevice.Clear(Color.Black);

			float w = ((Texture2D)renderTarget).Width;
			float h = ((Texture2D)renderTarget).Height;
			float diag = (float)Math.Sqrt(w * w + h * h);

			Vector2 middle = new Vector2(w / 2.0f, h / 2.0f);

			float proportion = h / w;
			float scale = (float) Math.Abs((h / (Math.Cos(Math.Atan2(w,h)-angle)*diag)));
			scale = Math.Min(scale,(float)Math.Abs((h / (Math.Cos(Math.Atan2(w, h) - angle + 2 * Math.Atan2(h, w)) * diag))));

			bool rotation = false;
			SpriteEffects effect = SpriteEffects.None;

			try
			{
				if (!gm.MenuRunning)
				{
					foreach (Player p in gm.gi.players)
					{
						rotation |= p.rotation;
						if (p.flipH)
							effect = SpriteEffects.FlipHorizontally;
						else if (p.flipV)
							effect = SpriteEffects.FlipVertically;
						else
							effect = SpriteEffects.None;
					}
				}
			}
			catch { angle = 0; scale = 1;}

			if (!rotation && Math.Abs(angle) < Math.Pow(10, -3))
			{
				angle = 0;
				scale = 1;
			}
			else
			{
				angle += pas;
				if (angle > max)
					pas = -pas;
				if (angle < -max)
					pas = -pas;
			}

			scale /= TimGame.general_scale;

			spriteBatch.Begin();
			spriteBatch.Draw((Texture2D)renderTarget, middle/TimGame.general_scale, null, Color.White,
			                 angle, middle,
							 scale, effect, 0);
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
