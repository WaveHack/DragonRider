using DragonRider.Shared.Api;
using DragonRider.Shared.Game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game
{
    public class Game : Api.Game
    {
        #region Properties

        public Camera2D Camera { get; private set; }

        #endregion

        #region Constructors

        public Game() : base(new GameConfig
        {
            WindowWidth = Constants.WindowWidth,
            WindowHeight = Constants.WindowHeight,
            ScreenClearColor = new Color(41, 41, 41)
        })
        {
            GameStateManager.ChangeState(new TestState(this), PlayerIndex.One);
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Constants.ViewportWidth, Constants.ViewportHeight);
            Camera = new Camera2D(viewportAdapter);
            Services.AddService(Camera);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        #endregion
    }
}
