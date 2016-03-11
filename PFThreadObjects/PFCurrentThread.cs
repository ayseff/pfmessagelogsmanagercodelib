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
    /// Class to manage the currently executing thread.
    /// </summary>
    public class PFCurrentThread
    {
        //private work variables
        //private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties

        //constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        public PFCurrentThread()
        {
            ;
        }

        //properties

        //methods
        /// <summary>
        /// Pauses current executing thread the specified number of seconds.
        /// </summary>
        /// <param name="numSecsToWait">Number of seconds to wait before resuming thread.</param>
        /// <remarks>Specify zero to indicate that curent thread should be suspended to allow other waiting threads to execute.</remarks>
        public static void Wait(int numSecsToWait)
        {
            int numMilliseconds = numSecsToWait * 1000; //convert to milliseconds;

            System.Threading.Thread.Sleep(numMilliseconds);
        }

        /// <summary>
        /// Pauses current executing thread the specified number of milliseconds.
        /// </summary>
        /// <param name="numMillisecondsToSleep">Number of milliseconds to wait before resuming thread.</param>
        /// <remarks>Specify zero to indicate that curent thread should be suspended to allow other waiting threads to execute.</remarks>
        public static void Sleep(int numMillisecondsToSleep)
        {
            System.Threading.Thread.Sleep(numMillisecondsToSleep);
        }

        /// <summary>
        /// Pauses current executing thread for the amount of time specified in the TimeSpan parameter.
        /// </summary>
        /// <param name="waitTime">TimeSpan object that specifies how much time to suspend the current thread.</param>
        public static void Sleep(TimeSpan waitTime)
        {
            System.Threading.Thread.Sleep(waitTime);
        }

    }//end class
}//end namespace
