using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        /*
        Constant Fields
        Fields
        Constructors
        Finalizers (Destructors)
        Delegates
        Events
        Enums
        Interfaces
        Properties
        Indexers
        Methods
        Structs
        Classes

        public
        internal
        protected internal
        protected
        private

        static
        non-static

        readonly
        non-readonly
        */

        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        private SpriteFont _font;

        #endregion

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
        }

        #region Methods

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(
                Window,
//#if WINDOWS
                _graphics,
//#else
//                GraphicsDevice,
//#endif
                320,
                180
            );
            _camera = new Camera2D(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = _camera.GetViewMatrix();

            _spriteBatch.Begin(
                transformMatrix: transformMatrix,
                samplerState: SamplerState.PointClamp
            );
            _spriteBatch.DrawString(
                _font,
                "Hello world!",
                new Vector2(0, 0),
                Color.Black /*,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                0*/
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
