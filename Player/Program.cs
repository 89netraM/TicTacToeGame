using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MoreTec.Player
{
	static class Program
	{
		public static void Main(string[] args)
		{
			Dictionary<string, string> argsMap = ParseArguments(args);

			if (argsMap.ContainsKey("help"))
			{
				PrintHelp();
				return;
			}

			int boardSize = 3;
			if (argsMap.ContainsKey("size"))
			{
				boardSize = int.Parse(argsMap["size"]);
			}

			int goalLength = boardSize;
			if (argsMap.ContainsKey("goal-length"))
			{
				goalLength = int.Parse(argsMap["goal-length"]);
			}

			TextReader crossInput;
			TextWriter crossOutput;
			if (argsMap.ContainsKey("cross"))
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(argsMap["cross"], String.Join(" ", boardSize, goalLength, "X"))
				{
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					CreateNoWindow = false
				};
				Process process = Process.Start(startInfo);
				crossInput = process.StandardOutput;
				crossOutput = process.StandardInput;
			}
			else
			{
				crossInput = Console.In;
				crossOutput = Console.Out;
			}

			TextReader circleInput;
			TextWriter circleOutput;
			if (argsMap.ContainsKey("circle"))
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(argsMap["circle"], String.Join(" ", boardSize, goalLength, "O"))
				{
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					CreateNoWindow = false
				};
				Process process = Process.Start(startInfo);
				circleInput = process.StandardOutput;
				circleOutput = process.StandardInput;
			}
			else
			{
				circleInput = Console.In;
				circleOutput = Console.Out;
			}

			Player player = new Player(boardSize, goalLength, crossInput, crossOutput, circleInput, circleOutput);
			player.Play();
		}

		private static Dictionary<string, string> ParseArguments(string[] args)
		{
			Dictionary<string, string> argsMap = new Dictionary<string, string>();

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith("--"))
				{
					string key = args[i].Substring(2).ToLower();
					string value = null;
					if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
					{
						value = args[i + 1];
						i++;
					}

					argsMap.Add(key, value);
				}
			}

			return argsMap;
		}

		private static void PrintHelp()
		{
			Console.WriteLine(
@"Options:
  --help               Prints out the help.
  --size <int>         Sets the size of the game board.
  --goal-length <int>  Sets the length required for victory. Default is same
                       as the board size.
  --cross <string>     Path to an executable that should act as the cross
                       player.
  --circle <string>    Path to an executable that should act as the circle
                       player."
			);
		}
	}
}