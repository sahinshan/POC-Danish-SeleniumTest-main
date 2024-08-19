using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServicesAPI.Models
{
    public class IssueInCycle
    {
        public IssueInCycle() { }

        public string Key { get; set; }
        public string TestSummary { get; set; }
        public string TestName { get; set; }
        public string VersionName { get; set; }
        public List<string> Labels { get; set; }
        public List<AtlassianServiceAPI.Models.Step> Steps { get; set; }
        public List<string> CycleNames { get; set; }
        public bool Overwritten { get; set; }
    }
}
