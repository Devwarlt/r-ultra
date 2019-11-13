using org.loesoft.rotmg.ultra.core.assets;
using org.loesoftgames.rotmg.rultra;
using System;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoft.rotmg.ultra.core
{
    public class Game : UltravioletApplication
    {
        private SpriteBatch batch;
        private OpenGLUltravioletConfiguration config;
        private ContentManager core;
        private Sprite sprite;

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
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            batch.DrawSprite(sprite[0].Controller, new Vector2(App.size.Width / 2, App.size.Height / 2));
            batch.End();

            base.OnDrawing(time);
        }

        protected override void OnLoadingContent()
        {
            core = ContentManager.Create("core/assets");

            GameUtils.UpdateCaption();

            OnLoadingContentManifests();
            OnLoadingSprites();

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            sprite[0].Controller.Update(time);

            base.OnUpdating(time);
        }

        private void OnLoadingContentManifests()
        {
            Contract.Require(core, nameof(core));

            var content = App.context.GetContent();
            content.Manifests.Load(core.GetAssetFilePathsInDirectory("manifests"));
            content.Manifests["assets"]["sprites"].PopulateAssetLibrary(typeof(GlobalSpriteID));
        }

        private void OnLoadingSprites()
        {
            sprite = core.Load<Sprite>(GlobalSpriteID.SampleWizard);
            batch = SpriteBatch.Create();
        }
    }
}
