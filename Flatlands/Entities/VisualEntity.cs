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
    public class VisualEntity : Entity
    {
        public Sprite Sprite;
        public bool IsVisible;

        public override float X { get { return Sprite.X; } set { Sprite.X = value; } }
        public override float Y { get { return Sprite.Y; } set { Sprite.Y = value; } }
        //public override float Width { get; set; }
        //public override float Height { get; set; }

        public Rectangle SpriteBounds { get { return Sprite.BoundingBox; } }

        public float OriginX
        {
            get { return Sprite.Origin.X; }
            set { Sprite.Origin.X = value; }
        }

        public float OriginY
        {
            get { return Sprite.Origin.Y; }
            set { Sprite.Origin.Y = value; }
        }

        public VisualEntity(Sprite sprite, bool isVisible = true)
        {
            if (sprite == null)
                throw new Exception("Sprite can't be null. Use 'Entity' instead.");
            IsVisible = isVisible;
            Sprite = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void StartBlinking(float fadeDelay = 0.035f, float fadeIncrement = 0.03f,
            float minAlpha = 0, float maxAlpha = 1)
        {
            Sprite.StartBlinkingEffect(fadeDelay, fadeIncrement, minAlpha, maxAlpha);
        }

        public void StopBlinking()
        {
            Sprite.StopBlinkingEffect();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
                Sprite.Draw(spriteBatch, Global.EntityAtlas);
        }

        public virtual void SecondDraw(SpriteBatch spriteBatch) { }
    }
}
