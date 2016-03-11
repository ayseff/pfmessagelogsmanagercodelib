//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobals
{
    /// <summary>
    /// Class to manage display of modeless dialog form.
    /// </summary>
    public class ModelessPopupDialog
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private ModelessPopupDialogForm _frm = null;
        private string _message = string.Empty;
        private string _buttonLabel = string.Empty;
        private int _maxSecondsToDisplayPopup = 0;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public ModelessPopupDialog()
        {
            InitInstance((int)0);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxSecondsToDisplayPopup">Specifies number of seconds to display the message before closing the dialog form automatically.</param>
        /// <remarks>Leave blank or specify 0 to disable automatic close.</remarks>
        public ModelessPopupDialog(int maxSecondsToDisplayPopup)
        {
            InitInstance(maxSecondsToDisplayPopup);
        }

        private void InitInstance(int maxSecondsToDisplayPopup)
        {
            _maxSecondsToDisplayPopup = maxSecondsToDisplayPopup;
            if (_maxSecondsToDisplayPopup > 0)
                _frm = new ModelessPopupDialogForm(_maxSecondsToDisplayPopup);
            else
                _frm = new ModelessPopupDialogForm();
        }

        //properties

        /// <summary>
        /// Max number of seconds to display the message before closing the dialog form automatically.
        /// If set to 0 or a negative number, dialog will not close automatically. Calling module will have to explicitly close the form.
        /// </summary>
        public int MaxSecondsToDisplayPopup
        {
            get
            {
                return _maxSecondsToDisplayPopup;
            }
            set
            {
                _maxSecondsToDisplayPopup = value;
            }
        }

        /// <summary>
        /// Provides access to the properties of the underlying Windows Form.
        /// </summary>
        public ModelessPopupDialogForm Frm
        {
            get
            {
                return _frm;
            }
        }

        /// <summary>
        /// Message to be displayed.
        /// </summary>
        public string Message
        {
            get
            {
                return _frm.MessageText;
            }
            set
            {
                _frm.MessageText = value;
            }
        }

        /// <summary>
        /// Level for the form's button.
        /// </summary>
        public string ButtonLabel
        {
            get
            {
                return _buttonLabel;
            }
            set
            {
                _buttonLabel = value;
            }
        }

        /// <summary>
        /// Returns true if the popup form is currently visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _frm.Visible;
            }
            set
            {
                _frm.Visible = value;
            }
        }

        /// <summary>
        /// If true, then the action button for the modeless popup dialog has been pressed. (For example, Cancel button is pressed.)
        /// </summary>
        public bool ActionButtonPressed
        {
            get
            {
                return _frm.ActionButtonPressed;
            }
            set
            {
                _frm.ActionButtonPressed = value;
            }
        }

        //methods

        /// <summary>
        /// Opens a modeless dialog box and displays the text specified in the parameter.
        /// </summary>
        public void Show()
        {
            _frm.Show();
        }

        /// <summary>
        /// Opens a modeless dialog box and displays the text specified in the parameter.
        /// </summary>
        /// <param name="messageToShow">Message to display on the form.</param>
        public void Show(string messageToShow)
        {
            _frm.MessageText = messageToShow;
            //_frm.Show();
            this.Show();
        }

        /// <summary>
        /// Opens a modeless dialog box and displays the text specified in the parameter.
        /// </summary>
        /// <param name="messageToShow">Message to display on the form.</param>
        /// <param name="buttonLabel">Label to display on the form's button. Default is Cancel.</param>
        public void Show(string messageToShow, string buttonLabel)
        {
            _frm.MessageText = messageToShow;
            _frm.ButtonText = buttonLabel;
            //_frm.Show();
            this.Show();
        }


        /// <summary>
        /// Closes the modeless popup form.
        /// </summary>
        public void Close()
        {
            _frm.Close();
        }

        /// <summary>
        /// Set focus to modeless popup's action button.
        /// </summary>
        public void SetFocusToPopup()
        {
            _frm.SetFocusToActionButton();
        }


    }//end class
}//end namespace
