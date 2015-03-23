namespace RSSRTReader
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class NewTblForm : Form
    {
        private Button button1;
        private IContainer components;
        private Label typeLabel;
        private TextBox lines;
        private Label linesLabel;
        private Label nameLabel;
        private TextBox nom;
        private Button Cancelbutton;
        private GroupBox FieldsgroupBox;
        private Button Editbutton;
        private Button Removebutton;
        private Button Addbutton;
        private ListBox FieldslistBox;
        private ComboBox typecomboBox;

        public NewTblForm()
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

        public int GetLines()
        {
            return int.Parse(this.lines.Text);
        }

        public string GetNom()
        {
            return this.nom.Text;
        }
        public string GetFieldType()
        {
            return typecomboBox.Text;
        }
        public string getFields()
        {
            string fields = null;
            if ( FieldslistBox.Items.Count == 0 )
                return fields;

            for (int i = 0; i < FieldslistBox.Items.Count; i++)
            {
                fields += (string)FieldslistBox.Items[i];
                if (i != FieldslistBox.Items.Count - 1)
                    fields += ", ";
            }
            fields += ", guid UINT64"; // le guid est unique pour un article de flux RSS

            return fields;
        }
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.linesLabel = new System.Windows.Forms.Label();
            this.nom = new System.Windows.Forms.TextBox();
            this.lines = new System.Windows.Forms.TextBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.typecomboBox = new System.Windows.Forms.ComboBox();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.FieldsgroupBox = new System.Windows.Forms.GroupBox();
            this.Addbutton = new System.Windows.Forms.Button();
            this.Removebutton = new System.Windows.Forms.Button();
            this.Editbutton = new System.Windows.Forms.Button();
            this.FieldslistBox = new System.Windows.Forms.ListBox();
            this.FieldsgroupBox.SuspendLayout();
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
            // linesLabel
            // 
            this.linesLabel.AutoSize = true;
            this.linesLabel.Location = new System.Drawing.Point(23, 58);
            this.linesLabel.Name = "linesLabel";
            this.linesLabel.Size = new System.Drawing.Size(140, 13);
            this.linesLabel.TabIndex = 1;
            this.linesLabel.Text = "nombre de lignes périsionnel";
            // 
            // nom
            // 
            this.nom.Location = new System.Drawing.Point(193, 25);
            this.nom.Name = "nom";
            this.nom.Size = new System.Drawing.Size(274, 20);
            this.nom.TabIndex = 2;
            this.nom.Text = "RSSMaster";
            // 
            // lines
            // 
            this.lines.Location = new System.Drawing.Point(193, 55);
            this.lines.Name = "lines";
            this.lines.Size = new System.Drawing.Size(274, 20);
            this.lines.TabIndex = 3;
            this.lines.Text = "1000000";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(23, 86);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(27, 13);
            this.typeLabel.TabIndex = 6;
            this.typeLabel.Text = "type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 386);
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
            "MASTER",
            "SIMPLE"});
            this.typecomboBox.Location = new System.Drawing.Point(193, 86);
            this.typecomboBox.Name = "typecomboBox";
            this.typecomboBox.Size = new System.Drawing.Size(121, 21);
            this.typecomboBox.TabIndex = 9;
            this.typecomboBox.Text = "MASTER";
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(592, 386);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 10;
            this.Cancelbutton.Text = "Annuler";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // FieldsgroupBox
            // 
            this.FieldsgroupBox.Controls.Add(this.Editbutton);
            this.FieldsgroupBox.Controls.Add(this.Removebutton);
            this.FieldsgroupBox.Controls.Add(this.Addbutton);
            this.FieldsgroupBox.Controls.Add(this.FieldslistBox);
            this.FieldsgroupBox.Location = new System.Drawing.Point(26, 131);
            this.FieldsgroupBox.Name = "FieldsgroupBox";
            this.FieldsgroupBox.Size = new System.Drawing.Size(641, 249);
            this.FieldsgroupBox.TabIndex = 12;
            this.FieldsgroupBox.TabStop = false;
            this.FieldsgroupBox.Text = "Champs";
            // 
            // Addbutton
            // 
            this.Addbutton.Location = new System.Drawing.Point(6, 220);
            this.Addbutton.Name = "Addbutton";
            this.Addbutton.Size = new System.Drawing.Size(75, 23);
            this.Addbutton.TabIndex = 1;
            this.Addbutton.Text = "Ajouter";
            this.Addbutton.UseVisualStyleBackColor = true;
            this.Addbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Addbutton_MouseClick);
            // 
            // Removebutton
            // 
            this.Removebutton.Location = new System.Drawing.Point(87, 220);
            this.Removebutton.Name = "Removebutton";
            this.Removebutton.Size = new System.Drawing.Size(75, 23);
            this.Removebutton.TabIndex = 2;
            this.Removebutton.Text = "Enlever";
            this.Removebutton.UseVisualStyleBackColor = true;
            this.Removebutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Removebutton_MouseClick);
            // 
            // Editbutton
            // 
            this.Editbutton.Location = new System.Drawing.Point(168, 220);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(75, 23);
            this.Editbutton.TabIndex = 3;
            this.Editbutton.Text = "Modifier";
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Editbutton_MouseClick);
            // 
            // FieldslistBox
            // 
            this.FieldslistBox.FormattingEnabled = true;
            this.FieldslistBox.Location = new System.Drawing.Point(6, 19);
            this.FieldslistBox.Name = "FieldslistBox";
            this.FieldslistBox.Size = new System.Drawing.Size(629, 186);
            this.FieldslistBox.TabIndex = 0;
            // 
            // NewTblForm
            // 
            this.ClientSize = new System.Drawing.Size(670, 412);
            this.Controls.Add(this.FieldsgroupBox);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.typecomboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.lines);
            this.Controls.Add(this.nom);
            this.Controls.Add(this.linesLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "NewTblForm";
            this.Text = "Création d\'une nouvelle Table";
            this.FieldsgroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Addbutton_MouseClick(object sender, MouseEventArgs e)
        {
            Addbutton.Enabled = false;

            NewFieldForm f = new NewFieldForm();
            string row=null;
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                row = f.GetNom()+" ";
                string t = f.GetFieldType();
                row += t;
                if (t.Equals("CHAR"))
                    row += " " + f.getCharN();

                FieldslistBox.Items.Add(row);
                FieldslistBox.Refresh();
            }
            Addbutton.Enabled = true;
                
        }

        private void Removebutton_MouseClick(object sender, MouseEventArgs e)
        {
            if (FieldslistBox.SelectedItem != null)
            {
                FieldslistBox.Items.RemoveAt(FieldslistBox.SelectedIndex);
                FieldslistBox.Refresh();
            }
        }

        private void Editbutton_MouseClick(object sender, MouseEventArgs e)
        {
            if (FieldslistBox.SelectedItem != null)
            {
                string s = (string)  FieldslistBox.SelectedItem;
                char[] sep = {' '};
                string[] st = s.Split(sep);
                NewFieldForm f = new NewFieldForm();
                string row = null;
                f.SetNom(st[0]);
                if (st.Length == 2)
                    f.SetType(st[1], "null");
                else
                    f.SetType(st[1], st[2]);
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    row = f.GetNom() + " " + f.GetType();
                    if (f.GetType().Equals("CHAR"))
                        row += " " + f.getCharN();
                    FieldslistBox.SelectedItem = row;
                    FieldslistBox.Refresh();
                }
                f.Dispose();
            }

        }


    }
}

