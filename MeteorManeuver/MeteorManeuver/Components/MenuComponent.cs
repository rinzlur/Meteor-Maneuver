// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class MenuComponent : DrawableGameComponent
    {
        // SpriteBatches and SpriteFonts:
        private SpriteBatch sb;
        private SpriteFont regularFont, selectionFont;

        // List of Strings that will be seen on the StartScene:
        private List<string> menuItems;

        // Property for maintianing user selection:
        public int SelectedItem { get; set; }

        // Position the Menu Items:
        private Vector2 position;

        // Colours that will be used for the StartScene:
        private Color regularColour = Color.LightYellow;
        private Color selectionColour = Color.Aqua;

        // KeyboardState is required to make selection possible:
        private KeyboardState oldState;


        public MenuComponent(Game game, SpriteBatch sb, SpriteFont regularFont, SpriteFont selectionFont, string[] items) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.selectionFont = selectionFont;
            menuItems = items.ToList();
            position = new Vector2(310, 310);
        }

        public override void Update(GameTime gameTime)
        {
            // Get Keyboard State:
            KeyboardState state = Keyboard.GetState();

            // Check if down key was previously up but now down, then move:
            if(state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                // Increase Index:
                SelectedItem++;

                // Check if Selection is out of bounds:
                if(SelectedItem == menuItems.Count)
                {
                    SelectedItem = 0;
                }
            }

            // Check if up key was previously up, but now down, then move:
            if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                // Decrement Selection:
                SelectedItem--;

                // Check if selection is out of bounds:
                if (SelectedItem == -1)
                {
                    // Make selection loop around, and accomodate for zero based array:
                    SelectedItem = menuItems.Count - 1;
                }
            }

            // Assign state to the oldSate:
            oldState = state;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // We will use a temp variable to move to the next items upper left corner:
            Vector2 tempPos = position;
            // Begin Drawing:
            sb.Begin();

            // Iterate through menu items:
            for (int i = 0; i < menuItems.Count; i++)
            {
                // Check if the user has made a selection:
                if(SelectedItem == i) 
                {
                    // If yes, select the item, using our declared variables:
                    sb.DrawString(selectionFont, menuItems.ElementAt(i), tempPos, selectionColour);
                    // Update TempPos:
                    tempPos.Y += selectionFont.LineSpacing;
                }
                else
                {
                    // For all other non-selected items:
                    sb.DrawString(regularFont, menuItems.ElementAt(i), tempPos, regularColour);
                    // Update TempPos:
                    tempPos.Y += regularFont.LineSpacing;
                }
            }

            // End Draw:
            sb.End();

            base.Draw(gameTime);
        }

    }
}
