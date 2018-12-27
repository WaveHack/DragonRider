using System.Diagnostics;
using DragonRider.Shared.Api.Scene;
using DragonRider.Shared.Game.Component;
using DragonRider.Shared.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragonRider.Shared.Game.Scenes
{
    public class TestScene : Scene
    {
        private CameraSystem _cameraSystem;
        private Player _player;

        public TestScene(Api.Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            Debug.WriteLine("[Game] TestScene.Initialize()");

            _cameraSystem = new CameraSystem(Game);
            _player = new Player(Game, new Vector2(50, 50), new Vector2(16, 24));
            _player.Initialize();
            Game.Components.Add(_player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Debug.WriteLine("[Game] TestScene.LoadContent()");

            //
        }

        public override void Update(float delta)
        {
            //
        }

        public override void Draw()
        {
            SpriteBatch.Begin(
                transformMatrix: _cameraSystem.Camera.GetViewMatrix(),
                samplerState: SamplerState.PointClamp
            );

            //

            SpriteBatch.End();
        }

        protected override void UnloadContent()
        {
            Debug.WriteLine("[Game] TestScene.UnloadContent()");

            //
        }
    }
}
