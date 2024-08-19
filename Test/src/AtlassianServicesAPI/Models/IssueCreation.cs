using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    public class IssueCreation
    {
        public Fields fields { get; set; }
    }

    public class ProjectField
    {
        public string key { get; set; }
    }

    public class Issuetype
    {
        public string name { get; set; }
    }

    public class Reporter
    {
        public string name { get; set; }
    }
    public class Components
    {
        public string name { get; set; }
    }
    public class Customfield_10298
    {
        public string name { get; set; }
    }
    public class Versions
    {
        public string name { get; set; }
    }
    public class Customfield_19613
    {
        public string name { get; set; }
    }

    public class Fields
    {
        public ProjectField project { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public Issuetype issuetype { get; set; }
        public Reporter reporter { get; set; }
        public List<string> labels { get; set; }
        public List<Components> components { get; set; }
        public Customfield_10298 customfield_10298 { get; set; }
        public List<Versions> versions { get; set; }
        public Customfield_19613 customfield_19613 { get; set; }
    }
}
