using org.loesoftgames.rotmg.rultra;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics.Graphics2D;

namespace org.loesoft.rotmg.ultra.core.assets.entities
{
    public abstract class Entity
    {
        protected Vector2 position = Vector2.Zero;

        private readonly Size2 size;
        private readonly Sprite sprite;

        protected Entity(AssetID asset)
        {
            sprite = App.content.Load<Sprite>(asset);

            var controller = GetAnimationController();

            size = new Size2(controller.Width, controller.Height);
        }

        public void Draw(SpriteBatch batch) => batch.DrawSprite(sprite[0].Controller, position);

        public Vector2 GetPosition() => position;

        public Size2 GetSpriteSize() => size;

        public void Update(UltravioletTime time) => GetAnimationController().Update(time);

        protected abstract void Move();

        protected void SetPosition(float x, float y) => position = new Vector2(x, y);

        private SpriteAnimationController GetAnimationController() => sprite[0].Controller;
    }
}
