namespace RSSRTReader
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NewRSSFeedForm : Form
    {
        private Button button1;
        private IContainer components;
        private Label nameLabel;
        private Button Cancelbutton;
        private Label Titlelabel;
        private TextBox TitletextBox;
        private TextBox URL;

        public NewRSSFeedForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }
        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetTitle()
        {
            return this.TitletextBox.Text;
        }

        public void SetTitle(string name)
        {
            this.TitletextBox.Text = name;
        }

        public string GetURL()
        {
            return this.URL.Text;
        }

        public void SetURL(string name)
        {
            this.URL.Text = name;
        }


        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.URL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.Titlelabel = new System.Windows.Forms.Label();
            this.TitletextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(10, 66);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(29, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "URL";
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(45, 63);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(431, 20);
            this.URL.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(244, 89);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 12;
            this.Cancelbutton.Text = "Annuler";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // Titlelabel
            // 
            this.Titlelabel.AutoSize = true;
            this.Titlelabel.Location = new System.Drawing.Point(13, 27);
            this.Titlelabel.Name = "Titlelabel";
            this.Titlelabel.Size = new System.Drawing.Size(27, 13);
            this.Titlelabel.TabIndex = 13;
            this.Titlelabel.Text = "Title";
            // 
            // TitletextBox
            // 
            this.TitletextBox.Location = new System.Drawing.Point(45, 24);
            this.TitletextBox.Name = "TitletextBox";
            this.TitletextBox.Size = new System.Drawing.Size(431, 20);
            this.TitletextBox.TabIndex = 14;
            // 
            // NewRSSFeedForm
            // 
            this.ClientSize = new System.Drawing.Size(488, 118);
            this.Controls.Add(this.TitletextBox);
            this.Controls.Add(this.Titlelabel);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.nameLabel);
            this.Name = "NewRSSFeedForm";
            this.Text = "Création d\'un nouveau RSS Feed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}

