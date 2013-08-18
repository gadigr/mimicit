using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace MimicIt
{
    static class InputUtils
    {
        public static Vector2 firstPos;
        static GestureState state;
        static GestureState prevState;

        public static PXCMGesture.Gesture.Label gesture;

        static MyPipeline pipe;

        private static PXCMGesture.GeoNode.Openness pressedDef = PXCMGesture.GeoNode.Openness.LABEL_CLOSE;

        private static Texture2D texture;
        public static Texture2D openPic, closePic;


        public static String word;
        public static Thread thread;
        public static Texture2D camImage;

        public static int idleTime = 0;

        public static void Init()
        {
            state = new GestureState();
            prevState = new GestureState();
            prevState.openState = PXCMGesture.GeoNode.Openness.LABEL_OPENNESS_ANY;
            pipe = new MyPipeline();
            thread = new Thread(new ThreadStart(pipe.StartWork));
            thread.Start();
        }

        public static void Update()
        {
            idleTime++;
            gesture = pipe.GetGesture();

            prevState = state;
            state = pipe.GetState();

            if (state.position != prevState.position)
                idleTime = 0;

            //if (GetClick())
            //    firstPos = state.position;
        }

        public static void End()
        {
            pipe.End();
            while (thread.IsAlive)
            {
            }
        }
    }
}
