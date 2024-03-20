// Will  Smith 8657254
using MeteorManeuver.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Scenes
{
    public class HelpScene : SuperClassScene
    {
        // Initialize Background, T2D, mastercontroller:
        Background helpScreen;
        Texture2D helpBackground;
        MasterController masterController;

        public HelpScene(Game game) : base(game)
        {
            masterController = (MasterController)game;
            helpBackground = masterController.Content.Load<Texture2D>("images/HelpScene");
            helpScreen = new Background(game, masterController._spriteBatch, helpBackground, new Rectangle(0, 0, 780, 720), Vector2.Zero, Vector2.Zero);
            this.Components.Add(helpScreen);
        }

        public override void EndGame() { }
    }
}
