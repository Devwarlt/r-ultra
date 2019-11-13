using org.loesoft.rotmg.ultra.core;
using System;
using System.Diagnostics;
using System.Reflection;
using Ultraviolet;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoftgames.rotmg.rultra
{
    public class App
    {
        public static readonly Size2 size = new Size2() { Width = 800, Height = 600 };

        public static string application { get; private set; }
        public static string company { get; private set; }
        public static UltravioletContext context { get; private set; }
        public static string version { get; private set; }
        public static IUltravioletWindow window { get; private set; }

        [STAThread]
        private static void Main()
        {
            var assembly = Assembly.GetExecutingAssembly();
            application = assembly.GetName().Name;
            company = FileVersionInfo.GetVersionInfo(assembly.Location).CompanyName;
            version =
                $"{assembly.GetName().Version}".Substring(0,
                $"{assembly.GetName().Version}".Length - 2);

            OpenGLUltravioletContext ultravioletContext = null;

            var game = new Game(company, application);
            game.Configure(
                ref ultravioletContext,
                out IUltravioletWindow ultravioletWindow);

            context = ultravioletContext;
            window = ultravioletWindow;
            window.ClientSize = size;

            using (game) game.Run();
        }
    }
}
