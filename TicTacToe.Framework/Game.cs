namespace TicTacToe.Framework
{
    class Game
    {
        public int CurrentTurn { get; private set; }
        public Mark CurrentPlayer { get; private set; }
        public GameState State { get; private set; }
        public Board Board { get; }

        public Game(Mark firstPlayer)
        {
            Board = new Board();
            Reset(firstPlayer);
        }

        public void Reset(Mark firstPlayer)
        {
            Board.Reset();
            CurrentPlayer = firstPlayer;
            CurrentTurn = 1;
            State = GameState.Playing;
        }

        private void updateState()
        {
            if (Board.IsFull())
                State = GameState.Tie;
            else
            {
                switch (Board.SomeMarkWon())
                {
                    case Mark.O:
                        State = GameState.PlayerOWon;
                        break;
                    case Mark.X:
                        State = GameState.PlayerXWon;
                        break;
                    default:
                        State = GameState.Playing;
                        break;
                }
            }
        }

        private void switchCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == Mark.X ? Mark.O : Mark.X;
            CurrentTurn++;
        }

        public bool PlaceMark(int x, int y)
        {
            if (State != GameState.Playing)
                return false;
            
            if (!Board.PlaceMark(x, y, CurrentPlayer))
                return false;

            updateState();

            if (State == GameState.Playing)
                switchCurrentPlayer();
            
            return true;
        }

        public string MetaString
        {
            get => "<< Tic-Tac-Toe Game >>"
                + $"\n| Turn n°{CurrentTurn}"
                + $"\n| Current player: {CurrentPlayer}"
                + $"\n| Game state: {State}"
                + $"\n+------------------------";
        }

        public override string ToString()
        {
            return $"{MetaString}\n{Board}";
        }
    }
}
