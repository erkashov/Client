using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{

    public class Rootobject
    {
        
    }

    public class Errors
    {
        public string[] Sklad_rashod_tov { get; set; }
    }

    public class Error_HTTP
    {        
        public Errors errors { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }

    }
}
