using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MimicIt
{
    static class MyFonts
    {
        private static Dictionary<string, SpriteFont> fontStore = new Dictionary<string, SpriteFont>();

        public static void AddFont(string key, SpriteFont font)
        {
            if (!fontStore.ContainsKey(key))
                fontStore.Add(key, font);
        }

        public static SpriteFont GetFont(string key)
        {
            if (fontStore.ContainsKey(key))
                return fontStore[key];
            else
                return null;
        }
    }
}
