using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MoreTec.TicTacToe.UnitTests
{
	[TestClass]
	public class GameTests
	{
		private Game game;

		[TestInitialize]
		public void Initialize()
		{
			game = new Game(5, 3);
		}

		[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldNotBeAbleToHaveATooLongGoal()
		{
			// Should throw an exception
			new Game(3, 4);
		}

		[TestMethod]
		public void ShouldStartInProgress()
		{
			Assert.AreEqual(GameState.InProgress, game.State, "The game should start as in progress.");
		}

		[TestMethod]
		public void ShouldBeAbleToStartAsCross()
		{
			game.PlaceMarker(Marker.Cross, 0, 0);
		}

		[TestMethod]
		public void ShouldBeAbleToStartAsCircle()
		{
			game.PlaceMarker(Marker.Circle, 0, 0);
		}

		[TestMethod, ExpectedException(typeof(InvalidOperationException))]
		public void ShouldNotBeAbleToPlayTwiceInARow()
		{
			game.PlaceMarker(Marker.Cross, 0, 0);

			// Should throw an exception
			game.PlaceMarker(Marker.Cross, 0, 1);
		}

		[TestMethod]
		public void ShouldBeAbleToWinVertically()
		{
			game.PlaceMarker(Marker.Circle, 2, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 3, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 2, 1);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 3, 1);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 2, 3);
			Assert.AreEqual(GameState.CircleWon, game.State);
		}

		[TestMethod]
		public void ShouldBeAbleToWinHorizontally()
		{
			game.PlaceMarker(Marker.Circle, 2, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 2, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 1, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 1, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 3, 2);
			Assert.AreEqual(GameState.CircleWon, game.State);
		}

		[TestMethod]
		public void ShouldBeAbleToWinDiagonally()
		{
			game.PlaceMarker(Marker.Circle, 2, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 2, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 1, 1);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 1, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 3, 3);
			Assert.AreEqual(GameState.CircleWon, game.State);
		}

		[TestMethod]
		public void ShouldBeAbleToWinOtherDiagonally()
		{
			game.PlaceMarker(Marker.Circle, 2, 2);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 2, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 1, 3);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Cross, 1, 0);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 3, 1);
			Assert.AreEqual(GameState.CircleWon, game.State);
		}

		[TestMethod]
		public void ShouldBeAbleToDraw()
		{
			game.PlaceMarker(Marker.Circle, 0, 0);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 1, 0);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 2, 0);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 3, 0);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 4, 0);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 1, 1);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 0, 1);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 3, 1);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 2, 1);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 0, 2);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 4, 1);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 2, 2);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 1, 2);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 4, 2);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 3, 2);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 0, 3);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 1, 3);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 2, 3);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 3, 3);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 4, 3);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 0, 4);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 1, 4);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Circle, 2, 4);
			Assert.AreEqual(GameState.InProgress, game.State);
			game.PlaceMarker(Marker.Cross, 3, 4);
			Assert.AreEqual(GameState.InProgress, game.State);

			game.PlaceMarker(Marker.Circle, 4, 4);
			Assert.AreEqual(GameState.Draw, game.State);
		}
	}
}