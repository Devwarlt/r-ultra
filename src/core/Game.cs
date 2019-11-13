using System;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.OpenGL;
using Ultraviolet.Presentation;
using Ultraviolet.Presentation.Styles;

namespace org.loesoft.rotmg.ultra.core
{
    public class Game : UltravioletApplication
    {
        private ContentManager core;

        public Game() : base("LoESoft Games", "R-Ultra")
        {
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing) SafeDispose.DisposeRef(ref core);

            base.Dispose(disposing);
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();

            PopulateConfiguration(configuration);

            PresentationFoundation.Configure(configuration);
#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) => System.Diagnostics.Debug.WriteLine(message);
#endif
            return new OpenGLUltravioletContext(this, configuration);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            // TODO: Draw the game

            base.OnDrawing(time);
        }

        private GlobalStyleSheet globalStyleSheet;

        protected override void OnLoadingContent()
        {
            core = ContentManager.Create("core");

            var upf = Ultraviolet.GetUI().GetPresentationFoundation();
            upf.RegisterKnownTypes(GetType().Assembly);

            //globalStyleSheet = GlobalStyleSheet.Create();
            //globalStyleSheet.Append(content, "core/gui/style/<sheet>");

            //upf.SetGlobalStyleSheet(globalStyleSheet);
            upf.CompileExpressionsIfSupported("core");

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            // TODO: Update the game state

            base.OnUpdating(time);
        }
    }
}
