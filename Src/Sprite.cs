using System;
using System.Text;
using System.Xml;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Sprite
	{
		private XmlDocument doc = new XmlDocument();

		public Sprite()
		{
			LoadXml(); 
			nowState = State.Stay;
			nowFrame = 0;
			time = 0;
			Effect = SpriteEffects.None;
		}

		public SpriteEffects Effect
		{
			get;
			protected set;
		}

		public enum State {
			Stay = 0,
			Walk = 1
		}

		public const float FrameTime = 0.1f;
		public const int NbState = 2;

		public float time;

		public State state
		{
			get;
			private set;
		}

		private RectAllSprite[] rect;

		private State nowState;

		private int nowFrame;

		public void NextFrame()
		{
			nowFrame += 1;
			if (nowFrame == NumberOfSprite())
			{
				nowFrame = 0;
			}
		}

		public void ChangeState(State state)
		{
			if (state != nowState && (int)state < NbState)
			{
				nowState = state;
				nowFrame = 0;
			}	
		}

		private ControlableObject.Direction Direction;

		public void ChangeDirection(ControlableObject.Direction newdirection)
		{
			if (newdirection != Direction)
			{
				Direction = newdirection;

				switch (newdirection)
				{
					case ControlableObject.Direction.TOP:
						//
						break;

					case ControlableObject.Direction.LEFT:
						Effect = SpriteEffects.FlipHorizontally;
						break;

					case ControlableObject.Direction.RIGHT:
						Effect = SpriteEffects.None;
						break;

					case ControlableObject.Direction.BOTTOM:
						//
						break;

					case ControlableObject.Direction.NONE:
						//
						break;
				}
			}
		}

		public void UpdateFrame(GameTime gameTime)
		{
			time += (float)gameTime.ElapsedGameTime.TotalSeconds;

			while (time > FrameTime)
			{
				NextFrame();
				time = 0f;
			}
		}

		public int NumberOfSprite()
		{
			return rect[(int)nowState].nbSprites;
		}

		public Rectangle RectOfSprite()
		{
			return (rect[(int)nowState].sprites[nowFrame]).source;
		}

		public void LoadXml()
		{
			var test = GetType().Module.Assembly.GetManifestResourceStream("tim_dodge.Content.character.TimXml.xml");
			var stream = new System.IO.StreamReader(test); 

			string docs = stream.ReadToEnd();
			doc.LoadXml(docs);
			System.Xml.XmlElement all = doc.DocumentElement;

			if (all != null)
			{
				rect = new RectAllSprite[all.Attributes.Count];

				int i = 0;
				foreach (System.Xml.XmlElement child in all)
				{
					rect[i] = new RectAllSprite();

					int nbchild = 0; // How to count element of a enumerator ?
					foreach (System.Xml.XmlElement ligne in child) 
					{
						nbchild += 1;
					}

					rect[i].sprites = new RectSprite[nbchild];
					rect[i].nbSprites = nbchild;

					int j = 0;
					foreach (System.Xml.XmlElement ligne in child)
					{
						rect[i].sprites[j] = new RectSprite(ligne.Attributes);
						j += 1;
					}
					i += 1;
				}
			}
		}
	}
}
