//using Flatlands.Entities.Types;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Flatlands.Inputs
//{
//    class GunslingerGamePadInput : GunslingerInput
//    {
//        private GamePadState currentGamePadState;
//        private GamePadState previousGamePadState;

//        private PlayerIndex index;

//        public GunslingerGamePadInput(PlayerIndex index)
//        {
//            this.index = index;
//        }

//        protected override void GetInputs()
//        {
//            previousGamePadState = currentGamePadState;
//            currentGamePadState = GamePad.GetState(index);
//        }

//        protected override void JumpCommandValidation(Gunslinger player)
//        {
//            if (currentGamePadState.Buttons.A == ButtonState.Pressed &&
//                previousGamePadState.Buttons.A == ButtonState.Released)
//            {
//                JumpCommandArgs args = new JumpCommandArgs() { From = InputType.Keyboard };

//                if (currentGamePadState.DPad.Down == ButtonState.Pressed ||
//                    currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
//                    args.VerticalDirection = VerticalDirection.Down;
//                else
//                    args.VerticalDirection = VerticalDirection.Up;

//                onJumpCommand?.Invoke(this, args);
//            }
//        }

//        protected override void MovementCommandValidation(Gunslinger player)
//        {
//            MovementCommandArgs args = new MovementCommandArgs()
//            {
//                From = InputType.GamePad
//            };

//            if (currentGamePadState.DPad.Left == ButtonState.Pressed ||
//                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
//                args.HorizontalDirection = HorizontalDirection.Left;
//            else if (currentGamePadState.DPad.Right == ButtonState.Pressed ||
//                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
//                args.HorizontalDirection = HorizontalDirection.Right;

//            if (currentGamePadState.DPad.Up == ButtonState.Pressed ||
//                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
//                args.VerticalDirection = VerticalDirection.Up;
//            else if (currentGamePadState.DPad.Down == ButtonState.Pressed ||
//                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
//                args.VerticalDirection = VerticalDirection.Down;

//            if (args.HorizontalDirection.HasValue || args.VerticalDirection.HasValue)
//                onMovementCommand?.Invoke(this, args);
//        }
//    }
//}
