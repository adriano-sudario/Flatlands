using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static Flatlands.Drawings.Animation;
using Microsoft.Xna.Framework.Graphics;

namespace Flatlands.Drawings
{
    public class AnimatedSprite : Sprite
    {
        private Animation currentAnimation;
        private int currentFrameIndex;
        private double elapsedTime;

        public Dictionary<string, Animation> Animations { get; set; }
        public Animation CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                RestartedLoop = false;
                currentFrameIndex = 0;
                elapsedTime = 0;
                currentAnimation = value;
                if (value != null)
                    Source = CurrentFrame.Source;
            }
        }

        public override int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                foreach (Frame f in CurrentAnimation.Frames)
                {
                    f.Source.Width = value;
                }
            }
        }

        public override int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                foreach (Frame f in CurrentAnimation.Frames)
                {
                    f.Source.Height = value;
                }
            }
        }

        public Frame CurrentFrame
        {
            get { return CurrentAnimation.Frames[currentFrameIndex]; }
        }

        public bool RestartedLoop { get; private set; }

        public AnimatedSprite(List<Animation> animations, string initialAnimationName = "", 
            Vector2? origin = null, float scale = 1, SpriteEffects effect = SpriteEffects.None,
            Color? color = null) : 
            base(origin, scale, effect, color)
        {
            Animations = new Dictionary<string, Animation>();

            if (animations == null || animations.Count == 0)
                throw new Exception("Deve ser passada pelo menos uma animação.");

            if (initialAnimationName == "")
                CurrentAnimation = animations[0];

            foreach (Animation animation in animations)
            {
                Animations.Add(animation.Name, animation);
                if (initialAnimationName != "" && animation.Name == initialAnimationName)
                    CurrentAnimation = animation;
            }

            RestartedLoop = false;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime < CurrentFrame.Duration)
                return;
            RestartedLoop = false;
            elapsedTime = 0;
            currentFrameIndex++;
            if (currentFrameIndex >= CurrentAnimation.Frames.Count)
            {
                if (CurrentAnimation.IsLooping)
                {
                    currentFrameIndex = 0;
                    RestartedLoop = true;
                }
                else
                {
                    currentFrameIndex--;
                }
            }
            Source = CurrentFrame.Source;
        }
    }
}
