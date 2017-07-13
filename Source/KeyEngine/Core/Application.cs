using System;
using System.Collections.Generic;

namespace KeyEngine.Core
{
	public class Application
	{
		public static bool IsEditor;
		public static bool IsPlaying;

		public static string Version = "v0.0.1";

		protected static event Action OnQuit;

		public static void Quit()
		{
			OnQuit?.Invoke();
		}
	}
}
