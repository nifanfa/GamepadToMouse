using Loamen.KeyMouseHook;
using SharpDX.XInput;
using System.Threading;

namespace WindowsFormsApp1
{
    class GamepadToMouse
    {
        private const int MomentDivider = 2500;
        private const int ScrollDivider = 5000;
        private Controller controller;
        private IMouseSimulator mouseSimulator;

        private Timer timer;

        private bool wasADown;
        private bool wasBDown;

        private int RefreshRate = 480;

        public GamepadToMouse()
        {
            controller = new Controller(UserIndex.One);
            mouseSimulator = new InputSimulator().Mouse;
            timer = new Timer(obj => Update());
        }

        public void Start()
        {
            timer.Change(0, 1000 / RefreshRate);
        }

        public bool Enabled = true;

        private void Update()
        {
            controller.GetState(out var state);

            if (Enabled)
            {
                Movement(state);
                Scroll(state);
                LeftButton(state);
                RightButton(state);
            }
        }

        private void RightButton(State state)
        {
            var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
            if (isBDown && !wasBDown) mouseSimulator.RightButtonDown();
            if (!isBDown && wasBDown) mouseSimulator.RightButtonUp();
            wasBDown = isBDown;
        }

        private void LeftButton(State state)
        {
            var isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
            if (isADown && !wasADown) mouseSimulator.LeftButtonDown();
            if (!isADown && wasADown) mouseSimulator.LeftButtonUp();
            wasADown = isADown;
        }

        private void Scroll(State state)
        {
            var x = state.Gamepad.RightThumbX / ScrollDivider;
            var y = state.Gamepad.RightThumbY / ScrollDivider;

            mouseSimulator.HorizontalScroll(x);
            mouseSimulator.VerticalScroll(y);
        }

        private void Movement(State state)
        {
            var x = state.Gamepad.LeftThumbX / MomentDivider;
            var y = state.Gamepad.LeftThumbY / MomentDivider;

            mouseSimulator.MoveMouseBy(x, -y);
        }
    }
}