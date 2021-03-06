﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace tim_dodge
{
	/// <summary>
	/// Represents a texture (an image loaded into memory). For each texture, a collision mask is computed.
	/// </summary>
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
        // mask for collision
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
        /// <summary>
        /// Com
        /// </summary>
		protected void computeMask()
		{
			Color[] T = new Color[image.Width * image.Height];
			image.GetData(T);
			// Mask = new BitArray(T.Select(c => c != new Color(0,0,0,0)).ToArray());
			Mask = new BitArray(T.Select(c => c.A != 0).ToArray());
		}
	}
}
