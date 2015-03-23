namespace RSSRTReader
{
    partial class NewCustomDocument
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
            this.locationlabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // locationlabel
            // 
            this.locationlabel.AutoSize = true;
            this.locationlabel.Location = new System.Drawing.Point(23, 29);
            this.locationlabel.Name = "locationlabel";
            this.locationlabel.Size = new System.Drawing.Size(48, 13);
            this.locationlabel.TabIndex = 0;
            this.locationlabel.Text = "Location";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(78, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // NewCustomDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 304);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.locationlabel);
            this.Name = "NewCustomDocument";
            this.Text = "New Custom Document";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label locationlabel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}