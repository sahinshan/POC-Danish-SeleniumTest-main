using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteContactTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-6145

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6348")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-6145 - " +
            "Perform a call to 'portal/{websiteid}/contact' - Send the contact data with all fields set - Validate that the record is correctly saved in the CareDirector side")]
        public void WebsiteContact_TestMethod001()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitepointofcontactid = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            var contact = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteContact
            {
                name = "Website 07 - Contact 04",
                email = "person@mail.com",
                message = "contact message goes here ....",
                subject = "Website Contact Test Method 001",
                websitepointofcontactid = websitepointofcontactid
            };

            Guid contactid = WebAPIHelper.WebsiteContactProxy.CreateContact(contact, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);

            var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.websiteContact.GetByID(records[0], "name", "subject", "emailaddress", "websitepointofcontactid", "ownerid", "websiteid", "message");
            Assert.AreEqual(contactid, fields["websitecontactid"]);
            Assert.AreEqual(contact.email, fields["emailaddress"]);
            Assert.AreEqual(contact.name, fields["name"]);
            Assert.AreEqual(contact.subject, fields["subject"]);
            Assert.AreEqual(contact.websitepointofcontactid, fields["websitepointofcontactid"]);
            Assert.AreEqual(websiteID, fields["websiteid"]);
            Assert.AreEqual(contact.message, fields["message"]);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6350")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-6145 - " +
            "Perform a call to 'portal/{websiteid}/contact' - Send the contact data with all fields set except for Email - Validate that the record is not saved")]
        public void WebsiteContact_TestMethod003()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitepointofcontactid = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            var contact = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteContact
            {
                name = "Website 07 - Contact 04",
                email = "",
                message = "contact message goes here ....",
                subject = "Website Contact Test Method 003",
                websitepointofcontactid = websitepointofcontactid
            };

            try
            {
                this.WebAPIHelper.WebsiteContactProxy.CreateContact(contact, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Email.\"}", ex.Message);
                var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6351")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-6145 - " +
            "Perform a call to 'portal/{websiteid}/contact' - Send the contact data with all fields set except for Message - Validate that the record is not saved")]
        public void WebsiteContact_TestMethod004()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitepointofcontactid = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            var contact = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteContact
            {
                name = "Website 07 - Contact 04",
                email = "person@mail.com",
                message = "",
                subject = "Website Contact Test Method 004",
                websitepointofcontactid = websitepointofcontactid
            };

            try
            {
                this.WebAPIHelper.WebsiteContactProxy.CreateContact(contact, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Message.\"}", ex.Message);
                var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6352")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-6145 - " +
            "Perform a call to 'portal/{websiteid}/contact' - Send the contact data with all fields set except for Subject - Validate that the record is not saved")]
        public void WebsiteContact_TestMethod005()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitepointofcontactid = new Guid("09a2c43f-c428-eb11-a2cd-005056926fe4"); //Generic Opinion

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            var contact = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteContact
            {
                name = "Website 07 - Contact 04",
                email = "person@mail.com",
                message = "contact message goes here ....",
                subject = "",
                websitepointofcontactid = websitepointofcontactid
            };

            try
            {
                this.WebAPIHelper.WebsiteContactProxy.CreateContact(contact, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Please provide non-empty string for Subject.\"}", ex.Message);
                var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6353")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-6145 - " +
            "Perform a call to 'portal/{websiteid}/contact' - Send the contact data with all fields set but with an invalid Point of Contact - Validate that the record is not saved")]
        public void WebsiteContact_TestMethod006()
        {
            var websiteID = new Guid("ccbb9db6-2e19-eb11-a2cd-005056926fe4"); //Automation - Web Site 07
            var websitepointofcontactid = new Guid("11aaa11a-c428-eb11-a2cd-005056926fe4"); //invalid value for the point of contact

            //remove all matching Contacts
            foreach (var recordid in dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04"))
                dbHelper.websiteContact.DeleteWebsiteContact(recordid);

            var contact = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteContact
            {
                email = "person@mail.com",
                message = "contact message goes here ....",
                subject = "Website 07 - Contact 04",
                websitepointofcontactid = websitepointofcontactid
            };

            try
            {
                this.WebAPIHelper.WebsiteContactProxy.CreateContact(contact, websiteID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"Related Point of Contact could not be found\"}", ex.Message);
                var records = dbHelper.websiteContact.GetByWebSiteIDAndName(websiteID, "Website 07 - Contact 04");
                Assert.AreEqual(0, records.Count);
                return;
            }

            Assert.Fail("No exception was thrown.");
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
