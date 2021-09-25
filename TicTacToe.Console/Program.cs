using System;
using System.Threading;
using TicTacToe.Framework;

namespace TicTacToe.Console
{
    class TicTacToeGame
    {
        static Game Game;

        static void Main(string[] args)
        {
            Console.WriteLine("\x1b[1mHello X_O World!\x1b[0m");
            Mark firstPlayer = PromptChooseFirstPlayer();
            Thread.Sleep(1500);

            Game = new Game(firstPlayer);
            for (;;)
            {
                Play();
                Thread.Sleep(1000);
                Console.WriteLine("Want to do another game? (y/N) > ");
                for (bool chosen = false; !chosen;)
                {
                    var cki = Console.ReadKey();
                    Console.WriteLine();

                    switch (cki.Key)
                    {
                        case ConsoleKey.Y:
                        case ConsoleKey.Enter:
                            Console.WriteLine("Have fun!");
                            Thread.Sleep(1000);
                            Console.Clear();

                            firstPlayer = PromptChooseFirstPlayer();
                            Thread.Sleep(1000);
                            Game.Reset(firstPlayer);
                            chosen = true;
                            break;
                        case ConsoleKey.N:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static Mark PromptChooseFirstPlayer()
        {
            Mark firstPlayer = Mark._;
            do
            {
                Console.Write("\x1b[32mEnter who plays the first move. [XxOo] >\x1b[0m ");
                ConsoleKeyInfo cki = Console.ReadKey();
                Console.WriteLine();

                switch (cki.Key)
                {
                    case ConsoleKey.X:
                        firstPlayer = Mark.X;
                        break;
                    case ConsoleKey.O:
                        firstPlayer = Mark.O;
                        break;
                    default:
                        Console.WriteLine($"{cki.Key} is not a valid player.");
                        break;
                }
            }
            while (firstPlayer == Mark._);
            Console.WriteLine($"{firstPlayer} will play the first move!");
            return firstPlayer;
        }

        /// Simulates a player's turn.
        static bool PromptTurn()
        {
            int x = 0, y = 0;
            for (bool invalid = false;;)
            {
                Console.Clear();
                Console.WriteLine(Game.MetaString);
                Console.WriteLine(BoardHighlightedString(x, y));
                Console.WriteLine();
                if (invalid) Console.WriteLine("\x1b[31mInvalid position!\x1b[0m");
                Console.WriteLine("\x1b[33mMoving keys: ←↓↑→ or hjkl. Q to quit.\x1b[0m");
                Console.WriteLine("Press any other key to validate.");

                invalid = false;
                ConsoleKeyInfo cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.H:
                        x = x == 0 ? 2 : x - 1;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                        y = (y + 1) % 3;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.K:
                        y = y == 0 ? 2 : y - 1;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.L:
                        x = (x + 1) % 3;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        Exit();
                        break;
                    default:
                        if (Game.PlaceMark(x, y))
                            return true;
                        else
                            invalid = true;
                        break;
                }
            }
        }

        static void Exit()
        {
            Console.WriteLine("\x1b[1mSee you next time :)\x1b[0m");
            System.Environment.Exit(0);
        }

        static void Play()
        {
            for (;;)
            {
                PromptTurn();

                switch (Game.State)
                {
                    case GameState.PlayerOWon:
                        Console.WriteLine("\x1b[1m\x1b[32mPlayer O won!\x1b[0m");
                        return;
                    case GameState.PlayerXWon:
                        Console.WriteLine("\x1b[1m\x1b[32mPlayer X won!\x1b[0m");
                        return;
                    case GameState.Tie:
                        Console.WriteLine("\x1b[1m... I guess it's a tie!\x1b[0m");
                        return;
                    default:
                        break;
                }
            }
        }

        /// Returns a string of the board with a highlighted position.
        static string BoardHighlightedString(int x, int y)
        {
            var b = Game.Board;
            string result = "";
            for (int j = 0; j < 3; j++)
            {
                result += j == 0 ? "" : "\n";

                if (j == y)
                    result += $"\x1b[41m| {b[0, j]} {b[1, j]} {b[2, j]} |\x1b[0m";
                else
                {
                    result += "| ";
                    for (int i = 0; i < 3; i++)
                        result += i == x ? $"\x1b[41m{b[i, j]}\x1b[0m " : $"{b[i, j]} ";
                    result += "|";
                }
            }
            return result;
        }
    }
}
