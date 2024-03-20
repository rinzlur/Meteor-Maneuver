using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeteorManeuver
{
    // Shared class allows the program to share things globally:
    public class Shared
    {
        // Stage contains the dimensions of the applicaion window size:
        public static Vector2 stage;

        // DifficultySelection holds an int that is assigned to the difficulty that is assigned: (0, 1 or 2):
        public static int difficultySelection { get; set; }

        // Monitor Width and Height Dimensions
        public static int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    }
}
