// Will  Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Entities
{
    public class SpaceShipEntity : SuperClassEntity
    {
        // General parameters:
        private int delay;
        private Vector2 dimension;
        private new Vector2 position;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delayCounter;

        // SpriteSheet Splitters:
        private const int ROWS = 2;
        private const int COLS = 6;

        // Timer Parameters for Missle Cooldown:
        private double coolDownTime = 3.0;
        private double elapsedTime = 0;
        
        // Initialize Sound Effect:
        SoundEffect launch;

        MasterController masterController;

        // Public accessors:
        public Vector2 Position { get => position; set => position = value; }

        public SpaceShipEntity(Game game, SpriteBatch sb, Texture2D tex, Vector2 speed, int delay, Vector2 position) : base(game, sb, tex, position, speed)
        {
            masterController = (MasterController)game;
            this.sb = sb;
            this.tex = tex;
            this.Position = position;
            this.delay = delay;
            dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);

            // Assign launch a soundeffect:
            launch = masterController.Content.Load<SoundEffect>("sounds/launch1");

            // Generate a list of frames:
            CreateFrames();
        }

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

        public override void Update(GameTime gameTime)
        {
            // Assign elapsed Time:
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            // Animation Handling: 

            delayCounter++;

            if(delayCounter > delay)
            {
                frameIndex++;

                if(frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = 0;
                }

                delayCounter = 0;
            }

            // Movement Handling:

            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.Left)) // Move left
            {
                position.X -= speed.X;

                // Check if out of bounds:
                if(position.X < 0)
                {
                    position.X = 0;
                }
            }
            if (state.IsKeyDown(Keys.Down)) // Move down
            {
                position.Y -= speed.Y;

                // Check if out of bounds:
                if (position.Y > Shared.stage.Y - frames[frameIndex].Height)
                {
                    position.Y = Shared.stage.Y - frames[frameIndex].Height;
                }
            }
            if (state.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;

                // Check if out of bounds:
                if (position.X > Shared.stage.X - frames[frameIndex].Width)
                {
                    position.X = Shared.stage.X - frames[frameIndex].Width;
                }
            }
            if (state.IsKeyDown(Keys.Up))
            {
                position.Y += speed.Y;

                // Check if out of bounds:
                if (position.Y <= 0)
                {
                    position.Y = 0;
                }

            }
            if (state.IsKeyDown(Keys.C) && elapsedTime >= coolDownTime)
            {
                // Create new MissleEntity at Ship Position:
                Texture2D missleTex = masterController.Content.Load<Texture2D>("images/MissleSheetSmall");
                MissleEntity missle = new MissleEntity(this.Game, masterController._spriteBatch, missleTex, this.position, new Vector2(0, 4));
                missle.IsActive = true;
                masterController.Components.Add(missle);

                // Play Launch Sound Effect:
                launch.Play();

                // Reset the timer once used:
                elapsedTime = 0.0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(frameIndex >= 0)
            {
                sb.Begin();
                sb.Draw(tex, position, frames[frameIndex], Color.White);
                sb.End();
            }

        }
    }
}
