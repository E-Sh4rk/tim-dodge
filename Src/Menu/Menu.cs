using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Menu
	{
		public List<MenuItem> ListItems;
		private Item Background;
		private int itemNumber;
		private Color ColorHighlightSelection; // color of the selection

		public SpriteFont FontMenu { get; }
		public Color ColorTextMenu { get; }

		public Menu(GameManager GameManager)
		{
			ListItems = new List<MenuItem>();
			Background = new Item(GameManager.BackgroundMenu);
			itemNumber = 0;

			FontMenu = GameManager.FontMenu;
			ColorTextMenu = GameManager.ColorTextMenu;
			ColorHighlightSelection = GameManager.ColorHighlightSelection;
		}

		protected void ConstructMenu()
		{           
			// Proportionnality constant (proportional to the size of the item)
			float BackgroundBordureX = 1f / 4f;
			float BackgroundBordureY = 1f / 2f;
			float SpacingBetweenItems = 1f / 4f;

			SetBackground(BackgroundBordureX, BackgroundBordureY, SpacingBetweenItems);
			AlignItems(BackgroundBordureY, SpacingBetweenItems);
		}

		private void SetBackground(float BackgroundBordureX, float BackgroundBordureY, float SpacingBetweenItems)
		{	// Calculates and set the dimensions of the menu's background
			float MaxLengthItem = 0;
			foreach (MenuItem item in ListItems)
				MaxLengthItem = Math.Max(MaxLengthItem, item.source.Size.X);

			float heightItem = ListItems[0].source.Size.Y;

			float Width = ((2 * BackgroundBordureX) + 1) * MaxLengthItem;
			float Height = (2 * BackgroundBordureY) * heightItem 
				+ (heightItem * ListItems.Count)
				+ (SpacingBetweenItems * heightItem) * (ListItems.Count - 1); 

			float X = (TimGame.WINDOW_WIDTH - Width) / 2;
			float Y = (TimGame.WINDOW_HEIGHT - Height) / 2;

			Background.Size = new Vector2(Width, Height);
			Background.Position = new Vector2(X, Y); // menu at the center of the window
			Background.Opacity = 0.5f;
		}

		private void AlignItems(float BackgroundBordureY, float SpacingBetweenItems)
		{	// Calculates and positions the items of the menu
			foreach (MenuItem item in ListItems)
				item.Origin = Background.Position;

			float heightItem = ListItems[0].source.Size.Y;
			float interval = SpacingBetweenItems * heightItem;

			for (int i = 0; i < ListItems.Count; i++)
			{
				MenuItem item = ListItems[i];
				float X = (Background.source.Size.X - item.source.Size.X) / 2; // center item in the menu
				float Y = (BackgroundBordureY * heightItem) + (i * interval) + (i * heightItem); // position of the item
				item.Position = new Vector2(X, Y);
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
				ListItems[itemNumber].LaunchSelection();

			if (Controller.KeyPressed(Keys.Down))
				itemNumber++;

			if (Controller.KeyPressed(Keys.Up))
				itemNumber--;

			if (itemNumber < 0)
				itemNumber = 0;
			if (itemNumber >= ListItems.Count)
				itemNumber = ListItems.Count - 1;

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
