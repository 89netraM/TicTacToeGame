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
		public Marker? this[int x, int y]
		{
			get => squares[x, y];
			private set => squares[x, y] = value;
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
		/// <param name="x">The x coordinate of the square.</param>
		/// <param name="y">The y coordinate of the square.</param>
		/// <exception cref="InvalidOperationException">Throws an exception if the square is already occupied.</exception>
		public void PlaceMarker(Marker marker, int x, int y)
		{
			if (this[x, y] != null)
			{
				throw new InvalidOperationException("Can not place a marker on an already occupied square.");
			}
			else
			{
				this[x, y] = marker;
			}
		}
	}
}