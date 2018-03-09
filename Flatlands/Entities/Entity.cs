using Flatlands.Drawings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Entities
{
    public abstract class Entity
    {
        private float x;
        private float y;

        public virtual float PreviousX { get; set; }
        public virtual float PreviousY { get; set; }
        public virtual float X
        {
            get { return x; }
            set
            {
                PreviousX = x;
                x = value;
            }
        }
        public virtual float Y
        {
            get { return y; }
            set
            {
                PreviousY = y;
                y = value;
            }
        }
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }
        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            }
        }

        public Vector2 Position { get { return new Vector2(X, Y); } }

        public virtual Rectangle PreviousBoundingBox
        {
            get
            {
                return new Rectangle((int)PreviousX, (int)PreviousY, (int)Width, (int)Height);
            }
        }
        
        public abstract void Update(GameTime gameTime);
        // public abstract void Draw(SpriteBatch spriteBatch);
        //public virtual void SecondDraw(SpriteBatch spriteBatch) { }
    }
}
