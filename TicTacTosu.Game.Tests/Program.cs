using osu.Framework;
using osu.Framework.Platform;

namespace TicTacTosu.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost("visual-tests"))
            using (var game = new TicTacTosuTestBrowser())
                host.Run(game);
        }
    }
}
