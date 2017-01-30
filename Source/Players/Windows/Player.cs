using System;

namespace KeyEngine
{
    class Player
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (PlayerApplication app = new PlayerApplication())
            {
                app.Run();
            }
        }
    }
}
