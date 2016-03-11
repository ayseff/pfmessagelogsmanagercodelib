using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Runtime.CompilerServices;

namespace AppGlobals
{
    /// <summary>
    /// Form for displaying messages.
    /// </summary>
    public partial class ModelessPopupDialogForm : Form
    {
        private bool _actionButtonPressed = false;
        private System.Timers.Timer _timer;
        private int _maxSecondsToDisplayPopup = 0;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// If true, then the form's action button has been pressed.
        ///  Usually, this would be used when action button is defined as a Cancel button
        /// </summary>
        public bool ActionButtonPressed
        {
            get
            {
                return _actionButtonPressed;
            }
            set
            {
                _actionButtonPressed = value;
                if (_actionButtonPressed == true)
                    this.Hide();

            }
        }

        /// <summary>
        /// Text to be displayed on the popup form.
        /// </summary>
        public string MessageText
        {
            get
            {
                return this.txtMessage.Text;
            }
            set
            {
                this.txtMessage.Text = value;
                ResizeForm();
                RefreshForm();
            }
        }

        /// <summary>
        /// Specifies the label text for the action button.
        /// </summary>
        public string ButtonText
        {
            get
            {
                return this.cmdActionButton.Text;
            }
            set
            {
                this.cmdActionButton.Text = value;
                RefreshForm();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ModelessPopupDialogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxSecondsToDisplayPopup">Maximum number of seconds to display the popup form.</param>
        public ModelessPopupDialogForm(int maxSecondsToDisplayPopup)
        {
            InitializeComponent();
            _maxSecondsToDisplayPopup = maxSecondsToDisplayPopup;
        }

        private void ModelessPopupDialogForm_Load(object sender, EventArgs e)
        {
            HideCaret(this.txtMessage.Handle);
            if (_maxSecondsToDisplayPopup > 0)
                SetTimerForAutomaticClose();
        }

        private void SetTimerForAutomaticClose()
        {
            this.Timer = new System.Timers.Timer();
            this.Timer.Enabled = false;
            this.Timer.Interval = _maxSecondsToDisplayPopup * 1000; //convert to milliseconds
            this.Timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Timer.Enabled = false;
            //this.Close();
            this.Invoke((MethodInvoker)delegate() { this.Close(); });
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


        private void cmdActionButton_Click_1(object sender, EventArgs e)
        {
            _actionButtonPressed = true;
            if(this.Timer != null)
                this.Timer.Enabled = false;
            this.Hide();
        }

        /// <summary>
        /// Override of Focus to hide the caret in the message textbox.
        /// </summary>
        /// <param name="e">Arguments passed to this routine from Widnows.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            HideCaret(this.txtMessage.Handle);
        }

        private void RefreshForm()
        {
            if (this.Visible)
            {
                this.Refresh();
                Application.DoEvents();
            }
        }

        private void ResizeForm()
        {
            this.txtMessage.SelectionStart = 0;
            this.txtMessage.SelectionLength = 0;

            Size size = TextRenderer.MeasureText(this.txtMessage.Text, this.txtMessage.Font);
            this.pnlMessage.Width = size.Width;
            this.pnlMessage.Height = size.Height;
            this.cmdActionButton.Top = this.pnlMessage.Bottom + 20;
            this.txtFiller.Top = this.cmdActionButton.Bottom + 2;
            this.cmdActionButton.Left = (this.pnlMessage.Width / 2) - (this.cmdActionButton.Width / 2);

            SetFocusToActionButton();
        }

        /// <summary>
        /// Move focus to the form's action button.
        /// </summary>
        public void SetFocusToActionButton()
        {
            this.cmdActionButton.Focus();
        }

    }//end class
}//end namespace
