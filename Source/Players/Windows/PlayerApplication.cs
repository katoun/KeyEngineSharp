using System;

using OpenTK;

namespace KeyEngine
{
    class PlayerApplication : Core.Application, IDisposable
    {
        public PlayerApplication()
        {
            IsPlaying = false;
            IsEditor = false;
        }

        public void Run()
        {
            IsPlaying = true;
            using (GameWindow example = new PlayerWindow())
            {
                OnQuit -= example.Close;
                OnQuit += example.Close;

                example.Run();
            }
        }

        void IDisposable.Dispose()
        {
            IsPlaying = false;
        }
    }
}
