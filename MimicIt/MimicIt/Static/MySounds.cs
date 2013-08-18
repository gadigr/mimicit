using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MimicIt
{
    static class MySounds
    {
        private static Dictionary<string, SoundEffect> effectStore = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> soundStore = new Dictionary<string, Song>();

        public static void AddEffect(string key, SoundEffect effect)
        {
            if (!effectStore.ContainsKey(key))
                effectStore.Add(key, effect);
        }

        public static SoundEffect GetEffect(string key)
        {
            if (effectStore.ContainsKey(key))
                return effectStore[key];
            else
                return null;
        }

        public static void AddSong(string key, Song sng)
        {
            if (!soundStore.ContainsKey(key))
                soundStore.Add(key, sng);
        }

        public static Song getSong(string key)
        {
            if (soundStore.ContainsKey(key))
                return soundStore[key];
            else
                return null;
        }
    }
}
