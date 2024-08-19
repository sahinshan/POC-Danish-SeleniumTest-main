using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianServiceAPI.Models
{
    public class TestToCycle
    {
        public long projectId { get; set; }
        public long versionId { get; set; }
        public List<long> issues { get; set; }
        public string method { get; set; }
    }
}
