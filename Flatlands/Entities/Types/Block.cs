using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flatlands.Entities.Types
{
    public enum BlockType { Stone, Plank }

    public class Block : Entity
    {
        public BlockType Type { get; private set; }

        public Block(BlockType type = BlockType.Stone)
        {
            Type = type;
        }

        public override void Update(GameTime gameTime) { }
    }
}
