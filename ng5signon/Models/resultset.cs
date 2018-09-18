using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng5signon.Models
{
    public class resultset
    {
        public dynamic data { get; set; }
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public string exceptionMessage { get; set; }
    }
}
