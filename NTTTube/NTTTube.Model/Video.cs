using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTTube.Model
{
    public class Video : BaseEntity
    {
        public string path { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public IEnumerable<comment> comments { get; set; }
        public string channel { get; set; }
        public IEnumerable<string> like { get; set; }
        public IEnumerable<string> unlike { get; set; }
    }

    public class comment
    {
        public string username { get; set; }
        public string text { get; set; }
        public DateTime data { get; set; }
    }

    

}
