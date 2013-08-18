
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;


namespace MimicIt
{
    struct GestureState
    {
        public Vector2 position;
        public float oppenes;
        public PXCMGesture.GeoNode.Openness openState;
    }

    class MyPipeline : UtilMPipeline
    {
        //  protected int nframes;
        public bool start = true;
        protected bool device_lost;

        public Vector2 position;
        public float opennes;
        PXCMGesture.GeoNode.Openness openState;
        public float xx = 0;
        public float yy = 0;

        public float z;

        public String word;

        private PXCMGesture.Gesture.Label gesture;

        public Bitmap img;

        public MyPipeline()
            : base()
        {
            EnableImage(PXCMImage.ColorFormat.COLOR_FORMAT_DEPTH);
            EnableGesture();
            EnableVoiceRecognition();
            //SetVoiceCommands
            

            // nframes = 0;
            device_lost = false;
            position = new Vector2();
            opennes = 1;

            word = "";

        }
          
        

        public GestureState GetState()
        {
            GestureState state = new GestureState();
            state.position = position;
            state.oppenes = opennes;
            state.openState = openState;
            return state;
        }

        public Texture2D getImage()
        {
            Texture2D ans = GetTexture2DFromBitmap(img);
            img = null;
            return ans;
        }

        public static Texture2D GetTexture2DFromBitmap(Bitmap bitmap)
        {
            Texture2D tex = new Texture2D(MyGraphics.gdm.GraphicsDevice, bitmap.Width, bitmap.Height);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            //create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetData(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            return tex;
        }

        public override void OnGesture(ref PXCMGesture.Gesture data)
        {

            if (data.active)
            {
                Console.WriteLine("OnGesture(" + data.label + ")");
                gesture = data.label;
            }
        }

        public PXCMGesture.Gesture.Label GetGesture()
        {
            PXCMGesture.Gesture.Label ans = gesture;
            gesture = PXCMGesture.Gesture.Label.LABEL_ANY;
            return ans;
        }

        public override bool OnDisconnect()
        {
            if (!device_lost) Console.WriteLine("Device disconnected");
            device_lost = true;
            return base.OnDisconnect();
        }
        public override void OnReconnect()
        {
            Console.WriteLine("Device reconnected");
            device_lost = false;
        }

        public override bool OnNewFrame()
        {
            PXCMGesture gesture = QueryGesture();
            PXCMGesture.GeoNode ndata;
            pxcmStatus sts = gesture.QueryNodeData(0, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, out ndata);
            uint sx;
            uint sy;
            QueryImageSize(PXCMImage.ImageType.IMAGE_TYPE_DEPTH, out sx, out sy);
            xx = sx;
            yy = sy;
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                Console.WriteLine("HAND_MIDDLE:" + ndata.positionImage.x.ToString() + "," + ndata.positionImage.y.ToString() + " Openess:" + ndata.openness.ToString());

            float x = ndata.positionImage.x;
            float y = ndata.positionImage.y;
            z = ndata.positionWorld.z;

            position = new Vector2(MyGraphics.screenRect.Width / 2 - (x / xx - 0.5f) * MyGraphics.screenRect.Width * 1.5f,
                                   MyGraphics.screenRect.Height / 2 + (y / yy - 0.5f) * MyGraphics.screenRect.Height * 1.5f);
            opennes = ndata.openness;
            openState = ndata.opennessState;
            //return (++nframes < 50000);
            return start;
        }

        public void End()
        {
            start = false;
        }

        public void StartWork()
        {
            this.LoopFrames();
        }

        private void CameraToScreen(out float x, out float y)
        {
            x = 0;
            y = 0;
        }

        public override void OnRecognized(ref PXCMVoiceRecognition.Recognition data)
        {
            Console.WriteLine("\nRecognized<{0}>", data.dictation);
            word = data.dictation;
        }

        public String GetWord()
        {
            String ans = word;
            word = "";
            return ans;
        }


    }
}


