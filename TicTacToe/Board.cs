using System;

namespace MoreTec.TicTacToe
{
	public class Board
	{
		private readonly Marker?[,] squares;

		/// <summary>
		/// The square sizeof this board.
		/// </summary>
		public int Size => squares.GetLength(0);
		public Marker? this[int row, int col]
		{
			get => squares[row, col];
			private set => squares[row, col] = value;
		}

		/// <summary>
		/// Creates a square TicTacToe board with the size of <paramref name="boardSize"/>.
		/// </summary>
		/// <param name="boardSize">The square size of the new board.</param>
		public Board(int boardSize)
		{
			squares = new Marker?[boardSize, boardSize];
		}

		/// <summary>
		/// Places a marker on the board.
		/// </summary>
		/// <param name="marker">The type of the marker.</param>
		/// <param name="row">The row of the square.</param>
		/// <param name="col">The column of the square.</param>
		/// <exception cref="InvalidOperationException">Throws an exception if the square is already occupied.</exception>
		public void PlaceMarker(Marker marker, int row, int col)
		{
			if (this[row, col] != null)
			{
				throw new InvalidOperationException("Can not place a marker on an already occupied square.");
			}
			else
			{
				this[row, col] = marker;
			}
		}
	}
}