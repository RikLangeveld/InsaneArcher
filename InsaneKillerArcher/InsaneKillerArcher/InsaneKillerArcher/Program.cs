using System;

namespace InsaneKillerArcher
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (InsaneKillerArcher game = new InsaneKillerArcher())
            {
                game.Run();
            }
        }
    }
#endif
}

