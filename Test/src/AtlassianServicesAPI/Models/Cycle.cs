using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    public class Cycle
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public long ProjectId { get; set; }

        public long VersionId { get; set; }
    }
}
