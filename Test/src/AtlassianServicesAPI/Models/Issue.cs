using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    /// <summary>
    /// Jira issue
    /// </summary>
    public class Issue
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Summary { get; set; }

        public long ProjectId { get; set; }
    }
}
