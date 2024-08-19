using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceElement1
{
    [TestClass]
    public class ServiceElement1_UITestCases_Part2 : FunctionalTest
    {
        #region Properties
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private string partialValueSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string serviceElementCode = DateTime.Now.ToString("HHmmssFF");
        #endregion

        #region Properties - Part 2
        private Guid _serviceProvisionStartReasonId;
        private Guid _serviceProvisionEndReasonId;
        private Guid _rateTypeId;
        private Guid _glCodeLocationId;
        private Guid _glCodeId;
        private Guid _serviceElement1ValidRateUnitId;
        #endregion

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

                _systemUsername = "ServiceElement1User1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceElement1", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Service Provision Start Reason
                _serviceProvisionStartReasonId = dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_7994" + partialValueSuffix, new DateTime(2022, 1, 1));

                #endregion

                #region Service Provision End Reason
                _serviceProvisionEndReasonId = dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "EndReason_7994" + partialValueSuffix, new DateTime(2022, 1, 1));

                #endregion

                #region Rate Type

                if (!dbHelper.rateType.GetByName("RateType7994").Any())
                    _rateTypeId = dbHelper.rateType.CreateRateType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "RateType7994", 1357901, new DateTime(2022, 1, 1), 1, 0, 1);
                _rateTypeId = dbHelper.rateType.GetByName("RateType7994")[0];
                #endregion

                #region Rate Unit

                if (!dbHelper.rateUnit.GetByCode(146802).Any())
                    _serviceElement1ValidRateUnitId = dbHelper.rateUnit.CreateRateUnit(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "RateUnit7994", new DateTime(2022, 1, 1), 146802, _rateTypeId);
                _serviceElement1ValidRateUnitId = dbHelper.rateUnit.GetByCode(146802)[0];
                #endregion

                #region GL Code Location
                _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
                #endregion

                #region GL Code       
                if (!dbHelper.glCode.GetByDescription("7994ServiceElement").Any())
                    _glCodeId = dbHelper.glCode.CreateGLCode(_careDirectorQA_TeamId, _glCodeLocationId, "7994ServiceElement", "7994", "7994", false);
                _glCodeId = dbHelper.glCode.GetByDescription("7994ServiceElement")[0];
                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22571

        [TestProperty("JiraIssueID", "CDV6-22598")]
        [Description("Step 4 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod002()
        {
            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

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
            .ClickNewRecordButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateNewRecordFieldsVisible();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertName("SE22598_" + partialValueSuffix)
                .InsertCode(serviceElementCode)
                .InsertGovCode("22598_A")
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .InsertEndDate(commonMethodsHelper.GetCurrentDate())
                .SelectPaymentsCommence("Actual")
                .ClickDefaultStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_7994" + partialValueSuffix)
                .TapSearchButton()
                .SelectResultElement(_serviceProvisionStartReasonId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickDefaultEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("EndReason_7994" + partialValueSuffix)
                .TapSearchButton()
                .SelectResultElement(_serviceProvisionEndReasonId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickValidRateUnitsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .ClickAddSelectedButton(_serviceElement1ValidRateUnitId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickDefaultRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .SelectResultElement(_serviceElement1ValidRateUnitId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickGLCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("7994")
                .TapSearchButton()
                .SelectResultElement(_glCodeId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertNotes("Service Element: " + partialValueSuffix)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceElement1RecordPageToLoad();

            serviceElement1RecordPage
                .ValidateNameFieldValue("SE22598_" + partialValueSuffix)
                .ValidateCodeFieldValue(serviceElementCode)
                .ValidateGovCodeFieldValue("22598_A")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateStartDateFieldValue(commonMethodsHelper.GetCurrentDate())
                .ValidateEndDateFieldValue(commonMethodsHelper.GetCurrentDate())
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateWhoToPayFieldValue("Provider")
                .ValidatePaymentsCommenceFieldValue("Actual")
                .ValidateEndDateRequiredYesOptionChecked(false)
                .ValidateLACLegalStatusAppliesYesOptionChecked(false)
                .ValidateDefaultStartReasonLinkFieldText("StartReason_7994" + partialValueSuffix)
                .ValidateDefaultEndReasonLinkFieldText("EndReason_7994" + partialValueSuffix)
                .ValidateUsedInFinanceYesOptionChecked(false)

                .ValidateValidRateUnitsLinkFieldText("RateUnit7994 \\ RateType7994")
                .ValidateDefaultRateUnitLinkFieldText("RateUnit7994 \\ RateType7994")
                .ValidateGLCodeLinkFieldText("7994 \\ 7994ServiceElement \\ Exempt from Charging? = No")
                .ValidateNotesFieldValue("Service Element: " + partialValueSuffix);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22572

        [TestProperty("JiraIssueID", "CDV6-22619")]
        [Description("Step 5 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod003()
        {

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

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
            .ClickNewRecordButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateNewRecordFieldsVisible();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertName("SE22619_" + partialValueSuffix)
                .InsertCode(serviceElementCode)
                .InsertGovCode("22619_A")
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .InsertEndDate(commonMethodsHelper.GetCurrentDate())
                .SelectPaymentsCommence("Actual")
                .ClickDefaultStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_7994" + partialValueSuffix)
                .TapSearchButton()
                .SelectResultElement(_serviceProvisionStartReasonId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickDefaultEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("EndReason_7994" + partialValueSuffix)
                .TapSearchButton()
                .SelectResultElement(_serviceProvisionEndReasonId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickValidRateUnitsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .ClickAddSelectedButton(_serviceElement1ValidRateUnitId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickDefaultRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .SelectResultElement(_serviceElement1ValidRateUnitId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickGLCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("7994")
                .TapSearchButton()
                .SelectResultElement(_glCodeId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertNotes("Service Element: " + partialValueSuffix)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceElement1RecordPageToLoad();

            serviceElement1RecordPage
                .ClickInactiveYesOption()
                .ClickSaveButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateInactiveYesOptionChecked(true)
                .ValidateWhoToPayOptionIsDisabled()
                .ValidateUsedInFinanceOptionsIsDisabled()
                .ValidateNameFieldIsDisabled()
                .ValidateCodeFieldIsDisabled()
                .ValidateGovCodeFieldIsDisabled()
                .ValidateInactiveFieldOptionsDisabled()
                .ValidateStartDateFieldIsDisabled()
                .ValidateEndDateFieldIsDisabled()
                .ValidateResponsibleTeamFieldIsDisabled()
                .ValidatePaymentsCommenceFieldIsDisabled()
                .ValidateDefaultStartReasonFieldIsDisabled()
                .ValidateDefaultEndReasonFieldIsDisabled()
                .ValidateEndDateRequiredOptionsDisabled()
                .ValidateLACLegalStatusAppliesFieldOptionsDisabled()
                .ValidateValidRateUnitsLookupFieldButtonIsDisabled()
                .ValidateDefaultRateUnitLookupFieldButtonIsDisabled()
                .ValidateGLCodeLookupFieldButtonIsDisabled()
                .ValidateNotesFieldIsDisabled();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22573

        [TestProperty("JiraIssueID", "CDV6-22640")]
        [Description("Step 6-9 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod004()
        {
            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

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
            .ClickNewRecordButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertName("SE22640_" + partialValueSuffix)
                .InsertCode(serviceElementCode)
                .InsertStartDate(commonMethodsHelper.GetCurrentDate())
                .ValidateWhoToPayFieldValue("Provider")
                .ValidateWhoToPayFieldIsDisplayed(true)
                .ValidateValidRateUnitFieldIsDisplayed(true)
                .ValidateDefaultRateUnitFieldIsDisplayed(true);

            serviceElement1RecordPage
                .SelectWhoToPay("Person")
                .ValidateWhoToPayFieldValue("Person")
                .ValidateWhoToPayFieldIsDisplayed(true)
                .ValidateValidRateUnitFieldIsDisplayed(true)
                .ValidateDefaultRateUnitFieldIsDisplayed(true);

            serviceElement1RecordPage
                .SelectWhoToPay("Carer")
                .ValidateWhoToPayFieldValue("Carer")
                .ValidateWhoToPayFieldIsDisplayed(true)
                .ValidateValidRateUnitFieldIsDisplayed(false)
                .ValidateDefaultRateUnitFieldIsDisplayed(false);

            #endregion

            #region Step 7
            serviceElement1RecordPage
                .ValidateExemptionOrExtensionRulesApplyOptionsIsDisplayed()
                .ValidateMaximumCapacityAppliesOptionsIsDisplayed();

            #endregion

            #region Step 8
            serviceElement1RecordPage
                .ClickMaximumCapacityAppliesYesOption()
                .ValidateCapacityFieldIsDisplayed(true);

            #endregion

            #region Step 9
            serviceElement1RecordPage
                .SelectWhoToPay("Person")
                .ValidatePaymentTypeLookupFieldButtonIsDisplayed(true)
                .ValidateProviderBatchGroupingLookupFieldButtonIsDisplayed(true)
                .ValidateVATCodeLookupFieldButtonIsDisplayed(true)
                .ValidateAdjustedDaysFieldIsDisplayed(true);

            #endregion
        }

        #endregion
    }
}
