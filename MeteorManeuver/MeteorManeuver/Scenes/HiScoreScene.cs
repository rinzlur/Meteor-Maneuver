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
    public class HiScoreScene : SuperClassScene
    {
        MasterController game;
        private HiScoreComponent easyHiScore;
        private HiScoreComponent mediumHiScore;
        private HiScoreComponent hardHiScore;
        private ReadWriteFile readwrite = new ReadWriteFile();

        public HiScoreScene(Game game) : base(game)
        {
            // Unpack Game, typcast it to MasterController:
            this.game = (MasterController)game;

            // Initialize Fonts Needed for HiScoreScene:
            SpriteFont easyFont = game.Content.Load<SpriteFont>("fonts/EasyFontSelect");
            SpriteFont mediumFont = game.Content.Load<SpriteFont>("fonts/MediumFontSelect");
            SpriteFont hardFont = game.Content.Load<SpriteFont>("fonts/HardFontSelect");
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/GameTimerFont");

            // Initialize and Seed the List that hold the hiScore data:
            List<string> easyHiScores = readwrite.GetListData("EasyHiScores.txt");
            List<string> mediumHiScores = readwrite.GetListData("MediumHiScores.txt");
            List<string> hardHiScores = readwrite.GetListData("HardHiScores.txt");

            // Initialize the HiScoreComponents:
            easyHiScore = new HiScoreComponent(game, this.game._spriteBatch, regularFont, easyFont, easyHiScores, new Vector2(50, 220));
            mediumHiScore = new HiScoreComponent(game, this.game._spriteBatch, regularFont, mediumFont, mediumHiScores, new Vector2(300, 220));
            hardHiScore = new HiScoreComponent(game, this.game._spriteBatch, regularFont, hardFont, hardHiScores, new Vector2(550, 220));

            // Add Components to Comonents List:
            this.Components.Add(easyHiScore);
            this.Components.Add(mediumHiScore);
            this.Components.Add(hardHiScore);
        }

        public void UpdateHiScores()
        {
            // Remove all components and reinitialize them:    
            this.Components.Clear();

            // Initialize Fonts Needed for HiScoreScene:
            SpriteFont easyFont = game.Content.Load<SpriteFont>("fonts/EasyFontSelect");
            SpriteFont mediumFont = game.Content.Load<SpriteFont>("fonts/MediumFontSelect");
            SpriteFont hardFont = game.Content.Load<SpriteFont>("fonts/HardFontSelect");
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/GameTimerFont");

            // Initialize and Seed the List that hold the hiScore data:
            List<string> easyHiScores = readwrite.GetListData("EasyHiScores.txt");
            List<string> mediumHiScores = readwrite.GetListData("MediumHiScores.txt");
            List<string> hardHiScores = readwrite.GetListData("HardHiScores.txt");

            // Initialize the HiScoreComponents:
            easyHiScore = new HiScoreComponent(game, game._spriteBatch, regularFont, easyFont, easyHiScores, new Vector2(50, 220));
            mediumHiScore = new HiScoreComponent(game, game._spriteBatch, regularFont, mediumFont, mediumHiScores, new Vector2(270, 220));
            hardHiScore = new HiScoreComponent(game, game._spriteBatch, regularFont, hardFont, hardHiScores, new Vector2(550, 220));

            // Add Components to Comonents List:
            this.Components.Add(easyHiScore);
            this.Components.Add(mediumHiScore);
            this.Components.Add(hardHiScore);
        }

        public override void EndGame() { }
    }
}
