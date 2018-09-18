using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng5signon.Models
{
    public class database
    {
    }

    public class Users {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
    }
}
