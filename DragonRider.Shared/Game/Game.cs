using System.Diagnostics;
using DragonRider.Shared.Api;
using DragonRider.Shared.Game.Scenes;
using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Game
{
    public class Game : Api.Game
    {
        public Game() : base(new GameConfig
        {
            ViewportWidth = 640,
            ViewportHeight = 360,
            ScreenClearColor = new Color(41, 41, 41)
        })
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            Debug.WriteLine("[Game] Game.Initialize()");

            SceneManager.ChangeScene(new TestScene(this));
        }
    }
}
