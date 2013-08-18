using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MimicIt
{
    class ClockTimer 
    {    
        private int endTimer; 
        private int countTimerRef; 
        public String displayClock { get; private set; } 
        public bool isRunning { get; private set; } 
        public bool isFinished { get; private set; }

        public int SecondsPassed { get; private set; }
        

        public int SecondsLeft {
            get
            {
                return endTimer;
            }
            set
            {
                endTimer = value;
            }
        }

        public ClockTimer() 
        {
            SecondsPassed = 0;
            displayClock = ""; 
            endTimer = 0; 
            countTimerRef = 0; 
            isRunning = false; 
            isFinished = false; 
 
        } 
        public void start(int seconds) 
        {
            SecondsPassed = 0;
            endTimer = seconds; 
            isRunning = true; 
            displayClock = endTimer.ToString(); 
        }

        public void Pause()
        {
            isRunning = false;
        }

        public void Continue()
        {
            isRunning = true;
        }
 
        public Boolean checkTime(GameTime gameTime) 
        { 
            countTimerRef += (int)gameTime.ElapsedGameTime.TotalMilliseconds; 
            if (!isFinished) 
            {
                if (isRunning)
                {
                    if (countTimerRef >= 1000.0f)
                    {
                        SecondsPassed++;
                        endTimer = endTimer - 1;
                        displayClock = endTimer.ToString();
                        countTimerRef = 0;
                        if (endTimer <= 0)
                        {
                            endTimer = 0;
                            isFinished = true;
                            displayClock = "Game Over";
                        }
                    }  
                }
            } 
            else 
            { 
                 
                displayClock = "Game Over"; 
            } 
            return isFinished; 
        } 
        public void reset() 
        { 
            isRunning = false; 
            isFinished = false; 
            displayClock = "None"; 
            countTimerRef = 0; 
            endTimer = 0; 
        }   
    } 
}
