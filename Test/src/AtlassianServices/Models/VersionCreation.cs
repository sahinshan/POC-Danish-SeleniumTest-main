using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianService.Models
{
    public class VersionCreation
    {
        public string description { get; set; }
        public string name { get; set; }
        public bool archived { get; set; }
        public bool released { get; set; }
        public string startDate { get; set; }
        public string project { get; set; }
        public long projectId { get; set; }
    }
}
