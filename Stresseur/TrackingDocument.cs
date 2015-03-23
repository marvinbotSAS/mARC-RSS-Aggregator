using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSRTReader
{
    class TrackingDocument
    {

        public string dbID; // l'id dans la DB du serveur mARC
        public string link; // le lien vers la donnée
        public string title; // le titre du document
        public string pubDate; // sa date d'insertion dans la DB du serveur mARC
        public List<string> similarDocs = new List<string>();

        public override string ToString()
        {
            return this.title;
        }
    }
}
