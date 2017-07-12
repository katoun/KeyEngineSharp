using System;

namespace KeyEngine.Player
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
            using (Graphics.NativeWindow window = new Graphics.NativeWindow())
            {
                OnQuit -= window.Close;
                OnQuit += window.Close;

                window.Run();
            }
        }

        void IDisposable.Dispose()
        {
            IsPlaying = false;
        }
    }
}
