using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IWebsiteFeedback
    {
        Guid Feedback(Entities.Portal.WebsiteFeedback WebsiteFeedback, Guid WebsiteID, string SecurityToken);
    }
}
