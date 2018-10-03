using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;

namespace DragonRider.Shared.Api.Artemis.System.Camera
{
    public class CameraSystem : PassiveSystem
    {
        #region Fields

        private int _viewportWidth;
        private int _viewportHeight;

        #endregion

        #region Properties

        public Camera2D Camera { get; private set; }

        #endregion

        #region Constructors

        public CameraSystem(int viewportWidth, int viewportHeight)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            var viewportAdapter = new BoxingViewportAdapter(
                Game.Window,
                Game.GraphicsDevice,
                _viewportWidth,
                _viewportHeight
            );
            Camera = new Camera2D(viewportAdapter);
        }

        #endregion
    }
}
