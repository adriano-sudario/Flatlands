using Flatlands.Drawings;
using Flatlands.Entities.Types;
using Flatlands.Inputs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Entities.Characters
{
    class Joey : Gunslinger
    {
        private static AnimatedSprite animations
        {
            get
            {
                Animation idleAnimation = Animation.GetFromAtlas(Action.Walking.ToString(), 0, 2, 48, 48, 2, 100);
                Animation walkinAnimation = Animation.GetFromAtlas(Action.Idle.ToString(), 2, 2, 48, 48, 2, 250);
                Animation jumpingAnimation = Animation.GetFromAtlas(Action.Jumping.ToString(), 4, 2, 48, 48, 1, 100);
                Animation fallingAnimation = Animation.GetFromAtlas(Action.Falling.ToString(), 5, 2, 48, 48, 1, 100);
                Animation dyingAnimation = Animation.GetFromAtlas(
                    Action.DroppingDead.ToString(), 6, 2, 48, 48, 4, 100, hasLoop: false);
                Animation pullingOutTheGunAnimation = Animation.GetFromAtlas(
                    Action.PullingOutTheGun.ToString(), 10, 2, 48, 48, 2, 100);

                Animation aimingUpAnimation = Animation.GetFromAtlas(
                    Action.AimingUp.ToString(), 13, 2, 48, 48, 1, 250);
                Animation aimingDiagonalUp = Animation.GetFromAtlas(
                    Action.AimingDiagonalUp.ToString(), 14, 2, 48, 48, 1, 250);
                Animation aimingFrontAnimation = Animation.GetFromAtlas(
                    Action.AimingFront.ToString(), 15, 2, 48, 48, 1, 250);
                Animation aimingDiagonalDown = Animation.GetFromAtlas(
                    Action.AimingDiagonalDown.ToString(), 16, 2, 48, 48, 1, 250);
                Animation aimingDown = Animation.GetFromAtlas(
                    Action.AimingDown.ToString(), 17, 2, 48, 48, 1, 250);

                return new AnimatedSprite(
                    new List<Animation>() {
                        idleAnimation, walkinAnimation, jumpingAnimation, dyingAnimation, fallingAnimation,
                        pullingOutTheGunAnimation, aimingUpAnimation, aimingDiagonalUp, aimingFrontAnimation,
                        aimingDiagonalDown, aimingDown
                    })
                {
                    Name = "Joey Patife",
                    X = 380,
                    Y = 349,
                    Origin = new Vector2(16, 10)
                };
            }
        }

        public Joey() :
            base(animations)
        {
            Width = 20;
            Height = 35;
        }

        public Joey(GameInput.AttachedTo inputtAttached) :
            base(animations, inputtAttached)
        {
            Width = 20;
            Height = 35;
        }
    }
}
