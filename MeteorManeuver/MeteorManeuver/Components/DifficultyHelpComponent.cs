// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class DifficultyHelpComponent : DrawableGameComponent
    {
        // Initialize MasterController:
        MasterController masterController;

        // Initialize variables needed for the component:
        private int selectedDifficulty;
        private Vector2 dimension;
        private Vector2 position;
        private Texture2D tex;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delayCounter;
        private SpriteBatch sb;

        // SpriteSheet Splitters:
        private const int ROWS = 3;
        private const int COLS = 1;


        public DifficultyHelpComponent(Game game, SpriteBatch sb, Vector2 position, Texture2D tex) : base(game)
        {
            masterController = (MasterController)game;
            this.sb = sb;
            this.position = position;
            this.tex = tex;
            selectedDifficulty = Shared.difficultySelection;
            dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);


            CreateFrames();
        }

        /// <summary>
        /// Create Frames for the Sprite
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
            selectedDifficulty = Shared.difficultySelection;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position, frames[selectedDifficulty], Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
