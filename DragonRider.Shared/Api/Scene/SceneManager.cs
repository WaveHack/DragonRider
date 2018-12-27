using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DragonRider.Shared.Api.Scene
{
    public class SceneManager : System.System
    {
        public Scene ActiveScene { get; private set; }

        public SceneManager(Game game) : base(game)
        {
        }

        public void ChangeScene(Scene scene)
        {
            Debug.WriteLine("[Api] - Change scene: " + scene.GetType().Name);

            ActiveScene?.Dispose();

            ActiveScene = scene;
            scene.Initialize();
        }

        public void Update(float delta)
        {
            ActiveScene.Update(delta);
        }

        public void Draw()
        {
            ActiveScene.Draw();
        }
    }
}
