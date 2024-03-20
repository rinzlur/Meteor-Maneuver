// Will  Smith 8657254
using MeteorManeuver.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Scenes
{
    public class StartScene : SuperClassScene
    {
        // Declare Components:
        private MenuComponent menu;
        private TitleComponent title;

        // Accessibility:
        public MenuComponent Menu { get => menu; set => menu = value; }
        public TitleComponent Title { get => title; set => title = value; }

        public StartScene(Game game) : base(game)
        {
            // Unpack Game, typcast it to MasterController:
            MasterController masterController = (MasterController)game;

            // Provide MenuItems with strings:
            string[] menuItems = { " Play Game" , "   Help", " Hi-Scores", "  Credits", "   Exit" };

            // Initialize Fonts:
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont selectedFont = game.Content.Load<SpriteFont>("fonts/SelectedFont");
            SpriteFont titleFont = game.Content.Load<SpriteFont>("fonts/TitleFont");

            // Create a new instance of MenuComponent, provide it variables:
            Menu = new MenuComponent(game, masterController._spriteBatch, regularFont, selectedFont, menuItems);
            this.Components.Add(Menu);

            // create a new instance of titleComponent, provide variables:
            Title = new TitleComponent(game, masterController._spriteBatch, titleFont, "Meteor Maneuver", new Vector2(180,90));
            this.Components.Add(Title);

        }

        public override void EndGame() { }
    }
}
