/**
*
*  RSSRTReader is a lightweight RSS client written in C#
*
*  Copyright 2012 Ashot Aslanyan <ashot.aslanian@gmail.com>
*
*  This file is part of RSSRTReader.
*
*  RSSRTReader is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  RSSRTReader is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with servmonitor.  If not, see <http://www.gnu.org/licenses/>.
*
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using RSSRTReader.Misc;

using System.Security.Cryptography;
using System.Text;
namespace RSSRTReader
{
    /// <summary>
    /// Main RSS reader class
    /// </summary>
    public static class Reader
    {
        static UInt64 LeftDecal = (UInt64)Math.Pow(2, 32); // décalage vers la gauche de 32 bits

        public delegate void NewItemHandler(List<string> articles);

        public static event NewItemHandler NewItem;

        public static List<string> newArticles = new List<string>();

        private static string[] formats = new string[0];


        private class sortPubTimeDescHelper : IComparer<XElement>
        {
            int IComparer<XElement>.Compare(XElement a, XElement b)
            {
                if (Reader.getTimeStamp(a.Element("pubDate").Value) > Reader.getTimeStamp(b.Element("pubDate").Value))
                    return -1;
                else if (Reader.getTimeStamp(a.Element("pubDate").Value) < Reader.getTimeStamp(b.Element("pubDate").Value))
                    return 1;
                else
                    return 0;
            }

            internal static IComparer<XElement> SortPubTimeDesc()
            {
                return (IComparer<XElement>)new sortPubTimeDescHelper();
            }
        }

        private static bool getXmlFile(Uri url, string path)
        {

            try
            {
                Logger.Instance.Log("Reader", String.Format("url is {0}. trying to save as {1}...", url.ToString(), path));


                WebClient wc = new WebClient();

                wc.DownloadFile(url, path);

                if (File.Exists(path))
                {
                    Logger.Instance.Log("Reader.getXmlFile", "file to download is already there");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Reader", ex.Message);
                return false;
            }
        }

        private static long getLastID(string filename)
        {
            try
            {
                XDocument doc = XDocument.Load(filename);
                long id = 0;
                XAttribute Xid;
                long fileID = 0;
                string s;
                foreach (XElement item in doc.Root.Element("channel").Elements("item"))
                {
                    Xid = item.Attribute("id");

                    if (Xid != null)
                    {
                        s = Xid.Value;
                        fileID = System.Int32.Parse(s);
                        id = (fileID > id) ? fileID : id;
                    }

                }

                return id;
            }
            catch (UriFormatException ex)
            {
                Logger.Instance.Log("Reader", String.Format("incorrect file format for {0}", filename));
                throw(ex);
            }
        }

        public static string[] Rfc822DateTimePatterns
        {
            get
            {
                if (formats.Length > 0)
                {
                    return formats;
                }
                else
                {
                    formats = null;
                    formats = new string[35];

                    // two-digit day, four-digit year patterns
                    formats[0] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffffff zzzz";
                    formats[1] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffffff zzzz";
                    formats[2] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fffff zzzz";
                    formats[3] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ffff zzzz";
                    formats[4] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'fff zzzz";
                    formats[5] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'ff zzzz";
                    formats[6] = "ddd',' dd MMM yyyy HH':'mm':'ss'.'f zzzz";
                    formats[7] = "ddd',' dd MMM yyyy HH':'mm':'ss zzzz";

                    // two-digit day, two-digit year patterns
                    formats[8] = "ddd',' dd MMM yy HH':'mm':'ss'.'fffffff zzzz";
                    formats[9] = "ddd',' dd MMM yy HH':'mm':'ss'.'ffffff zzzz";
                    formats[10] = "ddd',' dd MMM yy HH':'mm':'ss'.'fffff zzzz";
                    formats[11] = "ddd',' dd MMM yy HH':'mm':'ss'.'ffff zzzz";
                    formats[12] = "ddd',' dd MMM yy HH':'mm':'ss'.'fff zzzz";
                    formats[13] = "ddd',' dd MMM yy HH':'mm':'ss'.'ff zzzz";
                    formats[14] = "ddd',' dd MMM yy HH':'mm':'ss'.'f zzzz";
                    formats[15] = "ddd',' dd MMM yy HH':'mm':'ss zzzz";

                    // one-digit day, four-digit year patterns
                    formats[16] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fffffff zzzz";
                    formats[17] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ffffff zzzz";
                    formats[18] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fffff zzzz";
                    formats[19] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ffff zzzz";
                    formats[20] = "ddd',' d MMM yyyy HH':'mm':'ss'.'fff zzzz";
                    formats[21] = "ddd',' d MMM yyyy HH':'mm':'ss'.'ff zzzz";
                    formats[22] = "ddd',' d MMM yyyy HH':'mm':'ss'.'f zzzz";
                    formats[23] = "ddd',' d MMM yyyy HH':'mm':'ss zzzz";

                    // two-digit day, two-digit year patterns
                    formats[24] = "ddd',' d MMM yy HH':'mm':'ss'.'fffffff zzzz";
                    formats[25] = "ddd',' d MMM yy HH':'mm':'ss'.'ffffff zzzz";
                    formats[26] = "ddd',' d MMM yy HH':'mm':'ss'.'fffff zzzz";
                    formats[27] = "ddd',' d MMM yy HH':'mm':'ss'.'ffff zzzz";
                    formats[28] = "ddd',' d MMM yy HH':'mm':'ss'.'fff zzzz";
                    formats[29] = "ddd',' d MMM yy HH':'mm':'ss'.'ff zzzz";
                    formats[30] = "ddd',' d MMM yy HH':'mm':'ss'.'f zzzz";
                    formats[31] = "ddd',' d MMM yy HH':'mm':'ss zzzz";

                    // Fall back patterns
                    formats[32] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK"; // RoundtripDateTimePattern
                    formats[33] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
                    formats[34] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;

                    return formats;
                }
            }
        }

        private static string ConvertZoneToLocalDifferential(string s)
        {
            string zoneRepresentedAsLocalDifferential = String.Empty;

            //------------------------------------------------------------
            //  Validate parameter
            //------------------------------------------------------------
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("s");
            }

            if (s.EndsWith(" UT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" UT") + 1)), "+00:00");
            }
            else if (s.EndsWith(" GMT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" GMT") + 1)), "+00:00");
            }
            else if (s.EndsWith(" EST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" EST") + 1)), "-05:00");
            }
            else if (s.EndsWith(" EDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" EDT") + 1)), "-04:00");
            }
            else if (s.EndsWith(" CST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" CST") + 1)), "-06:00");
            }
            else if (s.EndsWith(" CDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" CDT") + 1)), "-05:00");
            }
            else if (s.EndsWith(" MST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" MST") + 1)), "-07:00");
            }
            else if (s.EndsWith(" MDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" MDT") + 1)), "-06:00");
            }
            else if (s.EndsWith(" PST", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" PST") + 1)), "-08:00");
            }
            else if (s.EndsWith(" PDT", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" PDT") + 1)), "-07:00");
            }
            else if (s.EndsWith(" Z", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" Z") + 1)), "+00:00");
            }
            else if (s.EndsWith(" A", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" A") + 1)), "-01:00");
            }
            else if (s.EndsWith(" M", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" M") + 1)), "-12:00");
            }
            else if (s.EndsWith(" N", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" N") + 1)), "+01:00");
            }
            else if (s.EndsWith(" Y", StringComparison.OrdinalIgnoreCase))
            {
                zoneRepresentedAsLocalDifferential = String.Concat(s.Substring(0, (s.LastIndexOf(" Y") + 1)), "+12:00");
            }
            else
            {
                zoneRepresentedAsLocalDifferential = s;
            }

            return zoneRepresentedAsLocalDifferential;
        }

        /// <summary>
        /// Get Unix time stamp of RFC822
        /// </summary>
        /// <param name="date">Date in "ddd, dd MMM yyyy HH:mm:ss zzz" format</param>
        /// <returns>A long value that represent Unix timestamp</returns>
        private static long getTimeStamp(string date)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            try
            {
                DateTime dt = DateTime.ParseExact(Reader.ConvertZoneToLocalDifferential(date), Reader.Rfc822DateTimePatterns, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal); 
                TimeSpan t = (dt.ToLocalTime() - epoch.ToLocalTime());
                return (long)t.TotalSeconds;
            }
            catch(FormatException ex)
            {
                Logger.Instance.Log("Reader", "could not parse date in file");
                Logger.Instance.Log("Reader", String.Format("error was caused by: {0}", date));
                Logger.Instance.Log("Reader", String.Format("exception message was: {0}", ex.Message));
                throw (ex);
            }
        }

        /// <summary>
        /// Get date from Unix timestamp
        /// </summary>
        /// <param name="ts">Timestamp</param>
        /// <returns>A string with format dd/mm/yyyy hh:mm:ss</returns>
        public static string getDateTime(long ts)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            // Add the number of seconds in UNIX timestamp to be converted.
            dateTime = dateTime.AddSeconds(ts);

            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.

            return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
        }

        /// <summary>
        /// Read items from RSS file
        /// </summary>
        /// <param name="xmlFileName">RSS File name</param>
        /// <param name="doc">An <see cref="System.Xml.Linq.XDocument"/>, that contains RSS</param>
        /// <param name="newfile">Is <paramref name="xmlFileName"/> a new file or not</param>
        /// <returns>A <see cref="System.Collections.Generic.List{XElement}"/>, that contains items</returns>
        public static List<XElement> ReadFromFile(string xmlFileName, ref XDocument doc, bool newfile)
        {
            try
            {
                doc = XDocument.Load(xmlFileName);
            }
            catch (UriFormatException ex)
            {
                Logger.Instance.Log("Reader", String.Format("incorrect file format for {0}", xmlFileName));
                throw(ex);
            }

            if (newfile)
            {
                int i = 0;
                foreach (XElement item in doc.Root.Element("channel").Elements("item"))
                {
                    item.SetAttributeValue("id", i++);
                    item.SetAttributeValue("isRead", 0);
                }

                doc.Save(xmlFileName);
            }

            List<XElement> items = doc.Root.Element("channel").Elements("item").ToList();

            items.Sort(sortPubTimeDescHelper.SortPubTimeDesc());

            return items;
        }

        /// <summary>
        /// Read items from RSS file
        /// </summary>
        /// <param name="xmlFileName">RSS File name</param>
        /// <returns>A <see cref="System.Collections.Generic.List{XElement}"/>, that contains items</returns>
        public static List<XElement> ReadFromFile(string xmlFileName)
        {
            XDocument doc = new XDocument();
            return Reader.ReadFromFile(xmlFileName, ref doc, false);
        }

        /// <summary>
        /// Converts a <see cref="System.Collections.Generic.List{XElement}"/> object to <see cref="System.Collections.Generic.List{RssItem}"/> object
        /// </summary>
        /// <param name="xmlItems">An <see cref="System.Collections.Generic.List{XElement}"/></param>
        /// <returns>An <see cref="System.Collections.Generic.List{RssItem}"/></returns>
        public static List<RSSItem> ToRssItemList(List<XElement> xmlItems)
        {
            return xmlItems.Select(e => new RSSItem(
                        e.Attribute("id").Value,
                        e.Attribute("isRead").Value,
                        e.Element("title").Value,
                        e.Element("link").Value,
                        e.Element("description").Value,
                        Reader.getTimeStamp(e.Element("pubDate").Value)
                    )).ToList();
        }

        /// <summary>
        /// Download feed at <paramref name="url"/> and transforms it into <see cref="Reader.RSSObject"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// Also checks if feed updated or not
        /// </para>
        /// </remarks>
        /// <param name="url">The URL of feed</param>
        /// <returns>An <see cref="Reader.RSSObject"/>, which represents feed.</returns>
        public static RSSObject getFeed(RSSRTReader.Misc.Config feed)
        {
            string filename = Program.AppDataPath + feed.File;
            Uri url = new Uri(feed.URL);
            XDocument doc = new XDocument();
            List<XElement> myItems = new List<XElement>();

            Logger.Instance.Log("Reader", "reading feed file");

            if (File.Exists(filename))
            {
                Logger.Instance.Log("Reader", "file exists. maybe any updates?");

                try
                {
                    myItems = Reader.ReadFromFile(filename, ref doc, false);
                }
                catch (UriFormatException ex)
                {
                    Logger.Instance.Log("Reader", String.Format("could not read {0}", filename));
                    throw (ex);
                }

                if (Reader.getXmlFile(url, filename + ".new"))
                {
                    List<XElement> webItems = new List<XElement>();

                    try
                    {
                        webItems = Reader.ReadFromFile(filename + ".new", ref doc, false);
                        if (Reader.getTimeStamp(webItems.First().Element("pubDate").Value)
                            > Reader.getTimeStamp(myItems.First().Element("pubDate").Value))
                        {
                            List<XElement> newItems = new List<XElement>();
                            newItems = (
                                        from i in webItems
                                        where Reader.getTimeStamp(i.Element("pubDate").Value) > Reader.getTimeStamp(myItems.First().Element("pubDate").Value)
                                        select i
                                       ).ToList();

                            Logger.Instance.Log("Reader", String.Format("{0} new items in feed found", newItems.Count));

                            long lastID = Reader.getLastID(filename);

                            foreach (XElement newItem in newItems)
                            {
                                newItem.SetAttributeValue("id", ++lastID);
                            }

                            if (newItems.Count > 0)
                            {
                                Logger.Instance.Log("RSSRTReader", "throwing event");
                                List<string> articlesLinks = new List<string>();
                                string s;
                                byte[] hash;
                                UInt64 gid;
                                for (int i = 0; i < newItems.Count; i++)
                                {
                                    // compute un guid pour l'article qui sera injecté dans le B-TREE unique de mARC serveur
                                    s = (string)newItems[i].Element("guid").Value;
                                    using (MD5 md5 = MD5.Create())
                                    {
                                        hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                                        Guid result = new Guid(hash);
                                        gid = (UInt32)result.GetHashCode();
                                        // reverse string s
                                        char[] arr = s.ToCharArray();
                                        Array.Reverse(arr);
                                        s = new string(arr);
                                        hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                                        result = new Guid(hash);
                                        gid += (UInt32)result.GetHashCode();
                                    }
                                    DateTime date = Convert.ToDateTime(newItems[i].Element("pubDate").Value);
                                    // DateTime date= start.AddMilliseconds(dateNumber).ToLocalTime();
                                    s = gid.ToString() + "<pubDate>" + date.ToString() + "<pubDate/>" + "<description>" + newItems[i].Element("description").Value + "<description/>" + newItems[i].Element("link").Value;
                                    newItems[i].SetAttributeValue("isRead", "1");
                                    articlesLinks.Add(s);
                                }
                                Reader.NewItem(articlesLinks);


                                Logger.Instance.Log("RSSRTReader", "throwed");
                                newArticles.AddRange(articlesLinks);
                                Logger.Instance.Log("Reader", String.Format("saving changes to {0}", filename));
                                doc = XDocument.Load(filename);
                                doc.Root.Element("channel").Elements("item").First().AddBeforeSelf(newItems);
                                doc.Save(filename);
                                Logger.Instance.Log("Reader", "saved");
                            }

                            myItems.AddRange(newItems);
                        }

                        File.Delete(filename + ".new");
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("Reader", String.Format("could not read {0}", filename + ".new. Deleting it "));
                        File.Delete(filename + ".new");
                        throw (ex);
                    }

                }
                else
                {
                    throw (new FileDownloadException("couldn't download new feed"));
                }

            }
            else
            {
                if (!Reader.getXmlFile(url, filename))
                    throw (new FileDownloadException("couldn't download feed"));

                myItems = Reader.ReadFromFile(filename, ref doc, true); 
            }

            myItems.Sort(sortPubTimeDescHelper.SortPubTimeDesc());
            int unread = 0;
            try
            {
                unread = myItems.Count(t => (t.Attribute("isRead") != null && Int32.Parse(t.Attribute("isRead").Value) == 0) || t.Attribute("isRead") == null );
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("MainForm GetFeeds", String.Format("exception message was: {0}. \n Stack trace: {1}\n", ex.Message, ex.StackTrace));
            }

            if (unread > 0)
            {

               Logger.Instance.Log("RSSRTReader", "throwing event");
                            List<string> articlesLinks = new List<string>();
                            string s;
                            byte[] hash;
                            UInt64 gid;
                            for (int i = 0; i < myItems.Count; i++)
                            {
                                if (myItems[i].Attribute("isRead") != null  && Int32.Parse(myItems[i].Attribute("isRead").Value) != 0)
                                    continue;
                                // compute un guid pour l'article qui sera injecté dans le B-TREE unique de mARC serveur
                                s = (string)myItems[i].Element("guid").Value;
                                using (MD5 md5 = MD5.Create())
                                {
                                    hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                                    Guid result = new Guid(hash);
                                    gid = (UInt32)result.GetHashCode();
                                    // reverse string s
                                    char[] arr = s.ToCharArray();
                                    Array.Reverse(arr);
                                    s = new string(arr);
                                    hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
                                    result = new Guid(hash);
                                    gid += (UInt32)result.GetHashCode();
                                }
                                DateTime date = Convert.ToDateTime(myItems[i].Element("pubDate").Value);
                                s = gid.ToString() + "<pubDate>" + date.ToString() + "<pubDate/>" + "<description>" + myItems[i].Element("description").Value + "<description/>" + myItems[i].Element("link").Value;

                                articlesLinks.Add(s);

                                myItems[i].SetAttributeValue("isRead", "1");
                            }

                            doc.Root.ReplaceAll(myItems);
                            doc.Save(filename);
                            newArticles.AddRange(articlesLinks);

                            Logger.Instance.Log("Reader", String.Format("saving changes to {0}", filename));
                            
                            Logger.Instance.Log("Reader", "saved");

                            Logger.Instance.Log("RSSRTReader", "throwed");
            }
            Logger.Instance.Log("Reader", "reading from file to objects");
            doc = XDocument.Load(filename);

            RSSObject rssFeed = doc.Root.Elements("channel").
                Select(e => new RSSObject(
                    e.Element("title").Value,
                    e.Element("link").Value,
                    e.Element("description").Value,
                    myItems.Count,
                    filename,
                    unread
                )).First();

            rssFeed.items = Reader.ToRssItemList(myItems);

            Logger.Instance.Log("Reader", "read complete.\n" +
                "Overall there are " + myItems.Count + " items.\n" +
                "Undread items: " + unread + ".");

            return rssFeed;
        }
    }
}