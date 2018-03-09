//using Flatlands.Entities.Types;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Flatlands.Inputs
//{
//    class GunslingerKeyboardInput : GunslingerInput
//    {
//        private KeyboardState currentKeyboardState;
//        private KeyboardState previousKeyboardState;

//        Keys leftKey;
//        Keys rightKey;
//        Keys upKey;
//        Keys downKey;

//        Keys selectKey;

//        public GunslingerKeyboardInput()
//        {
//            SetDefaultKeys();
//        }

//        public void SetDefaultKeys()
//        {
//            leftKey = Keys.Left;
//            rightKey = Keys.Right;
//            upKey = Keys.Up;
//            downKey = Keys.Down;

//            selectKey = Keys.Space;
//        }

//        protected override void GetInputs()
//        {
//            previousKeyboardState = currentKeyboardState;
//            currentKeyboardState = Keyboard.GetState();
//        }

//        protected override void JumpCommandValidation(Gunslinger player)
//        {
//            if (currentKeyboardState.IsKeyDown(selectKey) &&
//                previousKeyboardState.IsKeyUp(selectKey))
//            {
//                JumpCommandArgs args = new JumpCommandArgs() { From = InputType.Keyboard };
//                args.VerticalDirection = currentKeyboardState.IsKeyDown(downKey) ?
//                    VerticalDirection.Down : VerticalDirection.Up;

//                onJumpCommand?.Invoke(this, args);
//            }
//        }

//        protected override void MovementCommandValidation(Gunslinger player)
//        {
//            MovementCommandArgs args = new MovementCommandArgs()
//            {
//                From = InputType.Keyboard
//            };

//            if (currentKeyboardState.IsKeyDown(leftKey))
//                args.HorizontalDirection = HorizontalDirection.Left;
//            else if (currentKeyboardState.IsKeyDown(rightKey))
//                args.HorizontalDirection = HorizontalDirection.Right;

//            if (currentKeyboardState.IsKeyDown(upKey))
//                args.VerticalDirection = VerticalDirection.Up;
//            else if (currentKeyboardState.IsKeyDown(downKey))
//                args.VerticalDirection = VerticalDirection.Down;

//            if (args.HorizontalDirection.HasValue || args.VerticalDirection.HasValue)
//                onMovementCommand?.Invoke(this, args);
//        }
//    }
//}
