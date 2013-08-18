using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MimicIt
{
    static class MyGraphics
    {
        public static SpriteBatch sb;
        public static GraphicsDeviceManager gdm;
        private static Dictionary<string, Texture2D> textureStore = new Dictionary<string, Texture2D>();
        

        public static Texture2D pixelTexture;

        public static Texture2D circleTexture;

        public static Rectangle screenRect;

        public static void Init(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            sb = spriteBatch;
            gdm = graphics;
            pixelTexture = new Texture2D(gdm.GraphicsDevice, 1, 1);
            Color[] color = new Color[1];
            color[0] = Color.White;
            pixelTexture.SetData<Color>(color);
            screenRect = new Rectangle(0, 0, gdm.GraphicsDevice.Viewport.Width, gdm.GraphicsDevice.Viewport.Height);

        }

        public static void AddTexture(string key, Texture2D texture)
        {
            if (!textureStore.ContainsKey(key))
                textureStore.Add(key, texture);
        }

        public static Texture2D GetTexture(string key)
        {
            return textureStore[key];
        }

        public static void CameraCircle(Vector2 position, float rad, Color color)
        {

            /*   Texture2D texture = circleTexture;
               sb.Draw(texture, camera.GetScreenPos(position), null, color, 0, new Vector2(texture.Width / 2, texture.Height / 2)
                   , camera.GetZoom() * rad/194f, SpriteEffects.None, 0);*/
        }

        public static void Circle(Vector2 position, float rad, Color color)
        {

            Texture2D texture = circleTexture;
            sb.Draw(texture, position, null, color, 0, new Vector2(texture.Width / 2, texture.Height / 2)
                , rad / 194f, SpriteEffects.None, 0);
        }

        /* public static void CameraDraw(Camera camera, Texture2D texture, Vector2 pos, float rot, float size, Color col)
         {
             sb.Draw(texture, camera.GetScreenPos(pos), null, col, rot, new Vector2(texture.Width / 2, texture.Height / 2)
                 , camera.GetZoom() * size, SpriteEffects.None, 0);
         }*/

    }
}
