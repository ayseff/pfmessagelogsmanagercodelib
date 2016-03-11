using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Management;

namespace PFTimers
{
    /// <summary>
    /// Class implements functionality to get elapsed time for a section of code.
    /// </summary>
    public class Stopwatch
    {

        private bool _stopwatchIsRunning = false;
        private DateTime _currentTime = new DateTime();
        private DateTime _endTime = new DateTime();
        private DateTime _startTime = new DateTime();
        private int _elapsedDays = 0;
        private int _elapsedHours = 0;
        private int _elapsedMinutes = 0;
        private int _elapsedSeconds = 0;
        private int _elapsedMilliseconds = 0;
        private TimeSpan _elapsedTime = TimeSpan.MinValue;
        private StopwatchOutputFormat _outputFormat = StopwatchOutputFormat.String;
        private string _formattedElapsedTime = "";
        private string _formattedStartTime = "";
        private string _formattedStopTime = "";
        private bool _showMilliseconds = true;

        /// <summary>
        /// Specifies how elapsed time output will be formatted.
        /// </summary>
        public enum StopwatchOutputFormat
        {
            /// <summary>
            /// Output looks like ddd:HH:mm:ss.ms (days:hours:minutes:seconds.milliseconds). Example: Elapsed time: 000:00:00:22.651
            /// </summary>
            Fixed = 2,
            /// <summary>
            /// Output will look like: Elapsed time: 1 day 1 hour 5 minutes 21 seconds 291 ms 
            /// </summary>
            String = 1
        }

        // Properties

        /// <summary>
        /// If true, stopwatch is currently doing a timing.
        /// </summary>
        public bool StopwatchIsRunning
        {
            get
            {
                return _stopwatchIsRunning;
            }
            set
            {
                _stopwatchIsRunning = value;
            }
        }

        /// <summary>
        /// Returns total elapsed days since stopwatch started.
        /// </summary>
        public int ElapsedDays
        {
            get
            {
                this.CalcTime();
                return (int)this.ElapsedTime.TotalDays;
            }
        }

        /// <summary>
        /// Returns total elapsed hours since stopwatch started.
        /// </summary>
        public int ElapsedHours
        {
            get
            {
                this.CalcTime();
                return (int)this.ElapsedTime.TotalHours;
            }
        }

        /// <summary>
        /// Returns total elapsed minutes since stopwatch started.
        /// </summary>
        public int ElapsedMinutes
        {
            get
            {
                this.CalcTime();
                return (int)this.ElapsedTime.TotalMinutes;
            }
        }

        /// <summary>
        /// Returns total elapsed seconds since stopwatch started.
        /// </summary>
        public int ElapsedSeconds
        {
            get
            {
                this.CalcTime();
                return (int)this.ElapsedTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Returns total elapsed milliseconds since stopwatch started.
        /// </summary>
        public int ElapsedMilliseconds
        {
            get
            {
                this.CalcTime();
                return (int)this.ElapsedTime.TotalMilliseconds;
            }
        }

        /// <summary>
        /// 
        /// Returns string with formatted elapsed time since stopwatch started.
        /// </summary>
        public string FormattedElapsedTime
        {
            get
            {
                this.CalcTime();
                this.FormatOutput();
                return this._formattedElapsedTime;
            }
        }

        /// <summary>
        /// Returns formatted start time.
        /// </summary>
        public string FormattedStartTime
        {
            get
            {
                return this._formattedStartTime;
            }
        }

        /// <summary>
        /// Returns formatted stop time.
        /// </summary>
        public string FormattedStopTime
        {
            get
            {
                return this._formattedStopTime;
            }
        }

        /// <summary>
        /// Type of format to use for output of timings.
        /// </summary>
        public StopwatchOutputFormat OutputFormat
        {
            get
            {
                return this._outputFormat;
            }
            set
            {
                this._outputFormat = value;
            }
        }

        /// <summary>
        /// Returns StartTime as DateTime value.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this._startTime;
            }
            set
            {
                this._startTime = value;
                this._formattedStartTime = FormatStartTime();
            }
       }

        /// <summary>
        /// Returns StopTime as DateTime value.
        /// </summary>
        public DateTime StopTime
        {
            get
            {
                return this._endTime;
            }
            set
            {
                this._endTime = value;
                this._formattedStopTime = FormatStopTime();
            }
        }

        /// <summary>
        /// Returns current time as DateTime value.
        /// </summary>
        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Returns TimeSpan structure containing elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                this.CalcTime();
                return this._elapsedTime;
            }
        }

        /// <summary>
        /// If true, millisecnds will be shown in output. If false, output will only show to seconds.
        /// </summary>
        public bool ShowMilliseconds
        {
            get
            {
                return _showMilliseconds;
            }
            set
            {
                _showMilliseconds = value;
            }
        }


        // Methods

        /// <summary>
        /// Begins a timing interval.
        /// </summary>
        public void Start()
        {
            this.ResetCounters();
            this._stopwatchIsRunning = true;
            this._startTime = DateTime.Now;
            this._formattedStartTime = FormatStartTime();
        }

        /// <summary>
        /// Stop the timer. Marks the end of the timed interval.
        /// </summary>
        public void Stop()
        {
            if (this._stopwatchIsRunning)
            {
                this._endTime = DateTime.Now;
                this._formattedStopTime = FormatStopTime();
                this._stopwatchIsRunning = false;
                this.CalcTime();
            }
        }

        /// <summary>
        /// Resets stopwatch by clearing all counters and stopping the watch if it is running.
        /// </summary>
        public void Clear()
        {
            this.ResetCounters();
            this._stopwatchIsRunning = false;
        }

        private void ResetCounters()
        {
            this._formattedElapsedTime = "";
            this._elapsedMilliseconds = 0;
            this._elapsedSeconds = 0;
            this._elapsedMinutes = 0;
            this._elapsedHours = 0;
            this._elapsedDays = 0;
            this._formattedStartTime = "";
            this._formattedStopTime = "";
        }


        private void CalcTime()
        {
            if (this._stopwatchIsRunning)
            {
                this._currentTime = DateTime.Now;
            }
            else
            {
                if (DateTime.Compare(this._endTime, DateTime.MinValue) == 0)
                {
                    return;
                }
                this._currentTime = this._endTime;
            }
            TimeSpan span = _currentTime.Subtract(this._startTime);
            this._elapsedTime = span;
            this._elapsedMilliseconds = span.Milliseconds;
            this._elapsedSeconds = span.Seconds;
            this._elapsedMinutes = span.Minutes;
            this._elapsedHours = span.Hours;
            this._elapsedDays = span.Days;
        }

        private void FormatOutput()
        {
            switch (this._outputFormat)
            {
                case StopwatchOutputFormat.String:
                    this.FormatString();
                    break;

                case StopwatchOutputFormat.Fixed:
                    this.FormatFixed();
                    break;

                default:
                    this._formattedElapsedTime = "Invalid Output Format value";
                    break;
            }
        }

        private void FormatFixed()
        {
            this._formattedElapsedTime = this._elapsedDays.ToString("000") + ":";
            this._formattedElapsedTime = this._formattedElapsedTime + this._elapsedHours.ToString("00") + ":";
            this._formattedElapsedTime = this._formattedElapsedTime + this._elapsedMinutes.ToString("00") + ":";
            this._formattedElapsedTime = this._formattedElapsedTime + this._elapsedSeconds.ToString("00") + "";
            if (this._showMilliseconds)
                this._formattedElapsedTime = this._formattedElapsedTime + "." + this._elapsedMilliseconds.ToString("000") + "";
        }

        private void FormatString()
        {
            int milliseconds = this._elapsedMilliseconds;
            int seconds = this._elapsedSeconds;
            int minutes = this._elapsedMinutes;
            int hours = this._elapsedHours;
            int elapsedDays = this._elapsedDays;
            this._formattedElapsedTime = "";
            if (elapsedDays > 0)
            {
                if (elapsedDays > 1)
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + elapsedDays.ToString() + " days ";
                }
                else
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + elapsedDays.ToString() + " day ";
                }
            }
            if ((hours > 0) | (this._formattedElapsedTime.Length  > 0))
            {
                if ((hours > 1) | (hours == 0))
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + hours.ToString() + " hours ";
                }
                else
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + hours.ToString() + " hour ";
                }
            }
            if ((minutes > 0) | (this._formattedElapsedTime.Length > 0))
            {
                if ((minutes > 1) | (minutes == 0))
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + minutes.ToString() + " minutes ";
                }
                else
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + minutes.ToString() + " minute ";
                }
            }
            if ((seconds > 0) | this._formattedElapsedTime.Length > 0)
            {
                if ((seconds > 1) | (seconds == 0))
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + seconds.ToString() + " seconds ";
                }
                else
                {
                    this._formattedElapsedTime = this._formattedElapsedTime + seconds.ToString() + " second ";
                }
            }
            if(this._showMilliseconds)
            {
                if (milliseconds>0 || this._formattedElapsedTime.Length>0)
                    this._formattedElapsedTime = this._formattedElapsedTime+ milliseconds.ToString() + " ms ";
            }

            if (this._formattedElapsedTime.Length == 0)
            {
                this._formattedElapsedTime = "0 seconds 0 ms";
            }
        }

        private string FormatStartTime()
        {
            return(this._startTime.ToString("M/d/yyyy HH:mm:ss"));
        }

        private string FormatStopTime()
        {
            return (this._endTime.ToString("M/d/yyyy HH:mm:ss"));
        }


    }//end class
}//end namespace
