using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class FAQ
    {
        public Guid Id { get; set; }
        public string SEOName { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string CategorySEOName { get; set; }

    }
}
