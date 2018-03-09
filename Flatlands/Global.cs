using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands
{
    public enum HorizontalDirection { Left = -1, Right = 1 }
    public enum VerticalDirection { Up = -1, Down = 1 }
    public enum Direction { Vertical, Horizontal }

    static class Global
    {
        public static Texture2D EntityAtlas;
        public static Texture2D MapAtlas;

        public static void Initialize()
        {

        }
    }
}
