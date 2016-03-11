using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Runtime.CompilerServices;

namespace PFTimers
{
    /// <summary>
    /// Class implementing wait interval functionality.
    /// </summary>
    public class WaitTimer
    {


        // Fields
        private System.Timers.Timer _timer;
        private bool _done;
        private int threadSleepInterval = 100;   //milliseconds

        /// <summary>
        /// Constructor.
        /// </summary>
        public WaitTimer()
        {
            this.Timer = new System.Timers.Timer();
            this._done = true;
            this.Timer.Interval = 1000;
            this.Timer.Enabled = false;
        }

        //methods
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Timer.Enabled = false;
            this.TimerDone();
        }

        private void TimerDone()
        {
            this._done = true;
        }

        /// <summary>
        /// Causes current thread to sleep for specified number of seconds.
        /// </summary>
        /// <param name="numSecsToWait"></param>
        public void Wait(long numSecsToWait)
        {
            this._done = false;
            this.Timer.Interval = numSecsToWait * 1000; //convert to milliseconds
            this.Timer.Enabled = true;
            while (!this._done)
            {
                if (this._done)
                {
                    break;
                }
                ThreadSleep(threadSleepInterval);
            }
        }

        
        private void ThreadSleep(int numMillisecondsToSleep)
        {
            System.Threading.Thread.Sleep(numMillisecondsToSleep);
        }

        // Properties
        /// <summary>
        /// Number of milliseconds an instance of this class will sleep to allow other processes cpu time.
        /// </summary>
        public int ThreadSleepInterval
        {
            get
            {
                return threadSleepInterval;
            }
            set
            {
                threadSleepInterval = value;
            }
        }

        /// <summary>
        /// Get or set the value for the system timer used by this class
        /// </summary>
        private System.Timers.Timer Timer
        {
            get
            {
                return this._timer;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (this._timer != null)
                {
                    this._timer.Elapsed -= new ElapsedEventHandler(this.Timer_Elapsed);
                }
                this._timer = value;
                if (this._timer != null)
                {
                    this._timer.Elapsed += new ElapsedEventHandler(this.Timer_Elapsed);
                }
            }
        }

    }//end class

}//end namespace


