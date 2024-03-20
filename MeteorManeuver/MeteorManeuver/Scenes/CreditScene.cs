// Will  Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Scenes
{
    public class CreditScene : SuperClassScene
    {
        // General Parameters:
        Background CreditPage;
        Texture2D CreditBackground;
        MasterController masterController;
        Vector2 position;

        public CreditScene(Game game) : base(game)
        {
            // Assign masterController:
            masterController = (MasterController)game;

            // Load Texture:
            CreditBackground = masterController.Content.Load<Texture2D>("images/CreditScene");
            position = Vector2.Zero;

            // Make a new background object, assign variables:
            CreditPage = new Background(game, masterController._spriteBatch, CreditBackground, new Rectangle(0, 0, 780, 720), position, Vector2.Zero);
            this.Components.Add(CreditPage);
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void EndGame() { }
    }
}
