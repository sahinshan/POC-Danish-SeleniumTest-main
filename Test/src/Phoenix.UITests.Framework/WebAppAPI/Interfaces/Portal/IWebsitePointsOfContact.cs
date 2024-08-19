using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IWebsitePointsOfContact
    {
        List<Entities.Portal.WebsitePointOfContact> PointsOfContacts(Guid WebsiteID, string SecurityToken);
    }
}
