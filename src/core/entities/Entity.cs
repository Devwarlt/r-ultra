using org.loesoftgames.rotmg.rultra;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics.Graphics2D;

namespace org.loesoft.rotmg.ultra.core.entities
{
    public abstract class Entity
    {
        protected readonly Hotkeys hotkeys;
        protected Vector2 position = Vector2.Zero;

        private static readonly Vector2 scale = new Vector2(5f, 5f);

        private readonly Size2 size;
        private readonly Sprite sprite;

        protected Entity(AssetID asset)
        {
            hotkeys = App.context.GetInput().GetHotkeys();
            sprite = GameUtils.LoadContent<Sprite>(asset);

            var controller = GetAnimationController();

            size = new Size2(controller.Width, controller.Height);
        }

        protected MoveState moveState { get; private set; } = MoveState.Idle;

        public void Draw(SpriteBatch batch) =>
            batch.DrawScaledSprite(sprite[0].Controller, position, scale);

        public Vector2 GetPosition() => position;

        public Size2 GetSpriteSize() => size;

        public virtual void Update(UltravioletTime time)
        {
            GetAnimationController().Update(time);

            if (isMoving())
            {
                moveState = MoveState.Moving;
                Move();
            }
            else moveState = MoveState.Idle;
        }

        protected abstract void Move();

        protected void SetPosition(float x, float y) => position = new Vector2(x, y);

        private SpriteAnimationController GetAnimationController() => sprite[0].Controller;

        private bool isMoving()
            => hotkeys.moveDown.IsPressed(false)
            || hotkeys.moveLeft.IsPressed(false)
            || hotkeys.moveRight.IsPressed(false)
            || hotkeys.moveUp.IsPressed(false);
    }
}
