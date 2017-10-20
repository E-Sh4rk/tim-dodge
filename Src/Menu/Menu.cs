using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Menu
	{
		protected String Title;
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
		protected List<MenuItem> ListItems;
		private Item Background;
		private int itemNumber;
		private Color ColorHighlightSelection; // color of the selection

		public SpriteFont FontMenu { get; }
		public Color ColorTextMenu { get; }
		public SpriteFont FontTitle { get; }
		public Color ColorTitle { get; }

		public Menu(GameManager GameManager)
		{
			ListItems = new List<MenuItem>();
			Background = new Item(GameManager.BackgroundMenu);

			FontMenu = GameManager.FontMenu;
			ColorTextMenu = GameManager.ColorTextMenu;
			ColorHighlightSelection = GameManager.ColorHighlightSelection;

			FontTitle = GameManager.FontTitleMenu;
			ColorTitle = GameManager.ColorTitleMenu;
		}

		protected void ConstructMenu()
		{
			itemNumber = firstItem;

			// Proportionnality constant (proportional to the size of the item)
			float BackgroundBordureX = 1f / 4f;
			float BackgroundBordureY = 1f / 2f;
			float SpacingBetweenItems = 1f / 4f;

			if (ThereIsTitle)
			{
				// Put the title on the top of the list
				List<MenuItem> TitleItem = new List<MenuItem>();
				TitleItem.Add(new MenuItem(Title, this));
				TitleItem.AddRange(ListItems);
				ListItems = TitleItem;
			}

			SetBackground(BackgroundBordureX, BackgroundBordureY, SpacingBetweenItems);
			AlignItems(BackgroundBordureY, SpacingBetweenItems);
		}

		private void SetBackground(float BackgroundBordureX, float BackgroundBordureY, float SpacingBetweenItems)
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

			Background.Size = new Vector2(Width, Height);
			Background.Position = new Vector2(X, Y); // menu at the center of the window
			Background.Opacity = 0.5f;
		}

		private void AlignItems(float BackgroundBordureY, float SpacingBetweenItems)
		{   // Calculates and positions the items of the menu
			float currentY = BackgroundBordureY * ListItems[0].Size.Y;
			int i;
			for (i = 0; i < ListItems.Count; i++)
			{
				MenuItem item = ListItems[i];
				item.Origin = Background.Position;

				float X = (Background.Size.X - item.Size.X) / 2; // center item in the menu
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

		public void Draw(SpriteBatch spriteBatch)
		{
			Background.Draw(spriteBatch);
			foreach (MenuItem item in ListItems)
				item.Draw(spriteBatch);
		}

	}
}
