namespace RSSRTReader.Misc
{
    public class Config
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string File { get; set; }

        public Config() { }

        public Config(int id, string title, string url, string file)
        {
            this.ID = id;
            this.Title= title;
            this.URL = url;
            this.File = file;
        }
    }
}
