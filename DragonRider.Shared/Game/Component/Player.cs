using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Game.Component
{
    public class Player : DrawableGameComponent
    {
        protected SpriteBatch SpriteBatch { get; private set; }

        protected Vector2 Position { get; }
        protected Vector2 Bounds { get; }
        protected Vector2 Origin { get; }

        protected Texture2D Texture { get; private set; }

        public Player(Api.Game game, Vector2 position, Vector2 bounds, Vector2 origin = new Vector2()) : base(game)
        {
            Position = position;
            Bounds = bounds;
            Origin = origin;
        }

        public void Initialize()
        {
            Debug.WriteLine("Player.Initialize");

            SpriteBatch = Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Debug.WriteLine("Player.LoadContent");

            Texture = Game.Content.Load<Texture2D>("Graphics/Sprites/Player");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, Position);

            base.Draw(gameTime);
        }
    }
}
