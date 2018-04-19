using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KeyEngine.Core
{
	public class Application
	{
		public static bool IsEditor;
		public static bool IsPlaying;

		public static string Version = "v0.0.1";

		protected static event Action OnQuit;

		public static string ExecutableName { get; } = Path.GetFileName(typeof(Application).GetTypeInfo().Assembly.GetModules()[0].FullyQualifiedName);

		public static void Quit()
		{
			OnQuit?.Invoke();
		}
	}
}
