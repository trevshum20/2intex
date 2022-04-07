using System;
namespace Intex2.Models
{
    public class Search
    {
        public string City { get; set; }
        public string County { get; set; }
        public int CrashId { get; set; }
        public string Road { get; set; }
        public string Topic { get; set; }
        
        public string SearchTerm { get; set; }
    }
}
