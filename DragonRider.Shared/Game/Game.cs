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
        private const int PPU = 16;
        private float deltaTime;

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
            deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();
            const float speed = 4 * PPU;

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

            _camera.Position = Vector2.Clamp(_camera.Position, new Vector2(0, 0), new Vector2(20 * PPU, 15 * PPU));
            _camera.Zoom = _camera.Zoom.Clamp(.5f, 2f);

            base.Update(gameTime);
        }

        public struct ColoredText
        {
            public readonly string Text;
            public readonly Color Color;

            public ColoredText(string text, Color color)
            {
                Text = text;
                Color = color;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(
                transformMatrix: _camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp
            );

            void DrawShadowed(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color, float scale)
            {
                // Shadow
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 1), Color.Gray, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                spriteBatch.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            void DrawOutlined(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color, float scale)
            {
                // Stroke
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, -1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, 1), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(-1, 0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 0), Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                spriteBatch.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

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
                DrawOutlined(_spriteBatch, _font, coloredText.Text, pos + offset, coloredText.Color, 1f);
                offset.X += _font.MeasureString(coloredText.Text).X;
            }

            _spriteBatch.End();

            const int textHeight = 24;

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            DrawShadowed(_spriteBatch, _font, "Camera: x" + _camera.Position.X.ToString("0.00") + " y" + _camera.Position.Y.ToString("0.00"), Vector2.Zero + new Vector2(0, textHeight * 0), Color.White, 2f);
            DrawShadowed(_spriteBatch, _font, "Zoom: " + _camera.Zoom.ToString("0.00"), Vector2.Zero + new Vector2(0, textHeight * 1), Color.White, 2f);
            DrawShadowed(_spriteBatch, _font, "Delta: " + deltaTime.ToString("0.0000"), Vector2.Zero + new Vector2(0, textHeight * 2), Color.White, 2f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
