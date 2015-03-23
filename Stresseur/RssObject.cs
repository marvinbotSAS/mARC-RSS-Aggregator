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
using RSSRTReader.Misc;

namespace RSSRTReader
{
    /// <summary>
    /// Represents structure of an RSS document.
    /// </summary>
    public class RSSObject
    {
        /// <summary>
        /// Feed's title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Feed's link
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Feed's description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Filename where feed saved
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// Count of unreaded items in feed
        /// </summary>
        public int unreadItems { get; set; }

        /// <summary>
        /// Feed items
        /// </summary>
        public List<RSSItem> items;

        /// <summary>
        /// Standart constructor
        /// </summary>
        public RSSObject() { }

        /// <summary>
        /// Standart constructor
        /// </summary>
        /// <param name="t">Feed title</param>
        /// <param name="l">Feed link</param>
        /// <param name="d">Feed description</param>
        /// <param name="itemSize">Feed length</param>
        /// <param name="f">Filename where feed is saved</param>
        /// <param name="ui">Unread items quanity in feed</param>
        public RSSObject(string t, string l, string d, int itemSize, string f, int ui)
        {
            this.title = t; this.link = l; this.description = d;
            this.unreadItems = ui;

            this.items = new List<RSSItem>();

            this.filename = f;

            Logger.Instance.Log("RssObject", "title: " + this.title +
                        " | link: "         + this.link + 
                        " | description: "  + this.description + 
                        " | size: "         + itemSize);
        }
    }

    /// <summary>
    /// Represents structure of RSS item
    /// </summary>
    public class RSSItem
    {
        /// <summary>
        /// Item title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Item link
        /// </summary>
        public string link  { get; set; }

        /// <summary>
        /// Item description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Item publication date
        /// </summary>
        public long pubDate { get; set; }

        /// <summary>
        /// Item ID in feed file
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// If item is read or not
        /// </summary>
        public bool isRead { get; set; }

        /// <summary>
        /// Standart constructor
        /// </summary>
        public RSSItem() { }

        /// <summary>
        /// Standart constructor
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="isRead">If item is read or not</param>
        /// <param name="t">Item title</param>
        /// <param name="l">Item link</param>
        /// <param name="d">Item description</param>
        /// <param name="p">Item publication date</param>
        public RSSItem(string id, string isRead, string t, string l, string d, long p)
        {
            this.title = t; this.link = l; this.description = d;
            this.pubDate = p;

            try
            {
                this.id = System.Int32.Parse(id);
                this.isRead = (System.Int32.Parse(isRead) == 0) ? false : true;
            }
            catch (Exception)
            {
                Logger.Instance.Log("RssItem", "Unable to convert attribute \"id\" and \"isRead\".");
                Logger.Instance.Log("RssItem", "Setting values to 0.");
                this.id = 0;
                this.isRead = false;
            }
        }
    }
}
