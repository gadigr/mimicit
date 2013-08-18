using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MimicIt
{
    public class Menu
    {
        private enum MenuState 
        {
            Idle,
            IdleToAbout,
            About,
            AboutToIdle,
            StartGame
        };

        private Rectangle rPractice;
        private Rectangle rTimeAttack;
        private Rectangle rBackCloud;
        private Rectangle rSurvival;
        private Rectangle rClose;
        private MenuState msState;
        private Vector2 vTitle;

        public void Init()
        {
            // Init Rectangles
            rPractice = new Rectangle(MyGraphics.screenRect.Right - MyGraphics.GetTexture("btn_practice").Bounds.Width, 100,
                                      MyGraphics.GetTexture("btn_practice").Width, MyGraphics.GetTexture("btn_practice").Height);
            rTimeAttack = new Rectangle(MyGraphics.screenRect.Left + 30, 50,
                                      MyGraphics.GetTexture("btn_TimeAttack").Width, MyGraphics.GetTexture("btn_TimeAttack").Height);
            rSurvival = new Rectangle(MyGraphics.screenRect.Width / 2 - (MyGraphics.GetTexture("btn_Survival").Width / 2), 0,
                                    MyGraphics.GetTexture("btn_Survival").Width, MyGraphics.GetTexture("btn_Survival").Height);
            rBackCloud = MyGraphics.screenRect;
            rClose = new Rectangle(MyGraphics.screenRect.Right - MyGraphics.GetTexture("btn_close").Width, 0,
                                   MyGraphics.GetTexture("btn_close").Width, MyGraphics.GetTexture("btn_close").Height);
            vTitle = new Vector2((MyGraphics.screenRect.Width / 2) - (MyGraphics.GetTexture("title").Width / 2),
                                 MyGraphics.screenRect.Top);

            // Init MenuState
            msState = MenuState.Idle;
        }

        public void Update(GameTime gameTime)
        {

            switch (msState)
            {
                case MenuState.Idle:
                    UpdateIdle(gameTime);
                    break;
                case MenuState.IdleToAbout:
                    break;
                case MenuState.About:
                    break;
                case MenuState.AboutToIdle:
                    break;
                case MenuState.StartGame:
                    break;
                default:
                    break;
            }
            
        }

        public void Draw()
        {
            MyGraphics.sb.Draw(MyGraphics.GetTexture("mnu_back"), MyGraphics.screenRect, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("mnu_backcloud"), rBackCloud, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("title"), vTitle, Color.White);

            MyGraphics.sb.Draw(MyGraphics.GetTexture("btn_practice"), rPractice, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("btn_TimeAttack"), rTimeAttack, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("btn_close"), rClose, Color.White);
            MyGraphics.sb.Draw(MyGraphics.GetTexture("btn_Survival"), rSurvival, Color.White);

        }

        #region UpdateStates

        private void UpdateIdle(GameTime gameTime)
        {
            rPractice.Y = (int)(150 + (Math.Cos(gameTime.TotalGameTime.TotalMilliseconds * 0.003) * 5));
            rBackCloud.Y = (int)(100 + (Math.Cos(gameTime.TotalGameTime.TotalMilliseconds * 0.001) * 10));
            rTimeAttack.Y = (int)(150 + (Math.Cos(gameTime.TotalGameTime.TotalMilliseconds * 0.0035) * 5));
            rSurvival.Y = (int)(MyGraphics.screenRect.Height / 2 - 150 + (Math.Cos(gameTime.TotalGameTime.TotalMilliseconds * 0.0032) * 5));

            if (MyMouse.IsLeftPressed())
            {
                if (rSurvival.Contains(MyMouse.GetPointPosition()))
                {
                    // Go to time attack mode
                    Game1.state = GameState.Survival;
                }
                else if (rPractice.Contains(MyMouse.GetPointPosition()))
                {
                    // Go to Practice mode
                    Game1.state = GameState.Practice;
                }
                else if (rClose.Contains(MyMouse.GetPointPosition()))
                {
                    Game1.state = GameState.Exit;
                }
                else if (rTimeAttack.Contains(MyMouse.GetPointPosition()))
                {
                    // Go to time attack mode
                    Game1.state = GameState.TimeAttack;
                }
                
            }
            if (InputUtils.gesture == PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_UP)
            {
                msState = MenuState.IdleToAbout;
            }
        }

        #endregion
    }
}
