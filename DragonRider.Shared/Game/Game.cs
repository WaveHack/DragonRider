using System.Collections.Generic;
using DragonRider.Shared.Api.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        private SpriteFont _font;

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

        protected override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 320, 180);
            _camera = new Camera2D(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Fonts/FreePixel");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            const float speed = 2f;

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.Move(new Vector2(0, -speed));
            else if (keyboardState.IsKeyDown(Keys.Down))
                _camera.Move(new Vector2(0, speed));

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.Move(new Vector2(-speed, 0));
            else if (keyboardState.IsKeyDown(Keys.Right))
                _camera.Move(new Vector2(speed, 0));

            if (keyboardState.IsKeyDown(Keys.OemMinus))
                _camera.Zoom /= 1.025f;
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
                _camera.Zoom *= 1.025f;
            else if (keyboardState.IsKeyDown(Keys.D0))
                _camera.Zoom = 1;

            _camera.Position = new Vector2(
                _camera.Position.X.Clamp(0, 100),
                _camera.Position.Y.Clamp(0, 100)
            );

            _camera.Zoom = _camera.Zoom.Clamp(.1f, 2f);

            base.Update(gameTime);
        }

        public struct ColoredText
        {
            public string _text;
            public Color _color;

            public ColoredText(string text, Color color)
            {
                _text = text;
                _color = color;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(
                transformMatrix: _camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp
            );
            void DrawOutline(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color, float scale)
            {
                // Shadow
//                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 1), Color.Gray, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                // Stroke
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, -1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, 1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(-1, 0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                spriteBatch.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            var texts = new List<ColoredText>();
            texts.Add(new ColoredText("All your ", Color.White));
            texts.Add(new ColoredText("base ", Color.Yellow));
            texts.Add(new ColoredText("are belong to ", Color.White));
            texts.Add(new ColoredText("us", Color.LimeGreen));
            texts.Add(new ColoredText(".", Color.White));

            Vector2 pos = new Vector2(320, 180) / 2 - _font.MeasureString("All your base are belong to us.") / 2;
            Vector2 offset = Vector2.Zero;

            foreach (var coloredText in texts)
            {
                DrawOutline(_spriteBatch, _font, coloredText._text, pos + offset, coloredText._color, 1f);
                offset.X += _font.MeasureString(coloredText._text).X;
            }

            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            DrawOutline(_spriteBatch, _font, "Camera: " + _camera.Position.X + "/" + _camera.Position.Y, Vector2.Zero, Color.White, 2f);
            DrawOutline(_spriteBatch, _font, "Zoom: " + _camera.Zoom.ToString("0.00"), Vector2.Zero + new Vector2(0, 24), Color.White, 2f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
