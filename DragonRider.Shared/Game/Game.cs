using DragonRider.Shared.Api.GameState;
using DragonRider.Shared.Game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Properties

        public float Delta { get; private set; }

        public GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Camera2D Camera { get; private set; }
        public StateManager StateManager { get; }

        #endregion

        #region Constructors

        public Game()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";

            StateManager = new StateManager(this);
            Components.Add(StateManager);

            StateManager.ChangeState(new TestState(this), PlayerIndex.One);
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 640, 360);
            Camera = new Camera2D(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }

        #endregion
    }
}
