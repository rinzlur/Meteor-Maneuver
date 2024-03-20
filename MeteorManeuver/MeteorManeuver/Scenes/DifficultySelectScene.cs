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
    public class DifficultySelectScene : SuperClassScene
    {
        // Initalize components:
        private DifficultySelectionComponent difficulty;
        private DifficultyHelpComponent difficultyHelp;

        // Public Accessor:
        public DifficultySelectionComponent Difficulty { get => difficulty; set => difficulty = value; }

        public DifficultySelectScene(Game game) : base(game)
        {
            // Unpack Game, typcast it to MasterController:
            MasterController masterController = (MasterController)game;

            // Initialize MenuItems with data:
            string[] menuItems = { "Easy", "Medium", "Hard" };

            // Initalize all fonts:
            SpriteFont easyFont = game.Content.Load<SpriteFont>("fonts/EasyFont");
            SpriteFont mediumFont = game.Content.Load<SpriteFont>("fonts/MediumFont");
            SpriteFont hardFont = game.Content.Load<SpriteFont>("fonts/HardFont");
            SpriteFont easyFontSelect = game.Content.Load<SpriteFont>("fonts/EasyFontSelect");
            SpriteFont mediumFontSelect = game.Content.Load<SpriteFont>("fonts/MediumFontSelect");
            SpriteFont hardFontSelect = game.Content.Load<SpriteFont>("fonts/HardFontSelect");

            // New Instance of DifficultySelectionComponent:
            difficulty = new DifficultySelectionComponent(game, masterController._spriteBatch, easyFont, mediumFont, hardFont ,menuItems, easyFontSelect, mediumFontSelect, hardFontSelect);
            this.Components.Add(difficulty);

            // Initialize DifficultyHelpComponent:
            Texture2D helpTex = game.Content.Load<Texture2D>("images/DifficultySelectionSheet");
            difficultyHelp = new DifficultyHelpComponent(Game, masterController._spriteBatch, new Vector2(0, 480), helpTex);
            this.Components.Add(difficultyHelp);
        }

        public override void EndGame() { }
    }
}
