using System;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.OpenGL;

namespace org.loesoft.rotmg.ultra.core
{
    public class Game : UltravioletApplication
    {
        private ContentManager content;

        public Game() : base("LoESoft Games", "R-Ultra")
        {
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing) SafeDispose.DisposeRef(ref content);

            base.Dispose(disposing);
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            PopulateConfiguration(configuration);

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

        protected override void OnLoadingContent()
        {
            content = ContentManager.Create("Content");

            // TODO: Load content here

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            // TODO: Update the game state

            base.OnUpdating(time);
        }
    }
}
