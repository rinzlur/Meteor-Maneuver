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
    public class TitleComponent : DrawableGameComponent
    {
        // General Parameter Initializers:
        private SpriteBatch sb;
        private SpriteFont titleFont;
        private Vector2 position;
        private Color titleColour = Color.Aqua;
        private string titleText;


        public TitleComponent(Game game, SpriteBatch sb, SpriteFont titleFont, string titleText, Vector2 position) : base(game)
        {
            this.sb = sb;
            this.titleFont = titleFont;
            this.position = position;
            this.titleText = titleText;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw String:
            sb.Begin();
            sb.DrawString(titleFont, titleText, position, titleColour);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
