using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace PFMessageLogs
{
    internal class TextPrinter
    {
        // Fields
        //private int _STATIC_pdoc_PrintPage_20211C124D_nCurrentChar;
        private int _currentChar;
        [AccessedThroughProperty("pdoc")]
        private PrintDocument _pdoc;
        private bool _showPageNumbers;
        private int _pageCount;
        private Font _font;
        private string _textToPrint;
        private string _title;

        // Methods
        public TextPrinter()
        {
            this.pdoc = new PrintDocument();
            this._textToPrint = "No text specified for print. You must set the TextToPrint property.";
            this._font = new Font("Microsoft Sans Serif", 10f);
            this._title = "";
            this._showPageNumbers = true;
            this._pageCount = 0;
        }

        private void pdoc_BeginPrint(object sender, PrintEventArgs e)
        {
            this._pageCount = 0;
        }

        private void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charsFitted;
            int linesFilled;
            int printAreaHeight;
            int printAreaWidth;
            PageSettings defaultPageSettings = this.pdoc.DefaultPageSettings;
            if (!defaultPageSettings.Landscape)
            {
                printAreaHeight = (defaultPageSettings.PaperSize.Height - defaultPageSettings.Margins.Top) - defaultPageSettings.Margins.Bottom;
                printAreaWidth = (defaultPageSettings.PaperSize.Width - defaultPageSettings.Margins.Left) - defaultPageSettings.Margins.Right;
            }
            else
            {
                printAreaHeight = (defaultPageSettings.PaperSize.Width - defaultPageSettings.Margins.Top) - defaultPageSettings.Margins.Bottom;
                printAreaWidth = (defaultPageSettings.PaperSize.Height - defaultPageSettings.Margins.Left) - defaultPageSettings.Margins.Right;
            }
            int left = defaultPageSettings.Margins.Left;
            int top = defaultPageSettings.Margins.Top;
            int right = defaultPageSettings.Margins.Right;
            int bottom = defaultPageSettings.Margins.Bottom;
            int footerBegin = top + printAreaHeight;
            defaultPageSettings = null;
            StringFormat stringFormat = new StringFormat(StringFormatFlags.LineLimit);
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
            format.Alignment = StringAlignment.Center;
            StringFormat format2 = new StringFormat(StringFormatFlags.LineLimit);
            format2.Alignment = StringAlignment.Center;
            this._pageCount++;
            int headerHeight = top;
            int headerWidth = printAreaWidth;
            RectangleF layoutRectangle = new RectangleF((float)left, (float)(((double)top) / 2), (float)printAreaWidth, (float)(((double)top) / 2));
            if (this._title.Length > 0)
            {
                e.Graphics.DrawString(this._title, this._font, Brushes.Black, layoutRectangle, format);
            }
            int footerHeight = bottom;
            int footerWidth = printAreaWidth;
            RectangleF footerPrintingArea = new RectangleF((float)left, (float)footerBegin, (float)printAreaWidth, (float)(((double)bottom) / 2));
            if (this._showPageNumbers)
            {
                e.Graphics.DrawString("Page " + this._pageCount.ToString(), this._font, Brushes.Black, footerPrintingArea, format2);
            }
            int lineCount = (int)Math.Round((double)(((double)printAreaHeight) / ((double)this._font.Height)));
            RectangleF printingArea = new RectangleF((float)left, (float)top, (float)printAreaWidth, (float)printAreaHeight);
            SizeF layoutArea = new SizeF((float)printAreaWidth, (float)printAreaHeight);
            e.Graphics.MeasureString(Strings.Mid(this._textToPrint, this._currentChar + 1), this._font, layoutArea, stringFormat, out charsFitted, out linesFilled);
            e.Graphics.DrawString(Strings.Mid(this._textToPrint, this._currentChar + 1), this._font, Brushes.Black, printingArea, stringFormat);
            this._currentChar += charsFitted;
            if (this._currentChar < this._textToPrint.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                this._currentChar = 0;
            }
        }

        public void Print()
        {
            this.pdoc.DocumentName = this._title;
            this.pdoc.Print();
        }

        public void Print(bool bShowPrintDialog)
        {
            if (bShowPrintDialog)
            {
                PrintDialog dialog = new PrintDialog();
                dialog.Document = this.pdoc;
                dialog.Document.DocumentName = this._title;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Print();
                }
            }
            else
            {
                this.Print();
            }
        }

        public void Print(string rsTextToPrint)
        {
            this._textToPrint = rsTextToPrint;
            this.Print();
        }

        public void Print(string rsTextToPrint, bool rbShowPrintDialog)
        {
            this._textToPrint = rsTextToPrint;
            this.Print(rbShowPrintDialog);
        }

        public void ShowPageSettings()
        {
            PageSetupDialog dialog = new PageSetupDialog();
            PageSetupDialog dialog2 = dialog;
            dialog2.Document = this.pdoc;
            dialog2.Document.DocumentName = this._title;
            dialog2.PageSettings = this.pdoc.DefaultPageSettings;
            dialog2 = null;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.pdoc.DefaultPageSettings = dialog.PageSettings;
            }
        }

        public void ShowPrintDialog()
        {
            PrintDialog dialog = new PrintDialog();
            dialog.Document = this.pdoc;
            dialog.Document.DocumentName = this._title;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Print();
            }
        }

        public void ShowPrintPreview()
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            try
            {
                dialog.Document = this.pdoc;
                dialog.Document.DocumentName = this._title;
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to load the document for Print Preview. Make sure you currently have access to a printer. A printer must be connected and accessible for Print Preview to work.\n" + ex.Message, "Print Preview", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        // Properties
        public Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        private PrintDocument pdoc
        {
            get
            {
                return this._pdoc;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (this._pdoc != null)
                {
                    this._pdoc.PrintPage -= new PrintPageEventHandler(this.pdoc_PrintPage);
                    this._pdoc.BeginPrint -= new PrintEventHandler(this.pdoc_BeginPrint);
                }
                this._pdoc = value;
                if (this._pdoc != null)
                {
                    this._pdoc.PrintPage += new PrintPageEventHandler(this.pdoc_PrintPage);
                    this._pdoc.BeginPrint += new PrintEventHandler(this.pdoc_BeginPrint);
                }
            }
        }

        public bool ShowPageNumbers
        {
            get
            {
                return this._showPageNumbers;
            }
            set
            {
                this._showPageNumbers = value;
            }
        }

        public string TextToPrint
        {
            get
            {
                return this._textToPrint;
            }
            set
            {
                this._textToPrint = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
    }

}
