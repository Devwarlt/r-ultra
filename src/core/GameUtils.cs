using org.loesoft.rotmg.ultra.core.texts;
using org.loesoftgames.rotmg.rultra;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics.Graphics2D.Text;

namespace org.loesoft.rotmg.ultra.core
{
    public static class GameUtils
    {
        public static TextFlags GetFlags(this TextAlignment alignment)
        {
            switch (alignment)
            {
                case TextAlignment.BottomCenter: return TextFlags.AlignBottom | TextFlags.AlignCenter;
                case TextAlignment.BottomLeft: return TextFlags.AlignBottom | TextFlags.AlignLeft;
                case TextAlignment.BottomRight: return TextFlags.AlignBottom | TextFlags.AlignRight;
                case TextAlignment.MiddleCenter: return TextFlags.AlignMiddle | TextFlags.AlignCenter;
                case TextAlignment.MiddleLeft: return TextFlags.AlignMiddle | TextFlags.AlignLeft;
                case TextAlignment.MiddleRight: return TextFlags.AlignMiddle | TextFlags.AlignRight;
                case TextAlignment.TopCenter: return TextFlags.AlignTop | TextFlags.AlignCenter;
                case TextAlignment.TopLeft: return TextFlags.AlignTop | TextFlags.AlignLeft;
                case TextAlignment.TopRight: return TextFlags.AlignTop | TextFlags.AlignRight;
                case TextAlignment.Standard:
                default: return TextFlags.Standard;
            }
        }

        public static Hotkeys GetHotkeys(this IUltravioletInput input) => Hotkeys.singleton;

        public static T LoadContent<T>(AssetID asset, bool cache = true, bool fromsln = false) =>
            App.content.Load<T>(asset, cache, fromsln);

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
