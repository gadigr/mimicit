using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MimicIt
{
    static class MyMouse
    {
        static MouseState mouseState, previousMouseState;

        public static void Init()
        {
            mouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public static Vector2 GetPosition()
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static Point GetPointPosition()
        {
            return new Point(mouseState.X, mouseState.Y);
        }

        public static bool IsLeftPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsRightPressed()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

    }
}
