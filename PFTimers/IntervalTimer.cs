using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PFTimers
{
    /// <summary>
    /// IntervalTimer is used when smallest time slices down to millisends need to be measured. Timing is done using the Win32 API GetTickCount.
    /// </summary>
    public class IntervalTimer
    {
        // Fields
        private bool _timerIsPaused = false;
        private bool _timerIsRunning = false;
        private int _elapsedTime = 0;
        private int _startTime = 0;
        private int _stopTime = 0;

        // Properties
        /// <summary>
        /// ElapsedTime in milliseconds
        /// </summary>
        public int ElapsedTime  //in milliseconds
        {
            get
            {
                if (this._timerIsRunning)
                {
                    if (this._timerIsPaused)
                    {
                        return this._elapsedTime;
                    }
                    return (this._elapsedTime + (GetTickCount() - this._startTime));
                }
                return this._elapsedTime;
            }
        }

        /// <summary>
        /// Elapsed time in seconds.
        /// </summary>
        public int ElapsedTimeInSeconds
        {
            get
            {
                double numSeconds = (double)this.ElapsedTime / (double)1000.0;
                return (int)System.Math.Round(numSeconds,0); 
            }
        }

        /// <summary>
        /// Stopwatch is still running but is paused.
        /// </summary>
        public bool TimerIsPaused
        {
            get
            {
                return this._timerIsPaused;
            }
        }

        /// <summary>
        /// Stopwatch is running.
        /// </summary>
        public bool TimerIsRunning
        {
            get
            {
                return this._timerIsRunning;
            }
        }

        // Methods
        /// <summary>
        /// Use Win32 API GetTickCount. Retrieves the number of milliseconds that have elapsed since the system was started, up to 49.7 days. 
        /// </summary>
        /// <returns>The return value is the number of milliseconds that have elapsed since the system was started.</returns>
        [DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        private static extern int GetTickCount();

        /// <summary>
        /// Pause is only valid if the timer is running and the
        /// timer has not already been paused.
        /// </summary>
        public void PauseTimer()
        {
            if (this._timerIsRunning & !this._timerIsPaused)
            {
                //Pause is only valid if the timer is running and the
                //timer has not already been paused.
                this._stopTime = GetTickCount();
                this._elapsedTime += this._stopTime - this._startTime;
                this._timerIsPaused = true;
            }
        }

        /// <summary>
        /// Begins the timed interval.
        /// </summary>
        /// <remarks>If timer is alredy running when this method is called,
        ///  then no changes are made to the time counters. If timer is stopped or paused, a new time interval is started.</remarks>
        public void StartTimer()
        {
            //If timer is alredy running when this method is called,
            //then no changes are made to the time counters
            if (!this._timerIsRunning | this._timerIsPaused)
            {
                //timer is stopped or paused
                //get a new start time
                this._startTime = GetTickCount();
            }
            if (!this._timerIsRunning)
            {
                //timer is stopped.
                //reinitialize the elapsed time counter.
                //elapsed time counter is not reinitialized if timer is paused.
                this._elapsedTime = 0;
            }
            this._timerIsRunning = true;
            this._timerIsPaused = false;
        }

        /// <summary>
        /// Marks the end of timed interval.
        /// </summary>
        public void StopTimer()
        {
            //Only update the timer if it is running.
            //if user tries to stop the timer when it is already stopped,
            //then no changes are made to the elapsed time counter.
            //Do not update elapsed time if the timer was in paused state
            //when StopTimer was run.
            if (this._timerIsRunning && !this.TimerIsPaused)
            {
                this._stopTime = GetTickCount();
                this._elapsedTime += this._stopTime - this._startTime;
            }
            this._timerIsRunning = false;
            this._timerIsPaused = false;
        }



    }//end class
}//end namespace
