using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class WebsiteFeedbackProxy
    {
        public WebsiteFeedbackProxy()
        {
            _WebsiteFeedback = new WebsiteFeedback();
        }


        private IWebsiteFeedback _WebsiteFeedback;


        public Guid Feedback(Entities.Portal.WebsiteFeedback WebsiteFeedback, Guid WebsiteID, string SecurityToken)
        {
            return _WebsiteFeedback.Feedback(WebsiteFeedback, WebsiteID, SecurityToken);
        }

    }
}
