using org.loesoft.rotmg.ultra.core;

namespace org.loesoftgames.rotmg.rultra
{
    public class App
    {
        private static void Main()
        {
            using (var game = new Game()) game.Run();
        }
    }
}
