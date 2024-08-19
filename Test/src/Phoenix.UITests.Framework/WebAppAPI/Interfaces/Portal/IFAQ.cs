using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IFAQ
    {
        List<Entities.Portal.FAQ> TopTen(Guid WebsiteID, string SecurityToken);
        List<Entities.Portal.FAQCategory> Categories(Guid WebsiteID, string SecurityToken);
        Entities.Portal.FAQ GetByCategorySEONameAndFAQSEOName(Guid WebsiteID, string CategorySeoName, string FaqSeoName, string SecurityToken);
        List<Entities.Portal.FAQ> GetByCategorySEOName(Guid WebsiteID, string CategorySeoName, string SecurityToken);
    }
}
