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
		public Texture(Texture2D texture)
		{
			Image = texture;
			computeMask();
		}

		protected Texture2D image = null;
		public Texture2D Image
		{
			get
			{
				return image;
			}
			protected set
			{
				image = value;
				mask = null;
			}
		}
		protected BitArray mask = null;
		public BitArray Mask
		{
			get
			{
				if (mask == null)
					computeMask();
				return mask;
			}
			protected set
			{
				mask = value;
			}
		}

		protected void computeMask()
		{
			Color[] T = new Color[image.Width * image.Height];
			image.GetData(T);
			// Mask = new BitArray(T.Select(c => c != new Color(0,0,0,0)).ToArray());
			Mask = new BitArray(T.Select(c => c.A != 0).ToArray());
		}

		/// <summary>
		/// Return a boolean which correspond to have a object or not
		/// </summary>
		/// <param name="i">Number of ligne</param>
		/// <param name="j">Number of columns</param>
		public bool getAlpha(int i, int j)
		{
			return Mask[i * image.Width + j];
		}
	}
}
