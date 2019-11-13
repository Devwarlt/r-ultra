using org.loesoftgames.rotmg.rultra;
using System;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoft.rotmg.ultra.core
{
    public class Game : UltravioletApplication
    {
        private OpenGLUltravioletConfiguration config;
        private ContentManager core;

        public Game(string company, string application)
            : base(company, application)
        {
        }

        public void Configure(ref OpenGLUltravioletContext context, out IUltravioletWindow window)
        {
            config = new OpenGLUltravioletConfiguration();
#if DEBUG
            config.Debug = true;
            config.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            config.DebugCallback = (uv, level, message) => System.Diagnostics.Debug.WriteLine(message);
#endif
            context = new OpenGLUltravioletContext(this, config);
            window = context.GetPlatform().Windows.GetPrimary();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing) SafeDispose.DisposeRef(ref core);

            base.Dispose(disposing);
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            PopulateConfiguration(config);

            return App.context;
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            // TODO: Draw the game

            base.OnDrawing(time);
        }

        //private GlobalStyleSheet globalStyleSheet;

        protected override void OnLoadingContent()
        {
            core = ContentManager.Create("core");

            GameUtils.UpdateCaption();

            //globalStyleSheet = GlobalStyleSheet.Create();
            //globalStyleSheet.Append(content, "core/gui/style/<sheet>");

            //upf.SetGlobalStyleSheet(globalStyleSheet);

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            // TODO: Update the game state

            base.OnUpdating(time);
        }
    }
}
