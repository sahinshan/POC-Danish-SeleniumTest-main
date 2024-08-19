using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class DataFormTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-6145

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9861")]
        [Description("Test for the route '{formId:Guid}' - Try to access a Data Form for a BO associated with the website - Assert that the Data Form info should be returned")]
        public void DataForm_TestMethod001()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var formID = new Guid("3e0a350e-6722-eb11-a2ce-0050569231cf"); // "Registration" data form (person BO)

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            var result = WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, formID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("person", result.BusinessObjectName);
            Assert.AreEqual("Registration", result.Label);
            
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9862")]
        [Description("Test for the route '{formId:Guid}' - Try to access a Data Form for a BO that is not associated with the website - Assert the api will return an exception")]
        public void DataForm_TestMethod002()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var formID = new Guid("c9650a63-c05f-eb11-a310-0050569231cf"); // "PersonAttachment: Portal" data form

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, formID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You don't have create privilege for 'Attachment (For Person)' business object.\",\"errorCode\":\"NoCreateAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9863")]
        [Description("Test for the route '{formId:Guid}/{id:Guid}' - Try to access a Data Form for a BO that is not associated with the website - Assert the api will return an exception")]
        public void DataForm_TestMethod003()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var formID = new Guid("c9650a63-c05f-eb11-a310-0050569231cf"); // "PersonAttachment: Portal" data form
            var personAttachmentID = new Guid("28ABFCA3-DE66-EB11-A308-005056926FE4"); 

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, formID, personAttachmentID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type Attachment (For Person). If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9864")]
        [Description("Test for the route '{formId:Guid}/{id:Guid}' - Try to access a Data Form for a BO that is not associated with the website - supply an id record that will not match any record in the database - Expect the api will return an exception")]
        public void DataForm_TestMethod004()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var formID = new Guid("9E609C4E-8851-EB11-A2FE-0050569231CF"); // "feedbackportal" data form
            var healthAppointmentRecordID = new Guid("AC401829-328C-49CC-8154-D5DD658000AA");

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, formID, healthAppointmentRecordID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id ac401829-328c-49cc-8154-d5dd658000aa and Type WebsiteFeedback.\",\"errorCode\":\"RecordNotFound\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9865")]
        [Description("Test for the route '{businessObjectName}/{formName}' - Try to access a Data Form for a BO that is not associated with the website - Assert the api will return an exception")]
        public void DataForm_TestMethod005()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var businessObjectName = "personfinancialdetail";
            var formName = "PortalBenefit";

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, businessObjectName, formName, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You don't have create privilege for 'Person Financial Detail' business object.\",\"errorCode\":\"NoCreateAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9866")]
        [Description("Test for the route '{businessObjectName}/{formName}/{id:Guid}' - Try to access a Data Form for a BO that is not associated with the website - Assert the api will return an exception")]
        public void DataForm_TestMethod006()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var businessObjectName = "personfinancialdetail";
            var formName = "PortalBenefit";
            var recordID = new Guid("819D53FB-1AE8-403D-8149-00038A8711D9");

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                WebAPIHelper.DataFormsProxy.GetDataForm(websiteID, businessObjectName, formName, recordID, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type Person Financial Detail. If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9867")]
        [Description("Test for the route 'get-by-request' - Try to access a Data Form for a BO that is not associated with the website - supply an id record that will not match any record in the database - Expect the api will return an exception")]
        public void DataForm_TestMethod007()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var businessObjectName = "personfinancialdetail";
            var formName = "PortalBenefit";
            var recordID = new Guid("819D53FB-1AE8-403D-8149-00038A8711D9");

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataFormRequest request = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataFormRequest
                {
                    BusinessObjectName = businessObjectName,
                    DataFormName = formName,
                    RecordId = recordID,
                    FirstView = true,
                };

                WebAPIHelper.DataFormsProxy.GetDataFormByRequest(websiteID, request, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type Person Financial Detail. If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9868")]
        [Description("Test for the route 'save' - Try to access a Data Form for a BO that is not associated with the website - supply an id record that will not match any record in the database - Expect the api will return an exception")]
        public void DataForm_TestMethod008()
        {
            var websiteID = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16
            var businessObjectName = "personfinancialdetail";

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteID, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            try
            {
                UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord record = new UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord();
                record.BusinessObjectName = businessObjectName;
                record.Fields.Add("name", "new website feedback");

                WebAPIHelper.DataFormsProxy.Save(websiteID, record, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"You don't have create privilege for 'PersonFinancialDetail' business object.\",\"errorCode\":\"NoCreateAccess\"}", ex.Message);

                return;
            }

            throw new Exception("No exception was thrown by the api.");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8344

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9869")]
        [Description("Security test for the dataform 'save' API route - Send and update post request for the person record linked to the website user - " +
            "My Record (Citizen Portal) data view should grant the user access to this person record - validate that the update request succeeds")]
        public void PortalSecurity_TestMethod001()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserID = new Guid("23d6635e-4f61-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser16@mail.com
            var personID = new Guid("9cd27d1e-1594-4377-9920-11840ec9c136"); //Bernard Roth
            var ethnicity = new Guid("6c8fb74f-f534-e911-a2c5-005056926fe4"); //English


            //Set website Email Verification Required to No
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //Reset two factor authentication for the website
            dbHelper.website.UpdateTwoFactorAuthenticationInfo(websiteid, false, null, null, null, null);

            //reset the person Personal Details
            dbHelper.person.UpdatePersonalDetails(personID, "Bernard", "Roth", new DateTime(1966, 5, 4), 1, ethnicity, "231 659 9044", "1234567890");


            var businessObjectName = "person";

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser16@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord record = new UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord();
            record.BusinessObjectName = businessObjectName;
            record.Id = personID;
            record.Fields.Add("nationalinsurancenumber", "999999999");

            var response = WebAPIHelper.DataFormsProxy.Save(websiteid, record, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            var responseid = Guid.Parse(response.Replace("\"", ""));
            Assert.AreEqual(personID, responseid);
        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9870")]
        [Description("Security test for the dataform 'save' API route - Send and update post request for a person record that is not linked to the website user - " +
            "My Record (Citizen Portal) data view should not grant the user access to this person record - validate that the update request will fail")]
        public void PortalSecurity_TestMethod002()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal
            var websiteUserID = new Guid("23d6635e-4f61-eb11-a308-005056926fe4"); //StaffordshireCitizenPortalUser16@mail.com
            var personID = new Guid("21c65d8c-e0af-4d11-8a98-1adb27a6dd30"); //Lamont Stevens


            //Set website Email Verification Required to No
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //Reset two factor authentication for the website
            dbHelper.website.UpdateTwoFactorAuthenticationInfo(websiteid, false, null, null, null, null);


            var businessObjectName = "person";

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser16@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);

            UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord record = new UITests.Framework.WebAppAPI.Entities.Portal.PortalRecord();
            record.BusinessObjectName = businessObjectName;
            record.Id = personID;
            record.Fields.Add("nationalinsurancenumber", "999999999");
            try
            {
                var response = WebAPIHelper.DataFormsProxy.Save(websiteid, record, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type Person. If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);
                return;
            }
            Assert.Fail("No exception was thrown by the API");
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
