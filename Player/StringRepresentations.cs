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

			for (int x = 0; x < board.Size; x++)
			{
				StringBuilder rowBuilder = new StringBuilder();

				for (int y = 0; y < board.Size; y++)
				{
					rowBuilder.Append(Marker(board[x, y]));
				}

				rows[x] = rowBuilder.ToString();
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