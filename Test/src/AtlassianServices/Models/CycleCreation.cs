using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianService.Models
{
    /// <summary>
    /// Zephyr request payload to new a test cycle
    /// </summary>
    public class CycleCreation
    {
        public string name { get; set; }
        public string build { get; set; }
        public string environment { get; set; }
        public string description { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public long projectId { get; set; }
        public long versionId { get; set; }
    }
}
