using Flatlands.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Extensions
{
    public static class EnumerableExtension
    {
        public static void Draw(this IEnumerable<VisualEntity> entities, SpriteBatch spriteBatch)
        {
            foreach (VisualEntity entity in entities)
                entity.Draw(spriteBatch);
        }

        public static void SecondDraw(this IEnumerable<VisualEntity> entities, SpriteBatch spriteBatch)
        {
            foreach (VisualEntity entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
