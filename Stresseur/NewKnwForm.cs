namespace RSSRTReader
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NewKnwForm : Form
    {
        private Button button1;
        private IContainer components;
        private TextBox filterTable;
        private Label label1;
        private TextBox maxcels;
        private Label maxcelsLabel;
        private Label maxlinks;
        private Label nameLabel;
        private TextBox nom;
        private Button Cancelbutton;
        private TextBox textBox1;

        public NewKnwForm()
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

        public string GetFilter()
        {
            return this.filterTable.Text;
        }

        public int GetMaxCels()
        {
            return int.Parse(this.maxcels.Text);
        }

        public int GetMaxLinks()
        {
            return int.Parse(this.maxlinks.Text);
        }

        public string GetNom()
        {
            return this.nom.Text;
        }

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.maxcelsLabel = new System.Windows.Forms.Label();
            this.nom = new System.Windows.Forms.TextBox();
            this.maxcels = new System.Windows.Forms.TextBox();
            this.maxlinks = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filterTable = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(23, 28);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(27, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "nom";
            // 
            // maxcelsLabel
            // 
            this.maxcelsLabel.AutoSize = true;
            this.maxcelsLabel.Location = new System.Drawing.Point(23, 58);
            this.maxcelsLabel.Name = "maxcelsLabel";
            this.maxcelsLabel.Size = new System.Drawing.Size(103, 13);
            this.maxcelsLabel.TabIndex = 1;
            this.maxcelsLabel.Text = "nombre maxi de cels";
            // 
            // nom
            // 
            this.nom.Location = new System.Drawing.Point(193, 25);
            this.nom.Name = "nom";
            this.nom.Size = new System.Drawing.Size(274, 20);
            this.nom.TabIndex = 2;
            this.nom.Text = "RSSknw";
            // 
            // maxcels
            // 
            this.maxcels.Location = new System.Drawing.Point(193, 55);
            this.maxcels.Name = "maxcels";
            this.maxcels.Size = new System.Drawing.Size(274, 20);
            this.maxcels.TabIndex = 3;
            this.maxcels.Text = "1000000";
            // 
            // maxlinks
            // 
            this.maxlinks.AutoSize = true;
            this.maxlinks.Location = new System.Drawing.Point(23, 91);
            this.maxlinks.Name = "maxlinks";
            this.maxlinks.Size = new System.Drawing.Size(157, 13);
            this.maxlinks.TabIndex = 4;
            this.maxlinks.Text = "nombre maxi de liens associatifs";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(193, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(274, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "130000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "table ASCII associée";
            // 
            // filterTable
            // 
            this.filterTable.Location = new System.Drawing.Point(193, 120);
            this.filterTable.Name = "filterTable";
            this.filterTable.Size = new System.Drawing.Size(274, 20);
            this.filterTable.TabIndex = 7;
            this.filterTable.Text = "null";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(292, 157);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 9;
            this.Cancelbutton.Text = "Annuler";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // NewFieldForm
            // 
            this.ClientSize = new System.Drawing.Size(479, 192);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filterTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.maxlinks);
            this.Controls.Add(this.maxcels);
            this.Controls.Add(this.nom);
            this.Controls.Add(this.maxcelsLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "NewFieldForm";
            this.Text = "Création d\'un nouveau Knowledge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}

