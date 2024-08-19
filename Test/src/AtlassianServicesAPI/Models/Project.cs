using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    /// <summary>
    /// Jira project
    /// </summary>
    public class Project
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }
    }
}
