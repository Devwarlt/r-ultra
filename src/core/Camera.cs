using org.loesoft.rotmg.ultra.core.entities;
using Ultraviolet;

namespace org.loesoft.rotmg.ultra.core
{
    public sealed class Camera
    {
        private readonly Size2 screenSize;

        private Entity entity;
        private Vector2 position = Vector2.Zero;

        public Camera(Size2 screenSize) => this.screenSize = new Size2(screenSize.Width / 2, screenSize.Height / 2);

        public void AttachToEntity(Entity entity) => this.entity = entity;

        public Matrix GetMatrix()
            => Matrix.CreateTranslation(-position.X, -position.Y, 0) *
            Matrix.CreateTranslation(screenSize.Width, screenSize.Height, 1);

        public void SetPosition(Entity entity) => position = entity.GetPosition();
    }
}
