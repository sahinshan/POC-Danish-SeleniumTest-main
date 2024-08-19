using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceElement1
{
    [TestClass]
    public class ServiceElement1_UITestCases_Part5 : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private Guid _systemUserId;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss_FFFFFFF");
        private Guid _rateTypeId;
        private Guid _rateUnitId;
        private Guid _serviceElement1Id1;
        private string _serviceElement1Name1;
        private Guid _serviceElement1Id2;
        private string _serviceElement1Name2;
        private string serviceElementCode = DateTime.Now.ToString("HHmmssFF");
        private Guid _ethnicityId;

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
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceElement1", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region  Rate Type

                if (!dbHelper.rateType.GetByName("RateType7994").Any())
                    _rateTypeId = dbHelper.rateType.CreateRateType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "RateType7994", 1357901, new DateTime(2022, 1, 1), 1, 0, 1);
                _rateTypeId = dbHelper.rateType.GetByName("RateType7994")[0];

                #endregion

                #region Rate Unit

                if (!dbHelper.rateUnit.GetByCode(146802).Any())
                    dbHelper.rateUnit.CreateRateUnit(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "RateUnit7994", new DateTime(2022, 1, 1), 146802, _rateTypeId);
                _rateUnitId = dbHelper.rateUnit.GetByCode(146802).FirstOrDefault();

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        public void SetupServiceProvision()
        {
            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_22575").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_22575", new DateTime(2022, 1, 1));
            var serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_22575")[0];

            #endregion

            #region Service Provision Status

            var serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            Guid AuthoriseStatus = this.dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2022, 6, 20), 4, 999999, true, true);

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_22575", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Care Type

            var careTypeExist = dbHelper.careType.GetByName("CDV6_22575_CareType").Any();
            if (!careTypeExist)
                dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6_22575_CareType", 221222, new DateTime(2022, 11, 11));
            var _careRoomTypeId = dbHelper.careType.GetByName("CDV6_22575_CareType")[0];

            #endregion

            #region Care Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id1, _careRoomTypeId, null);

            #endregion

            #region Service Provision

            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID,
                serviceprovisionstatusid, _serviceElement1Id1, _careRoomTypeId,
                serviceprovisionstartreasonid,
                _systemUserId, placementRoomTypeId, new DateTime(2022, 6, 20), new DateTime(2022, 6, 26));

            #endregion

            #region Update Status 

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, AuthoriseStatus);

            #endregion
        }

        public void SetupFinanceInvoiceBatchSetups()
        {
            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];

            var ExtractNameId = dbHelper.extractName.CreateExtractName(_careDirectorQA_TeamId, "Extract Batch 1" + currentTimeString, new DateTime(2022, 12, 12), true, false, false);
            var InvoiceById = dbHelper.invoiceBy.GetByName("Provider\\Purchasing Team\\Client")[0];
            var InvoiceFrequencyId = dbHelper.invoiceFrequency.GetByName("Every Week")[0];

            dbHelper.financeInvoiceBatchSetup.CreateFinanceInvoiceBatchSetup(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", ExtractNameId, 2, InvoiceById, InvoiceFrequencyId, paymentTypeCodeId, providerBatchGroupingId, 2, _serviceElement1Id2, new DateTime(2022, 12, 12), true, true, true);
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22574

        [TestProperty("JiraIssueID", "CDV6-22605")]
        [Description("Steps 10 to 13 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod005()
        {
            #region  Service Element 1

            _serviceElement1Name1 = "Service_Element1_CDV6_22574";
            if (!dbHelper.serviceElement1.GetByName(_serviceElement1Name1).Any())
                dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, _serviceElement1Name1, DateTime.Now.Date, 22574, 1, 2);

            _serviceElement1Id1 = dbHelper.serviceElement1.GetByName(_serviceElement1Name1)[0];

            #endregion

            #region Step 10

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
                .InsertName("Record_" + currentTimeString)
                .InsertCode("22574")
                .ClickValidRateUnitsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .AddElementToList(_rateUnitId.ToString())
                .TapOKButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("There is already a Service Element 1 record using this Code. Please correct as necessary.")
              .TapCloseButton();

            #endregion

            #region Step 11

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertCode(serviceElementCode)
                .ClickSaveAndCloseButton();

            dbHelper = new DBHelper.DatabaseHelper();

            _serviceElement1Id2 = dbHelper.serviceElement1.GetByCode(serviceElementCode)[0];
            _serviceElement1Name2 = (string)dbHelper.serviceElement1.GetByID(_serviceElement1Id2, "name")["name"];

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name1)
                .TapSearchButton()
                .ValidateRecordCellContent(_serviceElement1Id1.ToString(), 2, _serviceElement1Name1)
                .InsertSearchQuery(_serviceElement1Name2)
                .TapSearchButton()
                .ValidateRecordCellContent(_serviceElement1Id2.ToString(), 2, _serviceElement1Name2);

            #endregion

            #region Step 12

            serviceElement1Page
                .OpenRecord(_serviceElement1Id2.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateWhoToPayOptionIsDisabled();

            #endregion

            #region Step 13

            serviceElement1RecordPage
                .ValidateUsedInFinanceOptionsIsDisabled()
                .ValidateUsedInFinanceOptionSelection(false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22575

        [TestProperty("JiraIssueID", "CDV6-22625")]
        [Description("Steps 14 to 15 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod006()
        {
            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            _serviceElement1Name1 = "Carer_" + currentTimeString;
            _serviceElement1Name2 = "Person_" + currentTimeString;

            #region Step 14

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
                .InsertName(_serviceElement1Name1)
                .InsertCode(serviceElementCode)
                .SelectWhoToPay("Carer")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            dbHelper = new DBHelper.DatabaseHelper();
            _serviceElement1Id1 = dbHelper.serviceElement1.GetByName(_serviceElement1Name1)[0];

            SetupServiceProvision();

            serviceElement1RecordPage
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name1)
                .TapSearchButton()
                .OpenRecord(_serviceElement1Id1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidateExemptionOrExtensionRulesApplyFieldOptionsDisabled()
                .ClickSaveAndCloseButton();

            #endregion

            #region Step 15

            serviceElementCode = DateTime.Now.ToString("HHmmssFF");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .ClickNewRecordButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertName(_serviceElement1Name2)
                .InsertCode(serviceElementCode)
                .SelectWhoToPay("Person")
                .ClickValidRateUnitsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("RateUnit7994")
                .TapSearchButton()
                .AddElementToList(_rateUnitId.ToString())
                .TapOKButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickPaymentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Invoice")
                .TapSearchButton()
                .SelectResultElement(paymentTypeCodeId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickProviderBatchGroupingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Batching Not Applicable")
                .TapSearchButton()
                .SelectResultElement(providerBatchGroupingId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .InsertAdjustedDays("0")
                .ClickVATCodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Standard Rated")
                .TapSearchButton()
                .SelectResultElement(vatCodeId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            dbHelper = new DBHelper.DatabaseHelper();
            _serviceElement1Id2 = dbHelper.serviceElement1.GetByName(_serviceElement1Name2)[0];

            SetupFinanceInvoiceBatchSetups();

            serviceElement1RecordPage
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name2)
                .TapSearchButton()
                .OpenRecord(_serviceElement1Id2.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ValidatePaymentTypeLookupIsDisabled()
                .ValidateProviderBatchGroupingLookupIsDisabled()
                .ValidateAdjustedDaysFieldIsDisabled()
                .ValidateVATCodeLookupIsDisabled()
                .ClickSaveAndCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22576

        [TestProperty("JiraIssueID", "CDV6-22683")]
        [Description("Steps 16 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceElement1_UITestMethod007()
        {
            _serviceElement1Name1 = "Carer_" + currentTimeString;

            #region Step 16

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
                .InsertName(_serviceElement1Name1)
                .InsertCode(serviceElementCode)
                .SelectWhoToPay("Carer")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            dbHelper = new DBHelper.DatabaseHelper();
            _serviceElement1Id1 = dbHelper.serviceElement1.GetByName(_serviceElement1Name1)[0];

            serviceElement1RecordPage
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name1)
                .TapSearchButton()
                .OpenRecord(_serviceElement1Id1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
               .TapOKButton();

            System.Threading.Thread.Sleep(5000);

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name1)
                .TapSearchButton()
                .ValidateNoRecordsMessageVisible();

            #endregion

        }

        #endregion

    }
}
