using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;

namespace PFTextFiles
{
    /// <summary>
    /// Windows form that can be used to capture column and line delimiter selections for a delimited output file.
    /// </summary>
    public partial class PFDataDelimitersPrompt : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private string _columnDelimiter = ",";
        private string _lineTerminator = "\r\n";
        private bool _includeColumnHeadersInOutput = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFDataDelimitersPrompt()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ColumnDelimiter Property.
        /// </summary>
        public string ColumnDelimiter
        {
            get
            {
                return _columnDelimiter;
            }
            set
            {
                _columnDelimiter = value;
            }
        }

        /// <summary>
        /// LineTerminator Property.
        /// </summary>
        public string LineTerminator
        {
            get
            {
                return _lineTerminator;
            }
            set
            {
                _lineTerminator = value;
            }
        }


        /// <summary>
        /// IncludeColumnHeadersInOutput property.
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
            this.optCommaDelimited.Checked = true;
            this.optCrLf.Checked = true;
            this.chkIncludeColumnHeadingsInOutput.Checked = true;

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
            if (this.optCommaDelimited.Checked)
                _columnDelimiter = ",";
            else if (this.optTabDelimited.Checked)
                _columnDelimiter = "\t";
            else if (this.optOtherDelimiter.Checked)
                _columnDelimiter = this.txtOtherSeparator.Text;
            else
                _columnDelimiter = ",";
            if (_columnDelimiter == string.Empty)
                _columnDelimiter = " ";

            if (this.optCrLf.Checked)
                _lineTerminator = "\r\n";
            else if (this.optOtherLineTerminator.Checked)
                _lineTerminator = this.txtOtherLineTerminator.Text;
            else
                _lineTerminator = "\r\n";
            if (_lineTerminator == string.Empty)
                _lineTerminator = " ";

            if (this.chkIncludeColumnHeadingsInOutput.Checked)
                _includeColumnHeadersInOutput = true;
            else
                _includeColumnHeadersInOutput = false;
        
        }

    }//end class
}//end namespace
