using DragonRider.Shared.Game;

namespace DragonRider.Windows
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
