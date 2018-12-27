using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api
{
    public class GameConfig
    {
        public int WindowWidth { get; set; } = 1280;
        public int WindowHeight { get; set; } = 720;

        public bool AllowWindowResizing { get; set; } = true;

        public int ViewportWidth { get; set; } = 1280;
        public int ViewportHeight { get; set; } = 720;

        public Color ScreenClearColor { get; set; } = Color.Black;
    }
}
