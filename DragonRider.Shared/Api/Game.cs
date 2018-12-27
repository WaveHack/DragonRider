using System.Diagnostics;
using DragonRider.Shared.Api.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Api
{
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
        // System Properties
        public GraphicsDeviceManager Graphics { get; }

        // Properties
        public GameConfig Config { get; }
//        public float Delta { get; private set; }
        public SceneManager SceneManager { get; private set; }

        public Game(GameConfig config)
        {
            Services.AddService(this);

            Config = config;

            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = config.WindowWidth,
                PreferredBackBufferHeight = config.WindowHeight
            };

            Content.RootDirectory = "Content";
            Window.AllowUserResizing = config.AllowWindowResizing;
        }

        protected override void Initialize()
        {
            Debug.WriteLine("[Api] Game.Initialize()");

            Debug.WriteLine("[Api] - Register service: SpriteBatch");
            Services.AddService(new SpriteBatch(GraphicsDevice));

            SceneManager = new SceneManager(this);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
//            Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
//            SceneManager.Update(Delta);

            var delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            SceneManager.Update(delta);

//            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Config.ScreenClearColor);

            SceneManager.Draw();

//            base.Draw(gameTime);
        }
    }
}
