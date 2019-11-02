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


			TextReader crossInput;
			TextWriter crossOutput;
			if (argsMap.ContainsKey("cross"))
			{
				Process cross = Process.Start(argsMap["cross"]);
				crossInput = cross.StandardOutput;
				crossOutput = cross.StandardInput;
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
				Process circle = Process.Start(argsMap["circle"]);
				circleInput = circle.StandardOutput;
				circleOutput = circle.StandardInput;
			}
			else
			{
				circleInput = Console.In;
				circleOutput = Console.Out;
			}

			Player player = new Player(3, 3, crossInput, crossOutput, circleInput, circleOutput);
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
	}
}