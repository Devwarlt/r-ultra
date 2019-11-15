using org.loesoftgames.rotmg.rultra;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace org.loesoft.rotmg.ultra.core
{
    public sealed class Game2 : UltravioletApplication
    {
        private OpenGLUltravioletConfiguration config;

        public Game2(string company, string application)
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

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            PopulateConfiguration(config);

            return App.context;
        }

        protected override void OnLoadingContent()
        {
            GameUtils.UpdateCaption();

            base.OnLoadingContent();
        }
    }
}
