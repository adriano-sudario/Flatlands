using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Inputs
{
    public abstract class GameInput
    {
        public enum AttachedTo { Keyboard, ControllerOne, ControllerTwo, ControllerThree, ControllerFour }
        public enum InputType { Keyboard, GamePad }
        public enum CommandState { Started, Happening, Ended }

        public class CommandArgs
        {
            public InputType From;
            public CommandState State;
        }

        public class JumpCommandArgs : CommandArgs
        {
            public VerticalDirection InitialBoostDirection;
        }

        public class MovementCommandArgs : CommandArgs
        {
            public HorizontalDirection? HorizontalDirection;
            public VerticalDirection? VerticalDirection;
        }

        public class AimCommandArgs : CommandArgs
        {
            public float AngleDegree;
            public float AngleRadian;
            public bool IsMovingThumbstick;
        }

        public class ShootCommandArgs : CommandArgs
        {

        }

        private CommandsValidation commandsValidation;

        public event CommandsValidation OnValidationBegin;
        public event CommandsValidation OnValidationEnd;

        public delegate void CommandsValidation();
        public delegate void Command(object sender, CommandArgs args);
        public delegate void MovementCommand(object sender, MovementCommandArgs args);
        public delegate void JumpCommand(object sender, JumpCommandArgs args);
        public delegate void AimCommand(object sender, AimCommandArgs args);
        public delegate void ShootCommand(object sender, ShootCommandArgs args);
        protected JumpCommand onJumpCommand;
        protected MovementCommand onMovementCommand;
        protected Command onIdleCommand;
        protected AimCommand onAimCommand;
        protected ShootCommand onShootCommand;

        public event JumpCommand OnJumpCommand
        {
            remove
            {
                commandsValidation -= JumpCommandValidation;
                onJumpCommand -= value;
            }
            add
            {
                AddValidation("JumpCommandValidation", JumpCommandValidation);
                onJumpCommand += value;
            }
        }

        public event Command OnIdleCommand
        {
            remove
            {
                commandsValidation -= IdleCommandValidation;
                onIdleCommand -= value;
            }
            add
            {
                AddValidation("IdleCommandValidation", IdleCommandValidation);
                onIdleCommand += value;
            }
        }

        public event AimCommand OnAimCommand
        {
            remove
            {
                commandsValidation -= AimCommandValidation;
                onAimCommand -= value;
            }
            add
            {
                AddValidation("AimCommandValidation", AimCommandValidation);
                onAimCommand += value;
            }
        }

        public event ShootCommand OnShootCommand
        {
            remove
            {
                commandsValidation -= ShootCommandValidation;
                onShootCommand -= value;
            }
            add
            {
                AddValidation("ShootCommandValidation", ShootCommandValidation);
                onShootCommand += value;
            }
        }

        public event MovementCommand OnMovementCommand
        {
            remove
            {
                commandsValidation -= MovementCommandValidation;
                onMovementCommand -= value;
            }
            add
            {
                AddValidation("MovementCommandValidation", MovementCommandValidation);
                onMovementCommand += value;
            }
        }

        private void AddValidation(string methodName, CommandsValidation method)
        {
            if (!HasAlreadyAddedValidation(methodName))
                commandsValidation += method;
        }

        private bool HasAlreadyAddedValidation(string methodName)
        {
            if (commandsValidation == null)
                return false;

            return (from d in commandsValidation.GetInvocationList()
                    where d.Method.Name == methodName
                    select d).Any();
        }

        public void Update()
        {
            GetInputs();

            OnValidationBegin?.Invoke();
            commandsValidation?.Invoke();
            OnValidationEnd?.Invoke();
        }

        protected abstract void GetInputs();
        protected abstract void JumpCommandValidation();
        protected abstract void MovementCommandValidation();
        protected abstract void IdleCommandValidation();
        protected abstract void AimCommandValidation();
        protected abstract void ShootCommandValidation();
    }
}
