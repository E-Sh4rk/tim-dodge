using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace tim_dodge
{
	public class RectSprite
	{
		public Rectangle source
		{
			get;
			protected set;
		}

		public RectSprite(System.Xml.XmlAttributeCollection ligne)
		{
			int[] att = new int[] { 0, 0, 0, 0 };
			int i = 0;
			foreach (System.Xml.XmlAttribute elem in ligne)
			{
				if (i < 4) // Just the first 4 items are usefull
				{
					att[i] = Convert.ToInt32(elem.Value);
					i += 1;
				}
			}
			source = new Rectangle(att[1], att[2], att[0], att[3]);
		}
	}
}
