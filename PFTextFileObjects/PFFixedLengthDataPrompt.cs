using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFTextObjects;

namespace PFTextFiles
{
    /// <summary>
    /// Windows form that can be used to capture column and line delimiter selections for a delimited output file.
    /// </summary>
    public partial class PFFixedLengthDataPrompt : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _includeColumnHeadersInOutput = true;
        private bool _allowDataTruncation = false;
        private bool _useLineTerminator = true;
        private string _lineTerminatorChars = Environment.NewLine;
        private int _columnWidthForStringData = 255;
        private int _maximumAllowedColumnWidth = 1024;
        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFixedLengthDataPrompt()
        {
            InitializeComponent();
        }



        /// <summary>
        /// IncludeColumnHeadersInOutput Property.
        /// </summary>
        public bool IncludeColumnHeadersInOutput
        {
            get
            {
                return _includeColumnHeadersInOutput;
            }
            set
            {
                _includeColumnHeadersInOutput = value;
            }
        }

        /// <summary>
        /// AllowDataTruncation Property.
        /// </summary>
        public bool AllowDataTruncation
        {
            get
            {
                return _allowDataTruncation;
            }
            set
            {
                _allowDataTruncation = value;
            }
        }

        /// <summary>
        /// UseLineTerminator property.
        /// </summary>
        public bool UseLineTerminator
        {
            get
            {
                return _useLineTerminator;
            }
            set
            {
                _useLineTerminator = value;
            }
        }

        /// <summary>
        /// LineTerminatorChars Property.
        /// </summary>
        public string LineTerminatorChars
        {
            get
            {
                return _lineTerminatorChars;
            }
            set
            {
                _lineTerminatorChars = value;
            }
        }

        /// <summary>
        /// ColumnWidthForStringData Property.
        /// </summary>
        public int ColumnWidthForStringData
        {
            get
            {
                return _columnWidthForStringData;
            }
            set
            {
                _columnWidthForStringData = value;
            }
        }

        /// <summary>
        /// MaximumAllowedColumnWidth Property.
        /// </summary>
        public int MaximumAllowedColumnWidth
        {
            get
            {
                return _maximumAllowedColumnWidth;
            }
            set
            {
                _maximumAllowedColumnWidth = value;
            }
        }


        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            ProcessSelections();
            this.DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            HideForm();
            this.DialogResult = DialogResult.Cancel;
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        /// <summary>
        /// Method to set form variables to their defaults.
        /// </summary>
        public void InitializeForm()
        {
            this.chkIncludeColumnHeadingsInOutput.Checked = true;
            this.chkAllowDataTruncation.Checked = false;
            this.chkUseLineTerminator.Checked = true;
            this.optCrLf.Checked = true;
            this.txtColumnWidthForStringData.Text = "255";
            this.txtMaximumAllowedColumnWidth.Text = "1024";

            EnableFormControls();
        }

        /// <summary>
        /// Method to hide the form.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Method to close the form.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
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

        private void ProcessSelections()
        {
            _includeColumnHeadersInOutput = this.chkIncludeColumnHeadingsInOutput.Checked;
            _allowDataTruncation = this.chkAllowDataTruncation.Checked;
            if (this.optCrLf.Checked)
                _lineTerminatorChars = Environment.NewLine;
            else if (this.optOtherLineTerminator.Checked)
                _lineTerminatorChars = this.txtOtherLineTerminator.Text;
            else
                _lineTerminatorChars = Environment.NewLine;
            if (_lineTerminatorChars == string.Empty)
                _lineTerminatorChars = " ";
            _columnWidthForStringData = AppTextGlobals.ConvertStringToInt(this.txtColumnWidthForStringData.Text, 255);
            _maximumAllowedColumnWidth = AppTextGlobals.ConvertStringToInt(this.txtMaximumAllowedColumnWidth.Text, 1024);
        }

    }//end class
}//end namespace
