// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver
{
    internal class Background : DrawableGameComponent
    {
        // Initialize Variables:
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position1, position2;
        private Vector2 speed;
        private Rectangle srcRect;


        public Background(Game game, SpriteBatch sb, Texture2D tex, Rectangle srcRect, Vector2 position, Vector2 speed) : base(game)
        {
            // Assign Variables:
            this.sb = sb;
            this.tex = tex;
            this.srcRect = srcRect;

            this.position1 = position;
            this.position2 = new Vector2(position1.X, position1.Y + srcRect.Width);
            
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            // Move images downwards based on speed:
            position1 += speed;
            position2 += speed;
            
            // Check if pos1 is less than the negative value of srcRect's height:
            if(position1.Y > srcRect.Height)
            {
                // Make position1.Y position equal to pos2.Y plus the srcRect's height:
                position1.Y = position2.Y - srcRect.Height; 
            }

            // Check if pos2 is less than the negative value of srcRect's height:
            if(position2.Y > srcRect.Height)
            {
                // Make pos2.Y equal to pos1.Y plus srcRect's height:
                position2.Y = position1.Y - srcRect.Height;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Begin Draw:
            sb.Begin();
            sb.Draw(tex, position1, srcRect, Color.White);
            sb.Draw(tex, position2, srcRect, Color.White);
            sb.End();
            base.Draw(gameTime);
        }

        
    }
}
