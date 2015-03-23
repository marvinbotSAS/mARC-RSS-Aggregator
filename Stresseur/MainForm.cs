namespace RSSRTReader
{
    using mARC;
using RSSRTReader.Misc;
using RSSRTReader.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Xsl;
using HtmlAgilityPack;
using System.Security.Cryptography;
using Microsoft.Build.Framework;
using System.IO.Packaging;
using System.Net.Mail;

    public class MainForm : Form
    {

        private SmtpClient smtpClient = new SmtpClient(); // pour l'envoi d'email
        private Connector _connector;

        /// <summary>
        /// <see cref="System.Collections.Generic.List{T}"/> that contains feeds
        /// </summary>
        public List<RSSObject> RSSFeed { get; set; }

        private UInt64 TotalArticles;

        private UInt64 updateIndexFrequency;
        private Button button2;
        private IContainer components;
        private GroupBox connection;
        private Button connectionAction;
        private CheckBox defaulttables;
        private string current_table;

         private static HtmlWeb htmlWeb = new HtmlWeb();

        private string IP;
        private Label IPlabel;
        private TextBox IpText;
        private string knw_name;

        private PictureBox LedPicture;
        private MaskedTextBox nom;
        private MenuStrip menuStrip1;
        private Label nomLabel;
        private int Port;
        private Label portLabel;
        private TextBox PortText;
        private Random random;

        private GroupBox Tables;

        private TextBox build;
        private Label buildlabel;

        private GroupBox propertiesBox;
        private DataGridView PropertiesdataGridView;
        private DataGridViewTextBoxColumn Propriété;
        private DataGridViewTextBoxColumn Valeur;
        protected GroupBox log;
        private RichTextBox logTextBox;
        private Button clearLog;
        private CheckBox displayLog;
        private CheckBox autoRefresh;
        private System.Windows.Forms.Timer autoRefreshTimer;
        private BackgroundWorker autoRefreshPropertiesbackgroundWorker;
        private GroupBox RefreshPropertiesBox;
        private TrackBar trackBar1;
        private Label label1;
        private TextBox interval;
        private TabControl mARCtab;
        private TabPage mARCServerPage;
        private TabControl mARCServertab;
        private TabPage ConnexionPage;
        private TabPage CustomDocPage;
        private TabPage TablesPage;
        private TabPage RSSFeedsPage;
        private TabPage RSSFeedsTrackingPage;
        private ToolStripMenuItem fToolStripMenuItem;
        private GroupBox ContentgroupBox;
        private RichTextBox ContentrichTextBox;
        private GroupBox TitlegroupBox;
        private TextBox titletextBox;
        private TabPage LogServerPage;
        private TabControl tabControl2;
        private TabPage createPage;
        private TabPage VisualizePage;
        private TabPage selectTablePage;
        private GroupBox FieldsgroupBox;
        private GroupBox InstancesgroupBox;
        private GroupBox DatagroupBox;
        private ListBox tablesInstances;
        private CheckedListBox tableStructure;
        private GroupBox TblContentgroupBox;
        private DataGridView tableData;
        private RichTextBox lineContent;
        private BackgroundWorker updateLinebackgroundWorker;
        private BackgroundWorker DisplayLineContentbackgroundWorker;
        private BackgroundWorker GetTblInstancesbackgroundWorker;
        private BackgroundWorker GetTblFieldsbackgroundWorker;


        // pour les tables
        private UInt64[] tablesTotalLines;
        private string[] instances;
        private string[] names;
        private List<string[]> values = new List<string[]>();
        string[] headers;
        private GroupBox FeedsgroupBox;
        private GroupBox visualizeFeedgroupBox;
        private Button modifyFeedbutton;
        private Button RemoveFeedbutton;
        private Button addFeedbutton;
        private Button updateLine;
        private BackgroundWorker GetTblDatabackgroundWorker;
        private Button backward;
        private Button forward;
        private Button refreshData;
        private TextBox fetchSize;
        private TextBox fetchStart;
        private string[] types;
        private GroupBox URLgroupBox;
        private TextBox URLtextBox;
        private TrackBar trackBar;
        private ToolStripMenuItem chargerToolStripMenuItem;
        public DataGridView FeedsTable;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn URL;
        private WebBrowser webBrowser1;
        private GroupBox FeedsSelectiongroupBox1;
        public CheckedListBox FeedscheckedListBox;
        private GroupBox RefreshRategroupBox;
        private TrackBar RRtrackBar;
        private TextBox RRtextBox;
        private CheckBox RRActifcheckBox;
        private BackgroundWorker UpdateFeedsbackgroundWorker;
        private System.Windows.Forms.Timer updateFeedstimer;
        private BackgroundWorker ParseHTMLbackgroundWorker;
        private GroupBox logServergroupBox;
        private RichTextBox logServerrichTextBox;
        private int current_table_id;
        private GroupBox knwSavegroupBox;
        private Label label4;
        private Label label3;
        private TextBox saveKnwtextBox;
        private Label label2;
        private List<string> newArticles;
        private GroupBox KnwgroupBox;
        private Button InsertAndLearnbutton;
        private Button publishKnwbutton;
        private Button saveKnwButton;
        private BackgroundWorker InsertAndLearnbackgroundWorker;
        private BackgroundWorker KnwSavebackgroundWorker;
        private BackgroundWorker IndexPublishbackgroundWorker;
        private BackgroundWorker CheckGUIDbackgroundWorker;
        private TabPage ContextuelTrackingtabPage;
        private GroupBox trackingDocsgroupBox;
        private ulong saveKnwFrequency;
        public CheckedListBox trackingDocscheckedListBox;
        private GroupBox similardDocsgroupBox;
        private BackgroundWorker getSimilarDocsbackgroundWorker;
        private GroupBox TuninggroupBox;
        private Label taillelabel;
        private TextBox seuilsimilarDocstextBox;
        private Label Seuillabel;
        private Label depthlabel;
        private TextBox maxgentextBox;
        private Label maxgenlabel;
        private TextBox maxSignatureSimilarDocstextBox;
        private Label tailleMaxlabel;
        private TextBox tailleSimilarDocstextBox;
        private TextBox depthSimilarDocstextBox;
        private GroupBox LearngroupBox;
        private TrackingDocument current_custom_doc;
        private string seuilsimilarDoc;
        private string tailleSimilarDocs;
        private string maxSignatureSimilarDocs;
        private string maxgenSimilarDocs;
        private GroupBox timerSimilarDocsgroupBox;
        private CheckBox timerSimilarDocscheckBox;
        private TextBox timerSimilarDocstextBox;
        private System.Windows.Forms.Timer timersimilarDocs;
        private TreeView similarDocstreeView;
        private GroupBox rebuildgroupBox;
        private string depthSimilarDocs;
        private GroupBox rebuildrangegroupBox;
        private TextBox rebuildTotextBox;
        private Label rebuildTolabel;
        private TextBox rebuildFromtextBox;
        private Label rebuildFromlabel;
        private Button rebuildbutton;
        private BackgroundWorker rebuildKnwbackgroundWorker;
        private GroupBox rebuildParamgroupBox;
        private ComboBox rebuildParamcomboBox;
        private Label rebuildOptionslabel;
        private GroupBox rebuildFieldsgroupBox;
        private TextBox fieldsToRebuildtextBox;
        private CheckBox saveKnwAfterRebuildcheckBox;
        private BindingSource mainFormBindingSource;
        private string currentTable_TotalLines;
        private GroupBox groupBox2;
        private TextBox similarDocsFormattextBox;
        private DataGridView similarDocsdataGridView;
        private List< List<string[]>  > similarDocs;
        private ComboBox RefreshRateUnitcomboBox;
        private ulong deltaArticles;
        private GroupBox loggergroupBox;
        private ComboBox verbositycomboBox;
        private Label verbositylabel;
        private Label FetchSizelabel;
        private BackgroundWorker GetTblTotalLinesbackgroundWorker;
        private BackgroundWorker CheckRSSReadbackgroundWorker;
        private TabPage SMTPConfigurationTabPage;
        private GroupBox UserDataGourpBox;
        private TextBox textBox2;
        private Label emailAddresslabel;
        private GroupBox SMTPServergroupBox;
        private TextBox SMTPPorttextBox;
        private Label SMTPPortlabel;
        private TextBox SMTPHosttextBox;
        private Label SMTPName;
        private Button AddDocFromDBbutton;
        private bool enable_updatefeeds = false;
        private GroupBox AutoRebuildgroupBox;
        private Label label5;
        private TextBox AutoRebuildtextBox;
        private Label label6;
        private ComboBox timersimilarDocscomboBox;
        private TextBox fetchSizeSimilarDocsTextBox;
        private Label fetchSsimilarDocslabel;
        private GroupBox groupBox1;
        private Label articlelabel;
        private TextBox MaJIndextextBox;
        private Label majIndexlabel;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private ulong AutoRebuildFrequency;

        public MainForm()
        {
            this.InitializeComponent();


            smtpClient.Host = SMTPHosttextBox.Text;
            smtpClient.Port = Int32.Parse(SMTPPorttextBox.Text);

            updateIndexFrequency = UInt64.Parse(MaJIndextextBox.Text);
            trackBar1.Value = 1000;
            newArticles = new List<string>();
            this.random = new Random();
            this._connector = new Connector(false);
            this.knw_name = "none";
            this.Port = -1;
            this.IP = null;

            similarDocs = new List< List<string[]> >();
            FeedsTable.RowCount = 0;
            similarDocsdataGridView.RowCount = 0;

            FeedsTable.Refresh();


                for (int i = 0; i < Program.Config.Count; i++)
                {
                    FeedsTable.Rows.Add(new string[] { Program.Config[i].Title, Program.Config[i].URL });
                    FeedscheckedListBox.Items.Add(Program.Config[i].Title);

                }
            // lorsqu'un nouvel article est détecté et doit être envoyéau serveur mARC
            //
                Reader.NewItem += new Reader.NewItemHandler(RSSReader_NewItem);

            current_table = null;

            similarDocstreeView.Nodes.Add(new TreeNode("Documents"));

            saveKnwFrequency = UInt64.Parse(saveKnwtextBox.Text);
            updateIndexFrequency = UInt64.Parse(MaJIndextextBox.Text);
            AutoRebuildFrequency = UInt64.Parse(AutoRebuildtextBox.Text);
        }

        /**
         * fonction exécutée lors de la détection de nouvels articles
         * cette fonction va insérer dans la DB du serveur mARC les nouveaux articles
         * et les apprendre
         **/

        public void RSSReader_NewItem(List<string> articles)
        {

            newArticles.AddRange( articles );

        }

        private void ParseHTMLbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> articles = (List<string>)e.Argument;
            HtmlAgilityPack.HtmlDocument wdoc = new HtmlAgilityPack.HtmlDocument() ;
            Int32 indice;
            string sgid;
            string uri;
            string title, article="";
            Encoding iso = Encoding.GetEncoding("ISO-8859-15"); 
            Encoding utf8 = iso;
            string[] result;

            string[] v = new string[10];
            v[0] = "guid";
            v[2] = "title";
            v[4] = "content";
            v[6] = "link";
            v[8] = "pubDate";

            string description = "";
            HtmlNode articleNode, encodingNode;
            _connector._DirectExecute = false;

/*
            var htmlWeb = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.GetEncoding(28591),
            };*/

            System.Net.WebClient client = new System.Net.WebClient();

            string date;
            DateTime dt;

            e.Result = 0;

            for (int i = 0; i < articles.Count; i++)
            {
                uri = articles[i];
                indice = uri.IndexOf("<pubDate>");
                if (indice == -1)
                    break;
                sgid = uri.Substring(0, indice);
                // le guid existe t il dans le B-TREE ?
                _connector.OpenScript(null);
                _connector.Table().Select(current_table, "new", "guid", "=", sgid, " ");
                _connector.Session().Clear("results");
                _connector.ExecuteScript();
                result = _connector.GetDataByName("ResultCount", 0);
                if (result == null || Int32.Parse(result[0]) != 0) // le guid existe déjà
                    continue;

                uri = uri.Substring(indice+9);
                indice = uri.IndexOf("<pubDate/>");
                date = uri.Substring(0, indice);
                dt = Convert.ToDateTime(date);

                uri = uri.Substring(indice + 10);
                indice = uri.IndexOf("<description/>");
                description = uri.Substring(13, indice - 13);
                // on extrait le texte seul de la balise description
                //
                wdoc.LoadHtml(description);
                description = wdoc.DocumentNode.InnerText;
                description = HtmlEntity.DeEntitize(description);
                //
                uri = uri.Substring(indice + 14);

           //     wdoc = htmlWeb.Load( uri );
           //     srcEncoding = wdoc.Encoding;

                wdoc.LoadHtml(client.DownloadString(uri));
                utf8 = wdoc.Encoding;
                //Stream stream = client.OpenRead(uri);

  /*              if (srcEncoding == Encoding.UTF8)
                {
                    wdoc.Load(stream , Encoding.UTF8);
                }
   * */

                encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@content='text/html; charset=UTF-8']");
                if (encodingNode != null)
                {
                    utf8 = Encoding.UTF8;
                    client.Encoding = utf8;
                    wdoc.Load(client.OpenRead(uri), Encoding.UTF8);
                }
                else
                 //   if ((encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@charset='iso-8859-1']")) != null)
                {
                    utf8 = Encoding.GetEncoding("ISO-8859-15");
                    client.Encoding = utf8;
                    wdoc.Load(client.OpenRead(uri), iso );
                }

                // on enlève tous les commentaires de la page HTML
                foreach (HtmlNode comment in wdoc.DocumentNode.SelectNodes("//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }

                // pages web encodées en général en srcEncoding
                // on change l'encoding vers ISO8859-15
                title = wdoc.DocumentNode.SelectSingleNode("//title").InnerText;
                title = HtmlEntity.DeEntitize(title);
                
                title = iso.GetString( Encoding.Convert(utf8, iso, utf8.GetBytes(title)) );
                articleNode = wdoc.GetElementbyId("articleBody");

                if ( articleNode != null ) // leMonde RSS ?
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                }
                else if ( ( articleNode = wdoc.GetElementbyId("mediaarticlebody") ) != null )//  yahoo RSS ?
                {
                    // on récupère le bon titre !!!
                    title = wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value;
                    title = HtmlEntity.DeEntitize(title);
                    title = iso.GetString(Encoding.Convert(utf8, iso, utf8.GetBytes(title)));
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@class='entry-content']")) != null) // blog leMonde
                {
                    title = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//h1[@class='entry-title']").InnerText);
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@itemprop='description']")) != null)
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                }

                article = description + " " + article;// on concatène la description et l'article
                title = iso.GetString(Encoding.Convert(utf8, iso, utf8.GetBytes(title)));
                article = iso.GetString(Encoding.Convert(utf8, iso, utf8.GetBytes(article)));
                //description = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(description)));
                // on insère l'article dans le serveur mARC et on l'apprend avec indexation
                _connector.OpenScript(null);

                v[1] = sgid;

                v[3] = title;

                v[5] = article;

                v[7] = uri;
        
                v[9] = dt.Year + ":"+dt.Month+":"+dt.Day+":"+dt.Hour+":"+dt.Minute+":"+dt.Second;

                _connector.Table().Insert(current_table, v);
                _connector.ExecuteScript();
                result = _connector.GetDataByName("RowId", -1);
                if (result == null)
                    continue;
                if (TotalArticles != 0 )
                {
                    if ( TotalArticles % updateIndexFrequency == 0 )
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcPublish();
                        _connector.ExecuteScript();
                    }
                    if ( TotalArticles % saveKnwFrequency == 0 )
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcSave();
                        _connector.ExecuteScript();
                    }
                }
                TotalArticles++;
                if (TotalArticles != 0 )
                {
                    if (TotalArticles % updateIndexFrequency == 0)
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcPublish();
                        _connector.ExecuteScript();
                    }
                    if (TotalArticles % saveKnwFrequency == 0)
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcSave();
                        _connector.ExecuteScript();
                    }

                }
                _connector.OpenScript(null);
                if (articleNode != null)
                {
                    _connector.Session().Store( article, "ranked",result[0]);
                }
                _connector.Session().Store(title,"ranked", result[0]);
                _connector.ExecuteScript();

            

            }
            _connector._DirectExecute = true;

        }


        private void ParseHTMLbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
            // on vide la liste des new articles
            newArticles.RemoveRange(0, newArticles.Count );

            // on vérifie l'intégrité de la liste des articles lus
            // car des erreurs d'accès web lors du download d'un fichier sur un flux
            // peuvent désynchroniser les attributs isRead
            CheckRSSIntegrity();

            deltaArticles = TotalArticles - deltaArticles;
            // si on a de nouveaux articles, on cherche les similarDocs
            if ( deltaArticles != 0)
            {
                if (getSimilarDocsbackgroundWorker.IsBusy)
                    return;
                // on récupère la liste des docs user sélectionnée
                List<string> dbids = new List<string>();

                for (int i = 0; i < trackingDocscheckedListBox.SelectedItems.Count; i++)
                {
                    dbids.Add(((TrackingDocument)trackingDocscheckedListBox.SelectedItems[i]).dbID);
                }

                if (dbids.Count == 0)
                    return;

                similarDocsFormattextBox.Enabled = false;

                dbids.Add(seuilsimilarDoc);
                dbids.Add(tailleSimilarDocs );
                dbids.Add(maxSignatureSimilarDocs);
                dbids.Add(maxgenSimilarDocs);
                dbids.Add(depthSimilarDocs);
                dbids.Add(similarDocsFormattextBox.Text);
                if (dbids.Count == 0)
                    return;
                // on y va
                _connector.Lock();
                timersimilarDocs.Enabled = false;
                getSimilarDocsbackgroundWorker.RunWorkerAsync(dbids);
            }

            if (enable_updatefeeds)
                updateFeedstimer.Enabled = true;

        }




        private void similarDocstreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = similarDocstreeView.SelectedNode;

            if (selected.Level == 1)
            {
                // on est sur un noeud intermédiaire
                // on affiche ds le datagridview le détail de tous les articles
                similarDocstreeView.Enabled = false;
                similarDocsdataGridView.Rows.Clear();
                List<string[]> s = similarDocs[selected.Index];
                for (int i = 0; i < s.Count; i++)
                {
                    similarDocsdataGridView.Rows.Add(s[i]);
                }
                similarDocstreeView.Enabled = true;
            }
            if (selected.Level == 2)
            {
                // on est sur un noeud en fin de tree
                // on affiche ds le datagridview le détail de l'article correspondant
                similarDocstreeView.Enabled = false;
                similarDocsdataGridView.Rows.Clear();
                TreeNode parent = selected.Parent;
                string[] s = similarDocs[parent.Index][selected.Index];
                similarDocsdataGridView.Rows.Add(s);
                similarDocstreeView.Enabled = true;
            }

        }


        private void connectionAction_MouseUp(object sender, MouseEventArgs e)
        {
            connectionAction.Enabled = false;
            if (this._connector.isConnected)
            {
                this._connector.disConnect();
                this.LedPicture.Image = Properties.Resources.redled;
            }
            else
            {
                this.connectionAction.Text = "En cours...";
                this.connectionAction.Refresh();
                if (this.IP == null)
                {
                    this.IP = this.IpText.Text;
                }
                if (this.Port == -1)
                {
                    this.Port = int.Parse(this.PortText.Text);
                }
                this._connector.IP = this.IP;
                this._connector.Port = this.Port;
                _connector.Lock();
                this._connector.Connect();
                if (displayLog.Checked)
                {
                    logTextBox.AppendText(_connector.challengeMessage + "\n");
                }
                if (this._connector.isConnected)
                {

                    this.LedPicture.Image = Properties.Resources.greenled;
                    build.Text = _connector.ServerBuild;
                    nom.Text = _connector.ServerName;


                    PropertiesdataGridView.Rows.Clear();
                    string[] row = new string[2];
                    for (int i = 0; i < _connector._properties.Length; i++)
                    {
                        row[0] = _connector._properties[i];
                        row[1] = _connector._propertieValue[i];
                        PropertiesdataGridView.Rows.Add(row);
                    }
 
                    _connector.UnLock();



                    // récupération des instances de tables
                    if (!GetTblInstancesbackgroundWorker.IsBusy)
                    {
                        tablesInstances.Items.Clear();
                        _connector.Lock();
                        GetTblInstancesbackgroundWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    this.LedPicture.Image = Resources.redled;
                    _connector.UnLock();
                }
                this.connectionAction.Text = "connection";

            }

            connectionAction.Enabled = true;

        }


        private void CreateTables_Click(object sender, EventArgs e)
        {
            _connector._DirectExecute = true;

            if (this._connector != null)
            {
                _connector.Lock();

                if (this.defaulttables.Checked)
                {
                    _connector._DirectExecute = false;
                    _connector.OpenScript(null);
                    this._connector.Table().Create("rssdata","NULL", "NULL" ,"NULL","MASTER", "link STRING, title CHAR 254, content STRING, guid UINT64, pubDate SIMPLEDATE");
                    // un B-TREE sur le champ guid pour savoir si l'article est déjà stocké en base
                    this._connector.Table().BIndexCreate("rssdata", "guid", "true");
                    _connector.ExecuteScript();
                    _connector._DirectExecute = true;

                }
                else
                {
                    NewTblForm f = new NewTblForm();
                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (f.getFields() != null && f.GetNom() != null)
                        {
                            _connector._DirectExecute = false;
                            _connector.OpenScript(null);
                            this._connector.Table().Create(f.GetNom(),"null",  "null", "10000", "MASTER", f.getFields());
                            // un B-TREE sur le champ guid pour savoir si l'article est déjà stocké en base
                            this._connector.Table().BIndexCreate( f.GetNom(), "guid", "true");
                            _connector.ExecuteScript();
                            _connector._DirectExecute = true;
                        }
                        else
                        {
                            logTextBox.AppendText("aucuns champs et/ou aucun nom de table n'a été défini(s). \n");
                        }
                    }
                    f.Dispose();

                }
                _connector.UnLock();
                // on récupère la table créée
                if (!GetTblInstancesbackgroundWorker.IsBusy)
                {
                    _connector.Lock();
                    GetTblInstancesbackgroundWorker.RunWorkerAsync();
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



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connection = new System.Windows.Forms.GroupBox();
            this.propertiesBox = new System.Windows.Forms.GroupBox();
            this.RefreshPropertiesBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.interval = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.autoRefresh = new System.Windows.Forms.CheckBox();
            this.PropertiesdataGridView = new System.Windows.Forms.DataGridView();
            this.Propriété = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.build = new System.Windows.Forms.TextBox();
            this.buildlabel = new System.Windows.Forms.Label();
            this.LedPicture = new System.Windows.Forms.PictureBox();
            this.connectionAction = new System.Windows.Forms.Button();
            this.nomLabel = new System.Windows.Forms.Label();
            this.nom = new System.Windows.Forms.MaskedTextBox();
            this.PortText = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.IPlabel = new System.Windows.Forms.Label();
            this.IpText = new System.Windows.Forms.TextBox();
            this.Tables = new System.Windows.Forms.GroupBox();
            this.knwSavegroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saveKnwtextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.defaulttables = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log = new System.Windows.Forms.GroupBox();
            this.clearLog = new System.Windows.Forms.Button();
            this.displayLog = new System.Windows.Forms.CheckBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.autoRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.autoRefreshPropertiesbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mARCtab = new System.Windows.Forms.TabControl();
            this.mARCServerPage = new System.Windows.Forms.TabPage();
            this.mARCServertab = new System.Windows.Forms.TabControl();
            this.ConnexionPage = new System.Windows.Forms.TabPage();
            this.CustomDocPage = new System.Windows.Forms.TabPage();
            this.LearngroupBox = new System.Windows.Forms.GroupBox();
            this.InsertAndLearnbutton = new System.Windows.Forms.Button();
            this.URLgroupBox = new System.Windows.Forms.GroupBox();
            this.URLtextBox = new System.Windows.Forms.TextBox();
            this.ContentgroupBox = new System.Windows.Forms.GroupBox();
            this.ContentrichTextBox = new System.Windows.Forms.RichTextBox();
            this.TitlegroupBox = new System.Windows.Forms.GroupBox();
            this.titletextBox = new System.Windows.Forms.TextBox();
            this.TablesPage = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.createPage = new System.Windows.Forms.TabPage();
            this.AutoRebuildgroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AutoRebuildtextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rebuildgroupBox = new System.Windows.Forms.GroupBox();
            this.saveKnwAfterRebuildcheckBox = new System.Windows.Forms.CheckBox();
            this.rebuildParamgroupBox = new System.Windows.Forms.GroupBox();
            this.rebuildOptionslabel = new System.Windows.Forms.Label();
            this.rebuildFieldsgroupBox = new System.Windows.Forms.GroupBox();
            this.fieldsToRebuildtextBox = new System.Windows.Forms.TextBox();
            this.rebuildParamcomboBox = new System.Windows.Forms.ComboBox();
            this.rebuildbutton = new System.Windows.Forms.Button();
            this.rebuildrangegroupBox = new System.Windows.Forms.GroupBox();
            this.rebuildTotextBox = new System.Windows.Forms.TextBox();
            this.rebuildTolabel = new System.Windows.Forms.Label();
            this.rebuildFromtextBox = new System.Windows.Forms.TextBox();
            this.rebuildFromlabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.articlelabel = new System.Windows.Forms.Label();
            this.MaJIndextextBox = new System.Windows.Forms.TextBox();
            this.majIndexlabel = new System.Windows.Forms.Label();
            this.selectTablePage = new System.Windows.Forms.TabPage();
            this.FieldsgroupBox = new System.Windows.Forms.GroupBox();
            this.tableStructure = new System.Windows.Forms.CheckedListBox();
            this.InstancesgroupBox = new System.Windows.Forms.GroupBox();
            this.tablesInstances = new System.Windows.Forms.ListBox();
            this.VisualizePage = new System.Windows.Forms.TabPage();
            this.TblContentgroupBox = new System.Windows.Forms.GroupBox();
            this.updateLine = new System.Windows.Forms.Button();
            this.lineContent = new System.Windows.Forms.RichTextBox();
            this.DatagroupBox = new System.Windows.Forms.GroupBox();
            this.AddDocFromDBbutton = new System.Windows.Forms.Button();
            this.FetchSizelabel = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.backward = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            this.refreshData = new System.Windows.Forms.Button();
            this.fetchSize = new System.Windows.Forms.TextBox();
            this.fetchStart = new System.Windows.Forms.TextBox();
            this.tableData = new System.Windows.Forms.DataGridView();
            this.LogServerPage = new System.Windows.Forms.TabPage();
            this.RSSFeedsPage = new System.Windows.Forms.TabPage();
            this.visualizeFeedgroupBox = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.FeedsgroupBox = new System.Windows.Forms.GroupBox();
            this.FeedsTable = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifyFeedbutton = new System.Windows.Forms.Button();
            this.RemoveFeedbutton = new System.Windows.Forms.Button();
            this.addFeedbutton = new System.Windows.Forms.Button();
            this.RSSFeedsTrackingPage = new System.Windows.Forms.TabPage();
            this.logServergroupBox = new System.Windows.Forms.GroupBox();
            this.logServerrichTextBox = new System.Windows.Forms.RichTextBox();
            this.RefreshRategroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshRateUnitcomboBox = new System.Windows.Forms.ComboBox();
            this.RRtrackBar = new System.Windows.Forms.TrackBar();
            this.RRtextBox = new System.Windows.Forms.TextBox();
            this.RRActifcheckBox = new System.Windows.Forms.CheckBox();
            this.FeedsSelectiongroupBox1 = new System.Windows.Forms.GroupBox();
            this.FeedscheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ContextuelTrackingtabPage = new System.Windows.Forms.TabPage();
            this.TuninggroupBox = new System.Windows.Forms.GroupBox();
            this.fetchSizeSimilarDocsTextBox = new System.Windows.Forms.TextBox();
            this.fetchSsimilarDocslabel = new System.Windows.Forms.Label();
            this.depthSimilarDocstextBox = new System.Windows.Forms.TextBox();
            this.depthlabel = new System.Windows.Forms.Label();
            this.maxgentextBox = new System.Windows.Forms.TextBox();
            this.maxgenlabel = new System.Windows.Forms.Label();
            this.maxSignatureSimilarDocstextBox = new System.Windows.Forms.TextBox();
            this.tailleMaxlabel = new System.Windows.Forms.Label();
            this.tailleSimilarDocstextBox = new System.Windows.Forms.TextBox();
            this.taillelabel = new System.Windows.Forms.Label();
            this.seuilsimilarDocstextBox = new System.Windows.Forms.TextBox();
            this.Seuillabel = new System.Windows.Forms.Label();
            this.similardDocsgroupBox = new System.Windows.Forms.GroupBox();
            this.similarDocsdataGridView = new System.Windows.Forms.DataGridView();
            this.similarDocstreeView = new System.Windows.Forms.TreeView();
            this.trackingDocsgroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.similarDocsFormattextBox = new System.Windows.Forms.TextBox();
            this.timerSimilarDocsgroupBox = new System.Windows.Forms.GroupBox();
            this.timersimilarDocscomboBox = new System.Windows.Forms.ComboBox();
            this.timerSimilarDocstextBox = new System.Windows.Forms.TextBox();
            this.timerSimilarDocscheckBox = new System.Windows.Forms.CheckBox();
            this.trackingDocscheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SMTPConfigurationTabPage = new System.Windows.Forms.TabPage();
            this.UserDataGourpBox = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.emailAddresslabel = new System.Windows.Forms.Label();
            this.SMTPServergroupBox = new System.Windows.Forms.GroupBox();
            this.SMTPPorttextBox = new System.Windows.Forms.TextBox();
            this.SMTPPortlabel = new System.Windows.Forms.Label();
            this.SMTPHosttextBox = new System.Windows.Forms.TextBox();
            this.SMTPName = new System.Windows.Forms.Label();
            this.updateLinebackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.DisplayLineContentbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.GetTblInstancesbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.GetTblFieldsbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.GetTblDatabackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.UpdateFeedsbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.updateFeedstimer = new System.Windows.Forms.Timer(this.components);
            this.ParseHTMLbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.KnwgroupBox = new System.Windows.Forms.GroupBox();
            this.publishKnwbutton = new System.Windows.Forms.Button();
            this.saveKnwButton = new System.Windows.Forms.Button();
            this.InsertAndLearnbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.KnwSavebackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.IndexPublishbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.CheckGUIDbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.getSimilarDocsbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timersimilarDocs = new System.Windows.Forms.Timer(this.components);
            this.rebuildKnwbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.loggergroupBox = new System.Windows.Forms.GroupBox();
            this.verbositycomboBox = new System.Windows.Forms.ComboBox();
            this.verbositylabel = new System.Windows.Forms.Label();
            this.GetTblTotalLinesbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.CheckRSSReadbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.connection.SuspendLayout();
            this.propertiesBox.SuspendLayout();
            this.RefreshPropertiesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesdataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedPicture)).BeginInit();
            this.Tables.SuspendLayout();
            this.knwSavegroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.log.SuspendLayout();
            this.mARCtab.SuspendLayout();
            this.mARCServerPage.SuspendLayout();
            this.mARCServertab.SuspendLayout();
            this.ConnexionPage.SuspendLayout();
            this.CustomDocPage.SuspendLayout();
            this.LearngroupBox.SuspendLayout();
            this.URLgroupBox.SuspendLayout();
            this.ContentgroupBox.SuspendLayout();
            this.TitlegroupBox.SuspendLayout();
            this.TablesPage.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.createPage.SuspendLayout();
            this.AutoRebuildgroupBox.SuspendLayout();
            this.rebuildgroupBox.SuspendLayout();
            this.rebuildParamgroupBox.SuspendLayout();
            this.rebuildFieldsgroupBox.SuspendLayout();
            this.rebuildrangegroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.selectTablePage.SuspendLayout();
            this.FieldsgroupBox.SuspendLayout();
            this.InstancesgroupBox.SuspendLayout();
            this.VisualizePage.SuspendLayout();
            this.TblContentgroupBox.SuspendLayout();
            this.DatagroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).BeginInit();
            this.LogServerPage.SuspendLayout();
            this.RSSFeedsPage.SuspendLayout();
            this.visualizeFeedgroupBox.SuspendLayout();
            this.FeedsgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeedsTable)).BeginInit();
            this.RSSFeedsTrackingPage.SuspendLayout();
            this.logServergroupBox.SuspendLayout();
            this.RefreshRategroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RRtrackBar)).BeginInit();
            this.FeedsSelectiongroupBox1.SuspendLayout();
            this.ContextuelTrackingtabPage.SuspendLayout();
            this.TuninggroupBox.SuspendLayout();
            this.similardDocsgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.similarDocsdataGridView)).BeginInit();
            this.trackingDocsgroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.timerSimilarDocsgroupBox.SuspendLayout();
            this.SMTPConfigurationTabPage.SuspendLayout();
            this.UserDataGourpBox.SuspendLayout();
            this.SMTPServergroupBox.SuspendLayout();
            this.KnwgroupBox.SuspendLayout();
            this.loggergroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // connection
            // 
            this.connection.BackColor = System.Drawing.SystemColors.Control;
            this.connection.Controls.Add(this.propertiesBox);
            this.connection.Controls.Add(this.build);
            this.connection.Controls.Add(this.buildlabel);
            this.connection.Controls.Add(this.LedPicture);
            this.connection.Controls.Add(this.connectionAction);
            this.connection.Controls.Add(this.nomLabel);
            this.connection.Controls.Add(this.nom);
            this.connection.Controls.Add(this.PortText);
            this.connection.Controls.Add(this.portLabel);
            this.connection.Controls.Add(this.IPlabel);
            this.connection.Controls.Add(this.IpText);
            this.connection.Location = new System.Drawing.Point(3, 3);
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(865, 157);
            this.connection.TabIndex = 0;
            this.connection.TabStop = false;
            this.connection.Text = "mARC";
            // 
            // propertiesBox
            // 
            this.propertiesBox.Controls.Add(this.RefreshPropertiesBox);
            this.propertiesBox.Controls.Add(this.PropertiesdataGridView);
            this.propertiesBox.Location = new System.Drawing.Point(280, 10);
            this.propertiesBox.Name = "propertiesBox";
            this.propertiesBox.Size = new System.Drawing.Size(583, 141);
            this.propertiesBox.TabIndex = 11;
            this.propertiesBox.TabStop = false;
            this.propertiesBox.Text = "Propriétés";
            // 
            // RefreshPropertiesBox
            // 
            this.RefreshPropertiesBox.Controls.Add(this.label1);
            this.RefreshPropertiesBox.Controls.Add(this.interval);
            this.RefreshPropertiesBox.Controls.Add(this.trackBar1);
            this.RefreshPropertiesBox.Controls.Add(this.autoRefresh);
            this.RefreshPropertiesBox.Location = new System.Drawing.Point(405, 19);
            this.RefreshPropertiesBox.Name = "RefreshPropertiesBox";
            this.RefreshPropertiesBox.Size = new System.Drawing.Size(158, 116);
            this.RefreshPropertiesBox.TabIndex = 12;
            this.RefreshPropertiesBox.TabStop = false;
            this.RefreshPropertiesBox.Text = "Refresh";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "ms";
            // 
            // interval
            // 
            this.interval.Location = new System.Drawing.Point(24, 88);
            this.interval.Name = "interval";
            this.interval.ReadOnly = true;
            this.interval.Size = new System.Drawing.Size(72, 20);
            this.interval.TabIndex = 13;
            this.interval.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(24, 37);
            this.trackBar1.Maximum = 10000;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(113, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.TickFrequency = 1000;
            this.trackBar1.Value = 10;
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // autoRefresh
            // 
            this.autoRefresh.AutoSize = true;
            this.autoRefresh.Location = new System.Drawing.Point(6, 19);
            this.autoRefresh.Name = "autoRefresh";
            this.autoRefresh.Size = new System.Drawing.Size(48, 17);
            this.autoRefresh.TabIndex = 11;
            this.autoRefresh.Text = "Auto";
            this.autoRefresh.UseVisualStyleBackColor = true;
            // 
            // PropertiesdataGridView
            // 
            this.PropertiesdataGridView.AllowUserToAddRows = false;
            this.PropertiesdataGridView.AllowUserToDeleteRows = false;
            this.PropertiesdataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PropertiesdataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.PropertiesdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PropertiesdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propriété,
            this.Valeur});
            this.PropertiesdataGridView.Location = new System.Drawing.Point(3, 19);
            this.PropertiesdataGridView.Name = "PropertiesdataGridView";
            this.PropertiesdataGridView.ReadOnly = true;
            this.PropertiesdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PropertiesdataGridView.Size = new System.Drawing.Size(383, 116);
            this.PropertiesdataGridView.TabIndex = 10;
            this.PropertiesdataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PropertiesdataGridView_KeyUp);
            // 
            // Propriété
            // 
            this.Propriété.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Propriété.HeaderText = "Propriété";
            this.Propriété.Name = "Propriété";
            this.Propriété.ReadOnly = true;
            // 
            // Valeur
            // 
            this.Valeur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Valeur.HeaderText = "Valeur";
            this.Valeur.Name = "Valeur";
            this.Valeur.ReadOnly = true;
            // 
            // build
            // 
            this.build.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.build.Location = new System.Drawing.Point(44, 98);
            this.build.Name = "build";
            this.build.ReadOnly = true;
            this.build.Size = new System.Drawing.Size(230, 20);
            this.build.TabIndex = 9;
            // 
            // buildlabel
            // 
            this.buildlabel.AutoSize = true;
            this.buildlabel.Location = new System.Drawing.Point(2, 100);
            this.buildlabel.Name = "buildlabel";
            this.buildlabel.Size = new System.Drawing.Size(30, 13);
            this.buildlabel.TabIndex = 8;
            this.buildlabel.Text = "Build";
            // 
            // LedPicture
            // 
            this.LedPicture.BackColor = System.Drawing.SystemColors.Control;
            this.LedPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LedPicture.Image = global::RSSRTReader.Properties.Resources.redled;
            this.LedPicture.Location = new System.Drawing.Point(182, 127);
            this.LedPicture.Name = "LedPicture";
            this.LedPicture.Padding = new System.Windows.Forms.Padding(3);
            this.LedPicture.Size = new System.Drawing.Size(26, 26);
            this.LedPicture.TabIndex = 7;
            this.LedPicture.TabStop = false;
            // 
            // connectionAction
            // 
            this.connectionAction.Location = new System.Drawing.Point(76, 127);
            this.connectionAction.Name = "connectionAction";
            this.connectionAction.Size = new System.Drawing.Size(100, 25);
            this.connectionAction.TabIndex = 6;
            this.connectionAction.Text = "connection";
            this.connectionAction.UseVisualStyleBackColor = true;
            this.connectionAction.MouseUp += new System.Windows.Forms.MouseEventHandler(this.connectionAction_MouseUp);
            // 
            // nomLabel
            // 
            this.nomLabel.AutoSize = true;
            this.nomLabel.Location = new System.Drawing.Point(2, 73);
            this.nomLabel.Name = "nomLabel";
            this.nomLabel.Size = new System.Drawing.Size(29, 13);
            this.nomLabel.TabIndex = 5;
            this.nomLabel.Text = "Nom";
            // 
            // nom
            // 
            this.nom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nom.Location = new System.Drawing.Point(44, 71);
            this.nom.Name = "nom";
            this.nom.ReadOnly = true;
            this.nom.Size = new System.Drawing.Size(230, 20);
            this.nom.TabIndex = 4;
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(43, 45);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(231, 20);
            this.PortText.TabIndex = 3;
            this.PortText.Text = "1254";
            this.PortText.TextChanged += new System.EventHandler(this.PortText_TextChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(2, 47);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            // 
            // IPlabel
            // 
            this.IPlabel.AutoSize = true;
            this.IPlabel.Location = new System.Drawing.Point(2, 22);
            this.IPlabel.Name = "IPlabel";
            this.IPlabel.Size = new System.Drawing.Size(17, 13);
            this.IPlabel.TabIndex = 1;
            this.IPlabel.Text = "IP";
            // 
            // IpText
            // 
            this.IpText.Location = new System.Drawing.Point(43, 19);
            this.IpText.Name = "IpText";
            this.IpText.Size = new System.Drawing.Size(231, 20);
            this.IpText.TabIndex = 0;
            this.IpText.Text = "127.0.0.1";
            this.IpText.TextChanged += new System.EventHandler(this.IPtext_TextChanged);
            // 
            // Tables
            // 
            this.Tables.BackColor = System.Drawing.SystemColors.Control;
            this.Tables.Controls.Add(this.knwSavegroupBox);
            this.Tables.Controls.Add(this.defaulttables);
            this.Tables.Controls.Add(this.button2);
            this.Tables.Location = new System.Drawing.Point(3, 6);
            this.Tables.Name = "Tables";
            this.Tables.Size = new System.Drawing.Size(227, 171);
            this.Tables.TabIndex = 10;
            this.Tables.TabStop = false;
            this.Tables.Text = "Tables";
            // 
            // knwSavegroupBox
            // 
            this.knwSavegroupBox.Controls.Add(this.label4);
            this.knwSavegroupBox.Controls.Add(this.label3);
            this.knwSavegroupBox.Controls.Add(this.saveKnwtextBox);
            this.knwSavegroupBox.Controls.Add(this.label2);
            this.knwSavegroupBox.Location = new System.Drawing.Point(11, 93);
            this.knwSavegroupBox.Name = "knwSavegroupBox";
            this.knwSavegroupBox.Size = new System.Drawing.Size(210, 72);
            this.knwSavegroupBox.TabIndex = 4;
            this.knwSavegroupBox.TabStop = false;
            this.knwSavegroupBox.Text = "Knowledge Sauvegarde";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "articles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // saveKnwtextBox
            // 
            this.saveKnwtextBox.Location = new System.Drawing.Point(50, 17);
            this.saveKnwtextBox.Name = "saveKnwtextBox";
            this.saveKnwtextBox.Size = new System.Drawing.Size(100, 20);
            this.saveKnwtextBox.TabIndex = 1;
            this.saveKnwtextBox.Text = "1000";
            this.saveKnwtextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "tous les";
            // 
            // defaulttables
            // 
            this.defaulttables.AutoSize = true;
            this.defaulttables.Checked = true;
            this.defaulttables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaulttables.Location = new System.Drawing.Point(123, 69);
            this.defaulttables.Name = "defaulttables";
            this.defaulttables.Size = new System.Drawing.Size(58, 17);
            this.defaulttables.TabIndex = 3;
            this.defaulttables.Text = "default";
            this.defaulttables.UseMnemonic = false;
            this.defaulttables.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 21);
            this.button2.TabIndex = 2;
            this.button2.Text = "créer les tables";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CreateTables_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chargerToolStripMenuItem});
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fToolStripMenuItem.Text = "Fichier";
            // 
            // chargerToolStripMenuItem
            // 
            this.chargerToolStripMenuItem.Name = "chargerToolStripMenuItem";
            this.chargerToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.chargerToolStripMenuItem.Text = "Charger";
            this.chargerToolStripMenuItem.Click += new System.EventHandler(this.chargerToolStripMenuItem_Click);
            // 
            // log
            // 
            this.log.Controls.Add(this.clearLog);
            this.log.Controls.Add(this.displayLog);
            this.log.Controls.Add(this.logTextBox);
            this.log.Location = new System.Drawing.Point(2, 3);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(878, 361);
            this.log.TabIndex = 18;
            this.log.TabStop = false;
            this.log.Text = "Log";
            // 
            // clearLog
            // 
            this.clearLog.Location = new System.Drawing.Point(238, 11);
            this.clearLog.Name = "clearLog";
            this.clearLog.Size = new System.Drawing.Size(75, 21);
            this.clearLog.TabIndex = 2;
            this.clearLog.Text = "Clear Log";
            this.clearLog.UseVisualStyleBackColor = true;
            this.clearLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clearLog_MouseClick);
            // 
            // displayLog
            // 
            this.displayLog.AutoSize = true;
            this.displayLog.Location = new System.Drawing.Point(7, 16);
            this.displayLog.Name = "displayLog";
            this.displayLog.Size = new System.Drawing.Size(62, 17);
            this.displayLog.TabIndex = 1;
            this.displayLog.Text = "Afficher\r\n";
            this.displayLog.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(5, 38);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(864, 320);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // autoRefreshTimer
            // 
            this.autoRefreshTimer.Enabled = true;
            this.autoRefreshTimer.Interval = 1000;
            this.autoRefreshTimer.Tick += new System.EventHandler(this.autoRefreshTimer_Tick);
            // 
            // autoRefreshPropertiesbackgroundWorker
            // 
            this.autoRefreshPropertiesbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.autoRefreshPropertiesbackgroundWorker_DoWork);
            this.autoRefreshPropertiesbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.autoRefreshPropertiesbackgroundWorker_RunWorkerCompleted);
            // 
            // mARCtab
            // 
            this.mARCtab.Controls.Add(this.mARCServerPage);
            this.mARCtab.Controls.Add(this.RSSFeedsPage);
            this.mARCtab.Controls.Add(this.RSSFeedsTrackingPage);
            this.mARCtab.Controls.Add(this.ContextuelTrackingtabPage);
            this.mARCtab.Controls.Add(this.SMTPConfigurationTabPage);
            this.mARCtab.Location = new System.Drawing.Point(0, 27);
            this.mARCtab.Name = "mARCtab";
            this.mARCtab.SelectedIndex = 0;
            this.mARCtab.Size = new System.Drawing.Size(919, 502);
            this.mARCtab.TabIndex = 19;
            // 
            // mARCServerPage
            // 
            this.mARCServerPage.Controls.Add(this.mARCServertab);
            this.mARCServerPage.Location = new System.Drawing.Point(4, 22);
            this.mARCServerPage.Name = "mARCServerPage";
            this.mARCServerPage.Padding = new System.Windows.Forms.Padding(3);
            this.mARCServerPage.Size = new System.Drawing.Size(911, 476);
            this.mARCServerPage.TabIndex = 0;
            this.mARCServerPage.Text = "mARC Serveur";
            this.mARCServerPage.UseVisualStyleBackColor = true;
            // 
            // mARCServertab
            // 
            this.mARCServertab.Controls.Add(this.ConnexionPage);
            this.mARCServertab.Controls.Add(this.CustomDocPage);
            this.mARCServertab.Controls.Add(this.TablesPage);
            this.mARCServertab.Controls.Add(this.LogServerPage);
            this.mARCServertab.Location = new System.Drawing.Point(-4, 0);
            this.mARCServertab.Name = "mARCServertab";
            this.mARCServertab.SelectedIndex = 0;
            this.mARCServertab.Size = new System.Drawing.Size(915, 469);
            this.mARCServertab.TabIndex = 20;
            // 
            // ConnexionPage
            // 
            this.ConnexionPage.BackColor = System.Drawing.SystemColors.Control;
            this.ConnexionPage.Controls.Add(this.connection);
            this.ConnexionPage.Location = new System.Drawing.Point(4, 22);
            this.ConnexionPage.Name = "ConnexionPage";
            this.ConnexionPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConnexionPage.Size = new System.Drawing.Size(907, 443);
            this.ConnexionPage.TabIndex = 0;
            this.ConnexionPage.Text = "Connexion";
            // 
            // CustomDocPage
            // 
            this.CustomDocPage.BackColor = System.Drawing.SystemColors.Control;
            this.CustomDocPage.Controls.Add(this.LearngroupBox);
            this.CustomDocPage.Controls.Add(this.URLgroupBox);
            this.CustomDocPage.Controls.Add(this.ContentgroupBox);
            this.CustomDocPage.Controls.Add(this.TitlegroupBox);
            this.CustomDocPage.Location = new System.Drawing.Point(4, 22);
            this.CustomDocPage.Name = "CustomDocPage";
            this.CustomDocPage.Padding = new System.Windows.Forms.Padding(3);
            this.CustomDocPage.Size = new System.Drawing.Size(907, 443);
            this.CustomDocPage.TabIndex = 1;
            this.CustomDocPage.Text = "Custom Document";
            // 
            // LearngroupBox
            // 
            this.LearngroupBox.Controls.Add(this.InsertAndLearnbutton);
            this.LearngroupBox.Location = new System.Drawing.Point(3, 394);
            this.LearngroupBox.Name = "LearngroupBox";
            this.LearngroupBox.Size = new System.Drawing.Size(351, 46);
            this.LearngroupBox.TabIndex = 3;
            this.LearngroupBox.TabStop = false;
            this.LearngroupBox.Text = "Apprentissage";
            // 
            // InsertAndLearnbutton
            // 
            this.InsertAndLearnbutton.Location = new System.Drawing.Point(145, 15);
            this.InsertAndLearnbutton.Name = "InsertAndLearnbutton";
            this.InsertAndLearnbutton.Size = new System.Drawing.Size(190, 23);
            this.InsertAndLearnbutton.TabIndex = 0;
            this.InsertAndLearnbutton.Text = "Insérer dans la Table et Apprendre";
            this.InsertAndLearnbutton.UseVisualStyleBackColor = true;
            this.InsertAndLearnbutton.Click += new System.EventHandler(this.InsertAndLearnbutton_Click);
            // 
            // URLgroupBox
            // 
            this.URLgroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.URLgroupBox.Controls.Add(this.URLtextBox);
            this.URLgroupBox.Location = new System.Drawing.Point(7, 47);
            this.URLgroupBox.Name = "URLgroupBox";
            this.URLgroupBox.Size = new System.Drawing.Size(855, 37);
            this.URLgroupBox.TabIndex = 2;
            this.URLgroupBox.TabStop = false;
            this.URLgroupBox.Text = "URL/emplacement";
            // 
            // URLtextBox
            // 
            this.URLtextBox.Location = new System.Drawing.Point(8, 15);
            this.URLtextBox.Name = "URLtextBox";
            this.URLtextBox.Size = new System.Drawing.Size(841, 20);
            this.URLtextBox.TabIndex = 0;
            this.URLtextBox.Click += new System.EventHandler(this.URLtextBox_Click);
            this.URLtextBox.TextChanged += new System.EventHandler(this.URLtextBox_TextChanged);
            // 
            // ContentgroupBox
            // 
            this.ContentgroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.ContentgroupBox.Controls.Add(this.ContentrichTextBox);
            this.ContentgroupBox.Location = new System.Drawing.Point(3, 86);
            this.ContentgroupBox.Name = "ContentgroupBox";
            this.ContentgroupBox.Size = new System.Drawing.Size(863, 304);
            this.ContentgroupBox.TabIndex = 1;
            this.ContentgroupBox.TabStop = false;
            this.ContentgroupBox.Text = "Contenu";
            // 
            // ContentrichTextBox
            // 
            this.ContentrichTextBox.Location = new System.Drawing.Point(4, 14);
            this.ContentrichTextBox.Name = "ContentrichTextBox";
            this.ContentrichTextBox.Size = new System.Drawing.Size(855, 284);
            this.ContentrichTextBox.TabIndex = 0;
            this.ContentrichTextBox.Text = "";
            // 
            // TitlegroupBox
            // 
            this.TitlegroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.TitlegroupBox.Controls.Add(this.titletextBox);
            this.TitlegroupBox.Location = new System.Drawing.Point(8, 4);
            this.TitlegroupBox.Name = "TitlegroupBox";
            this.TitlegroupBox.Size = new System.Drawing.Size(854, 41);
            this.TitlegroupBox.TabIndex = 0;
            this.TitlegroupBox.TabStop = false;
            this.TitlegroupBox.Text = "Titre";
            // 
            // titletextBox
            // 
            this.titletextBox.Location = new System.Drawing.Point(7, 15);
            this.titletextBox.Name = "titletextBox";
            this.titletextBox.Size = new System.Drawing.Size(841, 20);
            this.titletextBox.TabIndex = 0;
            // 
            // TablesPage
            // 
            this.TablesPage.Controls.Add(this.tabControl2);
            this.TablesPage.Location = new System.Drawing.Point(4, 22);
            this.TablesPage.Name = "TablesPage";
            this.TablesPage.Size = new System.Drawing.Size(907, 443);
            this.TablesPage.TabIndex = 2;
            this.TablesPage.Text = "Tables";
            this.TablesPage.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.createPage);
            this.tabControl2.Controls.Add(this.selectTablePage);
            this.tabControl2.Controls.Add(this.VisualizePage);
            this.tabControl2.Location = new System.Drawing.Point(3, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(904, 447);
            this.tabControl2.TabIndex = 11;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            this.tabControl2.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            // 
            // createPage
            // 
            this.createPage.BackColor = System.Drawing.SystemColors.Control;
            this.createPage.Controls.Add(this.AutoRebuildgroupBox);
            this.createPage.Controls.Add(this.rebuildgroupBox);
            this.createPage.Controls.Add(this.groupBox1);
            this.createPage.Controls.Add(this.Tables);
            this.createPage.Location = new System.Drawing.Point(4, 22);
            this.createPage.Name = "createPage";
            this.createPage.Padding = new System.Windows.Forms.Padding(3);
            this.createPage.Size = new System.Drawing.Size(896, 421);
            this.createPage.TabIndex = 0;
            this.createPage.Text = "Création";
            // 
            // AutoRebuildgroupBox
            // 
            this.AutoRebuildgroupBox.Controls.Add(this.label5);
            this.AutoRebuildgroupBox.Controls.Add(this.AutoRebuildtextBox);
            this.AutoRebuildgroupBox.Controls.Add(this.label6);
            this.AutoRebuildgroupBox.Location = new System.Drawing.Point(545, 6);
            this.AutoRebuildgroupBox.Name = "AutoRebuildgroupBox";
            this.AutoRebuildgroupBox.Size = new System.Drawing.Size(319, 117);
            this.AutoRebuildgroupBox.TabIndex = 12;
            this.AutoRebuildgroupBox.TabStop = false;
            this.AutoRebuildgroupBox.Text = "Ré Apprentissage Automatique";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(273, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "articles";
            // 
            // AutoRebuildtextBox
            // 
            this.AutoRebuildtextBox.Location = new System.Drawing.Point(167, 17);
            this.AutoRebuildtextBox.Name = "AutoRebuildtextBox";
            this.AutoRebuildtextBox.Size = new System.Drawing.Size(100, 20);
            this.AutoRebuildtextBox.TabIndex = 1;
            this.AutoRebuildtextBox.Text = "100000";
            this.AutoRebuildtextBox.TextChanged += new System.EventHandler(this.AutoRebuildtextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ré Apprentissage tous les ";
            // 
            // rebuildgroupBox
            // 
            this.rebuildgroupBox.Controls.Add(this.saveKnwAfterRebuildcheckBox);
            this.rebuildgroupBox.Controls.Add(this.rebuildParamgroupBox);
            this.rebuildgroupBox.Controls.Add(this.rebuildbutton);
            this.rebuildgroupBox.Controls.Add(this.rebuildrangegroupBox);
            this.rebuildgroupBox.Location = new System.Drawing.Point(236, 140);
            this.rebuildgroupBox.Name = "rebuildgroupBox";
            this.rebuildgroupBox.Size = new System.Drawing.Size(278, 218);
            this.rebuildgroupBox.TabIndex = 12;
            this.rebuildgroupBox.TabStop = false;
            this.rebuildgroupBox.Text = "Ré-Apprentissage des documents";
            // 
            // saveKnwAfterRebuildcheckBox
            // 
            this.saveKnwAfterRebuildcheckBox.AutoSize = true;
            this.saveKnwAfterRebuildcheckBox.Checked = true;
            this.saveKnwAfterRebuildcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveKnwAfterRebuildcheckBox.Location = new System.Drawing.Point(10, 165);
            this.saveKnwAfterRebuildcheckBox.Name = "saveKnwAfterRebuildcheckBox";
            this.saveKnwAfterRebuildcheckBox.Size = new System.Drawing.Size(265, 17);
            this.saveKnwAfterRebuildcheckBox.TabIndex = 3;
            this.saveKnwAfterRebuildcheckBox.Text = "Sauvegarde du Knowlegde après ré-apprentissage";
            this.saveKnwAfterRebuildcheckBox.UseVisualStyleBackColor = true;
            // 
            // rebuildParamgroupBox
            // 
            this.rebuildParamgroupBox.Controls.Add(this.rebuildOptionslabel);
            this.rebuildParamgroupBox.Controls.Add(this.rebuildFieldsgroupBox);
            this.rebuildParamgroupBox.Controls.Add(this.rebuildParamcomboBox);
            this.rebuildParamgroupBox.Location = new System.Drawing.Point(10, 72);
            this.rebuildParamgroupBox.Name = "rebuildParamgroupBox";
            this.rebuildParamgroupBox.Size = new System.Drawing.Size(262, 87);
            this.rebuildParamgroupBox.TabIndex = 2;
            this.rebuildParamgroupBox.TabStop = false;
            this.rebuildParamgroupBox.Text = "Paramètres";
            // 
            // rebuildOptionslabel
            // 
            this.rebuildOptionslabel.AutoSize = true;
            this.rebuildOptionslabel.Location = new System.Drawing.Point(91, 19);
            this.rebuildOptionslabel.Name = "rebuildOptionslabel";
            this.rebuildOptionslabel.Size = new System.Drawing.Size(41, 13);
            this.rebuildOptionslabel.TabIndex = 14;
            this.rebuildOptionslabel.Text = "options";
            // 
            // rebuildFieldsgroupBox
            // 
            this.rebuildFieldsgroupBox.Controls.Add(this.fieldsToRebuildtextBox);
            this.rebuildFieldsgroupBox.Location = new System.Drawing.Point(3, 44);
            this.rebuildFieldsgroupBox.Name = "rebuildFieldsgroupBox";
            this.rebuildFieldsgroupBox.Size = new System.Drawing.Size(194, 39);
            this.rebuildFieldsgroupBox.TabIndex = 13;
            this.rebuildFieldsgroupBox.TabStop = false;
            this.rebuildFieldsgroupBox.Text = "Champs (séparés par un espace)";
            // 
            // fieldsToRebuildtextBox
            // 
            this.fieldsToRebuildtextBox.Location = new System.Drawing.Point(3, 16);
            this.fieldsToRebuildtextBox.Name = "fieldsToRebuildtextBox";
            this.fieldsToRebuildtextBox.Size = new System.Drawing.Size(188, 20);
            this.fieldsToRebuildtextBox.TabIndex = 2;
            this.fieldsToRebuildtextBox.Text = "content title";
            // 
            // rebuildParamcomboBox
            // 
            this.rebuildParamcomboBox.FormattingEnabled = true;
            this.rebuildParamcomboBox.Items.AddRange(new object[] {
            "ref",
            "abstract",
            "ref repeat",
            "abstract repeat",
            "vide"});
            this.rebuildParamcomboBox.Location = new System.Drawing.Point(134, 16);
            this.rebuildParamcomboBox.Name = "rebuildParamcomboBox";
            this.rebuildParamcomboBox.Size = new System.Drawing.Size(121, 21);
            this.rebuildParamcomboBox.TabIndex = 0;
            this.rebuildParamcomboBox.Text = "ref";
            // 
            // rebuildbutton
            // 
            this.rebuildbutton.Location = new System.Drawing.Point(96, 189);
            this.rebuildbutton.Name = "rebuildbutton";
            this.rebuildbutton.Size = new System.Drawing.Size(75, 23);
            this.rebuildbutton.TabIndex = 1;
            this.rebuildbutton.Text = "Go";
            this.rebuildbutton.UseVisualStyleBackColor = true;
            this.rebuildbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rebuildbutton_MouseUp);
            // 
            // rebuildrangegroupBox
            // 
            this.rebuildrangegroupBox.Controls.Add(this.rebuildTotextBox);
            this.rebuildrangegroupBox.Controls.Add(this.rebuildTolabel);
            this.rebuildrangegroupBox.Controls.Add(this.rebuildFromtextBox);
            this.rebuildrangegroupBox.Controls.Add(this.rebuildFromlabel);
            this.rebuildrangegroupBox.Location = new System.Drawing.Point(10, 19);
            this.rebuildrangegroupBox.Name = "rebuildrangegroupBox";
            this.rebuildrangegroupBox.Size = new System.Drawing.Size(262, 48);
            this.rebuildrangegroupBox.TabIndex = 0;
            this.rebuildrangegroupBox.TabStop = false;
            this.rebuildrangegroupBox.Text = "range";
            // 
            // rebuildTotextBox
            // 
            this.rebuildTotextBox.Location = new System.Drawing.Point(151, 17);
            this.rebuildTotextBox.Name = "rebuildTotextBox";
            this.rebuildTotextBox.Size = new System.Drawing.Size(104, 20);
            this.rebuildTotextBox.TabIndex = 3;
            // 
            // rebuildTolabel
            // 
            this.rebuildTolabel.AutoSize = true;
            this.rebuildTolabel.Location = new System.Drawing.Point(132, 20);
            this.rebuildTolabel.Name = "rebuildTolabel";
            this.rebuildTolabel.Size = new System.Drawing.Size(13, 13);
            this.rebuildTolabel.TabIndex = 2;
            this.rebuildTolabel.Text = "à";
            // 
            // rebuildFromtextBox
            // 
            this.rebuildFromtextBox.Location = new System.Drawing.Point(32, 17);
            this.rebuildFromtextBox.Name = "rebuildFromtextBox";
            this.rebuildFromtextBox.Size = new System.Drawing.Size(94, 20);
            this.rebuildFromtextBox.TabIndex = 1;
            this.rebuildFromtextBox.Text = "0";
            // 
            // rebuildFromlabel
            // 
            this.rebuildFromlabel.AutoSize = true;
            this.rebuildFromlabel.Location = new System.Drawing.Point(7, 20);
            this.rebuildFromlabel.Name = "rebuildFromlabel";
            this.rebuildFromlabel.Size = new System.Drawing.Size(19, 13);
            this.rebuildFromlabel.TabIndex = 0;
            this.rebuildFromlabel.Text = "de";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.articlelabel);
            this.groupBox1.Controls.Add(this.MaJIndextextBox);
            this.groupBox1.Controls.Add(this.majIndexlabel);
            this.groupBox1.Location = new System.Drawing.Point(236, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 117);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Indexation";
            // 
            // articlelabel
            // 
            this.articlelabel.AutoSize = true;
            this.articlelabel.Location = new System.Drawing.Point(225, 20);
            this.articlelabel.Name = "articlelabel";
            this.articlelabel.Size = new System.Drawing.Size(40, 13);
            this.articlelabel.TabIndex = 2;
            this.articlelabel.Text = "articles";
            // 
            // MaJIndextextBox
            // 
            this.MaJIndextextBox.Location = new System.Drawing.Point(147, 17);
            this.MaJIndextextBox.Name = "MaJIndextextBox";
            this.MaJIndextextBox.Size = new System.Drawing.Size(75, 20);
            this.MaJIndextextBox.TabIndex = 1;
            this.MaJIndextextBox.Text = "10000";
            this.MaJIndextextBox.TextChanged += new System.EventHandler(this.MaJIndextextBox_TextChanged);
            // 
            // majIndexlabel
            // 
            this.majIndexlabel.AutoSize = true;
            this.majIndexlabel.Location = new System.Drawing.Point(7, 20);
            this.majIndexlabel.Name = "majIndexlabel";
            this.majIndexlabel.Size = new System.Drawing.Size(143, 13);
            this.majIndexlabel.TabIndex = 0;
            this.majIndexlabel.Text = "MaJ de l\'indexBegin tous les ";
            // 
            // selectTablePage
            // 
            this.selectTablePage.BackColor = System.Drawing.SystemColors.Control;
            this.selectTablePage.Controls.Add(this.FieldsgroupBox);
            this.selectTablePage.Controls.Add(this.InstancesgroupBox);
            this.selectTablePage.Location = new System.Drawing.Point(4, 22);
            this.selectTablePage.Name = "selectTablePage";
            this.selectTablePage.Size = new System.Drawing.Size(896, 421);
            this.selectTablePage.TabIndex = 2;
            this.selectTablePage.Text = "Sélection";
            // 
            // FieldsgroupBox
            // 
            this.FieldsgroupBox.Controls.Add(this.tableStructure);
            this.FieldsgroupBox.Location = new System.Drawing.Point(122, 17);
            this.FieldsgroupBox.Name = "FieldsgroupBox";
            this.FieldsgroupBox.Size = new System.Drawing.Size(139, 317);
            this.FieldsgroupBox.TabIndex = 1;
            this.FieldsgroupBox.TabStop = false;
            this.FieldsgroupBox.Text = "Champs";
            // 
            // tableStructure
            // 
            this.tableStructure.CheckOnClick = true;
            this.tableStructure.FormattingEnabled = true;
            this.tableStructure.Location = new System.Drawing.Point(5, 19);
            this.tableStructure.MultiColumn = true;
            this.tableStructure.Name = "tableStructure";
            this.tableStructure.Size = new System.Drawing.Size(130, 289);
            this.tableStructure.TabIndex = 2;
            // 
            // InstancesgroupBox
            // 
            this.InstancesgroupBox.Controls.Add(this.tablesInstances);
            this.InstancesgroupBox.Location = new System.Drawing.Point(7, 17);
            this.InstancesgroupBox.Name = "InstancesgroupBox";
            this.InstancesgroupBox.Size = new System.Drawing.Size(114, 317);
            this.InstancesgroupBox.TabIndex = 0;
            this.InstancesgroupBox.TabStop = false;
            this.InstancesgroupBox.Text = "Instances";
            // 
            // tablesInstances
            // 
            this.tablesInstances.FormattingEnabled = true;
            this.tablesInstances.Location = new System.Drawing.Point(6, 19);
            this.tablesInstances.Name = "tablesInstances";
            this.tablesInstances.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.tablesInstances.Size = new System.Drawing.Size(101, 290);
            this.tablesInstances.TabIndex = 4;
            this.tablesInstances.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tablesInstances_MouseClick);
            // 
            // VisualizePage
            // 
            this.VisualizePage.AutoScroll = true;
            this.VisualizePage.BackColor = System.Drawing.SystemColors.Control;
            this.VisualizePage.Controls.Add(this.splitContainer1);
            this.VisualizePage.Location = new System.Drawing.Point(4, 22);
            this.VisualizePage.Name = "VisualizePage";
            this.VisualizePage.Padding = new System.Windows.Forms.Padding(3);
            this.VisualizePage.Size = new System.Drawing.Size(896, 421);
            this.VisualizePage.TabIndex = 1;
            this.VisualizePage.Text = "Visualisation";
            // 
            // TblContentgroupBox
            // 
            this.TblContentgroupBox.AutoSize = true;
            this.TblContentgroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TblContentgroupBox.Controls.Add(this.updateLine);
            this.TblContentgroupBox.Controls.Add(this.lineContent);
            this.TblContentgroupBox.Location = new System.Drawing.Point(3, 3);
            this.TblContentgroupBox.Name = "TblContentgroupBox";
            this.TblContentgroupBox.Size = new System.Drawing.Size(402, 425);
            this.TblContentgroupBox.TabIndex = 1;
            this.TblContentgroupBox.TabStop = false;
            this.TblContentgroupBox.Text = "Contenu";
            // 
            // updateLine
            // 
            this.updateLine.Location = new System.Drawing.Point(6, 14);
            this.updateLine.Name = "updateLine";
            this.updateLine.Size = new System.Drawing.Size(118, 23);
            this.updateLine.TabIndex = 12;
            this.updateLine.Text = "Update Line in Table";
            this.updateLine.UseVisualStyleBackColor = true;
            this.updateLine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.updateLine_MouseClick);
            // 
            // lineContent
            // 
            this.lineContent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lineContent.Location = new System.Drawing.Point(4, 38);
            this.lineContent.Name = "lineContent";
            this.lineContent.Size = new System.Drawing.Size(392, 368);
            this.lineContent.TabIndex = 11;
            this.lineContent.Text = "";
            // 
            // DatagroupBox
            // 
            this.DatagroupBox.AutoSize = true;
            this.DatagroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DatagroupBox.Controls.Add(this.panel1);
            this.DatagroupBox.Location = new System.Drawing.Point(1, 13);
            this.DatagroupBox.Name = "DatagroupBox";
            this.DatagroupBox.Size = new System.Drawing.Size(430, 428);
            this.DatagroupBox.TabIndex = 0;
            this.DatagroupBox.TabStop = false;
            this.DatagroupBox.Text = "Lignes/Colonnes";
            // 
            // AddDocFromDBbutton
            // 
            this.AddDocFromDBbutton.Location = new System.Drawing.Point(1, 367);
            this.AddDocFromDBbutton.Name = "AddDocFromDBbutton";
            this.AddDocFromDBbutton.Size = new System.Drawing.Size(188, 23);
            this.AddDocFromDBbutton.TabIndex = 18;
            this.AddDocFromDBbutton.Text = "Ajouter comme document à tracker";
            this.AddDocFromDBbutton.UseVisualStyleBackColor = true;
            this.AddDocFromDBbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AddDocFromDBbutton_MouseUp);
            // 
            // FetchSizelabel
            // 
            this.FetchSizelabel.AutoSize = true;
            this.FetchSizelabel.Location = new System.Drawing.Point(305, 9);
            this.FetchSizelabel.Name = "FetchSizelabel";
            this.FetchSizelabel.Size = new System.Drawing.Size(43, 13);
            this.FetchSizelabel.TabIndex = 17;
            this.FetchSizelabel.Text = "Afficher";
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.LargeChange = 50;
            this.trackBar.Location = new System.Drawing.Point(5, 318);
            this.trackBar.Margin = new System.Windows.Forms.Padding(1);
            this.trackBar.Maximum = 100000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(408, 45);
            this.trackBar.SmallChange = 5;
            this.trackBar.TabIndex = 16;
            this.trackBar.TickFrequency = 5;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar.Value = 1;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // backward
            // 
            this.backward.Location = new System.Drawing.Point(169, 4);
            this.backward.Name = "backward";
            this.backward.Size = new System.Drawing.Size(63, 23);
            this.backward.TabIndex = 15;
            this.backward.Text = "backward";
            this.backward.UseVisualStyleBackColor = true;
            this.backward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.backward_MouseClick);
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(238, 4);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(61, 23);
            this.forward.TabIndex = 14;
            this.forward.Text = "forward";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.MouseClick += new System.Windows.Forms.MouseEventHandler(this.forward_MouseClick);
            // 
            // refreshData
            // 
            this.refreshData.Location = new System.Drawing.Point(3, 3);
            this.refreshData.Name = "refreshData";
            this.refreshData.Size = new System.Drawing.Size(99, 23);
            this.refreshData.TabIndex = 13;
            this.refreshData.Text = "refresh Data";
            this.refreshData.UseVisualStyleBackColor = true;
            this.refreshData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refreshData_MouseClick);
            // 
            // fetchSize
            // 
            this.fetchSize.Location = new System.Drawing.Point(354, 6);
            this.fetchSize.Name = "fetchSize";
            this.fetchSize.Size = new System.Drawing.Size(41, 20);
            this.fetchSize.TabIndex = 12;
            this.fetchSize.Text = "10";
            // 
            // fetchStart
            // 
            this.fetchStart.Location = new System.Drawing.Point(108, 6);
            this.fetchStart.Name = "fetchStart";
            this.fetchStart.Size = new System.Drawing.Size(55, 20);
            this.fetchStart.TabIndex = 10;
            this.fetchStart.Text = "1";
            this.fetchStart.TextChanged += new System.EventHandler(this.fetchStart_TextChanged);
            // 
            // tableData
            // 
            this.tableData.AllowUserToAddRows = false;
            this.tableData.AllowUserToDeleteRows = false;
            this.tableData.AllowUserToOrderColumns = true;
            this.tableData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tableData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tableData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.tableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableData.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableData.Location = new System.Drawing.Point(3, 38);
            this.tableData.Name = "tableData";
            this.tableData.ReadOnly = true;
            this.tableData.RowTemplate.Height = 24;
            this.tableData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.tableData.Size = new System.Drawing.Size(410, 276);
            this.tableData.TabIndex = 1;
            this.tableData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableData_MouseClick);
            // 
            // LogServerPage
            // 
            this.LogServerPage.Controls.Add(this.log);
            this.LogServerPage.Location = new System.Drawing.Point(4, 22);
            this.LogServerPage.Name = "LogServerPage";
            this.LogServerPage.Size = new System.Drawing.Size(907, 443);
            this.LogServerPage.TabIndex = 3;
            this.LogServerPage.Text = "Log Serveur";
            this.LogServerPage.UseVisualStyleBackColor = true;
            // 
            // RSSFeedsPage
            // 
            this.RSSFeedsPage.BackColor = System.Drawing.SystemColors.Control;
            this.RSSFeedsPage.Controls.Add(this.visualizeFeedgroupBox);
            this.RSSFeedsPage.Controls.Add(this.FeedsgroupBox);
            this.RSSFeedsPage.Location = new System.Drawing.Point(4, 22);
            this.RSSFeedsPage.Name = "RSSFeedsPage";
            this.RSSFeedsPage.Padding = new System.Windows.Forms.Padding(3);
            this.RSSFeedsPage.Size = new System.Drawing.Size(911, 476);
            this.RSSFeedsPage.TabIndex = 1;
            this.RSSFeedsPage.Text = "RSS Feeds";
            // 
            // visualizeFeedgroupBox
            // 
            this.visualizeFeedgroupBox.Controls.Add(this.webBrowser1);
            this.visualizeFeedgroupBox.Location = new System.Drawing.Point(273, 15);
            this.visualizeFeedgroupBox.Name = "visualizeFeedgroupBox";
            this.visualizeFeedgroupBox.Size = new System.Drawing.Size(596, 363);
            this.visualizeFeedgroupBox.TabIndex = 3;
            this.visualizeFeedgroupBox.TabStop = false;
            this.visualizeFeedgroupBox.Text = "Visualisation";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(6, 20);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(584, 337);
            this.webBrowser1.TabIndex = 1;
            // 
            // FeedsgroupBox
            // 
            this.FeedsgroupBox.Controls.Add(this.FeedsTable);
            this.FeedsgroupBox.Controls.Add(this.modifyFeedbutton);
            this.FeedsgroupBox.Controls.Add(this.RemoveFeedbutton);
            this.FeedsgroupBox.Controls.Add(this.addFeedbutton);
            this.FeedsgroupBox.Location = new System.Drawing.Point(8, 15);
            this.FeedsgroupBox.Name = "FeedsgroupBox";
            this.FeedsgroupBox.Size = new System.Drawing.Size(266, 363);
            this.FeedsgroupBox.TabIndex = 1;
            this.FeedsgroupBox.TabStop = false;
            this.FeedsgroupBox.Text = "Feeds";
            // 
            // FeedsTable
            // 
            this.FeedsTable.AllowUserToAddRows = false;
            this.FeedsTable.AllowUserToDeleteRows = false;
            this.FeedsTable.AllowUserToOrderColumns = true;
            this.FeedsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FeedsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.URL});
            this.FeedsTable.Location = new System.Drawing.Point(7, 20);
            this.FeedsTable.Name = "FeedsTable";
            this.FeedsTable.ReadOnly = true;
            this.FeedsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FeedsTable.Size = new System.Drawing.Size(252, 312);
            this.FeedsTable.TabIndex = 4;
            this.FeedsTable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FeedsTable_MouseUp);
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // URL
            // 
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.ReadOnly = true;
            // 
            // modifyFeedbutton
            // 
            this.modifyFeedbutton.Location = new System.Drawing.Point(184, 338);
            this.modifyFeedbutton.Name = "modifyFeedbutton";
            this.modifyFeedbutton.Size = new System.Drawing.Size(75, 23);
            this.modifyFeedbutton.TabIndex = 3;
            this.modifyFeedbutton.Text = "Modifier";
            this.modifyFeedbutton.UseVisualStyleBackColor = true;
            this.modifyFeedbutton.Click += new System.EventHandler(this.modifyFeedbutton_Click);
            // 
            // RemoveFeedbutton
            // 
            this.RemoveFeedbutton.Location = new System.Drawing.Point(102, 338);
            this.RemoveFeedbutton.Name = "RemoveFeedbutton";
            this.RemoveFeedbutton.Size = new System.Drawing.Size(75, 23);
            this.RemoveFeedbutton.TabIndex = 2;
            this.RemoveFeedbutton.Text = "Enlever";
            this.RemoveFeedbutton.UseVisualStyleBackColor = true;
            this.RemoveFeedbutton.Click += new System.EventHandler(this.RemoveFeedbutton_Click);
            // 
            // addFeedbutton
            // 
            this.addFeedbutton.Location = new System.Drawing.Point(21, 338);
            this.addFeedbutton.Name = "addFeedbutton";
            this.addFeedbutton.Size = new System.Drawing.Size(75, 23);
            this.addFeedbutton.TabIndex = 1;
            this.addFeedbutton.Text = "Ajouter";
            this.addFeedbutton.UseVisualStyleBackColor = true;
            this.addFeedbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addFeedbutton_MouseClick);
            // 
            // RSSFeedsTrackingPage
            // 
            this.RSSFeedsTrackingPage.BackColor = System.Drawing.SystemColors.Control;
            this.RSSFeedsTrackingPage.Controls.Add(this.logServergroupBox);
            this.RSSFeedsTrackingPage.Controls.Add(this.RefreshRategroupBox);
            this.RSSFeedsTrackingPage.Controls.Add(this.FeedsSelectiongroupBox1);
            this.RSSFeedsTrackingPage.Location = new System.Drawing.Point(4, 22);
            this.RSSFeedsTrackingPage.Name = "RSSFeedsTrackingPage";
            this.RSSFeedsTrackingPage.Size = new System.Drawing.Size(911, 476);
            this.RSSFeedsTrackingPage.TabIndex = 2;
            this.RSSFeedsTrackingPage.Text = "RSS Feeds Tracking";
            // 
            // logServergroupBox
            // 
            this.logServergroupBox.Controls.Add(this.logServerrichTextBox);
            this.logServergroupBox.Location = new System.Drawing.Point(3, 348);
            this.logServergroupBox.Name = "logServergroupBox";
            this.logServergroupBox.Size = new System.Drawing.Size(866, 114);
            this.logServergroupBox.TabIndex = 2;
            this.logServergroupBox.TabStop = false;
            this.logServergroupBox.Text = "Log Serveur";
            // 
            // logServerrichTextBox
            // 
            this.logServerrichTextBox.Location = new System.Drawing.Point(6, 20);
            this.logServerrichTextBox.Name = "logServerrichTextBox";
            this.logServerrichTextBox.ReadOnly = true;
            this.logServerrichTextBox.Size = new System.Drawing.Size(854, 89);
            this.logServerrichTextBox.TabIndex = 0;
            this.logServerrichTextBox.Text = "";
            // 
            // RefreshRategroupBox
            // 
            this.RefreshRategroupBox.Controls.Add(this.RefreshRateUnitcomboBox);
            this.RefreshRategroupBox.Controls.Add(this.RRtrackBar);
            this.RefreshRategroupBox.Controls.Add(this.RRtextBox);
            this.RefreshRategroupBox.Controls.Add(this.RRActifcheckBox);
            this.RefreshRategroupBox.Location = new System.Drawing.Point(337, 20);
            this.RefreshRategroupBox.Name = "RefreshRategroupBox";
            this.RefreshRategroupBox.Size = new System.Drawing.Size(428, 59);
            this.RefreshRategroupBox.TabIndex = 1;
            this.RefreshRategroupBox.TabStop = false;
            this.RefreshRategroupBox.Text = "Refresh Rate";
            // 
            // RefreshRateUnitcomboBox
            // 
            this.RefreshRateUnitcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RefreshRateUnitcomboBox.FormattingEnabled = true;
            this.RefreshRateUnitcomboBox.Items.AddRange(new object[] {
            "milliseconde",
            "seconde",
            "minute",
            "heure",
            "jour"});
            this.RefreshRateUnitcomboBox.Location = new System.Drawing.Point(188, 17);
            this.RefreshRateUnitcomboBox.Name = "RefreshRateUnitcomboBox";
            this.RefreshRateUnitcomboBox.Size = new System.Drawing.Size(82, 21);
            this.RefreshRateUnitcomboBox.TabIndex = 4;
            this.RefreshRateUnitcomboBox.TextChanged += new System.EventHandler(this.RefreshRateUnitcomboBox_TextChanged);
            // 
            // RRtrackBar
            // 
            this.RRtrackBar.Location = new System.Drawing.Point(318, 10);
            this.RRtrackBar.Name = "RRtrackBar";
            this.RRtrackBar.Size = new System.Drawing.Size(104, 45);
            this.RRtrackBar.TabIndex = 3;
            this.RRtrackBar.TickFrequency = 1000;
            this.RRtrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.RRtrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RRtrackBar_MouseUp);
            // 
            // RRtextBox
            // 
            this.RRtextBox.Location = new System.Drawing.Point(71, 17);
            this.RRtextBox.Name = "RRtextBox";
            this.RRtextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RRtextBox.Size = new System.Drawing.Size(100, 20);
            this.RRtextBox.TabIndex = 2;
            this.RRtextBox.Text = "1";
            this.RRtextBox.TextChanged += new System.EventHandler(this.RRtextBox_TextChanged);
            this.RRtextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RRtextBox_KeyUp);
            // 
            // RRActifcheckBox
            // 
            this.RRActifcheckBox.AutoSize = true;
            this.RRActifcheckBox.Location = new System.Drawing.Point(6, 19);
            this.RRActifcheckBox.Name = "RRActifcheckBox";
            this.RRActifcheckBox.Size = new System.Drawing.Size(47, 17);
            this.RRActifcheckBox.TabIndex = 0;
            this.RRActifcheckBox.Text = "Actif";
            this.RRActifcheckBox.UseVisualStyleBackColor = true;
            this.RRActifcheckBox.CheckedChanged += new System.EventHandler(this.RRActifcheckBox_CheckedChanged);
            // 
            // FeedsSelectiongroupBox1
            // 
            this.FeedsSelectiongroupBox1.Controls.Add(this.FeedscheckedListBox);
            this.FeedsSelectiongroupBox1.Location = new System.Drawing.Point(9, 20);
            this.FeedsSelectiongroupBox1.Name = "FeedsSelectiongroupBox1";
            this.FeedsSelectiongroupBox1.Size = new System.Drawing.Size(308, 207);
            this.FeedsSelectiongroupBox1.TabIndex = 0;
            this.FeedsSelectiongroupBox1.TabStop = false;
            this.FeedsSelectiongroupBox1.Text = "Sélection des Feeds";
            // 
            // FeedscheckedListBox
            // 
            this.FeedscheckedListBox.CheckOnClick = true;
            this.FeedscheckedListBox.FormattingEnabled = true;
            this.FeedscheckedListBox.Location = new System.Drawing.Point(6, 19);
            this.FeedscheckedListBox.Name = "FeedscheckedListBox";
            this.FeedscheckedListBox.Size = new System.Drawing.Size(296, 184);
            this.FeedscheckedListBox.TabIndex = 0;
            this.FeedscheckedListBox.ThreeDCheckBoxes = true;
            // 
            // ContextuelTrackingtabPage
            // 
            this.ContextuelTrackingtabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ContextuelTrackingtabPage.Controls.Add(this.TuninggroupBox);
            this.ContextuelTrackingtabPage.Controls.Add(this.similardDocsgroupBox);
            this.ContextuelTrackingtabPage.Controls.Add(this.trackingDocsgroupBox);
            this.ContextuelTrackingtabPage.Location = new System.Drawing.Point(4, 22);
            this.ContextuelTrackingtabPage.Name = "ContextuelTrackingtabPage";
            this.ContextuelTrackingtabPage.Size = new System.Drawing.Size(911, 476);
            this.ContextuelTrackingtabPage.TabIndex = 3;
            this.ContextuelTrackingtabPage.Text = "Contextual Tracking";
            // 
            // TuninggroupBox
            // 
            this.TuninggroupBox.Controls.Add(this.fetchSizeSimilarDocsTextBox);
            this.TuninggroupBox.Controls.Add(this.fetchSsimilarDocslabel);
            this.TuninggroupBox.Controls.Add(this.depthSimilarDocstextBox);
            this.TuninggroupBox.Controls.Add(this.depthlabel);
            this.TuninggroupBox.Controls.Add(this.maxgentextBox);
            this.TuninggroupBox.Controls.Add(this.maxgenlabel);
            this.TuninggroupBox.Controls.Add(this.maxSignatureSimilarDocstextBox);
            this.TuninggroupBox.Controls.Add(this.tailleMaxlabel);
            this.TuninggroupBox.Controls.Add(this.tailleSimilarDocstextBox);
            this.TuninggroupBox.Controls.Add(this.taillelabel);
            this.TuninggroupBox.Controls.Add(this.seuilsimilarDocstextBox);
            this.TuninggroupBox.Controls.Add(this.Seuillabel);
            this.TuninggroupBox.Location = new System.Drawing.Point(203, 416);
            this.TuninggroupBox.Name = "TuninggroupBox";
            this.TuninggroupBox.Size = new System.Drawing.Size(666, 45);
            this.TuninggroupBox.TabIndex = 2;
            this.TuninggroupBox.TabStop = false;
            this.TuninggroupBox.Text = "Tuning";
            // 
            // fetchSizeSimilarDocsTextBox
            // 
            this.fetchSizeSimilarDocsTextBox.Location = new System.Drawing.Point(560, 19);
            this.fetchSizeSimilarDocsTextBox.Name = "fetchSizeSimilarDocsTextBox";
            this.fetchSizeSimilarDocsTextBox.Size = new System.Drawing.Size(100, 20);
            this.fetchSizeSimilarDocsTextBox.TabIndex = 11;
            this.fetchSizeSimilarDocsTextBox.Text = "20";
            // 
            // fetchSsimilarDocslabel
            // 
            this.fetchSsimilarDocslabel.AutoSize = true;
            this.fetchSsimilarDocslabel.Location = new System.Drawing.Point(502, 23);
            this.fetchSsimilarDocslabel.Name = "fetchSsimilarDocslabel";
            this.fetchSsimilarDocslabel.Size = new System.Drawing.Size(54, 13);
            this.fetchSsimilarDocslabel.TabIndex = 10;
            this.fetchSsimilarDocslabel.Text = "fetch Size";
            // 
            // depthSimilarDocstextBox
            // 
            this.depthSimilarDocstextBox.Location = new System.Drawing.Point(465, 19);
            this.depthSimilarDocstextBox.Name = "depthSimilarDocstextBox";
            this.depthSimilarDocstextBox.Size = new System.Drawing.Size(31, 20);
            this.depthSimilarDocstextBox.TabIndex = 9;
            this.depthSimilarDocstextBox.Text = "1";
            this.depthSimilarDocstextBox.TextChanged += new System.EventHandler(this.depthSimilarDocstextBox_TextChanged);
            // 
            // depthlabel
            // 
            this.depthlabel.AutoSize = true;
            this.depthlabel.Location = new System.Drawing.Point(427, 22);
            this.depthlabel.Name = "depthlabel";
            this.depthlabel.Size = new System.Drawing.Size(34, 13);
            this.depthlabel.TabIndex = 8;
            this.depthlabel.Text = "depth";
            // 
            // maxgentextBox
            // 
            this.maxgentextBox.Location = new System.Drawing.Point(385, 18);
            this.maxgentextBox.Name = "maxgentextBox";
            this.maxgentextBox.Size = new System.Drawing.Size(38, 20);
            this.maxgentextBox.TabIndex = 7;
            this.maxgentextBox.Text = "2";
            this.maxgentextBox.TextChanged += new System.EventHandler(this.maxgentextBox_TextChanged);
            // 
            // maxgenlabel
            // 
            this.maxgenlabel.AutoSize = true;
            this.maxgenlabel.Location = new System.Drawing.Point(335, 22);
            this.maxgenlabel.Name = "maxgenlabel";
            this.maxgenlabel.Size = new System.Drawing.Size(44, 13);
            this.maxgenlabel.TabIndex = 6;
            this.maxgenlabel.Text = "maxgen";
            // 
            // maxSignatureSimilarDocstextBox
            // 
            this.maxSignatureSimilarDocstextBox.Location = new System.Drawing.Point(290, 19);
            this.maxSignatureSimilarDocstextBox.Name = "maxSignatureSimilarDocstextBox";
            this.maxSignatureSimilarDocstextBox.Size = new System.Drawing.Size(43, 20);
            this.maxSignatureSimilarDocstextBox.TabIndex = 5;
            this.maxSignatureSimilarDocstextBox.Text = "5";
            this.maxSignatureSimilarDocstextBox.TextChanged += new System.EventHandler(this.maxSignatureSimilarDocstextBox_TextChanged);
            // 
            // tailleMaxlabel
            // 
            this.tailleMaxlabel.AutoSize = true;
            this.tailleMaxlabel.Location = new System.Drawing.Point(155, 22);
            this.tailleMaxlabel.Name = "tailleMaxlabel";
            this.tailleMaxlabel.Size = new System.Drawing.Size(132, 13);
            this.tailleMaxlabel.TabIndex = 4;
            this.tailleMaxlabel.Text = "Taille Max Sous Contextes";
            // 
            // tailleSimilarDocstextBox
            // 
            this.tailleSimilarDocstextBox.Location = new System.Drawing.Point(119, 19);
            this.tailleSimilarDocstextBox.Name = "tailleSimilarDocstextBox";
            this.tailleSimilarDocstextBox.Size = new System.Drawing.Size(32, 20);
            this.tailleSimilarDocstextBox.TabIndex = 3;
            this.tailleSimilarDocstextBox.Text = "10";
            this.tailleSimilarDocstextBox.TextChanged += new System.EventHandler(this.tailleSimilarDocstextBox_TextChanged);
            // 
            // taillelabel
            // 
            this.taillelabel.AutoSize = true;
            this.taillelabel.Location = new System.Drawing.Point(83, 22);
            this.taillelabel.Name = "taillelabel";
            this.taillelabel.Size = new System.Drawing.Size(32, 13);
            this.taillelabel.TabIndex = 2;
            this.taillelabel.Text = "Taille";
            // 
            // seuilsimilarDocstextBox
            // 
            this.seuilsimilarDocstextBox.Location = new System.Drawing.Point(40, 19);
            this.seuilsimilarDocstextBox.Name = "seuilsimilarDocstextBox";
            this.seuilsimilarDocstextBox.Size = new System.Drawing.Size(38, 20);
            this.seuilsimilarDocstextBox.TabIndex = 1;
            this.seuilsimilarDocstextBox.Text = "80";
            this.seuilsimilarDocstextBox.TextChanged += new System.EventHandler(this.seuilsimilarDocstextBox_TextChanged);
            // 
            // Seuillabel
            // 
            this.Seuillabel.AutoSize = true;
            this.Seuillabel.Location = new System.Drawing.Point(7, 23);
            this.Seuillabel.Name = "Seuillabel";
            this.Seuillabel.Size = new System.Drawing.Size(30, 13);
            this.Seuillabel.TabIndex = 0;
            this.Seuillabel.Text = "Seuil";
            // 
            // similardDocsgroupBox
            // 
            this.similardDocsgroupBox.Controls.Add(this.similarDocsdataGridView);
            this.similardDocsgroupBox.Controls.Add(this.similarDocstreeView);
            this.similardDocsgroupBox.Location = new System.Drawing.Point(200, 15);
            this.similardDocsgroupBox.Name = "similardDocsgroupBox";
            this.similardDocsgroupBox.Size = new System.Drawing.Size(669, 398);
            this.similardDocsgroupBox.TabIndex = 1;
            this.similardDocsgroupBox.TabStop = false;
            this.similardDocsgroupBox.Text = "Documents in Context";
            // 
            // similarDocsdataGridView
            // 
            this.similarDocsdataGridView.AllowUserToAddRows = false;
            this.similarDocsdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.similarDocsdataGridView.Location = new System.Drawing.Point(294, 14);
            this.similarDocsdataGridView.Name = "similarDocsdataGridView";
            this.similarDocsdataGridView.Size = new System.Drawing.Size(369, 377);
            this.similarDocsdataGridView.TabIndex = 1;
            // 
            // similarDocstreeView
            // 
            this.similarDocstreeView.Location = new System.Drawing.Point(2, 14);
            this.similarDocstreeView.Name = "similarDocstreeView";
            this.similarDocstreeView.ShowRootLines = false;
            this.similarDocstreeView.Size = new System.Drawing.Size(285, 377);
            this.similarDocstreeView.TabIndex = 0;
            this.similarDocstreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.similarDocstreeView_AfterSelect);
            // 
            // trackingDocsgroupBox
            // 
            this.trackingDocsgroupBox.Controls.Add(this.groupBox2);
            this.trackingDocsgroupBox.Controls.Add(this.timerSimilarDocsgroupBox);
            this.trackingDocsgroupBox.Controls.Add(this.trackingDocscheckedListBox);
            this.trackingDocsgroupBox.Location = new System.Drawing.Point(9, 15);
            this.trackingDocsgroupBox.Name = "trackingDocsgroupBox";
            this.trackingDocsgroupBox.Size = new System.Drawing.Size(189, 447);
            this.trackingDocsgroupBox.TabIndex = 0;
            this.trackingDocsgroupBox.TabStop = false;
            this.trackingDocsgroupBox.Text = "Documents to Track";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.similarDocsFormattextBox);
            this.groupBox2.Location = new System.Drawing.Point(2, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 68);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Format de récupération des données";
            // 
            // similarDocsFormattextBox
            // 
            this.similarDocsFormattextBox.Location = new System.Drawing.Point(7, 37);
            this.similarDocsFormattextBox.Name = "similarDocsFormattextBox";
            this.similarDocsFormattextBox.Size = new System.Drawing.Size(174, 20);
            this.similarDocsFormattextBox.TabIndex = 0;
            this.similarDocsFormattextBox.Text = "RowId title link Act";
            // 
            // timerSimilarDocsgroupBox
            // 
            this.timerSimilarDocsgroupBox.Controls.Add(this.timersimilarDocscomboBox);
            this.timerSimilarDocsgroupBox.Controls.Add(this.timerSimilarDocstextBox);
            this.timerSimilarDocsgroupBox.Controls.Add(this.timerSimilarDocscheckBox);
            this.timerSimilarDocsgroupBox.Location = new System.Drawing.Point(6, 404);
            this.timerSimilarDocsgroupBox.Name = "timerSimilarDocsgroupBox";
            this.timerSimilarDocsgroupBox.Size = new System.Drawing.Size(180, 39);
            this.timerSimilarDocsgroupBox.TabIndex = 2;
            this.timerSimilarDocsgroupBox.TabStop = false;
            this.timerSimilarDocsgroupBox.Text = "Timer";
            // 
            // timersimilarDocscomboBox
            // 
            this.timersimilarDocscomboBox.FormattingEnabled = true;
            this.timersimilarDocscomboBox.Items.AddRange(new object[] {
            "ms",
            "s",
            "minute",
            "heure",
            "jour"});
            this.timersimilarDocscomboBox.Location = new System.Drawing.Point(101, 13);
            this.timersimilarDocscomboBox.Name = "timersimilarDocscomboBox";
            this.timersimilarDocscomboBox.Size = new System.Drawing.Size(76, 21);
            this.timersimilarDocscomboBox.TabIndex = 5;
            this.timersimilarDocscomboBox.Text = "s";
            this.timersimilarDocscomboBox.TextChanged += new System.EventHandler(this.timersimilarDocscomboBox_TextChanged);
            // 
            // timerSimilarDocstextBox
            // 
            this.timerSimilarDocstextBox.Location = new System.Drawing.Point(51, 14);
            this.timerSimilarDocstextBox.Name = "timerSimilarDocstextBox";
            this.timerSimilarDocstextBox.Size = new System.Drawing.Size(44, 20);
            this.timerSimilarDocstextBox.TabIndex = 2;
            this.timerSimilarDocstextBox.Text = "1";
            this.timerSimilarDocstextBox.TextChanged += new System.EventHandler(this.timerSimilarDocstextBox_TextChanged);
            // 
            // timerSimilarDocscheckBox
            // 
            this.timerSimilarDocscheckBox.AutoSize = true;
            this.timerSimilarDocscheckBox.Location = new System.Drawing.Point(6, 16);
            this.timerSimilarDocscheckBox.Name = "timerSimilarDocscheckBox";
            this.timerSimilarDocscheckBox.Size = new System.Drawing.Size(47, 17);
            this.timerSimilarDocscheckBox.TabIndex = 0;
            this.timerSimilarDocscheckBox.Text = "Actif";
            this.timerSimilarDocscheckBox.UseVisualStyleBackColor = true;
            this.timerSimilarDocscheckBox.CheckedChanged += new System.EventHandler(this.timeSimilarDocscheckBox_CheckedChanged);
            // 
            // trackingDocscheckedListBox
            // 
            this.trackingDocscheckedListBox.CheckOnClick = true;
            this.trackingDocscheckedListBox.FormattingEnabled = true;
            this.trackingDocscheckedListBox.Location = new System.Drawing.Point(6, 94);
            this.trackingDocscheckedListBox.Name = "trackingDocscheckedListBox";
            this.trackingDocscheckedListBox.Size = new System.Drawing.Size(177, 304);
            this.trackingDocscheckedListBox.TabIndex = 0;
            // 
            // SMTPConfigurationTabPage
            // 
            this.SMTPConfigurationTabPage.Controls.Add(this.UserDataGourpBox);
            this.SMTPConfigurationTabPage.Controls.Add(this.SMTPServergroupBox);
            this.SMTPConfigurationTabPage.Location = new System.Drawing.Point(4, 22);
            this.SMTPConfigurationTabPage.Name = "SMTPConfigurationTabPage";
            this.SMTPConfigurationTabPage.Size = new System.Drawing.Size(911, 476);
            this.SMTPConfigurationTabPage.TabIndex = 4;
            this.SMTPConfigurationTabPage.Text = "SMTP Configuration";
            this.SMTPConfigurationTabPage.UseVisualStyleBackColor = true;
            // 
            // UserDataGourpBox
            // 
            this.UserDataGourpBox.Controls.Add(this.textBox2);
            this.UserDataGourpBox.Controls.Add(this.emailAddresslabel);
            this.UserDataGourpBox.Location = new System.Drawing.Point(18, 134);
            this.UserDataGourpBox.Name = "UserDataGourpBox";
            this.UserDataGourpBox.Size = new System.Drawing.Size(385, 69);
            this.UserDataGourpBox.TabIndex = 1;
            this.UserDataGourpBox.TabStop = false;
            this.UserDataGourpBox.Text = "Données Utilisateur";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(94, 31);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(285, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "patrice.descourt@marvinbot.com";
            // 
            // emailAddresslabel
            // 
            this.emailAddresslabel.AutoSize = true;
            this.emailAddresslabel.Location = new System.Drawing.Point(10, 34);
            this.emailAddresslabel.Name = "emailAddresslabel";
            this.emailAddresslabel.Size = new System.Drawing.Size(73, 13);
            this.emailAddresslabel.TabIndex = 0;
            this.emailAddresslabel.Text = "Adresse eMail";
            // 
            // SMTPServergroupBox
            // 
            this.SMTPServergroupBox.Controls.Add(this.SMTPPorttextBox);
            this.SMTPServergroupBox.Controls.Add(this.SMTPPortlabel);
            this.SMTPServergroupBox.Controls.Add(this.SMTPHosttextBox);
            this.SMTPServergroupBox.Controls.Add(this.SMTPName);
            this.SMTPServergroupBox.Location = new System.Drawing.Point(18, 18);
            this.SMTPServergroupBox.Name = "SMTPServergroupBox";
            this.SMTPServergroupBox.Size = new System.Drawing.Size(255, 96);
            this.SMTPServergroupBox.TabIndex = 0;
            this.SMTPServergroupBox.TabStop = false;
            this.SMTPServergroupBox.Text = "Serveur SMTP";
            // 
            // SMTPPorttextBox
            // 
            this.SMTPPorttextBox.Location = new System.Drawing.Point(94, 52);
            this.SMTPPorttextBox.Name = "SMTPPorttextBox";
            this.SMTPPorttextBox.Size = new System.Drawing.Size(142, 20);
            this.SMTPPorttextBox.TabIndex = 3;
            this.SMTPPorttextBox.Text = "587";
            this.SMTPPorttextBox.TextChanged += new System.EventHandler(this.SMTPPorttextBox_TextChanged);
            // 
            // SMTPPortlabel
            // 
            this.SMTPPortlabel.AutoSize = true;
            this.SMTPPortlabel.Location = new System.Drawing.Point(7, 55);
            this.SMTPPortlabel.Name = "SMTPPortlabel";
            this.SMTPPortlabel.Size = new System.Drawing.Size(63, 13);
            this.SMTPPortlabel.TabIndex = 2;
            this.SMTPPortlabel.Text = "Port Sortant";
            // 
            // SMTPHosttextBox
            // 
            this.SMTPHosttextBox.Location = new System.Drawing.Point(94, 26);
            this.SMTPHosttextBox.Name = "SMTPHosttextBox";
            this.SMTPHosttextBox.Size = new System.Drawing.Size(142, 20);
            this.SMTPHosttextBox.TabIndex = 1;
            this.SMTPHosttextBox.Text = "smtp.marvinbot.com";
            this.SMTPHosttextBox.TextChanged += new System.EventHandler(this.SMTPHosttextBox_TextChanged);
            // 
            // SMTPName
            // 
            this.SMTPName.AutoSize = true;
            this.SMTPName.Location = new System.Drawing.Point(7, 29);
            this.SMTPName.Name = "SMTPName";
            this.SMTPName.Size = new System.Drawing.Size(29, 13);
            this.SMTPName.TabIndex = 0;
            this.SMTPName.Text = "Nom";
            // 
            // updateLinebackgroundWorker
            // 
            this.updateLinebackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateLinebackgroundWorker_DoWork);
            this.updateLinebackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateLinebackgroundWorker_RunWorkerCompleted);
            // 
            // DisplayLineContentbackgroundWorker
            // 
            this.DisplayLineContentbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DisplayLineContentbackgroundWorker_DoWork);
            this.DisplayLineContentbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DisplayLineContentbackgroundWorker_RunWorkerCompleted);
            // 
            // GetTblInstancesbackgroundWorker
            // 
            this.GetTblInstancesbackgroundWorker.WorkerReportsProgress = true;
            this.GetTblInstancesbackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblInstancesbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblInstancesbackgroundWorker_DoWork);
            this.GetTblInstancesbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetTblInstancesbackgroundWorker_RunWorkerCompleted);
            // 
            // GetTblFieldsbackgroundWorker
            // 
            this.GetTblFieldsbackgroundWorker.WorkerReportsProgress = true;
            this.GetTblFieldsbackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblFieldsbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblFieldsbackgroundWorker_DoWork);
            this.GetTblFieldsbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetTblFieldsbackgroundWorker_RunWorkerCompleted);
            // 
            // GetTblDatabackgroundWorker
            // 
            this.GetTblDatabackgroundWorker.WorkerReportsProgress = true;
            this.GetTblDatabackgroundWorker.WorkerSupportsCancellation = true;
            this.GetTblDatabackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblDatabackgroundWorker_DoWork);
            this.GetTblDatabackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetTblDatabackgroundWorker_RunWorkerCompleted);
            // 
            // UpdateFeedsbackgroundWorker
            // 
            this.UpdateFeedsbackgroundWorker.WorkerSupportsCancellation = true;
            this.UpdateFeedsbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateFeedsbackgroundWorker_DoWork);
            this.UpdateFeedsbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.UpdateFeedsbackgroundWorker_RunWorkerCompleted);
            // 
            // updateFeedstimer
            // 
            this.updateFeedstimer.Interval = 1000;
            this.updateFeedstimer.Tick += new System.EventHandler(this.updateFeedstimer_Tick);
            // 
            // ParseHTMLbackgroundWorker
            // 
            this.ParseHTMLbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ParseHTMLbackgroundWorker_DoWork);
            this.ParseHTMLbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ParseHTMLbackgroundWorker_RunWorkerCompleted);
            // 
            // KnwgroupBox
            // 
            this.KnwgroupBox.Controls.Add(this.publishKnwbutton);
            this.KnwgroupBox.Controls.Add(this.saveKnwButton);
            this.KnwgroupBox.Location = new System.Drawing.Point(3, 535);
            this.KnwgroupBox.Name = "KnwgroupBox";
            this.KnwgroupBox.Size = new System.Drawing.Size(324, 41);
            this.KnwgroupBox.TabIndex = 20;
            this.KnwgroupBox.TabStop = false;
            this.KnwgroupBox.Text = "Objet Knowledge";
            // 
            // publishKnwbutton
            // 
            this.publishKnwbutton.Location = new System.Drawing.Point(166, 14);
            this.publishKnwbutton.Name = "publishKnwbutton";
            this.publishKnwbutton.Size = new System.Drawing.Size(152, 23);
            this.publishKnwbutton.TabIndex = 2;
            this.publishKnwbutton.Text = "Mettre à Jour l\'Indexation";
            this.publishKnwbutton.UseVisualStyleBackColor = true;
            this.publishKnwbutton.Click += new System.EventHandler(this.publishKnwbutton_Click);
            // 
            // saveKnwButton
            // 
            this.saveKnwButton.Location = new System.Drawing.Point(8, 14);
            this.saveKnwButton.Name = "saveKnwButton";
            this.saveKnwButton.Size = new System.Drawing.Size(152, 23);
            this.saveKnwButton.TabIndex = 1;
            this.saveKnwButton.Text = "sauvegarde du knowledge";
            this.saveKnwButton.UseVisualStyleBackColor = true;
            this.saveKnwButton.Click += new System.EventHandler(this.saveKnwButton_Click);
            // 
            // InsertAndLearnbackgroundWorker
            // 
            this.InsertAndLearnbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InsertAndLearnbackgroundWorker_DoWork);
            this.InsertAndLearnbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InsertAndLearnbackgroundWorker_RunWorkerCompleted);
            // 
            // KnwSavebackgroundWorker
            // 
            this.KnwSavebackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KnwSavebackgroundWorker_DoWork);
            this.KnwSavebackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.KnwSavebackgroundWorker_RunWorkerCompleted);
            // 
            // IndexPublishbackgroundWorker
            // 
            this.IndexPublishbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.IndexPublishbackgroundWorker_DoWork);
            this.IndexPublishbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.IndexPublishbackgroundWorker_RunWorkerCompleted);
            // 
            // CheckGUIDbackgroundWorker
            // 
            this.CheckGUIDbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CheckGUIDbackgroundWorker_DoWork);
            this.CheckGUIDbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckGUIDbackgroundWorker_RunWorkerCompleted);
            // 
            // getSimilarDocsbackgroundWorker
            // 
            this.getSimilarDocsbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getSimilarDocsbackgroundWorker_DoWork);
            this.getSimilarDocsbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getSimilarDocsbackgroundWorker_RunWorkerCompleted);
            // 
            // timersimilarDocs
            // 
            this.timersimilarDocs.Interval = 1000;
            this.timersimilarDocs.Tick += new System.EventHandler(this.timersimilarDocs_Tick);
            // 
            // rebuildKnwbackgroundWorker
            // 
            this.rebuildKnwbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.rebuildKnwbackgroundWorker_DoWork);
            this.rebuildKnwbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.rebuildKnwbackgroundWorker_RunWorkerCompleted);
            // 
            // loggergroupBox
            // 
            this.loggergroupBox.Controls.Add(this.verbositycomboBox);
            this.loggergroupBox.Controls.Add(this.verbositylabel);
            this.loggergroupBox.Location = new System.Drawing.Point(333, 535);
            this.loggergroupBox.Name = "loggergroupBox";
            this.loggergroupBox.Size = new System.Drawing.Size(200, 41);
            this.loggergroupBox.TabIndex = 21;
            this.loggergroupBox.TabStop = false;
            this.loggergroupBox.Text = "Logger";
            // 
            // verbositycomboBox
            // 
            this.verbositycomboBox.FormattingEnabled = true;
            this.verbositycomboBox.Items.AddRange(new object[] {
            "Quiet",
            "Detailed"});
            this.verbositycomboBox.Location = new System.Drawing.Point(66, 14);
            this.verbositycomboBox.Name = "verbositycomboBox";
            this.verbositycomboBox.Size = new System.Drawing.Size(121, 21);
            this.verbositycomboBox.TabIndex = 1;
            this.verbositycomboBox.TextChanged += new System.EventHandler(this.verbositycomboBox_TextChanged);
            // 
            // verbositylabel
            // 
            this.verbositylabel.AutoSize = true;
            this.verbositylabel.Location = new System.Drawing.Point(11, 19);
            this.verbositylabel.Name = "verbositylabel";
            this.verbositylabel.Size = new System.Drawing.Size(49, 13);
            this.verbositylabel.TabIndex = 0;
            this.verbositylabel.Text = "verbosity";
            // 
            // GetTblTotalLinesbackgroundWorker
            // 
            this.GetTblTotalLinesbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetTblTotalLinesbackgroundWorker_DoWork);
            this.GetTblTotalLinesbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetTblTotalLinesbackgroundWorker_RunWorkerCompleted);
            // 
            // CheckRSSReadbackgroundWorker
            // 
            this.CheckRSSReadbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CheckRSSReadbackgroundWorker_DoWork);
            this.CheckRSSReadbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckRSSReadbackgroundWorker_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.DatagroupBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.TblContentgroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(867, 418);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.refreshData);
            this.panel1.Controls.Add(this.fetchSize);
            this.panel1.Controls.Add(this.AddDocFromDBbutton);
            this.panel1.Controls.Add(this.FetchSizelabel);
            this.panel1.Controls.Add(this.trackBar);
            this.panel1.Controls.Add(this.fetchStart);
            this.panel1.Controls.Add(this.tableData);
            this.panel1.Controls.Add(this.backward);
            this.panel1.Controls.Add(this.forward);
            this.panel1.Location = new System.Drawing.Point(6, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 393);
            this.panel1.TabIndex = 1;
            // 
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(RSSRTReader.MainForm);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(919, 578);
            this.Controls.Add(this.loggergroupBox);
            this.Controls.Add(this.KnwgroupBox);
            this.Controls.Add(this.mARCtab);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RSS Real Time Reader mARC";
            this.connection.ResumeLayout(false);
            this.connection.PerformLayout();
            this.propertiesBox.ResumeLayout(false);
            this.RefreshPropertiesBox.ResumeLayout(false);
            this.RefreshPropertiesBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PropertiesdataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedPicture)).EndInit();
            this.Tables.ResumeLayout(false);
            this.Tables.PerformLayout();
            this.knwSavegroupBox.ResumeLayout(false);
            this.knwSavegroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.log.ResumeLayout(false);
            this.log.PerformLayout();
            this.mARCtab.ResumeLayout(false);
            this.mARCServerPage.ResumeLayout(false);
            this.mARCServertab.ResumeLayout(false);
            this.ConnexionPage.ResumeLayout(false);
            this.CustomDocPage.ResumeLayout(false);
            this.LearngroupBox.ResumeLayout(false);
            this.URLgroupBox.ResumeLayout(false);
            this.URLgroupBox.PerformLayout();
            this.ContentgroupBox.ResumeLayout(false);
            this.TitlegroupBox.ResumeLayout(false);
            this.TitlegroupBox.PerformLayout();
            this.TablesPage.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.createPage.ResumeLayout(false);
            this.AutoRebuildgroupBox.ResumeLayout(false);
            this.AutoRebuildgroupBox.PerformLayout();
            this.rebuildgroupBox.ResumeLayout(false);
            this.rebuildgroupBox.PerformLayout();
            this.rebuildParamgroupBox.ResumeLayout(false);
            this.rebuildParamgroupBox.PerformLayout();
            this.rebuildFieldsgroupBox.ResumeLayout(false);
            this.rebuildFieldsgroupBox.PerformLayout();
            this.rebuildrangegroupBox.ResumeLayout(false);
            this.rebuildrangegroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.selectTablePage.ResumeLayout(false);
            this.FieldsgroupBox.ResumeLayout(false);
            this.InstancesgroupBox.ResumeLayout(false);
            this.VisualizePage.ResumeLayout(false);
            this.TblContentgroupBox.ResumeLayout(false);
            this.DatagroupBox.ResumeLayout(false);
            this.DatagroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableData)).EndInit();
            this.LogServerPage.ResumeLayout(false);
            this.RSSFeedsPage.ResumeLayout(false);
            this.visualizeFeedgroupBox.ResumeLayout(false);
            this.FeedsgroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FeedsTable)).EndInit();
            this.RSSFeedsTrackingPage.ResumeLayout(false);
            this.logServergroupBox.ResumeLayout(false);
            this.RefreshRategroupBox.ResumeLayout(false);
            this.RefreshRategroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RRtrackBar)).EndInit();
            this.FeedsSelectiongroupBox1.ResumeLayout(false);
            this.ContextuelTrackingtabPage.ResumeLayout(false);
            this.TuninggroupBox.ResumeLayout(false);
            this.TuninggroupBox.PerformLayout();
            this.similardDocsgroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.similarDocsdataGridView)).EndInit();
            this.trackingDocsgroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.timerSimilarDocsgroupBox.ResumeLayout(false);
            this.timerSimilarDocsgroupBox.PerformLayout();
            this.SMTPConfigurationTabPage.ResumeLayout(false);
            this.UserDataGourpBox.ResumeLayout(false);
            this.UserDataGourpBox.PerformLayout();
            this.SMTPServergroupBox.ResumeLayout(false);
            this.SMTPServergroupBox.PerformLayout();
            this.KnwgroupBox.ResumeLayout(false);
            this.loggergroupBox.ResumeLayout(false);
            this.loggergroupBox.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void IPtext_TextChanged(object sender, EventArgs e)
        {
            if (!this._connector.isConnected)
            {
                this.IP = this.IpText.Text;
                this._connector.IP = this.IP;
            }
        }


        private void PortText_TextChanged(object sender, EventArgs e)
        {
            if (!this._connector.isConnected)
            {
                this.Port = int.Parse(this.PortText.Text);
                this._connector.Port = this.Port;
            }
        }



        public static string RemoveAllCR(string str)
        {
            string source = str;
            while (source.Contains<char>('\n') || source.Contains<char>('\r'))
            {
                source = source.Replace("\n", "").Replace("\r", "");
            }
            return source;
        }

        public static string UnicodetoIso(string _str)
        {
            Encoding dstEncoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding srcEncoding = Encoding.UTF8;
            byte[] bytes = srcEncoding.GetBytes(_str);
            byte[] buffer2 = Encoding.Convert(srcEncoding, dstEncoding, bytes);
            return dstEncoding.GetString(buffer2);
        }


        private void randomUpdatebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

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
            strB.AppendLine("<tr>");
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

        private void PropertiesdataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (PropertiesdataGridView.SelectedCells.Count == 0)
                return;

            if (e.Control && e.KeyCode.Equals(Keys.C)) // Cntrl + C pour copier le contenu dans le clipboard 
            {

                StringBuilder b = DataGridToHMTLTable(PropertiesdataGridView);

                Clipboard.SetDataObject(b.ToString());
                Clipboard.SetText(b.ToString());
            }
        }

        private void clearLog_MouseClick(object sender, MouseEventArgs e)
        {
            logTextBox.Clear();
        }

        private void autoRefreshTimer_Tick(object sender, EventArgs e)
        {

            if (_connector == null || _connector.sock == null || !_connector.sock.Connected || !autoRefresh.Checked || autoRefreshPropertiesbackgroundWorker.IsBusy)
                return;

            _connector.Lock();
            autoRefreshPropertiesbackgroundWorker.RunWorkerAsync();
        }

        private void autoRefreshPropertiesbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = true;
            _connector.GetProperties();
        }

        private void autoRefreshPropertiesbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            int first_index = PropertiesdataGridView.FirstDisplayedScrollingRowIndex;
            PropertiesdataGridView.Rows.Clear();
            string[] row = new string[2];
            for (int i = 0; i < _connector._properties.Length; i++)
            {
                row[0] = _connector._properties[i];
                row[1] = _connector._propertieValue[i];
                PropertiesdataGridView.Rows.Add(row);
            }
            PropertiesdataGridView.FirstDisplayedScrollingRowIndex = first_index;
            PropertiesdataGridView.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            interval.Text = trackBar1.Value.ToString();
            autoRefreshTimer.Interval = trackBar1.Value;
        }

        private void updateLine_MouseClick(object sender, MouseEventArgs e)
        {
            if (updateLinebackgroundWorker.IsBusy)
                return;
            if (tablesInstances.SelectedIndices.Count == 0)
                return;
            if (tableData.Rows == null || tableData.Rows.Count == 0 || tableData.SelectedCells.Count != 1)
                return;
            if (!(tableData.Columns[0].HeaderText.Equals("RowId")))
                return;

            Int32 col = tableData.SelectedCells[0].ColumnIndex;
            string field = tableData.Columns[col].HeaderText;


            // table  rowid champ valeur
            int buffersize = 0;
            _connector.Lock();
            updateLinebackgroundWorker.RunWorkerAsync(new string[] { (string)tablesInstances.SelectedItem, (string)tableData.SelectedCells[0].Value, field, KMString.NormalizeString(lineContent.Text, ref buffersize) });

        }

        private void updateLinebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] p = (string[])e.Argument;
            _connector._DirectExecute = true;
            // on fait l'update
            _connector.Table().Update(p[0], p[1], new string[] { p[2] }, new string[] { p[3] });
        }

        private void updateLinebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
        }

        private void DisplayLineContentbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = true;
            string[] p = (string[])e.Argument;
            string content = "";
            if (p[0].ToLower().Equals("string") || p[0].ToLower().Equals("bin"))
            {
                this._connector.Table().ReadBlock(this.instances[this.current_table_id], p[2], p[1], "1", "4096");
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
            else if (p[1].ToLower().Equals("knw_abstract"))
            {
                _connector.Session().DocToContext(p[2], "false");
            }
            else
            {
                _connector.Table().ReadLine(this.instances[this.current_table_id], p[2], new string[] { p[1] });
                string[] strArray4 = this._connector.GetDataByName(p[1].ToLower(), -1);
                if (strArray4 != null)
                {
                    content = strArray4[0];
                }
                else
                {
                    content = " ";
                }
            }

            e.Result = content;
        }

        private void DisplayLineContentbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(28605);
            lineContent.Text = encoding.GetString(encoding.GetBytes((string)e.Result));
            trackBar.Enabled = true;
        }

        private void GetTblDatabackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            this._connector._DirectExecute = true;

            if (((this.instances != null) && (this.instances.Length != 0)) && (this.tablesTotalLines[this.current_table_id] != 0))
            {
                string field;
                values.Clear();
                CheckedListBox.CheckedIndexCollection checkedIndices = this.tableStructure.CheckedIndices;
                if (checkedIndices.Count != 0)
                {
                    int[] p = (int[])e.Argument;
                    //float num7 = 0f;
                    int index = 0;
                    string[] row;
                    string[] strArray4;
                    int k;
                    // on vérifie si la sélection contient "knw_abstract"
                    bool contains = false;
                    for (k = 0; k < checkedIndices.Count; k++)
                    {
                        index = checkedIndices[k];
                        if (names[index].ToLower().Equals("knw_abstract"))
                        {
                            contains = true;
                            break;
                        }
                    }
                    int count = checkedIndices.Count + 1 ;
                    headers = new string[ count ];

                    headers[0] = "RowId";
                    for (k = 0; k < checkedIndices.Count; k++)
                    {
                        index = checkedIndices[k];
                        headers[k + 1] = names[index];
                    }

                    string[] newheaders = null;
                    if (contains)
                    {
                        newheaders = new string[checkedIndices.Count];
                        int j = 0;
                        for (int i = 0; i < count; i++)
                        {
                            if (!headers[i].ToLower().Equals("knw_abstract"))
                            {
                                newheaders[j++] = headers[i];
                            }
                        }
                    }

                    for (int j = p[0]; j < p[1]; j++)
                    {
                        row = new string[count];
                        if (!contains)
                        {
                            this._connector.Table().ReadLine(this.instances[this.current_table_id], j.ToString(), headers);
                            for (k = 0; k < count; k++)
                            {
                                field = headers[k];
                                if (!field.Equals("RowId"))
                                    field = field.ToLower();

                                strArray4 = this._connector.GetDataByName(field, -1);
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
                        }
                        else
                        {
                            _connector._DirectExecute = false;
                            _connector.OpenScript(null);
                            _connector.Table().ReadLine(this.instances[this.current_table_id], j.ToString(), newheaders);
                            _connector.Session().DocToContext( j.ToString(), "false");
                            _connector.Contexts().Fetch("100000", "1", "0");
                            _connector.ExecuteScript();
                            _connector._DirectExecute = true;
                            for (k = 0; k < count; k++)
                            {
                                field = headers[k];
                                if (!field.Equals("RowId") && !field.Equals("KNW_ABSTRACT") )
                                    field = field.ToLower();

                                if ( field.Equals("KNW_ABSTRACT") )
                                {
                                strArray4 = this._connector.GetDataByName("shape", -1);
                                }
                                else
                                {
                                    strArray4 = this._connector.GetDataByName(field, 0);
                                }
                                if (strArray4 != null && strArray4.Length > 0 )
                                {
                                    row[k] = strArray4[0];

                                    if ( field.Equals("KNW_ABSTRACT") )
                                    {
                                        row[k] += " ";
                                        for (int jj = 1; jj < strArray4.Length; jj++)
                                        {
                                            row[k] += strArray4[jj] + " ";
                                        }
                                    }

                                }
                                else
                                {
                                    row[k] = " ";
                                }
                            }
                            values.Add(row);

                        }
                        //num7 = (((float)(j - indexEnd)) / ((float)(num6 - indexEnd))) * 100f;
                        //GetTblDatabackgroundWorker.ReportProgress((int)num7);
                    }
                }
            }
        }

        /*
        private void GetTblDatabackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.refreshprogressBar.Value = e.ProgressPercentage;
        }
        */

        private void GetTblDatabackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            _connector.UnLock();

            this.tableData.Rows.Clear();
            this.tableData.Columns.Clear();
            CheckedListBox.CheckedIndexCollection checkedIndices = this.tableStructure.CheckedIndices;
            this.tableData.ColumnCount = checkedIndices.Count + 1;
            if (values.Count != 0)
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

            refreshData.Enabled = true;
            trackBar.Enabled = true;

            if (!backward.Enabled)
                backward.Enabled = true;

        }

        private void GetTableContent()
        {
            if (!GetTblDatabackgroundWorker.IsBusy)
            {
                refreshData.Enabled = false;

                int num = int.Parse(this.fetchStart.Text) + int.Parse(this.fetchSize.Text);

                if (num > int.Parse(currentTable_TotalLines) - 1)
                {
                    num = int.Parse(currentTable_TotalLines) - 1;
                }
                _connector.Lock();
                int[] p = new int[] { int.Parse(this.fetchStart.Text), num };
                GetTblDatabackgroundWorker.RunWorkerAsync(p);
            }
        }

        private void refreshData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                GetTableContent();

            }
        }


        private void fetchStart_TextChanged(object sender, EventArgs e)
        {
            int start = -1;
            if (  !int.TryParse(this.fetchStart.Text, out start  ) )
                return;

            int num = start;
            if (num < 0)
            {
                num = 0;
                this.fetchStart.Text = "0";
                this.fetchStart.Refresh();
            }
            if (num > trackBar.Maximum)
            {
                trackBar.Maximum = num + Int32.Parse(fetchSize.Text);
            }

            this.trackBar.Value = num;
        }

        private void forward_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (GetTblTotalLinesbackgroundWorker.IsBusy)
                    return;

                fetchStart.Text = (int.Parse(fetchStart.Text) + int.Parse(fetchSize.Text)).ToString();
                forward.Enabled = false;
                _connector.Lock();
                GetTblTotalLinesbackgroundWorker.RunWorkerAsync();

            }
        }

        private void backward_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (GetTblDatabackgroundWorker.IsBusy)
                    return;

                int num = int.Parse(this.fetchStart.Text) - int.Parse(this.fetchSize.Text);
                if (num < 1)
                    num = 1;

                backward.Enabled = false;

                    this.fetchStart.Text = num.ToString();
                    _connector.Lock();
                    GetTblDatabackgroundWorker.RunWorkerAsync(new int[] { int.Parse(this.fetchStart.Text), num + int.Parse(this.fetchSize.Text) });
            }
        }

        private void GetTblInstancesbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = true;

            this._connector.Table().GetInstances("1","-1");
            instances = _connector.GetDataByName("tables", -1);
            if ( this.instances == null || instances.Length == 0 )
            {
                return;
            }
            tablesTotalLines = new UInt64[this.instances.Length];
            //float progressf = 0;
            for (int i = 0; i < this.instances.Length; i++)
            {
                this._connector.Table().GetLines(this.instances[i]);
                string[] dataByName = this._connector.GetDataByName("lines", -1);
                if ((dataByName != null) && (dataByName.Length > 0))
                {
                    this.tablesTotalLines[i] = UInt64.Parse(dataByName[0]);
                }
                else
                {
                    this.tablesTotalLines[i] = 0;
                }
                // progressf = (float)i / (float)instances.Length * 100;
                // GetTblInstancesbackgroundWorker.ReportProgress((Int32)progressf);
            }
        }

        /*
        private void GetTblInstancesbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        */
        private void GetTblInstancesbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
            tablesInstances.Items.Clear();
            rebuildTotextBox.Text = "";
            TotalArticles = 0;
            currentTable_TotalLines = "0";

            if ((this.instances != null) && (this.instances.Length != 0))
            {
                Array.Sort<string>(this.instances);

                foreach (string str in this.instances)
                {
                    tablesInstances.Items.Add(str);
                }

                if (instances.Length != 0)
                {
                    TotalArticles = (UInt64)tablesTotalLines[0];
                    currentTable_TotalLines = tablesTotalLines[0].ToString();
                    rebuildTotextBox.Text = (tablesTotalLines[0] - 1).ToString();
                    tablesInstances.SelectedIndex = 0;
                }

            }

            if (tablesInstances.Items.Count != 0)
            {
                current_table = (string)tablesInstances.Items[0];
                current_table_id = 0;
                if (GetTblFieldsbackgroundWorker.IsBusy)
                    return;
                _connector.Lock();
                GetTblFieldsbackgroundWorker.RunWorkerAsync();
            }

        }

        private void GetTblFieldsbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = true;
            int index = current_table_id;
            //float progressf = 0;
            this._connector.Table().GetStructure(this.instances[index]);
            this.names = this._connector.GetDataByName("name", -1);
            if ((this.names != null) && (this.names.Length != 0))
            {
                this.types = this._connector.GetDataByName("type", -1);
               
                /*
                if (types != null && types.Length > 0)
                {
                    
                    for (int i = 0; i < types.Length; i++)
                    {
                        progressf = (float)i / (float)types.Length * 100;
                        GetTblFieldsbackgroundWorker.ReportProgress((Int32)progressf);
                    }
                     
                }
                 */
            }
        }
        /*
        private void GetTblFieldsbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }
         * */
        private void GetTblFieldsbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            if ((types != null) && (types.Length != 0))
            {
                this.tableStructure.Items.Clear();
                for (int i = 0; i < this.names.Length; i++)
                {
                    this.tableStructure.Items.Add(this.types[i] + "    " + this.names[i]);
                }

                this.trackBar.Maximum = (Int32) this.tablesTotalLines[current_table_id];
                this.trackBar.Value = 0;
            }

            tablesInstances.Enabled = true;
        }

        private void GetTblStructurebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void tablesInstances_MouseClick(object sender, MouseEventArgs e)
        {
            if ((this._connector != null) && this._connector.isConnected)
            {
                tablesInstances.Enabled = false;
                int selectedIndex = this.tablesInstances.SelectedIndex;
                if (selectedIndex != -1)
                {
                    current_table_id = selectedIndex;
                    currentTable_TotalLines = tablesTotalLines[current_table_id].ToString();
                    this.current_table = (string) this.tablesInstances.Items[selectedIndex];
                    if (e.Button == MouseButtons.Left)
                    {

                        if (GetTblFieldsbackgroundWorker.IsBusy)
                        {
                            tablesInstances.Enabled = true;
                            return;
                        }
                        _connector.Lock();
                        GetTblFieldsbackgroundWorker.RunWorkerAsync();
                    }
                }
            }
        }

        private void tableData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if ( tableData.SelectedCells.Count == 0 )
                    return;

                Int32 col = tableData.SelectedCells[0].ColumnIndex;
                string field = tableData.Columns[col].HeaderText;
                string type = "";
                for (int i = 0; i < types.Length; i++)
                {
                    string tmp = this.types[i] + "    " + field;
                    type = (string)tableStructure.Items[i];
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
                string row = (Int32.Parse(fetchStart.Text) + tableData.SelectedCells[0].RowIndex).ToString();

                if (field.ToLower().Equals("knw_abstract"))
                {
                    System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(28605);
                    lineContent.Text = encoding.GetString(encoding.GetBytes( (string) tableData.Rows[tableData.SelectedCells[0].RowIndex ].Cells[col].Value ));
                    trackBar.Enabled = true;
                    return;
                }

                if (DisplayLineContentbackgroundWorker.IsBusy)
                    return;
                trackBar.Enabled = false;

                _connector.Lock();
                DisplayLineContentbackgroundWorker.RunWorkerAsync(new string[] { type, field, row });
            }
        }



        private void AddaFeed()
        {
            NewRSSFeedForm f = new NewRSSFeedForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FeedsTable.Rows.Add(new string[] { f.GetTitle(), f.GetURL() });

                int id = Program.Config.Count;
                string title = f.GetTitle();
                string url = f.GetURL();
                string file = title.Replace(" ", "_").ToLower() + ".rss";

                Misc.Config cfg = new Misc.Config(id, title, url, file);

                Program.Config.Add(cfg);

                /*
                int count = Program.Config.Count;
                this.RSSFeed.Add(Reader.getFeed(Program.Config[Count - 1]));
                */
                Program.SaveConfig();

            // maj des feeds sélectionnés

                FeedscheckedListBox.Items.Add(f.GetTitle(), false);

            }
        }
        private void addFeedbutton_MouseClick(object sender, MouseEventArgs e)
        {
            AddaFeed();
        }

        private void RemoveFeedbutton_Click(object sender, EventArgs e)
        {
            FeedsTable.Enabled = false;
            FeedscheckedListBox.Enabled = false;
            int rows = FeedsTable.Rows.GetRowCount(DataGridViewElementStates.Selected);
                while ( rows > 0)
                {
                    FeedscheckedListBox.Items.Remove( (string) FeedsTable.SelectedRows[0].Cells[0].Value );
                    FeedsTable.Rows.Remove(FeedsTable.SelectedRows[0]);
                    FeedsTable.Refresh();
                    rows--;
                }
            FeedscheckedListBox.Refresh();
            FeedscheckedListBox.Enabled = true;
            FeedsTable.Enabled = true;
        }

        private void modifyFeedbutton_Click(object sender, EventArgs e)
        {
            if (FeedsTable.SelectedRows.Count == 1)
            {

                NewRSSFeedForm f = new NewRSSFeedForm();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    int id = this.FeedsTable.SelectedRows[0].Index;
                    FeedsTable.SelectedRows[0].Cells[0].Value = f.GetTitle();
                    FeedsTable.SelectedRows[0].Cells[1].Value = f.GetURL();

                    string title = f.GetTitle();
                    string url = f.GetURL();
                    string file = title.Replace(" ", "_").ToLower() + ".rss";

                    Program.Config[id].Title = title;
                    Program.Config[id].URL = url;
                    Program.Config[id].File = file;

                    Program.SaveConfig();

                    FeedscheckedListBox.Items[id] = title;

                    FeedsTable.Refresh();
                }
            }
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            this.fetchStart.Text = this.trackBar.Value.ToString();
            this.fetchStart.Refresh();

        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_connector.isConnected)
                return;

            if (!GetTblTotalLinesbackgroundWorker.IsBusy)
            {
                trackBar.Enabled = false;

                _connector.Lock();
                fetchStart.Text = trackBar.Value.ToString();
                GetTblTotalLinesbackgroundWorker.RunWorkerAsync();
            }
        }

        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int error = 0;
            try
            {
                error = Program.GetConfig();
            }
            catch (Exception exc )
            {
                Logger.Instance.Log("MainForm:chargerToolStripMenuItem", String.Format("exception message was: {0}. \n Stack trace: {1}\n", exc.Message, exc.StackTrace));
                switch (error)
                {
                    case 0:
                        logTextBox.AppendText(" configuration file loaded. \n");
                        break;
                    case -1:
                        logTextBox.AppendText("config file is empty. consider adding some feeds. \n");
                        break;
                    case -2:
                        logTextBox.AppendText("could not create config file. \n");
                        break;
                    case -3:
                        logTextBox.AppendText(" configuration file did not exist. New configuration file created. \n");
                        break;
                    case -4:
                        logTextBox.AppendText("invalid configuration xml file. please delete it. \n");
                        break;
                    case -5:
                        logTextBox.AppendText("could not create config directory. \n");
                        break;
                    case -6:
                        logTextBox.AppendText("could not create config file. \n");
                        break;
                }
            }

        }

        private void FeedsTable_MouseUp(object sender, MouseEventArgs e)
        {


            if (FeedsTable.SelectedRows.Count == 1 )
            {
                if (FeedsTable.SelectedRows[0].Cells[1].Value != null)
                {
                    XslCompiledTransform objTransform = new XslCompiledTransform();
                    StringWriter objStream = new StringWriter();
                    // load xml default style sheet
                    try
                    {
                        objTransform.Load("xml.xsl");
                    }
                    catch (Exception ee)
                    {
                        webBrowser1.DocumentText = ee.Message;
                    }
                    try
                    {
                        objTransform.Transform((string)FeedsTable.SelectedRows[0].Cells[1].Value, null, objStream);
                        objStream.Close();
                        string html = objStream.ToString();
                        // load html page
                        webBrowser1.DocumentText = html;
                    }
                    catch (Exception wex)
                    {
                        webBrowser1.DocumentText = wex.Message;
                    }

                }
                else
                {
                    AddaFeed();
                }
            }
        }


        private void RRtextBox_TextChanged(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty( RRtextBox.Text) || int.Parse( RRtextBox.Text ) <= 0 )
                return;


            if (RefreshRateUnitcomboBox.Text == "milliseconde")
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text);
            }
            else if (RefreshRateUnitcomboBox.Text == "seconde")
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 1000;
            }
            else if (RefreshRateUnitcomboBox.Text == "minute")
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 60000;
            }
            else if (RefreshRateUnitcomboBox.Text == "heure")
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 3600000;
            }
            else if (RefreshRateUnitcomboBox.Text == "jour")
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 86400000;
            }

        }

        private void RRtrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            RRtextBox.Text = RRtrackBar.Value.ToString();
        }

        private void RRtextBox_KeyUp(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(RRtextBox.Text) || int.Parse(RRtextBox.Text) <= 0 )
            {
                return;
            }

            if (int.Parse(RRtextBox.Text) + 10000 > RRtrackBar.Maximum )
            {
                RRtrackBar.Maximum = int.Parse(RRtextBox.Text) + 10000;
                RRtrackBar.Minimum = int.Parse(RRtextBox.Text) - 10000;
            }
            if (RRtrackBar.Minimum < 0)
                RRtrackBar.Minimum = 0;
        }

        private void updateFeedstimer_Tick(object sender, EventArgs e)
        {
            if ( UpdateFeedsbackgroundWorker.IsBusy )
                return;

            updateFeedstimer.Enabled = false;
            FeedscheckedListBox.Enabled = false;
            List<string> thelist = new List<string>();

            // on récupère les feeds sélectionnés
            for ( int i= 0; i < FeedscheckedListBox.CheckedIndices.Count;i++)
            {
               thelist.Add( (string) FeedscheckedListBox.Items[ FeedscheckedListBox.CheckedIndices[i] ] );
            }

            // on vide les new articles du buffer
            Reader.newArticles.RemoveRange(0, Reader.newArticles.Count);

            _connector.Lock();
            UpdateFeedsbackgroundWorker.RunWorkerAsync( thelist );

        }


        private void UpdateFeedsbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
          List<string> thelist = (List<string> ) e.Argument ;
        //  UpdateFeeds(thelist);
          GetTheFeeds(thelist);
        }

        private void UpdateFeedsbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
            if (RRActifcheckBox.Checked)
            {
                updateFeedstimer.Enabled = true;
            }
            else
            {
                updateFeedstimer.Enabled = false;
            }
            FeedscheckedListBox.Enabled = true;
            if (GetTblTotalLinesbackgroundWorker.IsBusy)
                return;

            _connector.Lock();
            GetTblTotalLinesbackgroundWorker.RunWorkerAsync();
        }

        private void GetTheFeeds(List<string> thelist)
        {
            WebClient wc = new WebClient();
            string feedS ="";
            XDocument doc;
            List<XElement> elements;
            Encoding srcEncoding = Encoding.UTF8;
            foreach (RSSRTReader.Misc.Config feed in Program.Config)
            {
                if (thelist.Contains(feed.Title))
                {
                    try
                    {
                        wc.Encoding = System.Text.Encoding.UTF8; 
                        feedS = wc.DownloadString(feed.URL);
                        string contentEncoding = wc.ResponseHeaders["charset"];
                        if (!string.IsNullOrEmpty(contentEncoding))
                        {
                            wc.Encoding = System.Text.Encoding.GetEncoding(contentEncoding);
                            srcEncoding = wc.Encoding;
                            feedS = wc.DownloadString(feed.URL);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Log("GetTheFeeds", e.Message);
                        continue;
                    }
                    try
                    {
                        Encoding iso = Encoding.GetEncoding("ISO-8859-15");
                        feedS = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(feedS)));
                        doc = XDocument.Parse(feedS);
                        elements = doc.Root.Element("channel").Elements("item").ToList();
                        ParseHTML(elements);
                    }
                    catch (Exception eee)
                    {
                        Logger.Instance.Log("GetTheFeeds",eee.Message);
                    }

                }
            }

        }

        private void ParseHTML(List<XElement>  items)
        {
            HtmlAgilityPack.HtmlDocument wdoc = new HtmlAgilityPack.HtmlDocument() ;
            string sgid;
            string uri;
            string title, article="";
            Encoding iso = Encoding.GetEncoding("ISO-8859-15"); 
            Encoding srcEncoding = iso;
            string[] result;

            string[] v = new string[10];
            v[0] = "guid";
            v[2] = "title";
            v[4] = "content";
            v[6] = "link";
            v[8] = "pubDate";

            string description = "";
            HtmlNode articleNode = null, encodingNode =null;
            _connector._DirectExecute = false;

/*
            var htmlWeb = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.GetEncoding(28591),
            };*/

            System.Net.WebClient client = new System.Net.WebClient();

            DateTime dt;
            MD5 md5 = MD5.Create();
            Guid res;
            byte[] hash;
            UInt64 gid;
            char[] arr;
             foreach (XElement item in items)
            {
                sgid = item.Element("guid").Value;

                hash = md5.ComputeHash(Encoding.Default.GetBytes(sgid));
                res = new Guid(hash);
                gid = (UInt32)res.GetHashCode();
                // reverse string
                arr = sgid.ToCharArray();
                Array.Reverse(arr);
                sgid = new string(arr);
                hash = md5.ComputeHash(Encoding.Default.GetBytes(sgid));
                res = new Guid(hash);
                gid += (UInt32)res.GetHashCode();
                sgid = gid.ToString();

                // le guid existe t il dans le B-TREE ?
                _connector.OpenScript(null);
                _connector.Table().Select(current_table, "new", "guid", "=", sgid , " ");
                _connector.Session().Clear("results");
                _connector.ExecuteScript();
                result = _connector.GetDataByName("result_count", 0);
                if (result == null || Int32.Parse(result[0]) != 0) // le guid existe déjà
                {
                    continue;
                }
                dt = Convert.ToDateTime(item.Element("pubDate").Value);

                uri = item.Element("link").Value;

                description = item.Element("description").Value;
                // on extrait le texte seul de la balise description
                //
                wdoc.LoadHtml(description);
                description = wdoc.DocumentNode.InnerText;
                description = HtmlEntity.DeEntitize(description);
                //

           //     wdoc = htmlWeb.Load( uri );
           //     srcEncoding = wdoc.Encoding;

                wdoc.LoadHtml(client.DownloadString(uri));
                srcEncoding = wdoc.Encoding;
                //Stream stream = client.OpenRead(uri);

  /*              if (srcEncoding == Encoding.UTF8)
                {
                    wdoc.Load(stream , Encoding.UTF8);
                }
   * */

                encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@content='text/html; charset=UTF-8']");
                if (encodingNode != null)
                {
                    srcEncoding = Encoding.UTF8;
                    client.Encoding = srcEncoding;
                    wdoc.Load(client.OpenRead(uri), Encoding.UTF8);
                }
                else if ( (encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@content='text/html; charset=utf-8']") ) != null )
                {
                    srcEncoding = Encoding.UTF8;
                    client.Encoding = srcEncoding;
                    wdoc.Load(client.OpenRead(uri), Encoding.UTF8);
                }
                else if ((encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@charset='UTF-8']")) != null)
                {
                    srcEncoding = Encoding.UTF8;
                    client.Encoding = srcEncoding;
                    wdoc.Load(client.OpenRead(uri), srcEncoding);
                }

                // on enlève tous les commentaires de la page HTML
                foreach (HtmlNode comment in wdoc.DocumentNode.SelectNodes("//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }

                // pages web encodées en général en srcEncoding
                // on change l'encoding vers ISO8859-15
                title = wdoc.DocumentNode.SelectSingleNode("//title").InnerText;
                title = HtmlEntity.DeEntitize(title);
                
                title = iso.GetString( Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(title)) );

                if (title.ToLower().Contains("afp:")) // flux afp
                {
                    title = HtmlEntity.DeEntitize( wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value );
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:description']").Attributes["expr:content"].Value);
                    articleNode = wdoc.DocumentNode.SelectSingleNode("//p[@class=\"hn-byline\"]");
                    article = "";
                    articleNode = articleNode.NextSibling.NextSibling;
                    while (articleNode.Name != null && articleNode.Name == "p")
                    {    
                        article += HtmlEntity.DeEntitize(articleNode.InnerText);
                        articleNode = articleNode.NextSibling; // on passe au suivant <p>
                    }

                }
                else if (title.ToLower().Contains("reuters"))
                {
                    srcEncoding = Encoding.UTF8;
                    wdoc.Load(client.OpenRead(uri), srcEncoding);
                    title = wdoc.DocumentNode.SelectSingleNode("//title").InnerText;
                    title = HtmlEntity.DeEntitize(title);

                    if ( title.Contains("| Reuters") )
                    {
                        title = title.Substring(0, title.IndexOf("| Reuters") - 1 );
                    }
                    while(title.Contains('*'))
                    {
                        title = title.Replace('*',' ');
                    }
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value);

                    articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@id='resizeableText']");
                    if (articleNode != null)
                    {
                        try
                        {
                            article = HtmlEntity.DeEntitize(articleNode.InnerText);
                        }
                        catch (Exception eee)
                        {
                            article = articleNode.InnerText;
                            Logger.Instance.Log("ParseHTML", eee.ToString());
                        }
                    }
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//span[@itemprop='articleBody']")) != null)
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@itemprop='description']").Attributes["content"].Value);
                    description = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(description)));
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@itemprop='articleBody']")) != null)
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value);
                    description = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(description)));
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@id='story_content']")) != null) // bloomberg
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value);
                    title = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value);
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@class='contentpage currentpage']")) != null) // The Times
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value);
                    title = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value);
                }
                else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@class='story-body linkmonde']")) != null) //courrier international
                {
                    article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value);
                    title = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value);
                }
                else
                {
                    articleNode = wdoc.GetElementbyId("articleBody");

                    if (articleNode != null) // leMonde RSS ?
                    {
                        article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    }
                    else if ((articleNode = wdoc.GetElementbyId("mediaarticlebody")) != null)//  yahoo RSS ?
                    {
                        // on récupère le bon titre !!!
                        title = wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value;

                        description = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//meta[@property='og:description']").Attributes["content"].Value);

                        title = HtmlEntity.DeEntitize(title);
                        title = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(title)));

                        description = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(description)));

                        article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    }
                    else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@class='entry-content']")) != null) // blog leMonde
                    {
                        title = HtmlEntity.DeEntitize(wdoc.DocumentNode.SelectSingleNode("//h1[@class='entry-title']").InnerText);
                        article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    }
                    else if ((articleNode = wdoc.DocumentNode.SelectSingleNode("//div[@itemprop='description']")) != null)
                    {
                        article = HtmlEntity.DeEntitize(articleNode.InnerText);
                    }
                }
                article = description + " " + article;// on concatène la description et l'article
                title = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(title)));
                article = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(article)));
                //description = iso.GetString(Encoding.Convert(srcEncoding, iso, srcEncoding.GetBytes(description)));
                // on insère l'article dans le serveur mARC et on l'apprend avec indexation
                _connector.OpenScript(null);

                v[1] = sgid;

                v[3] = title;

                v[5] = article;

                v[7] = uri;
        
                v[9] = dt.Year + ":"+dt.Month+":"+dt.Day+":"+dt.Hour+":"+dt.Minute+":"+dt.Second;

                _connector.Table().Insert(current_table, v);
                _connector.ExecuteScript();
                result = _connector.GetDataByName("RowId", -1);
                if (result == null)
                {
                    Logger.Instance.Log("ParseHTML", "ERROR from mARC Server : could not insert article '" + title + "'");
                    continue;
                }
                if (TotalArticles != 0 )
                {
                    if ( TotalArticles % updateIndexFrequency == 0 )
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcPublish();
                        _connector.ExecuteScript();
                    }
                    if ( TotalArticles % saveKnwFrequency == 0 )
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcSave();
                        _connector.ExecuteScript();
                    }
                    if ( TotalArticles % AutoRebuildFrequency == 0)
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcRebuild("content title", "0",(int.Parse(currentTable_TotalLines) -1).ToString(),"ref");
                        _connector.ExecuteScript();
                    }
                }
                TotalArticles++;
                if (TotalArticles != 0 )
                {
                    if (TotalArticles % updateIndexFrequency == 0)
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcPublish();
                        _connector.ExecuteScript();
                    }
                    if (TotalArticles % saveKnwFrequency == 0)
                    {
                        _connector.OpenScript(null);
                        _connector.Session().MarcSave();
                        _connector.ExecuteScript();
                    }

                }

                Logger.Instance.Log("ParseHTML","inserted a new article '" + title + "'");

                _connector.OpenScript(null);
                if (articleNode != null)
                {
                    _connector.Session().Store( article, "ranked",result[0]);
                }
                _connector.Session().Store( title, "ranked", result[0]);
                _connector.ExecuteScript();

            

            }
             tablesTotalLines[current_table_id] =  TotalArticles;
            _connector._DirectExecute = true;


        }



 /*       private void UpdateFeedsbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Reader.newArticles.Count > 0)
            {
                updateFeedstimer.Enabled = false;
                newArticles.AddRange(Reader.newArticles);
                if (!ParseHTMLbackgroundWorker.IsBusy)
                {
                    _connector.Lock();
                    deltaArticles = TotalArticles;
                    enable_updatefeeds = RRActifcheckBox.Checked;
                    ParseHTMLbackgroundWorker.RunWorkerAsync(newArticles);
                }
            }
            else
            {
                updateFeedstimer.Enabled = true;
                FeedscheckedListBox.Enabled = true;
            }
        }
*/

        private void UpdateFeeds(List<string>  thelist)
        {
            //Thread.Sleep(1 * 1000);

            Logger.Instance.Log("Main Form", "trying to update feeds");
            bool firsttime = true;


            this.RSSFeed = new List<RSSObject>();

            Logger.Instance.Log("FormView", "getting feeds");
            Logger.Instance.Log("FormView", "firsttime: " + firsttime.ToString());
            this.GetFeeds(thelist);
            Logger.Instance.Log("FormView", "finished");



            //Thread.Sleep(120 * 1000);

        }

                /// <summary>
        /// Get feeds URLs from config file.
        /// </summary>
        private void GetFeeds( List<string> thelist)
        {

            if (Program.Config.Count > 0)
            {
                try
                {
                    foreach (RSSRTReader.Misc.Config feed in Program.Config)
                    {
                        if ( thelist.Contains( feed.Title ) )
                        {
                            this.RSSFeed.Add(Reader.getFeed(feed));
                            Logger.Instance.Log("FormView", String.Format("{0} added from configuration file.", feed.URL));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log("MainForm GetFeeds", String.Format("error while adding feeds."));
                    Logger.Instance.Log("MainForm GetFeeds", String.Format("exception message was: {0}. \n Stack trace: {1}\n", ex.Message, ex.StackTrace));
                }

            }

        }




        private void RRActifcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ( current_table == null )
            {
                logServerrichTextBox.AppendText("Aucune Table n'est sélectionnée. Impossible d'activer le tracking." );
                return;
            }
            if (RRActifcheckBox.Checked)
            {
                updateFeedstimer.Enabled = true;
            }
            else
            {
                updateFeedstimer.Enabled = false;
            }
        }

        private void MaJIndextextBox_TextChanged(object sender, EventArgs e)
        {
            updateIndexFrequency = UInt64.Parse(MaJIndextextBox.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            saveKnwFrequency = UInt64.Parse(saveKnwtextBox.Text);
        }

        private void URLtextBox_Click(object sender, EventArgs e)
        {

        }

        private void URLtextBox_TextChanged(object sender, EventArgs e)
        {
            
            
            string URL = URLtextBox.Text;

            if (!RemoteFileExists(URL))
            {
                ContentrichTextBox.AppendText(" l'URL '" + URL + "' n'est pas disponible. Vérifiez la syntaxe et/ou la disponibilité de celle-ci. \n");
            }
            WebClient client = new WebClient();
            
            Stream stream = null;
            try
            {
                stream = client.OpenRead(new Uri(URL));
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message);
            }
            Encoding iso = Encoding.GetEncoding("ISO-8859-15");

            HtmlAgilityPack.HtmlDocument wdoc = new HtmlAgilityPack.HtmlDocument();
            try
            {
                wdoc.Load(stream, iso);
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message);
            }

            HtmlNode encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@content='text/html; charset=UTF-8']");
            if (encodingNode != null)
            {
                iso = Encoding.UTF8;
                client.Encoding = iso;
                wdoc.Load(client.OpenRead(URL), Encoding.UTF8);
            }
            else
            //   if ((encodingNode = wdoc.DocumentNode.SelectSingleNode("//meta[@charset='iso-8859-1']")) != null)
            {
                iso = Encoding.GetEncoding("ISO-8859-15");
                client.Encoding = iso;
                wdoc.Load(client.OpenRead(URL), iso);
            }

            // on enlève tous les commentaires de la page HTML
                foreach (HtmlNode comment in wdoc.DocumentNode.SelectNodes("//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }

                try
                {
                    ContentrichTextBox.Text = HtmlEntity.DeEntitize(wdoc.DocumentNode.InnerText);
                }
                catch (Exception ee)
                {
                    ContentrichTextBox.Text = wdoc.DocumentNode.InnerText;
                }
            
            HtmlNode titleNode = wdoc.DocumentNode.SelectSingleNode("//title");

            if (titleNode != null)
            {
                try
                {
                    titletextBox.Text = HtmlEntity.DeEntitize(titleNode.InnerText);
                }
                catch( Exception eee)
                {
                    titletextBox.Text = titleNode.InnerText;
                }
            }
        }

        ///
        /// Checks the file exists or not.
        ///
        /// The URL of the remote file.
        /// True : If the file exits, False if file not exists
        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        private void InsertAndLearnbutton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty( ContentrichTextBox.Text ) )
            {
                MessageBox.Show("saisissez ou charger d'abord un texte.");
                return;
            }
            if (!_connector.isConnected)
            {
                MessageBox.Show("connectez vous d'abord à un serveur mARC.");
                return;
            }
            if (String.IsNullOrEmpty(titletextBox.Text))
            {
                MessageBox.Show("tapez d'abord un titre.");
                return;
            }
            if (String.IsNullOrEmpty(URLtextBox.Text))
            {
                MessageBox.Show("choisissez un emplacement.");
                return;
            }
            //on calcul un guid
            byte[] hash;
            UInt64 gid;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.Default.GetBytes(URLtextBox.Text));
                Guid result = new Guid(hash);
                gid = (UInt32)result.GetHashCode();
                // reverse string s
                char[] arr = URLtextBox.Text.ToCharArray();
                Array.Reverse(arr);
                string s = new string(arr);
                hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                result = new Guid(hash);
                gid += (UInt32)result.GetHashCode();
            }


            _connector.Lock();
            string[] param = new string[5];
            param[0] = titletextBox.Text;
            param[1] = URLtextBox.Text;
            param[2] = gid.ToString(); // le guid
            param[3] = ContentrichTextBox.Text;
            DateTime dt = DateTime.Now;
            param[4] = dt.Year + ":" + dt.Month + ":" + dt.Day + ":" + dt.Hour + ":" + dt.Minute + ":" + dt.Second;

            current_custom_doc = new TrackingDocument();
            current_custom_doc.title = param[0];
            current_custom_doc.link = param[1];
            current_custom_doc.pubDate = param[4];


            InsertAndLearnbackgroundWorker.RunWorkerAsync(param);

        }

        private void InsertAndLearnbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] param = (string[])e.Argument;

            e.Result = "Ok";

            string[] v = new string[10];
            v[0] = "title";
            v[1] = param[0];
            v[2] = "link";
            v[3] = param[1];
            v[4] = "guid";
            v[5] = param[2];
            v[6] = "content";
            v[7] = param[3];
            v[8] = "pubDate";
            v[9] = param[4];

            // on entre en mode script
            //
            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Table().Select(current_table, "new", "guid", "=", param[2], " ");
            _connector.Session().Clear("results");
            _connector.ExecuteScript();


            string[] result = _connector.GetDataByName("ResultCount", 0);
            if (result == null || Int32.Parse(result[0]) != 0) // le guid existe déjà
            {
                e.Result = "GUIDExists";
                return;
            }
            _connector.OpenScript(null);
            _connector.Table().Insert(current_table, v);
            _connector.ExecuteScript();
            result = _connector.GetDataByName("RowId", -1);
            if (result != null)
            {
                // apprentissage
                _connector.OpenScript(null);
                _connector.Session().Store( param[3], "ranked",result[0]);
                _connector.Session().Store( param[0],"ranked", result[0]);
                _connector.Session().MarcPublish();
                _connector.ExecuteScript();

                current_custom_doc.dbID = result[0];
            }
            else
            {
                e.Result = " l'insertion a échoué. Aucun Apprentissage Possible. Log Serveur :'" + _connector.toReceive + "'"; 
            }
            //on sort du mode script
            _connector._DirectExecute = true;

        }
        private void InsertAndLearnbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // on récupère le nombre total de lignes de la table courante
            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Table().GetLines(current_table);
            _connector.ExecuteScript();
            _connector._DirectExecute = true;
            string[] results = _connector.GetDataByName("Lines",-1);
            if (results == null)
            {
                MessageBox.Show("InsertAndLearnbackgroundWorker_RunWorkerCompleted : ERREUR de SORTIE: impossible de récupérer le nombr de lignes de la table courante");
            }
            else
            {
                currentTable_TotalLines = results[0];
                rebuildTotextBox.Text = (Int32.Parse(currentTable_TotalLines) - 1).ToString() ;
            }

            if (((string)e.Result).Equals("GUIDExists"))
            {
                MessageBox.Show("Le document existe déjà dans la connaissance du serveur mARC.");
            }
            else if (!((string)e.Result).Equals("Ok"))
            {
                MessageBox.Show((string)e.Result);
                current_custom_doc = null;
            }
            else
            {
                trackingDocscheckedListBox.Items.Add(current_custom_doc);
                Program.SaveConfig();
            }
            _connector.UnLock();
        }


        private void saveKnwButton_Click(object sender, EventArgs e)
        {
            if (KnwSavebackgroundWorker.IsBusy)
                return;



            if (string.IsNullOrEmpty(knw_name))
            {
                MessageBox.Show("Aucun Objet Knowledge défini. Impossible.");
                return;
            }
            saveKnwButton.Enabled = false;
            _connector.Lock();
            KnwSavebackgroundWorker.RunWorkerAsync();
        }

        private void KnwSavebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Session().MarcSave();
            _connector.ExecuteScript();
            _connector._DirectExecute = true;
        }

        private void KnwSavebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            _connector.UnLock();
            saveKnwButton.Enabled = true;
        }

        private void publishKnwbutton_Click(object sender, EventArgs e)
        {
            if (IndexPublishbackgroundWorker.IsBusy)
                return;

            if (string.IsNullOrEmpty(knw_name))
            {
                MessageBox.Show("Aucun Objet Knowledge défini. Impossible.");
                return;
            }
            publishKnwbutton.Enabled = false;

            _connector.Lock();
            IndexPublishbackgroundWorker.RunWorkerAsync();
        }

        private void IndexPublishbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Session().MarcPublish();
            _connector.ExecuteScript();
            _connector._DirectExecute = true;
        }

        private void IndexPublishbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
            publishKnwbutton.Enabled = true;
        }

        private void CheckGUIDbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string gid = (string)e.Argument;
            e.Result = "Ok";

            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Table().Select(current_table, "new", "guid", "=", gid, " ");
            _connector.Session().Clear("results");
            _connector.ExecuteScript();
            _connector._DirectExecute = true;

            string[] result = _connector.GetDataByName("ResultCount", 0);
            if (result == null || Int32.Parse(result[0]) != 0) // le guid existe déjà
            {
                e.Result = "GUIDExists";
            }
        }

        private void CheckGUIDbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (((string)e.Result).Equals("GUIDExists"))
            {
                MessageBox.Show("Le document existe déjà dans la connaissance du serveur mARC.");
            }
            _connector.UnLock();
        }

        private void AddTrackingDocbutton_Click(object sender, EventArgs e)
        {
            mARCServertab.SelectedTab = CustomDocPage;
            mARCServertab.Refresh();
        }

        private void seuilsimilarDocstextBox_TextChanged(object sender, EventArgs e)
        {
            int r = -1;

            if (!int.TryParse(seuilsimilarDocstextBox.Text, out r))
                return;

            this.seuilsimilarDoc = r.ToString();
        }

        private void tailleSimilarDocstextBox_TextChanged(object sender, EventArgs e)
        {
            int r = -1;

            if (!int.TryParse(tailleSimilarDocstextBox.Text, out r))
                return;

            this.tailleSimilarDocs = tailleSimilarDocstextBox.Text;
        }

        private void maxSignatureSimilarDocstextBox_TextChanged(object sender, EventArgs e)
        {

            int r = -1;

            if (!int.TryParse(maxSignatureSimilarDocstextBox.Text, out r))
                return;

            maxSignatureSimilarDocs = maxSignatureSimilarDocstextBox.Text;
        }

        private void maxgentextBox_TextChanged(object sender, EventArgs e)
        {
            int r = -1;

            if (!int.TryParse(maxgentextBox.Text, out r))
                return;

            maxgenSimilarDocs = maxgentextBox.Text;
        }

        private void depthSimilarDocstextBox_TextChanged(object sender, EventArgs e)
        {
            int r = -1;

            if (!int.TryParse(depthSimilarDocstextBox.Text, out r))
                return;

            depthSimilarDocs = depthSimilarDocstextBox.Text;
        }

        private void timeSimilarDocscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (timerSimilarDocscheckBox.Checked)
            {
                timersimilarDocs.Enabled = true;
            }
            else
            {
                timersimilarDocs.Enabled = false;
            }
        }

        private void timersimilarDocs_Tick(object sender, EventArgs e)
        {
            timersimilarDocs.Enabled = false;

            // on récupère la liste des docs user sélectionnée
            List<string> dbids = new List<string>() ;

            for (int i = 0; i < trackingDocscheckedListBox.CheckedItems.Count;i++)
            {
                dbids.Add( ((TrackingDocument)  trackingDocscheckedListBox.CheckedItems[i]).dbID );
            }

            if (dbids.Count == 0)
                return;

            if (string.IsNullOrEmpty(similarDocsFormattextBox.Text))
            {
                MessageBox.Show("format de récupération des données invalide ");
            }

            similarDocsFormattextBox.Enabled = false;

            Int32 seuil, taille, maxS,maxgen,depth,fetchS;

            bool ok = Int32.TryParse(seuilsimilarDocstextBox.Text, out seuil) &&
                      Int32.TryParse(tailleSimilarDocstextBox.Text, out taille) &&
                      Int32.TryParse(maxSignatureSimilarDocstextBox.Text, out maxS) &&
                      Int32.TryParse(maxgentextBox.Text, out maxgen) &&
                      Int32.TryParse(depthSimilarDocstextBox.Text, out depth) &&
                      Int32.TryParse(fetchSizeSimilarDocsTextBox.Text, out fetchS);
            if (!ok)
            {
                timersimilarDocs.Enabled = true;
                similarDocsFormattextBox.Enabled = true;
                return;
            }

            dbids.Add(seuilsimilarDocstextBox.Text);
            dbids.Add(tailleSimilarDocstextBox.Text);
            dbids.Add(maxSignatureSimilarDocstextBox.Text);
            dbids.Add(maxgentextBox.Text);
            dbids.Add(depthSimilarDocstextBox.Text);
            dbids.Add(similarDocsFormattextBox.Text);
            dbids.Add(fetchSizeSimilarDocsTextBox.Text);
            // on y va

            if (getSimilarDocsbackgroundWorker.IsBusy)
            {
                timersimilarDocs.Enabled = true;
                return;
            }
            timerSimilarDocscheckBox.Enabled = false;
            _connector.Lock();
            getSimilarDocsbackgroundWorker.RunWorkerAsync(dbids);
        }

        private void getSimilarDocsbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            List<string> dbids = (List<string>)e.Argument;

            Int32 fetchSize = Int32.Parse(dbids[dbids.Count - 1]);
            dbids.RemoveAt(dbids.Count - 1);
            string format = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);
            string depth = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);
            string maxgen = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);
            string maxSignature = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);
            string taille = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);
            string seuil = dbids[dbids.Count - 1];
            dbids.RemoveAt(dbids.Count - 1);

            List<List<string[]>> d = new List<List<string[]>>();
            e.Result = d;


            _connector._DirectExecute = false;
            List<string[]> l;
            string[] results;
            int totalToFetch = 0;
            int fetched = 0, totalFetched = 0;
            for (int i = 0; i < dbids.Count; i++)
            {
                // PYD @ modifier
                continue;
                _connector.OpenScript(null);
               // _connector.Knowledge().SimilarDocs(knw_name, dbids[i], seuil, maxSignature, taille, maxgen, depth);
               // _connector.Results().SetFormat(format);
               // _connector.Results().SortBy("pubDate", false);
               // _connector.Results().Fetch(fetchSize, 1);
               //  _connector.ExecuteScript();
                // on récupère le nombre total de documents fetché par le serveur
                results = _connector.GetDataByName("Elements", 0);
                if (results == null)
                    continue;
                // on récupère le nombre de docs fetchés
                fetched = (Int32) _connector._result._lines[3];
                totalFetched = 0;
                totalToFetch = Int32.Parse(results[0]);

                    l = new List<string[]>();
                    int row = 3;
                    while (true)
                    {
                        for (int j = 0; j < fetched; j++)
                        {
                            results = _connector.GetDataByLine(j, row);
                            if (results != null)
                            {
                                l.Add(results);
                            }
                        }
                        totalFetched += fetched;
                        if (totalFetched >= totalToFetch)
                        {
                            break;
                        }
                        row = 0;
                        // on fait du fetch streaming
                        _connector.OpenScript(null);
                        _connector.Results().SetProperties( "1", new string[] { "format = " + format } );
                        _connector.Results().Fetch(fetchSize.ToString(), totalFetched.ToString(), "1");
                        _connector.ExecuteScript();
                        fetched = (Int32)_connector._result._lines[row];

                    }
                    d.Add(l);
                
            }
            _connector._DirectExecute = true;
        }

        private void getSimilarDocsbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            List<List<string[]>> l = (List<List<string[]>>)e.Result;


            similarDocs.Clear();

            similarDocs.AddRange(l);


            similarDocsdataGridView.Rows.Clear();
            // on replace les bons headers à partir du format
            similarDocsdataGridView.Columns.Clear();
            string[] headers = similarDocsFormattextBox.Text.Split(' ');
            similarDocsdataGridView.ColumnCount = headers.Length;
            for (int i = 0; i < headers.Length; i++)
            {
                similarDocsdataGridView.Columns[i].HeaderText = headers[i];
            }


            similarDocstreeView.Nodes[0].Nodes.Clear();


            List<string[]> ll;
            string[] ss;
            TreeNode cc = null;
            TrackingDocument trdoc;
            TreeNode c = null;
            for (int i = 0; i < trackingDocscheckedListBox.CheckedItems.Count; i++)
            {
                trdoc = (TrackingDocument)trackingDocscheckedListBox.CheckedItems[i];
                c = new TreeNode(trdoc.title);
                similarDocstreeView.Nodes[0].Nodes.Add(c);
                ll = l[i];
                for (int j = 0; j < ll.Count; j++)
                {
                    ss = ll[j]; // ll[j] contient le tableau de la ligne "RowId title link Act" du similar doc courant
                    if (ss != null)
                    {
                        cc = new TreeNode(ss[1]); // ss[1] contient le champs "title" du similar doc courant
                        c.Nodes.Add(cc);
                        // on remplit la pile de similarDocs
                        trdoc.similarDocs.Add(ss[0]);
                    }
                }
            }

            similarDocstreeView.Nodes[0].ExpandAll();
            similarDocsFormattextBox.Enabled = true;
            //
            if (timerSimilarDocscheckBox.Checked)
            {
                timersimilarDocs.Enabled = true;
            }
            else
            {
                timersimilarDocs.Enabled = false;
            }

            timerSimilarDocscheckBox.Enabled = true;
        }


        private void rebuildbutton_MouseUp(object sender, MouseEventArgs e)
        {
            rebuildbutton.Enabled = false;

            if ( fieldsToRebuildtextBox.Text.Equals("") )
            {
                MessageBox.Show("aucuns champs spécifiés.");
                rebuildbutton.Enabled = true;
                return;
            }
            if (Int32.Parse(rebuildTotextBox.Text) >= Int32.Parse(currentTable_TotalLines))
            {
                rebuildTotextBox.Text = (Int32.Parse(currentTable_TotalLines) - 1).ToString();
            }

            string[] p = new string[]{fieldsToRebuildtextBox.Text,rebuildFromtextBox.Text,rebuildTotextBox.Text, rebuildParamcomboBox.Text};

            if (rebuildKnwbackgroundWorker.IsBusy)
                return;
            _connector.Lock();
            rebuildKnwbackgroundWorker.RunWorkerAsync(p);
            
        }

        private void rebuildKnwbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] p = (string[]) e.Argument;
            _connector._DirectExecute = false;
            _connector.OpenScript(null);
            _connector.Session().MarcRebuild(p[0], p[1],p[2], p[3]);
            _connector.ExecuteScript();
            _connector._DirectExecute = true;
        }

        private void rebuildKnwbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            if ( saveKnwAfterRebuildcheckBox.Checked && !KnwSavebackgroundWorker.IsBusy )
            {
                _connector.Lock();
                saveKnwButton.Enabled = false;
                KnwSavebackgroundWorker.RunWorkerAsync();
            }
            rebuildbutton.Enabled = true;
        }

        private void RefreshRateUnitcomboBox_TextChanged(object sender, EventArgs e)
        {
            
            if ( RefreshRateUnitcomboBox.Text == "milliseconde" )
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text);
            }
            else if( RefreshRateUnitcomboBox.Text == "seconde" )
            {
                updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 1000;
            }
            else if( RefreshRateUnitcomboBox.Text == "minute" )
            {
                 updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 60000;
            }
            else if ( RefreshRateUnitcomboBox.Text == "heure" )
            {
                 updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 3600000;
            }
            else if ( RefreshRateUnitcomboBox.Text == "jour" )
            {
                 updateFeedstimer.Interval = Int32.Parse(RRtextBox.Text) * 86400000;
            }

        }

        private void verbositycomboBox_TextChanged(object sender, EventArgs e)
        {
            if (verbositycomboBox.Text == "Quiet")
            {
                Logger.Instance.Verbosity = LoggerVerbosity.Quiet;
            }
            else if (verbositycomboBox.Text == "Details")
            {
                Logger.Instance.Verbosity = LoggerVerbosity.Detailed;
            }
        }

        private void GetTblTotalLinesbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _connector._DirectExecute = true;
            for (int i = 0; i < this.instances.Length; i++)
            {
                this._connector.Table().GetLines(this.instances[i]);
                string[] dataByName = this._connector.GetDataByName("Lines", -1);
                if ((dataByName != null) && (dataByName.Length > 0))
                {
                    this.tablesTotalLines[i] = UInt64.Parse(dataByName[0]);
                }
                else
                {
                    this.tablesTotalLines[i] = 0;
                }
                // progressf = (float)i / (float)instances.Length * 100;
                // GetTblInstancesbackgroundWorker.ReportProgress((Int32)progressf);
            }


        }

        private void GetTblTotalLinesbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();

            if (tablesInstances.SelectedItem != null)
            {
                current_table_id = tablesInstances.SelectedIndex;
            }
            else
            {
                current_table_id = 0;
                tablesInstances.SelectedIndex = 0;
            }

            TotalArticles = (UInt64)tablesTotalLines[current_table_id];
            currentTable_TotalLines = tablesTotalLines[current_table_id].ToString();
            rebuildTotextBox.Text = (tablesTotalLines[current_table_id] - 1).ToString();

            int num = int.Parse(this.fetchStart.Text) + int.Parse(this.fetchSize.Text);

            if (num > int.Parse(currentTable_TotalLines) - 1)
            {
                num = int.Parse(currentTable_TotalLines) - 1;

                fetchStart.Text = (num - int.Parse(this.fetchSize.Text)).ToString();
            }

            if (int.Parse(fetchStart.Text) < 1)
            {
                fetchStart.Text = "1";
            }

            if ( !trackBar.Enabled )
                trackBar.Enabled = true;

            if ( !forward.Enabled )
                forward.Enabled = true;

            if (!GetTblDatabackgroundWorker.IsBusy)
            {
                _connector.Lock();
                int[] p = new int[] { int.Parse(this.fetchStart.Text), num };
                GetTblDatabackgroundWorker.RunWorkerAsync(p);
            }

        }

        private void CheckRSSIntegrity()
        {
            if (CheckRSSReadbackgroundWorker.IsBusy )
                return;


            List<string> thelist = new List<string>();

            // on récupère les feeds sélectionnés
            string link;
            for (int i = 0; i < Program.Config.Count; i++)
            {
                link = (string) Program.Config[i].File;
                if ( !string.IsNullOrEmpty( link  ) )
                thelist.Add( link );
            }

            _connector.Lock();
            CheckRSSReadbackgroundWorker.RunWorkerAsync( thelist );

            
        }
        private void CheckRSSReadbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> thelist = (List<string>)e.Argument;

            XDocument doc = null ;
            List<XElement> items;
            byte[] hash;
            UInt64 gid = 0;
            MD5 md5 = MD5.Create();
            string[] res = null;
            Guid result;
            XAttribute isRead = null;
            _connector._DirectExecute = false;
            bool isModified ;
            string filename;
            for (int i = 0; i < thelist.Count; i++)
            {
                isModified = false;
                filename = Program.AppDataPath + thelist[i];
                if ( File.Exists( filename ) )
                {
                    items = Reader.ReadFromFile(filename, ref doc, false);
                   // pour chaque item, on prend le lien http et on regarde sir le guid correspondant est dans la DB du serveur mARC
                    // sinon on met son attribut isRead à 0
                   //on calcul un guid
                    for (int j = 0; j < items.Count;j ++ )
                    {
                       isRead = items[j].Attribute("isRead");
                        if ( isRead == null )
                            continue;
                       hash = md5.ComputeHash(Encoding.Default.GetBytes( items[j].Element("guid").Value ));
                       result = new Guid(hash);
                       gid = (UInt32)result.GetHashCode();
                       // reverse string s
                       char[] arr = items[j].Element("guid").Value.ToCharArray();
                       Array.Reverse(arr);
                       string s = new string(arr);
                       hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                       result = new Guid(hash);
                       gid += (UInt32)result.GetHashCode();

                    _connector.OpenScript(null);
                    _connector.Table().Select(current_table, "new", "guid", "=", gid.ToString(), " ");
                    _connector.Session().Clear("results");
                    _connector.ExecuteScript();
                    res = _connector.GetDataByName("ResultCount", 0);
                    if (res != null && Int32.Parse(res[0]) != 0) // le guid existe déjà
                    {
                        items[j].SetAttributeValue("isRead", "1");
                        isModified = true;
                    }
                    else
                    {
                        items[j].SetAttributeValue("isRead", "0");
                    }

                 }

                    if ( isModified )
                        doc.Save(filename);
                }
            }
            _connector._DirectExecute = true;
        }

        private void CheckRSSReadbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _connector.UnLock();
            FeedscheckedListBox.Enabled = true;
            updateFeedstimer.Enabled = true;
        }

        private void SMTPHosttextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( SMTPHosttextBox.Text ) )
                return;

            smtpClient.Host = SMTPHosttextBox.Text;
        }

        private void SMTPPorttextBox_TextChanged(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty( SMTPPorttextBox.Text ) )
                return;

            Int32 port = 587;

            if ( !Int32.TryParse(SMTPPorttextBox.Text,out port) )
                return;
            smtpClient.Port = port;
        }

        private void AddDocFromDBbutton_MouseUp(object sender, MouseEventArgs e)
        {
            AddDocFromDBbutton.Enabled = false;

            if (tableData.SelectedCells == null )
                return;


            int rowSelected = tableData.SelectedCells[0].RowIndex;

            string dbId = (string)tableData.Rows[rowSelected].Cells[0].Value;
            TrackingDocument doc = new TrackingDocument();
            doc.dbID = dbId;
            doc.title = null;
            doc.pubDate = null;
            doc.link = null;
            for (int i = 0;i < tableData.Columns.Count;i++)
            {
                if ( tableData.Columns[i].HeaderText.Equals("title") )
                {
                    doc.title = (string)tableData.Rows[rowSelected].Cells[i].Value;
                }
                else if ( tableData.Columns[i].HeaderText.Equals("link") )
                {
                    doc.link = (string)tableData.Rows[rowSelected].Cells[i].Value;
                }
                else if ( tableData.Columns[i].HeaderText.Equals("pubdate") )
                {
                    doc.pubDate = (string)tableData.Rows[rowSelected].Cells[i].Value;
                }
            }
                if ( doc.title ==null || doc.link == null || doc.pubDate == null )
                {
                    AddDocFromDBbutton.Enabled = true;
                    MessageBox.Show(" il manque des informations pour le document à tracker. sélectionner au moins les champs \"title\", \"link\", \"pubDate\" ");
                    return;
                }
            

            trackingDocscheckedListBox.Items.Add( doc );
            Program.SaveConfig();


        }

        private void timersimilarDocscomboBox_TextChanged(object sender, EventArgs e)
        {
            int time;
            if (!Int32.TryParse(timerSimilarDocstextBox.Text, out time))
                return;

            if (string.IsNullOrEmpty(timerSimilarDocstextBox.Text) || int.Parse(timerSimilarDocstextBox.Text) <= 0)
                return;


            if (timersimilarDocscomboBox.Text == "ms")
            {
                timersimilarDocs.Interval = time;
            }
            else if (timersimilarDocscomboBox.Text == "s")
            {
                timersimilarDocs.Interval = time * 1000;
            }
            else if (timersimilarDocscomboBox.Text == "minute")
            {
                timersimilarDocs.Interval = time * 60000;
            }
            else if (timersimilarDocscomboBox.Text == "heure")
            {
                timersimilarDocs.Interval = time * 3600000;
            }
            else if (timersimilarDocscomboBox.Text == "jour")
            {
                timersimilarDocs.Interval = time * 86400000;
            }

        }

        private void timerSimilarDocstextBox_TextChanged(object sender, EventArgs e)
        {
            int time;
            if (!Int32.TryParse(timerSimilarDocstextBox.Text, out time))
                return;

            if (string.IsNullOrEmpty(timerSimilarDocstextBox.Text) || int.Parse(timerSimilarDocstextBox.Text) <= 0)
                return;


            if (timersimilarDocscomboBox.Text == "ms")
            {
                timersimilarDocs.Interval = time;
            }
            else if (timersimilarDocscomboBox.Text == "s")
            {
                timersimilarDocs.Interval = time * 1000;
            }
            else if (timersimilarDocscomboBox.Text == "minute")
            {
                timersimilarDocs.Interval = time * 60000;
            }
            else if (timersimilarDocscomboBox.Text == "heure")
            {
                timersimilarDocs.Interval = time * 3600000;
            }
            else if (timersimilarDocscomboBox.Text == "jour")
            {
                timersimilarDocs.Interval = time * 86400000;
            }
        }

        private void AutoRebuildtextBox_TextChanged(object sender, EventArgs e)
        {
            AutoRebuildFrequency = UInt64.Parse(AutoRebuildtextBox.Text);
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            if (!current.Text.Equals("Sélection"))
            {
                return;
            }
            // récupération des instances de tables
            if (!GetTblInstancesbackgroundWorker.IsBusy)
            {
                tablesInstances.Items.Clear();
                _connector.Lock();
                GetTblInstancesbackgroundWorker.RunWorkerAsync();
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            if (current.Text.Equals("Visualisation"))
            {
                GetTableContent();
            }
        }
    }
}


    




