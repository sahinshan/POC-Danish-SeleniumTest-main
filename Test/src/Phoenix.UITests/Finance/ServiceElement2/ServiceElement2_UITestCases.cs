using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Finance.ServiceElement2
{
    [TestClass]
    public class ServiceElement2_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string serviceElement2Code = DateTime.Now.ToString("HHmmssFF");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User ServiceElement2User1

                commonMethodsDB.CreateSystemUserRecord("ServiceElement2User1", "ServiceElement2", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22771

        [TestProperty("JiraIssueID", "CDV6-22772")]
        [Description("Steps 1 to 6 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement2_UITestMethod001()
        {
            var serviceElement2Name = "CDV6_22772_" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement2User1", "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 2")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 2");

            serviceElement2Page
                .WaitForServiceElement2PageToLoad();
            #endregion

            #region Step 3
            serviceElement2Page
                .ClickNewRecordButton();

            serviceElement2RecordPage
                .WaitForServiceElement2RecordPageToLoad()
                .ValidateNewRecordFieldsVisible()
                .ValidateHealthNursingContributionFieldIsDisplayed(true)
                .ValidateThirdPartyContributionFieldIsDisplayed(true);
            #endregion

            #region Step 4
            serviceElement2RecordPage
                .InsertName(serviceElement2Name)
                .InsertCode(serviceElement2Code)
                .InsertGovCode("22772_A")
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .InsertEndDate(commonMethodsHelper.GetCurrentDate())
                .ClickHealthNursingContributionNoOption()
                .ClickThirdPartyContributionNoOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceElement2RecordPage
                .ValidateNameFieldValue(serviceElement2Name)
                .ValidateCodeFieldValue(serviceElement2Code)
                .ValidateGovCodeFieldValue("22772_A")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateStartDateFieldValue(commonMethodsHelper.GetCurrentDate())
                .ValidateEndDateFieldValue(commonMethodsHelper.GetCurrentDate())
                .ValidateValidForExportYesOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateHealthNursingContributionYesOptionChecked(false)
                .ValidateThirdPartyContributionYesOptionChecked(false);
            #endregion

            #region Step 5
            serviceElement2RecordPage
                .ClickInactiveYesOption()
                .ClickSaveButton();

            serviceElement2RecordPage
                .WaitForServiceElement2RecordPageToLoad()
                .ValidateInactiveYesOptionChecked(true)
                .ValidateNameFieldIsDisabled()
                .ValidateCodeFieldIsDisabled()
                .ValidateGovCodeFieldIsDisabled()
                .ValidateInactiveFieldOptionsDisabled()
                .ValidateStartDateFieldIsDisabled()
                .ValidateEndDateFieldIsDisabled()
                .ValidateResponsibleTeamFieldIsDisabled()
                .ValidateHealthNursingContributionOptionsIsDisabled()
                .ValidateThirdPartyContribution_OptionsDisabled();

            #endregion

            #region Step 6
            serviceElement2RecordPage
                .WaitForServiceElement2RecordPageToLoad()
                .ClickBackButton();

            serviceElement2Page
                .WaitForServiceElement2PageToLoad()
                .ClickNewRecordButton();

            serviceElement2RecordPage
                .WaitForServiceElement2RecordPageToLoad()
                .InsertName("CDV6_22772_" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss"))
                .InsertCode(serviceElement2Code)
                .InsertGovCode("22772_B")
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .InsertEndDate(commonMethodsHelper.GetCurrentDate())
                .ClickHealthNursingContributionYesOption()
                .ClickThirdPartyContributionNoOption()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Service Element 2 record using this Code. Please correct as necessary")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-22773")]
        [Description("Steps 7 to 11 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement2_UITestMethod002()
        {
            #region Person 

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var personId = commonMethodsDB.CreatePersonRecord("CDV6-22773", commonMethodsHelper.GetCurrentDateTimeString(), _ethnicityId, _careDirectorQA_TeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Service Element 1 & Service Element 2

            var serviceElement1Name = "SE_1_CDV6_22773_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement2Name = "SE_2_CDV6_22773_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var serviceElement1 = dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, serviceElement1Name, startDate, code, 1, 1);
            var serviceElement2 = dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement2Name, startDate, code);

            dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement1, serviceElement2);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement2User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 2")
                .SelectSavedView("Active Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Start Date")
                .ResultsPageValidateHeaderCellText(5, "End Date")
                .ResultsPageValidateHeaderCellText(6, "Health Nursing Contribution?")
                .ResultsPageValidateHeaderCellText(7, "Third Party Contribution?");

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 2")
                .SelectSavedView("Inactive Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Start Date")
                .ResultsPageValidateHeaderCellText(5, "End Date")
                .ResultsPageValidateHeaderCellText(6, "Health Nursing Contribution?")
                .ResultsPageValidateHeaderCellText(7, "Third Party Contribution?");

            #endregion

            #region Step 11

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapNewButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
                .ValidateGridHeaderCellText(2, "Name")
                .ValidateGridHeaderCellText(3, "Code")
                .ValidateGridHeaderCellText(4, "Start Date")
                .ValidateGridHeaderCellText(5, "End Date")
                .ValidateGridHeaderCellText(6, "Health Nursing Contribution?")
                .ValidateGridHeaderCellText(7, "Third Party Contribution?")
                .ClickCloseButton();

            #endregion
        }

        #endregion

    }
}
