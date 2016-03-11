namespace AppGlobals
{
    partial class ModelessPopupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdActionButton = new System.Windows.Forms.Button();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtFiller = new System.Windows.Forms.TextBox();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdActionButton
            // 
            this.cmdActionButton.Location = new System.Drawing.Point(80, 96);
            this.cmdActionButton.Name = "cmdActionButton";
            this.cmdActionButton.Size = new System.Drawing.Size(100, 23);
            this.cmdActionButton.TabIndex = 4;
            this.cmdActionButton.Text = "Cancel";
            this.cmdActionButton.UseVisualStyleBackColor = true;
            this.cmdActionButton.Click += new System.EventHandler(this.cmdActionButton_Click_1);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.txtMessage);
            this.pnlMessage.Location = new System.Drawing.Point(12, 12);
            this.pnlMessage.MaximumSize = new System.Drawing.Size(750, 650);
            this.pnlMessage.MinimumSize = new System.Drawing.Size(240, 63);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(240, 63);
            this.pnlMessage.TabIndex = 3;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.MaximumSize = new System.Drawing.Size(750, 650);
            this.txtMessage.MinimumSize = new System.Drawing.Size(240, 63);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(240, 63);
            this.txtMessage.TabIndex = 0;
            // 
            // txtFiller
            // 
            this.txtFiller.BackColor = System.Drawing.SystemColors.Control;
            this.txtFiller.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFiller.Location = new System.Drawing.Point(80, 121);
            this.txtFiller.Name = "txtFiller";
            this.txtFiller.Size = new System.Drawing.Size(100, 13);
            this.txtFiller.TabIndex = 5;
            // 
            // ModelessPopupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(262, 138);
            this.ControlBox = false;
            this.Controls.Add(this.cmdActionButton);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.txtFiller);
            this.Name = "ModelessPopupDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModelessPopupDialogForm";
            this.Load += new System.EventHandler(this.ModelessPopupDialogForm_Load);
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdActionButton;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtFiller;
    }
}