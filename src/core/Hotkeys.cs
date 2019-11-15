using Ultraviolet;
using Ultraviolet.Input;

namespace org.loesoft.rotmg.ultra.core
{
    public sealed class Hotkeys : InputActionCollection
    {
        public Hotkeys(UltravioletContext uv) : base(uv)
        {
        }

        public static Hotkeys singleton { get; } = CreateSingleton<Hotkeys>();

        public InputAction moveDown { get; private set; }
        public InputAction moveLeft { get; private set; }
        public InputAction moveRight { get; private set; }
        public InputAction moveUp { get; private set; }

        protected override void OnCreatingActions()
        {
            moveDown = CreateAction("MOVE_DOWN");
            moveLeft = CreateAction("MOVE_LEFT");
            moveRight = CreateAction("MOVE_RIGHT");
            moveUp = CreateAction("MOVE_UP");

            base.OnCreatingActions();
        }

        protected override void OnResetting()
        {
            moveDown.Primary = CreateKeyboardBinding(Key.S);
            moveLeft.Primary = CreateKeyboardBinding(Key.A);
            moveRight.Primary = CreateKeyboardBinding(Key.D);
            moveUp.Primary = CreateKeyboardBinding(Key.W);

            base.OnResetting();
        }
    }
}
