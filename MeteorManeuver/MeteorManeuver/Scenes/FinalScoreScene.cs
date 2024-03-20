// Will  Smith 8657254
using MeteorManeuver.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;


namespace MeteorManeuver.Scenes
{
    public class FinalScoreScene : SuperClassScene
    {
        // General Initializers:
        private FinalScoreComponent scoreComponent;
        private TitleComponent titleComponent;
        
        // Initialize Form1 and Name variable:
        private UserInput.Form1 UserInputForm;
        private string name;

        // Public Accessors:
        public string Name { get => name; set => name = value; }

        public FinalScoreScene(Game game, int finalScore, int missleScore) : base(game)
        {
            // Unpack Game, typcast it to MasterController:
            MasterController masterController = (MasterController)game;

            // 'Game Over' Title Component Code:
            
            // Load Font:
            SpriteFont titleFont = game.Content.Load<SpriteFont>("fonts/TitleFont");
            // Create new Instance:
            titleComponent = new TitleComponent(game, masterController._spriteBatch, titleFont, "Game Over!", new Vector2(235, 90));
            // Add To Components:
            this.Components.Add(titleComponent);

            // Score Component Code;

            // Initialize Array:
            string[] items = { $"Time Alive: {finalScore}", $"Missile Score: {missleScore}", $"Total Score: {finalScore + missleScore}"};
            // Initialize Font:
            SpriteFont finalScoreFont = game.Content.Load<SpriteFont>("fonts/FinalScoreFont");
            // Create new Instance of FinalScoreComponent:
            scoreComponent = new FinalScoreComponent(game, masterController._spriteBatch, finalScoreFont, items, new Vector2(190, 220));
            // Add to components:
            this.Components.Add(scoreComponent);

            // Initialize Bottom Screen Text:
            SpriteFont bottomFont = game.Content.Load<SpriteFont>("fonts/GameTimerFont");
            titleComponent = new TitleComponent(game, masterController._spriteBatch, bottomFont, "Press Space To Continue, or Esc to Exit", new Vector2(75, 670));
            this.Components.Add(titleComponent);    

            this.Enabled = true;
            this.Visible = true;

            // Show userinput field:
            UserInputForm = new UserInput.Form1();
            UserInputForm.ShowDialog();
            name = UserInputForm.GetUserInput();

            // Check if name is null:
            if(string.IsNullOrWhiteSpace(name))
            {
                name = "Unknown";
            }

            Debug.WriteLine(name);
        }

        public override void EndGame(){}
    }
}
