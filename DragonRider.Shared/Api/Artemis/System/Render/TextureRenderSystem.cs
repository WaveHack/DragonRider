using DragonRider.Shared.Api.Artemis.Component.Basic;
using DragonRider.Shared.Api.Artemis.Component.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace DragonRider.Shared.Api.Artemis.System.Render
{
    [Aspect(AspectType.All, typeof(PositionComponent), typeof(TextureComponent))]
    [EntitySystem(GameLoopType.Draw, Layer = 0)]
    public class TextureRenderSystem : EntityProcessingSystem
    {
        private Camera2D _camera;
        private SpriteBatch _spriteBatch;

        public override void Initialize()
        {
            _camera = Game.Services.GetService<Camera2D>();
            _spriteBatch = Game.Services.GetService<SpriteBatch>();
        }

        protected override void Begin(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(
                transformMatrix: transformMatrix,
                samplerState: SamplerState.PointClamp
           );
        }

        protected override void Process(GameTime gameTime, Entity entity)
        {
            var position = entity.Get<PositionComponent>();
            var texture = entity.Get<TextureComponent>();

            _spriteBatch.Draw(texture.Texture2D, position.Position);
        }

        protected override void End(GameTime gameTime)
        {
            _spriteBatch.End();
        }
    }
}
