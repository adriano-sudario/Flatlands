using Flatlands.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Inputs
{
    class KeyboardInput : GameInput
    {
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        Keys leftKey;
        Keys rightKey;
        Keys upKey;
        Keys downKey;

        Keys selectKey;

        public KeyboardInput()
        {
            SetDefaultKeys();
        }

        public void SetDefaultKeys()
        {
            leftKey = Keys.Left;
            rightKey = Keys.Right;
            upKey = Keys.Up;
            downKey = Keys.Down;

            selectKey = Keys.Space;
        }

        protected override void GetInputs()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        protected override void JumpCommandValidation()
        {
            JumpCommandArgs args = null;

            if (currentKeyboardState.IsKeyDown(selectKey) &&
                previousKeyboardState.IsKeyUp(selectKey))
            {
                args = new JumpCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Started
                };

                args.InitialBoostDirection = currentKeyboardState.IsKeyDown(downKey) ?
                    VerticalDirection.Down : VerticalDirection.Up;
            }
            else if (currentKeyboardState.IsKeyDown(selectKey))
            {
                args = new JumpCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Happening
                };
            }
            else if (currentKeyboardState.IsKeyUp(selectKey) &&
                previousKeyboardState.IsKeyDown(selectKey))
            {
                args = new JumpCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Ended
                };
            }

            if (args != null)
                onJumpCommand?.Invoke(this, args);
        }

        protected override void IdleCommandValidation()
        {
            if (currentKeyboardState.IsKeyUp(leftKey) && currentKeyboardState.IsKeyUp(rightKey) &&
                currentKeyboardState.IsKeyUp(upKey) && currentKeyboardState.IsKeyUp(downKey))
                onIdleCommand?.Invoke(this, new CommandArgs() { From = InputType.GamePad });
        }

        protected override void MovementCommandValidation()
        {
            MovementCommandArgs args = new MovementCommandArgs()
            {
                From = InputType.Keyboard
            };

            if (currentKeyboardState.IsKeyDown(leftKey))
                args.HorizontalDirection = HorizontalDirection.Left;
            else if (currentKeyboardState.IsKeyDown(rightKey))
                args.HorizontalDirection = HorizontalDirection.Right;

            if (currentKeyboardState.IsKeyDown(upKey))
                args.VerticalDirection = VerticalDirection.Up;
            else if (currentKeyboardState.IsKeyDown(downKey))
                args.VerticalDirection = VerticalDirection.Down;

            if (args.HorizontalDirection.HasValue || args.VerticalDirection.HasValue)
                onMovementCommand?.Invoke(this, args);
        }

        protected override void AimCommandValidation()
        {
            throw new NotImplementedException();
        }

        protected override void ShootCommandValidation()
        {
            throw new NotImplementedException();
        }
    }
}
