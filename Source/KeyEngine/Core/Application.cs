using System;
using System.Collections.Generic;

namespace KeyEngine.Core
{
    public class Application
    {
        public static bool IsEditor;
        public static bool IsPlaying;

        protected static event Action OnQuit;

        public static void Quit()
        {
            OnQuit?.Invoke();
        }
    }
}
