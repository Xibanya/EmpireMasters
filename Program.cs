using System;

namespace Nebulous
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (GameEngine game = new GameEngine())
                game.Run();
        }
    }
#endif
}
