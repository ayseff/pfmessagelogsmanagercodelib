//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFTimers
{
    /// <summary>
    /// Class to manage to frequency with which status events occur. Class uses an internal timer to determine if the interval between status reports has been passed and a new status report is needed.
    ///  Calling program can also retrieve a formatted display of the elapsed time at any point in its processing for display to the user.
    /// </summary>
    public class StatusTimer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private Stopwatch _sw = new Stopwatch();

        //private variables for properties

        private int _numSecondsInterval = 5;
        private bool _showElapsedTimeMilliseconds = false;
        private DateTime _startDateTime = DateTime.MinValue;
        private DateTime _endDateTime = DateTime.MinValue;
        private DateTime _lastStatusReport = DateTime.MinValue;
        private DateTime _currentDateTime = DateTime.MinValue;

        //constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public StatusTimer()
        {
            ;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numSecondsInterval">Number of seconds between each status report.</param>
        public StatusTimer(int numSecondsInterval)
        {
            _numSecondsInterval = numSecondsInterval;
        }


        //properties

        /// <summary>
        /// Number of seconds between each status report.
        /// </summary>
        /// <remarks>Default is 5 seconds.</remarks>
        public int NumSecondsInterval
        {
            get
            {
                return _numSecondsInterval;
            }
            set
            {
                _numSecondsInterval = value;
            }
        }

        /// <summary>
        /// Property ShowElapsedTimeMilliseconds
        /// </summary>
        public bool ShowElapsedTimeMilliseconds
        {
            get
            {
                return _showElapsedTimeMilliseconds;
            }
            set
            {
                _showElapsedTimeMilliseconds = value;
            }
        }

        /// <summary>
        /// StartDateTime Property.
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return _startDateTime;
            }
        }

        /// <summary>
        /// EndDateTime Property.
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return _endDateTime;
            }
        }

        /// <summary>
        /// LastStatusReport Property.
        /// </summary>
        public DateTime LastStatusReport
        {
            get
            {
                return _lastStatusReport;
            }
        }

        /// <summary>
        /// CurrentDateTime Property.
        /// </summary>
        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
        }

        /// <summary>
        /// Returns true if status timer is currently running; otherwise returns false.
        /// </summary>
        public bool StatusTimerIsRunning
        {
            get
            {
                return _sw.StopwatchIsRunning;
            }
        }



        //methods

        /// <summary>
        /// Begins the timings for status reporting.
        /// </summary>
        public void Start()
        {
            _sw.ShowMilliseconds = _showElapsedTimeMilliseconds;
            _sw.Start();
            _startDateTime = _sw.StartTime;
            _lastStatusReport = _sw.StartTime;
            _currentDateTime = _sw.StartTime;
        }

        /// <summary>
        /// If true then the interval between status reports has been reached and caller should initiate code to get a revised status report.
        /// </summary>
        /// <returns>Boolean.</returns>
        public bool StatusReportDue()
        {
            bool reportDue = false;
            _currentDateTime = DateTime.Now;
            TimeSpan ts = _currentDateTime.Subtract(_lastStatusReport);
            if (ts.TotalSeconds > (double)_numSecondsInterval)
            {
                reportDue = true;
                _lastStatusReport = _currentDateTime;
            }
            return reportDue;
        }

        /// <summary>
        /// Returns a formatted display of the elapsed time for the current status timer.
        /// </summary>
        /// <returns>String containing formatted elapsed time.</returns>
        public string GetFormattedElapsedTime()
        {
            return _sw.FormattedElapsedTime;
        }

        /// <summary>
        /// Returns the elapsed time for the current status timer.
        /// </summary>
        /// <returns>TimeSpan value.</returns>
        public TimeSpan GetElapsedTime()
        {
            return _sw.ElapsedTime;
        }

        /// <summary>
        /// Ends timings for status reporting.
        /// </summary>
        public void Stop()
        {
            _sw.Stop();
            _currentDateTime = _sw.StopTime;
            _endDateTime = _sw.StopTime;
        }

    }//end class
}//end namespace
