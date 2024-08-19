using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class AttachmentTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-8272

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9830")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{fileType}/{id}' controller - " +
            "Supply a DocumentFile Id linked to a person attachment (Available On Citizen Portal = Yes) that is linked to the User Person record - " +
            "Validate that the api will return the file content ")]
        public void Attachments_Get_TestMethod001()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            UITests.Framework.WebAppAPI.Entities.Portal.FileType fileType = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            Guid documentFileId = new Guid("4508e04f-4365-eb11-a308-005056926fe4");
            var response = this.WebAPIHelper.AttachmentsProxy.Get(websiteid, type, fileType, documentFileId, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("Line 1\r\nLine 2", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9831")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{fileType}/{id}' controller - " +
            "Supply a DocumentFile Id linked to a person attachment (Available On Citizen Portal = No) that is linked to the User Person record - " +
            "Validate that the api will NOT return the file content")]
        public void Attachments_Get_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            UITests.Framework.WebAppAPI.Entities.Portal.FileType fileType = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            Guid documentFileId = new Guid("717c4013-6265-eb11-a308-005056926fe4");

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Get(websiteid, type, fileType, documentFileId, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id 717c4013-6265-eb11-a308-005056926fe4 and Type personattachment.\",\"errorCode\":\"RecordNotFound\"}", ex.Message);
                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9832")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{fileType}/{id}' controller - " +
            "Supply a DocumentFile Id linked to a person attachment (Available On Citizen Portal = Yes) that is linked to another Person record - " +
            "Validate that the api will NOT return the file content")]
        public void Attachments_Get_TestMethod003()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            UITests.Framework.WebAppAPI.Entities.Portal.FileType fileType = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            Guid documentFileId = new Guid("64448639-6765-eb11-a308-005056926fe4"); //this file is linked to a person attachment that belongs to another person

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Get(websiteid, type, fileType, documentFileId, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Invalid Request\",\"errorCode\":\"NoViewAccess\"}", ex.Message);
                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9833")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{fileType}/{id}' controller - " +
            "Supply a non existing DocumentFile Id -" +
            "Validate that the api will return an exception message")]
        public void Attachments_Get_TestMethod004()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            UITests.Framework.WebAppAPI.Entities.Portal.FileType fileType = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            Guid documentFileId = Guid.NewGuid();

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Get(websiteid, type, fileType, documentFileId, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + documentFileId.ToString() + " and Type personattachment.\",\"errorCode\":\"RecordNotFound\"}", ex.Message);
                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }




        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9834")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has no data in AttachmentLimitDates field - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod005()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9835")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 1 date in the last 24 hours on the AttachmentLimitDates field - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod006()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 1; i <= 1; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9836")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 9 date in the last 24 hours on the AttachmentLimitDates field - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod007()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 15; i <= 23; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9837")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 10 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return false")]
        public void Attachments_CheckUploadLimit_TestMethod008()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 14; i <= 23; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You have reached the maximum number of uploaded files allowed in 24 hours.\",\"errorCode\":\"AttachmentXHourLimit\"}", ex.Message);

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9838")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 9 date in the last 24 hours and 5 dates after the last 24 hours on the AttachmentLimitDates field - " +
            "In this test the dates are saved in the ascending order" +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod009()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");


            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");

            for (var i = 30; i >= 26; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            for (var i = 23; i >= 15; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9839")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 9 date in the last 24 hours and 5 dates after the last 24 hours on the AttachmentLimitDates field - " +
            "In this test the dates are saved in the descending order" +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod010()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");

            for (var i = 15; i <= 23; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            for (var i = 26; i <= 30; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9840")]
        [Description("'Attachments.HoursUploadsLimitation' = 24 - 'Attachments.MaximumUserUploadsPerXHours' = 10  - Logged in user has 9 date in the last 24 hours and 5 dates after the last 24 hours on the AttachmentLimitDates field - " +
            "In this test the dates are saved in randomized order" +
            "Perform a call to the 'api/portal/{websiteId:Guid}/attachment/CheckUploadLimit' API - " +
            "Validate that the API will return true")]
        public void Attachments_CheckUploadLimit_TestMethod011()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");

            for (var i = 15; i <= 16; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            for (var i = 26; i <= 30; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            for (var i = 17; i <= 23; i++)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");

            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var response = this.WebAPIHelper.AttachmentsProxy.CheckUploadLimit(websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("true", response);
        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9841")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Set type to 'personattachment' - set the person id linked to the website user - Supply a valid text file - " +
            "Validate that a new person attachment record is created and linked to the specified person record")]
        public void Attachments_Post_TestMethod001()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.IsNotNull(response);

            var attachments = dbHelper.personAttachment.GetByPersonID(personID);
            Assert.AreEqual(1, attachments.Count);

            var documentTypeid = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents
            var documentSubTypeid = new Guid("20f7f73e-0466-eb11-a308-005056926fe4"); //Independent Living Grant

            var fields = dbHelper.personAttachment.GetByID(attachments[0], "title", "documenttypeid", "documentsubtypeid", "fileid");
            Assert.AreEqual("DocToUpload.txt", fields["title"]);
            Assert.AreEqual(documentTypeid, fields["documenttypeid"]);
            Assert.AreEqual(documentSubTypeid, fields["documentsubtypeid"]);
            Assert.AreEqual(new Guid(response.Replace("\"", "")), fields["fileid"]);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9842")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Set type to 'personattachment' - set the person id linked to the website user - Supply a text file with a size greater than 1 Megabyte - " +
            "Validate that the API will return an error preventing the attachment from being created")]
        public void Attachments_Post_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\LargeDocToUpload.txt";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"File cannot exceed 1024 KB. Value Provided: 2009\",\"errorCode\":\"MaxSizeExceed\"}", ex.Message);

                var attachments = dbHelper.personAttachment.GetByPersonID(personID);
                Assert.AreEqual(0, attachments.Count);
                return;
            }

            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9843")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Supply an empty parentid value - " +
            "Validate that the API will return an error preventing the attachment from being created")]
        public void Attachments_Post_TestMethod003()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, Guid.Empty, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Please provide non-empty GUID value for Parent Id.\",\"errorCode\":\"ProvideNonEmptyGuidValue\"}", ex.Message);

                var attachments = dbHelper.personAttachment.GetByPersonID(personID);
                Assert.AreEqual(0, attachments.Count);
                return;
            }

            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9844")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Supply an invalid parentfieldname value - " +
            "Validate that the API will return an error preventing the attachment from being created")]
        public void Attachments_Post_TestMethod004()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personidInvalid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Please provide a non null value for Person.\",\"errorCode\":\"ProvideNonNullValue\"}", ex.Message);

                var attachments = dbHelper.personAttachment.GetByPersonID(personID);
                Assert.AreEqual(0, attachments.Count);
                return;
            }

            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9845")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Supply an invalid type value - " +
            "Validate that the API will return an error preventing the attachment from being created")]
        public void Attachments_Post_TestMethod005()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachmentINVALID";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unable to find the requested business object personattachmentINVALID.\",\"errorCode\":\"BusinessObjectNotFound\"}", ex.Message);

                var attachments = dbHelper.personAttachment.GetByPersonID(personID);
                Assert.AreEqual(0, attachments.Count);
                return;
            }

            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9846")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}' controller - " +
            "Supply an file that is not part of the valid types - " +
            "Validate that the API will return an error preventing the attachment from being created")]
        public void Attachments_Post_TestMethod006()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett

            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.abcd";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Invalid file extension, only files of type .bmp, .doc, .docm, .docx, .htm, .jpeg, .jpg, .m4a, .mht, .mov, .mp4, .msg, .ods, .odt, .pdf, .png, .ppt, .pptx, .pub, .rtf, .snp, .tif, .tiff, .txt, .url, .xls, .xlsb, .xlsx can be uploaded.\",\"errorCode\":\"FileNotValid\"}", ex.Message);

                var attachments = dbHelper.personAttachment.GetByPersonID(personID);
                Assert.AreEqual(0, attachments.Count);
                return;
            }

            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9848")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{id}' controller - " +
            "Set type to 'personattachment' - set the Person Attachment ID to attach the uploaded file to (person attachment is already linked to a file) - Supply a valid text file - " +
            "Validate that the Put api method will return an error.")]
        public void Attachments_Put_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("28abfca3-de66-eb11-a308-005056926fe4"); //Attachment 002
            var documentFileId = new Guid("2dabfca3-de66-eb11-a308-005056926fe4"); //DocToUpload.Txt


            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Put(websiteid, type, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Provided record is not valid for update.\",\"errorCode\":\"RecordNotValidForUpdate\"}", ex.Message);

                var documentTypeid = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents
                var documentSubTypeid = new Guid("20f7f73e-0466-eb11-a308-005056926fe4"); //Independent Living Grant

                var attachmentFields = dbHelper.personAttachment.GetByID(personAttachmentID, "title", "documenttypeid", "documentsubtypeid", "fileid");
                Assert.AreEqual("Attachment 002", attachmentFields["title"]);
                Assert.AreEqual(documentTypeid, attachmentFields["documenttypeid"]);
                Assert.AreEqual(documentSubTypeid, attachmentFields["documentsubtypeid"]);
                Assert.AreEqual(documentFileId, attachmentFields["fileid"]);

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9849")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{type}/{id}' controller - " +
            "Set type to 'personattachment' - set the Person Attachment ID to attach the uploaded file to (person attachment is not linked to any file) - Supply a file with an invalid extension - " +
            "Validate that the Put api method will return an error")]
        public void Attachments_Put_TestMethod003()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001


            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //unlink any file to the attachment 
            var fields = dbHelper.personAttachment.GetByID(personAttachmentID, "fileid");
            if (fields.ContainsKey("fileid"))
            {
                var fileid = (Guid)fields["fileid"];
                dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);
            }


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.abcd";
            try
            {
                var response = this.WebAPIHelper.AttachmentsProxy.Put(websiteid, type, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Invalid file extension, only files of type .bmp, .doc, .docm, .docx, .htm, .jpeg, .jpg, .m4a, .mht, .mov, .mp4, .msg, .ods, .odt, .pdf, .png, .ppt, .pptx, .pub, .rtf, .snp, .tif, .tiff, .txt, .url, .xls, .xlsb, .xlsx can be uploaded.\",\"errorCode\":\"FileNotValid\"}", ex.Message);

                var documentTypeid = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents
                var documentSubTypeid = new Guid("20f7f73e-0466-eb11-a308-005056926fe4"); //Independent Living Grant

                var attachmentFields = dbHelper.personAttachment.GetByID(personAttachmentID, "title", "documenttypeid", "documentsubtypeid", "fileid");
                Assert.AreEqual("Attachment 001", attachmentFields["title"]);
                Assert.AreEqual(documentTypeid, attachmentFields["documenttypeid"]);
                Assert.AreEqual(documentSubTypeid, attachmentFields["documentsubtypeid"]);
                Assert.IsFalse(attachmentFields.ContainsKey("fileid"));

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }




    

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9851")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{fileType}/{type}/{fileField}/{id}' controller - " +
            "Set type to 'personattachment' - set the Person Attachment ID to attach the uploaded file to (person attachment is already linked to a file) - Supply a valid text file - " +
            "Validate that the person attachment record is linked to the uploaded document file record")]
        public void Attachments_Put2_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("28abfca3-de66-eb11-a308-005056926fe4"); //Attachment 002
            var documentFileId = new Guid("2dabfca3-de66-eb11-a308-005056926fe4"); //DocToUpload.Txt


            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var filetype = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            var type = "personattachment";
            var fileField = "fileid";
            var FilePath = "c:\\temp\\DocToUpload.txt";

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Put(websiteid, filetype, type, fileField, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Provided record is not valid for update.\",\"errorCode\":\"RecordNotValidForUpdate\"}", ex.Message);

                var documentTypeid = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents
                var documentSubTypeid = new Guid("20f7f73e-0466-eb11-a308-005056926fe4"); //Independent Living Grant

                var attachmentFields = dbHelper.personAttachment.GetByID(personAttachmentID, "title", "documenttypeid", "documentsubtypeid", "fileid");
                Assert.AreEqual("Attachment 002", attachmentFields["title"]);
                Assert.AreEqual(documentTypeid, attachmentFields["documenttypeid"]);
                Assert.AreEqual(documentSubTypeid, attachmentFields["documentsubtypeid"]);
                Assert.AreEqual(documentFileId, attachmentFields["fileid"]);

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9852")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/{fileType}/{type}/{fileField}/{id}' controller - " +
            "SetSet type to 'personattachment' - set the Person Attachment ID to attach the uploaded file to (person attachment is not linked to any file) - Supply a file with an invalid extension - " +
            "Validate that the person attachment record is linked to the uploaded document file record")]
        public void Attachments_Put2_TestMethod003()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001



            //Set AttachmentLimitDates to null
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, null);

            //attach a file to the person attachment record
            dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var filetype = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            var type = "personattachment";
            var fileField = "fileid";
            var FilePath = "c:\\temp\\DocToUpload.abcd";

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Put(websiteid, filetype, type, fileField, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Invalid file extension, only files of type .bmp, .doc, .docm, .docx, .htm, .jpeg, .jpg, .m4a, .mht, .mov, .mp4, .msg, .ods, .odt, .pdf, .png, .ppt, .pptx, .pub, .rtf, .snp, .tif, .tiff, .txt, .url, .xls, .xlsb, .xlsx can be uploaded.\",\"errorCode\":\"FileNotValid\"}", ex.Message);

                var documentTypeid = new Guid("62d55830-0466-eb11-a308-005056926fe4"); //All Attached Documents
                var documentSubTypeid = new Guid("20f7f73e-0466-eb11-a308-005056926fe4"); //Independent Living Grant

                var attachmentFields = dbHelper.personAttachment.GetByID(personAttachmentID, "title", "documenttypeid", "documentsubtypeid", "fileid");
                Assert.AreEqual("Attachment 001", attachmentFields["title"]);
                Assert.AreEqual(documentTypeid, attachmentFields["documenttypeid"]);
                Assert.AreEqual(documentSubTypeid, attachmentFields["documentsubtypeid"]);
                Assert.IsFalse(attachmentFields.ContainsKey("fileid"));

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }


        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9853")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/getattachmentinfo/{type}/{id?}' controller - " +
            "Set type to 'personattachment' - set id to a valid person attachment - " +
            "Validate that API will return the attachment information.")]
        public void Attachments_GetAttachmentInfo_TestMethod001()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var personAttachmentID = new Guid("4008e04f-4365-eb11-a308-005056926fe4"); //Attachment 001
            Guid documentFileId = new Guid("4508e04f-4365-eb11-a308-005056926fe4"); //documentfile id

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            var response = this.WebAPIHelper.AttachmentsProxy.GetAttachmentInfo(websiteid, type, personAttachmentID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
                        
            Assert.AreEqual(true, response.CanDownload);
            Assert.AreEqual(false, response.CanUpload);
            Assert.AreEqual(documentFileId, response.FileId);
            Assert.AreEqual("Attachment 001", response.Name);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9854")]
        [Description("Perform a call to the 'api/portal/{websiteId:Guid}/attachment/getattachmentinfo/{type}/{id?}' controller - " +
            "Set type to 'personattachment' - do not send a person attachment id - " +
            "Validate that API will return the attachment information.")]
        public void Attachments_GetAttachmentInfo_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            string type = "personattachment";
            var response = this.WebAPIHelper.AttachmentsProxy.GetAttachmentInfo(websiteid, type, null, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(".bmp, .doc, .docm, .docx, .htm, .jpeg, .jpg, .m4a, .mht, .mov, .mp4, .msg, .ods, .odt, .pdf, .png, .ppt, .pptx, .pub, .rtf, .snp, .tif, .tiff, .txt, .url, .xls, .xlsb, .xlsx", response.AllowedExtensions);
            Assert.AreEqual(1024, response.MaxFileSize);
            Assert.AreEqual(true, response.CanUpload);
            Assert.AreEqual(null, response.Message);
        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8205

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9855")]
        [Description("Logged in user has 10 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user is prevented from using the POST method api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod001()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 10 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 1; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            
            try
            {
                this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You have reached the maximum number of uploaded files allowed in 24 hours.\",\"errorCode\":\"AttachmentXHourLimit\"}", ex.Message);

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9856")]
        [Description("Logged in user has 9 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user can use the POST method api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("6b8793a1-6f65-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser18@mail.com
            Guid personID = new Guid("96291190-ce1e-4d3d-ba7c-94721b8cdc0a"); //Adrienne Bartlett
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 9 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 2; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //remove all attachments for the person
            foreach (var attachmentid in dbHelper.personAttachment.GetByPersonID(personID))
                dbHelper.personAttachment.DeletePersonAttachment(attachmentid);


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser18@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";

            var response = this.WebAPIHelper.AttachmentsProxy.Post(websiteid, type, personID, "personid", FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.IsTrue(!string.IsNullOrEmpty(response));
            
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9857")]
        [Description("Logged in user has 10 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user is prevented from using the PUT method api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod003()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 10 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 1; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //unlink any file to the attachment 
            var fields = dbHelper.personAttachment.GetByID(personAttachmentID, "fileid");
            if (fields.ContainsKey("fileid"))
            {
                var fileid = (Guid)fields["fileid"];
                dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);
            }


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Put(websiteid, type, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You have reached the maximum number of uploaded files allowed in 24 hours.\",\"errorCode\":\"AttachmentXHourLimit\"}", ex.Message);

                return;
            }
            Assert.Fail("No exception was thrown by the API");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9858")]
        [Description("Logged in user has 9 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user case use the PUT method api/portal/{websiteId:Guid}/attachment/{type}/{parentid:Guid}/{parentfieldname}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod004()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 10 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 2; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //unlink any file to the attachment 
            var fields = dbHelper.personAttachment.GetByID(personAttachmentID, "fileid");
            if (fields.ContainsKey("fileid"))
            {
                var fileid = (Guid)fields["fileid"];
                dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);
            }


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var type = "personattachment";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            var response = this.WebAPIHelper.AttachmentsProxy.Put(websiteid, type, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.IsTrue(!string.IsNullOrEmpty(response));
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9859")]
        [Description("Logged in user has 10 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user is prevented from using the PUT method api/portal/{websiteId:Guid}/attachment/{fileType}/{type}/{fileField}/{id}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod005()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 10 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 1; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //unlink any file to the attachment 
            var fields = dbHelper.personAttachment.GetByID(personAttachmentID, "fileid");
            if (fields.ContainsKey("fileid"))
            {
                var fileid = (Guid)fields["fileid"];
                dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);
            }


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var filetype = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            var type = "personattachment";
            var fileField = "fileid";
            var FilePath = "c:\\temp\\DocToUpload.txt";

            try
            {
                this.WebAPIHelper.AttachmentsProxy.Put(websiteid, filetype, type, fileField, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You have reached the maximum number of uploaded files allowed in 24 hours.\",\"errorCode\":\"AttachmentXHourLimit\"}", ex.Message);

                return;
            }
            Assert.Fail("No exception was thrown by the API");

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9860")]
        [Description("Logged in user has 9 dates in the last 24 hours on the AttachmentLimitDates field - " +
            "Validate that the user case use the PUT method api/portal/{websiteId:Guid}/attachment/{fileType}/{type}/{fileField}/{id}")]
        public void Attachments_LimitTheNumberOfUploads_TestMethod006()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserId = new Guid("9d838dc3-0d66-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser19@mail.com
            var personID = new Guid("103c13a5-5a06-4014-ac12-f167d6ac35b5"); //Benita Thomas
            var personAttachmentID = new Guid("95771cf9-0d66-eb11-a308-005056926fe4"); //Attachment 001
            var websiteSetting = new Guid("21a33183-9c60-eb11-a311-0050569231cf"); //Attachments.MaximumUserUploadsPerXHours

            dbHelper.websiteSetting.UpdateWebsiteSettingValue(websiteSetting, "10");

            //set 10 uploads in the last 24 hours
            var AttachmentLimitDates = new StringBuilder();
            AttachmentLimitDates.Append("<ArrayOfDateTime>");
            for (var i = 10; i >= 2; i--)
                AttachmentLimitDates.Append("<dateTime>" + DateTime.Now.AddHours(i * -1).ToString("o") + "</dateTime>");
            AttachmentLimitDates.Append("</ArrayOfDateTime>");

            //Set AttachmentLimitDates
            dbHelper.websiteUser.UpdateAttachmentLimitDates(websiteUserId, AttachmentLimitDates.ToString());


            //unlink any file to the attachment 
            var fields = dbHelper.personAttachment.GetByID(personAttachmentID, "fileid");
            if (fields.ContainsKey("fileid"))
            {
                var fileid = (Guid)fields["fileid"];
                dbHelper.personAttachment.UpdateFileId(personAttachmentID, null);
            }


            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser19@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var filetype = UITests.Framework.WebAppAPI.Entities.Portal.FileType.Document;
            var type = "personattachment";
            var fileField = "fileid";
            var FilePath = "c:\\temp\\DocToUpload.txt";
            var response = this.WebAPIHelper.AttachmentsProxy.Put(websiteid, filetype, type, fileField, personAttachmentID, FilePath, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.IsTrue(!string.IsNullOrEmpty(response));
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
