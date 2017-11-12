using System;
using System.Text;
using System.Xml;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace tim_dodge
{
	/// <summary>
	/// Contains information to determine the area of a texture that must be shown at a given moment.
	/// Every texture areas are described in XML files.
	/// </summary>
	public class Sprite
	{
		private XmlDocument doc = new XmlDocument();

		public Sprite(string xml_path)
		{
			LoadXml(xml_path); 
			nowState = 0;
			nowFrame = 0;
			time = 0;
			Effect = SpriteEffects.None;
			Direction = Controller.Direction.BOTTOM;
		}

		public SpriteEffects Effect
		{
			get;
			protected set;
		}

		private float time;

		private RectSprite[][] rect;
		private float[] FrameTime;

		private int nowState;

		private int nowFrame;

		protected void NextFrame()
		{
			nowFrame += 1;
			if (nowFrame == NumberOfFrames())
				nowFrame = 0;
		}

		public void ChangeState(int state)
		{
			if (state != nowState)
			{
				nowState = state;
				nowFrame = 0;
			}	
		}

		public void ChangeFrame(int frame)
		{
			if (frame >= 0 && frame < NumberOfFrames())
				nowFrame = frame;
		}

		public Controller.Direction Direction
		{
			get;
			protected set;
		}

		public float GetFrameTime()
 		{
 			return FrameTime[nowState];
 		}
 
 		public float GetFrameTimeOfState(int state)
 		{
 			return FrameTime[state];
 		}

		/// <summary>
		/// Change the direction of the sprite (flip horizontally)
		/// </summary>
		/// <param name="new_dir">New direction</param>
		public void ChangeDirection(Controller.Direction new_dir)
		{
			if (new_dir != Direction)
			{
				Direction = new_dir;
				if (new_dir == Controller.Direction.RIGHT)
					Effect = SpriteEffects.None;
				if (new_dir == Controller.Direction.LEFT)
					Effect = SpriteEffects.FlipHorizontally;
			}
		}

		public void UpdateFrame(double elapsed)
		{
			time += (float)elapsed;

			while (time > GetFrameTime())
			{
				NextFrame();
				time -= GetFrameTime();
			}
		}

		public int NumberOfFrames()
		{
			return rect[(int)nowState].Length;
		}

		public int NowFrame() { return nowFrame; }
		public int NowState() { return nowState; }

		public Rectangle RectOfSprite()
		{
			return (rect[(int)nowState][nowFrame]).source;
		}

		/// <summary>
		/// Load sprites relative to the xml file
		/// </summary>
		/// <param name="xml_path">Path to the xml file, replace / by . in the string exemple : Content.character.TimXml.xml</param>
		private void LoadXml(string xml_path)
		{
			var res = GetType().Module.Assembly.GetManifestResourceStream("tim_dodge."+xml_path);

			var stream = new System.IO.StreamReader(res); 

			string docs = stream.ReadToEnd();
			doc.LoadXml(docs);
			XmlElement all = doc.DocumentElement;

			if (all != null)
			{
				rect = new RectSprite[all.ChildNodes.Count][];
				FrameTime = new float[all.ChildNodes.Count];

				int i = 0;
				foreach (XmlNode child in all.ChildNodes)
				{
					FrameTime[i] = 0f;
					foreach (XmlAttribute val in child.Attributes)
					{
						if (val.Name == "delay")
							FrameTime[i] = Convert.ToInt32(val.Value) / 1000f;
					}

					rect[i] = new RectSprite[child.ChildNodes.Count];
					int j = 0;
					foreach (XmlNode ligne in child.ChildNodes)
					{
						rect[i][j] = new RectSprite(ligne.Attributes);
						j += 1;
					}
					i += 1;
				}
			}
		}
	}
}
