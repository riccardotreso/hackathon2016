using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTTube.Model
{
    public class Video : BaseEntity
    {
        public string username { get; set; }
        public DateTime date { get; set; }
        public string path { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public string channel { get; set; }
        public IEnumerable<string> like { get; set; }
        public IEnumerable<string> unlike { get; set; }

        public int likeCount
        {
            get
            {
                return like.Count();
            }
        }

        public int unlikeCount
        {
            get
            {
                return unlike.Count();
            }
        }

        public string filePath {
            get {
                return string.Format("{0}{1}.mp4", path, Id);
            }
        }
    }

    public class Comment
    {
        public string username { get; set; }
        public string text { get; set; }
        public DateTime data { get; set; }
    }


    public class Channel : BaseEntity
    {
        public Channel(string pName)
        {
            name = pName;
        }
        public string name { get; set; }
    }


}
