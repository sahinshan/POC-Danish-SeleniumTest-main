using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.ServiceProvision
{
    /// <summary>
    /// This class contains Automated UI test scripts for Service Provision Related
    /// </summary>
    [TestClass]
    public class ServiceProvision_UITestCases2 : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _systemUserId;
        private Guid _languageId;
        private string EnvironmentName;
        private Guid _serviceProvisionStatus;
        private Guid _rateTypeId;
        private Guid _rateUnitId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private Guid _serviceMappingId;
        private Guid _serviceProvisionStartReason;
        private Guid _placementRoomTypeId;
        private Guid _serviceProvisionId;
        private int _serviceProvisionNumber;
        private DateTime _actualStartDate;
        private Guid _careRoomTypeId;
        private Guid _clientCategoryId;
        private Guid _glCodeserviceLocationId;
        private Guid _glCodeId;
        private Guid _serviceElement1ValidRateUnitId;
        private Guid _teamGLCodeId;
        private Guid _glCode_teamid;

        [TestInitialize()]
        public void ServiceProvison_SetupTest()
        {
            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Environment Name

            EnvironmentName = ConfigurationManager.AppSettings["EnvironmentName"];

            #endregion

            #region Providers

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Create Default System User 

            var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").Any();
            if (!adminUserExists)
            {
                _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_1", "CW", "Forms Test User 1", "CW Forms Test User 1", "Passw0rd_!", "CW_Forms_Test_User_1@somemail.com", "CW_Forms_Test_User_1@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemAdministratorSecurityProfileId);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemUserSecureFieldsSecurityProfileId);
            }

            if (Guid.Empty == _systemUserId)
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17908_Ethnicity").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "CDV6-17908_Ethnicity", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17908_Ethnicity")[0];

            #endregion

            #region Person

            var personRecordExists = dbHelper.person.GetByFirstName("CDV6-17908_TestUser").Any();
            if (!personRecordExists)
            {
                _personID = dbHelper.person.CreatePersonRecord("", "CDV6-17908_TestUser", "", "1", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstName("CDV6-17908_TestUser").FirstOrDefault();
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            }
            _personFullName = "CDV6-17908_TestUser 1";

            #endregion

            #region Service Provision Status

            _serviceProvisionStatus = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft").FirstOrDefault();

            #endregion

            #region  RateType

            var rateTypeExist = dbHelper.rateType.GetByName("CDV6-17908_RateType").Any();
            if (!rateTypeExist)
                dbHelper.rateType.CreateRateType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_RateType", 987654, new DateTime(2020, 1, 1), 5, 6, 7);
            _rateTypeId = dbHelper.rateType.GetByName("CDV6-17908_RateType")[0];


            #endregion

            #region  RateUnit

            var rateUnitExist = dbHelper.rateUnit.GetByCode(111234).Any();
            if (!rateUnitExist)
                dbHelper.rateUnit.CreateRateUnit(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_RateUnit", new DateTime(2020, 1, 1), 111234, _rateTypeId);
            _rateUnitId = dbHelper.rateUnit.GetByCode(111234).FirstOrDefault();

            #endregion

            #region  ServiceElement1

            var serviceElement1Exist = dbHelper.serviceElement1.GetByName("CDV6-17908_ServiceElement1").Any();
            if (!serviceElement1Exist)
                dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, "CDV6-17908_ServiceElement1", new DateTime(2020, 1, 1), 1345, 1, 1);
            _serviceElement1Id = dbHelper.serviceElement1.GetByName("CDV6-17908_ServiceElement1")[0];


            #endregion

            #region  ServiceElement2

            var serviceElement2Exist = dbHelper.serviceElement2.GetByName("CDV6-17908_ServiceElement2").Any();
            if (!serviceElement2Exist)
                dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_ServiceElement2", new DateTime(2020, 1, 1), 13456);
            _serviceElement2Id = dbHelper.serviceElement2.GetByName("CDV6-17908_ServiceElement2")[0];


            #endregion

            #region Service Mapping 

            var serviceMappingExist = dbHelper.serviceMapping.GetByName("CDV6-17908_ServiceElement1 \\ CDV6-17908_ServiceElement2").Any();
            if (!serviceMappingExist)
                dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, _serviceElement2Id);
            _serviceMappingId = dbHelper.serviceMapping.GetByName("CDV6-17908_ServiceElement1 \\ CDV6-17908_ServiceElement2").FirstOrDefault();

            #endregion

            #region Service Provision start reason

            var serviceProvisionStartReasonExist = dbHelper.serviceProvisionStartReason.GetByName("CDV6-17908_StartReason").Any();
            if (!serviceProvisionStartReasonExist)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_StartReason", new DateTime(2020, 1, 1));
            _serviceProvisionStartReason = dbHelper.serviceProvisionStartReason.GetByName("CDV6-17908_StartReason")[0];

            #endregion

            #region Placement Room Type

            var placementRoomTypeExist = dbHelper.placementRoomType.GetPlacementRoomTypeByName("CDV6-17908_RoomType").Any();
            if (!placementRoomTypeExist)
                dbHelper.placementRoomType.CreatePlacementRoomType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_RoomType", new DateTime(2020, 1, 1));
            _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("CDV6-17908_RoomType")[0];

            #endregion

            #region Service Element 1 valid rate units

            var serviceElement1ValidRateUnitExist = dbHelper.serviceElement1ValidRateUnits.GetByServiceElement1Id(_serviceElement1Id).Any();
            if (!serviceElement1ValidRateUnitExist)
                dbHelper.serviceElement1ValidRateUnits.CreateServiceElement1ValidRateUnits(_serviceElement1Id, _rateUnitId);
            _serviceElement1ValidRateUnitId = dbHelper.serviceElement1ValidRateUnits.GetByServiceElement1Id(_serviceElement1Id).FirstOrDefault();

            #endregion

            #region Service Provision 

            var serviceProvisionRecordExist = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personID).Any();
            if (!serviceProvisionRecordExist)
                dbHelper.serviceProvision.CreateNewServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, _serviceProvisionStatus, _serviceElement1Id,
                                    _serviceElement2Id, _rateUnitId, _serviceProvisionStartReason, 1, _placementRoomTypeId, new DateTime(2020, 1, 1));
            _serviceProvisionId = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personID)[0];

            _serviceProvisionNumber = (int)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionId, "serviceprovisionnumber")["serviceprovisionnumber"];

            _actualStartDate = (DateTime)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionId, "actualstartdate")["actualstartdate"];

            #endregion Service Provision 

            #region Care Type

            var careTypeExist = dbHelper.careType.GetByName("CDV6-17908_CareType").Any();
            if (!careTypeExist)
                dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17908_CareType", 6666, new DateTime(2020, 1, 1));
            _careRoomTypeId = dbHelper.careType.GetByName("CDV6-17908_CareType")[0];

            #endregion

            #region Finance Client categories

            var clientCategoryExist = dbHelper.financeClientCategory.GetByName("CDV6-17908_ClientCategory").Any();
            if (!clientCategoryExist)
                dbHelper.financeClientCategory.CreateFinanceClientCategory(_careDirectorQA_TeamId, "CDV6-17908_ClientCategory", new DateTime(2020, 1, 1), "6677");
            _clientCategoryId = dbHelper.financeClientCategory.GetByName("CDV6-17908_ClientCategory").FirstOrDefault();

            #endregion

            #region GL Code Location

            _glCodeserviceLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1 / Service Element 2 / Client Category").FirstOrDefault();

            _teamGLCodeId = dbHelper.glCodeLocation.GetByName("Team / Service Element 1 / Service Element 2 / Client Category").FirstOrDefault();

            #endregion

            #region GL Code (Service Gl)

            var glCodeExist = dbHelper.glCode.GetByDescription("CDV6-17908_GLCode").Any();
            if (!glCodeExist)
                dbHelper.glCode.CreateGLCode(_careDirectorQA_TeamId, _glCodeserviceLocationId, "CDV6-17908_GLCode", "platform_123", "456");
            _glCodeId = dbHelper.glCode.GetByDescription("CDV6-17908_GLCode").FirstOrDefault();

            #endregion

            #region GL Code (Team Gl)

            var teamGlCodeExist = dbHelper.glCode.GetByDescription("CDV6-17908_TeamGLCode").Any();
            if (!teamGlCodeExist)
                dbHelper.glCode.CreateGLCode(_careDirectorQA_TeamId, _teamGLCodeId, "CDV6-17908_TeamGLCode", "team_123", "999");
            _glCode_teamid = dbHelper.glCode.GetByDescription("CDV6-17908_TeamGLCode").FirstOrDefault();

            #endregion

        }

        /*Bug fix CDV6 -17822 Automation script*/

        #region https://advancedcsg.atlassian.net/browse/CDV6-17908

        [TestProperty("JiraIssueID", "CDV6-17890")]
        [Description("Navigate to Workplace > Finance > Service Provisions.Create a Service Provision record.In the Advanced Find page, do a search for Service Provisions." +
            "Verify that after a Service Provision record is selected from Lookup dialog window, the name of the Service provision is visible in the Lookup field" +
            "Navigate to Settings > Reference Data > Service Provision > Service Element 1.Open the Service Element 1 record.Open the Service GL Codings tab.Click + to create a new record." +
            "Click Service GL Code field lookup button > Perform a Search by using Expenditure Code value of GL Code record" + "Enter the mandatory details and save the record" +
            "Navigate back to Service GL Codings tab.Click + to create a new record.Select a Team.Click Team GL Code field lookup button" +
            " Perform a Search by using Expenditure Code value of GL Code record." +
            "Verify that after the matching GL Code record is selected from Lookup Dialog Window, the value is visible in the Team GL Code Lookup field." +
            "Enter the mandatory details and save the record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_UITestCase01()

        {
            foreach (var serviceGLCodeRecord in dbHelper.serviceGLCoding.GetByServiceElement1Id(_serviceElement1Id))
                dbHelper.serviceGLCoding.DeleteServiceGLCoding(serviceGLCodeRecord);

            loginPage
               .GoToLoginPage()
               .Login("CW_Forms_Test_User_1", "Passw0rd_!")
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                  .WaitForAdvanceSearchPageToLoad()
                  .SelectRecordType("Service Provisions")
                  .SelectFilter("1", "Service Provision")
                  .SelectOperator("1", "Equals")
                  .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(_serviceProvisionNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(_serviceProvisionId.ToString());

            advanceSearchPage
                 .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_serviceProvisionId.ToString());

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToReferenceDataSection();

            referenceDataPage
                 .WaitForReferenceDataPageToLoad()
                 .InsertSearchQuery("Service Element 1")
                 .TapSearchButton()
                 .ClickReferenceDataMainHeader("Service Provision")
                 .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery("CDV6-17908_ServiceElement1")
                .TapSearchButton()
                .SelectServiceProvisionElement1Record(_serviceElement1Id.ToString())
                .OpenRecord(_serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceElement2LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery("CDV6-17908_ServiceElement2")
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());


            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickClientCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery("CDV6-17908_ClientCategory")
                .TapSearchButton()
                .SelectResultElement(_clientCategoryId.ToString());

            serviceGLCodingsRecordPage
                 .WaitForServiceGLCodingsRecordPageToLoad()
                 .ClickServiceGlCodeLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("platform_123")
                  .TapSearchButton()
                  .ValidateResultElementPresent(_glCodeId.ToString())
                  .TypeSearchQuery("456")
                  .TapSearchButton()
                  .ValidateResultElementPresent(_glCodeId.ToString())
                  .SelectResultElement(_glCodeId.ToString());

            serviceGLCodingsRecordPage
                    .WaitForServiceGLCodingsRecordPageToLoad()
                    .ClickSaveButton();

            Thread.Sleep(3000);

            var Glcoderecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(_serviceElement1Id);
            Assert.AreEqual(1, Glcoderecord.Count);

            foreach (var serviceGLCodeRecord in dbHelper.serviceGLCoding.GetByServiceElement1Id(_serviceElement1Id))
                dbHelper.serviceGLCoding.DeleteServiceGLCoding(serviceGLCodeRecord);

            mainMenu
                    .WaitForMainMenuToLoad()
                    .NavigateToReferenceDataSection();

            referenceDataPage
                    .WaitForReferenceDataPageToLoad()
                    .InsertSearchQuery("Service Element 1")
                    .TapSearchButton()
                    .ClickReferenceDataMainHeader("Service Provision")
                    .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                    .WaitForServiceElement1PageToLoad()
                    .InsertSearchQuery("CDV6-17908_ServiceElement1")
                    .TapSearchButton()
                    .SelectServiceProvisionElement1Record(_serviceElement1Id.ToString())
                    .OpenRecord(_serviceElement1Id.ToString());

            serviceElement1RecordPage
                    .WaitForServiceElement1RecordPageToLoad()
                    .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                    .WaitForServiceGLCodingsPageToLoad()
                    .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                    .WaitForServiceGLCodingsRecordPageToLoad()
                    .ClickServiceElement2LookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .SelectViewByText("Lookup Records")
                    .TypeSearchQuery("CDV6-17908_ServiceElement2")
                    .TapSearchButton()
                    .SelectResultElement(_serviceElement2Id.ToString());

            serviceGLCodingsRecordPage
                   .WaitForServiceGLCodingsRecordPageToLoad()
                   .ClickClientCategoryLookUpButton();

            lookupPopup
                    .WaitForLookupPopupToLoad()
                    .SelectViewByText("Lookup Records")
                    .TypeSearchQuery("CDV6-17908_ClientCategory")
                    .TapSearchButton()
                    .SelectResultElement(_clientCategoryId.ToString());

            serviceGLCodingsRecordPage
                 .WaitForServiceGLCodingsRecordPageToLoad()
                 .ClickTeamLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("CareDirector QA")
                  .TapSearchButton()
                  .SelectResultElement(_careDirectorQA_TeamId.ToString());

            serviceGLCodingsRecordPage
                    .WaitForServiceGLCodingsRecordPageToLoad()
                    .ClickTeamGLCodeLookUpButton();

            lookupPopup
                  .WaitForLookupPopupToLoad()
                  .TypeSearchQuery("team_123")
                  .TapSearchButton()
                  .ValidateResultElementPresent(_glCode_teamid.ToString())
                  .TypeSearchQuery("999")
                  .TapSearchButton()
                  .ValidateResultElementPresent(_glCode_teamid.ToString())
                  .SelectResultElement(_glCode_teamid.ToString());

            serviceGLCodingsRecordPage
                   .WaitForServiceGLCodingsRecordPageToLoad()
                   .ClickSaveButton()
                   .ValidateIconsAfterSave();

            System.Threading.Thread.Sleep(3000);

            var TeamGlcoderecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(_serviceElement1Id);
            Assert.AreEqual(1, TeamGlcoderecord.Count);

            string GlCodeRecordTitle = (string)dbHelper.serviceGLCoding.GetByID(TeamGlcoderecord[0], "name")["name"];

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateRecordTitle(GlCodeRecordTitle);

        }

        #endregion
    }

}