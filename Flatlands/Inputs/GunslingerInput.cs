//using Flatlands.Entities.Types;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Flatlands.Inputs
//{
//    class GunslingerInput
//    {
//        public enum AttachedTo { Keyboard, ControllerOne, ControllerTwo, ControllerThree, ControllerFour }
//        public enum InputType { Keyboard, GamePad }
//        public enum CommandState { Started, Happening, Ended }

//        public class CommandArgs
//        {
//            public InputType From;
//            public CommandState State;
//        }

//        public class JumpCommandArgs : CommandArgs
//        {
//            public VerticalDirection VerticalDirection;
//        }

//        public class MovementCommandArgs : CommandArgs
//        {
//            public HorizontalDirection? HorizontalDirection;
//            public VerticalDirection? VerticalDirection;
//        }

//        private CommandsValidation commandsValidation;

//        //public event CommandsValidation OnValidationBegin;
//        //public event CommandsValidation OnValidationEnd;

//        public delegate void CommandsValidation(Gunslinger player);
//        public delegate void Command(object sender, CommandArgs args);
//        public delegate void MovementCommand(object sender, MovementCommandArgs args);
//        public delegate void JumpCommand(object sender, JumpCommandArgs args);
//        protected JumpCommand onJumpCommand;
//        protected MovementCommand onMovementCommand;

//        public event JumpCommand OnJumpCommand;
//        public event MovementCommand OnMovementCommand;

//        public GunslingerInput()
//        {
//            commandsValidation += JumpCommandValidation;
//            commandsValidation += MovementCommandValidation;
//        }

//        public void Update(Gunslinger player)
//        {
//            GetInputs();

//            //OnValidationBegin?.Invoke(player);
//            commandsValidation?.Invoke(player);
//            //OnValidationEnd?.Invoke(player);
//        }

//        protected virtual void GetInputs() { }

//        protected virtual void JumpCommandValidation(Gunslinger player) { }

//        protected virtual void MovementCommandValidation(Gunslinger player) { }
//    }
//}
