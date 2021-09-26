using osu.Framework;
using osu.Framework.Platform;
using TicTacTosu.Game;

namespace TicTacTosu.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"TicTacTosu"))
            using (osu.Framework.Game game = new TicTacTosuGame())
                host.Run(game);
        }
    }
}
