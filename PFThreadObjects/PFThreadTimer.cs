//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFThreadObjects
{
    /// <summary>
    /// Common routines for timing threads.
    /// </summary>
    public class PFThreadTimer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private varialbles for properties
        private bool _hasFinished = false;
        private DateTime _startTime = DateTime.MinValue;
        private DateTime _finishTime = DateTime.MinValue;
        private bool _showElapsedMilliseconds = false;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFThreadTimer()
        {
            ;
        }

        //properties

        /// <summary>
        /// True if background worker has completed. Otherwise false.
        /// </summary>
        public bool HasFinished
        {
            get
            {
                return _hasFinished;
            }
            set
            {
                _hasFinished = value;
            }
        }

        /// <summary>
        /// StartTime Property.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        /// <summary>
        /// FinishTime Property.
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                return _finishTime;
            }
            set
            {
                _finishTime = value;
            }
        }

        /// <summary>
        /// ElapsedTime Property.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                return CalculateElapsedTime();
            }
        }

        /// <summary>
        /// Elapsed time returned as formatted string.
        /// </summary>
        public string ElapsedTimeFormatted
        {
            get
            {
                return CalculateFormattedElapsedTime();
            }
        }

        /// <summary>
        /// Determines whether or not the ElapsedTime and ElapsedTimeFormatted properties will show milliseconds in their output.
        /// Set to true if you wish to see the elapsed time broken down to milliseconds.
        /// </summary>
        public bool ShowElapsedMilliseconds
        {
            get
            {
                return _showElapsedMilliseconds;
            }
            set
            {
                _showElapsedMilliseconds = value;
            }
        }


        //methods
        private TimeSpan CalculateElapsedTime()
        {
            TimeSpan elapsedTime = TimeSpan.MinValue;
            if (this.StartTime == DateTime.MinValue)
            {
                //worker has not yet started
                elapsedTime = TimeSpan.MinValue;
            }
            else
            {
                //worker is working
                if (this.HasFinished)
                {
                    elapsedTime = this.FinishTime.Subtract(this.StartTime);
                }
                else
                {
                    elapsedTime = DateTime.Now.Subtract(this.StartTime);
                }
            }
            return elapsedTime;
        }//end CalculateElapsedTime


        private string CalculateFormattedElapsedTime()
        {
            string formattedElapsedTime = string.Empty;
            TimeSpan elapsedTime = CalculateElapsedTime();

            _str.Length = 0;
            if (elapsedTime.Days > 0)
            {
                _str.Append(elapsedTime.Days.ToString());
                if (elapsedTime.Days != 1)
                    _str.Append(" days");
                else
                    _str.Append(" day");
            }

            if (elapsedTime.Hours > 0)
            {
                if (_str.Length > 0)
                    _str.Append(" ");
                _str.Append(elapsedTime.Hours.ToString());
                if (elapsedTime.Hours != 1)
                    _str.Append(" hours");
                else
                    _str.Append(" hour");
            }

            if (elapsedTime.Minutes > 0)
            {
                if (_str.Length > 0)
                    _str.Append(" ");
                _str.Append(elapsedTime.Minutes.ToString());
                if (elapsedTime.Minutes != 1)
                    _str.Append(" minutes");
                else
                    _str.Append(" minute");
            }

            if (_str.Length > 0)
                _str.Append(" ");
            _str.Append(elapsedTime.Seconds.ToString());
            if (elapsedTime.Seconds != 1)
                _str.Append(" seconds");
            else
                _str.Append(" second");

            if (this.ShowElapsedMilliseconds)
            {
                if (_str.Length > 0)
                    _str.Append(" ");
                _str.Append(elapsedTime.Milliseconds.ToString());
                _str.Append(" ms");
            }

            formattedElapsedTime = _str.ToString();
            return formattedElapsedTime;
        }//end CalculateFormattedElapsedTime




    }//end class
}//end namespace
