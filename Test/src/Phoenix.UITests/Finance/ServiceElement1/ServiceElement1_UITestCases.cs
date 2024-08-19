using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceElement1
{
    [TestClass]
    public class ServiceElement1_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;

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

                #region System User AllActivitiesUser1

                commonMethodsDB.CreateSystemUserRecord("ServiceElement1User1", "ServiceElement1", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22570

        [TestProperty("JiraIssueID", "CDV6-22596")]
        [Description("Steps 1 to 3 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement1User1", "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad();

            #endregion

            #region Step 3

            serviceElement1Page
            .ClickNewRecordButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateNewRecordFieldsVisible();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22577

        [TestProperty("JiraIssueID", "CDV6-22629")]
        [Description("Steps 17 to 20 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod008()
        {
            var serviceElement1Name = "CDV6_22577_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var validRateUnits = new List<Guid>();
            var rateUnit1Id = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();
            var rateUnit2Id = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            var rateUnit3Id = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            validRateUnits.Add(rateUnit1Id);
            validRateUnits.Add(rateUnit2Id);
            validRateUnits.Add(rateUnit3Id);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, _careDirectorQA_TeamId, startDate, code, 1, 1, validRateUnits, rateUnit3Id);

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement1User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateTopMenuLinksVisible();

            #endregion

            #region Step 18

            serviceElement1RecordPage
                .ClickDefaultRateUnitLookupButton();

            var rateUnit4Id = dbHelper.rateUnit.GetByName("Banded Rate \\ Hours (Banded)").FirstOrDefault();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(rateUnit1Id.ToString())
                .ValidateResultElementPresent(rateUnit2Id.ToString())
                .ValidateResultElementPresent(rateUnit3Id.ToString())
                .ValidateResultElementNotPresent(rateUnit4Id.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 19

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickValidRateUnitsLookupButton();

            var rateUnit5Id = dbHelper.rateUnit.GetByName("One-Off \\ One-Off").FirstOrDefault();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Banded Rate").TapSearchButton().AddElementToList(rateUnit4Id.ToString())
                .TypeSearchQuery("One-Off").TapSearchButton().AddElementToList(rateUnit5Id.ToString())
                .TapOKButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateValidRateUnitOptionVisible(rateUnit1Id.ToString(), "Per 1 Hour \\ Hours (Whole)\r\nRemove")
                .ValidateValidRateUnitOptionVisible(rateUnit2Id.ToString(), "Per 1 Hour Unit \\ Units (Parts)\r\nRemove")
                .ValidateValidRateUnitOptionVisible(rateUnit3Id.ToString(), "Per Day \\ Days\r\nRemove")
                .ValidateValidRateUnitOptionVisible(rateUnit4Id.ToString(), "Banded Rate \\ Hours (Banded)\r\nRemove")
                .ValidateValidRateUnitOptionVisible(rateUnit5Id.ToString(), "One-Off \\ One-Off\r\nRemove");

            #endregion

            #region Step 20

            serviceElement1RecordPage
                .ValidateDefaultRateUnitLinkFieldVisibility(true)
                .ValidateDefaultRateUnitLinkFieldText("Per Day \\ Days")
                .ClickValidRateUnitOptionRemoveButton(rateUnit3Id.ToString());

            serviceElement1RecordPage
                .ValidateDefaultRateUnitLinkFieldVisibility(false)
                .ClickSaveAndCloseButton();

            serviceElement1Page
               .WaitForServiceElement1PageToLoad()
               .InsertSearchQuery(serviceElement1Name)
               .TapSearchButton()
               .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateDefaultRateUnitLinkFieldVisibility(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22578

        [TestProperty("JiraIssueID", "CDV6-22654")]
        [Description("Steps 21 to 25 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod009()
        {

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement1User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 1")
                .SelectSavedView("Active Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Who to Pay")
                .ResultsPageValidateHeaderCellText(5, "Payments Commence")
                .ResultsPageValidateHeaderCellText(6, "End Date Required")
                .ResultsPageValidateHeaderCellText(7, "Default Rate Unit")
                .ResultsPageValidateHeaderCellText(8, "Default Start Reason")
                .ResultsPageValidateHeaderCellText(9, "Default End Reason")
                .ResultsPageValidateHeaderCellText(10, "Used In Finance")
                .ResultsPageValidateHeaderCellText(11, "Used In Batch Setup");

            #endregion

            #region Step 22

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 1")
                .SelectSavedView("Inactive Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Who to Pay")
                .ResultsPageValidateHeaderCellText(5, "Payments Commence")
                .ResultsPageValidateHeaderCellText(6, "End Date Required")
                .ResultsPageValidateHeaderCellText(7, "Default Rate Unit")
                .ResultsPageValidateHeaderCellText(8, "Default Start Reason")
                .ResultsPageValidateHeaderCellText(9, "Default End Reason")
                .ResultsPageValidateHeaderCellText(10, "Used In Finance")
                .ResultsPageValidateHeaderCellText(11, "Used In Batch Setup");

            #endregion

            #region Step 23

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 1")
                .SelectSavedView("Active Records - Pay Provider")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Who to Pay")
                .ResultsPageValidateHeaderCellText(5, "Payments Commence")
                .ResultsPageValidateHeaderCellText(6, "End Date Required")
                .ResultsPageValidateHeaderCellText(7, "Default Rate Unit")
                .ResultsPageValidateHeaderCellText(8, "Default Start Reason")
                .ResultsPageValidateHeaderCellText(9, "Default End Reason")
                .ResultsPageValidateHeaderCellText(10, "Used In Finance")
                .ResultsPageValidateHeaderCellText(11, "Used In Batch Setup");

            #endregion

            #region Step 24

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 1")
                .SelectSavedView("Active Records - Pay Carer")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Who to Pay")
                .ResultsPageValidateHeaderCellText(5, "Payments Commence")
                .ResultsPageValidateHeaderCellText(6, "End Date Required")
                .ResultsPageValidateHeaderCellText(7, "Default Rate Unit")
                .ResultsPageValidateHeaderCellText(8, "Default Start Reason")
                .ResultsPageValidateHeaderCellText(9, "Default End Reason")
                .ResultsPageValidateHeaderCellText(10, "Used In Finance")
                .ResultsPageValidateHeaderCellText(11, "Used In Batch Setup");

            #endregion

            #region Step 25

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Element 1")
                .SelectSavedView("Active Records - Pay Person")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Code")
                .ResultsPageValidateHeaderCellText(4, "Who to Pay")
                .ResultsPageValidateHeaderCellText(5, "Payments Commence")
                .ResultsPageValidateHeaderCellText(6, "End Date Required")
                .ResultsPageValidateHeaderCellText(7, "Default Rate Unit")
                .ResultsPageValidateHeaderCellText(8, "Default Start Reason")
                .ResultsPageValidateHeaderCellText(9, "Default End Reason")
                .ResultsPageValidateHeaderCellText(10, "Used In Finance")
                .ResultsPageValidateHeaderCellText(11, "Used In Batch Setup");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22579

        [TestProperty("JiraIssueID", "CDV6-22690")]
        [Description("Steps 26 to 27 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod010()
        {
            var _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var personId = commonMethodsDB.CreatePersonRecord("Jhon", commonMethodsHelper.GetCurrentDateTimeString(), _ethnicityId, _careDirectorQA_TeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #region Step 26

            loginPage
                .GoToLoginPage()
                .Login("ServiceElement1User1", "Passw0rd_!");

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
                .ValidateGridHeaderCellText(2, "Name")
                .ValidateGridHeaderCellText(3, "Code")
                .ValidateGridHeaderCellText(4, "Who to Pay")
                .ValidateGridHeaderCellText(5, "Payments Commence")
                .ValidateGridHeaderCellText(6, "Used In Finance")
                .ValidateGridHeaderCellText(7, "Used In Batch Setup")
                .ClickCloseButton();

            #endregion

            #region Step 27

            serviceProvisionRecordPage
                 .WaitForNewServiceProvisionRecordPageToLoad()
                 .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickServiceProvisionAreaButton()
                .ClickServiceElement1Button();

            serviceElement1Page
                .WaitForServiceElement1PageToLoadFromFinanceAdminArea();

            #endregion


        }

        #endregion

    }
}
