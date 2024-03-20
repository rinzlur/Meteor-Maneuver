// Will Smith 8657254
using MeteorManeuver.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.DXGI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MeteorManeuver
{
    public class MasterController : Game
    {
        /* Edit Legend:
         * 
         * Day 1: Thursday, November 30th
         * --> Created /Components/MenuComponent and /Components/TitleComponent
         * --> Created 3x Resources :: TitleFont, SelectedFont, RegularFont, background.png, background2.png
         * --> Created /Scenes/GameScene and /Scenes/StartScene
         * --> Created Background.cs
         * --> Renamed 'Game1.cs' to 'MasterController.cs'
         * 
         * Day 2: Friday, December 1st
         * --> Added StartScreen Logic
         * --> Created 'HelpScene.cs', 'HiScoreScene.cs' and 'CreditScene.cs', 'DifficultySelectScene.cs' and 'ActionScene.cs'
         * --> Added 'Entities' folder, created 'AsteroidEntity.cs', 'MissleEntity.cs', 'SpaceShipEntity.cs', and 'SuperClassEntity.cs'
         * --> Added 6 more fonts, used within 'DifficultySelectionComponent.cs'
         * --> Implemented Space Ship movement and wall collision logic
         * 
         * Day 3: Monday, December 4th
         * --> Implemented Difficulty Logic, Turned 'ActionScene' into 'EasyActionScene'
         * --> Created 'CountDownComponent.cs', 'FinalScoreComponent.cs', 'GameTimerComponent.cs' and 'HiScoreComponent.cs'
         * --> Implemented Collision Logic for EasyActionScene (handled in 'AsteroidEntity.cs' and 'MissleEntity.cs'
         * --> Finalized 'CreditScene.cs'
         * 
         * Day 4: Tuesday, December 5th
         * --> Refactored a lot of 'MasterController.cs' to accomodate for multiple action scenes, as well as reinitialization of actionScenes
         * --> implemented hiScoreScene logic, Looping from one game, to the hiScoreScene, back to game is now possible
         * --> Added 'LaserEntity.cs', and 'MediumActionScene.cs', began implementing logic
         * 
         * Day 5: Wednesday, December 6th
         * --> implemented laser logic
         * --> created medium action scene, hardactionscene
         * --> added explosion animation to colliding missle/asteroids
         * --> finalised most of the project
         * 
         * Day 6: Thursday, December 7th
         * --> Finished commenting in the entire program
         * --> Created ReadwriteFile.cs
         * --> Finished Help Screen
         * --> bugfixes, finilizing project.
         * 
         * Program Completed.
         * 
         */

        // Initialize Graphics and SpriteBatch:
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        // Declare Scenes:
        private StartScene startScene;
        private HelpScene helpScene;
        private HiScoreScene hiScoreScene;
        private CreditScene creditScene;
        private DifficultySelectScene difficultySelectScene;
        private EasyActionScene easyActionScene;
        private MediumActionScene mediumActionScene;
        private HardActionScene hardActionScene;
        private FinalScoreScene finalScoreScene;

        // Sound Effects and Songs:
        SoundEffect select;
        Song actionMusic;
        Song backGroundMusic;

        // Declare Keyboard State:
        public KeyboardState oldState = Keyboard.GetState();

        // Initialize Timer Name:
        public Timer GameDelay;

        public MasterController()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set the window width and height:
            _graphics.PreferredBackBufferWidth = 780;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            
        }

        protected override void Initialize()
        {
            // Initalize Global Stage Variable:
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Background Parameters:
            Texture2D bckTex = this.Content.Load<Texture2D>("images/background");
            Rectangle srcRect = new Rectangle(0, 0, 800, 800);
            Vector2 bckPos = new Vector2(Shared.stage.X - srcRect.Width, 0);
            Vector2 bckSpeed = new Vector2(0, 1.8f);

            // Create and Add Background to Components:
            Background bckGrnd = new Background(this, _spriteBatch, bckTex, srcRect, bckPos, bckSpeed);
            this.Components.Add(bckGrnd);
            Texture2D bckTex2 = this.Content.Load<Texture2D>("images/background2");
            Background bckGrnd2 = new Background(this, _spriteBatch, bckTex2, srcRect, bckPos, new Vector2(0, 2.7f));
            this.Components.Add(bckGrnd2);

            // Load Static Scenes Into Content:
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            hiScoreScene = new HiScoreScene(this);
            this.Components.Add(hiScoreScene);
            creditScene = new CreditScene(this);
            this.Components.Add(creditScene);
            difficultySelectScene = new DifficultySelectScene(this);
            this.Components.Add(difficultySelectScene);

            // Load BGM from Content:
            backGroundMusic = this.Content.Load<Song>("sounds/StartSong");
            actionMusic = this.Content.Load<Song>("sounds/MainSong");
            
            // Adjust MediaPlayer Parameters:
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;

            // Play Music:
            StartHomeMusic();

            // Load Sound Effects:
            select = this.Content.Load<SoundEffect>("sounds/select1");

            // On Start, Show only StartScene:
            startScene.Show();

        }

        protected override void Update(GameTime gameTime)
        {
            /*
             * Page Movement Logic Handled Below:
             */

            // Declare Variables Needed for Page Movement:
            int selectedIndex = 0;

            KeyboardState state = Keyboard.GetState();


            // Check if StartScene is curently open:
            if (startScene.Enabled)
            {
                // Assign variables:
                selectedIndex = startScene.Menu.SelectedItem;

                // Handle A Users Input:
                if (selectedIndex == 0 && (state.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)))
                {
                    // Show difficulty, hide start:
                    difficultySelectScene.Show();
                    startScene.Hide();
                    // Play sound:
                    select.Play();
                }
                else if (selectedIndex == 1 && state.IsKeyDown(Keys.Enter))
                {
                    // Show help scene, hide start scene
                    startScene.Hide();
                    helpScene.Show();
                    // Play sound:
                    select.Play();
                }
                else if (selectedIndex == 2 && state.IsKeyDown(Keys.Enter))
                {
                    // Hide start, show hiscore:
                    startScene.Hide();
                    hiScoreScene.Show();
                    // Play sound:
                    select.Play();
                }
                else if (selectedIndex == 3 && state.IsKeyDown(Keys.Enter))
                {
                    // Hide start, show credit:
                    startScene.Hide();
                    creditScene.Show();
                    // Play sound:
                    select.Play();
                }
                else if (selectedIndex == 4 && state.IsKeyDown(Keys.Enter))
                {
                    // Exit application:
                    Exit();
                }
            }
            else if (helpScene.Enabled)
            {
                if (state.IsKeyDown(Keys.Escape))
                {
                    // show start scene, hide help
                    helpScene.Hide();
                    startScene.Show();
                }
            }
            else if (hiScoreScene.Enabled)
            {
                if (state.IsKeyDown(Keys.Escape))
                {
                    // Hide hiscore, show start
                    hiScoreScene.Hide();
                    startScene.Show();
                }
            }
            else if (creditScene.Enabled)
            {
                if (state.IsKeyDown(Keys.Escape))
                {
                    // hide credit, show start
                    creditScene.Hide();
                    startScene.Show();
                }
            }
            else if (difficultySelectScene.Enabled)
            {
                if (state.IsKeyDown(Keys.Escape))
                {
                    // hide selectionScreen, show start:
                    difficultySelectScene.Hide();
                    startScene.Show();
                }
                if (state.IsKeyDown(Keys.Space) && oldState.IsKeyDown(Keys.Space))
                {
                    // Handle difficulty selection:
                    switch (difficultySelectScene.Difficulty.SelectedItem)
                    {
                        case 0:
                            // Pause HomeScreen Music, Start Action Music:
                            MediaPlayer.Stop();
                            MediaPlayer.Play(actionMusic);
                            
                            // Initialize and Begin Action Scene:
                            Shared.difficultySelection = 0;
                            InitializeActionScene(Shared.difficultySelection);
                            difficultySelectScene.Hide();
                            break;
                        case 1:
                            // Pause HomeScreen Music, Start Action Music:
                            MediaPlayer.Stop();
                            MediaPlayer.Play(actionMusic);

                            // Initialize and Begin Action Scene:
                            Shared.difficultySelection = 1;
                            InitializeActionScene(Shared.difficultySelection);
                            difficultySelectScene.Hide();
                            break;
                        case 2:
                            // Pause HomeScreen Music, Start Action Music:
                            MediaPlayer.Stop();
                            MediaPlayer.Play(actionMusic);

                            // Initialize and Begin Action Scene:
                            Shared.difficultySelection = 2;
                            InitializeActionScene(Shared.difficultySelection);
                            difficultySelectScene.Hide();
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (Components.OfType<FinalScoreScene>().Any()) // Check if a finalScoreScene has been created
            {
                finalScoreScene = Components.OfType<FinalScoreScene>().Last();

                // Reset Window Position Back to Centre:
                this.Window.Position = new Point(562, 145);

                if (finalScoreScene != null)
                {
                    if(finalScoreScene.Enabled)
                    {
                        if (state.IsKeyDown(Keys.Space))
                        {
                            // User goes back to start scene:
                            StartHomeMusic();
                            hiScoreScene.UpdateHiScores();
                            finalScoreScene.Hide();
                            hiScoreScene.Show();
                        }
                        else if(state.IsKeyDown(Keys.Escape))
                        {
                            // user wishes to exit :(
                            Exit();
                        }
                    }
                }
            }

            // Assign oldState:
            oldState = state;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Starts the homescreen music
        /// </summary>
        public void StartHomeMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(backGroundMusic);
        }

        /// <summary>
        /// CallBack for Timer
        /// </summary>
        /// <param name="state">Object that calls</param>
        public void GameDelayCallBack(object state)
        {
            // Assign gamestart bool depending on DifficultySelection:
            if (Shared.difficultySelection == 0)
                easyActionScene.GameStart = true;
            else if (Shared.difficultySelection == 1)
                mediumActionScene.GameStart = true;
            else if (Shared.difficultySelection == 2)
                hardActionScene.GameStart = true;
        }

        /// <summary>
        /// Initializes an action scene depending on the difficulty:
        /// </summary>
        /// <param name="difficulty"></param>
        private void InitializeActionScene(int difficulty)
        {
            // Get difficulty:
            int difficultySelection = difficulty;

            if(difficultySelection == 0) 
            {
                // Dispose Currently Active EasyActionScene:
                if(easyActionScene != null)
                {
                    easyActionScene.Components.Clear();
                    easyActionScene.Dispose();
                }

                // Easy Mode Selected, Reinitialize EasyActionScene:
                easyActionScene = new EasyActionScene(this);
                this.Components.Add(easyActionScene);

                // Initialize DelayTimer:
                GameDelay = new Timer(GameDelayCallBack, null, 4000, Timeout.Infinite);

                // Show Action Scene:
                easyActionScene.Show();

            }
            if (difficultySelection == 1)
            {
                // Dispose Currently Active MediumActionScene:
                if (mediumActionScene != null)
                {
                    mediumActionScene.Components.Clear();
                    mediumActionScene.Dispose();
                }

                // Medium Mode Selected, Reinitialize MediumActionScene:
                mediumActionScene = new MediumActionScene(this);
                this.Components.Add(mediumActionScene);

                // Initialize DelayTimer:
                GameDelay = new Timer(GameDelayCallBack, null, 4000, Timeout.Infinite);

                // Show Action Scene:
                mediumActionScene.Show();

            }
            if (difficultySelection == 2)
            {
                // Dispose Currently Active MediumActionScene:
                if (hardActionScene != null)
                {
                    hardActionScene.Components.Clear();
                    hardActionScene.Dispose();
                }

                // Hard Mode Selected, Reinitialize HardActionScene:
                hardActionScene = new HardActionScene(this);
                this.Components.Add(hardActionScene);

                // Initialize DelayTimer:
                GameDelay = new Timer(GameDelayCallBack, null, 4000, Timeout.Infinite);

                // Show Action Scene:
                hardActionScene.Show();

            }
        }
    }
}