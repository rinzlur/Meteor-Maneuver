// Will  Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Entities
{
    public class MissleEntity : SuperClassEntity
    {
        // General Parameters 
        private int delay;
        private Vector2 dimension;
        private new Vector2 position;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delayCounter;

        // Spritesheet dimensions
        private const int ROWS = 1;
        private const int COLS = 6;

        // Flag:
        private bool isActive = false;

        // Public Accessors:
        public Vector2 Position { get => position; set => position = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public MissleEntity(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Vector2 speed) : base(game, sb, tex, position, speed)
        {
            MasterController masterController = (MasterController)game;
            this.sb = sb;
            this.tex = tex;
            this.Position = position;
            this.speed = speed;
            dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            CreateFrames();

        }

        /// <summary>
        /// Create frames for the current object
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
 
        public override void Update(GameTime gameTime)
        {
            // Handle Animations:

            delayCounter++;

            if (delayCounter > delay)
            {
                frameIndex++;

                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = 0;
                }

                delayCounter = 0;
            }

            // Handle Movement:

            position.Y -= speed.Y;


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                // Draw String:
                sb.Begin();
                sb.Draw(tex, position, frames[frameIndex], Color.White);
                sb.End();
            }

        }
    }
}
