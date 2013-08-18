using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace MimicIt
{
    public class Survival
    {
        private enum SurvivalState
        {
            Start,
            Count,
            Game,
            Finish
        };

        private ClockTimer clCountDown;
        private ClockTimer clTimer;
        private PXCMGesture.Gesture.Label currGesture;
        private PXCMGesture.Gesture.Label prevGesture;
        private SurvivalState state;
        private Vector2 vGestLoc;
        private Vector2 vTime;
        private int nScore;
        private bool bAddSec;

        private List<PXCMGesture.Gesture.Label> gestList;

        private const int START_TIME = 10;

        public void Init()
        {
            clCountDown = new ClockTimer();
            clTimer = new ClockTimer();
            state = SurvivalState.Start;
            nScore = 0;
            bAddSec = false;

            gestList = new List<PXCMGesture.Gesture.Label>() 
            {
                PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_LEFT,
                PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_RIGHT,
                PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_UP,
                PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_DOWN, 
                PXCMGesture.Gesture.Label.LABEL_POSE_PEACE
            };

            vGestLoc = new Vector2((MyGraphics.screenRect.Width / 2) - (MyGraphics.GetTexture("g_up").Width / 2), 
                                    MyGraphics.screenRect.Height / 5);
            vTime = new Vector2((MyGraphics.screenRect.Right - 150), 0);
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case SurvivalState.Start:
                    if (InputUtils.gesture == PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_UP)
                    {
                        state = SurvivalState.Count;
                        clCountDown.start(3);
                    }
                    break;
                case SurvivalState.Count:
                    clCountDown.checkTime(gameTime);
                    if (clCountDown.isFinished)
                    {
                        clTimer.start(START_TIME);
                        state = SurvivalState.Game;
                    }
                    break;
                case SurvivalState.Game:
                    clTimer.checkTime(gameTime);
                    UpdateGame(gameTime);
                    break;
                case SurvivalState.Finish:
                    break;
                default:
                    break;
            }
        }

        public void Draw()
        {
            // Draw Background
            MyGraphics.sb.Draw(MyGraphics.GetTexture("mnu_back"), MyGraphics.screenRect, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("mnu_backcloud"), MyGraphics.screenRect, Color.White);

            switch (state)
            {
                case SurvivalState.Start:
                    MyGraphics.sb.DrawString(MyFonts.GetFont("f_time"), "THUMBS UP TO START!",
                        new Vector2(MyGraphics.screenRect.Center.X - 500, 50), Color.White);
                    MyGraphics.sb.Draw(MyGraphics.GetTexture("g_up"), vGestLoc, Color.White);

                    break;
                case SurvivalState.Count:
                    MyGraphics.sb.DrawString(MyFonts.GetFont("f_countdown"), clCountDown.SecondsLeft.ToString(),
                                             new Vector2((MyGraphics.screenRect.Width / 2) - 150,
                                                          MyGraphics.screenRect.Height / 4), Color.White);
                    break;
                case SurvivalState.Game:
                    DrawGame();
                    break;
                case SurvivalState.Finish:
                    break;
                default:
                    break;
            }
        }

        #region Game Region

        public void DrawGame()
        {
            if (clTimer.isRunning)
            {
                MyGraphics.sb.DrawString(MyFonts.GetFont("f_time"), clTimer.SecondsLeft.ToString(), vTime, Color.White);
                MyGraphics.sb.DrawString(MyFonts.GetFont("f_time"), "SCORE:" + nScore.ToString(), new Vector2(10, 0), Color.White);

                //if (bAddSec)
                //{
                //    MyGraphics.sb.DrawString(MyFonts.GetFont("f_opac"), clTimer.SecondsLeft.ToString(), vTime, Color.White);
                //}

                switch (currGesture)
                {
                    case PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_DOWN:
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_LEFT:
                        MyGraphics.sb.Draw(MyGraphics.GetTexture("g_left"), vGestLoc, Color.White);
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_RIGHT:
                        MyGraphics.sb.Draw(MyGraphics.GetTexture("g_right"), vGestLoc, Color.White);
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_UP:
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_POSE_BIG5:
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_POSE_PEACE:
                        MyGraphics.sb.Draw(MyGraphics.GetTexture("g_peace"), vGestLoc, Color.White);
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_DOWN:
                        MyGraphics.sb.Draw(MyGraphics.GetTexture("g_down"), vGestLoc, Color.White);
                        break;
                    case PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_UP:
                        MyGraphics.sb.Draw(MyGraphics.GetTexture("g_up"), vGestLoc, Color.White);
                        break;
                    default:
                        break;
                }
            }
        }

        public void UpdateGame(GameTime gameTime)
        {
            if (clTimer.isRunning)
            {
                if (InputUtils.gesture == currGesture)
                {
                    GetNextGesture();
                    bAddSec = true;
                    nScore++;
                    clTimer.SecondsLeft++;
                }
            }
            else
            {
                state = SurvivalState.Finish;
            }
        }

        private void GetNextGesture()
        {
            currGesture = gestList[new Random().Next(0, gestList.Count)];

            while (currGesture == prevGesture)
            {
                currGesture = gestList[new Random().Next(0, gestList.Count)];
            }

            prevGesture = currGesture;
        }

        #endregion
    }
}
