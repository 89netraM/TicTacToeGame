using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MoreTec.TicTacToe.UnitTests
{
	[TestClass]
	public class BoardTests
	{
		private Board board;

		[TestInitialize]
		public void Initialize()
		{
			board = new Board(3);
		}

		[TestMethod]
		public void ShouldStartFullOfNull()
		{
			for (int row = 0; row < board.Size; row++)
			{
				for (int col = 0; col < board.Size; col++)
				{
					Assert.IsNull(board[row, col], "All squares on a new board should be null.");
				}
			}
		}

		[TestMethod]
		public void ShouldBeAbelToPlaceACircle()
		{
			board.PlaceMarker(Marker.Circle, 0, 0);
		}

		[TestMethod]
		public void ShouldBeAbelToPlaceACross()
		{
			board.PlaceMarker(Marker.Cross, 0, 0);
		}

		[TestMethod, ExpectedException(typeof(InvalidOperationException))]
		public void ShouldNotBeAbelToPlaceTwice()
		{
			board.PlaceMarker(Marker.Circle, 0, 0);

			// Should throw an exception
			board.PlaceMarker(Marker.Cross, 0, 0);
		}
	}
}