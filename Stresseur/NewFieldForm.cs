namespace RSSRTReader
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NewFieldForm : Form
    {
        private Button button1;
        private IContainer components;
        private Label nameLabel;
        private ComboBox typecomboBox;
        private Label label1;
        private TextBox CharNtextBox;
        private Button Cancelbutton;
        private TextBox nom;

        public NewFieldForm()
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



        public string GetNom()
        {
            return this.nom.Text;
        }

        public void SetNom(string name)
        {
            this.nom.Text = name;
        }

        public void SetType( string type, string charN)
        {
            this.typecomboBox.Text = type;
            if ( type.Equals("CHAR") )
                   this.CharNtextBox.Text = charN;
        }

        public string GetFieldType()
        {

            return this.typecomboBox.Text;

        }

        public string getCharN()
        {
            return CharNtextBox.Text;
        }

        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.nom = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.typecomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CharNtextBox = new System.Windows.Forms.TextBox();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 25);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(27, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "nom";
            // 
            // nom
            // 
            this.nom.Location = new System.Drawing.Point(45, 22);
            this.nom.Name = "nom";
            this.nom.Size = new System.Drawing.Size(274, 20);
            this.nom.TabIndex = 2;
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
            // typecomboBox
            // 
            this.typecomboBox.FormattingEnabled = true;
            this.typecomboBox.Items.AddRange(new object[] {
            "STRING",
            "CHAR",
            "INT64",
            "UINT64",
            "INT32",
            "UINT32",
            "INT8",
            "UINT8",
            "FLOAT",
            "DOUBLE",
            "BOOL",
            "SIMPLEDATE"});
            this.typecomboBox.Location = new System.Drawing.Point(45, 48);
            this.typecomboBox.Name = "typecomboBox";
            this.typecomboBox.Size = new System.Drawing.Size(121, 21);
            this.typecomboBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Type";
            // 
            // CharNtextBox
            // 
            this.CharNtextBox.Location = new System.Drawing.Point(172, 48);
            this.CharNtextBox.Name = "CharNtextBox";
            this.CharNtextBox.Size = new System.Drawing.Size(100, 20);
            this.CharNtextBox.TabIndex = 11;
            this.CharNtextBox.TextChanged += new System.EventHandler(this.CharNtextBox_TextChanged);
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
            // NewFieldForm
            // 
            this.ClientSize = new System.Drawing.Size(327, 118);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.CharNtextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typecomboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nom);
            this.Controls.Add(this.nameLabel);
            this.Name = "NewFieldForm";
            this.Text = "Création d\'un nouveau Knowledge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CharNtextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(CharNtextBox.Text) > 255)
            {
                CharNtextBox.Text = "254";
            }
            if (int.Parse(CharNtextBox.Text) < 1)
            {
                CharNtextBox.Text = "1";
            }
        }


    }
}

