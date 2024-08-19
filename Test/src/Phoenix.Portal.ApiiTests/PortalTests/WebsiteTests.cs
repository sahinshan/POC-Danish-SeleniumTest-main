using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteTests: UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5654

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6380")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
            "Perform a call to 'services/api/portal/{websiteid}' - supply a websiteid for a website that have all fields set - " +
            "Validate that the website information returned is the correct one")]
        public void Website_TestMethod001()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01

            var websiteInfo = this.WebAPIHelper.WebsiteProxy.GetWebsiteInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            var expectedApplicationID = new Guid("393e0925-f418-e511-80cf-00505605009e");
            var expectedUserRecordType = "person";

            Assert.AreEqual(websiteid, websiteInfo.PublishedWebsiteId);
            Assert.AreEqual("Automation - Web Site 01", websiteInfo.Name);
            Assert.AreEqual("Page_1", websiteInfo.HomePage);
            Assert.AreEqual("Page_1_1", websiteInfo.MemberHomePage);
            Assert.AreEqual("Automation - Widget Footer 1", websiteInfo.Footer);
            Assert.AreEqual("site 01 file.css", websiteInfo.Stylesheet);
            Assert.AreEqual("site 01 file.js", websiteInfo.Script);
            Assert.AreEqual("Logo.png", websiteInfo.Logo);
            Assert.AreEqual(expectedApplicationID, websiteInfo.ApplicationId);
            Assert.AreEqual(expectedUserRecordType, websiteInfo.UserRecordType);
            
            Assert.AreEqual("http://AutomationWebSite01.com/", websiteInfo.Administration.WebsiteUrl);
            Assert.AreEqual(true, websiteInfo.Administration.IsVerificationEmailRequired);
            Assert.AreEqual(true, websiteInfo.Administration.IsUserApprovalRequired);

            Assert.AreEqual(7, websiteInfo.PasswordComplexity.MinimumPasswordLength);
            Assert.AreEqual(2, websiteInfo.PasswordComplexity.MinimumSpecialCharacters);
            Assert.AreEqual(1, websiteInfo.PasswordComplexity.MinimumNumericCharacters);
            Assert.AreEqual(3, websiteInfo.PasswordComplexity.MinimumUppercaseLetters);

            Assert.AreEqual(365, websiteInfo.PasswordPolicy.MaximumPasswordAge);
            Assert.AreEqual(5, websiteInfo.PasswordPolicy.EnforcePasswordHistory);
            Assert.AreEqual(30, websiteInfo.PasswordPolicy.MinimumPasswordAge);
            Assert.AreEqual(8, websiteInfo.PasswordPolicy.PasswordResetExpiresIn);

            Assert.AreEqual(true, websiteInfo.AccountLockoutPolicy.IsAccountLockingEnabled);
            Assert.AreEqual(20, websiteInfo.AccountLockoutPolicy.AccountLockoutDuration);
            Assert.AreEqual(16, websiteInfo.AccountLockoutPolicy.AccountLockoutThreshold);
            Assert.AreEqual(null, websiteInfo.AccountLockoutPolicy.ResetAccountLockoutCounterAfter);

            Assert.AreEqual(true, websiteInfo.TwoFactorAuthentication.IsEnabled);
            Assert.AreEqual(10, websiteInfo.TwoFactorAuthentication.PinExpiresIn);
            Assert.AreEqual(2, websiteInfo.TwoFactorAuthentication.DefaultPinReceivingMethodId);
            Assert.AreEqual(5, websiteInfo.TwoFactorAuthentication.NumberOfPinDigits);





        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6381")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
            "Perform a call to 'services/api/portal/{websiteid}' - supply a websiteid for a website that have only the mandatory fields set - " +
            "Validate that the website information returned is the correct one")]
        public void Website_TestMethod002()
        {
            var websiteid = new Guid("45d4158a-7e2a-eb11-a2cd-005056926fe4"); //Automation - Web Site 12

            var websiteInfo = this.WebAPIHelper.WebsiteProxy.GetWebsiteInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);

            var expectedApplicationID = new Guid("393e0925-f418-e511-80cf-00505605009e");
            var expectedUserRecordType = "accommodationtype";

            Assert.AreEqual(websiteid, websiteInfo.PublishedWebsiteId);
            Assert.AreEqual("Automation - Web Site 12", websiteInfo.Name);
            Assert.AreEqual(null, websiteInfo.HomePage);
            Assert.AreEqual(null, websiteInfo.MemberHomePage);
            Assert.AreEqual(null, websiteInfo.Footer);
            Assert.AreEqual(null, websiteInfo.Stylesheet);
            Assert.AreEqual(null, websiteInfo.Script);
            Assert.AreEqual(null, websiteInfo.Logo);
            Assert.AreEqual(expectedApplicationID, websiteInfo.ApplicationId);
            Assert.AreEqual(expectedUserRecordType, websiteInfo.UserRecordType);

            Assert.AreEqual("http://AutomationWebSite12.com", websiteInfo.Administration.WebsiteUrl);
            Assert.AreEqual(false, websiteInfo.Administration.IsVerificationEmailRequired);
            Assert.AreEqual(false, websiteInfo.Administration.IsUserApprovalRequired);

            Assert.AreEqual(5, websiteInfo.PasswordComplexity.MinimumPasswordLength);
            Assert.AreEqual(null, websiteInfo.PasswordComplexity.MinimumSpecialCharacters);
            Assert.AreEqual(null, websiteInfo.PasswordComplexity.MinimumNumericCharacters);
            Assert.AreEqual(null, websiteInfo.PasswordComplexity.MinimumUppercaseLetters);

            Assert.AreEqual(null, websiteInfo.PasswordPolicy.MaximumPasswordAge);
            Assert.AreEqual(null, websiteInfo.PasswordPolicy.EnforcePasswordHistory);
            Assert.AreEqual(null, websiteInfo.PasswordPolicy.MinimumPasswordAge);
            Assert.AreEqual(5, websiteInfo.PasswordPolicy.PasswordResetExpiresIn);

            Assert.AreEqual(false, websiteInfo.AccountLockoutPolicy.IsAccountLockingEnabled);
            Assert.AreEqual(null, websiteInfo.AccountLockoutPolicy.AccountLockoutDuration);
            Assert.AreEqual(null, websiteInfo.AccountLockoutPolicy.AccountLockoutThreshold);
            Assert.AreEqual(null, websiteInfo.AccountLockoutPolicy.ResetAccountLockoutCounterAfter);

            Assert.AreEqual(false, websiteInfo.TwoFactorAuthentication.IsEnabled);
            Assert.AreEqual(null, websiteInfo.TwoFactorAuthentication.PinExpiresIn);
            Assert.AreEqual(null, websiteInfo.TwoFactorAuthentication.DefaultPinReceivingMethodId);
            Assert.AreEqual(null, websiteInfo.TwoFactorAuthentication.NumberOfPinDigits);





        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6382")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
            "Perform a call to 'services/api/portal/{websiteid}' - supply a websiteid that will not match any website - " +
            "Validate that no website information is returned")]
        public void Website_TestMethod003()
        {
            var websiteid = new Guid("11d4158a-7e2a-eb11-a2cd-005056926fa1"); //invalid website id

            try
            {
                var websiteInfo = this.WebAPIHelper.WebsiteProxy.GetWebsiteInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Object reference not set to an instance of an object.\",\"errorCode\":\"\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-6383")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5654 - " +
            "Perform a call to 'services/api/portal/{websiteid}' - supply an invalid Guid for websiteid - " +
            "Validate that no website information is returned")]
        public void Website_TestMethod004()
        {
            var websiteid = "11d4158a-7e2a-eb11-a2cd-0050569xxxxx"; //invalid website id Guid

            try
            {
                var websiteInfo = this.WebAPIHelper.WebsiteProxy.GetWebsiteInfo(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"Message\":\"The request is invalid.\"}", ex.Message);
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
