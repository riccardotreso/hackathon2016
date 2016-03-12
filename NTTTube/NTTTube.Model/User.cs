using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTTube.Model
{
    public class User : BaseEntity
    {
        public string name { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string foto { get; set; }
    }
}
