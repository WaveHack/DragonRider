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

            _camera.Position = new Vector2(
                _camera.Position.X.Clamp(0, 100),
                _camera.Position.Y.Clamp(0, 100)
            );

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

            void DrawOutline(SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color)
            {
                // Shadow
//                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 1), Color.Gray);

                // Stroke
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, -1), Color.Black);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(0, 1), Color.Black);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(-1, 0), Color.Black);
                spriteBatch.DrawString(spriteFont, text, position + new Vector2(1, 0), Color.Black);

                spriteBatch.DrawString(spriteFont, text, position, color);
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
                DrawOutline(_spriteBatch, _font, coloredText.text, pos + offset, coloredText.color);
                offset += _font.MeasureString(coloredText.text);
                offset.Y = 0;
            }


//
//            Vector2 pos = new Vector2(320, 180) / 2
//                          - _font.MeasureString("All your base are belong to us.\nAll your base are belong to us.") / 2;
//
//            DrawOutline(_spriteBatch, _font, "All your ", pos, Color.White);
//
//            Vector2 offset = _font.MeasureString("All your ");
//            offset.Y = 0;
//            DrawOutline(_spriteBatch, _font, "base", pos + offset, Color.Yellow);
//
//            offset = _font.MeasureString("All your base ");
//            offset.Y = 0;
//            DrawOutline(_spriteBatch, _font, "are belong to ", pos + offset, Color.White);
//
//            offset = _font.MeasureString("All your base are belong to ");
//            offset.Y = 0;
//            DrawOutline(_spriteBatch, _font, "us", pos + offset, Color.LimeGreen);
//
//            offset = _font.MeasureString("All your base are belong to us");
//            offset.Y = 0;
//            DrawOutline(_spriteBatch, _font, ".", pos + offset, Color.White);
//
//            pos.Y += _font.MeasureString("All your base are belong to us.").Y;
//            DrawOutline(_spriteBatch, _font, "All your base are belong to us.", pos, Color.LightGray);


//            string text = "All your base are belong\nto us !@#$%^&*()-=_+\n[]{};':\",./<>?";

//            _spriteBatch.DrawString(_font, text, pos + new Vector2(0, -1), Color.Black);
//            _spriteBatch.DrawString(_font, text, pos + new Vector2(0, 1), Color.Black);
//            _spriteBatch.DrawString(_font, text, pos + new Vector2(-1, 0), Color.Black);
//            _spriteBatch.DrawString(_font, text, pos + new Vector2(1, 0), Color.Black);
//            _spriteBatch.DrawString(_font, text, pos, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            /*_spriteBatch.DrawString(
                _font,
//                "All your base are belong\nto us !@#$%^&*()-=_+\n[]{};':\",./<>?",
                "private void main(int args[]) {\n  return 0;\n}",
                new Vector2(0, 0),
                Color.Black/*,
                0,
                new Vector2(0, 0),
                1,//0.5f,
                SpriteEffects.None,
                0* /
            );*/

            _spriteBatch.End();

            _spriteBatch.Begin();
            DrawOutline(_spriteBatch, _font, "Camera: " + _camera.Position.X + "/" + _camera.Position.Y, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
