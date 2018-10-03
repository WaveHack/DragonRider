using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DragonRider.Shared.Api.Artemis.Component.Basic;
using DragonRider.Shared.Api.Artemis.Component.Graphics;
using DragonRider.Shared.Api.DataTypes.Text;
using DragonRider.Shared.Api.Extensions;
using DragonRider.Shared.Api.Helpers.Render;
using DragonRider.Shared.Game.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace DragonRider.Shared.Game.GameState
{
    public class TestState : BaseState
    {
        #region Fields

        private EntityComponentSystem _ecs;

        private SpriteFont _font;
        private readonly TextRenderer _textRenderer;
//        private TiledMap _map;
//        private TiledMapRenderer _mapRenderer;

//        private Player _player;

        #endregion

        #region Constructors

        public TestState(Game game) : base(game)
        {
            _textRenderer = new TextRenderer();

//            _player = new Player(game, new Vector2(33 * 16, 15 * 16), new Vector2(16, 24));
//            Components.Add(_player);
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            _ecs = new EntityComponentSystem(Game);
            Game.Services.AddService(_ecs);

            _ecs.Scan(Assembly.GetExecutingAssembly());
            _ecs.Initialize();


//            _player.Initialize(Game.SpriteBatch);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _textRenderer.SpriteBatch = Game.SpriteBatch;

            _font = Content.Load<SpriteFont>("Fonts/FreePixel");
            _textRenderer.SpriteFont = _font;

//            _map = Content.Load<TiledMap>("Maps/Test");
//            _mapRenderer = new TiledMapRenderer(Game.GraphicsDevice);

            var testEntity = _ecs.EntityManager.CreateEntity();
            testEntity.Attach<PositionComponent>(component => component.Position = new Vector2(50, 50));
            testEntity.Attach<TextureComponent>(component => component.Texture2D = Content.Load<Texture2D>("Graphics/Sprites/Player"));
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            const float speed = 8 * Constants.Ppu;

            if (keyboardState.IsKeyDown(Keys.Up))
                Game.Camera.Move(new Vector2(0, -speed * Game.Delta));
            else if (keyboardState.IsKeyDown(Keys.Down))
                Game.Camera.Move(new Vector2(0, speed * Game.Delta));

            if (keyboardState.IsKeyDown(Keys.Left))
                Game.Camera.Move(new Vector2(-speed * Game.Delta, 0));
            else if (keyboardState.IsKeyDown(Keys.Right))
                Game.Camera.Move(new Vector2(speed * Game.Delta, 0));

            if (keyboardState.IsKeyDown(Keys.OemMinus))
                Game.Camera.Zoom /= 1 + .5f * Game.Delta;
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
                Game.Camera.Zoom *= 1 + .5f * Game.Delta;
            else if (keyboardState.IsKeyDown(Keys.D0))
                Game.Camera.Zoom = 1;

            Game.Camera.Zoom = Game.Camera.Zoom.Clamp(1f, 2f);

//            float mapWidthInTiles = 40;
//            float mapHeightInTiles = 22.5f;

//            Game.Camera.Position = Vector2.Clamp(
//                Game.Camera.Position,
//                new Vector2(
//                    -(_map.WidthInPixels - _map.WidthInPixels * (1 / Game.Camera.Zoom)) / 2,
//                    -(_map.HeightInPixels - _map.HeightInPixels * (1 / Game.Camera.Zoom)) / 2
//                ),
//                new Vector2(
//                    ((mapWidthInTiles * Constants.Ppu) - Constants.ViewportWidth) + (_map.WidthInPixels - _map.WidthInPixels * (1 / Game.Camera.Zoom)) / 2,
//                    ((mapHeightInTiles * Constants.Ppu) - Constants.ViewportHeight) + (_map.HeightInPixels - _map.HeightInPixels * (1 / Game.Camera.Zoom)) / 2
//                )
//            );

//            _mapRenderer.Update(_map, gameTime);

            base.Update(gameTime);

            _ecs.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _ecs.Draw(gameTime);
            base.Draw(gameTime);

            var viewMatrix = Game.Camera.GetViewMatrix();

            SpriteBatch.Begin(
                transformMatrix: viewMatrix,
                samplerState: SamplerState.PointClamp
            );

//            _mapRenderer.Draw(_map.GetLayer("Background"), viewMatrix);
//            _mapRenderer.Draw(_map.GetLayer("Wall"), viewMatrix);

            // old player

//            _mapRenderer.Draw(_map.GetLayer("Foreground"), viewMatrix);

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

            SpriteBatch.End();

            const int textHeight = 24;

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _textRenderer.DrawShadowed(
                "Camera: x" + Game.Camera.Position.X.ToString("0.00") + " y" +
                Game.Camera.Position.Y.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 0), Color.White, 2f);
            _textRenderer.DrawShadowed("Zoom: " + Game.Camera.Zoom.ToString("0.00"),
                Vector2.Zero + new Vector2(0, textHeight * 1), Color.White, 2f);
            _textRenderer.DrawShadowed("Delta: " + Game.Delta.ToString("0.0000"),
                Vector2.Zero + new Vector2(0, textHeight * 2), Color.White, 2f);
            SpriteBatch.End();
        }

        #endregion
    }
}
