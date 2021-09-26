using osu.Framework.Testing;

namespace TicTacTosu.Game.Tests.Visual
{
    public class TicTacTosuTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new TicTacTosuTestSceneTestRunner();

        private class TicTacTosuTestSceneTestRunner : TicTacTosuGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
