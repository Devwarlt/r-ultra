using org.loesoft.rotmg.ultra.core;
using System;
using System.Reflection;
using Ultraviolet;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoftgames.rotmg.rultra
{
    public class App
    {
        public static UltravioletContext context { get; private set; }
        public static string name { get; private set; }
        public static string version { get; private set; }
        public static IUltravioletWindow window { get; private set; }

        [STAThread]
        private static void Main()
        {
            var game = new Game();
            game.Configure(
                out OpenGLUltravioletContext ultravioletContext,
                out IUltravioletWindow ultravioletWindow);

            context = ultravioletContext;
            window = ultravioletWindow;
            name = Assembly.GetExecutingAssembly().GetName().Name;
            version =
                $"{Assembly.GetExecutingAssembly().GetName().Version}".Substring(0,
                $"{Assembly.GetExecutingAssembly().GetName().Version}".Length - 2);

            using (game) game.Run();
        }
    }
}
