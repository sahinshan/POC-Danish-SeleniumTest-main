using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class ServiceProvisions_UITestCases : FunctionalTest
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-17923

        private string environmentName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _AutomationAdminTestUser1_SystemUserId;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private Guid serviceprovisionstatusid;
        private Guid serviceelement1id;
        private Guid serviceelement2id;
        private Guid rateunitid;
        private Guid serviceprovisionstartreasonid;
        private Guid serviceprovisionendreasonid;
        private Guid providerid;
        private Guid serviceProvidedId;
        private Guid placementRoomTypeId;
        private Guid serviceProvisionID;
        private Guid serviceDeliveryID;

        [TestInitialize()]
        public void UITests_SetupTest()
        {

            #region Environment

            environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

            #endregion

            #region Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region System User

            var automationAdminTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_Admin_Test_User_1").Any();
            if (!automationAdminTestUser1Exists)
            {
                _AutomationAdminTestUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_Admin_Test_User_1", "Automation", "Admin Test User 1", "Automation Admin Test User 1", "Passw0rd_!", "Automation_Admin_Test_User_1@somemail.com", "Automation_Admin_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, false, 4, null, DateTime.Now.Date);

                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _AutomationAdminTestUser1_SystemUserId, new DateTime(2022, 1, 1), 4, 999999, true, true);
            }
            if (_AutomationAdminTestUser1_SystemUserId == Guid.Empty)
                _AutomationAdminTestUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_Admin_Test_User_1").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_AutomationAdminTestUser1_SystemUserId, DateTime.Now);

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personID = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_17923", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Service Provision Status

            serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];

            #endregion

            #region Service Element 1

            if (!dbHelper.serviceElement1.GetByName("SE_1_CDV6_17923").Any())
                dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, "SE_1_CDV6_17923", DateTime.Now.Date, 9998, 1, 2);
            serviceelement1id = dbHelper.serviceElement1.GetByName("SE_1_CDV6_17923")[0];

            #endregion

            #region Service Element 2

            if (!dbHelper.serviceElement2.GetByName("SE_2_CDV6_17923").Any())
            {
                serviceelement2id = dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "SE_2_CDV6_17923", DateTime.Now.Date, 9999);
                dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceelement1id, serviceelement2id);
            }
            serviceelement2id = dbHelper.serviceElement2.GetByName("SE_2_CDV6_17923")[0];

            #endregion

            #region Rate Units

            rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_17923").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_17923", new DateTime(2022, 1, 1));
            serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_17923")[0];

            #endregion

            #region Service Provision End Reason

            if (!dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_17923").Any())
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "EndReason_CDV6_17923", new DateTime(2022, 1, 1));
            serviceprovisionendreasonid = dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_17923")[0];

            #endregion

            #region Provider

            if (!dbHelper.provider.GetProviderByName("Provider_Testing_CDV6-17923").Any())
            {
                providerid = dbHelper.provider.CreateProvider("Provider_Testing_CDV6-17923", _careDirectorQA_TeamId, 2); //creating a "Supplier" provider
                serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _AutomationAdminTestUser1_SystemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
                var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(2022, 1, 1), 2);
                var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

                var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
                var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
                var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
                dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);
            }
            providerid = dbHelper.provider.GetProviderByName("Provider_Testing_CDV6-17923")[0];
            serviceProvidedId = dbHelper.serviceProvided.GetByProviderId(providerid)[0];

            #endregion

            #region Placement Room Type

            placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _AutomationAdminTestUser1_SystemUserId, _personID,
                serviceprovisionstatusid, serviceelement1id, serviceelement2id,
                rateunitid,
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId,
                providerid, _AutomationAdminTestUser1_SystemUserId, placementRoomTypeId, new DateTime(2022, 6, 20), new DateTime(2022, 6, 26), null);

            #endregion

            #region Service Delivery

            serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID, rateunitid, 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-18896")]
        [Description("Test method for the task CDV6-17923")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void AuthoriseServiceProvision_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login("Automation_Admin_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .TapReadyToAuthoriseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n").TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .TapAuthoriseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n").TapOKButton();

            personServiceProvisionsPage
               .WaitForPersonServiceProvisionsPageToLoad();

            var serviceProvisionFields = dbHelper.serviceProvision.GetByID(serviceProvisionID, "serviceprovisionstatusid");

            var authorisedServiceProvisionStatusId = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            Assert.AreEqual(authorisedServiceProvisionStatusId.ToString(), serviceProvisionFields["serviceprovisionstatusid"].ToString());
        }

        #endregion
    }
}
