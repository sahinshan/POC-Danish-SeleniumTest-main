using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceMappings
{
    [TestClass]
    public class ServiceMappings_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private Guid _ethnicityId;

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

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("ServiceMappingsUser1", "ServiceMappings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-22782

        [TestProperty("JiraIssueID", "CDV6-22929")]
        [Description("Steps 1 to 5 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod001()
        {
            #region Service Element 1

            var serviceElement1Name = "CDV6_22782_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, _careDirectorQA_TeamId, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

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
                .ValidateSelectedSystemView("Active Records");

            #endregion

            #region Step 4

            serviceElement1Page
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            #endregion

            #region Step 5

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()

                .ValidateServiceElement1LinkFieldVisibility(true)

                .ValidateServiceElement2LookupButtonVisibility(true)
                .ValidateServiceElement2LinkFieldVisibility(false)

                .ValidateResponsibleTeamLookupButtonVisibility(true)
                .ValidateResponsibleTeamLinkFieldVisibility(true)

                .ValidatePlacementLookupButtonVisibility(true)

                .ValidatePersonalBudgetTypeLookupButtonVisibility(true)

                .ValidateNotesFieldIsDisplayed(true);


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22783

        [TestProperty("JiraIssueID", "CDV6-22946")]
        [Description("Steps 1 to 5 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod002()
        {
            #region Service Element 1

            var serviceElement1Name_Provider = "CDV6_22783_Provider_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement1Name_Person = "CDV6_22783_Person_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement1Name_Carer = "CDV6_22783_Carer_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer
            var whotopayid_Person = 3; // Person
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id_Provider = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Person = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Person, _careDirectorQA_TeamId, startDate, code2, whotopayid_Person, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code3, whotopayid_Carer, paymentscommenceid_Actual, null);

            #endregion

            #region Step 6 & 7

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Provider

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Provider.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateServiceElement2LookupButtonVisibility(true)
                .ValidateServiceElement2LinkFieldVisibility(false)
                .ValidateCareTypeLookupButtonVisibility(false)
                .ValidateCareTypeLinkFieldVisibility(false);

            serviceMappingRecordPage
                .ClickBackButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickBackButton();

            //Who to pay = Person

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Person)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Person.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateServiceElement2LookupButtonVisibility(true)
                .ValidateServiceElement2LinkFieldVisibility(false)
                .ValidateCareTypeLookupButtonVisibility(false)
                .ValidateCareTypeLinkFieldVisibility(false);

            serviceMappingRecordPage
                .ClickBackButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickBackButton();

            //Who to pay = Carer

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Carer.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateServiceElement2LookupButtonVisibility(false)
                .ValidateServiceElement2LinkFieldVisibility(false)
                .ValidateCareTypeLookupButtonVisibility(true)
                .ValidateCareTypeLinkFieldVisibility(false);



            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22784

        [TestProperty("JiraIssueID", "CDV6-22949")]
        [Description("Steps 8 to 10 from the original jira test, who to pay is a provider/person")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod003A()
        {
            #region Service Element 1
            var dateTimeSuffix = commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement1Name_Provider = "CDV6_22949_Provider_" + dateTimeSuffix;
            var serviceElement1Name_Person = "CDV6_22949_Person_" + dateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider            
            var whotopayid_Person = 3; // Person
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id_Provider = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Person = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Person, _careDirectorQA_TeamId, startDate, code2, whotopayid_Person, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "SE2_CDV6_22949_" + dateTimeSuffix;
            var code4 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2 = dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement2Name, startDate, code4);

            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_careDirectorQA_TeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);

            var personalBudgetTypes = new List<Guid>();
            personalBudgetTypes.Add(dbHelper.personalBudgetType.GetByName("Community").FirstOrDefault());
            personalBudgetTypes.Add(dbHelper.personalBudgetType.GetByName("Equipment").FirstOrDefault());

            #endregion

            #region Step 8, 9 and 10

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Provider
            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Provider.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPlacementLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Family and Friends")
                .TapSearchButton()
                .ClickAddSelectedButton(lacPlacementId.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPersonalBudgetTypeLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Community")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(0).ToString())
                .TypeSearchQuery("Equipment")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(1).ToString())
                .TapOKButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .InsertNotes("Test Provider: " + dateTimeSuffix)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceMappingRecordPage
                .ValidateServiceElement1LinkFieldText(serviceElement1Name_Provider)
                .ValidateServiceElement2LinkFieldText(serviceElement2Name)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidatePlacementOptionVisible(lacPlacementId.ToString(), "Family and Friends")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(0).ToString(), "Community")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(1).ToString(), "Equipment")
                .ValidateNotesFieldValue("Test Provider: " + dateTimeSuffix);

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateRecordTitle(serviceElement1Name_Provider + " \\ " + serviceElement2Name);

            //Who to pay = Person
            serviceMappingRecordPage
                .ClickBackButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickBackButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Person)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Person.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPlacementLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Family and Friends")
                .TapSearchButton()
                .ClickAddSelectedButton(lacPlacementId.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPersonalBudgetTypeLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Community")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(0).ToString())
                .TypeSearchQuery("Equipment")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(1).ToString())
                .TapOKButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .InsertNotes("Test Person: " + dateTimeSuffix)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceMappingRecordPage
                .ValidateServiceElement1LinkFieldText(serviceElement1Name_Person)
                .ValidateServiceElement2LinkFieldText(serviceElement2Name)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidatePlacementOptionVisible(lacPlacementId.ToString(), "Family and Friends")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(0).ToString(), "Community")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(1).ToString(), "Equipment")
                .ValidateNotesFieldValue("Test Person: " + dateTimeSuffix);

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateRecordTitle(serviceElement1Name_Person + " \\ " + serviceElement2Name);
            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22785

        [TestProperty("JiraIssueID", "CDV6-22955")]
        [Description("Steps 8 to 10 from the original jira test, who to pay is a carer")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod003B()
        {
            #region Service Element 1
            var dateTimeSuffix = commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement1Name_Carer = "CDV6_22955_Carer_" + dateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whoToPayId_Carer = 2; // Carer

            var paymentscommenceid_Actual = 1; //Actual

            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code, whoToPayId_Carer, paymentscommenceid_Actual, null);

            #endregion

            #region Care Type

            var careTypeName = "SE2_CDV6_22949_" + dateTimeSuffix;
            var code4 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careType = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeName, code, startDate);

            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_careDirectorQA_TeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);

            var personalBudgetTypes = new List<Guid>();
            personalBudgetTypes.Add(dbHelper.personalBudgetType.GetByName("Community").FirstOrDefault());
            personalBudgetTypes.Add(dbHelper.personalBudgetType.GetByName("Equipment").FirstOrDefault());
            #endregion

            #region Step 8, 9 and 10

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");


            //Who to pay = Carer

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Carer.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeName)
                .TapSearchButton()
                .SelectResultElement(careType.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPlacementLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Family and Friends")
                .TapSearchButton()
                .ClickAddSelectedButton(lacPlacementId.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickPersonalBudgetTypeLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Community")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(0).ToString())
                .TypeSearchQuery("Equipment")
                .TapSearchButton()
                .AddElementToList(personalBudgetTypes.ElementAt(1).ToString())
                .TapOKButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .InsertNotes("Test Provider: " + dateTimeSuffix)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceMappingRecordPage
                .ValidateServiceElement1LinkFieldText(serviceElement1Name_Carer)
                .ValidateCareTypeLinkFieldText(careTypeName)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidatePlacementOptionVisible(lacPlacementId.ToString(), "Family and Friends")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(0).ToString(), "Community")
                .ValidatePersonalBudgetTypeOptionVisible(personalBudgetTypes.ElementAt(1).ToString(), "Equipment")
                .ValidateNotesFieldValue("Test Provider: " + dateTimeSuffix);

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateRecordTitle(serviceElement1Name_Carer + " \\ " + careTypeName);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22788

        [TestProperty("JiraIssueID", "CDV6-22951")]
        [Description("Steps 11 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod004()
        {
            #region Service Element 1

            var serviceElement1Name_Provider = "CDV6_22788_Provider_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id_Provider = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "SE_2_CDV6_22788" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement2Id = dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement2Name, DateTime.Now.Date, code1);

            #endregion

            #region Service Mapping 

            var serviceMappingId = dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement1Id_Provider, serviceElement2Id);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Provider

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Provider.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateRecordCellContent(serviceMappingId.ToString(), 2, serviceElement1Name_Provider)
                .ValidateRecordCellContent(serviceMappingId.ToString(), 3, serviceElement2Name)
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateServiceElement1LinkFieldText(serviceElement1Name_Provider)
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2Id.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("The record could not be created. There is already a Service Mapping record with the same values for Service Element 1 and Service Element 2")
              .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22789

        [TestProperty("JiraIssueID", "CDV6-22972")]
        [Description("Steps 12 to 15 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod005()
        {
            #region Service Element 1

            var serviceElement1Name_Carer = "CDV6_22789_Carer_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Carer = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code1, whotopayid_Carer, paymentscommenceid_Actual, null);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "SE_2_CDV6_22789" + commonMethodsHelper.GetCurrentDateTimeString();
            dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement2Name, DateTime.Now.Date, code1);

            #endregion

            #region Care Type

            var careTypeName = "CDV6_22789_CareType" + commonMethodsHelper.GetCurrentDateTimeString();
            var _careTypeId = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeName, code1, startDate);

            #endregion

            #region Service Mapping 

            var serviceMappingId = dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement1Id_Carer, _careTypeId, null);

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Carer

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Carer.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateRecordCellContent(serviceMappingId.ToString(), 2, serviceElement1Name_Carer)
                .ValidateRecordCellContent(serviceMappingId.ToString(), 4, careTypeName)
                .ClickNewRecordButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateServiceElement1LinkFieldText(serviceElement1Name_Carer)
                .ClickCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeName)
                .TapSearchButton()
                .SelectResultElement(_careTypeId.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("The record could not be created. There is already a Service Mapping record with the same values for Service Element 1 and Care Type")
              .TapCloseButton();

            #endregion

            #region Step 13

            dbHelper = new DBHelper.DatabaseHelper();
            var careTypeName2 = "CDV6_22788_CareType_2";
            if (!dbHelper.careType.GetByName(careTypeName2).Any())
                dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeName2, 227882, startDate);
            var _careTypeId2 = dbHelper.careType.GetByName(careTypeName2)[0];

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeName2)
                .TapSearchButton()
                .SelectResultElement(_careTypeId2.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateServiceElement1FieldIsDisabled()
                .ValidateCareTypeFieldIsDisabled()
                .ValidateResponsibleTeamFieldIsDisabled();

            #endregion

            #region Step 14 & 15

            serviceMappingRecordPage
                .ClickDeactivateButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure you want to deactivate this record? To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Inactive Service Mappings cannot be deleted.")
                .TapCloseButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ClickDeleteButton(true);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .InsertSearchQuery(careTypeName2)
                .TapSearchButton()
                .ValidateNoRecordsMessageVisible();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22790

        [TestProperty("JiraIssueID", "CDV6-22961")]
        [Description("Steps 16 to 20 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod006()
        {

            #region Service Element 1

            var serviceElement1Name = "CDV6_22790_SE1_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "CDV6_22790_SE2_A_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2Name, startDate, code1);

            serviceElement2Name = "CDV6_22790_SE2_B_" + commonMethodsHelper.GetCurrentDateTimeString();
            code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id_B = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2Name, startDate, code1);

            #endregion

            #region Service Mapping

            var serviceMappingA = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_A);
            var serviceMappingB = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_B);
            dbHelper.serviceMapping.DeactivateRecord(serviceMappingB);

            #endregion


            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Mappings")
                .SelectSavedView("Active Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Service Element 1")
                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
                .ResultsPageValidateHeaderCellText(3, "Service Element 2")
                .ResultsPageValidateHeaderCellSortOrdedDescending(3)
                .ResultsPageValidateHeaderCellText(4, "Care Type");

            #endregion

            #region Step 17

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Mappings")
                .SelectSavedView("Inactive Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Service Element 1")
                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
                .ResultsPageValidateHeaderCellText(3, "Service Element 2")
                .ResultsPageValidateHeaderCellSortOrdedDescending(3)
                .ResultsPageValidateHeaderCellText(4, "Care Type");

            #endregion

            #region Step 18

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Provider

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .ValidateSelectedSystemView("Active Service Mappings")

                .ValidateHeaderCellText(2, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(2)
                .ValidateHeaderCellText(3, "Service Element 2")
                .ValidateHeaderCellSortOrdedDescending(3)
                .ValidateHeaderCellText(4, "Care Type");

            #endregion

            #region Step 19


            serviceMappingsPage
                .SelectSystemView("Inactive Service Mappings")
                .WaitForServiceMappingsPageToLoad()

                .ValidateHeaderCellText(2, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(2)
                .ValidateHeaderCellText(3, "Service Element 2")
                .ValidateHeaderCellSortOrdedDescending(3)
                .ValidateHeaderCellText(4, "Care Type");


            #endregion

            #region Step 20

            serviceMappingsPage
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectServiceMappingRecord(serviceMappingA.ToString())

                .ValidateHeaderCellText(2, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(2)
                .ValidateHeaderCellText(3, "Service Element 2")
                .ValidateHeaderCellSortOrdedDescending(3)
                .ValidateHeaderCellText(4, "Care Type");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22791

        [TestProperty("JiraIssueID", "CDV6-23003")]
        [Description("Steps 21 to 22 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod007()
        {
            #region Service Element 1

            var serviceElement1Name_Provider_1 = "A_22791_SE1_Provider_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement1Name_Carer_1 = "B_22791_SE1_Carer_" + commonMethodsHelper.GetCurrentDateTimeString();
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

            var serviceElement1Id_Provider_1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider_1, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer_1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer_1, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "A_22791_SE2_" + commonMethodsHelper.GetCurrentDateTimeString();
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2Name, startDate, code1);

            #endregion

            #region Care Type

            var careTypeName_A = "A_22791_CareType" + commonMethodsHelper.GetCurrentDateTimeString();
            var _careTypeId_A = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeName_A, code1, startDate);

            #endregion

            #region Service Mapping

            var serviceMapping_Provider_A = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id_Provider_1, serviceElement2Id_A);
            var serviceMapping_Carer_A = dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement1Id_Carer_1, _careTypeId_A, null);

            #endregion

            #region Person

            var firstName = "Testing_CDV6_22791";
            var lastName = commonMethodsHelper.GetCurrentDateTimeString();

            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Service Provision Status

            var serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_22791").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_22791", new DateTime(2022, 1, 1));
            var serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_22791")[0];

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region  RateUnit

            var _rateUnitId = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            #endregion

            #region Service Provision

            // Carer
            var serviceProvisionID_Carer = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID,
                serviceprovisionstatusid, serviceElement1Id_Carer_1, _careTypeId_A,
                serviceprovisionstartreasonid,
                _systemUserId, placementRoomTypeId, new DateTime(2022, 6, 20), new DateTime(2022, 6, 26));

            // Provider
            var serviceProvisionID_Provider = dbHelper.serviceProvision.CreateNewServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, serviceprovisionstatusid, serviceElement1Id_Provider_1,
                    serviceElement2Id_A, _rateUnitId, serviceprovisionstartreasonid, 1, placementRoomTypeId, new DateTime(2020, 1, 1));

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForSystemUserPersonRecordPageToLoad()
                 .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .ClickServiceElement1HeaderCellToSortRecord()
                .ValidateServiceElement1HeaderCellSortOrdedAscending()
                .ValidateRecordCellContent(1, 10, serviceElement1Name_Provider_1)
                .ValidateRecordCellContent(2, 10, serviceElement1Name_Carer_1)
                .ValidateRecordCellContent(1, 11, serviceElement2Name)
                .ValidateRecordCellContent(2, 11, "")
                .ValidateRecordCellContent(1, 13, "")
                .ValidateRecordCellContent(2, 13, careTypeName_A);

            personServiceProvisionsPage
                .ClickServiceElement2HeaderCellToSortRecord()
                .ValidateServiceElement2HeaderCellSortOrdedAscending()
                .ValidateRecordCellContent(1, 10, serviceElement1Name_Carer_1)
                .ValidateRecordCellContent(2, 10, serviceElement1Name_Provider_1)
                .ValidateRecordCellContent(1, 11, "")
                .ValidateRecordCellContent(2, 11, serviceElement2Name)
                .ValidateRecordCellContent(1, 13, careTypeName_A)
                .ValidateRecordCellContent(2, 13, "");

            personServiceProvisionsPage
                .ClickCareTypeHeaderCellToSortRecord()
                .ValidateCareTypeHeaderCellSortOrdedAscending()
                .ValidateRecordCellContent(1, 10, serviceElement1Name_Provider_1)
                .ValidateRecordCellContent(2, 10, serviceElement1Name_Carer_1)
                .ValidateRecordCellContent(1, 11, serviceElement2Name)
                .ValidateRecordCellContent(2, 11, "")
                .ValidateRecordCellContent(1, 13, "")
                .ValidateRecordCellContent(2, 13, careTypeName_A);

            #endregion

            #region Step 22

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            //Who to pay = Provider

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider_1)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id_Provider_1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServiceMappingsTab();

            serviceMappingsPage
                .WaitForServiceMappingsPageToLoad()
                .OpenRecord(serviceMapping_Provider_A.ToString());

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateDeactiveButtonFieldIsDisplayed(true)
                .ValidateActiveButtonFieldIsDisplayed(false)
                .ValidateServiceElement1FieldIsDisabled()
                .ValidateServiceElement2FieldIsDisabled()
                .ValidateResponsibleTeamFieldIsDisabled()
                .ClickDeactivateButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure you want to deactivate this record? To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateDeactiveButtonFieldIsDisplayed(false)
                .ValidateActiveButtonFieldIsDisplayed(true)
                .ValidateServiceElement1FieldIsDisabled()
                .ValidateServiceElement2FieldIsDisabled()
                .ValidateResponsibleTeamFieldIsDisabled()
                .ValidatePlacementLookupFieldButtonIsDisabled()
                .ValidateNotesFieldIsDisabled()
                .ValidatePersonalBudgetTypeLookupFieldButtonIsDisabled()
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            serviceMappingRecordPage
                .WaitForServiceMappingRecordPageToLoad()
                .ValidateDeactiveButtonFieldIsDisplayed(true)
                .ValidateActiveButtonFieldIsDisplayed(false);

            #endregion

        }

        #endregion
    }
}
