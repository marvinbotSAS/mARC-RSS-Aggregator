using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSRTReader
{
    public partial class LoggerWindow : Form
    {

        public LoggerWindow()
        {
            InitializeComponent();

            writeTowindow = new WriteToWindow(this.Write);
        }

        public delegate void WriteToWindow(string[] lines);

        public WriteToWindow writeTowindow;

        public void AppendText(string s)
        {
            string[] lines = null;
            if (!string.IsNullOrEmpty(LogrichTextBox.Text))
            {
                lines = LogrichTextBox.Lines;
                if (lines != null && lines.Length > 500)
                {
                    LogrichTextBox.Text = "";
                }
            }
            LogrichTextBox.AppendText(s + "\n" );
        }

        public void Write(string[] lines)
        {
            if (this.LogrichTextBox.InvokeRequired)
            {
               
                this.Invoke(writeTowindow, new object[] { lines });
            }
            else
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    this.AppendText(lines[i]);
                }
            }
        }
    }
}
