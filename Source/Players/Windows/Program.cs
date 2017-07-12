using System;

namespace KeyEngine.Player
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (PlayerApplication app = new PlayerApplication())
            {
                app.Run();
            }
        }
    }
}
