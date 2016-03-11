using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PFMessageLogs
{
    /// <summary>
    /// Classes containing the routines for updating the MessageLogForm.
    /// </summary>
    public class MessageLog
    {
        // Fields
        private bool _retainFocus = true;
        private bool _showDatetime = false;
        private MessageLogForm _messageLogForm = new MessageLogForm();
        private string _caption = "Message Log";
        private string _font = "Microsoft Sans Serif";
        private float _fontSize = (float)8.25;
        private bool _changeFont = false;

        // Methods
        /// <summary>
        /// Contructor. Initializes various class variables.
        /// </summary>
        public MessageLog()
        {
            MessageLogForm messageLogForm = this._messageLogForm;
            messageLogForm.WindowState = FormWindowState.Normal;
            messageLogForm.Text = this._caption;
            messageLogForm.txtMessageLog.Text = "";
            //messageLogForm.txtMessageLog.Font = new System.Drawing.Font(_font, _fontSize);
            ChangeMessageLogFont();
            messageLogForm.Hide();
            messageLogForm = null;
        }

        // Properties
        /// <summary>
        /// If true the File/Erase menu item will be displayed on the MessageLogForm.
        /// </summary>
        public bool AllowFileErase
        {
            get
            {
                return this._messageLogForm.AllowFileErase;
            }
            set
            {
                this._messageLogForm.AllowFileErase = value;
            }
        }

        /// <summary>
        /// Sets MessageLogForm caption at top of form.
        /// </summary>
        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                this._caption = value;
                this._messageLogForm.Text = value;
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
                return this._messageLogForm;
            }
        }

        /// <summary>
        /// Returns whether or not the MessageLogForm is currently visible.
        /// </summary>
        public bool FormIsVisible
        {
            get
            {
                return this._messageLogForm.Visible;
            }
        }

        /// <summary>
        /// Forces focus to remain on MessageLogForm after a line of text is written.
        /// </summary>
        public bool RetainFocus
        {
            get
            {
                return this._retainFocus;
            }
            set
            {
                this._retainFocus = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show the date and time with each message displayed on MessageLogForm.
        /// </summary>
        public bool ShowDatetime
        {
            get
            {
                return this._showDatetime;
            }
            set
            {
                this._showDatetime = value;
            }
        }

        /// <summary>
        /// Specifies the name of the font to use for the displayed messages.
        /// </summary>
        public string Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
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
                return this._fontSize;
            }
            set
            {
                this._fontSize = value;
                _changeFont = true;
            }
        }

        /// <summary>
        /// Erases currently displayed text in the MessageLogForm.
        /// </summary>
        public void Clear()
        {
            this._messageLogForm.txtMessageLog.Clear();
        }

        /// <summary>
        /// Closes the MessageLogForm.
        /// </summary>
        public void CloseWindow()
        {
            this._messageLogForm.Close();
        }

        /// <summary>
        /// Shifts focus to MessageLogForm.
        /// </summary>
        public void Focus()
        {
            this._messageLogForm.Focus();
        }

        /// <summary>
        /// Hides MessageLogForm windows.
        /// </summary>
        public void HideWindow()
        {
            this._messageLogForm.Hide();
        }

        /// <summary>
        /// Loads contents of specified file to the MessageLogForm display.
        /// </summary>
        /// <param name="filename">Path to file containing text to load.</param>
        public void LoadFile(string filename)
        {
            this._messageLogForm.txtMessageLog.LoadFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Saves contents of the MessageLogForm display to a file.
        /// </summary>
        /// <param name="filename">Saves contents of message log window to specified file.</param>
        public void SaveFile(string filename)
        {
            this._messageLogForm.txtMessageLog.SaveFile(filename, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Makes the MessageLogForm visible.
        /// </summary>
        public void ShowWindow()
        {
            this._messageLogForm.Show();
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

            if (this._showDatetime)
            {
                text = DateTime.Now.ToString("yyy/MM/dd hh:mm:ss ");
            }
            else
            {
                text = "";
            }
            this._messageLogForm.txtMessageLog.Select(this._messageLogForm.txtMessageLog.Text.Length, 1);
            this._messageLogForm.txtMessageLog.SelectedText = text + text2 + "\r\n";
            this._messageLogForm.txtMessageLog.SelectionStart = this._messageLogForm.txtMessageLog.Text.Length;
            this._messageLogForm.txtMessageLog.ScrollToCaret();
            if (this.RetainFocus)
            {
                Application.DoEvents();
                this._messageLogForm.Focus();
            }
        }

        private void ChangeMessageLogFont()
        {
            this._messageLogForm.txtMessageLog.Font = new System.Drawing.Font(_font, _fontSize);
            _changeFont = false;
        }



    }//end class

}//end namespace
