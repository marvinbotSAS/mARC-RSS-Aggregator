using RSSRTReader.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RSSRTReader
{
    static class Program
    {
        /// <summary>
        /// Config file directory
        /// </summary>
        public static string AppDataPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Visual Studio 2012\\Projects\\RSSRTReader" + Path.DirectorySeparatorChar;
            }
        }

        private static List<RSSRTReader.Misc.Config> _cnf;

        public static List<RSSRTReader.Misc.Config> Config { get { return _cnf; } }

        public static MainForm mainform;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _cnf = new List<RSSRTReader.Misc.Config>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger logger = Logger.GetInstance("Rss Real Time Reader", false, true);

            mainform = new MainForm();

            int error = Program.GetConfig();
            switch (error)
            {
                case 0:
                    Logger.Instance.Log("Program", " configuration file loaded. \n");
                    break;
                case -1:
                    Logger.Instance.Log("Program", "config file is empty. consider adding some feeds. \n");
                    break;
                case -2:
                    Logger.Instance.Log("Program", "could not create config file. \n");
                    break;
                case -3:
                    Logger.Instance.Log("Program", " configuration file did not exist. New configuration file created. \n");
                    break;
                case -4:
                    Logger.Instance.Log("Program", "invalid configuration xml file. please delete it. \n");
                    break;
                case -5:
                    Logger.Instance.Log("Program", "could not create config directory. \n");
                    break;
                case -6:
                    Logger.Instance.Log("Program", "could not create config file. \n");
                    break;
            }

            try
            {
                Application.Run(mainform);
            }
            catch (System.Reflection.TargetInvocationException e)
            {

                System.Windows.Forms.MessageBox.Show(e.Message + " : inner Exception : '" + e.InnerException.Message+"' 2. inner exception '"+e.InnerException.InnerException.Message+"'  "+e.InnerException.InnerException.StackTrace);
            }
        }

        public static int GetConfig()
        {
            if (Directory.Exists(AppDataPath))
            {
                try
                {
                    XDocument doc = XDocument.Load(AppDataPath + "rssreader.config.xml");

                    try
                    {
                        foreach (XElement feed in doc.Root.Elements("feed"))
                        {
                            Config.Add(
                                new RSSRTReader.Misc.Config(
                                    System.Int32.Parse(feed.Attribute("id").Value),
                                    feed.Attribute("title").Value,
                                    feed.Attribute("url").Value,
                                    feed.Attribute("file").Value
                                )
                            );
                            mainform.FeedsTable.Rows.Add(new string[]{feed.Attribute("title").Value, feed.Attribute("url").Value} );
                            mainform.FeedscheckedListBox.Items.Add(feed.Attribute("title").Value);
                        }

                        TrackingDocument trdoc;
                        foreach (XElement d in doc.Root.Elements("trackingDocument"))
                        {
                            trdoc = new TrackingDocument();
                            trdoc.dbID = d.Attribute("dbID").Value;
                            trdoc.link = d.Attribute("link").Value;
                            trdoc.title = d.Attribute("title").Value;
                            trdoc.pubDate = d.Attribute("pubDate").Value;
                            XAttribute a = d.Attribute("similarDocs");
                            if (a != null)
                            {
                                string sdocs = a.Value;
                                string[] docs = sdocs.Split(':');
                                trdoc.similarDocs.AddRange(docs);
                                // on cherche tous les éléments vides 
                                //IEnumerable<string> nullItems = from string s in trdoc.similarDocs where string.IsNullOrEmpty(s) == null select s;
                                // et on les bute
                                trdoc.similarDocs.RemoveAll(item => string.IsNullOrEmpty(item) == true);
                            }
                            mainform.trackingDocscheckedListBox.Items.Add(trdoc);
                        }

                    }
                    catch (NullReferenceException)
                    {
                        Logger.Instance.Log("mARC Rss RT Reader Application", "config file is empty");
                        Logger.Instance.Log("mARC Rss RT Reader Application", "consider adding some feeds");
                        return -1;
                    }

                    return 0;

                }
                catch (FileNotFoundException)
                {
                    Logger.Instance.Log("mARC Rss RT Reader Application", "no config file found. creating...");
                    try
                    {
                        XDocument doc = new XDocument(new XElement("config"));
                        doc.Save(AppDataPath + "rssreader.config.xml");

                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("mARC Rss RT Reader Application", "could not create config file.");
                        return -2;
                        throw (new CreateConfigFileException("could not create config file", ex));
                    }
                    Logger.Instance.Log("mARC Rss RT Reader Application", "created");
                    return -3;
                }
                catch (UriFormatException)
                {
                    Logger.Instance.Log("mARC Rss RT Reader Application", "invalid xml file. please delete it.");
                    return -4;
                }
            }
            else
            {
                Logger.Instance.Log("mARC RT Rss Reader Application", "directory not found. creating...");
                try
                {
                    Directory.CreateDirectory(AppDataPath);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log("mARC Rss RT Reader Application", "could not create config directory.");
                    return -5;
                    throw (new CreateConfigDirException("could not create config directory", ex));
                }
                Logger.Instance.Log("mARC Rss RT Reader Application", "created");

                Logger.Instance.Log("mARC Rss RT Reader Application", "creating config file...");
                try
                {
                    XDocument doc = new XDocument(new XElement("config"));
                    doc.Save(AppDataPath + "rssreader.config.xml");
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log("mARC Rss RT Reader Application", "could not create config file.");
                    return -6;
                    throw (new CreateConfigFileException("could not create config file", ex));
                }
                Logger.Instance.Log("mARC Rss RT Reader Application", "created");
                return 0;
            }
        }

        public static bool SaveConfig()
        {
            string path = AppDataPath + "rssreader.config.xml";

            if (File.Exists(path))
            {
                File.Delete(AppDataPath + "rssreader.config.xml");
            }
            Logger.Instance.Log("mARC Rss RT Reader Application", "saving info to config file");
            XDocument doc = new XDocument(
                new XElement("config", Config.Select(
                    x => new XElement("feed",
                        new XAttribute("id", x.ID),
                        new XAttribute("title", x.Title),
                        new XAttribute("url", x.URL),
                        new XAttribute("file", x.File)
                        )
                    ))
                );

            try
            {
                XElement xE = doc.Root;
                TrackingDocument[] docs = new TrackingDocument[mainform.trackingDocscheckedListBox.Items.Count];
                TrackingDocument d;
                                    string similardocs ="";
                for (int i = 0; i < mainform.trackingDocscheckedListBox.Items.Count; i++)
                {
                    d = (TrackingDocument)mainform.trackingDocscheckedListBox.Items[i];
                    similardocs= ":";
                    foreach( string s in d.similarDocs )
                    {
                        similardocs+=s+":";
                    }
                    xE.Add(new XElement("trackingDocument",
                                          new XAttribute("dbID", d.dbID),
                                          new XAttribute("link", d.link),
                                          new XAttribute("title", d.title),
                                          new XAttribute("pubDate", d.pubDate),
                                          new XAttribute("similarDocs",similardocs)) );
                }
                doc.Save(path);
                Logger.Instance.Log("mARC Rss RT Reader Application", "saved");

                return true;
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.StackTrace+": "+eee.Message);
                return false;
            }

        }
    }
}
