using System.Collections.Generic;
using DragonRider.Shared.Api.DataTypes.Text;
using DragonRider.Shared.Api.Extensions;
using DragonRider.Shared.Api.Helpers.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private float deltaTime;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        private SpriteFont _font;

        private TextRenderer _textRenderer;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            _textRenderer = new TextRenderer();

            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 320, 180);
            _camera = new Camera2D(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textRenderer.SpriteBatch = _spriteBatch;

            _font = Content.Load<SpriteFont>("Fonts/FreePixel");
            _textRenderer.SpriteFont = _font;
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();
            const float speed = 4 * Constants.PPU;

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.Move(new Vector2(0, -speed * deltaTime));
            else if (keyboardState.IsKeyDown(Keys.Down))
                _camera.Move(new Vector2(0, speed * deltaTime));

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.Move(new Vector2(-speed * deltaTime, 0));
            else if (keyboardState.IsKeyDown(Keys.Right))
                _camera.Move(new Vector2(speed * deltaTime, 0));

            if (keyboardState.IsKeyDown(Keys.OemMinus))
                _camera.Zoom /= 1 + .5f * deltaTime;
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
                _camera.Zoom *= 1 + .5f * deltaTime;
            else if (keyboardState.IsKeyDown(Keys.D0))
                _camera.Zoom = 1;

            _camera.Position = Vector2.Clamp(_camera.Position, new Vector2(0, 0), new Vector2(20 * Constants.PPU, 15 * Constants.PPU));
            _camera.Zoom = _camera.Zoom.Clamp(.5f, 2f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(
                transformMatrix: _camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp
            );

            var coloredTexts = new List<ColoredText>();
            coloredTexts.Add(new ColoredText("All your ", Color.White));
            coloredTexts.Add(new ColoredText("base ", Color.Yellow));
            coloredTexts.Add(new ColoredText("are belong to ", Color.White));
            coloredTexts.Add(new ColoredText("us", Color.LimeGreen));
            coloredTexts.Add(new ColoredText(".", Color.White));

            Vector2 pos = new Vector2(320, 180) / 2 - _font.MeasureString("All your base are belong to us.") / 2;
            Vector2 offset = Vector2.Zero;

            foreach (var coloredText in coloredTexts)
            {
                _textRenderer.DrawStroked(coloredText.Text, pos + offset, coloredText.Color);
                offset.X += _font.MeasureString(coloredText.Text).X;
            }

            _spriteBatch.End();

            const int textHeight = 24;

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _textRenderer.DrawShadowed(
                "Camera: x" + _camera.Position.X.ToString("0.00") + " y" + _camera.Position.Y.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 0), Color.White, 2f);
            _textRenderer.DrawShadowed("Zoom: " + _camera.Zoom.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 1), Color.White, 2f);
            _textRenderer.DrawShadowed("Delta: " + deltaTime.ToString("0.0000"),
                Vector2.Zero + new Vector2(0, textHeight * 2), Color.White, 2f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
