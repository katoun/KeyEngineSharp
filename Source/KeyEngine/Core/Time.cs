using System;
using System.Diagnostics;

namespace KeyEngine.Core
{
    public class Time
    {
        static Stopwatch timer = new Stopwatch();

        static float deltaTime = 0f;

        public static float DeltaTime
        {
            get { return deltaTime; }
        }

        internal static void Start()
        {
            timer.Start();
        }

        internal static void Update()
        {
            deltaTime = (float)timer.Elapsed.TotalSeconds;
            timer.Reset();
            timer.Start();
        }

        internal static void Stop()
        {
            timer.Stop();
        }
    }
}
