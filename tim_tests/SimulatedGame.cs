using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tim_tests
{
	public class SimulatedGame : Game
	{
		GraphicsDeviceManager gdm = null;
		public SimulatedGame()
		{
			gdm = new GraphicsDeviceManager(this);
		}

		public Texture2D textureFromStream(Stream stream)
		{
			return Texture2D.FromStream(GraphicsDevice, stream);
		}
	}
}
