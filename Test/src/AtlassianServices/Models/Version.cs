using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianService.Models
{
    /// <summary>
    /// Zephyr test version
    /// </summary>
    public class Version
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ProjectId { get; set; }
    }
}
