using System;

namespace MoreTec.TicTacToe
{
	public class Game
	{
		public Board Board { get; }
		public int GoalLength { get; }

		public GameState State { get; private set; } = GameState.InProgress;

		/// <summary>
		/// The marker type to be place next. Or <c>null</c> if it's not decided.
		/// </summary>
		public Marker? NextTurn { get; private set; } = null;

		/// <summary>
		/// Creates a new game.
		/// </summary>
		/// <param name="boardSize">The size of the square playing board.</param>
		/// <param name="goalLength">The goal length of the game.</param>
		/// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the <paramref name="goalLength"/> is larger than the <paramref name="boardSize"/>.</exception>
		public Game(int boardSize, int goalLength)
		{
			if (goalLength > boardSize)
			{
				throw new ArgumentOutOfRangeException(nameof(goalLength), "The goal length can't be bigger than the board size.");
			}

			Board = new Board(boardSize);
			GoalLength = goalLength;
		}

		/// <summary>
		/// Places a marker on the specified coordinates.
		/// </summary>
		/// <param name="marker">The type of the marker.</param>
		/// <param name="x">The x coordinate of the square.</param>
		/// <param name="y">The y coordinate of the square.</param>
		/// <exception cref="InvalidOperationException">Throws an exception if the same player goes twies in a row.</exception>
		public void PlaceMarker(Marker marker, int x, int y)
		{
			if (NextTurn != null && marker != NextTurn)
			{
				throw new InvalidOperationException("The same player can't go twice in a row.");
			}

			NextTurn = marker == Marker.Cross ? Marker.Circle : Marker.Cross;

			Board.PlaceMarker(marker, x, y);

			UpdateGameState(marker, x, y);
		}

		private void UpdateGameState(Marker marker, int x, int y)
		{
			if (WasWinningHorizontalMove(marker, x, y) ||
				WasWinningVerticalMove(marker, x, y) ||
				WasWinningDiagonalMove(marker, x, y) ||
				WasWinningOtherDiagonalMove(marker, x, y))
			{
				if (marker == Marker.Cross)
				{
					State = GameState.CorssWon;
				}
				else
				{
					State = GameState.CircleWon;
				}
			}
			else if (IsBoardFull())
			{
				State = GameState.Draw;
			}
		}

		private int GetMinX(int x) => Math.Max(0, x - (GoalLength - 1)) - x;
		private int GetMaxX(int x) => Math.Min(x + 1, Board.Size - (GoalLength - 1)) - x;
		private int GetMinY(int y) => Math.Max(0, y - (GoalLength - 1)) - y;
		private int GetMaxY(int y) => Math.Min(y + 1, Board.Size - (GoalLength - 1)) - y;
		private bool WasWinningHorizontalMove(Marker marker, int x, int y)
		{
			return WasWinningMove(
				marker,
				GetMinX(x),
				GetMaxX(x),
				delta => Board[x + delta, y]
			);
		}
		private bool WasWinningVerticalMove(Marker marker, int x, int y)
		{
			return WasWinningMove(
				marker,
				GetMinY(y),
				GetMaxY(y),
				delta => Board[x, y + delta]
			);
		}
		private bool WasWinningDiagonalMove(Marker marker, int x, int y)
		{
			return WasWinningMove(
				marker,
				Math.Max(GetMinX(x), GetMinY(y)),
				Math.Min(GetMaxX(x), GetMaxY(y)),
				delta => Board[x + delta, y + delta]
			);
		}
		private bool WasWinningOtherDiagonalMove(Marker marker, int x, int y)
		{
			return WasWinningMove(
				marker,
				Math.Max(GetMinX(x), Math.Max(y - (Board.Size - 1), -(GoalLength - 1))),
				Math.Min(GetMaxX(x), Math.Min(1, 1 + y - (GoalLength - 1))),
				delta => Board[x + delta, y - delta]
			);
		}
		private bool WasWinningMove(Marker marker, int min, int max, Func<int, Marker?> boardSquareGetter)
		{
			for (int start = min; start < max; start++)
			{
				bool win = true;

				for (int pos = 0; pos < GoalLength; pos++)
				{
					if (boardSquareGetter.Invoke(start + pos) != marker)
					{
						win = false;
						break;
					}
				}

				if (win)
				{
					return true;
				}
			}

			return false;
		}

		private bool IsBoardFull()
		{
			for (int x = 0; x < Board.Size; x++)
			{
				for (int y = 0; y < Board.Size; y++)
				{
					if (Board[x, y] == null)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}