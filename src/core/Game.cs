using org.loesoft.rotmg.ultra.core.assets;
using org.loesoft.rotmg.ultra.core.assets.entities;
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
        private Camera camera;
        private OpenGLUltravioletConfiguration config;
        private Player player;

        public Game(string company, string application)
            : base(company, application)
        {
        }

        public void Configure(ref OpenGLUltravioletContext context, out IUltravioletWindow window, out ContentManager content)
        {
            config = new OpenGLUltravioletConfiguration();
            context = new OpenGLUltravioletContext(this, config);
            window = context.GetPlatform().Windows.GetPrimary();
            content = ContentManager.Create("core/assets");
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing) SafeDispose.Dispose(App.content);

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

            batch.End();

            base.OnDrawing(time);
        }

        protected override void OnLoadingContent()
        {
            GameUtils.UpdateCaption();

            OnLoadingContentManifests();

            batch = SpriteBatch.Create();

            OnLoadingExtra();

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            player.Update(time);
            camera.SetPosition(player);

            base.OnUpdating(time);
        }

        private void OnLoadingContentManifests()
        {
            Contract.Require(App.content, nameof(App.content));

            var uvContent = App.context.GetContent();
            uvContent.Manifests.Load(App.content.GetAssetFilePathsInDirectory("manifests"));
            uvContent.Manifests["assets"]["sprites"].PopulateAssetLibrary(typeof(GlobalSpriteID));
        }

        private void OnLoadingExtra()
        {
            camera = new Camera(App.size);
            player = new Player();

            camera.AttachToEntity(player);
        }
    }
}
