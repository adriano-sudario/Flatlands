using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Drawings
{
    public class Animation
    {
        public class Frame
        {
            public Rectangle Source;
            public float Duration;
        }

        public List<Frame> Frames { get; set; }
        public string Name { get; set; }
        public bool IsLooping { get; set; }

        public Animation(bool hasLoop = false)
        {
            IsLooping = hasLoop;
        }

        public static Animation GetFromAtlas(string name, int startingX, int startingY, 
            int width, int height, int frames, float duration, bool hasLoop = true)
        {
            Animation animation = new Animation(hasLoop)
            {
                Frames = new List<Frame>(),
                Name = name
            };

            for (int i = startingX; i < startingX + frames; i++)
            {
                animation.Frames.Add(new Frame()
                {
                    Source = new Rectangle(i * width, startingY * height, width, height),
                    Duration = duration
                });
            }

            return animation;
        }
    }
}
