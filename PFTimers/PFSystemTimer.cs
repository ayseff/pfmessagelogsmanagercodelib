//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PFTimers
{
    /// <summary>
    /// Class implements .NET system timer processing.
    /// </summary>
    public class PFSystemTimer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private DateTime _signalStart = DateTime.MaxValue;
        private TimeSpan _signalInterval = TimeSpan.MaxValue;

        //private variables for properties
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private int _intervalInSecs = 10;
        private bool _enabled = false;

        //event definitions
        /// <summary>
        /// Event delegate used for setting up callbacks that report on number of bytes and elapsed time from encryption and decryption methods.
        /// </summary>
        /// <param name="elapsedTime">Time elapsed since timer was started.</param>
        /// <param name="currentTime">Current time.</param>
        /// <param name="startTime">Time at which timer was started.</param>
        public delegate void ElapsedTimeReportDelegate(TimeSpan elapsedTime, DateTime currentTime, DateTime startTime);
        /// <summary>
        /// Event that returns status information for encrypt or decrypt operation to the calling program.
        /// </summary>
        public event ElapsedTimeReportDelegate elapsedTimeStatusReport;


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFSystemTimer()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFSystemTimer(int numSecsInterval)
        {
            _intervalInSecs = numSecsInterval;
        }

        //properties
        /// <summary>
        /// Timer Property.
        /// </summary>
        public System.Timers.Timer Timer
        {
            get
            {
                return _timer;
            }
        }

        /// <summary>
        /// IntervalInSecs Property.
        /// </summary>
        public int IntervalInSecs
        {
            get
            {
                return _intervalInSecs;
            }
            set
            {
                _intervalInSecs = value;
            }
        }

        /// <summary>
        /// Enabled Property.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
        }


        //methods
        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            _timer.Interval = Convert.ToDouble(_intervalInSecs * 1000);
            _signalStart = DateTime.Now;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            _timer.Enabled = false;
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause()
        {
            _timer.Enabled = false;
        }

        /// <summary>
        /// Restarts a paused timer.
        /// </summary>
        public void Restart()
        {
            _timer.Enabled = true;
        }

        /// <summary>
        /// Sets the routine to receive timer alerts.
        /// </summary>
        /// <param name="proc">Method that will receive the timer alerts.</param>
        public void SetEventHandler(object proc)
        {
            _timer.Elapsed+=new System.Timers.ElapsedEventHandler(OnTimedEvent);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _signalInterval = e.SignalTime.Subtract(_signalStart);
            if (elapsedTimeStatusReport != null)
            {
                elapsedTimeStatusReport(_signalInterval, e.SignalTime, _signalStart);
            }
        }



    }//end class
}//end namespace
