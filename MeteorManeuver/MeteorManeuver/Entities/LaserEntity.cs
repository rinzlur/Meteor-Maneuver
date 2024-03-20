// Will  Smith 8657254
using MeteorManeuver.Components;
using MeteorManeuver.Scenes;
using Microsoft.Xna.Framework;
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
    public class LaserEntity : DrawableGameComponent
    {
        // General Initializers
        public SpriteBatch sb;
        public Texture2D tex;
        public Vector2 position;
        public Vector2 speed;
        
        // Get userShip for collision detection (missle not neeeded, cannot affect laser)
        public SpaceShipEntity userShip;
        public SuperClassScene scene;
        public int collisionCount = 0;
        public GameTimerComponent gameTimer;
        MasterController masterController;

        // Random Variable initializer for Generation
        private static Random random = new Random();

        // Initializers Used to Generate Frames:
        private const int ROWS = 5;
        private const int COLS = 1;
        private int frameIndex = -1;
        private int delayCounter;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int delay;

        public LaserEntity(Game game, SpriteBatch sb, Texture2D tex, SpaceShipEntity userShip, SuperClassScene scene, GameTimerComponent gameTimer) : base(game)
        {
            masterController = (MasterController)game;
            this.sb = sb;
            this.tex = tex;
            this.position = GetNewPosition();
            this.speed = GetNewSpeed();
            this.userShip = userShip;
            this.gameTimer = gameTimer;
            dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);

            // Check game difficulty and assign scene accordingly:
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
                this.scene = scene as HardActionScene;
            }

            // Generate Frame List:
            CreateFrames();
        }

        /// <summary>
        /// Creates frames for the object
        /// </summary>
        private void CreateFrames()
        {
            // Initialize frames:
            frames = new List<Rectangle>();

            // Iterate through list:
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    // Create new rectange using calculated integers:
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    // Add to frames list:
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// Gets a new V2 parameter for the new object
        /// </summary>
        /// <returns>A randomized Vector2D object</returns>
        private Vector2 GetNewPosition()
        {
            // Initalize the bounds of the screen:
            float boundsX = Shared.stage.X;
            float boundsY = Shared.stage.Y;

            // Generate the starting position of the laser:
            float x = (float)random.NextDouble() * 395; 
            float y = -(float)random.NextDouble() * boundsY;

            return new Vector2(x, y);
            
        }

        /// <summary>
        /// Check for any collision between this object and passed parameters:
        /// </summary>
        public void CheckCollision()
        {
            // Get and Refactor ShipBounds:
            Rectangle shipBounds = userShip.GetBounds();
            shipBounds.Width = 50;
            shipBounds.Height = 50;

            // ensure the rectange accurately depict the location of the texture:
            shipBounds.X = (int)userShip.Position.X;
            shipBounds.Y = (int)userShip.Position.Y;

            // Get and Refactor Bounds of Laser:
            Rectangle laserBounds = this.GetBounds();
            laserBounds.Width = 380;
            laserBounds.Height = 50;

            // Check for Collisions:
            if (laserBounds.Intersects(shipBounds))
            {
                collisionCount++;

                // Check if user has collided with object for more than 4 frames:
                if (collisionCount == 4)
                {
                    Debug.WriteLine("Collision Detected With Laser");
                    // If so, end game:
                    this.scene.EndGame();
                }
            }
        }

        /// <summary>
        /// Get a randomized speed variable for the object
        /// </summary>
        /// <returns>A Vector2D variable that is randomized</returns>
        private Vector2 GetNewSpeed()
        {
            if(Shared.difficultySelection == 1)
            {
                float speedX = 0;
                float speedY = random.Next(3, 5);
                return new Vector2(speedX, speedY);
            }
            else if(Shared.difficultySelection == 2)
            {
                float speedX = 0;
                float speedY = random.Next(7, 9);
                return new Vector2(speedX, speedY);
            }
            else
            {
                return new Vector2(1, 3);
            }
        }

        public override void Update(GameTime gameTime)
        {
            /* Animation Logic: */

            // Increment Counter:
            delayCounter++;

            // Check if Theres another sprite Avaiable:
            if(delayCounter > delay)
            {
                // Update sprite:
                frameIndex++;

                // Check if frameindex is out of bounds:
                if(frameIndex > ROWS * COLS - 1)
                {
                    // Reset if true:
                    frameIndex = 0;
                }

                // Reset delaycounter:
                delayCounter = 0;
            }

            /* Position and Collision Logic: */

            // Update Position Based on Speed:
            position += speed;

            // Check For Collision:
            CheckCollision();

            // Remove Laser if Out Screen:
            if(this.position.Y >= 800)
            {
                this.Enabled = false;
                this.Dispose();
                scene.Components.Remove(this);
                Debug.WriteLine("Laser Disposed");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(frameIndex >= 0)
            {
                // Draw Object:
                sb.Begin();
                sb.Draw(tex, position, frames[frameIndex], Color.White);
                sb.End();
            }
           
        }

        /// <summary>
        /// Get the bounds of the object
        /// </summary>
        /// <returns>A rectange that contains the position and bounds of the object</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
