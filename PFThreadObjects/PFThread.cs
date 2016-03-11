//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PFThreadObjects
{
    /// <summary>
    /// Class inherits from Thread class and provides fields for optional extra information. 
    /// </summary>
    public class PFThread 
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private Thread _t = null;
        private PFThreadTimer _timer = new PFThreadTimer();
        private bool _threadHasParameter = false;

        //private variables for properties
        private string _threadDescription = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFThread(ThreadStart entryPoint)
        {
            AllocateNewThreadNoParameter(entryPoint);
            InitializeInstance(null);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFThread(ParameterizedThreadStart entryPoint)
        {
            AllocateNewThreadWithParameter(entryPoint);
            InitializeInstance(null);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFThread(ThreadStart entryPoint, string threadName)
        {
            AllocateNewThreadNoParameter(entryPoint);
            InitializeInstance(threadName);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFThread(ParameterizedThreadStart entryPoint, string threadName)
        {
            AllocateNewThreadWithParameter(entryPoint);
            InitializeInstance(threadName);
        }

        private void AllocateNewThreadNoParameter(ThreadStart entryPoint)
        {
            _t = new Thread(entryPoint);
            _threadHasParameter = false;
        }

        private void AllocateNewThreadWithParameter(ParameterizedThreadStart entryPoint)
        {
            _t = new Thread(entryPoint);
            _threadHasParameter = true;
        }

        private void InitializeInstance(string threadName)
        {
            string name = string.Empty;
            if (threadName == null)
                name = BuildThreadName();
            else
                name = threadName;

            _t.Name = name;

        }

        private string BuildThreadName()
        {
            _str.Length = 0;
            _str.Append("Thread_");
            _str.Append(DateTime.Now.ToString("yyyyMMdd_"));
            _str.Append(DateTime.Now.ToString("HHmmss"));
            return _str.ToString();
        }

        //properties

        /// <summary>
        /// Use this to access all the properties and methods of the underlying System.Threading.Thread being run by this inatance.
        /// </summary>
        public Thread ThreadObject
        {
            get
            {
                return _t;
            }
        }


        /// <summary>
        /// Name given to the thread.
        /// </summary>
        public string ThreadName
        {
            get
            {
                return _t.Name;
            }
        }

        /// <summary>
        /// Expanded description of what the thread is doing.
        /// </summary>
        public string ThreadDescription
        {
            get
            {
                return _threadDescription;
            }
            set
            {
                _threadDescription = value;
            }
        }

        /// <summary>
        /// Returns true if thread is executing.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return _t.IsAlive;
            }
        }
        
        /// <summary>
        /// True if background worker has completed. Otherwise false.
        /// </summary>
        public bool HasFinished
        {
            get
            {
                return _timer.HasFinished;
            }
            set
            {
                _timer.HasFinished = value;
            }
        }

        /// <summary>
        /// StartTime Property.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return _timer.StartTime;
            }
            set
            {
                _timer.StartTime = value;
            }
        }

        /// <summary>
        /// FinishTime Property.
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                return _timer.FinishTime;
            }
            set
            {
                _timer.FinishTime = value;
            }
        }

        /// <summary>
        /// ElapsedTime Property.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                return _timer.ElapsedTime;
            }
        }

        /// <summary>
        /// Elapsed time returned as formatted string.
        /// </summary>
        public string ElapsedTimeFormatted
        {
            get
            {
                return _timer.ElapsedTimeFormatted;
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
                return _timer.ShowElapsedMilliseconds;
            }
            set
            {
                _timer.ShowElapsedMilliseconds = value;
            }
        }


        //methods

        /// <summary>
        /// Causes the operating system to change the state of the current instance to ThreadState.Running. 
        /// Once a thread is in the ThreadState.Running state, the operating system can schedule it for execution. 
        /// The thread begins executing at the first line of the method represented by the ThreadStart or ParameterizedThreadStart delegate supplied to the thread constructor.
        /// </summary>
        public void Start()
        {
            if (_threadHasParameter)
            {
                _msg.Length = 0;
                _msg.Append("Thread requires a parameter to start.");
                throw new System.Exception(_msg.ToString());
            }
            _t.Start();
        }

        /// <summary>
        /// Starts the thread and passes in a parameter containing data to be used by the thread.
        /// </summary>
        /// <param name="parameter"></param>
        public void Start(object parameter)
        {
            if (_threadHasParameter==false)
            {
                _msg.Length = 0;
                _msg.Append("Thread does not use a parameter when starting.");
                throw new System.Exception(_msg.ToString());
            }
            _t.Start(parameter);
        }

        /// <summary>
        /// Interrupts a thread that is in the WaitSleepJoin thread state.
        /// </summary>
        /// <remarks>If this thread is not currently blocked in a wait, sleep, or join state, it will be interrupted when it next begins to block. ThreadInterruptedException is thrown 
        /// in the interrupted thread, but not until the thread blocks. 
        /// If the thread never blocks, the exception is never thrown, and thus the thread might complete without ever being interrupted.</remarks>
        public void Interrupt()
        {
            _t.Interrupt();
        }

        /// <summary>
        /// Raises a ThreadAbortException in the thread on which it is invoked, to begin the process of terminating the thread. Calling this method usually terminates the thread.
        /// </summary>
        /// <remarks> When this method is invoked on a thread, the system throws a ThreadAbortException in the thread to abort it. 
        /// ThreadAbortException is a special exception that can be caught by application code, 
        /// but is re-thrown at the end of the catch block unless ResetAbort is called. 
        /// ResetAbort cancels the request to abort, and prevents the ThreadAbortException from terminating the thread. Unexecuted finally blocks are executed before the thread is aborted. 
        /// See MSDN documentation for complete description of Thread.Abort.</remarks>
        public void Abort()
        {
            _t.Abort();
        }



    }//end class
}//end namespace
