using MoreTec.TicTacToe;
using System;
using System.Text;

namespace MoreTec.Player
{
	static class StringRepresentations
	{
		public static string Board(Board board)
		{
			string[] rows = new string[board.Size];

			for (int row = 0; row < board.Size; row++)
			{
				StringBuilder rowBuilder = new StringBuilder();

				for (int col = 0; col < board.Size; col++)
				{
					rowBuilder.Append(Marker(board[row, col]));
				}

				rows[row] = rowBuilder.ToString();
			}

			return String.Join('\n', rows);
		}

		public static string Marker(Marker? marker)
		{
			return marker switch
			{
				TicTacToe.Marker.Cross => "X",
				TicTacToe.Marker.Circle => "O",
				_ => " "
			};
		}
	}
}