using org.loesoftgames.rotmg.rultra;

namespace org.loesoft.rotmg.ultra.core
{
    public static class GameUtils
    {
        public static void UpdateCaption(string pattern = null) =>
            App.window.Caption =
                string.Format("{0} - version: {1}{2}",
                    App.name,
                    App.version,
                    string.IsNullOrEmpty(pattern) ? "" : $" | {pattern}"
                );
    }
}
