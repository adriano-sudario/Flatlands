using Flatlands.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Mechanics
{
    class Movement
    {
        public static void Move(Entity entity, Vector2 increment)
        {
            entity.X += increment.X;
            entity.Y += increment.Y;
        }

        public static void Move(Entity entity, HorizontalDirection horizontalDirection, float increment)
        {
            entity.X += increment * (int)horizontalDirection;
        }

        public static void Move(Entity entity, VerticalDirection verticalDirection, float increment)
        {
            entity.Y += increment * (int)verticalDirection;
        }

        public static void Move(Entity entity, Direction direction, float increment)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    entity.X += increment;
                    break;

                case Direction.Vertical:
                    entity.Y += increment;
                    break;
            }
        }

        public static void Teleport(Entity entity, Vector2 newPosition)
        {
            Move(entity, newPosition);
        }

        public static void Jump(Entity entity)
        {

        }
    }
}
