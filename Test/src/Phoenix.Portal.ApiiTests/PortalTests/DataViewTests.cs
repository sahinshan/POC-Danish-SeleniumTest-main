using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class DataViewTests : UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-8112

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9871")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number to page 1 - " +
            "Validate that the api will return the first page of information for the user assessments")]
        public void DataView_TestMethod001()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 1
            };


            var response = this.WebAPIHelper.DataViewProxy.GetView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("caseform", response.BusinessObjectName);
            Assert.IsFalse(response.HasCreatePrivilege);
            Assert.IsTrue(response.HasMoreRecords);
            Assert.IsTrue(response.IsSecure);
            Assert.AreEqual(10, response.Records.Count);
            Assert.AreEqual("My Assessments", response.Title);

            Assert.AreEqual("Automated UI Test Document 3", response.Records[0].Title);
            Assert.AreEqual("Start Date", response.Records[0].Fields[1].Label);
            Assert.AreEqual("03/01/2021", response.Records[0].Fields[1].Text);
            Assert.AreEqual("Status", response.Records[0].Fields[2].Label);
            Assert.AreEqual("Complete", response.Records[0].Fields[2].Text);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9872")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number to page 2 - " +
            "Validate that the api will return the second page of information for the user assessments")]
        public void DataView_TestMethod002()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 2
            };


            var response = this.WebAPIHelper.DataViewProxy.GetView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("caseform", response.BusinessObjectName);
            Assert.IsFalse(response.HasCreatePrivilege);
            Assert.IsFalse(response.HasMoreRecords);
            Assert.IsTrue(response.IsSecure);
            Assert.AreEqual(2, response.Records.Count);
            Assert.AreEqual("My Assessments", response.Title);

            Assert.AreEqual("Adult Care and Support Plan", response.Records[0].Title);
            Assert.AreEqual("Start Date", response.Records[0].Fields[1].Label);
            Assert.AreEqual("04/01/2021", response.Records[0].Fields[1].Text);
            Assert.AreEqual("Status", response.Records[0].Fields[2].Label);
            Assert.AreEqual("In Progress", response.Records[0].Fields[2].Text);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9873")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number that should retrieve no records - " +
            "Validate that the api will return no assessment records information")]
        public void DataView_TestMethod003()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 3
            };


            var response = this.WebAPIHelper.DataViewProxy.GetView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("caseform", response.BusinessObjectName);
            Assert.IsFalse(response.HasCreatePrivilege);
            Assert.IsFalse(response.HasMoreRecords);
            Assert.IsTrue(response.IsSecure);
            Assert.AreEqual(0, response.Records.Count);
            Assert.AreEqual("My Assessments", response.Title);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9874")]
        [Description("Website user has permissions to view Case Form records - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews' controller - Supply a DataViewId for case forms that is not linked to CareDirector.Portal -" +
            "Validate that the api will return the second page of information for the user assessments")]
        public void DataView_TestMethod004()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("ff75c093-fcbf-e811-9c04-1866da1e3bda"), //Active Records
                PageNumber = 1
            };

            try
            {

                this.WebAPIHelper.DataViewProxy.GetView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access.\",\"errorCode\":\"RecordNotFoundNoArguments\"}", ex.Message);

                return;
            }

            Assert.Fail("No exception was thrown by the api method.");
            

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9875")]
        [Description("Website user has permissions to view Case Form records - Login with the website user (user has no portal access to case forms)- " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number that should retrieve no records - " +
            "Validate that the api will return a permission denied error")]
        public void DataView_TestMethod005()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser17@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 1
            };

            try
            {

                var response = this.WebAPIHelper.DataViewProxy.GetView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type CaseForm. If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);

                return;
            }

            Assert.Fail("No exception was thrown by the api method.");

        }





        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9876")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews/lookup' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number to page 1 - " +
            "Validate that the api will return the first page of information for the user assessments")]
        public void DataView_TestMethod006()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 1
            };

            

            var response = this.WebAPIHelper.DataViewProxy.GetLookupView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(true, response.HasMoreRecords);
            Assert.AreEqual(10, response.Records.Count);

            Assert.AreEqual("Automated UI Test Document 3 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 03/01/2021 created by Security Test User Admin", response.Records[0].Text);
            Assert.AreEqual("Automated UI Test Document 1 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 01/01/2021 created by Security Test User Admin", response.Records[1].Text);
            Assert.AreEqual("CDA - Care and Support Plan for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 12/01/2021 created by Security Test User Admin", response.Records[2].Text);
            Assert.AreEqual("CDA - Care Act Assessment for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 11/01/2021 created by Security Test User Admin", response.Records[3].Text);
            Assert.AreEqual("Automated UI Test Document - CMS Questions for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 10/01/2021 created by Security Test User Admin", response.Records[4].Text);
            Assert.AreEqual("Auto Doc for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 09/01/2021 created by Security Test User Admin", response.Records[5].Text);
            Assert.AreEqual("Assessment_2 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 08/01/2021 created by Security Test User Admin", response.Records[6].Text);
            Assert.AreEqual("Assessment_1 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 07/01/2021 created by Security Test User Admin", response.Records[7].Text);
            Assert.AreEqual("Assessment Form for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 06/01/2021 created by Security Test User Admin", response.Records[8].Text);
            Assert.AreEqual("Allergy_Nov27 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 05/01/2021 created by Security Test User Admin", response.Records[9].Text);



        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9877")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews/lookup' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number to page 2 - " +
            "Validate that the api will return the second page of information for the user assessments")]
        public void DataView_TestMethod007()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 2
            };


            var response = this.WebAPIHelper.DataViewProxy.GetLookupView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(false, response.HasMoreRecords);
            Assert.AreEqual(2, response.Records.Count);

            Assert.AreEqual("Adult Care and Support Plan for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 04/01/2021 created by Security Test User Admin", response.Records[0].Text);
            Assert.AreEqual("Automated UI Test Document 2 for Stevens, Lamont - (29/10/1964) [CAS-000005-0820] Starting 02/01/2021 created by Security Test User Admin", response.Records[1].Text);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9878")]
        [Description("Website user has permissions to view Case Form records - User has more than 10 active records linked to him - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews/lookup' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number that should retrieve no records - " +
            "Validate that the api will return no assessment records information")]
        public void DataView_TestMethod008()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 3
            };


            var response = this.WebAPIHelper.DataViewProxy.GetLookupView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual(false, response.HasMoreRecords);
            Assert.AreEqual(0, response.Records.Count);

        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9879")]
        [Description("Website user has permissions to view Case Form records - Login with the website user - " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews/lookup' controller - Supply a DataViewId for case forms that is not linked to CareDirector.Portal -" +
            "Validate that the api will return the second page of information for the user assessments")]
        public void DataView_TestMethod009()
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


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("ff75c093-fcbf-e811-9c04-1866da1e3bda"), //Active Records
                PageNumber = 1
            };

            try
            {

                var response = this.WebAPIHelper.DataViewProxy.GetLookupView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access.\",\"errorCode\":\"RecordNotFoundNoArguments\"}", ex.Message);

                return;
            }

            Assert.Fail("No exception was thrown by the api method.");


        }

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9880")]
        [Description("Website user has permissions to view Case Form records - Login with the website user (user has no portal access to case forms)- " +
            "Perform a call to the 'api/portal/{websiteId:Guid}/dataviews/lookup' controller - Supply a DataViewId for case forms (linked to CareDirector.Portal) and specify Page Number that should retrieve no records - " +
            "Validate that the api will return a permission denied error")]
        public void DataView_TestMethod010()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //Staffordshire Citizen Portal

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "StaffordshireCitizenPortalUser17@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            var retrieveRequest = new UITests.Framework.WebAppAPI.Entities.Portal.RetrievePortalDataViewRequest()
            {
                DataViewId = new Guid("a68d5a42-361c-eb11-a2ce-0050569231cf"), //My Assessments
                PageNumber = 1
            };

            try
            {

                var response = this.WebAPIHelper.DataViewProxy.GetLookupView(websiteid, retrieveRequest, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"Unauthorized access to View Record Type CaseForm. If you have any questions, please contact the system administrator.\",\"errorCode\":\"NoViewAccess\"}", ex.Message);

                return;
            }

            Assert.Fail("No exception was thrown by the api method.");

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
