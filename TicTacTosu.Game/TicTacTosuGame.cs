using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Screens;

namespace TicTacTosu.Game
{
    public class TicTacTosuGame : TicTacTosuGameBase
    {
        private ScreenStack screenStack;
        private DrawableSample exampleSample;

        [BackgroundDependencyLoader]
        private void load()
        {
            exampleSample = new DrawableSample(Audio.Samples.Get("example.wav"));

            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainScreen
            {
                ExampleSample = exampleSample
            });
        }
    }
}
