namespace RSSRTReader
{
    partial class LoggerWindow
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
            this.LoggroupBox = new System.Windows.Forms.GroupBox();
            this.LogrichTextBox = new System.Windows.Forms.RichTextBox();
            this.LoggroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoggroupBox
            // 
            this.LoggroupBox.Controls.Add(this.LogrichTextBox);
            this.LoggroupBox.Location = new System.Drawing.Point(3, 2);
            this.LoggroupBox.Name = "LoggroupBox";
            this.LoggroupBox.Size = new System.Drawing.Size(1037, 258);
            this.LoggroupBox.TabIndex = 0;
            this.LoggroupBox.TabStop = false;
            this.LoggroupBox.Text = "Log";
            // 
            // LogrichTextBox
            // 
            this.LogrichTextBox.Location = new System.Drawing.Point(7, 20);
            this.LogrichTextBox.Name = "LogrichTextBox";
            this.LogrichTextBox.ReadOnly = true;
            this.LogrichTextBox.Size = new System.Drawing.Size(1030, 232);
            this.LogrichTextBox.TabIndex = 0;
            this.LogrichTextBox.Text = "";
            // 
            // LoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 262);
            this.Controls.Add(this.LoggroupBox);
            this.Name = "LoggerWindow";
            this.Text = "Logger Output";
            this.LoggroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox LoggroupBox;
        private System.Windows.Forms.RichTextBox LogrichTextBox;
    }
}