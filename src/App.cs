using org.loesoft.rotmg.ultra.core;
using System;

namespace org.loesoftgames.rotmg.rultra
{
    public class App
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Game()) game.Run();
        }
    }
}
