using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Flatlands.Inputs.GameInput;

namespace Flatlands.Inputs
{
    static class InputManager
    {
        public static KeyboardInput Keyboard;
        public static GamePadInput ControllerOne;
        public static GamePadInput ControllerTwo;
        public static GamePadInput ControllerThree;
        public static GamePadInput ControllerFour;

        static InputManager()
        {
            Keyboard = new KeyboardInput();
            ControllerOne = new GamePadInput(PlayerIndex.One);
            ControllerTwo = new GamePadInput(PlayerIndex.Two);
            ControllerThree = new GamePadInput(PlayerIndex.Three);
            ControllerFour = new GamePadInput(PlayerIndex.Four);
        }

        public static void Update(GameTime gameTime)
        {
            Keyboard.Update();
            ControllerOne.Update();
            ControllerTwo.Update();
            ControllerThree.Update();
            ControllerFour.Update();
        }

        public static GameInput Get(this AttachedTo inputAttached)
        {
            switch (inputAttached)
            {
                case AttachedTo.Keyboard:
                    return Keyboard;

                case AttachedTo.ControllerOne:
                    return ControllerOne;

                case AttachedTo.ControllerTwo:
                    return ControllerTwo;

                case AttachedTo.ControllerThree:
                    return ControllerThree;

                case AttachedTo.ControllerFour:
                    return ControllerFour;

                default:
                    throw new Exception("Kind of input not found.");
            }
        }
    }
}
