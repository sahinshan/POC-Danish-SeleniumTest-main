using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class WebsiteAnnouncements
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public string ExpiresOn { get; set; }
        public DateTime? ExpiresOnDate 
        { 
            get 
            {
                if (string.IsNullOrEmpty(ExpiresOn))
                    return null;

                return DateTime.Parse(ExpiresOn);
            } 
        }
        public string PublishedOn { get; set; }
        public DateTime? PublishedOnDate 
        {
            get
            {
                if (string.IsNullOrEmpty(PublishedOn))
                    return null;

                return DateTime.Parse(PublishedOn);
            }
        }

    }
}
