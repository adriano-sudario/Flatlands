using Flatlands.Drawings;
using Flatlands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flatlands.Inputs;
using Microsoft.Xna.Framework.Graphics;
using Flatlands.Mechanics;
using Microsoft.Xna.Framework;

namespace Flatlands.Entities.Types
{
    public class Gunslinger : VisualEntity, IControllable
    {
        private Action action;
        private HorizontalDirection side;
        private Block ground;
        private bool isAiming;

        public bool IsAiming
        {
            get { return isAiming; }
            private set
            {
                if (isAiming == value)
                    return;

                isAiming = value;

                if (isAiming)
                    Aim.Activate(this);
                else
                    Aim.Deactivate();
            }
        }

        public enum Action
        {
            Idle, Walking, Jumping, Falling, PullingOutTheGun, DroppingDead,
            AimingUp, AimingDiagonalUp, AimingFront, AimingDiagonalDown, AimingDown
        }

        public Action CurrentAction
        {
            get { return action; }
            set
            {
                if (action == value || action == Action.DroppingDead)
                    return;

                action = value;
                AnimatedSprite.CurrentAnimation = AnimatedSprite.Animations[action.ToString()];
            }
        }

        public HorizontalDirection Side
        {
            get { return side; }
            set
            {
                if (side == 0)
                {
                    side = value;
                    AnimatedSprite.Effect = side == HorizontalDirection.Left ? SpriteEffects.FlipHorizontally :
                    SpriteEffects.None;
                    if (side == HorizontalDirection.Left)
                        OriginX = SpriteBounds.Width - (OriginX + Width);
                    return;
                }

                if (side == value)
                    return;

                side = value;

                AnimatedSprite.Effect = side == HorizontalDirection.Left ? SpriteEffects.FlipHorizontally : 
                    SpriteEffects.None;

                OriginX = SpriteBounds.Width - (OriginX + Width);
            }
        }

        public AnimatedSprite AnimatedSprite
        {
            get { return ((AnimatedSprite)Sprite); }
        }

        public GameInput.AttachedTo InputAttached { get; set; }
        public GameInput Input { get { return InputAttached.Get(); } }
        public float Speed { get; set; }

        public Block Ground {
            get
            {
                return ground;
            }
            set
            {
                ground = value;

                if (ground != null)
                {
                    CurrentAction = Action.Idle;
                    GravityForceApplied = 0;
                }
                else if (IsAiming)
                {
                    IsAiming = false;
                }
            }
        }

        public Block GroundPassingThrough { get; set; }
        public float JumpForce { get { return 8f; } }
        public float StepHeight { get { return 2f; } }
        public Aim Aim { get; set; }

        public Rectangle LeftBounds
        {
            get
            {
                return new Rectangle(
                    (int)X,
                    (int)Y,
                    (int)Speed, (int)(Height - StepHeight));
            }
        }

        public Rectangle RightBounds
        {
            get
            {
                return new Rectangle(
                    (int)(X + (Width - Speed)),
                    (int)Y,
                    (int)Speed, (int)(Height - StepHeight));
            }
        }

        public Rectangle TopBounds
        {
            get
            {
                return new Rectangle(
                    (int)(X + Speed),
                    (int)Y,
                    (int)(Width - (Speed * 2)), (int)JumpForce);
            }
        }

        public Rectangle BottomBounds
        {
            get
            {
                return new Rectangle(
                    (int)(X + Speed),
                    (int)(Y + (Height - JumpForce)),
                    (int)(Width - (Speed * 2)), (int)JumpForce + 1);
            }
        }

        public float GravityForceApplied { get; set; }
        public bool HasJustShot { get; set; }
        public bool HasGun { get; set; }
        public bool IsDead { get; private set; }

        public Gunslinger(AnimatedSprite sprite, 
            bool isVisible = true) :
            base(sprite, isVisible)
        {
            Speed = 2f;
        }

        public Gunslinger(AnimatedSprite sprite, GameInput.AttachedTo inputAttached, 
            bool isVisible = true) : 
            this(sprite, isVisible)
        {
            InputAttached = inputAttached;
            AddCommands();
            Aim = new Aim();
            isAiming = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Ground == null)
            {
                if (GravityForceApplied <= 0)
                {
                    CurrentAction = Action.Falling;
                    Movement.Move(this, VerticalDirection.Down, GravityForceApplied * -1);
                }
                else
                {
                    Movement.Move(this, VerticalDirection.Up, GravityForceApplied);
                }
            }
        }

        public void Die()
        {
            IsDead = true;
            IsAiming = false;
            CurrentAction = Action.DroppingDead;
            ResetCommands();
        }

        public void FixBottomPosition(Block blockCollided)
        {
            if ((Y + GravityForceApplied) - (blockCollided.BoundingBox.Top - Height) <= StepHeight)
            {
                Y = blockCollided.BoundingBox.Top - Height;
                Ground = blockCollided;
            }
        }

        public void FixTopPosition(Block blockCollided)
        {
            if (Math.Round(Y + GravityForceApplied, MidpointRounding.AwayFromZero) - 
                blockCollided.BoundingBox.Bottom >= 0)
            {
                Y = blockCollided.BoundingBox.Bottom;
                GravityForceApplied = 0;
            }
        }

        public void FixRightPosition(Block blockCollided)
        {
            X = blockCollided.BoundingBox.Left - Width;
        }

        public void FixLeftPosition(Block blockCollided)
        {
            X = blockCollided.BoundingBox.Right;
        }

        private void OnMovementCommand(object sender, GameInput.MovementCommandArgs args)
        {
            if (IsAiming)
                return;

            if (args.HorizontalDirection.HasValue)
            {
                if ((BoundingBox.X <= 0 && args.HorizontalDirection.Value == HorizontalDirection.Left) ||
                (X + BoundingBox.Width >= FlatlandsGame.ScreenWidth && 
                args.HorizontalDirection.Value == HorizontalDirection.Right))
                {
                    if (CurrentAction == Action.Walking)
                        CurrentAction = Action.Idle;
                    return;
                }

                if (CurrentAction != Action.Jumping)
                    CurrentAction = Action.Walking;
                Side = args.HorizontalDirection.Value;
                Movement.Move(this, Side, Speed);
            }
            else if (CurrentAction != Action.Jumping && !IsAiming)
            {
                CurrentAction = Action.Idle;
            }
        }

        private void OnJumpCommand(object sender, GameInput.JumpCommandArgs args)
        {
            if (IsAiming)
                return;

            if (Ground != null && args.State == GameInput.CommandState.Started)
            {
                if (args.InitialBoostDirection == VerticalDirection.Down && Ground.Type == BlockType.Plank)
                {
                    CurrentAction = Action.Falling;
                    GravityForceApplied = -1;
                }
                else
                {
                    CurrentAction = Action.Jumping;
                    GravityForceApplied = JumpForce;
                }

                GroundPassingThrough = Ground;
                Ground = null;
            }
            else if (CurrentAction == Action.Jumping && args.State == GameInput.CommandState.Ended &&
                GravityForceApplied > JumpForce / 2)
            {
                GravityForceApplied = JumpForce / 2;
            }
        }

        private void OnIdleCommand(object sender, GameInput.CommandArgs args)
        {
            if (CurrentAction == Action.Jumping || IsAiming)
                return;

            CurrentAction = Action.Idle;
        }

        private void OnAimCommand(object sender, GameInput.AimCommandArgs args)
        {
            if (Ground == null || !HasGun)
                return;

            if (args.State == GameInput.CommandState.Started)
            {
                IsAiming = true;
                if (!args.IsMovingThumbstick)
                    Aim.AngleDegree = Side == HorizontalDirection.Left ? 180 : 0;
                else
                    Aim.AngleRadian = args.AngleRadian;
                CurrentAction = Action.PullingOutTheGun;
            }
            else if (args.State == GameInput.CommandState.Happening && IsAiming)
            {
                if (!args.IsMovingThumbstick && CurrentAction != Action.PullingOutTheGun)
                    return;

                if (!args.IsMovingThumbstick)
                    Aim.AngleDegree = Side == HorizontalDirection.Left ? 180 : 0;
                else
                    Aim.AngleRadian = args.AngleRadian;

                if (CurrentAction == Action.PullingOutTheGun && !AnimatedSprite.RestartedLoop)
                    return;

                if (Aim.AngleDegree > 90 || Aim.AngleDegree <= -90)
                    Side = HorizontalDirection.Left;
                else
                    Side = HorizontalDirection.Right;

                if (Aim.AngleDegree <= 112.5 && Aim.AngleDegree > 67.5)
                    CurrentAction = Action.AimingUp;
                else if ((Aim.AngleDegree <= 67.5 && Aim.AngleDegree > 22.5) ||
                    (args.AngleDegree > 112.5 && args.AngleDegree <= 157.5))
                    CurrentAction = Action.AimingDiagonalUp;
                else if ((Aim.AngleDegree <= 22.5 && Aim.AngleDegree > -22.5) ||
                    (Aim.AngleDegree <= 180 && Aim.AngleDegree >= 157.5) ||
                    (Aim.AngleDegree >= -180 && Aim.AngleDegree < -157.5))
                    CurrentAction = Action.AimingFront;
                else if ((Aim.AngleDegree <= -22.5 && Aim.AngleDegree > -67.5) ||
                    (Aim.AngleDegree >= -157.5 && Aim.AngleDegree < -112.5))
                    CurrentAction = Action.AimingDiagonalDown;
                else
                    CurrentAction = Action.AimingDown;
            }
            else if (args.State == GameInput.CommandState.Ended)
            {
                IsAiming = false;
            }
        }

        private void OnShootCommand(object sender, GameInput.ShootCommandArgs args)
        {
            if (IsAiming && args.State == GameInput.CommandState.Started)
                HasJustShot = true;
        }

        private void OnValidationBegin()
        {
            PreviousX = X;
            PreviousY = Y;
        }

        public void AddCommands()
        {
            Input.OnValidationBegin += OnValidationBegin;
            Input.OnJumpCommand += OnJumpCommand;
            Input.OnIdleCommand += OnIdleCommand;
            Input.OnMovementCommand += OnMovementCommand;
            Input.OnAimCommand += OnAimCommand;
            Input.OnShootCommand += OnShootCommand;
        }

        public void ResetCommands()
        {
            Input.OnValidationBegin -= OnValidationBegin;
            Input.OnJumpCommand -= OnJumpCommand;
            Input.OnIdleCommand -= OnIdleCommand;
            Input.OnMovementCommand -= OnMovementCommand;
            Input.OnAimCommand -= OnAimCommand;
            Input.OnShootCommand -= OnShootCommand;
        }
    }
}
