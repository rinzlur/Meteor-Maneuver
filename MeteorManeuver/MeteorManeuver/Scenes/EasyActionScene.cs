// Will  Smith 8657254
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeteorManeuver.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;
using MeteorManeuver.Components;

namespace MeteorManeuver.Scenes
{
    public class EasyActionScene : SuperClassScene
    {
        // Initialize MasterController:
        MasterController masterController;

        // Initialize a RNG:
        private static Random random = new Random();
        private Timer entityTimer;

        // Initialize CountDownComponent:
        private CountDownComponent countDownComponent;

        // Initialize GameTimerComponent:
        private GameTimerComponent gameTimer;

        // Initialize a NEW List of all Components (To Hide the Base Components List in SuperClass):
        public new List<GameComponent> Components;
        public List<Texture2D> Asteroids = new List<Texture2D>();
        private List<AsteroidEntity> asteroidEntities = new List<AsteroidEntity>();

        // Initialize Speed Integer for Controlling Asteroid Speed 
        private int speed = 5;

        // Iniitialize integers needed to count finalScore:
        double finalScore = 0;
        double missleScore = 0;

        // Initalize all Components:
        private SpaceShipEntity userShip;
        private AsteroidEntity asteroid;
        private MissleEntity missle;
        
        // Initialize ReadWriteFile:
        ReadWriteFile readWrite = new ReadWriteFile();

        // Public Accessors:
        public CountDownComponent CountDownComponent { get => countDownComponent; set => countDownComponent = value; }
        public List<AsteroidEntity> AsteroidEntities { get => asteroidEntities; set => asteroidEntities = value; }

        public EasyActionScene(Game game) : base(game)
        {
            Hide();

            masterController = (MasterController)game;

            Components = new List<GameComponent>();

            // Initialize a Timer that is used for entity Generation:
            entityTimer = new Timer(EntityTimerInvoke);
            entityTimer.Change(5000, GetRandomInterval());

            // Initialize SpaceShipEntity:
            Texture2D shipTex = masterController.Content.Load<Texture2D>("images/ShipSheet");
            Vector2 shipPos = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            Vector2 shipSpeed = new Vector2(7, -7);
            userShip = new SpaceShipEntity(game, masterController._spriteBatch, shipTex, shipSpeed, 7, shipPos);
            this.Components.Add(userShip);

            // Initialize Asteroid Images:
            for(int i = 1; i <= 16;  i++)
            {
                // Generate All Asteroid Textures From Content:
                Texture2D astTex = masterController.Content.Load<Texture2D>($"asteroids/Asteroid{i}");
                // Add asteroids to list
                Asteroids.Add(astTex); 
            }

            // Initialize Variables used for GameTimer, and Initialize GameTimer:
            SpriteFont gameTimerFont = game.Content.Load<SpriteFont>("fonts/GameTimerFont");
            gameTimer = new GameTimerComponent(game, masterController._spriteBatch, gameTimerFont, this);
            this.Components.Add(gameTimer);

            // Initialize Variables used for CountDown, and Initialize CountDown:
            string[] countDownItems = { "3", "2", "1", "Survive!" };
            SpriteFont countDownFont = game.Content.Load<SpriteFont>("fonts/CountDownFont");
            CountDownComponent = new CountDownComponent(game, masterController._spriteBatch, countDownFont, countDownItems);
            this.Components.Add(countDownComponent);
        }

        private void EntityTimerInvoke(object state)
        {
            if (this.Enabled && GameStart)
            {
                GameStart = true;
                // Make new asteroid object, pass it the textures:
                AsteroidEntity asteroid = new AsteroidEntity(Game, masterController._spriteBatch, Asteroids, userShip, this, gameTimer);
                // Add Asteroid to Components:
                this.Components.Add(asteroid);
            } 
        }

        private int GetRandomInterval()
        {
            // get a random interval between 3k to 5k seconds:
            return random.Next(1500, 3000);
        }

        public override void EndGame()
        {
            Debug.WriteLine("End Game Called.");

            // Stop all components, dispose all components (that are used by easyActionScene)
            foreach (DrawableGameComponent component in Components)
            {
                if (component is GameTimerComponent gameTimer)
                {
                    finalScore = gameTimer.TimerTime - gameTimer.ScoreModifier;
                    Debug.WriteLine(gameTimer.TimerTime);

                    missleScore = gameTimer.ScoreModifier;
                    Debug.WriteLine(gameTimer.ScoreModifier);
                }
            }

            try
            {
                // Remove all components:
                Components.RemoveAll(component => component is DrawableGameComponent);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error Occured Inside EndGame(): " + ex.Message);
            }

            // Hide Scene:
            this.Hide();

            // Show FinalScoreScene: --> Seconds Survived, Asteroids Destroyed, Final Score
            FinalScoreScene finalScoreScene = new FinalScoreScene(this.Game, (int)finalScore, (int)missleScore);
            masterController.Components.Add(finalScoreScene);
            finalScoreScene.Show();

            // Update Hi Scores if Needed:
            readWrite.AddNewHiScore(Shared.difficultySelection, finalScoreScene.Name, (int)finalScore + (int)missleScore);
        }

        public override void Update(GameTime gameTime)
        {
            // Iterate through all GameComponents
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] != null)
                {
                    GameComponent entity = Components[i];

                    if (entity is DrawableGameComponent)
                    {
                        // Update component only if enabled:
                        if (entity.Enabled)
                        {
                            entity.Update(gameTime);
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            try
            {
                foreach (GameComponent entity in Components)
                {
                    if(entity is DrawableGameComponent drawableEntity)
                    {
                        drawableEntity.Draw(gameTime);
                    }
                }

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error Occured: {ex.Message}");
            }
        }

    }

}
