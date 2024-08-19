using Phoenix.WebAPIHelper.WebAppAPI.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI
{
    public class WebAPIHelper
    {
        public CHIEProxy chie;
        public SecurityProxy Security;
        public ScheduleJobProxy ScheduleJob;
        public FinanceProxy FinanceProxy;
        public GetFileProxy GetFile;
        public WorkflowJobProxy WorkflowJob;
        public PortalSecurityProxy PortalSecurityProxy;
        public WebsitePointsOfContactProxy WebsitePointsOfContactProxy;
        public WebsiteFeedbackProxy WebsiteFeedbackProxy;
        public WebsiteProxy WebsiteProxy;
        public WebsiteAnnouncementsProxy WebsiteAnnouncementsProxy;
        public FAQProxy FAQProxy;
        public WebsiteContactProxy WebsiteContactProxy;
        public WebsiteResourceFilesProxy WebsiteResourceFilesProxy;
        public WebsitePagesProxy WebsitePagesProxy;
        public WebsiteUserLoginProxy WebsiteUserLoginProxy;
        public DataFormsProxy DataFormsProxy;
        public HandlersProxy HandlersProxy;
        public DataViewProxy DataViewProxy;
        public AttachmentsProxy AttachmentsProxy;

        public WebAPIHelper()
        {
            chie = new CHIEProxy();
            Security = new SecurityProxy();
            ScheduleJob = new ScheduleJobProxy();
            FinanceProxy = new FinanceProxy();
            GetFile = new GetFileProxy();
            WorkflowJob = new WorkflowJobProxy();
            PortalSecurityProxy = new PortalSecurityProxy();
            WebsitePointsOfContactProxy = new WebsitePointsOfContactProxy();
            WebsiteFeedbackProxy = new WebsiteFeedbackProxy();
            WebsiteProxy = new WebsiteProxy();
            WebsiteAnnouncementsProxy = new WebsiteAnnouncementsProxy();
            FAQProxy = new FAQProxy();
            WebsiteContactProxy = new WebsiteContactProxy();
            WebsiteResourceFilesProxy = new WebsiteResourceFilesProxy();
            WebsitePagesProxy = new WebsitePagesProxy();
            WebsiteUserLoginProxy = new WebsiteUserLoginProxy();
            DataFormsProxy = new DataFormsProxy();
            HandlersProxy = new HandlersProxy();
            DataViewProxy = new DataViewProxy();
            AttachmentsProxy = new AttachmentsProxy();
        }
    }
}
