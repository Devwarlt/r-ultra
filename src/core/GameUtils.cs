using org.loesoftgames.rotmg.rultra;

namespace org.loesoft.rotmg.ultra.core
{
    public static class GameUtils
    {
        public static void UpdateCaption(string pattern = null) =>
            App.window.Caption =
                string.Format("[{0}] {1} - version: {2}{3}",
                    App.company,
                    App.application,
                    App.version,
                    string.IsNullOrEmpty(pattern) ? "" : $" | {pattern}"
                );
    }
}
