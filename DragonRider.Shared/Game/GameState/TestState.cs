using System.Collections.Generic;
using DragonRider.Shared.Api.DataTypes.Text;
using DragonRider.Shared.Api.Extensions;
using DragonRider.Shared.Api.GameState;
using DragonRider.Shared.Api.Helpers.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DragonRider.Shared.Game.GameState
{
    public interface ITestState : IGameState
    {
    }

    public class TestState : GameState, ITestState
    {
        #region Fields

        private SpriteFont _font;
        private readonly TextRenderer _textRenderer;

        #endregion

        #region Constructors

        public TestState(Microsoft.Xna.Framework.Game game) : base(game)
        {
            _textRenderer = new TextRenderer();
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _textRenderer.SpriteBatch = GameRef.SpriteBatch;

            _font = content.Load<SpriteFont>("Fonts/FreePixel");
            _textRenderer.SpriteFont = _font;
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            const float speed = 4 * Constants.PPU;

            if (keyboardState.IsKeyDown(Keys.Up))
                GameRef.Camera.Move(new Vector2(0, -speed * GameRef.Delta));
            else if (keyboardState.IsKeyDown(Keys.Down))
                GameRef.Camera.Move(new Vector2(0, speed * GameRef.Delta));

            if (keyboardState.IsKeyDown(Keys.Left))
                GameRef.Camera.Move(new Vector2(-speed * GameRef.Delta, 0));
            else if (keyboardState.IsKeyDown(Keys.Right))
                GameRef.Camera.Move(new Vector2(speed * GameRef.Delta, 0));

            if (keyboardState.IsKeyDown(Keys.OemMinus))
                GameRef.Camera.Zoom /= 1 + .5f * GameRef.Delta;
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
                GameRef.Camera.Zoom *= 1 + .5f * GameRef.Delta;
            else if (keyboardState.IsKeyDown(Keys.D0))
                GameRef.Camera.Zoom = 1;

            float mapWidthInTiles = 40;
            float mapHeightInTiles = 22.5f;

            GameRef.Camera.Position = Vector2.Clamp(
                GameRef.Camera.Position,
                new Vector2(0, 0),
                new Vector2(
                    ((mapWidthInTiles * Constants.PPU) - Constants.VIEWPORT_WIDTH),
                    ((mapHeightInTiles * Constants.PPU) - Constants.VIEWPORT_HEIGHT)
                )
            );
            GameRef.Camera.Zoom = GameRef.Camera.Zoom.Clamp(.5f, 2f);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                transformMatrix: GameRef.Camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp
            );

            var coloredTexts = new List<ColoredText>
            {
                new ColoredText("All your ", Color.White),
                new ColoredText("base ", Color.Yellow),
                new ColoredText("are belong to ", Color.White),
                new ColoredText("us", Color.LimeGreen),
                new ColoredText(".", Color.White)
            };

            var pos = ((new Vector2(640, 360) / 2) - (_font.MeasureString("All your base are belong to us.") / 2));
            var offset = Vector2.Zero;

            foreach (var coloredText in coloredTexts)
            {
                _textRenderer.DrawStroked(coloredText.Text, pos + offset, coloredText.Color);
                offset.X += _font.MeasureString(coloredText.Text).X;
            }

            GameRef.SpriteBatch.End();

            const int textHeight = 24;

            GameRef.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _textRenderer.DrawShadowed(
                "Camera: x" + GameRef.Camera.Position.X.ToString("0.00") + " y" +
                GameRef.Camera.Position.Y.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 0), Color.White, 2f);
            _textRenderer.DrawShadowed("Zoom: " + GameRef.Camera.Zoom.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 1), Color.White, 2f);
            _textRenderer.DrawShadowed("Delta: " + GameRef.Delta.ToString("0.0000"),
                Vector2.Zero + new Vector2(0, textHeight * 2), Color.White, 2f);
            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
