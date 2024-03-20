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
    public class HackedComponent : DrawableGameComponent
    {
        // General Parameter Initializers:
        private SpriteBatch sb;
        private SpriteFont titleFont;
        private Vector2 position;
        private string hackText;
        private int counter = 0;
        private bool drawText;

        public HackedComponent(Game game, SpriteBatch sb, SpriteFont titleFont, Vector2 position, string hackText) : base(game)
        {
            this.sb = sb;
            this.titleFont = titleFont;
            this.position = position;
            this.hackText = hackText;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // check boolstate:
            if (drawText)
            {
                // Draw String:
                sb.Begin();
                sb.DrawString(titleFont, hackText, position, Color.Red);
                sb.End();

                // increment counter:
                counter++;

                // Reset counter on 10 ticks:
                if(counter == 10)
                {
                    drawText = false;
                    counter = 0;
                }

            }
            else
            {
                // Increment counter:
                counter++;

                // Reset counter after 10 ticks:
                if(counter == 10)
                {
                    drawText = true;
                    counter = 0;
                }
            }

            base.Draw(gameTime);
        }
    }
}
