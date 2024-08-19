using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceMappings
{
    [TestClass]
    public class GLCodeMappings_UITestCases_AK : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _startReasonId;
        private String _systemUsername;
        private Guid _GLCodeLocationId1;
        private Guid _GLCodeLocationId2;
        private Guid _GLCodeLocation_ConstantId;
        private Guid _GLCodeLocation_ServiceElement1Id;
        private Guid _GLCodeLocation_Provider;
        private Guid _GLCodeLocation_Rule;


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

                #region System User GLCodeMappingsUser1
                _systemUsername = "GLCodeMappingsUser1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "GLCodeMappings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22801

        [TestProperty("JiraIssueID", "CDV6-23210")]
        [Description("Steps 9 to 11 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod002()
        {

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "CDV6_22801_Provider_" + partialDateTimeSuffix;
            var serviceElement1Name_Carer = "CDV6_22801_Carer_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            //validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);
            #endregion

            #region Care Type

            var careTypeNameA = "CDV6_22801_CareTypeA" + partialDateTimeSuffix;
            //var careTypeNameB = "CDV6_22801_CareTypeB" + partialDateTimeSuffix;
            var _careTypeIdA = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameA, code1, startDate);
            //var _careTypeIdB = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameB, code2, startDate);
            #endregion

            #region GL Code Location
            _GLCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Allowance / Fee 1")[0];
            _GLCodeLocationId2 = dbHelper.glCodeLocation.GetByName("Allowance / Fee 2")[0];
            _GLCodeLocation_ConstantId = dbHelper.glCodeLocation.GetByName("Constant")[0];
            _GLCodeLocation_ServiceElement1Id = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region Step 9

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
                .NavigateToGLCodeMappingsPage();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Allowance / Fee 2")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId2.ToString())
                .TypeSearchQuery("Allowance / Fee 1")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId1.ToString());

            #endregion

            #region Step 10
            lookupPopup
                .TypeSearchQuery("Constant")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ConstantId.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel1LinkFieldText("Constant")
                .ValidateLevel1ConstantFieldIsVisible(true);

            #endregion

            #region Step 11
            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Service Element 1")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ServiceElement1Id.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel1LinkFieldText("Service Element 1")
                .ValidateLevel2LookupButtonIsVisible(true)
                .ClickLevel2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Allowance / Fee 2")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId2.ToString())
                .TypeSearchQuery("Allowance / Fee 1")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId1.ToString());
            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22803

        [TestProperty("JiraIssueID", "CDV6-23233")]
        [Description("Steps 12 to 16 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod003()
        {

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "CDV6_22803_Provider_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            #endregion

            #region GL Code Location
            _GLCodeLocation_ConstantId = dbHelper.glCodeLocation.GetByName("Constant")[0];
            _GLCodeLocation_ServiceElement1Id = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();
            _GLCodeLocation_Provider = dbHelper.glCodeLocation.GetByName("Provider").FirstOrDefault();

            #endregion

            #region Step 12

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
                .WaitForServiceElement1PageToLoad()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToGLCodeMappingsPage();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Service Element 1")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ServiceElement1Id.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel1LinkFieldText("Service Element 1")
                .ClickLevel2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Constant")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ConstantId.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel2LinkFieldText("Constant")
                .ValidateLevel2ConstantFieldIsVisible(true);

            #endregion

            #region Step 13

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Provider")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Provider.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel2LinkFieldText("Provider")
                .ValidateLevel2ConstantFieldIsVisible(false)
                .ValidateLevel3LookupButtonIsVisible(true);

            glCodeMappingsRecordPage
                .ClickLevel3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Allowance / Fee 2")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId2.ToString())
                .TypeSearchQuery("Allowance / Fee 1")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId1.ToString());

            #endregion

            #region Step 14
            lookupPopup
                .TypeSearchQuery("Constant")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ConstantId.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel3LinkFieldText("Constant")
                .ValidateLevel3ConstantFieldIsVisible(true);

            #endregion

            #region Step 15
            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Provider")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Provider.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel3ConstantFieldIsVisible(false)
                .ValidateLevel3LinkFieldText("Provider")
                .ValidateLevel4LookupButtonIsVisible(true);

            glCodeMappingsRecordPage
                .ClickLevel4LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Allowance / Fee 2")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId2.ToString())
                .TypeSearchQuery("Allowance / Fee 1")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_GLCodeLocationId1.ToString());

            #endregion

            #region Step 16
            lookupPopup
                .TypeSearchQuery("Constant")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ConstantId.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel4LinkFieldText("Constant")
                .ValidateLevel4ConstantFieldIsVisible(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22805

        [TestProperty("JiraIssueID", "CDV6-23235")]
        [Description("Steps 17 to 19 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod004()
        {

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "CDV6_22805_Provider_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            #endregion

            #region GL Code Location            
            _GLCodeLocation_ServiceElement1Id = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();
            _GLCodeLocation_Provider = dbHelper.glCodeLocation.GetByName("Provider").FirstOrDefault();
            _GLCodeLocation_Rule = dbHelper.glCodeLocation.GetByName("Rule").FirstOrDefault();

            #endregion

            #region Step 17

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
                .WaitForServiceElement1PageToLoad()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToGLCodeMappingsPage();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Rule")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Rule.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(true)
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Service Element 1")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ServiceElement1Id.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(false)
                .ClickLevel2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Rule")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Rule.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(true)
                .ClickLevel2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Service Element 1")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_ServiceElement1Id.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(false)
                .ClickLevel3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Rule")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Rule.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(true)
                .ClickLevel3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Provider")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Provider.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(false)
                .ClickLevel4LookupButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Rule")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Rule.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(true)
                .ClickLevel4LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Provider")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Provider.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateRuleDetailsFieldIsDisplayed(false);

            #endregion

            #region Step 18

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickLevel4LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Rule")
                .TapSearchButton()
                .SelectResultElement(_GLCodeLocation_Rule.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .SelectRuleDetailsMethod("By GL Code")
                .ValidateThenUseGLCodeLocationFieldIsVisible(true)
                .ValidateThenUsePositionFieldIsVisible(false);

            #endregion

            #region Step 19
            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .SelectRuleDetailsMethod("By Position")
                .ValidateThenUsePositionFieldIsVisible(true)
                .ValidateThenUseGLCodeLocationFieldIsVisible(false);

            #endregion
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22807

        [TestProperty("JiraIssueID", "CDV6-23269")]
        [Description("Steps 24 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod006()
        {

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "CDV6_22807_Provider_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            #endregion

            #region GL Code Location

            _GLCodeLocation_ServiceElement1Id = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code Mappings
            int TotalRecordsToCreate = 9;
            for (int i = 0; i < TotalRecordsToCreate; i++)
                dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, serviceElement1Id, _GLCodeLocation_ServiceElement1Id);

            #endregion

            #region Step 24

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
                .WaitForServiceElement1PageToLoad()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToGLCodeMappingsPage();

            Assert.AreEqual(TotalRecordsToCreate, dbHelper.glCodeMapping.GetByServiceElement1Id(serviceElement1Id.ToString()).Count);

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Position Number cannot be higher than 9")
                .TapOKButton();

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ClickBackButton();

            #endregion
        }

        #endregion
    }
}

