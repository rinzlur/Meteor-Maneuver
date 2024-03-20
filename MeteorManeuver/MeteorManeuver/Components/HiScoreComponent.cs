// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class HiScoreComponent : DrawableGameComponent
    {
        // SpriteBatches and SpriteFonts:
        private SpriteBatch sb;
        private SpriteFont regularFont, colouredFont;

        // List of Strings that will be seen on the StartScene:
        private List<string> menuItems;

        // Position the Menu Items:
        private Vector2 position;

        public HiScoreComponent(Game game, SpriteBatch sb, SpriteFont regularFont, SpriteFont colouredFont, List<string> menuItems, Vector2 position) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.colouredFont = colouredFont;
            this.menuItems = menuItems;
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Update tempPos:
            Vector2 tempPos = position;

            sb.Begin();

            // Draw each string inside menuItems:
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == 0)
                {
                    sb.DrawString(colouredFont, menuItems[i], tempPos, Color.Aqua);
                    tempPos.Y += 50;
                }
                else
                {
                    sb.DrawString(regularFont, menuItems[i], tempPos, Color.White);
                }

                tempPos.Y += colouredFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }
    }
}
