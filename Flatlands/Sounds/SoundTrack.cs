using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatlands.Sounds
{
    public static class SoundTrack
    {
        static SoundEffectInstance song;

        public static void Load(SoundEffect song, bool play = false, bool playOnLoop = true)
        {
            SoundTrack.song = song.CreateInstance();

            if (play)
                Play(playOnLoop);
        }

        public static void Play(bool playOnLoop = true)
        {
            song.IsLooped = playOnLoop;
            song.Play();
        }
    }
}
