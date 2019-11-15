using org.loesoft.rotmg.ultra.core.assets;
using org.loesoftgames.rotmg.rultra;
using Ultraviolet;

namespace org.loesoft.rotmg.ultra.core.entities
{
    public sealed class Player : Entity
    {
        private Size2 screenSize;

        public Player() : base(AssetSpriteID.SampleWizard)
        {
            var window = App.window;

            screenSize = new Size2(window.DrawableSize.Width, window.DrawableSize.Height);
            CentralizePosition();
        }

        public override void Update(UltravioletTime time)
        {
            var window = App.window;

            if (window.Disposed) return;

            var size = new Size2(window.DrawableSize.Width, window.DrawableSize.Height);

            if (size != screenSize)
            {
                screenSize = size;
                CentralizePosition();
            }

            base.Update(time);
        }

        protected override void Move()
        {
            if (hotkeys.moveDown.IsPressed()) SetPosition(position.X, position.Y--);
            if (hotkeys.moveLeft.IsPressed()) SetPosition(position.X--, position.Y);
            if (hotkeys.moveRight.IsPressed()) SetPosition(position.X++, position.Y);
            if (hotkeys.moveUp.IsPressed()) SetPosition(position.X, position.Y++);
        }

        private void CentralizePosition()
        {
            var spriteSize = GetSpriteSize();

            SetPosition(screenSize.Width / 2 - spriteSize.Width / 2, screenSize.Height / 2 - spriteSize.Height / 2);
        }
    }
}
