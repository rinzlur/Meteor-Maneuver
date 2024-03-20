// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class FinalScoreComponent : DrawableGameComponent
    {
        // General Intializers:
        private SpriteBatch sb;
        private SpriteFont finalScoreFont;

        // List of Strings for the FinalScoreComponent:
        private List<string> scoreList;

        // Positioning:
        private Vector2 position;

        // Colors:
        private Color finalScoreColour = Color.White;

        public FinalScoreComponent(Game game, SpriteBatch sb, SpriteFont finalScoreFont, string[] scoreList, Vector2 position) : base(game)
        {
            Debug.WriteLine("Inside FinalScoreComponent Constructor");

            this.sb = sb;
            this.finalScoreFont = finalScoreFont;
            this.scoreList = scoreList.ToList();
            this.position = position;
            this.Enabled = true;
            this.Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        { 
            // Assign temp Position:
            Vector2 tempPos = position;

            sb.Begin();

            // Loop through list, draw each index:
            for(int i = 0; i < scoreList.Count; i++)
            {
                // Draw Each String Found inside scoreList:
                sb.DrawString(finalScoreFont, scoreList[i], tempPos, finalScoreColour);
                // Increment tempPos:
                tempPos.Y += finalScoreFont.LineSpacing + 100;
            }

            sb.End();

            base.Draw(gameTime);
        }
    }
}
