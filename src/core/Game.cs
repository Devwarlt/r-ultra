using org.loesoft.rotmg.ultra.core.assets;
using org.loesoft.rotmg.ultra.core.entities;
using org.loesoftgames.rotmg.rultra;
using System;
using System.IO;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Graphics.Graphics2D.Text;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoft.rotmg.ultra.core
{
    public class Game : UltravioletApplication
    {
        private const string inputBindingsName = "r-ultra.bi.xml";

        private readonly TextLayoutCommandStream textLayoutCommands = new TextLayoutCommandStream();
        private SpriteBatch batch;
        private Camera camera;
        private OpenGLUltravioletConfiguration config;
        private Player player;
        private SpriteFont spriteFontGaramond;
        private SpriteFont spriteFontSegoe;
        private TextRenderer textRenderer;

        public Game(string company, string application)
            : base(company, application)
        {
        }

        public void Configure(ref OpenGLUltravioletContext context, out IUltravioletWindow window, out ContentManager content)
        {
            config = new OpenGLUltravioletConfiguration();
            context = new OpenGLUltravioletContext(this, config);
            window = context.GetPlatform().Windows.GetPrimary();
            content = ContentManager.Create("resources");
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                SafeDispose.Dispose(batch);
                SafeDispose.Dispose(App.content);
            }

            base.Dispose(disposing);
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            PopulateConfiguration(config);

            return App.context;
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.GetMatrix());

            player.Draw(batch);
            DrawAlignedText();
            DrawColoredAndStyledText();

            batch.End();

            base.OnDrawing(time);
        }

        protected override void OnLoadingContent()
        {
            GameUtils.UpdateCaption();
            Contract.Require(App.content, nameof(App.content));

            var uvContent = App.context.GetContent();

            OnLoadingContentManifests(uvContent);
            OnLoadingContentAssets(uvContent);
            OnLoadingInputs();
            OnLoadingExtra();

            base.OnLoadingContent();
        }

        protected override void OnShutdown()
        {
            OnSavingInputs();

            base.OnShutdown();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            player.Update(time);
            camera.SetPosition(player);

            base.OnUpdating(time);
        }

        private void DrawAlignedText()
        {
            var window = App.window;
            var size = new Size2(window.DrawableSize.Width, window.DrawableSize.Height);
            var width = size.Width;
            var height = size.Height;

            var settingsTopLeft = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignTop | TextFlags.AlignLeft);
            textRenderer.Draw(batch, "Aligned top left", Vector2.Zero, Color.White, settingsTopLeft);

            var settingsTopCenter = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignTop | TextFlags.AlignCenter);
            textRenderer.Draw(batch, "Aligned top center", Vector2.Zero, Color.White, settingsTopCenter);

            var settingsTopRight = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignTop | TextFlags.AlignRight);
            textRenderer.Draw(batch, "Aligned top right", Vector2.Zero, Color.White, settingsTopRight);

            var settingsBottomLeft = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignBottom | TextFlags.AlignLeft);
            textRenderer.Draw(batch, "Aligned bottom left", Vector2.Zero, Color.White, settingsBottomLeft);

            var settingsBottomCenter = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignBottom | TextFlags.AlignCenter);
            textRenderer.Draw(batch, "Aligned bottom center", Vector2.Zero, Color.White, settingsBottomCenter);

            var settingsBottomRight = new TextLayoutSettings(spriteFontSegoe, width, height, TextFlags.AlignBottom | TextFlags.AlignRight);
            textRenderer.Draw(batch, "Aligned bottom right", Vector2.Zero, Color.White, settingsBottomRight);
        }

        private void DrawColoredAndStyledText()
        {
            var window = App.window;
            var size = new Size2(window.DrawableSize.Width, window.DrawableSize.Height);
            var width = size.Width;
            var height = size.Height;

            if (textLayoutCommands.Settings.Width != width || textLayoutCommands.Settings.Height != height)
            {
                const string text =
                    "Formatting Commands\n" +
                    "\n" +
                    "||c:AARRGGBB| - Changes the color of text.\n" +
                    "|c:FFFF0000|red|c| |c:FFFF8000|orange|c| |c:FFFFFF00|yellow|c| |c:FF00FF00|green|c| |c:FF0000FF|blue|c| |c:FF6F00FF|indigo|c| |c:FFFF00FF|magenta|c|\n" +
                    "\n" +
                    "||font:name| - Changes the current font.\n" +
                    "We can |font:segoe|transition to a completely different font|font| within a single line\n" +
                    "\n" +
                    "||b| and ||i| - Changes the current font style.\n" +
                    "|b|bold|b| |i|italic|i| |b||i|bold italic|i||b|\n" +
                    "\n" +
                    "||style:name| - Changes to a preset style.\n" +
                    "|style:preset1|this is preset1|style| |style:preset2|this is preset2|style|\n" +
                    "\n" +
                    "||icon:name| - Draws an icon in the text.\n" +
                    "[|icon:ok| OK] [|icon:cancel| Cancel]";

                var settings = new TextLayoutSettings(spriteFontGaramond, width, height, TextFlags.AlignMiddle | TextFlags.AlignCenter);
                textRenderer.CalculateLayout(text, textLayoutCommands, settings);
            }

            textRenderer.Draw(batch, textLayoutCommands, Vector2.Zero, Color.White);
        }

        private string GetInputsPath() => Path.Combine(GetRoamingApplicationSettingsDirectory(), inputBindingsName);

        private void OnLoadingContentAssets(IUltravioletContent uvContent)
        {
            uvContent.Manifests["fonts"]["all"].PopulateAssetLibrary(typeof(AssetFontID));
            uvContent.Manifests["sprites"]["all"].PopulateAssetLibrary(typeof(AssetSpriteID));
            uvContent.Manifests["hud"]["all"].PopulateAssetLibrary(typeof(AssetHudID));
        }

        private void OnLoadingContentManifests(IUltravioletContent uvContent)
            => uvContent.Manifests.Load(App.content.GetAssetFilePathsInDirectory("manifests"));

        private void OnLoadingExtra()
        {
            batch = SpriteBatch.Create();
            camera = new Camera(App.size);
            player = new Player();
            spriteFontGaramond = GameUtils.LoadContent<SpriteFont>(AssetFontID.Garamond);
            spriteFontSegoe = GameUtils.LoadContent<SpriteFont>(AssetFontID.SegoeUI);

            var ok = GameUtils.LoadContent<Sprite>(AssetHudID.Ok);
            var cancel = GameUtils.LoadContent<Sprite>(AssetHudID.Cancel);

            textRenderer = new TextRenderer();
            textRenderer.RegisterFont("garamond", spriteFontGaramond);
            textRenderer.RegisterFont("segoe", spriteFontSegoe);
            textRenderer.RegisterStyle("preset1", new TextStyle(spriteFontGaramond, true, null, Color.Lime));
            textRenderer.RegisterStyle("preset2", new TextStyle(spriteFontSegoe, null, true, Color.Red));
            textRenderer.RegisterIcon("ok", ok[0]);
            textRenderer.RegisterIcon("cancel", cancel[0]);

            camera.AttachToEntity(player);
        }

        private void OnLoadingInputs() => App.context.GetInput().GetHotkeys().Load(GetInputsPath(), false);

        private void OnSavingInputs() => App.context.GetInput().GetHotkeys().Save(GetInputsPath());
    }
}
