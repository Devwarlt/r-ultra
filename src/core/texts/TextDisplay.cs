using org.loesoft.rotmg.ultra.core.assets;
using org.loesoftgames.rotmg.rultra;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Graphics.Graphics2D.Text;

namespace org.loesoft.rotmg.ultra.core.texts
{
    public sealed class TextDisplay
    {
        private readonly SpriteFont font;
        private readonly TextRenderer renderer;
        private readonly TextLayoutCommandStream stream;

        private Color color;
        private Vector2 position;
        private TextLayoutSettings settings;
        private string text;

        public TextDisplay(string text, Color color)
            : this(AssetFontID.SegoeUI, text, color, null, TextAlignment.Standard)
        {
        }

        public TextDisplay(string text, Color color, TextAlignment alignment)
            : this(AssetFontID.SegoeUI, text, color, null, alignment)
        {
        }

        public TextDisplay(string text, Color color, TextDisplayStyle[] textDisplayStyles)
            : this(AssetFontID.SegoeUI, Vector2.Zero, text, color, textDisplayStyles)
        {
        }

        public TextDisplay(AssetID asset, string text, Color color, TextDisplayStyle[] textDisplayStyles, TextAlignment alignment)
            : this(asset, Vector2.Zero, text, color, textDisplayStyles, null, null, alignment)
        {
        }

        public TextDisplay(AssetID asset, Vector2 position, string text, Color color, TextDisplayStyle[] textDisplayStyles = null,
            int? width = null, int? height = null, TextAlignment alignment = TextAlignment.Standard)
        {
            this.position = position;
            this.text = text;

            font = GameUtils.LoadContent<SpriteFont>(asset);
            renderer = new TextRenderer();
            renderer.RegisterFont(AssetID.GetAssetName(asset), font);

            if (textDisplayStyles != null && textDisplayStyles.Length != 0)
                for (var i = 0; i < textDisplayStyles.Length; i++)
                    renderer.RegisterStyle(textDisplayStyles[i].GetName(), textDisplayStyles[i].GetStyle());

            settings = new TextLayoutSettings(font, width, height, alignment.GetFlags());
            stream = new TextLayoutCommandStream();
        }

        public void Draw(SpriteBatch batch)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (settings.Flags == TextFlags.Standard) renderer.Draw(batch, text, position, color, settings);
                else
                {
                    var size = new Size2(App.window.DrawableSize.Width, App.window.DrawableSize.Height);

                    if (stream.Settings.Width != size.Width || stream.Settings.Height != size.Height)
                    {
                        settings = new TextLayoutSettings(settings.Font, size.Width, size.Height, settings.Flags);
                        renderer.CalculateLayout(text, stream, settings);

                        var streamSettings = stream.Settings;

                        System.Console.WriteLine(string.Format(
                            "[Stream Settings]:\n" +
                            "\t- Direction: {0}\n" +
                            "\t- Flags: {1}\n" +
                            "\t- Font: {2}\n" +
                            "\t- Size: [w: {3}, h: {4}]\n" +
                            "\t- Language: {5}\n" +
                            "\t- Options: {6}\n" +
                            "\t- Script: {7}\n" +
                            "\t- Style: {8} (initial: {9})\n",
                            streamSettings.Direction,
                            streamSettings.Flags,
                            streamSettings.Font.ToString(),
                            streamSettings.Width,
                            streamSettings.Height,
                            streamSettings.Language,
                            streamSettings.Options,
                            streamSettings.Script,
                            streamSettings.Style,
                            streamSettings.InitialLayoutStyle ?? "Unknown"
                        ));
                    }

                    renderer.Draw(batch, stream, position, color);
                }
            }
        }

        public Color GetColor() => color;

        public Vector2 GetPosition() => position;

        public string GetText() => text;

        public void SetColor(Color color) => this.color = color;

        public void SetPosition(Vector2 position) => this.position = position;

        public void SetText(string text) => this.text = text;
    }
}
