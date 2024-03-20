// Will  Smith 8657254
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Scenes
{
    /*
     * 
     * THIS IS THE SUPERCLASS THAT ALL SCENES INHERIT FROM
     * 
     */
    public abstract class SuperClassScene : DrawableGameComponent
    {
        // Components Initializer, and GameStart Bool:
        public List<GameComponent> Components { get; set; }
        public bool GameStart;

        public SuperClassScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            Hide();
        }

        /// <summary>
        /// Hide will hide the object that is called alongside it
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        /// <summary>
        /// Hide will show the object that is called alongside it
        /// </summary>
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        /// <summary>
        /// EndGame ends the currentGame (for action scenes)
        /// </summary>
        public abstract void EndGame();

        public override void Update(GameTime gameTime)
        {
            // Iterate through all GameComponents
            foreach (GameComponent component in Components)
            {
                // Update component only if enabled:
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(GameComponent component in Components)
            {
                if(component is DrawableGameComponent)
                {
                    // If component is drawable, typecast it to DrawableGameComponent
                    DrawableGameComponent DGC = (DrawableGameComponent)component;

                    // Then, check if DGC is visible:S
                    if (DGC.Visible)
                    {
                        DGC.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
