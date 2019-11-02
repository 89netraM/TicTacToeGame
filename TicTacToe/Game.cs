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
		/// <param name="row">The row of the square.</param>
		/// <param name="col">The column of the square.</param>
		/// <exception cref="InvalidOperationException">Throws an exception if the same player goes twies in a row.</exception>
		public void PlaceMarker(Marker marker, int row, int col)
		{
			if (NextTurn != null && marker != NextTurn)
			{
				throw new InvalidOperationException("The same player can't go twice in a row.");
			}

			NextTurn = marker == Marker.Cross ? Marker.Circle : Marker.Cross;

			Board.PlaceMarker(marker, row, col);

			UpdateGameState(marker, row, col);
		}

		private void UpdateGameState(Marker marker, int row, int col)
		{
			if (WasWinningHorizontalMove(marker, row, col) ||
				WasWinningVerticalMove(marker, row, col) ||
				WasWinningDiagonalMove(marker, row, col) ||
				WasWinningOtherDiagonalMove(marker, row, col))
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

		private int GetMinRow(int row) => Math.Max(0, row - (GoalLength - 1)) - row;
		private int GetMaxRow(int row) => Math.Min(row + 1, Board.Size - (GoalLength - 1)) - row;
		private int GetMinCol(int col) => Math.Max(0, col - (GoalLength - 1)) - col;
		private int GetMaxCol(int col) => Math.Min(col + 1, Board.Size - (GoalLength - 1)) - col;
		private bool WasWinningHorizontalMove(Marker marker, int row, int col)
		{
			return WasWinningMove(
				marker,
				GetMinRow(row),
				GetMaxRow(row),
				delta => Board[row + delta, col]
			);
		}
		private bool WasWinningVerticalMove(Marker marker, int row, int col)
		{
			return WasWinningMove(
				marker,
				GetMinCol(col),
				GetMaxCol(col),
				delta => Board[row, col + delta]
			);
		}
		private bool WasWinningDiagonalMove(Marker marker, int row, int col)
		{
			return WasWinningMove(
				marker,
				Math.Max(GetMinRow(row), GetMinCol(col)),
				Math.Min(GetMaxRow(row), GetMaxCol(col)),
				delta => Board[row + delta, col + delta]
			);
		}
		private bool WasWinningOtherDiagonalMove(Marker marker, int row, int col)
		{
			return WasWinningMove(
				marker,
				Math.Max(GetMinRow(row), Math.Max(col - (Board.Size - 1), -(GoalLength - 1))),
				Math.Min(GetMaxRow(row), Math.Min(1, 1 + col - (GoalLength - 1))),
				delta => Board[row + delta, col - delta]
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
			for (int row = 0; row < Board.Size; row++)
			{
				for (int col = 0; col < Board.Size; col++)
				{
					if (Board[row, col] == null)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}