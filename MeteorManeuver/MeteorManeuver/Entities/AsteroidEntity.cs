// Will  Smith 8657254
using MeteorManeuver.Components;
using MeteorManeuver.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Entities
{
    public class AsteroidEntity : DrawableGameComponent
    {
        // General Intitializers
        public SpriteBatch sb;
        public Texture2D tex;
        public Vector2 position;
        public Vector2 speed;
        MasterController masterController;

        // List of Asteroid textures:
        public List<Texture2D> asteroids;

        // User ship variable:
        public SpaceShipEntity userShip;

        // Scene:
        public SuperClassScene scene;
        
        // Collision and Generation Params:
        public int collisionCount = 0;
        public GameTimerComponent gameTimer;
        List<MissleEntity> missles = new List<MissleEntity>();
        SoundEffect explosion;
        private static Random random = new Random();

        public AsteroidEntity(Game game, SpriteBatch sb, List<Texture2D> Asteroids, SpaceShipEntity userShip, SuperClassScene scene, GameTimerComponent gameTimer) : base(game)
        { 
            masterController = (MasterController)game;
            this.sb = sb;
            this.tex = GetNewTex(Asteroids); 
            this.position = GetNewPosition();
            this.speed = GetNewSpeed();
            this.asteroids = Asteroids;
            this.userShip = userShip;
            this.gameTimer = gameTimer;
            explosion = masterController.Content.Load<SoundEffect>("sounds/explosion1");
            
            // Check difficulty state, and assign scene appropriately:
            if (Shared.difficultySelection == 0)
            {
                this.scene = scene as EasyActionScene;
            }
            else if (Shared.difficultySelection == 1)
            {
                this.scene = scene as MediumActionScene;
            }
            else if (Shared.difficultySelection == 2)
            {
                this.scene = (HardActionScene)scene;
            }
        }

        /// <summary>
        /// Gets a random texture from the list of asteroid textures:
        /// </summary>
        /// <param name="asteroids">List of T2D Asteroids</param>
        /// <returns>Returns a Texture2D asteroid texture</returns>
        private Texture2D GetNewTex(List<Texture2D> asteroids)
        {
            // CHANGE THESE TO CONSTANTS:
            int index = random.Next(0, asteroids.Count);
            return asteroids[index];
        }

        /// <summary>
        /// Gets a new starting point for the newly generated asteroid:
        /// </summary>
        /// <returns>A randomized V2D variable</returns>
        private Vector2 GetNewPosition()
        {
            // Initalize the bounds of the screen:
            float boundsX = Shared.stage.X;
            float boundsY = Shared.stage.Y;
            // Generate the starting position of the asteroid:
            float x = ((float)random.Next(3, 7) * 0.1f) * boundsX;

            float y = -(float)random.NextDouble() * boundsY;

            return new Vector2(x, y);

        }

        /// <summary>
        /// Gets a V2D variable
        /// </summary>
        /// <returns>A Vector2D variable that is randomized</returns>
        private Vector2 GetNewSpeed()
        {
            if(this.position.X < Shared.stage.X / 2)
            {
                float speedX = random.Next(-2, 0);
                float speedY = random.Next(7, 11);
                return new Vector2(speedX, speedY);

            }
            else
            {
                float speedX = random.Next(0, 2);
                float speedY = random.Next(7, 11);

                return new Vector2(speedX, speedY);
            }
           
        }

        /// <summary>
        /// Check for Collisions Between object and other entities
        /// </summary>
        public void CheckCollision()
        {
            // Get Ship and Missle Position/Rectangle: 
            Rectangle shipBounds = userShip.GetBounds();
            foreach(DrawableGameComponent entity in masterController.Components)
            {
                if(entity is SuperClassEntity)
                {
                    if(entity is MissleEntity missle)
                    {    
                        // Add Missle to List:
                        missles.Add(missle);
                    }
                   
                }
                
            }
            // Reassign the shipbounds to properly match sprite (without this, the bounds is the entire size of the spritesheet):
            shipBounds.Width = 50;
            shipBounds.Height = 50;

            // ensure the rectange accurately depict the location of the texture:
            shipBounds.X = (int)userShip.Position.X;
            shipBounds.Y = (int)userShip.Position.Y;

            // Get the bounds of the asteroid:
            Rectangle asteroidSize = this.GetBounds();

            // Check for collision with ship:
            if (asteroidSize.Intersects(shipBounds))
            {
                collisionCount++;

                // Check if user has collided with object for more than 4 frames:
                if(collisionCount == 4)
                {
                    // If so, end game:
                    this.scene.EndGame();
                    Debug.WriteLine("Collision Detected With Asteroid");
                }

            }
            else
            {
                // decrement collisionCount if not touching the ship for more than 1 frame:
                if(collisionCount != 0)
                    collisionCount--;
            }

            foreach(MissleEntity missle in missles)
            {
                // Get Missle Boundaries:
                Rectangle missleBounds = missle.GetBounds();
                // Assign the true dimensions of the missle:
                missleBounds.Width = 30;
                missleBounds.Height = 60;
                missleBounds.X = (int)missle.Position.X;
                missleBounds.Y = (int)missle.Position.Y;

                if (asteroidSize.Intersects(missleBounds) && missle.IsActive)
                {
                    // Get Asteroids Current Position:
                    Vector2 prevPosition = this.position;

                    // Initialize Explosion Texture:
                    Texture2D expTex = Game.Content.Load<Texture2D>("images/explosionSheetUpscaled");
                    // Play Explosion Animation and sound:
                    ExplosionEntity explosionAnimation = new ExplosionEntity(Game, masterController._spriteBatch, expTex, prevPosition, scene, masterController);
                    scene.Components.Add(explosionAnimation);
                    explosion.Play();

                    //explosion.Visible = true;

                    // Send asteroid to the grave:
                    this.position = new Vector2(-10000, 2000);

                    // Move the MissleRectangle far away (so that it doesnt affect any new entities):
                    missleBounds.Location = new Point(-10000, -10000);

                    // Dispose Missle:
                    missle.IsActive = false;
                    missle.Position = new Vector2(-10000, -10000);
                    missle.Dispose();
                    scene.Components.Remove(missle);
                    
                    // Increment Score:
                    gameTimer.ScoreModifier += 150;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {   
            // Draw Asteroid:
            sb.Begin();
            sb.Draw(tex, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Update Position Based on Speed:
            position += speed;

            // Check if collision is occuring within the current frame:
            CheckCollision();

            // Remove Asteroid if it is out of the screen:
            if(this.position.Y >= 800)
            {
                this.Enabled = false;
                this.Dispose();
                scene.Components.Remove(this);
                Debug.WriteLine("Asteroid Disposed");
                
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Creates a rectangle that is equal to the bounds of the object
        /// </summary>
        /// <returns>A rectange object</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
