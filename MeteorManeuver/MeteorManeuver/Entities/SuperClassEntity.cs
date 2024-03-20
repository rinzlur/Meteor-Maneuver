// Will  Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Entities
{
    /*
     * 
     * THIS IS THE SUPERCLASS THAT ALL ENTITIES INHERIT FROM 
     * 
     */

    public abstract class SuperClassEntity : DrawableGameComponent
    {
        // List of all parameters that every entity requires:
        protected SpriteBatch sb;
        protected Texture2D tex;
        protected Vector2 position;
        protected Vector2 speed;
        protected Vector2 stage;

        protected SuperClassEntity(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            stage = Shared.stage;
        }

        /// <summary>
        /// Getter Method Used to Locate and Get Dimensions of an Object
        /// </summary>
        /// <returns>Returns a rectangle, which contains the position, and the dimensions of the object</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position, Color.White);
            sb.End();
            base.Draw(gameTime);
        }



    }
}
