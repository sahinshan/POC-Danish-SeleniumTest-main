using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class FAQProxy
    {
        public FAQProxy()
        {
            _faq = new FAQ();
        }


        private IFAQ _faq;


        public List<Entities.Portal.FAQ> TopTen(Guid WebsiteID, string SecurityToken)
        {
            return _faq.TopTen(WebsiteID, SecurityToken);
        }

        public List<Entities.Portal.FAQCategory> Categories(Guid WebsiteID, string SecurityToken)
        {
            return _faq.Categories(WebsiteID, SecurityToken);
        }

        public Entities.Portal.FAQ GetByCategorySEONameAndFAQSEOName(Guid WebsiteID, string CategorySeoName, string FaqSeoName, string SecurityToken)
        {
            return _faq.GetByCategorySEONameAndFAQSEOName(WebsiteID, CategorySeoName, FaqSeoName, SecurityToken);
        }

        public List<Entities.Portal.FAQ> GetByCategorySEOName(Guid WebsiteID, string CategorySeoName, string SecurityToken)
        {
            return _faq.GetByCategorySEOName(WebsiteID, CategorySeoName, SecurityToken);
        }
    }
}
