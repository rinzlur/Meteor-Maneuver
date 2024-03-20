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
    public class DifficultySelectionComponent : DrawableGameComponent
    {
        // SpriteBatches and SpriteFonts:
        private SpriteBatch sb;
        private SpriteFont easyFont, mediumFont, hardFont;
        private SpriteFont easyFontSelect, mediumFontSelect, hardFontSelect;

        // List of Strings that will be seen on the StartScene:
        private List<string> menuItems;

        // Property for maintianing user selection:
        public int SelectedItem { get; set; }

        // Position the Menu Items:
        private Vector2 position;

        // Colours that will be used for the StartScene:
        private Color easyColour = Color.Green;
        private Color mediumColour = Color.Yellow;
        private Color hardColour = Color.Red;

        // KeyboardState is required to make selection possible:
        private KeyboardState oldState;

        public DifficultySelectionComponent(Game game, SpriteBatch sb, SpriteFont easyFont, SpriteFont mediumFont, SpriteFont hardFont, string[] items, SpriteFont easyFontSelect, SpriteFont mediumFontSelect, SpriteFont hardFontSelect) : base(game)
        {
            this.sb = sb;
            this.easyFont = easyFont;
            this.mediumFont = mediumFont;
            this.hardFont = hardFont;
            this.easyFontSelect = easyFontSelect;
            this.mediumFontSelect = mediumFontSelect;
            this.hardFontSelect = hardFontSelect;
            this.menuItems = items.ToList();
            // Positioning of the list:
            position = new Vector2(230, 320);

        }

        public override void Update(GameTime gameTime)
        {
            // Get Keyboard State:
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                // Increase Index:
                SelectedItem++;
                Shared.difficultySelection++;

                // Check if Selection is out of bounds:
                if (SelectedItem == menuItems.Count)
                {
                    SelectedItem = 0;
                    Shared.difficultySelection = 0;
                }
            }

            // Check if up key was previously up, but now down, then move:
            if (state.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                // Decrement Selection:
                SelectedItem--;
                Shared.difficultySelection--;

                // Check if selection is out of bounds:
                if (SelectedItem == -1)
                {
                    // Make selection loop around, and accomodate for zero based array:
                    SelectedItem = menuItems.Count - 1;
                    Shared.difficultySelection = menuItems.Count - 1;
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
            SpriteFont selection = null;

            // Begin Drawing:
            sb.Begin();

            // Iterate through menu items:
            for (int i = 0; i < menuItems.Count; i++)
            {
                // Check if the user has made a selection:
                if (SelectedItem == i)
                {
                    // If yes, select the item, using our declared variables:
                    switch (SelectedItem)
                    {
                        
                        case 0:
                            sb.DrawString(easyFontSelect, menuItems.ElementAt(i), tempPos, easyColour);
                            selection = easyFontSelect;
                            break;
                        case 1:
                            sb.DrawString(mediumFontSelect, menuItems.ElementAt(i), tempPos, mediumColour);
                            selection = mediumFontSelect;
                            break;
                        case 2:
                            sb.DrawString(hardFontSelect, menuItems.ElementAt(i), tempPos, hardColour);
                            selection = hardFontSelect;
                            break;
                        default:
                            break;
                    }
                }
                else
                {

                    switch (i)
                    {
                        case 0:
                            sb.DrawString(easyFont, menuItems.ElementAt(i), tempPos, easyColour);
                            selection = easyFont;
                            break;
                        case 1:
                            sb.DrawString(mediumFont, menuItems.ElementAt(i), tempPos, mediumColour);
                            selection = mediumFont;
                            break;
                        case 2:
                            sb.DrawString(hardFont, menuItems.ElementAt(i), tempPos, hardColour);
                            selection = hardFont;
                            break;
                        default:
                            break;
                    }
                   
                }

                // Update TempPos:
                if (selection != null)
                {
                    tempPos.X += selection.MeasureString(menuItems.ElementAt(i)).X + 30;
                }
            }

            // End Draw:
            sb.End();


            base.Draw(gameTime);
        }
    }
}
