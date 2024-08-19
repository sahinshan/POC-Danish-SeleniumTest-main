using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class FAQTests: UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5650

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6332")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/top-ten' - " +
            "Supply the website id linked to an Application that is linked to more than 10 FAQs - " +
            "validate that the 10 most upvoted FAQs are returned by the service")]
        public void FAQ_TopTen_TestMethod001()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13

            var allRecords = this.WebAPIHelper.FAQProxy.TopTen(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(10, allRecords.Count);

            Assert.AreEqual(new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"), allRecords[0].Id);
            Assert.AreEqual("Security-Question-SEC21", allRecords[0].SEOName);
            Assert.AreEqual("Security - Question SEC21", allRecords[0].Title);
            Assert.AreEqual(null, allRecords[0].Contents);
            Assert.AreEqual("security", allRecords[0].CategorySEOName);

            Assert.AreEqual(new Guid("a6dcbfd0-edc2-440d-80ca-7e988ad97674"), allRecords[1].Id);
            Assert.AreEqual("Security-Question-SEC20", allRecords[1].SEOName);
            Assert.AreEqual("Security - Question SEC20", allRecords[1].Title);
            Assert.AreEqual(null, allRecords[1].Contents);
            Assert.AreEqual("security", allRecords[1].CategorySEOName);

            Assert.AreEqual(new Guid("295c069c-dcac-475e-bd26-d36ee5559a7a"), allRecords[2].Id);
            Assert.AreEqual("Security-Question-SEC19", allRecords[2].SEOName);
            Assert.AreEqual("Security - Question SEC19", allRecords[2].Title);
            Assert.AreEqual(null, allRecords[2].Contents);
            Assert.AreEqual("security", allRecords[2].CategorySEOName);

            Assert.AreEqual(new Guid("91c04640-b99f-45dc-b3ae-b5348c6033c3"), allRecords[3].Id);
            Assert.AreEqual("Security-Question-SEC18", allRecords[3].SEOName);
            Assert.AreEqual("Security - Question SEC18", allRecords[3].Title);
            Assert.AreEqual(null, allRecords[3].Contents);
            Assert.AreEqual("security", allRecords[3].CategorySEOName);

            Assert.AreEqual(new Guid("492d5e3b-ad0d-45fc-a646-938ec55578ef"), allRecords[4].Id);
            Assert.AreEqual("Security-Question-SEC17", allRecords[4].SEOName);
            Assert.AreEqual("Security - Question SEC17", allRecords[4].Title);
            Assert.AreEqual(null, allRecords[4].Contents);
            Assert.AreEqual("security", allRecords[4].CategorySEOName);

            Assert.AreEqual(new Guid("9ce5c961-12c7-41f8-918c-9d0bfd70a736"), allRecords[5].Id);
            Assert.AreEqual("Security-Question-SEC16", allRecords[5].SEOName);
            Assert.AreEqual("Security - Question SEC16", allRecords[5].Title);
            Assert.AreEqual(null, allRecords[5].Contents);
            Assert.AreEqual("security", allRecords[5].CategorySEOName);

            Assert.AreEqual(new Guid("8a9eca3c-f34d-41ea-8418-753c5309d24b"), allRecords[6].Id);
            Assert.AreEqual("Security-Question-SEC15", allRecords[6].SEOName);
            Assert.AreEqual("Security - Question SEC15", allRecords[6].Title);
            Assert.AreEqual(null, allRecords[6].Contents);
            Assert.AreEqual("security", allRecords[6].CategorySEOName);

            Assert.AreEqual(new Guid("c6bf043c-9dee-426e-8fde-c6dff93cf0a5"), allRecords[7].Id);
            Assert.AreEqual("Security-Question-SEC14", allRecords[7].SEOName);
            Assert.AreEqual("Security - Question SEC14", allRecords[7].Title);
            Assert.AreEqual(null, allRecords[7].Contents);
            Assert.AreEqual("security", allRecords[7].CategorySEOName);

            Assert.AreEqual(new Guid("a106cf39-30f2-4883-b213-ac0b961cb2d6"), allRecords[8].Id);
            Assert.AreEqual("Security-Question-SEC13", allRecords[8].SEOName);
            Assert.AreEqual("Security - Question SEC13", allRecords[8].Title);
            Assert.AreEqual(null, allRecords[8].Contents);
            Assert.AreEqual("security", allRecords[8].CategorySEOName);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6333")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/top-ten' - " +
            "Supply the website id linked to an Application that is linked to less than 10 FAQs - " +
            "validate that all linked FAQs are returned")]
        public void FAQ_TopTen_TestMethod002()
        {
            var websiteid = new Guid("c8a79de2-602e-eb11-a2cf-005056926fe4"); //Automation - Web Site 14

            var allRecords = this.WebAPIHelper.FAQProxy.TopTen(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(2, allRecords.Count);

            Assert.AreEqual(new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"), allRecords[0].Id);
            Assert.AreEqual("Security-Question-SEC21", allRecords[0].SEOName);
            Assert.AreEqual("Security - Question SEC21", allRecords[0].Title);
            Assert.AreEqual(null, allRecords[0].Contents);
            Assert.AreEqual("security", allRecords[0].CategorySEOName);

            Assert.AreEqual(new Guid("a6dcbfd0-edc2-440d-80ca-7e988ad97674"), allRecords[1].Id);
            Assert.AreEqual("Security-Question-SEC20", allRecords[1].SEOName);
            Assert.AreEqual("Security - Question SEC20", allRecords[1].Title);
            Assert.AreEqual(null, allRecords[1].Contents);
            Assert.AreEqual("security", allRecords[1].CategorySEOName);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6334")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/top-ten' - " +
            "Supply the website id linked to an Application that is NOT linked to any FAQs - " +
            "validate that no data is returned by the service")]
        public void FAQ_TopTen_TestMethod003()
        {
            var websiteid = new Guid("9e4c8e6c-0b2f-eb11-a2d0-005056926fe4"); //Automation - Web Site 15

            var allRecords = this.WebAPIHelper.FAQProxy.TopTen(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6335")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/top-ten' - " +
            "Supply a non existing website id  - " +
            "validate that no data is returned by the service")]
        public void FAQ_TopTen_TestMethod004()
        {
            var websiteid = new Guid("11111330-6e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 02

            var allRecords = this.WebAPIHelper.FAQProxy.TopTen(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);

        }



        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6336")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/categories' - " +
            "Supply the website id linked to an Application that is linked to FAQs associated with different FAQ Categories - " +
            "validate that all FAQs Categories are returned by the service")]
        public void FAQ_Categories_TestMethod001()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13

            var allRecords = this.WebAPIHelper.FAQProxy.Categories(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(5, allRecords.Count);

            Assert.AreEqual(new Guid("1c09a097-effc-ea11-a2cd-005056926fe4"), allRecords[0].Id);
            Assert.AreEqual("financial-assessment", allRecords[0].SEOName);
            Assert.AreEqual("Financial Assessment", allRecords[0].Name);

            Assert.AreEqual(new Guid("2009a097-effc-ea11-a2cd-005056926fe4"), allRecords[1].Id);
            Assert.AreEqual("security", allRecords[1].SEOName);
            Assert.AreEqual("Security", allRecords[1].Name);

            Assert.AreEqual(new Guid("a4f070a1-effc-ea11-a2cd-005056926fe4"), allRecords[2].Id);
            Assert.AreEqual("forms", allRecords[2].SEOName);
            Assert.AreEqual("Forms", allRecords[2].Name);

            Assert.AreEqual(new Guid("669795a8-effc-ea11-a2cd-005056926fe4"), allRecords[3].Id);
            Assert.AreEqual("finance-transactions", allRecords[3].SEOName);
            Assert.AreEqual("Finance Transactions", allRecords[3].Name);

            Assert.AreEqual(new Guid("50c8d1b2-effc-ea11-a2cd-005056926fe4"), allRecords[4].Id);
            Assert.AreEqual("financial-details", allRecords[4].SEOName);
            Assert.AreEqual("Financial Details", allRecords[4].Name);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6337")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/categories' - " +
            "Supply the website id linked to an Application that is not linked to any FAQs - " +
            "validate that no data is returned by the service")]
        public void FAQ_Categories_TestMethod002()
        {
            var websiteid = new Guid("9e4c8e6c-0b2f-eb11-a2d0-005056926fe4"); //Automation - Web Site 02

            var allRecords = this.WebAPIHelper.FAQProxy.Categories(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6338")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/categories' - " +
            "Supply a non existing website id " +
            "validate that no data is returned by the service")]
        public void FAQ_Categories_TestMethod003()
        {
            var websiteid = new Guid("11111130-6e18-eb11-a2cd-005056926fe4"); //non existing website

            var allRecords = this.WebAPIHelper.FAQProxy.Categories(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, allRecords.Count);
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6339")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/{category}/{name}' - " +
            "Supply the website id a valid Category SEO and a valid FAQ SEO - " +
            "validate that the matching FAQ is returned")]
        public void FAQ_GetFAQByCategorySEOAndFAQSEO_TestMethod001()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13
            string categorySEO = "security";
            string FAQSEO = "Security-Question-SEC21";

            var record = this.WebAPIHelper.FAQProxy.GetByCategorySEONameAndFAQSEOName(websiteid, categorySEO, FAQSEO, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(new Guid("04469a2d-6c9d-421c-877e-c5f9d148818b"), record.Id);
            Assert.AreEqual("Security-Question-SEC21", record.SEOName);
            Assert.AreEqual("Security - Question SEC21", record.Title);
            Assert.AreEqual("<p>SEC21 Line 1</p>\n\n<p>SEC21 Line 2</p>\n\n<p>SEC21 Line 3</p>\n\n<p>SEC21 Line 4</p>\n\n<p>SEC21 Line 5</p>", record.Contents);
            Assert.AreEqual("security", record.CategorySEOName);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6340")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/{category}/{name}' - " +
            "Supply the website id a valid Category SEO and an INVALID FAQ SEO - " +
            "validate that no data is returned")]
        public void FAQ_GetFAQByCategorySEOAndFAQSEO_TestMethod002()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13
            string categorySEO = "security";
            string FAQSEO = "Security-Question-SEC22INVALID";

            var record = this.WebAPIHelper.FAQProxy.GetByCategorySEONameAndFAQSEOName(websiteid, categorySEO, FAQSEO, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(null, record);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6341")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/{category}/{name}' - " +
            "Supply the website id an INVALID Category SEO and a valid FAQ SEO - " +
            "validate that no data is returned")]
        public void FAQ_GetFAQByCategorySEOAndFAQSEO_TestMethod003()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13
            string categorySEO = "securityinvalid";
            string FAQSEO = "Security-Question-SEC22";

            var record = this.WebAPIHelper.FAQProxy.GetByCategorySEONameAndFAQSEOName(websiteid, categorySEO, FAQSEO, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(null, record);

        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6342")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/{category}' - " +
            "Supply the website id - Supply a valid Category SEO - " +
            "validate that the matching FAQs are returned")]
        public void FAQ_GetFAQByCategorySEO_TestMethod001()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13
            string categorySEO = "financial-assessment";

            var records = this.WebAPIHelper.FAQProxy.GetByCategorySEOName(websiteid, categorySEO, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(2, records.Count);

            Assert.AreEqual(new Guid("037b6b5f-a508-eb11-a2cd-005056926fe4"), records[0].Id);
            Assert.AreEqual(null, records[0].SEOName);
            Assert.AreEqual("Finance Assessment - Question FA2", records[0].Title);
            Assert.AreEqual(null, records[0].Contents);
            Assert.AreEqual("financial-assessment", records[0].CategorySEOName);

            Assert.AreEqual(new Guid("c5def971-a308-eb11-a2cd-005056926fe4"), records[1].Id);
            Assert.AreEqual(null, records[1].SEOName);
            Assert.AreEqual("Finance Assessment - Question FA1", records[1].Title);
            Assert.AreEqual(null, records[1].Contents);
            Assert.AreEqual("financial-assessment", records[1].CategorySEOName);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6343")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5650 - " +
            "Perform a call to 'portal/{websiteid}/faqs/{category}' - " +
            "Supply the website id - Supply a Category SEO name that will not match any of the valid ones for the website - " +
            "validate that no data is returned")]
        public void FAQ_GetFAQByCategorySEO_TestMethod002()
        {
            var websiteid = new Guid("7c72aa70-4e2e-eb11-a2cf-005056926fe4"); //Automation - Web Site 13
            string categorySEO = "financial-assessment-invalid";

            var records = this.WebAPIHelper.FAQProxy.GetByCategorySEOName(websiteid, categorySEO, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.AreEqual(0, records.Count);

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
