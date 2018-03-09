using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Drawings
{
    public class Sprite
    {
        protected int width;
        protected int height;
        protected Rectangle source;
        private Color color;
        private Color currentColor;
        private double fadeElapsedTime;
        private double fadeDelay;
        private float fadeIncrement;
        private float alpha;
        private float minAlpha;
        private float maxAlpha;
        
        protected float scale;

        public bool IsBlinking { get; protected set; }
        public float Rotation { get; set; }

        public string Name;
        public Rectangle Source
        {
            get { return source; }
            set
            {
                source = value;
                Width = source.Width;
                Height = source.Height;
            }
        }
        public float X { get; set; }
        public float Y { get; set; }
        public virtual int Width
        {
            get { return width; }
            set
            {
                width = value;
                source.Width = width;
            }
        }
        public virtual int Height
        {
            get { return height; }
            set
            {
                height = value;
                source.Height = height;
            }
        }

        public Vector2 Origin;

        public SpriteEffects Effect { get; set; }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            }
        }

        public Vector2 Position { get { return new Vector2(X, Y); } }

        public Sprite(Vector2? origin = null, float scale = 1, SpriteEffects effect = SpriteEffects.None,
            Color? color = null)
        {
            Origin = origin.HasValue ? origin.Value : new Vector2(0, 0);
            Effect = effect;
            this.scale = scale;
            Rotation = 0;
            IsBlinking = false;
            this.color = color ?? Color.White;
            currentColor = this.color;
        }

        public void StartBlinkingEffect(float fadeDelay, float fadeIncrement, float minAlpha, float maxAlpha)
        {
            IsBlinking = true;
            this.minAlpha = MathHelper.Clamp(minAlpha, 0, 1);
            this.maxAlpha = MathHelper.Clamp(maxAlpha, 0, 1);
            this.fadeDelay = fadeDelay;
            this.fadeIncrement = fadeIncrement;
            fadeElapsedTime = 0;
            currentColor = color;
        }

        public void StopBlinkingEffect()
        {
            IsBlinking = false;
            currentColor = color;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsBlinking)
                UpdateBlinking(gameTime);
        }

        private void UpdateBlinking(GameTime gameTime)
        {
            fadeElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (fadeDelay >= fadeElapsedTime)
            {
                fadeElapsedTime = 0;

                alpha += fadeIncrement;

                if (alpha >= maxAlpha || alpha <= minAlpha)
                {
                    fadeIncrement *= -1;
                    alpha = MathHelper.Clamp(alpha, minAlpha, maxAlpha);
                }

                currentColor = Color.Lerp(color, Color.Transparent, alpha);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D atlas)
        {
            spriteBatch.Draw(atlas, Position, Source, currentColor, Rotation, Origin, scale, Effect, 0);
        }
    }
}
