using DragonRider.Shared.Api.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Properties

        public GameConfig Config { get; }
        public GraphicsDeviceManager Graphics { get; }
        public StateManager StateManager { get; }

        public float Delta { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Constructors

        public Game(GameConfig config)
        {
            Config = config;

            Content.RootDirectory = "Content";

//            Services.AddService(typeof(Game), this);

            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = config.WindowWidth,
                PreferredBackBufferHeight = config.WindowHeight
            };

            StateManager = new StateManager(this);
            Components.Add(StateManager);

            Window.AllowUserResizing = config.AllowWindowResizing;
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), SpriteBatch);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Config.ScreenClearColor);

            base.Draw(gameTime);
        }

        #endregion
    }
}
