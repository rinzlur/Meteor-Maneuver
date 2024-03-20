// Will  Smith 8657254
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
    public class ExplosionEntity : DrawableGameComponent
    {
        // General Initializers
        public SpriteBatch sb;
        public Texture2D tex;
        public Vector2 position;
        public SuperClassScene scene;
        public MasterController masterController;

        // Initializers Used to Generate Frames:
        private const int ROWS = 9;
        private const int COLS = 9;
        private int frameIndex = -1;
        private int delayCounter;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int delay;

        public ExplosionEntity(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, SuperClassScene scene, MasterController masterController) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            this.position = position;
            this.scene = scene;
            this.masterController = masterController;
            dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);

            // Check game difficulty and assign Scene accordingly:
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
            /* Animation Logic: */

            // Increment Counter:
            delayCounter++;

            // Check if Theres another sprite Avaiable:
            if (delayCounter > delay)
            {
                // Update sprite:
                frameIndex++;

                // Check if frameindex is out of bounds:
                if (frameIndex > ROWS * COLS - 1)
                {
                    // When out of frames, end:
                    return;
                }

                // Reset delaycounter:
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Check if frame index is in bounds:
            if (frameIndex <= frames.Count - 1)
            {
                // Draw Explosion:
                sb.Begin();
                sb.Draw(tex, position, frames[frameIndex], Color.White);
                sb.End();
            }
        }
    }
}
