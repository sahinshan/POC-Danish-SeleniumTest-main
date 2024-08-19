using Phoenix.UITests.Framework.WebAppAPI.Classes;
using Phoenix.UITests.Framework.WebAppAPI.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI
{
    public class WebAPIHelper
    {
        public AdvanceFinancialsProxy AdvanceFinancials;
        public SecurityProxy Security;
        public AuditProxy Audit;
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
        public CacheMonitorProxy CacheMonitorProxy;
        public DataProxy DataProxy;

        public WebAPIHelper()
        {
            AdvanceFinancials = new AdvanceFinancialsProxy();
            Security = new SecurityProxy();
            ScheduleJob = new ScheduleJobProxy();
            Audit = new AuditProxy();
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
            CacheMonitorProxy = new CacheMonitorProxy();
            DataProxy = new DataProxy();
        }
    }
}
