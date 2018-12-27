using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Game.Systems
{
    public class CameraSystem : Api.System.System
    {
        public Camera2D Camera { get; }

        public CameraSystem(Api.Game game) : base(game)
        {
            Camera = new Camera2D(new BoxingViewportAdapter(
                Game.Window,
                Game.GraphicsDevice,
                Game.Config.ViewportWidth,
                Game.Config.ViewportHeight
            ));
        }
    }
}
