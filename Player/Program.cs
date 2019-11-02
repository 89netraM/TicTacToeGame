using System;

namespace MoreTec.Player
{
	class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player(3, 3, Console.In, Console.Out);
			player.Play();
		}
	}
}