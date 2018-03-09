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
    class Gonzalo : Gunslinger
    {
        //private static Animation walkinAnimation
        //{
        //    get
        //    {
        //        return new Animation()
        //        {
        //            Name = Action.Walking.ToString(),
        //            Frames = new List<Animation.Frame>()
        //            {
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 0,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 2,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 3,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                }
        //            }
        //        };
        //    }
        //}

        //private static Animation idleAnimation
        //{
        //    get
        //    {
        //        return new Animation()
        //        {
        //            Name = Action.Idle.ToString(),
        //            Frames = new List<Animation.Frame>()
        //            {
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 4,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 250
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 5,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 250
        //                }
        //            }
        //        };
        //    }
        //}

        //private static Animation jumpingAnimation
        //{
        //    get
        //    {
        //        return new Animation()
        //        {
        //            Name = Action.Jumping.ToString(),
        //            Frames = new List<Animation.Frame>()
        //            {
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 6,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                }
        //            }
        //        };
        //    }
        //}

        //private static Animation fallingAnimation
        //{
        //    get
        //    {
        //        return new Animation()
        //        {
        //            Name = Action.Falling.ToString(),
        //            Frames = new List<Animation.Frame>()
        //            {
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 7,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                }
        //            }
        //        };
        //    }
        //}

        //private static Animation dyingAnimation
        //{
        //    get
        //    {
        //        return new Animation()
        //        {
        //            Name = Action.DroppingDead.ToString(),
        //            Frames = new List<Animation.Frame>()
        //            {
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 8,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 9,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 10,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                },
        //                new Animation.Frame()
        //                {
        //                    Source = new Rectangle()
        //                    {
        //                        X = 48 * 11,
        //                        Y = 0,
        //                        Width = 48,
        //                        Height = 48
        //                    },
        //                    Duration = 100
        //                }
        //            }
        //        };
        //    }
        //}

        private static AnimatedSprite animations
        {
            get
            {
                Animation idleAnimation = Animation.GetFromAtlas(Action.Walking.ToString(), 0, 0, 48, 48, 2, 100);
                Animation walkinAnimation = Animation.GetFromAtlas(Action.Idle.ToString(), 2, 0, 48, 48, 2, 250);
                Animation jumpingAnimation = Animation.GetFromAtlas(Action.Jumping.ToString(), 4, 0, 48, 48, 1, 100);
                Animation fallingAnimation = Animation.GetFromAtlas(Action.Falling.ToString(), 5, 0, 48, 48, 1, 100);
                Animation dyingAnimation = Animation.GetFromAtlas(
                    Action.DroppingDead.ToString(), 6, 0, 48, 48, 4, 100, hasLoop: false);
                Animation pullingOutTheGunAnimation = Animation.GetFromAtlas(
                    Action.PullingOutTheGun.ToString(), 10, 0, 48, 48, 2, 100);
                
                Animation aimingUpAnimation = Animation.GetFromAtlas(
                    Action.AimingUp.ToString(), 13, 0, 48, 48, 1, 250);
                Animation aimingDiagonalUp = Animation.GetFromAtlas(
                    Action.AimingDiagonalUp.ToString(), 14, 0, 48, 48, 1, 250);
                Animation aimingFrontAnimation = Animation.GetFromAtlas(
                    Action.AimingFront.ToString(), 15, 0, 48, 48, 1, 250);
                Animation aimingDiagonalDown = Animation.GetFromAtlas(
                    Action.AimingDiagonalDown.ToString(), 16, 0, 48, 48, 1, 250);
                Animation aimingDown = Animation.GetFromAtlas(
                    Action.AimingDown.ToString(), 17, 0, 48, 48, 1, 250);

                return new AnimatedSprite(
                    new List<Animation>() {
                        idleAnimation, walkinAnimation, jumpingAnimation, dyingAnimation, fallingAnimation,
                        pullingOutTheGunAnimation, aimingUpAnimation, aimingDiagonalUp, aimingFrontAnimation,
                        aimingDiagonalDown, aimingDown
                    })
                {
                    Name = "Gonzalo",
                    X = 380,
                    Y = 349,
                    Origin = new Vector2(16, 10)
                };
            }
        }

        public Gonzalo() :
            base(animations)
        {
            Width = 20;
            Height = 35;
        }

        public Gonzalo(GameInput.AttachedTo inputtAttached) :
            base(animations, inputtAttached)
        {
            Width = 20;
            Height = 35;
        }
    }
}
