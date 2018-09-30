using DragonRider.Shared.Game;

namespace DragonRider.DesktopGL
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new Game())
                game.Run();
        }
    }
}
