using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteResourceFilesTests: UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5091

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6375")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5091 - " +
            "Perform a call to 'portal/{websiteid}/websiteresources/{resourcefilename}' - Supply the website id (with multiple Resource Files) - Supply the resourcefilename (css file) - " +
            "validate that the service will return the correct file content")]
        public void WebsiteResourceFiles_TestMethod001()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            string resourcefilename = "site 01 file.css";

            var filecontent = this.WebAPIHelper.WebsiteResourceFilesProxy.GetResourceFileByName(websiteid, resourcefilename, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual("css file line 1\r\ncss file line 2", filecontent);
        }
        
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6376")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5091 - " +
            "Perform a call to 'portal/{websiteid}/websiteresources/{resourcefilename}' - Supply the website id (with multiple Resource Files) - Supply the resourcefilename (js file) - " +
            "validate that the service will return the correct file content")]
        public void WebsiteResourceFiles_TestMethod002()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            string resourcefilename = "site 01 file.js";

            var filecontent = this.WebAPIHelper.WebsiteResourceFilesProxy.GetResourceFileByName(websiteid, resourcefilename, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual("js file line 1\r\njs file line 2", filecontent);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6377")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5091 - " +
            "Perform a call to 'portal/{websiteid}/websiteresources/{resourcefilename}' - Supply the website id (with multiple Resource Files) - Supply the resourcefilename (js file) - " +
            "validate that the service will return the correct file content")]
        public void WebsiteResourceFiles_TestMethod003()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            string resourcefilename = "site 01 file.htm";

            var filecontent = this.WebAPIHelper.WebsiteResourceFilesProxy.GetResourceFileByName(websiteid, resourcefilename, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual("<!DOCTYPE html>\r\n<html>\r\n<body>\r\n\r\n<h1>My First Heading</h1>\r\n<p>My first paragraph.</p>\r\n\r\n</body>\r\n</html>", filecontent);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6378")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5091 - " +
            "Perform a call to 'portal/{websiteid}/websiteresources/{resourcefilename}' - Supply the website id (with multiple Resource Files) - Supply a resourcefilename that will not match any resource file for the website - " +
            "validate that the service will return a 404 Status")]
        [ExpectedException(typeof(System.Exception))]
        public void WebsiteResourceFiles_TestMethod004()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            string resourcefilename = "site 09 file.css"; //this resource file do not exist

            var filecontent = this.WebAPIHelper.WebsiteResourceFilesProxy.GetResourceFileByName(websiteid, resourcefilename, this.WebAPIHelper.PortalSecurityProxy.Token);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6379")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5091 - " +
            "Perform a call to 'portal/{websiteid}/websiteresources/{resourcefilename}' - Supply the website id that will not match any website - " +
            "validate that the service will return a 404 Status")]
        [ExpectedException(typeof(System.Exception))]
        public void WebsiteResourceFiles_TestMethod005()
        {
            var websiteid = new Guid("1111c665-4e18-eb11-a2cd-005056926fe4"); //non existing website
            string resourcefilename = "site 01 file.css";

            var filecontent = this.WebAPIHelper.WebsiteResourceFilesProxy.GetResourceFileByName(websiteid, resourcefilename, this.WebAPIHelper.PortalSecurityProxy.Token);
        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
