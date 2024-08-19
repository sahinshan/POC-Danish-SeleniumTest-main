using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianService.Models
{
    public class IssueUpdate
    {
        public UpdateFields fields { get; set; }
    }

    public class UpdateFields
    {
        public string summary { get; set; }
        public string description { get; set; }
        public List<string> labels { get; set; }
    }
}
