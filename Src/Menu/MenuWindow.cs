using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	/// <summary>
	/// Represents a menu window.
	/// </summary>
	public class MenuWindow : Item
	{
		public String Title;
		private bool ThereIsTitle { get { return Title != null; } }

		public List<MenuItem> ListItems;
		public List<MenuItem> ListSelectableItems;
		public int itemNumber;

		// Proportionnality constant (proportional to the size of the item)
		public const float BackgroundBordureX = 1f / 4f;
		public const float BackgroundBordureY = 1f / 2f;
		public const float SpacingBetweenItems = 1f / 4f;

		public MenuWindow() : base(Load.BackgroundMenu)
		{
			ListItems = new List<MenuItem>();
		}

		public void ConstructMenu()
		{
			if (ThereIsTitle)
			{
				// Put the title on the top of the list
				List<MenuItem> TitleItem = new List<MenuItem> { new MenuItem(Title, Load.FontTitleMenu, Load.ColorTitleMenu) };
				TitleItem.AddRange(ListItems);
				ListItems = TitleItem;
			}

			SetBackground();
			AlignItems();

			ListSelectableItems = ListItems.FindAll(item => item.Selectable);
		}

		public void SetBackground()
		{	// Calculates and set the dimensions of the menu's background
			float MaxLengthItem = 0;
			foreach (MenuItem item in ListItems)
				MaxLengthItem = Math.Max(MaxLengthItem, item.Size.X);

			float Width = ((2 * BackgroundBordureX) + 1) * MaxLengthItem;

			float Height = 0;
			foreach (MenuItem item in ListItems)
				Height += item.Size.Y * (1 + SpacingBetweenItems); // heigth of menuItem + interval
			Height += ListItems[0].Size.Y * BackgroundBordureY 
			                      + ListItems.Last().Size.Y * (BackgroundBordureY - SpacingBetweenItems); // Top bordure

			float X = (TimGame.WINDOW_WIDTH - Width) / 2;
			float Y = (TimGame.WINDOW_HEIGHT - Height) / 2;

			Size = new Vector2(Width, Height);
			Position = new Vector2(X, Y); // menu at the center of the window
			Opacity = 0.5f;
		}

		public void AlignItems()
		{   
			// Calculates and positions the items of the menu
			float currentY = BackgroundBordureY * ListItems[0].Size.Y;
			int i;
			for (i = 0; i < ListItems.Count; i++)
			{
				MenuItem item = ListItems[i];
				item.Origin = Position;

				float X = (Size.X - item.Size.X) / 2; // center item in the menu
				item.Position = new Vector2(X, currentY);

				float heightItem = item.Size.Y;
				float interval = SpacingBetweenItems * heightItem;

				currentY += heightItem + interval;
			}
		}

		public void HighlightsCurrentItem()
		{
			foreach (MenuItem item in ListItems)
				item.unsetColor();
			ListSelectableItems[itemNumber].Color = Load.ColorHighlightSelection;
		}

		public void Update()
		{
			if (Controller.KeyPressed(Keys.Enter))
			{
				Load.sounds.playSound(Sound.SoundName.toogle);
				ListSelectableItems[itemNumber].LaunchSelection();
			}

			if (Controller.KeyPressed(Keys.Down))
			{
				Load.sounds.playSound(Sound.SoundName.menu);
				itemNumber++;
			}

			if (Controller.KeyPressed(Keys.Up))
			{
				Load.sounds.playSound(Sound.SoundName.menu);
				itemNumber--;
			}
			if (itemNumber < 0)
				itemNumber = ListSelectableItems.Count - 1;
			if (itemNumber >= ListSelectableItems.Count)
				itemNumber = 0;

			HighlightsCurrentItem();
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			foreach (MenuItem item in ListItems)
				item.Draw(spriteBatch);
		}

	}
}
