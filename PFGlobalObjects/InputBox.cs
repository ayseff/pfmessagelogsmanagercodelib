using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;

namespace AppGlobals
{
    /// <summary>
    /// Class to display an form where text data can be input.
    /// </summary>
    public partial class InputBox : Form
    {
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _caption = "Input Box";
        private string _inputText = string.Empty;
        private string _inputTextLabel = "Input text:";
        private string _okButtonText = "OK";
        private string _cancelButtonText = "&Cancel";

        /// <summary>
        /// Constructor
        /// </summary>

        public InputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        ///<param name="parent">Parent window for the input box.</param>
        public InputBox(Control parent)
        {
            this.Parent = parent;
            InitializeComponent();
        }

        //properties
        /// <summary>
        /// Caption Property.
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
                InitializeFormLabels();
            }
        }

        /// <summary>
        /// InputText Property.
        /// </summary>
        public string InputText
        {
            get
            {
                return _inputText;
            }
            set
            {
                _inputText = value;
                InitializeFormLabels();
            }
        }

        /// <summary>
        /// InputTextLabel Property.
        /// </summary>
        public string InputTextLabel
        {
            get
            {
                return _inputTextLabel;
            }
            set
            {
                _inputTextLabel = value;
                InitializeFormLabels();
            }
        }

        /// <summary>
        /// OkButtonText Property.
        /// </summary>
        public string OkButtonText
        {
            get
            {
                return _okButtonText;
            }
            set
            {
                _okButtonText = value;
                InitializeFormLabels();
            }
        }

        /// <summary>
        /// CancelButtonText Property.
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return _cancelButtonText;
            }
            set
            {
                _cancelButtonText = value;
                InitializeFormLabels();
            }
        }





        //button click events

        
        
        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        private void InitializeForm()
        {
            this.SetStyle(ControlStyles.DoubleBuffer |
                           ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint,
                           true);
            this.UpdateStyles();

            InitializeFormLabels();

            EnableFormControls();
        }

        private void InitializeFormLabels()
        {
            this.Text = _caption;
            this.lblInputData.Text = _inputTextLabel;
            this.txtInputData.Text = _inputText;
            this.cmdOK.Text = _okButtonText;
            this.cmdCancel.Text = _cancelButtonText;
        }

        /// <summary>
        /// Method to hide form.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Method to close form.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.InputText = this.txtInputData.Text;
            this.DialogResult = DialogResult.OK;
            this.CloseForm();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.CloseForm();
        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = true;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = true;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = true;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = true;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = true;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = true;
                }

            }//end foreach
        }//end method

        private void DisableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = false;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = false;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = false;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = false;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = false;
                }

            }//end foreach control

        }

        //Application routines

    }//end class
}//end namespace
