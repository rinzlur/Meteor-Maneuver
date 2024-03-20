// Will Smith 8657254
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeteorManeuver.Components
{
    public class CountDownComponent : DrawableGameComponent
    {
        // A Timer that is Used to Count the Seconds:
        private Timer countDown;
        private int countDownSeconds = 4;
        private int currentItemIndex = 0;
        private bool timerStarted = false;

        // SpriteBatches and SpriteFonts:
        private SpriteBatch sb;
        private SpriteFont countDownFont;

        // List of Strings that will be iterated through:
        private List<string> menuItems;

        // Position the Menu Items:
        private Vector2 position;

        // Colours that will be used for the StartScene:
        private Color colour = Color.White;

        public CountDownComponent(Game game, SpriteBatch sb, SpriteFont countDownFont, string[] menuItems) : base(game)
        {
            this.sb = sb;
            this.countDownFont = countDownFont;
            this.menuItems = menuItems.ToList();
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
          
        }

        /// <summary>
        /// Starts the Timer
        /// </summary>
        private void EnableTimer()
        {
            TimerCallback callBack = new TimerCallback(CountDownCallback);
            countDown = new Timer(callBack, null, 1000, 1000);
        }

        /// <summary>
        /// Callback that is called each timer tick.
        /// </summary>
        /// <param name="call">Timer Call</param>
        private void CountDownCallback(object call)
        {
            // Increment Item index:
            currentItemIndex++;
            if(currentItemIndex >= menuItems.Count)
            {
                currentItemIndex = 0;
            }

            // Decrement Count:
            countDownSeconds--;

            // Check if CountDown is Zero:
            if(countDownSeconds == 0)
            {
                // Nullify the timer:
                countDown.Change(Timeout.Infinite, Timeout.Infinite);
                menuItems = null;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Check if timer is started and enabled:
            if (!timerStarted && this.Enabled)
            { 
                EnableTimer();
                timerStarted = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            // Check is menuitems is null
            if(menuItems != null)
            {
                // Draw The Component:
                string onScreenText = menuItems[currentItemIndex];
                Vector2 stringSize = countDownFont.MeasureString(onScreenText);
                Vector2 newPosition = new Vector2((Shared.stage.X - stringSize.X) / 2, (Shared.stage.Y - stringSize.Y) / 2);
                sb.DrawString(countDownFont, onScreenText, newPosition, colour);
            }
            
            sb.End();

            base.Draw(gameTime);
        }


    }
}
