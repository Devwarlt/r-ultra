using org.loesoft.rotmg.ultra.core.assets.entities;
using Ultraviolet;

namespace org.loesoft.rotmg.ultra.core.assets
{
    public sealed class Camera
    {
        private const float scale = 5f;

        private readonly Size2 screenSize;

        private Entity entity;
        private Vector2 position = Vector2.Zero;
        private Size2 spriteSize = Size2.Zero;

        public Camera(Size2 screenSize) => this.screenSize = new Size2(screenSize.Width / 2, screenSize.Height / 2);

        public void AttachToEntity(Entity entity)
        {
            this.entity = entity;

            var spriteSize = entity.GetSpriteSize();

            this.spriteSize = new Size2(spriteSize.Width / 2, spriteSize.Height / 2);
        }

        public Matrix GetMatrix()
            => Matrix.CreateTranslation(-(position.X + spriteSize.Width), -(position.Y + spriteSize.Height), 0) *
            Matrix.CreateScale(scale) *
            Matrix.CreateTranslation(screenSize.Width, screenSize.Height, 1);

        public void SetPosition(Entity entity) => position = entity.GetPosition();
    }
}
