using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace tim_dodge
{
	public class Texture
	{
		public Texture()
		{
		}

		public Texture2D Image;
		public BitArray Mask;

		public void getBitmap()
		{
			Color[] T = new Color[Image.Width * Image.Height];
			Image.GetData(T);
			Mask = new BitArray(T.Select(c => c != new Color(0,0,0,0)).ToArray());
			// Mask = new BitArray(T.Select(c => c.A != 0).ToArray());
		}

		/// <summary>
		/// Return a boolean which correspond to have a object or not
		/// </summary>
		/// <param name="i">Number of ligne</param>
		/// <param name="j">Number of columns</param>
		public bool getAlpha(int i, int j)
		{
			return Mask[i * Image.Width + j];
		}

		/// <summary>
		/// Return a boolean which correspond to have a object or not
		/// </summary>
		/// <param name="i">Number of ligne</param>
		/// <param name="j">Number of columns</param>
		/// <param name="x">Offsets of ligne</param>
		/// <param name="y">Offsets of columns</param>
		public bool getAlpha(int i, int j, int x, int y)
		{
			return Mask[(i + x) * Image.Width + j + y];
		}
	}
}
