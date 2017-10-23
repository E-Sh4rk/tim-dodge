using System;

using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class KeyboardReader
	{

		public string Text { get; set; }
		private int SizeMax;

		public KeyboardReader(int SizeMax)
		{
			Text = string.Empty;
			this.SizeMax = SizeMax;
		}

		public void Update()
		{
			Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

			foreach (Keys key in pressedKeys)
			{
				if (Controller.KeyPressed(key))
				{
					if (key == Keys.Back && Text.Length > 0)
					{
						Text = Text.Remove(Text.Length - 1, 1);
					}
					else if (key == Keys.Space)
					{
						if (Text.Length < SizeMax)
							Text = Text.Insert(Text.Length, " ");
					}

					else
					{
						string keyString = key.ToString();
						bool isUpperCase = (Array.Exists(pressedKeys, k => k == Keys.RightShift)) ||
							(Array.Exists(pressedKeys, k => k == Keys.LeftShift));
						
						if (keyString.Length == 1) // to write only letters
						{
							if (Text.Length < SizeMax)
								Text += isUpperCase ? keyString.ToUpper() : keyString.ToLower();
						}
					}
				}
			}
		}
	}
}
