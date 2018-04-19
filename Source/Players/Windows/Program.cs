using System;

namespace KeyEngine.Player
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			using (PlayerApplication app = new PlayerApplication())
			{
				app.Run();
			}
		}
	}
}
