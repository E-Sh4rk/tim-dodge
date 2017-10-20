using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class MenuWindow : Item
	{
		public String Title;
		private bool ThereIsTitle { get { return Title != null; } }
		private int firstItem
		{
			get
			{
				if (ThereIsTitle)
					return 1;
				else
					return 0;
			}
		}
		public List<MenuItem> ListItems;
		private int itemNumber;
		private Color ColorHighlightSelection; // color of the selection

		private SpriteFont FontMenu { get; }
		private Color ColorTextMenu { get; }
		private SpriteFont FontTitle { get; }
		private Color ColorTitle { get; }

		// Proportionnality constant (proportional to the size of the item)
		public const float BackgroundBordureX = 1f / 4f;
		public const float BackgroundBordureY = 1f / 2f;
		public const float SpacingBetweenItems = 1f / 4f;

		public MenuWindow(MenuManager MenuManager) : base(MenuManager.BackgroundMenu)
		{
			ListItems = new List<MenuItem>();
			//Background = new Item(GameManager.BackgroundMenu);

			FontMenu = MenuManager.FontMenu;
			ColorTextMenu = MenuManager.ColorTextMenu;
			ColorHighlightSelection = MenuManager.ColorHighlightSelection;

			FontTitle = MenuManager.FontTitleMenu;
			ColorTitle = MenuManager.ColorTitleMenu;
		}

		public void ConstructMenu()
		{
			itemNumber = firstItem;

			if (ThereIsTitle)
			{
				// Put the title on the top of the list
				List<MenuItem> TitleItem = new List<MenuItem> { new MenuItem(Title, FontTitle, ColorTitle) };
				TitleItem.AddRange(ListItems);
				ListItems = TitleItem;
			}

			SetBackground(BackgroundBordureX, BackgroundBordureY, SpacingBetweenItems);
			AlignItems(BackgroundBordureY, SpacingBetweenItems);
		}

		public void SetBackground(float BackgroundBordureX, float BackgroundBordureY, float SpacingBetweenItems)
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

		public void AlignItems(float BackgroundBordureY, float SpacingBetweenItems)
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
			ListItems[itemNumber].Color = ColorHighlightSelection;
		}

		public void Update()
		{
			if (Controller.KeyPressed(Keys.Enter))
			{
				GameManager.sounds.playSound(Sound.SoundName.toogle);
				ListItems[itemNumber].LaunchSelection();
			}

			if (Controller.KeyPressed(Keys.Down))
			{
				GameManager.sounds.playSound(Sound.SoundName.menu);
				itemNumber++;
			}

			if (Controller.KeyPressed(Keys.Up))
			{
				GameManager.sounds.playSound(Sound.SoundName.menu);
				itemNumber--;
			}
			if (itemNumber < firstItem)
				itemNumber = ListItems.Count - 1;
			if (itemNumber >= ListItems.Count)
				itemNumber = firstItem;

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
