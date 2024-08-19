using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class WebsiteUserLogin : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-5593

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9882")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = No - Validate that the authentication succeeds and the access token is returned by the login api")]
        public void WebsiteUserLogin_TestMethod001()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.IsTrue(!string.IsNullOrEmpty(userLoginReturnedData.AccessToken));
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.IsNull(userLoginReturnedData.PinId);
            Assert.IsNull(userLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9883")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply an incorrect password - Validate that the authentication fails and a AuthenticationFailed message is returned and the 'Failed Password Attempt Count' value increases ")]
        public void WebsiteUserLogin_TestMethod002()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11
            var websiteUserId = new Guid("14c3a1c4-c42f-eb11-a2d1-005056926fe4"); //website11user1@mail.com

            //reset 'Failed Password Attempt Count' and 'Last Failed Password Attempt Date'
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserId, 0, null);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user1@mail.com",
                Password = "IncorrectPassw0rd_!"
            };

            try
            {
                WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The user name or password is incorrect.\",\"errorCode\":\"AuthenticationFailed\"}", ex.Message);
                var fields = dbHelper.websiteUser.GetByID(websiteUserId, "failedpasswordattemptcount", "lastfailedpasswordattemptdate");
                Assert.AreEqual(1, fields["failedpasswordattemptcount"]);
                Assert.IsNotNull(fields["lastfailedpasswordattemptdate"]);

                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9884")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - User Account is locked - Validate that the authentication fails and a AuthenticationFailed message is returned")]
        public void WebsiteUserLogin_TestMethod003()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user5@mail.com",
                Password = "Passw0rd_!"
            };

            try
            {
                WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Your account is locked. Please contact your system administrator.\",\"errorCode\":\"AccountLocked\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9885")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - User has reached the max password attempts and LastFailedPasswordAttemptDate is set to DateTime.UTCNow - " +
            "Validate that the authentication fails and a MaxFailedPasswordAccountLockDownPeriod message is returned")]
        public void WebsiteUserLogin_TestMethod004()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11
            var websiteUserID = new Guid("d11e14ff-1c3a-eb11-a2de-005056926fe4"); //website11user6@mail.com

            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserID, 15, DateTime.UtcNow, true);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user6@mail.com",
                Password = "Passw0rd_!"
            };

            try
            {
                var logininfo = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Your account is locked. Please contact your system administrator.\",\"errorCode\":\"AccountLocked\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9886")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - User Account is inactive - Validate that the authentication fails and a AuthenticationFailedInactiveAccount message is returned")]
        public void WebsiteUserLogin_TestMethod005()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user7@mail.com",
                Password = "Passw0rd_!"
            };

            try
            {
                WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Your account is inactive. Please contact your system administrator.\",\"errorCode\":\"AuthenticationFailedInactiveAccount\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9887")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - User Account Email Verified is set to false - Validate that the authentication fails and a AccountEmailNotVerified message is returned")]
        public void WebsiteUserLogin_TestMethod006()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user8@mail.com",
                Password = "Passw0rd_!"
            };

            try
            {
                WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Your account has not yet been verified. A verification email was sent to website11user8@mail.com when your account was created. Click the following link to resend the email.\",\"errorCode\":\"AccountEmailNotVerified\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9888")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - User Account is Waiting approval - Validate that the authentication fails and a AccountNotApproved message is returned")]
        public void WebsiteUserLogin_TestMethod007()
        {
            var websiteid = new Guid("431b80cf-c125-eb11-a2cd-005056926fe4"); //Automation - Web Site 11

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website11user10@mail.com",
                Password = "Passw0rd_!"
            };

            try
            {
                WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Your account status is Waiting for Approval.\",\"errorCode\":\"AccountNotApproved\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9889")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has no Two Factor Authentication Type - " +
            "Validate that the api will not return the token and will return the PinID and Message")]
        public void WebsiteUserLogin_TestMethod008()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("40c7d735-2b3a-eb11-a2de-005056926fe4"); //Website1User1@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website1user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            Assert.IsNull(userLoginReturnedData.AccessToken);
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.AreEqual(userPins[0], userLoginReturnedData.PinId);
            Assert.AreEqual("Please use the PIN that was sent to your phone. Click the following link to resend the PIN.", userLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9890")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to Email - " +
            "Validate that the api will not return the token and will return the PinID and Message")]
        public void WebsiteUserLogin_TestMethod009()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("7a452c1f-2d3a-eb11-a2de-005056926fe4"); //Website1User2@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website1user2@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            Assert.IsNull(userLoginReturnedData.AccessToken);
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.AreEqual(userPins[0], userLoginReturnedData.PinId);
            Assert.AreEqual("Please use the PIN that was sent to your email. Click the following link to resend the PIN.", userLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9891")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Validate that the api will not return the token and will return the PinID and Message")]
        public void WebsiteUserLogin_TestMethod010()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("5f041bb4-2d3a-eb11-a2de-005056926fe4"); //Website1User3@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User3@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            Assert.IsNull(userLoginReturnedData.AccessToken);
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.AreEqual(userPins[0], userLoginReturnedData.PinId);
            Assert.AreEqual("Please use the PIN that was sent to your phone. Click the following link to resend the PIN.", userLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9892")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply the Pin ID and Pin Number - Validate that the API service will return the user access token.")]
        public void WebsiteUserLogin_TestMethod011()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("5f041bb4-2d3a-eb11-a2de-005056926fe4"); //Website1User3@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User3@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber,
                PinId = userLoginReturnedData.PinId.Value
            };

            userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            Assert.IsTrue(!string.IsNullOrEmpty(userLoginReturnedData.AccessToken));
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.IsNull(userLoginReturnedData.PinId);
            Assert.IsNull(userLoginReturnedData.Message);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9893")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply the Pin ID and an incorrect Pin Number - " +
            "Validate that an error message is returned by the service.")]
        public void WebsiteUserLogin_TestMethod012()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("5f041bb4-2d3a-eb11-a2de-005056926fe4"); //Website1User3@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User3@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber + "0",
                PinId = userLoginReturnedData.PinId.Value
            };

            try
            {
                userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Please provide a valid PIN.\",\"errorCode\":\"InvalidPin\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9894")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply a Pin ID and an Pin Number that is expired- " +
            "Validate that an error message is returned by the service.")]
        public void WebsiteUserLogin_TestMethod013()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("5f041bb4-2d3a-eb11-a2de-005056926fe4"); //Website1User3@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User3@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            dbHelper.websiteUserPin.UpdateWebsiteUserPin(userPins[0], DateTime.Now.AddHours(-2)); //set the expire on date to 2 hours in the past, this will make the pin invalid

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber,
                PinId = userLoginReturnedData.PinId.Value
            };

            try
            {
                userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The PIN you have supplied has expired. If you would like to be sent a new PIN, please click the “Reset PIN” button. An updated PIN will be sent to you.\",\"errorCode\":\"InvalidPinExpired\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9895")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply a Pin ID and the Pin Number (user Failed PIN Attempt Count is greater than the maximum allowed value)- " +
            "Validate that an error message is returned by the service.")]
        public void WebsiteUserLogin_TestMethod014()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("f35b9554-333a-eb11-a2de-005056926fe4"); //Website1User4@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            //update user Failed PIN Attempt Count to 6
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserid, 6);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber,
                PinId = userLoginReturnedData.PinId.Value
            };

            try
            {
                userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Maximum number of invalid attempt reached for PIN. Please login again.\",\"errorCode\":\"InvalidPinMaxAttemptReached\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9896")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply a Pin ID and an incorrect Pin Number (user Failed PIN Attempt Count is greater than the maximum allowed value)- " +
            "Validate that an error message is returned by the service.")]
        public void WebsiteUserLogin_TestMethod015()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("f35b9554-333a-eb11-a2de-005056926fe4"); //Website1User4@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            //update user Failed PIN Attempt Count to 6
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserid, 6);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber + "0",
                PinId = userLoginReturnedData.PinId.Value
            };

            try
            {
                userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Maximum number of invalid attempt reached for PIN. Please login again.\",\"errorCode\":\"InvalidPinMaxAttemptReached\"}", ex.Message);
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9897")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "user Failed PIN Attempt Count is greater than the maximum allowed - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply a Pin ID and the Pin Number - " +
            "Validate that the api will return the access token and will reset the PIN Attempt Count to 0")]
        public void WebsiteUserLogin_TestMethod016()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("f35b9554-333a-eb11-a2de-005056926fe4"); //Website1User4@mail.com

            //update user Failed PIN Attempt Count to 6
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserid, 6);

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            //Failed PIN Attempt Count should now be set to 0
            var userRecordFiels = dbHelper.websiteUser.GetByID(websiteUserid, "failedpinattemptcount");
            Assert.IsFalse(userRecordFiels.ContainsKey("failedpinattemptcount"));

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber,
                PinId = userLoginReturnedData.PinId.Value
            };


            userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.IsTrue(!string.IsNullOrEmpty(userLoginReturnedData.AccessToken));
            Assert.IsNull(userLoginReturnedData.ExpireOn);
            Assert.IsNull(userLoginReturnedData.PinId);
            Assert.IsNull(userLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9898")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Set the user Failed PIN Attempt Count to a value greater than the maximum allowed - " +
            "Call the 'users/reissuepin' endpoint - Validate that a new pin id is returned by the API and that the Failed PIN Attempt Count is reset to 0")]
        public void WebsiteUserLogin_TestMethod017()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("f35b9554-333a-eb11-a2de-005056926fe4"); //Website1User4@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            //update user Failed PIN Attempt Count to 6
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserid, 6);

            //re-issue the Pin
            var reissuePincObject = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserReIssuePin() 
            { 
                PinId = userLoginReturnedData.PinId.Value
            };
            var newUserLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ReissuePin(websiteid, reissuePincObject, this.WebAPIHelper.PortalSecurityProxy.Token);

            //new pinid should not match the old one
            Assert.IsTrue(newUserLoginReturnedData.PinId != userLoginReturnedData.PinId);

            //Failed PIN Attempt Count should now be set to 0
            var userRecordFiels = dbHelper.websiteUser.GetByID(websiteUserid, "failedpinattemptcount");
            Assert.AreEqual(0, userRecordFiels["failedpinattemptcount"]);


            var fields = dbHelper.websiteUserPin.GetByID(newUserLoginReturnedData.PinId.Value, "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber,
                PinId = newUserLoginReturnedData.PinId.Value
            };

            newUserLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.IsTrue(!string.IsNullOrEmpty(newUserLoginReturnedData.AccessToken));
            Assert.IsNull(newUserLoginReturnedData.ExpireOn);
            Assert.IsNull(newUserLoginReturnedData.PinId);
            Assert.IsNull(newUserLoginReturnedData.Message);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9899")]
        [Description("Jira issue ID: https://advancedcsg.atlassian.net/browse/CDV6-5593 - " +
            "Login a with a valid website user - Supply the correct password - Website Enable Two Factor Authentication = Yes & Default PIN Receiving Method = SMS - " +
            "User account has Two Factor Authentication Type set to SMS - " +
            "Login and get the Pin ID - Call the 'users/validatepin' endpoint and supply a Pin ID and an incorrect Pin Number - " +
            "validate tha the user failedpinattemptcount value is incremented by 1.")]
        public void WebsiteUserLogin_TestMethod018()
        {
            var websiteid = new Guid("2022c665-4e18-eb11-a2cd-005056926fe4"); //Automation - Web Site 01
            var websiteUserid = new Guid("f35b9554-333a-eb11-a2de-005056926fe4"); //Website1User4@mail.com

            foreach (var pinid in dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid))
                dbHelper.websiteUserPin.DeleteWebsiteUserPin(pinid);

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "Website1User4@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            //update user Failed PIN Attempt Count to 1
            dbHelper.websiteUser.UpdateWebsiteUser(websiteUserid, 1);

            var userPins = dbHelper.websiteUserPin.GetByWebSiteUserID(websiteUserid);
            Assert.AreEqual(1, userPins.Count);

            var fields = dbHelper.websiteUserPin.GetByID(userPins[0], "pin");
            var pinNumber = (string)fields["pin"];

            var userPinRequest = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserPin()
            {
                Pin = pinNumber + "0",
                PinId = userLoginReturnedData.PinId.Value
            };

            try
            {
                userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.ValidatePin(websiteid, userPinRequest, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Please provide a valid PIN.\",\"errorCode\":\"InvalidPin\"}", ex.Message);

                //Failed PIN Attempt Count should now increasde to 2
                var userRecordFiels = dbHelper.websiteUser.GetByID(websiteUserid, "failedpinattemptcount");
                Assert.AreEqual(2, userRecordFiels["failedpinattemptcount"]);

                return;
            }

            Assert.Fail("No exception was thrown");
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
