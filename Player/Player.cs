using MoreTec.TicTacToe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoreTec.Player
{
	class Player
	{
		private readonly TextReader crossInput;
		private readonly TextWriter crossOutput;

		private readonly TextReader circleInput;
		private readonly TextWriter circleOutput;

		private readonly Game game;

		public Board Board => game.Board;
		public GameState GameState => game.State;

		/// <summary>
		/// Creates a new game and sets it to be played be two sets of reader/writers.
		/// </summary>
		/// <param name="boardSize">The size of the game board.</param>
		/// <param name="goalLength">The length needed for a win.</param>
		/// <param name="crossInput">The corsses input.</param>
		/// <param name="crossOutput">The crosses output.</param>
		/// <param name="circleInput">The circles input.</param>
		/// <param name="circleOutput">The circles output.</param>
		public Player(int boardSize, int goalLength, TextReader crossInput, TextWriter crossOutput, TextReader circleInput, TextWriter circleOutput)
		{
			this.game = new Game(boardSize, goalLength);

			this.crossInput = crossInput;
			this.crossOutput = crossOutput;
			this.circleInput = circleInput;
			this.circleOutput = circleOutput;
		}
		/// <summary>
		/// Creates a new game where both players communicate from the same set of reader/writer.
		/// </summary>
		/// <param name="boardSize">The size of the game board.</param>
		/// <param name="goalLength">The length needed for a win.</param>
		/// <param name="input">The input.</param>
		/// <param name="output">The output.</param>
		public Player(int boardSize, int goalLength, TextReader input, TextWriter output) : this(boardSize, goalLength, input, output, input, output) { }

		/// <summary>
		/// Starts a game.
		/// </summary>
		public void Play()
		{
			TextReader currentInput = crossInput;
			TextWriter currentOutput = crossOutput;
			Marker currentMarker = Marker.Cross;

			while (game.State == GameState.InProgress)
			{
				currentOutput.WriteLine(StringRepresentations.Board(game.Board));
				string input = currentInput.ReadLine();
				(int row, int col) = GetInputPosition(input);
				game.PlaceMarker(currentMarker, row, col);

				if (currentMarker == Marker.Cross)
				{
					currentInput = circleInput;
					currentOutput = circleOutput;
					currentMarker = Marker.Circle;
				}
				else
				{
					currentInput = crossInput;
					currentOutput = crossOutput;
					currentMarker = Marker.Cross;
				}
			}

			string resultsString = GetResultsString(game.State);
			crossOutput.WriteLine("Game Over: " + resultsString);
			circleOutput.WriteLine("Game Over: " + resultsString);
		}

		/// <summary>
		/// Parses an input string as a position of to integers.
		/// </summary>
		/// <param name="input">The string to parse.</param>
		/// <returns>A Tuple with the first and secnd integer from the input string.</returns>
		private ValueTuple<int, int> GetInputPosition(string input)
		{
			string[] split = input.Split(' ');

			if (split.Length != 2)
			{
				throw new ArgumentException("The input should be two integers separated by a single space.");
			}

			if (!int.TryParse(split[0], out int x))
			{
				throw new ArgumentException("The input should be two integers separated by a single space.");
			}

			if (!int.TryParse(split[1], out int y))
			{
				throw new ArgumentException("The input should be two integers separated by a single space.");
			}

			return (x, y);
		}

		/// <summary>
		/// Creates a cheerful message announcing the winner of a game.
		/// </summary>
		/// <param name="state">The end state of the game.</param>
		/// <returns>The cheerful message.</returns>
		private string GetResultsString(GameState state)
		{
			return state switch
			{
				GameState.Draw => "Draw!",
				GameState.CorssWon => "Cross Won!",
				GameState.CircleWon => "Circle Won!",
				_ => "What happend?"
			};
		}
	}
}