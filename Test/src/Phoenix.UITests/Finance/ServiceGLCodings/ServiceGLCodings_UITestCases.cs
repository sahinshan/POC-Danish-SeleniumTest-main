using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceGLCodings
{
    [TestClass]
    public class ServiceGLCodings_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;
        private Guid _ethnicityId;
        private Guid _GLCodeLocationId1;
        private Guid _GLCodeLocationId_Team;
        private Guid _GLCodeLocation_ServiceElement1Id;

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

                #region System User ServiceGLCodingsUser1
                _systemUsername = "ServiceGLCodingsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceGLCodings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity
                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

                #endregion                
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-349

        [TestProperty("JiraIssueID", "ACC-355")]
        [Description("Steps 1 to 8 from the original jira test. Verifying the Service GL Coding in Service Element 1 record.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceGLCodings_UITestMethod001()
        {
            #region Step 1 to Step 8

            #region GL Code Location
            _GLCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Allowance / Fee 1")[0];
            _GLCodeLocation_ServiceElement1Id = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId1, "GLC_" + expenditureCode1, expenditureCode1, "349A");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocation_ServiceElement1Id, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "ACC_349_Provider1_" + partialDateTimeSuffix;
            var serviceElement1Name_Carer = "ACC_349_Carer1_" + partialDateTimeSuffix;
            var serviceElement1Name_Person = "ACC_349_Person1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer
            var whotopayid_Person = 3; // Person
            var paymentscommenceid_Actual = 1; //Actual 
            int AdjustedDays = 0;
            var PaymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("No Payments").FirstOrDefault();
            var ProviderBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable").FirstOrDefault();
            var VatCodeId = dbHelper.vatCode.GetByName("Exempt").FirstOrDefault();
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);
            var serviceElement1Id_Person = commonMethodsDB.CreateServiceElement1(_careDirectorQA_TeamId, serviceElement1Name_Person, startDate, code3, whotopayid_Person, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, PaymentTypeCodeId, ProviderBatchGroupingId, AdjustedDays, VatCodeId);
            #endregion

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
                .ValidateSelectedSystemView("Active Records");

            ValidateServiceElement2OrCareTypeMandatoryField("Carer", serviceElement1Name_Carer, serviceElement1Id_Carer);

            serviceGLCodingsRecordPage
                .ClickServiceElement2OrCareTypeAllNoOption()
                .ClickClientCategoryAllNoOption()
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickBackButton();

            ValidateServiceElement2OrCareTypeMandatoryField("Provider", serviceElement1Name_Provider, serviceElement1Id);

            serviceGLCodingsRecordPage
                .ClickServiceElement2OrCareTypeAllNoOption()
                .ClickClientCategoryAllNoOption()
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickBackButton();

            ValidateServiceElement2OrCareTypeMandatoryField("Person", serviceElement1Name_Person, serviceElement1Id_Person);

            #endregion
        }

        private void ValidateServiceElement2OrCareTypeMandatoryField(string WhoToPay, string ServiceElement1Name, Guid ServiceElementId)
        {
            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(ServiceElement1Name)
                .TapSearchButton()
                .OpenRecord(ServiceElementId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceElement2OrCareTypeAllNoOption()
                .ValidateServiceElement2OrCareTypeAllNoOptionChecked();

            if (WhoToPay.Equals("Carer"))
            {
                serviceGLCodingsRecordPage
                        .ValidateCareTypeMandatoryFieldVisible(true);
            }
            else if (WhoToPay.Equals("Provider") || WhoToPay.Equals("Person"))
            {
                serviceGLCodingsRecordPage
                        .ValidateServiceElement2MandatoryFieldVisible(true);
            }

            serviceGLCodingsRecordPage
                .ClickServiceElement2OrCareTypeAllYesOption()
                .ValidateServiceElement2OrCareTypeAllYesOptionChecked();

            if (WhoToPay.Equals("Carer"))
            {
                serviceGLCodingsRecordPage
                        .ValidateCareTypeMandatoryFieldVisible(false)
                        .ValidateCareTypeFieldLinkText("");
            }
            else if (WhoToPay.Equals("Provider") || WhoToPay.Equals("Person"))
            {
                serviceGLCodingsRecordPage
                        .ValidateServiceElement2MandatoryFieldVisible(false)
                        .ValidateServiceElement2FieldLinkText("");
            }

            serviceGLCodingsRecordPage
                .ClickClientCategoryAllNoOption()
                .ValidateClientCategoryAllNoOptionChecked()
                .ValidateClientCategoryMandatoryFieldVisible(true);

            serviceGLCodingsRecordPage
                .ClickClientCategoryAllYesOption()
                .ValidateClientCategoryAllYesOptionChecked()
                .ValidateClientCategoryMandatoryFieldVisible(false);
        }

        private void ValidateServiceGLCodingRecordDuplicate(string WhoToPay, string ServiceElement1Name, Guid ServiceElementId, string ServiceElement2Name, Guid? ServiceElement2Id, string CareTypeName, Guid? CareTypeId, string ClientCategoryName, Guid ClientCategoryId, string expenditureCode1, Guid glCodeId1)
        {
            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(ServiceElement1Name)
                .TapSearchButton()
                .OpenRecord(ServiceElementId.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            var ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(ServiceElementId);
            Assert.AreEqual(1, ServiceGlCodingRecord.Count);
            //var ServiceGlCodingRecordTitle = (string)dbHelper.serviceGLCoding.GetByID(ServiceGlCodingRecord[0], "name")["name"];

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            if (WhoToPay.Equals("Provider"))
            {
                serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceElement2LookUpButton();


                lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery(ServiceElement2Name)
                    .TapSearchButton()
                    .SelectResultElement(ServiceElement2Id.ToString());
            }
            else
            {
                serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickCareTypeLookUpButton();

                lookupPopup
                    .WaitForLookupPopupToLoad()
                    .TypeSearchQuery(CareTypeName)
                    .TapSearchButton()
                    .SelectResultElement(CareTypeId.ToString());
            }

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickClientCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ClientCategoryName)
                .TapSearchButton()
                .SelectResultElement(ClientCategoryId.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceGlCodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(expenditureCode1)
                .TapSearchButton()
                .SelectResultElement(glCodeId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an Active Service GL Coding record open using these settings. Please correct as necessary")
                .TapCloseButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickBackButton();

            ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(ServiceElementId);
            Assert.AreEqual(1, ServiceGlCodingRecord.Count);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-356

        [TestProperty("JiraIssueID", "ACC-357")]
        [Description("Steps 9 to 11 from the original jira test. Verifying the Service GL Coding in Service Element 1 record.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceGLCodings_UITestMethod002()
        {
            #region Step 9 to Step 11

            #region GL Code Location
            _GLCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Service Element 1 / Service Element 2 / Client Category")[0];

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId1, "GLC_" + expenditureCode1, expenditureCode1, "349A");

            #endregion

            #region Client Category
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var clientCategoryId = commonMethodsDB.CreateFinanceClientCategory(_careDirectorQA_TeamId, "Cat355", new DateTime(2020, 1, 1), code.ToString());
            #endregion

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "ACC_356_Provider1_" + partialDateTimeSuffix;
            var serviceElement1Name_Carer = "ACC_356_Carer1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer            
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);
            #endregion

            #region Service Element 2

            var serviceElement2NameA = "ACC_356_SE2_A_" + partialDateTimeSuffix;
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameA, startDate, code1);

            #endregion

            #region Care Type

            var careTypeNameA = "ACC_356_CareTypeA" + partialDateTimeSuffix;
            var _careTypeIdA = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameA, code1, startDate);

            #endregion

            #region Service Mapping

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_A);
            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id_Carer, _careTypeIdA, false);

            #endregion

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
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateServiceElement1FieldVisible(true)
                .ValidateServiceElement1LookupButtonVisible(true)
                .ValidateServiceElement2DefaultFieldVisible(true)
                .ValidateServiceElement2LookupButtonVisible(true)
                .ValidateClientCategoryDefaultFieldVisible(true)
                .ValidateClientCategoryLookupButtonVisible(true)
                .ValidateServiceGLCodeDefaultFieldVisible(true)
                .ValidateServiceGLCodeLookupButtonVisible(true)
                .ValidateTeamDefaultFieldVisible(true)
                .ValidateTeamLookupButtonVisible(true)
                .ValidateTeamGLCodeLinkFieldVisible(true)
                .ValidateServiceElement2OrCareTypeAllYesOptionVisible(true)
                .ValidateServiceElement2OrCareTypeAllNoOptionVisible(true)
                .ValidateClientCategoryAllYesOptionVisible(true)
                .ValidateClientCategoryAllNoOptionVisible(true)
                .ValidateTeamGLCodeDefaultFieldVisible(false);

            serviceGLCodingsRecordPage
                .ClickServiceElement2LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2NameA)
                .TapSearchButton()
                .SelectResultElement(serviceElement2Id_A.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickClientCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Cat355")
                .TapSearchButton()
                .SelectResultElement(clientCategoryId.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceGlCodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(expenditureCode1)
                .TapSearchButton()
                .SelectResultElement(glCodeId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickSaveButton()
                .ValidateIconsAfterSave()
                .WaitForServiceGLCodingsRecordPageToLoad();

            var ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(serviceElement1Id);
            Assert.AreEqual(1, ServiceGlCodingRecord.Count);
            var ServiceGlCodingRecordTitle = (string)dbHelper.serviceGLCoding.GetByID(ServiceGlCodingRecord[0], "name")["name"];

            serviceGLCodingsRecordPage
                .ValidateServiceElement2FieldLinkText(serviceElement2NameA)
                .ValidateClientCategoryFieldLinkText("Cat355")
                .ValidateServiceGLCodeFieldLinkText(expenditureCode1 + " \\ " + "GLC_" + expenditureCode1 + " \\ " + "Exempt from Charging? = No")
                .ValidateServiceElement2OrCareTypeAllNoOptionChecked()
                .ValidateClientCategoryAllNoOptionChecked()
                .ValidateRecordTitle(ServiceGlCodingRecordTitle);

            serviceGLCodingsRecordPage
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickBackButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Carer.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateServiceElement1FieldVisible(true)
                .ValidateServiceElement1LookupButtonVisible(true)
                .ValidateServiceElement2DefaultFieldVisible(false)
                .ValidateServiceElement2LookupButtonVisible(false)
                .ValidateCareTypeDefaultFieldVisible(true)
                .ValidateCareTypeLookupButtonVisible(true)
                .ValidateClientCategoryDefaultFieldVisible(true)
                .ValidateClientCategoryLookupButtonVisible(true)
                .ValidateServiceGLCodeDefaultFieldVisible(true)
                .ValidateServiceGLCodeLookupButtonVisible(true)
                .ValidateTeamDefaultFieldVisible(true)
                .ValidateTeamLookupButtonVisible(true)
                .ValidateTeamGLCodeLinkFieldVisible(true)
                .ValidateServiceElement2OrCareTypeAllYesOptionVisible(true)
                .ValidateServiceElement2OrCareTypeAllNoOptionVisible(true)
                .ValidateClientCategoryAllYesOptionVisible(true)
                .ValidateClientCategoryAllNoOptionVisible(true)
                .ValidateTeamGLCodeDefaultFieldVisible(false);

            serviceGLCodingsRecordPage
                .ClickCareTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeNameA)
                .TapSearchButton()
                .SelectResultElement(_careTypeIdA.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickClientCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Cat355")
                .TapSearchButton()
                .SelectResultElement(clientCategoryId.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceGlCodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(expenditureCode1)
                .TapSearchButton()
                .SelectResultElement(glCodeId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickSaveButton()
                .ValidateIconsAfterSave()
                .WaitForServiceGLCodingsRecordPageToLoad();

            ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(serviceElement1Id_Carer);
            Assert.AreEqual(1, ServiceGlCodingRecord.Count);
            ServiceGlCodingRecordTitle = (string)dbHelper.serviceGLCoding.GetByID(ServiceGlCodingRecord[0], "name")["name"];

            serviceGLCodingsRecordPage
                .ValidateCareTypeFieldLinkText(careTypeNameA)
                .ValidateClientCategoryFieldLinkText("Cat355")
                .ValidateServiceGLCodeFieldLinkText(expenditureCode1 + " \\ " + "GLC_" + expenditureCode1 + " \\ " + "Exempt from Charging? = No")
                .ValidateServiceElement2OrCareTypeAllNoOptionChecked()
                .ValidateClientCategoryAllNoOptionChecked()
                .ValidateRecordTitle(ServiceGlCodingRecordTitle);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-369

        [TestProperty("JiraIssueID", "ACC-370")]
        [Description("Steps 12 to 17 from the original jira test. Verifying the Service GL Coding in Service Element 1 record.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceGLCodings_UITestMethod003()
        {
            #region Step 12 to Step 17

            #region GL Code Location
            _GLCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Service Element 1 / Service Element 2 / Client Category")[0];
            _GLCodeLocationId_Team = dbHelper.glCodeLocation.GetByName("Team / Service Element 1 / Service Element 2 / Client Category")[0];

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId1, "GLC_" + expenditureCode1, expenditureCode1, "369A");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId_Team, "GLC_" + expenditureCode2, expenditureCode2, "369B");

            #endregion

            #region Step 12 and Step 13

            #region Client Category
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var clientCategoryId = commonMethodsDB.CreateFinanceClientCategory(_careDirectorQA_TeamId, "Cat355", new DateTime(2020, 1, 1), code.ToString());
            #endregion

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "ACC_369_Provider1_" + partialDateTimeSuffix;
            var serviceElement1Name_Carer = "ACC_369_Carer1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer            
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);
            #endregion

            #region Service Element 2

            var serviceElement2NameA = "ACC_369_SE2_A_" + partialDateTimeSuffix;
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameA, startDate, code1);

            #endregion

            #region Care Type

            var careTypeNameA = "ACC_369_CareTypeA" + partialDateTimeSuffix;
            var _careTypeIdA = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameA, code1, startDate);

            #endregion

            #region Service Mapping

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_A);
            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id_Carer, _careTypeIdA, false);

            #endregion

            #region Service GL Coding

            var carerServiceGLCoding = dbHelper.serviceGLCoding.CreateServiceGLCoding(_careDirectorQA_TeamId, false, false, serviceElement1Id_Carer, null, _careTypeIdA,
                clientCategoryId, glCodeId1, null, null);
            var providerServiceGLCoding = dbHelper.serviceGLCoding.CreateServiceGLCoding(_careDirectorQA_TeamId, false, false, serviceElement1Id, serviceElement2Id_A, null,
                clientCategoryId, glCodeId1, null, null);

            #endregion

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

            ValidateServiceGLCodingRecordDuplicate("Carer", serviceElement1Name_Carer, serviceElement1Id_Carer, null, null, careTypeNameA, _careTypeIdA,
                "Cat355", clientCategoryId, expenditureCode1, glCodeId1);

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickBackButton();

            ValidateServiceGLCodingRecordDuplicate("Provider", serviceElement1Name_Provider, serviceElement1Id, serviceElement2NameA, serviceElement2Id_A, "", null,
                "Cat355", clientCategoryId, expenditureCode1, glCodeId1);

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 16 and Step 17

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .OpenRecord(providerServiceGLCoding.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateServiceElement1LookupButtonDisabled(true)
                .ValidateServiceElement2LookupButtonDisabled(true)
                .ValidateClientCategoryLookupButtonDisabled(true)
                .ValidateServiceElement2OrCareTypeAllButtonsDisabled(true)
                .ValidateClientCategoryAllButtonsDisabled(true)
                .ValidateServiceGLCodeLookupButtonDisabled(false)
                .ValidateTeamLookupButtonDisabled(true)
                .ValidateTeamGLCodeLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true);

            serviceGLCodingsRecordPage
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickBackButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Carer.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .OpenRecord(carerServiceGLCoding.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateServiceElement1LookupButtonDisabled(true)
                .ValidateCareTypeLookupButtonDisabled(true)
                .ValidateClientCategoryLookupButtonDisabled(true)
                .ValidateServiceElement2OrCareTypeAllButtonsDisabled(true)
                .ValidateClientCategoryAllButtonsDisabled(true)
                .ValidateServiceGLCodeLookupButtonDisabled(false)
                .ValidateTeamLookupButtonDisabled(true)
                .ValidateTeamGLCodeLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true);

            serviceGLCodingsRecordPage
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ValidateServiceGLCodingRecordIsNotPresent(carerServiceGLCoding.ToString())
                .ClickBackButton();

            #endregion

            #region Step 14 and Step 15

            var providerTeamGLCodingId1 = dbHelper.serviceGLCoding.CreateServiceGLCoding(_careDirectorQA_TeamId, false, false, serviceElement1Id, serviceElement2Id_A, null,
                clientCategoryId, null, _careDirectorQA_TeamId, glCodeId2);

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            System.Threading.Thread.Sleep(1200);

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ValidateServiceGLCodingRecordIsPresent(providerTeamGLCodingId1.ToString())
                .OpenRecord(providerTeamGLCodingId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ValidateServiceElement1LookupButtonDisabled(true)
                .ValidateServiceElement2LookupButtonDisabled(true)
                .ValidateClientCategoryLookupButtonDisabled(true)
                .ValidateServiceElement2OrCareTypeAllButtonsDisabled(true)
                .ValidateClientCategoryAllButtonsDisabled(true)
                .ValidateServiceGLCodeLookupButtonDisabled(true)
                .ValidateTeamLookupButtonDisabled(true)
                .ValidateTeamGLCodeLookupButtonDisabled(false)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ClickBackButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceElement2OrCareTypeAllYesOption()
                .ValidateServiceElement2LookupButtonDisabled(true)
                .ClickClientCategoryLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Cat355")
                .TapSearchButton()
                .SelectResultElement(clientCategoryId.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceGlCodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(expenditureCode1)
                .TapSearchButton()
                .SelectResultElement(glCodeId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickSaveButton();
            /*
             * Bug: https://advancedcsg.atlassian.net/browse/CDV6-25033
             */
            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("The combination of Service Element 2, Service Element 2 All? and Service Element 2 Nulls?across all relevant Active records for this Service Element 1 / Team combination is not permissible.Please correct as necessary")
                .TapCloseButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickBackButton();

            var ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(serviceElement1Id);
            Assert.AreEqual(2, ServiceGlCodingRecord.Count);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ClickNewRecordButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickClientCategoryAllYesOption()
                .ValidateClientCategoryLookupButtonDisabled(true)
                .ClickServiceElement2LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2NameA)
                .TapSearchButton()
                .SelectResultElement(serviceElement2Id_A.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickServiceGlCodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(expenditureCode1)
                .TapSearchButton()
                .SelectResultElement(glCodeId1.ToString());

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickSaveButton();

            /*
             * Bug: https://advancedcsg.atlassian.net/browse/CDV6-25033
             */
            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("The combination of Client Category, Client Category All? and Client Category Nulls?across all relevant Active records for this Service Element 1 / Team combination is not permissible.Please correct as necessary")
                .TapCloseButton();

            serviceGLCodingsRecordPage
                .WaitForServiceGLCodingsRecordPageToLoad()
                .ClickBackButton();

            ServiceGlCodingRecord = dbHelper.serviceGLCoding.GetByServiceElement1Id(serviceElement1Id);
            Assert.AreEqual(2, ServiceGlCodingRecord.Count);

            #endregion

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-429

        [TestProperty("JiraIssueID", "ACC-430")]
        [Description("Steps 18 to 20 from the original jira test. Verifying the Service GL Coding in Service Element 1 record.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceGLCodings_UITestMethod004()
        {
            #region Step 18
            #region GL Code Location
            _GLCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Service Element 1 / Service Element 2 / Client Category")[0];
            _GLCodeLocationId_Team = dbHelper.glCodeLocation.GetByName("Team / Service Element 1 / Service Element 2 / Client Category")[0];

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId1, "GLC_" + expenditureCode1, expenditureCode1, "369A");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, _GLCodeLocationId_Team, "GLC_" + expenditureCode2, expenditureCode2, "369B");

            #endregion            

            #region Client Category
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var clientCategoryId = commonMethodsDB.CreateFinanceClientCategory(_careDirectorQA_TeamId, "Cat355", new DateTime(2020, 1, 1), code.ToString());
            #endregion

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "ACC_429_Provider1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider            
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            #endregion

            #region Service Element 2

            var serviceElement2NameA = "ACC_429_SE2_A_" + partialDateTimeSuffix;
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameA, startDate, code1);

            var serviceElement2NameB = "ACC_429_SE2_B_" + partialDateTimeSuffix;
            var serviceElement2Id_B = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameB, startDate, code2);

            #endregion

            #region Service Mapping

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_A);
            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_B);

            #endregion

            #region Service GL Coding

            var providerServiceGLCoding = dbHelper.serviceGLCoding.CreateServiceGLCoding(_careDirectorQA_TeamId, false, false, serviceElement1Id, serviceElement2Id_A, null,
                clientCategoryId, glCodeId1, null, null);
            var providerTeamGLCodingId1 = dbHelper.serviceGLCoding.CreateServiceGLCoding(_careDirectorQA_TeamId, false, false, serviceElement1Id, serviceElement2Id_B, null,
                clientCategoryId, null, _careDirectorQA_TeamId, glCodeId2);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service GL Codings")
                .SelectFilter("1", "Inactive")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "No");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Service Element 1")
                .SelectOperator("2", "Equals")
                .ClickRuleValueLookupButton("2");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup Records").TypeSearchQuery(serviceElement1Name_Provider).TapSearchButton().SelectResultElement(serviceElement1Id.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(providerServiceGLCoding.ToString())
                .ValidateSearchResultRecordPresent(providerTeamGLCodingId1.ToString())
                .ResultsPageValidateHeaderCellText(2, "Service Element 1")
                .ClickColumnHeader(2)
                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
                .ResultsPageValidateHeaderCellText(3, "Service Element 2")
                .ClickColumnHeader(3)
                .ResultsPageValidateHeaderCellSortOrdedAscending(3)
                .ResultsPageValidateHeaderCellText(4, "Care Type")
                .ClickColumnHeader(4)
                .ResultsPageValidateHeaderCellSortOrdedAscending(4)
                .ResultsPageValidateHeaderCellText(5, "Client Category")
                .ClickColumnHeader(5)
                .ResultsPageValidateHeaderCellSortOrdedAscending(5)
                .ResultsPageValidateHeaderCellText(6, "Service GL Code")
                .ClickColumnHeader(6)
                .ResultsPageValidateHeaderCellSortOrdedAscending(6)
                .ResultsPageValidateHeaderCellText(7, "Team")
                .ClickColumnHeader(7)
                .ResultsPageValidateHeaderCellSortOrdedAscending(7)
                .ResultsPageValidateHeaderCellText(8, "Team GL Code")
                .ClickColumnHeader(8)
                .ResultsPageValidateHeaderCellSortOrdedAscending(8)
                .ResultsPageValidateHeaderCellText(9, "Service Element 2 or Care Type All?")
                .ClickColumnHeader(9)
                .ResultsPageValidateHeaderCellSortOrdedAscending(9)
                .ResultsPageValidateHeaderCellText(10, "Client Category All?")
                .ClickColumnHeader(10)
                .ResultsPageValidateHeaderCellSortOrdedAscending(10);

            #endregion

            #region Step 19

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
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceGLCodingsTab();

            serviceGLCodingsPage
                .WaitForServiceGLCodingsPageToLoad()
                .ValidateSelectedViewSelectorPicklistText("Service GL Coding Associated View")
                .ValidateHeaderCellText(2, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(2)
                .ValidateHeaderCellText(3, "Service Element 2")
                .ValidateHeaderCellSortOrdedAscending(3)
                .ValidateHeaderCellText(4, "Care Type")
                .ValidateHeaderCellText(5, "Client Category")
                .ValidateHeaderCellText(6, "Service GL Code")
                .ValidateHeaderCellText(7, "Team")
                .ValidateHeaderCellText(8, "Team GL Code")
                .ValidateHeaderCellText(9, "Service Element 2 or Care Type All?")
                .ValidateHeaderCellText(10, "Client Category All?");
            #endregion

            #region Step 20

            serviceGLCodingsPage
                .InsertSearchQuery(serviceElement2NameB)
                .TapSearchButton()
                .ValidateSelectedViewSelectorPicklistText("Service GL Coding - View")
                .ValidateServiceGLCodingRecordIsPresent(providerTeamGLCodingId1.ToString())
                .ValidateServiceGLCodingRecordIsNotPresent(providerServiceGLCoding.ToString())
                .ValidateHeaderCellText(2, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(2)
                .ValidateHeaderCellText(3, "Service Element 2")
                .ValidateHeaderCellSortOrdedAscending(3)
                .ValidateHeaderCellText(4, "Care Type")
                .ValidateHeaderCellText(5, "Client Category")
                .ValidateHeaderCellText(6, "Service GL Code")
                .ValidateHeaderCellText(7, "Team")
                .ValidateHeaderCellText(8, "Team GL Code")
                .ValidateHeaderCellText(9, "Service Element 2 or Care Type All?")
                .ValidateHeaderCellText(10, "Client Category All?");
            #endregion
        }

        #endregion
    }
}
