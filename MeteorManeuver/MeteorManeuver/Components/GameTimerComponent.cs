// Will Smith 8657254
using MeteorManeuver.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class GameTimerComponent : DrawableGameComponent
    {
        // Initialize Timer Parameters:
        private Stopwatch gameTimer = new Stopwatch();
        
        // Bool used to await timer until countdown ends:
        private bool timerStarted = false;
        
        // The text of the timer:
        string timerText;
        
        // Variable that holds the 'score':
        int timerTime = 0;
        
        // Displacer stopwatch for countdown:
        private Stopwatch gameTimerDisplacer = new Stopwatch();
        
        // Scene Initializer:
        public SuperClassScene scene;
        
        // Long Variable used to Interpolate the score:
        private long scoreModifier;

        // Initialize Sprite Parameters:
        private SpriteBatch sb;
        private SpriteFont gameTimerFont;
        private Vector2 position;
        private Color colour = Color.White;

        // Public Accessors:
        public long ScoreModifier { get => scoreModifier; set => scoreModifier = value; }
        public int TimerTime { get => timerTime; set => timerTime = value; }

        public GameTimerComponent(Game game, SpriteBatch sb, SpriteFont gameTimerFont, SuperClassScene scene) : base(game)
        {
            // Assign Variables:
            this.sb = sb;
            this.gameTimerFont = gameTimerFont;
            this.position = new Vector2(15, 680);

            // Start Displacer
            gameTimerDisplacer.Start();

            if(Shared.difficultySelection == 0)
            {
                this.scene = (EasyActionScene)scene;
            }
            else if (Shared.difficultySelection == 1)
            {
                this.scene = (MediumActionScene)scene;
            }
            else if (Shared.difficultySelection == 2)
            {
                this.scene = (HardActionScene)scene;
            }

            Debug.WriteLine("Inside GameTimerComponent Constructor");
        }

        /// <summary>
        /// Enables the Timer
        /// </summary>
        private void EnableTimer()
        {
            Debug.WriteLine("Timer Enabled");
            TimerCallback callBack = new TimerCallback(GameTimerCallback);
            gameTimer = new Stopwatch();
            gameTimer.Start();
        }


        public override void Update(GameTime gameTime)
        {
            // Check if timer is stopped:
            if(!timerStarted)
            {
                // Check if 4 seconds has passed:
                if(gameTimerDisplacer.ElapsedMilliseconds >= 4000 && scene.GameStart) 
                {
                    // Start Generation:
                    EnableTimer();
                    timerStarted = true;
                }
            }

            // Increment score:
            TimerTime = ((int)gameTimer.ElapsedMilliseconds / 10) + (int)scoreModifier ;

            // Create Score string:
            timerText = ($"Score: {TimerTime}");

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw Score String:
            sb.Begin();
            sb.DrawString(gameTimerFont, timerText, position, colour);
            sb.End();

            base.Draw(gameTime);
        }

        // Redundant, nothing needs to happen on this call:
        private void GameTimerCallback(object call) { }

    }
}
