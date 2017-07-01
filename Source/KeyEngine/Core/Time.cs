using System;
using System.Diagnostics;

namespace KeyEngine.Core
{
    public class Time
    {
        static Stopwatch timer = new Stopwatch();
        static long lastStartTime;

        static float deltaTime = 0f;

        public float DeltaTime
        {
            get { return deltaTime; }
        }

        internal static void Start()
        {
            timer.Start();

            lastStartTime = timer.ElapsedMilliseconds;
        }

        internal static void Update()
        {
            float result = (float)(timer.ElapsedMilliseconds - lastStartTime) / 1000;

            lastStartTime = timer.ElapsedMilliseconds;
        }

        internal static void Stop()
        {
            timer.Stop();
        }

        //TODO!!!
    }
}
