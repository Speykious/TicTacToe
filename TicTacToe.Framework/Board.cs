namespace TicTacToe.Framework
{
    /// This class handles all the state and logic of a Tic Tac Toe board.
    class Board
    {
        private Mark[] board;

        public Board()
        {
            board = new Mark[9];
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
                board[i] = Mark._;
        }

        public Mark this[int x, int y]
        {
            get => board[x + 3 * y];
            set => board[x + 3 * y] = value;
        }

        public bool IsValid(int x, int y) => x >= 0 && x < 3 && y >= 0 && y < 3;

        public bool IsFree(int x, int y) => IsValid(x, y) ? this[x, y] == Mark._ : false;

        public bool IsFull()
        {
            foreach (var mark in board)
                if (mark == Mark._)
                    return false;
            return true;
        }

        public bool PlaceMark(int x, int y, Mark mark)
        {
            if (!IsFree(x, y)) return false;
            this[x, y] = mark;
            return true;
        }

        private Mark lineWin(Mark a, Mark b, Mark c) => a == b && b == c ? a : Mark._;

        public Mark SomeMarkWon()
        {
            Mark winner;
            for (int i = 0; i < 3; i++)
            {
                winner = lineWin(this[0, i], this[1, i], this[2, i]);
                if (winner != Mark._) return winner;
                winner = lineWin(this[i, 0], this[i, 1], this[i, 2]);
                if (winner != Mark._) return winner;
            }

            winner = lineWin(this[0, 0], this[1, 1], this[2, 2]);
            if (winner != Mark._) return winner;
            winner = lineWin(this[2, 0], this[1, 1], this[0, 2]);
            if (winner != Mark._) return winner;

            return Mark._;
        }

        public override string ToString()
        {
            return $"| {this[0, 0]} {this[1, 0]} {this[2, 0]} |\n"
                + $"| {this[0, 1]} {this[1, 1]} {this[2, 1]} |\n"
                + $"| {this[0, 2]} {this[1, 2]} {this[2, 2]} |";
        }
    }
}
