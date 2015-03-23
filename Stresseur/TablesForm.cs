namespace RSSRTReader
{
    using mARC;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Text;
    using System.Collections.Generic;
    public class TablesForm : Form
    {
        private IContainer components;
        private Connector _connector;
        private List<string[]> values = new List<string[]>();
        string[] headers;
        private BackgroundWorker GetTblInstancesbackgroundWorker;
        private BackgroundWorker GetTblFieldsbackgroundWorker;
        private BackgroundWorker GetTblDatabackgroundWorker;
        private Button backward;
        private TabControl ControlTabs;
        private int current_table_id;
        private TextBox fetchEnd;
        private TextBox fetchSize;
        private TextBox fetchStart;
        private Button forward;
        private string[] instances;
        private string[] names;
        private Button refresh;
        private Button refreshData;
        private ProgressBar refreshprogressBar;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private DataGridView tableData;
        private Label tableDataLabel;
        private Label tableNames;
        private ListBox tablesInstances;
        private int[] tablesTotalLines;
        private CheckedListBox tableStructure;
        private Label tableStructureLabel;
        private TabPage tabPage1;
        private TrackBar trackBar;
        private ProgressBar progressBar1;
        private ProgressBar progressBar2;
        private BackgroundWorker backgroundWorker4;
        private RichTextBox lineContent;
        private BackgroundWorker DisplayLineContentbackgroundWorker;
        private Button updateLine;
        private BackgroundWorker updateLinebackgroundWorker;
        private string[] types;


        public TablesForm(Connector connector)
        {
            this._connector = connector;
            this.InitializeComponent();
            this.trackBar.Height = 15;

            if (!backgroundWorker4.IsBusy)
            {
                tablesInstances.Items.Clear();
                _connector.Lock();
                backgroundWorker4.RunWorkerAsync();
            } 
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


            this._connector.Table().GetInstances("1","-1");
            instances = _connector.GetDataByName("Tables", -1);
            if ((this.instances != null) && (this.instances.Length != 0))
            {
                Array.Sort<string>(this.instances);
                
                foreach (string str in this.instances)
                {
                    tablesInstances.Items.Add(str);
                }
                tablesTotalLines = new int[this.instances.Length];
                float progressf = 0;
                for (int i = 0; i < this.instances.Length; i++)
                {
                    this._connector.Table().GetLines(this.instances[i]);
                    string[] dataByName = this._connector.GetDataByName("Lines", -1);
                    if ((dataByName != null) && (dataByName.Length > 0))
                    {
                        this.tablesTotalLines[i] = int.Parse(dataByName[0]);
                    }
                    else
                    {
                        this.tablesTotalLines[i] = 0;
                    }
                    progressf = (float)i / (float)instances.Length * 100;
                    GetTblInstancesbackgroundWorker.ReportProgress((Int32)progressf);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
        }

        private void GetTblFieldsbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            int index = current_table_id;
            float progressf = 0;
            this._connector.Table().GetStructure(this.instances[index]);
            this.names = this._connector.GetDataByName("Name", -1);
            if ((this.names != null) && (this.names.Length != 0))
            {
                this.types = this._connector.GetDataByName("Type", -1);
                if (types != null && types.Length > 0 )
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        progressf = (float)i / (float)types.Length * 100;
                        GetTblFieldsbackgroundWorker.ReportProgress((Int32) progressf);
                    }
                }
            }
        }
        private void GetTblFieldsbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            progressBar2.Value = 100;
            if ((types != null) && (types.Length != 0))
            {
                this.tableStructure.Items.Clear();
                for (int i = 0; i < this.names.Length; i++)
                {
                    this.tableStructure.Items.Add(this.types[i] + "    " + this.names[i]);
                }
                this.fetchEnd.Text = this.tablesTotalLines[current_table_id].ToString();
                this.trackBar.Maximum = this.tablesTotalLines[current_table_id];
                this.trackBar.Value = 0;
                this.fetchEnd.Refresh();
            }
        }

        private void GetTblDatabackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (((this.instances != null) && (this.instances.Length != 0)) && (this.tablesTotalLines[this.current_table_id] != 0))
            {
                values.Clear();
                CheckedListBox.CheckedIndexCollection checkedIndices = this.tableStructure.CheckedIndices;
                if (checkedIndices.Count != 0)
                {
                    int num2 = int.Parse(this.fetchSize.Text);
                    int num5 = int.Parse(this.fetchStart.Text);
                    int num6 = num5 + num2;
                    float num7 = 0f;
                    int index = 0;
                    string[] row;
                    string[] strArray4 ;
                    headers = new string[checkedIndices.Count];
                    int k = 0;
                    for ( k = 0; k < checkedIndices.Count; k++)
                    {
                     index = checkedIndices[k];
                     headers[k] = names[index];
                    }
                    for (int j = num5; j < num6; j++)
                    {
                        row = new string[checkedIndices.Count];
                        this._connector.Table().ReadLine(this.instances[this.current_table_id], j.ToString(), headers);
                        for (k = 0; k < checkedIndices.Count; k++)
                        {
                            strArray4 = this._connector.GetDataByName(headers[k].ToLower(), -1);
                            if (strArray4 != null)
                            {
                                row[k] = strArray4[0];
                            }
                            else
                            {
                                row[k] = " ";
                            }
                        }
                        values.Add(row);
                        num7 = (((float)(j - num5)) / ((float)(num6 - num5))) * 100f;
                        GetTblDatabackgroundWorker.ReportProgress((int)num7);
                    }
                }
            }
        }

        private void GetTblDatabackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.refreshprogressBar.Value = e.ProgressPercentage;
        }

        private void GetTblDatabackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            this.refreshprogressBar.Value = 100;
            this.tableData.Rows.Clear();
            this.tableData.Columns.Clear();
            CheckedListBox.CheckedIndexCollection checkedIndices = this.tableStructure.CheckedIndices;
            this.tableData.ColumnCount = checkedIndices.Count;
            if ( values.Count != 0  )
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    tableData.Columns[i].HeaderText = headers[i];
                }
                for (int i = 0; i < values.Count; i++)
                {
                        this.tableData.Rows.Add(values[i]);
                }
            }
            trackBar.Enabled = true;
        }

        private void backward_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int num = int.Parse(this.fetchStart.Text) - int.Parse(this.fetchSize.Text);
                if (num >= 1)
                {
                    this.fetchStart.Text = num.ToString();
                    _connector.Lock();
                    GetTblDatabackgroundWorker.RunWorkerAsync();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void fetchEnd_TextChanged(object sender, EventArgs e)
        {
            if ((((this.instances != null) && (this.instances.Length != 0)) && (this.current_table_id != -1)) && (int.Parse(this.fetchEnd.Text) > this.tablesTotalLines[this.current_table_id]))
            {
                this.fetchEnd.Text = this.tablesTotalLines[this.current_table_id].ToString();
            }
        }

        private void fetchStart_TextChanged(object sender, EventArgs e)
        {
            int num = int.Parse(this.fetchStart.Text);
            if (num < 0)
            {
                this.fetchStart.Text = "0";
                this.fetchStart.Refresh();
            }
            this.trackBar.Value = num;
        }

        private void forward_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int num = int.Parse(this.fetchStart.Text) + int.Parse(this.fetchSize.Text);
                if (num <= int.Parse(this.fetchEnd.Text))
                {
                    this.fetchStart.Text = num.ToString();
                    if (!GetTblDatabackgroundWorker.IsBusy)
                    {
                        _connector.Lock();
                        GetTblDatabackgroundWorker.RunWorkerAsync();
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.GetTblInstancesbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.GetTblFieldsbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.GetTblDatabackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tablesInstances = new System.Windows.Forms.ListBox();
            this.refresh = new System.Windows.Forms.Button();
            this.tableNames = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.tableStructureLabel = new System.Windows.Forms.Label();
            this.tableStructure = new System.Windows.Forms.CheckedListBox();
            this.updateLine = new System.Windows.Forms.Button();
            this.lineContent = new System.Windows.Forms.RichTextBox();
            this.backward = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            this.refreshData = new System.Windows.Forms.Button();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.refreshprogressBar = new System.Windows.Forms.ProgressBar();
            this.fetchSize = new System.Windows.Forms.TextBox();
            this.fetchEnd = new System.Windows.Forms.TextBox();
            this.fetchStart = new System.Windows.Forms.TextBox();
            this.tableDataLabel = new System.Windows.Forms.Label();
            this.tableData = new System.Windows.Forms.DataGridView();
            this.ControlTabs = new System.Windows.Forms.TabControl();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.DisplayLineContentbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.updateLinebackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).BeginInit();
            this.ControlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // GetTblInstancesbackgroundWorker
            // 
            this.GetTblInstancesbackgroundWorker.WorkerReportsProgress = true;
            this.GetTblInstancesbackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblInstancesbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.GetTblInstancesbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.GetTblInstancesbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // GetTblFieldsbackgroundWorker
            // 
            this.GetTblFieldsbackgroundWorker.WorkerReportsProgress = true;
            this.GetTblFieldsbackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblFieldsbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblFieldsbackgroundWorker_DoWork);
            this.GetTblFieldsbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GetTblFieldsbackgroundWorker_ProgressChanged);
            this.GetTblFieldsbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // GetTblDatabackgroundWorker
            // 
            this.GetTblDatabackgroundWorker.WorkerReportsProgress = true;
            this.GetTblDatabackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblDatabackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblDatabackgroundWorker_DoWork);
            this.GetTblDatabackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GetTblDatabackgroundWorker_ProgressChanged);
            this.GetTblDatabackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetTblDatabackgroundWorker_RunWorkerCompleted);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1335, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tables";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-4, -20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.updateLine);
            this.splitContainer1.Panel2.Controls.Add(this.lineContent);
            this.splitContainer1.Panel2.Controls.Add(this.backward);
            this.splitContainer1.Panel2.Controls.Add(this.forward);
            this.splitContainer1.Panel2.Controls.Add(this.refreshData);
            this.splitContainer1.Panel2.Controls.Add(this.trackBar);
            this.splitContainer1.Panel2.Controls.Add(this.refreshprogressBar);
            this.splitContainer1.Panel2.Controls.Add(this.fetchSize);
            this.splitContainer1.Panel2.Controls.Add(this.fetchEnd);
            this.splitContainer1.Panel2.Controls.Add(this.fetchStart);
            this.splitContainer1.Panel2.Controls.Add(this.tableDataLabel);
            this.splitContainer1.Panel2.Controls.Add(this.tableData);
            this.splitContainer1.Size = new System.Drawing.Size(1339, 485);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer2.Panel1.Controls.Add(this.tablesInstances);
            this.splitContainer2.Panel1.Controls.Add(this.refresh);
            this.splitContainer2.Panel1.Controls.Add(this.tableNames);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressBar2);
            this.splitContainer2.Panel2.Controls.Add(this.tableStructureLabel);
            this.splitContainer2.Panel2.Controls.Add(this.tableStructure);
            this.splitContainer2.Size = new System.Drawing.Size(349, 485);
            this.splitContainer2.SplitterDistance = 156;
            this.splitContainer2.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 459);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(69, 14);
            this.progressBar1.TabIndex = 5;
            // 
            // tablesInstances
            // 
            this.tablesInstances.FormattingEnabled = true;
            this.tablesInstances.Location = new System.Drawing.Point(6, 59);
            this.tablesInstances.Name = "tablesInstances";
            this.tablesInstances.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.tablesInstances.Size = new System.Drawing.Size(144, 381);
            this.tablesInstances.TabIndex = 3;
            this.tablesInstances.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tablesInstances_MouseClick);
            this.tablesInstances.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tablesInstances_KeyUp);
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(71, 26);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(79, 23);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refresh_MouseClick);
            // 
            // tableNames
            // 
            this.tableNames.AutoSize = true;
            this.tableNames.Location = new System.Drawing.Point(3, 23);
            this.tableNames.Name = "tableNames";
            this.tableNames.Size = new System.Drawing.Size(34, 13);
            this.tableNames.TabIndex = 1;
            this.tableNames.Text = "Noms";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(6, 464);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(69, 14);
            this.progressBar2.TabIndex = 2;
            // 
            // tableStructureLabel
            // 
            this.tableStructureLabel.AutoSize = true;
            this.tableStructureLabel.Location = new System.Drawing.Point(0, 23);
            this.tableStructureLabel.Name = "tableStructureLabel";
            this.tableStructureLabel.Size = new System.Drawing.Size(45, 13);
            this.tableStructureLabel.TabIndex = 1;
            this.tableStructureLabel.Text = "Champs";
            this.tableStructureLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableStructure
            // 
            this.tableStructure.CheckOnClick = true;
            this.tableStructure.FormattingEnabled = true;
            this.tableStructure.Location = new System.Drawing.Point(6, 60);
            this.tableStructure.MultiColumn = true;
            this.tableStructure.Name = "tableStructure";
            this.tableStructure.Size = new System.Drawing.Size(174, 394);
            this.tableStructure.TabIndex = 0;
            this.tableStructure.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tableStructure_KeyUp);
            // 
            // updateLine
            // 
            this.updateLine.Location = new System.Drawing.Point(856, 30);
            this.updateLine.Name = "updateLine";
            this.updateLine.Size = new System.Drawing.Size(118, 23);
            this.updateLine.TabIndex = 11;
            this.updateLine.Text = "Update Line in Table";
            this.updateLine.UseVisualStyleBackColor = true;
            this.updateLine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.updateLine_MouseClick);
            // 
            // lineContent
            // 
            this.lineContent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lineContent.Location = new System.Drawing.Point(521, 59);
            this.lineContent.Name = "lineContent";
            this.lineContent.Size = new System.Drawing.Size(459, 370);
            this.lineContent.TabIndex = 10;
            this.lineContent.Text = "";
            // 
            // backward
            // 
            this.backward.Location = new System.Drawing.Point(356, 26);
            this.backward.Name = "backward";
            this.backward.Size = new System.Drawing.Size(78, 23);
            this.backward.TabIndex = 9;
            this.backward.Text = "backward";
            this.backward.UseVisualStyleBackColor = true;
            this.backward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.backward_MouseClick);
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(440, 26);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(75, 23);
            this.forward.TabIndex = 8;
            this.forward.Text = "forward";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.forward_MouseClick);
            // 
            // refreshData
            // 
            this.refreshData.Location = new System.Drawing.Point(74, 26);
            this.refreshData.Name = "refreshData";
            this.refreshData.Size = new System.Drawing.Size(99, 23);
            this.refreshData.TabIndex = 7;
            this.refreshData.Text = "refresh Data";
            this.refreshData.UseVisualStyleBackColor = true;
            this.refreshData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refreshData_MouseClick);
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.LargeChange = 50;
            this.trackBar.Location = new System.Drawing.Point(148, 433);
            this.trackBar.Margin = new System.Windows.Forms.Padding(1);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(833, 45);
            this.trackBar.SmallChange = 5;
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 5;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // refreshprogressBar
            // 
            this.refreshprogressBar.Location = new System.Drawing.Point(6, 449);
            this.refreshprogressBar.Name = "refreshprogressBar";
            this.refreshprogressBar.Size = new System.Drawing.Size(138, 20);
            this.refreshprogressBar.TabIndex = 5;
            // 
            // fetchSize
            // 
            this.fetchSize.Location = new System.Drawing.Point(309, 27);
            this.fetchSize.Name = "fetchSize";
            this.fetchSize.Size = new System.Drawing.Size(41, 20);
            this.fetchSize.TabIndex = 4;
            this.fetchSize.Text = "10";
            // 
            // fetchEnd
            // 
            this.fetchEnd.Location = new System.Drawing.Point(253, 27);
            this.fetchEnd.Name = "fetchEnd";
            this.fetchEnd.Size = new System.Drawing.Size(50, 20);
            this.fetchEnd.TabIndex = 3;
            this.fetchEnd.Text = "100";
            this.fetchEnd.TextChanged += new System.EventHandler(this.fetchEnd_TextChanged);
            // 
            // fetchStart
            // 
            this.fetchStart.Location = new System.Drawing.Point(193, 27);
            this.fetchStart.Name = "fetchStart";
            this.fetchStart.Size = new System.Drawing.Size(55, 20);
            this.fetchStart.TabIndex = 2;
            this.fetchStart.Text = "1";
            this.fetchStart.TextChanged += new System.EventHandler(this.fetchStart_TextChanged);
            // 
            // tableDataLabel
            // 
            this.tableDataLabel.AutoSize = true;
            this.tableDataLabel.Location = new System.Drawing.Point(3, 23);
            this.tableDataLabel.Name = "tableDataLabel";
            this.tableDataLabel.Size = new System.Drawing.Size(50, 13);
            this.tableDataLabel.TabIndex = 1;
            this.tableDataLabel.Text = "Données";
            // 
            // tableData
            // 
            this.tableData.AllowUserToAddRows = false;
            this.tableData.AllowUserToDeleteRows = false;
            this.tableData.AllowUserToOrderColumns = true;
            this.tableData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.tableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableData.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableData.Location = new System.Drawing.Point(6, 59);
            this.tableData.Name = "tableData";
            this.tableData.ReadOnly = true;
            this.tableData.RowTemplate.Height = 24;
            this.tableData.Size = new System.Drawing.Size(509, 371);
            this.tableData.TabIndex = 0;
            this.tableData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tableData_KeyUp);
            this.tableData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableData_MouseClick);
            // 
            // ControlTabs
            // 
            this.ControlTabs.Controls.Add(this.tabPage1);
            this.ControlTabs.Location = new System.Drawing.Point(2, 32);
            this.ControlTabs.Name = "ControlTabs";
            this.ControlTabs.SelectedIndex = 0;
            this.ControlTabs.Size = new System.Drawing.Size(1343, 505);
            this.ControlTabs.TabIndex = 0;
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerReportsProgress = true;
            this.backgroundWorker4.WorkerSupportsCancellation = true;
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker4_ProgressChanged);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // DisplayLineContentbackgroundWorker
            // 
            this.DisplayLineContentbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DisplayLineContentbackgroundWorker_DoWork);
            this.DisplayLineContentbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DisplayLineContentbackgroundWorker_ProgressChanged);
            this.DisplayLineContentbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DisplayLineContentbackgroundWorker_RunWorkerCompleted);
            // 
            // updateLinebackgroundWorker
            // 
            this.updateLinebackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateLinebackgroundWorker_DoWork);
            this.updateLinebackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateLinebackgroundWorker_RunWorkerCompleted);
            // 
            // TablesForm
            // 
            this.ClientSize = new System.Drawing.Size(1348, 537);
            this.Controls.Add(this.ControlTabs);
            this.Name = "TablesForm";
            this.Text = "visualisation des Tables ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TablesForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TablesForm_FormClosed);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).EndInit();
            this.ControlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void refresh_MouseClick(object sender, MouseEventArgs e)
        {
            if ((this._connector != null) && this._connector.isConnected)
            {

                if (backgroundWorker4.IsBusy)
                    return;

                _connector.Lock();
                backgroundWorker4.RunWorkerAsync();
            }
        }

        private void refreshData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (GetTblDatabackgroundWorker.IsBusy)
                    return;

                _connector.Lock();
                GetTblDatabackgroundWorker.RunWorkerAsync();
            }
        }


        private void TablesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.Hide();
        }

        private void TablesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void tablesInstances_MouseClick(object sender, MouseEventArgs e)
        {
            if ((this._connector != null) && this._connector.isConnected)
            {
                int selectedIndex = this.tablesInstances.SelectedIndex;
                if (selectedIndex != -1)
                {
                    this.current_table_id = selectedIndex;
                    if (e.Button == MouseButtons.Left)
                    {

                        if (GetTblFieldsbackgroundWorker.IsBusy)
                            return;

                        _connector.Lock();
                        GetTblFieldsbackgroundWorker.RunWorkerAsync();
                    }
                }
            }
        }
        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            this.fetchStart.Text = this.trackBar.Value.ToString();
            this.fetchEnd.Text = (this.trackBar.Value + int.Parse(this.fetchSize.Text)).ToString();
            this.fetchStart.Refresh();
            this.fetchEnd.Refresh();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }



        private void tableData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.Equals(Keys.C))
            {
                StringBuilder b = DataGridToHMTLTable(tableData);

                Clipboard.SetText( b.ToString() );
                //tableData.GetClipboardContent());
              //  MessageBox.Show( b.ToString() );
            }
        }

        public static StringBuilder DataGridToHMTLTable(DataGridView dg)
        {
            StringBuilder strB = new StringBuilder();
            //create html & table
            strB.AppendLine("<html><body><center><" +
                            "table border='1' cellpadding='0' cellspacing='0'>");
            strB.AppendLine("<tr>");
            //cteate table header
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                strB.AppendLine("<th align='center' valign='middle'>" +
                                dg.Columns[i].HeaderText + "</th>");
            }
            //create table body
            strB.AppendLine("</tr>");
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                strB.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dg.Rows[i].Cells)
                {
                    strB.AppendLine("<td align='center' valign='middle'>" +
                                    dgvc.Value.ToString() + "</td>");
                }
                strB.AppendLine("</tr>");

            }
            //table footer & end of html file
            strB.AppendLine("</table></center></body></html>");
            return strB;
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {

            this._connector.Table().GetInstances("1","-1");
            instances = _connector.GetDataByName("Tables", -1);
            if ((this.instances != null) && (this.instances.Length != 0))
            {
                tablesTotalLines = new int[this.instances.Length];
                float progressf = 0;
                for (int i = 0; i < this.instances.Length; i++)
                {
                    this._connector.Table().GetLines(this.instances[i]);
                    string[] dataByName = this._connector.GetDataByName("Lines", -1);
                    if ((dataByName != null) && (dataByName.Length > 0))
                    {
                        this.tablesTotalLines[i] = int.Parse(dataByName[0]);
                    }
                    else
                    {
                        this.tablesTotalLines[i] = 0;
                    }
                    progressf = (float)i / (float)instances.Length * 100;
                    backgroundWorker4.ReportProgress((Int32)progressf);
                }
            }
        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            progressBar1.Value = 100;
            tablesInstances.Items.Clear();
            if (instances != null)
            {
                tablesInstances.Items.AddRange(instances);
            }
        }

        private void tableStructure_KeyUp(object sender, KeyEventArgs e)
        {
            if (tableStructure.CheckedIndices.Count == 0)
                return;

            if (e.Control && e.KeyCode.Equals(Keys.C))
            {
                StringBuilder b = new StringBuilder();
                b.Append("<table border=\"1\">");

                for (int i = 0; i < tableStructure.CheckedIndices.Count; i++)
                {
                    b.Append("<tr>");
                    b.Append("<td> " + (string)tableStructure.CheckedItems[i] + " </td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
                Clipboard.SetText(b.ToString());
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void tablesInstances_KeyUp(object sender, KeyEventArgs e)
        {
            if (tablesInstances.SelectedIndices.Count == 0)
                return;

            if (e.Control && e.KeyCode.Equals(Keys.C))
            {
                StringBuilder b = new StringBuilder();
                b.Append("<table border=\"1\">");

                for (int i = 0; i < tablesInstances.SelectedItems.Count; i++)
                {
                    b.Append("<tr>");
                    b.Append("<td> " + (string)tablesInstances.SelectedItems[i] + " </td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
                Clipboard.SetText(b.ToString());
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void tableData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (tableData.SelectedCells.Count == 0 || tableData.SelectedCells.Count > 1 )
                    return;

                Int32 col = tableData.SelectedCells[0].ColumnIndex;
                string field = tableData.Columns[col].HeaderText;
                string type ="";
                for (int i = 0; i < types.Length;i++)
                {
                    string tmp = this.types[i] + "    " + field;
                    type = (string) tableStructure.Items[i];
                    if (type.Equals(tmp))
                    {
                        type = types[i];
                        break;
                    }
                }
                if (type == "")
                {
                    System.Windows.Forms.MessageBox.Show(" le champ '" + field + "' n'existe pas");
                    return;
                }
                string row = ( Int32.Parse(fetchStart.Text) + tableData.SelectedCells[0].RowIndex ).ToString();
                if (DisplayLineContentbackgroundWorker.IsBusy)
                    return;
                trackBar.Enabled = false;

                _connector.Lock();
                DisplayLineContentbackgroundWorker.RunWorkerAsync(new string[] { type, field,row });
            }

        }

        private void DisplayLineContentbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] p = (string[])e.Argument;
            string content ="";
            if ( p[0].ToLower().Equals("string") || p[0].ToLower().Equals("bin") )
            {
                this._connector.Table().ReadBlock(this.instances[this.current_table_id], p[2], p[1], "1" , "4096");
             string[] dataByName = this._connector.GetDataByName("NextPosition", -1);
             string str2 = this._connector.GetDataByName("Data", -1)[0];
             int position = int.Parse(dataByName[0]);
             while (position != 0)
             {
                 this._connector.Table().ReadBlock(this.instances[this.current_table_id], p[2], p[1], position.ToString(), "4096");
              dataByName = this._connector.GetDataByName("NextPosition", -1);
              string[] strArray3 = this._connector.GetDataByName("Data", -1);
              str2 = str2 + strArray3[0];
              position = int.Parse(dataByName[0]);
             }
             if (str2 != null)
             {
                content = str2;
             }
             else
             {
                content = " ";
             }
             }
             if (p[0].ToLower().Equals("char") || p[0].ToLower().Contains("int"))
             {
              _connector.Table().ReadLine(this.instances[this.current_table_id], p[2], new string[] { p[1] });
              string[] strArray4 = this._connector.GetDataByName( p[1].ToLower(), -1);
              if (strArray4 != null)
              {
                  content = strArray4[0];
              }
              else
              {
                content = " ";
              }
             }
             else if ( p[1].ToLower().Equals("knw_abstract"))
             {
                 _connector.Session().DocToContext(p[2], "false");
             }

             e.Result = content;
        }

        private void DisplayLineContentbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(28605);
            lineContent.Text = encoding.GetString( encoding.GetBytes( (string) e.Result ) );
            trackBar.Enabled = true;
        }

        private void DisplayLineContentbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (!GetTblDatabackgroundWorker.IsBusy)
            {
                trackBar.Enabled = false;

                _connector.Lock();
                GetTblDatabackgroundWorker.RunWorkerAsync();
            }
        }

        private void updateLinebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] p = ( string[] ) e.Argument;

            // on fait l'update
            _connector.Table().Update(p[0],p[1], new string[] {p[2]}, new string[]{p[3]});
        }

        private void updateLinebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
        } 

        private void updateLine_MouseClick(object sender, MouseEventArgs e)
        {
            if (updateLinebackgroundWorker.IsBusy)
                return;
            if (tablesInstances.SelectedIndices.Count == 0)
                return;
            if (tableData.Rows == null || tableData.Rows.Count == 0 || tableData.SelectedCells.Count != 1)
                return;
            if ( ! (tableData.Columns[0].HeaderText.Equals("RowId")  ) )
                return;

            Int32 col = tableData.SelectedCells[0].ColumnIndex;
            string field = tableData.Columns[col].HeaderText;


            // table  rowid champ valeur
            int buffersize = 0;
            _connector.Lock();
            updateLinebackgroundWorker.RunWorkerAsync(new string[] { (string) tablesInstances.SelectedItem, (string) tableData.SelectedCells[0].Value, field, KMString.NormalizeString(lineContent.Text, ref buffersize)} );
        }


    

    }
}

