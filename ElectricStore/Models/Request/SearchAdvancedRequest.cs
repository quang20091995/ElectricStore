using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class SearchAdvancedRequest
    {
        public String[] Company { get; set; }
        public String[] CPU { get; set; }
        public String[] Memory { get; set; }
        public String[] Disc { get; set; }
        public String[] Monitorsize { get; set; }
        public String[] Capacity { get; set; }
        public String[] Core { get; set; }
        public String[] Price { get; set; }
    }
}