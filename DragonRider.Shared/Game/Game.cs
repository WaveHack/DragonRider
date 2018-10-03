using DragonRider.Shared.Api;
using DragonRider.Shared.Api.GameState;
using DragonRider.Shared.Game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            WindowWidth = Constants.WINDOW_WIDTH,
            WindowHeight = Constants.WINDOW_HEIGHT,
            ScreenClearColor = new Color(41, 41, 41)
        })
        {
            StateManager.ChangeState(new TestState(this), PlayerIndex.One);
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Constants.VIEWPORT_WIDTH, Constants.VIEWPORT_HEIGHT);
            Camera = new Camera2D(viewportAdapter);
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
