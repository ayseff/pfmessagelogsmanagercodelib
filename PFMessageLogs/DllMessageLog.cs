//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFMessageLogs
{
    /// <summary>
    /// Class that wraps functionality for displaying a message log window directly from a DLL.
    /// </summary>
    public class DllMessageLog
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private bool _retainFocus = true;
        private bool _showDatetime = false;
        private MessageLogForm _messageLogForm = new MessageLogForm();
        private string _caption = "Message Log";
        private string _font = "Lucida Console";
        private float _fontSize = (float)10.0;
        private bool _changeFont = false;


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DllMessageLog()
        {
            _messageLogForm.WindowState = FormWindowState.Normal;
            _messageLogForm.Text = _caption;
            _messageLogForm.txtMessageLog.Text = "";
            ChangeMessageLogFont();

            _messageLogForm.Left = 0;
            int y = SystemInformation.VirtualScreen.Height - _messageLogForm.Size.Height - 100;
            _messageLogForm.Top = y;
            //_messageLogForm.Location = new System.Drawing.Point(0,y);

            _messageLogForm.Show();
        }

        // Properties

        /// <summary>
        /// If true the File/Erase menu item will be displayed on the MessageLogForm.
        /// </summary>
        public bool AllowFileErase
        {
            get
            {
                return _messageLogForm.AllowFileErase;
            }
            set
            {
                _messageLogForm.AllowFileErase = value;
            }
        }

        /// <summary>
        /// Sets MessageLogForm caption at top of form.
        /// </summary>
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                _messageLogForm.Text = value;
            }
        }

        /// <summary>
        /// Retrieves reference to the MessageLogForm. 
        /// This allows caller to directly control the form.
        /// </summary>
        public Form Form
        {
            get
            {
                return _messageLogForm;
            }
        }

        /// <summary>
        /// Returns whether or not the MessageLogForm is currently visible.
        /// </summary>
        public bool FormIsVisible
        {
            get
            {
                return _messageLogForm.Visible;
            }
        }

        /// <summary>
        /// Forces focus to remain on MessageLogForm after a line of text is written.
        /// </summary>
        public bool RetainFocus
        {
            get
            {
                return _retainFocus;
            }
            set
            {
                _retainFocus = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show the date and time with each message displayed on MessageLogForm.
        /// </summary>
        public bool ShowDatetime
        {
            get
            {
                return _showDatetime;
            }
            set
            {
                _showDatetime = value;
            }
        }

        /// <summary>
        /// Specifies the name of the font to use for the displayed messages.
        /// </summary>
        public string Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                _changeFont = true;
            }
        }

        /// <summary>
        /// Specifies the font size to use for displayed messages.
        /// </summary>
        public float FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                _changeFont = true;
            }
        }

        //methods
        /// <summary>
        /// Erases currently displayed text in the MessageLogForm.
        /// </summary>
        public void Clear()
        {
            _messageLogForm.txtMessageLog.Clear();
        }

        /// <summary>
        /// Closes the MessageLogForm.
        /// </summary>
        public void CloseWindow()
        {
            _messageLogForm.Close();
        }

        /// <summary>
        /// Shifts focus to MessageLogForm.
        /// </summary>
        public void Focus()
        {
            _messageLogForm.Focus();
        }

        /// <summary>
        /// Hides MessageLogForm windows.
        /// </summary>
        public void HideWindow()
        {
            _messageLogForm.Hide();
        }

        /// <summary>
        /// Loads contents of specified file to the MessageLogForm display.
        /// </summary>
        /// <param name="filename">Path to file containing text to load.</param>
        public void LoadFile(string filename)
        {
            _messageLogForm.txtMessageLog.LoadFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Saves contents of the MessageLogForm display to a file.
        /// </summary>
        /// <param name="filename">Saves contents of message log window to specified file.</param>
        public void SaveFile(string filename)
        {
            _messageLogForm.txtMessageLog.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Makes the MessageLogForm visible.
        /// </summary>
        public void ShowWindow()
        {
            _messageLogForm.Show();
        }

        /// <summary>
        /// Writes a line of text to the MessageLogForm display.
        /// </summary>
        /// <param name="message">Text to output.</param>
        public void WriteLine(string message)
        {
            string text;
            string text2 = message.Replace("\0", " ");

            if (_changeFont)
            {
                ChangeMessageLogFont();
            }

            if (_showDatetime)
            {
                text = DateTime.Now.ToString("yyy/MM/dd hh:mm:ss ");
            }
            else
            {
                text = "";
            }
            _messageLogForm.txtMessageLog.Select(_messageLogForm.txtMessageLog.Text.Length, 1);
            _messageLogForm.txtMessageLog.SelectedText = text + text2 + "\r\n";
            if (RetainFocus)
            {
                Application.DoEvents();
                _messageLogForm.Focus();
            }
        }

        private void ChangeMessageLogFont()
        {
            _messageLogForm.txtMessageLog.Font = new System.Drawing.Font(_font, _fontSize);
            _changeFont = false;
        }





    }//end class
}//end namespace
