using Flatlands.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Inputs
{
    class GamePadInput : GameInput
    {
        private GamePadState currentGamePadState;
        private GamePadState previousGamePadState;

        private PlayerIndex index;

        public GamePadInput(PlayerIndex index)
        {
            this.index = index;
        }

        protected override void GetInputs()
        {
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(index);
        }

        protected override void JumpCommandValidation()
        {
            JumpCommandArgs args = null;

            if (currentGamePadState.Buttons.A == ButtonState.Pressed &&
                previousGamePadState.Buttons.A == ButtonState.Released)
            {
                args = new JumpCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Started
                };

                if (currentGamePadState.DPad.Down == ButtonState.Pressed ||
                    currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                    args.InitialBoostDirection = VerticalDirection.Down;
                else
                    args.InitialBoostDirection = VerticalDirection.Up;
            }
            else if (currentGamePadState.Buttons.A == ButtonState.Pressed)
            {
                args = new JumpCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Happening
                };
            }
            else if (currentGamePadState.Buttons.A == ButtonState.Released &&
                previousGamePadState.Buttons.A == ButtonState.Pressed)
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
            if (currentGamePadState.DPad.Left != ButtonState.Pressed &&
                currentGamePadState.IsButtonUp(Buttons.LeftThumbstickLeft) &&
                currentGamePadState.DPad.Right != ButtonState.Pressed &&
                currentGamePadState.IsButtonUp(Buttons.LeftThumbstickRight) &&
                currentGamePadState.DPad.Up != ButtonState.Pressed &&
                currentGamePadState.IsButtonUp(Buttons.LeftThumbstickUp) &&
                currentGamePadState.DPad.Down != ButtonState.Pressed &&
                currentGamePadState.IsButtonUp(Buttons.LeftThumbstickDown))
                onIdleCommand?.Invoke(this, new CommandArgs() { From = InputType.GamePad });
        }

        protected override void AimCommandValidation()
        {
            AimCommandArgs args = null;

            if (currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                previousGamePadState.Buttons.RightShoulder == ButtonState.Released)
            {
                args = new AimCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Started
                };
            }
            else if (currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                args = new AimCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Happening
                };
            }
            else if (currentGamePadState.Buttons.RightShoulder == ButtonState.Released &&
                previousGamePadState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                args = new AimCommandArgs()
                {
                    From = InputType.Keyboard,
                    State = CommandState.Ended
                };
            }

            if (args != null)
            {
                if (args.State == CommandState.Started || args.State == CommandState.Happening)
                {
                    args.AngleRadian = (float)Math.Atan2(currentGamePadState.ThumbSticks.Left.Y,
                        currentGamePadState.ThumbSticks.Left.X);
                    args.AngleDegree = MathHelper.ToDegrees(args.AngleRadian);
                }

                args.IsMovingThumbstick = currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown) ||
                    currentGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                    currentGamePadState.IsButtonDown(Buttons.LeftThumbstickRight) ||
                    currentGamePadState.IsButtonDown(Buttons.LeftThumbstickUp);

                onAimCommand?.Invoke(this, args);
            }
        }

        protected override void MovementCommandValidation()
        {
            MovementCommandArgs args = new MovementCommandArgs()
            {
                From = InputType.GamePad
            };

            if (currentGamePadState.DPad.Left == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
                args.HorizontalDirection = HorizontalDirection.Left;
            else if (currentGamePadState.DPad.Right == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
                args.HorizontalDirection = HorizontalDirection.Right;

            if (currentGamePadState.DPad.Up == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
                args.VerticalDirection = VerticalDirection.Up;
            else if (currentGamePadState.DPad.Down == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                args.VerticalDirection = VerticalDirection.Down;

            if (args.HorizontalDirection.HasValue || args.VerticalDirection.HasValue)
                onMovementCommand?.Invoke(this, args);
        }

        protected override void ShootCommandValidation()
        {
            ShootCommandArgs args = null;

            if (currentGamePadState.Buttons.X == ButtonState.Pressed &&
                previousGamePadState.Buttons.X == ButtonState.Released)
                args = new ShootCommandArgs() { From = InputType.Keyboard, State = CommandState.Started };
            else if (currentGamePadState.Buttons.X == ButtonState.Pressed)
                args = new ShootCommandArgs() { From = InputType.Keyboard, State = CommandState.Happening };
            else if (currentGamePadState.Buttons.X == ButtonState.Released &&
                previousGamePadState.Buttons.X == ButtonState.Pressed)
                args = new ShootCommandArgs() { From = InputType.Keyboard, State = CommandState.Ended };

            if (args == null)
                return;

            onShootCommand?.Invoke(this, args);
        }
    }
}
