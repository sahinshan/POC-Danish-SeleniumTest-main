//Deactivating test as it should have been split by Suchi. The test code is to long and complex at this point, an it requires constant fixes to be applied to hit. The test will be deactivated and in the future it maybe be split into smaller ones so that the we can follow good development practices in terms of code

//using System;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Configuration;
//using Phoenix.DBHelper.Models;

//namespace Phoenix.UITests.People.Related_Items
//{
//    [TestClass]
//    public class PersonContracts_ServiceRate_Period_UITestCases : FunctionalTest
//    {
//        private Guid _authenticationproviderid;
//        private Guid _languageId;
//        private Guid _businessUnitId;
//        private Guid _teamId;
//        private Guid _ethnicityId;
//        private Guid _systemUserId;
//        private string _systemUserName;
//        private string _systemUserFullName;
//        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

//        [TestInitialize()]
//        public void TestsSetupMethod()
//        {
//            try
//            {
//                #region Internal

//                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

//                #endregion

//                #region Default User

//                string username = ConfigurationManager.AppSettings["Username"];
//                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

//                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
//                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
//                TimeZone localZone = TimeZone.CurrentTimeZone;
//                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

//                #endregion

//                #region Language

//                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

//                #endregion Language

//                #region Business Unit

//                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonContracts BU1");

//                #endregion

//                #region Team

//                _teamId = commonMethodsDB.CreateTeam("PersonContracts T1", null, _businessUnitId, "907678", "PersonContractsT1@careworkstempmail.com", "PersonContracts T1", "020 123456");

//                #endregion

//                #region Ethnicity

//                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

//                #endregion

//                #region Create SystemUser Record

//                _systemUserName = "PersonContractsServiceRateUser1";
//                _systemUserFullName = "PersonContractsServiceRate User1";
//                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonContracts", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
//                localZone = TimeZone.CurrentTimeZone;
//                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

//                #endregion
//            }
//            catch
//            {
//                if (driver != null)
//                    driver.Quit();

//                throw;
//            }
//        }

//        #region https: //advancedcsg.atlassian.net/browse/ACC-934

//        [TestProperty("JiraIssueID", "ACC-934")]
//        [Description("Test for Person Contract Service Rate Period")]
//        [TestMethod]
//        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        [TestProperty("BusinessModule", "Person Contracts")]
//        [TestProperty("Screen1", "Person Contracts")]
//        [TestProperty("Screen2", "Advanced Search")]
//        public void PersonContracts_UITestMethod03()
//        {
//            #region Person

//            var firstName = "OPServiceRateN";
//            var lastName = _currentDateSuffix;
//            var _person_fullName = firstName + " " + lastName;
//            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
//            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
//            var _personID1 = dbHelper.person.GetByFirstName("PServiceRateN").FirstOrDefault();

//            #endregion Person

//            #region Provider

//            var providerName = "PersonContractService " + _currentDateSuffix;
//            var providerType = 13; //Residential Establishment
//            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, true);

//            var provider2Name = "PersonContractService2" + _currentDateSuffix;
//            var provider2Type = 15; //Master Organisation
//            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

//            var provider3Name = "PersonContractService3" + _currentDateSuffix;
//            var provider3Type = 15; //Master Organisation
//            var provider3Id = commonMethodsDB.CreateProvider(provider3Name, _teamId, provider3Type, true);

//            var provider4Name = "PersonContractService4" + _currentDateSuffix;
//            var provider4Type = 15; //Master Organisation
//            var provider4Id = commonMethodsDB.CreateProvider(provider4Name, _teamId, provider4Type, true);

//            #endregion Provider

//            #region Care Provider Contract Scheme

//            var contractSchemeName = "Default_PCS" + _currentDateSuffix;
//            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 6;
//            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

//            var contractSchemeName2 = "Default_PCS2" + _currentDateSuffix;
//            var contractSchemeCode2 = dbHelper.careProviderContractScheme.GetAll().Count + 6;
//            var careProviderContractSchemeId2 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName2, new DateTime(2020, 1, 1), contractSchemeCode2, provider2Id, provider2Id);

//            var contractSchemeName3 = "Default_PCS3" + _currentDateSuffix;
//            var contractSchemeCode3 = dbHelper.careProviderContractScheme.GetAll().Count + 6;
//            var careProviderContractSchemeId3 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName3, new DateTime(2020, 1, 1), contractSchemeCode3, provider3Id, provider3Id);

//            var contractSchemeName4 = "Default_PCS4" + _currentDateSuffix;
//            var contractSchemeCode4 = dbHelper.careProviderContractScheme.GetAll().Count + 6;
//            var careProviderContractSchemeId4 = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName4, new DateTime(2020, 1, 1), contractSchemeCode4, provider4Id, provider4Id);

//            #endregion Care Provider Contract Scheme

//            #region Care Provider Service

//            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service PCS", new DateTime(2020, 1, 1), 999);
//            var careProviderServiceId2 = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service PCS2", new DateTime(2020, 1, 1), 799);
//            var careProviderServiceId3 = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service PCS3", new DateTime(2020, 1, 1), 899);
//            var careProviderServiceId4 = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service PCS4", new DateTime(2020, 1, 1), 699);

//            #endregion Care Provider Service

//            #region Care Provider Rate Unit

//            var careProviderRateUnitname1 = "Care Provider Rate Unit1 TnD No";
//            var careProviderRateUnitId1 = commonMethodsDB.CreateCareProviderRateUnit(_teamId, careProviderRateUnitname1, new DateTime(2020, 1, 1), 90099);
//            var careProviderRateUnitname2 = "Care Provider Rate Unit2 TnD Yes";
//            var careProviderRateUnitId2 = commonMethodsDB.CreateCareProviderRateUnit(_teamId, careProviderRateUnitname2, new DateTime(2020, 1, 1), 90009, true);

//            #endregion Care Provider Rate Unit

//            #region Care Provider Batch Grouping

//            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999);
//            var careProviderBatchGroupingId2 = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping2", new DateTime(2020, 1, 1), 8999);
//            var careProviderBatchGroupingId3 = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping3", new DateTime(2020, 1, 1), 7999);
//            var careProviderBatchGroupingId4 = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping4", new DateTime(2020, 1, 1), 6999);

//            #endregion Care Provider Batch Grouping

//            #region VAT Code

//            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

//            #endregion VAT Code

//            #region Care Provider Contract Service

//            var careProviderContractService = dbHelper.careProviderContractService.CreateCareProviderContractService(
//              _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
//              careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId1, 1, 1, false, true, true);

//            #endregion Care Provider Contract Service

//            #region Care Provider Person Contract

//            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId, providerId, new DateTime(2023, 6, 8), null);

//            #endregion Care Provider Person Contract

//            #region Step 1 - 2 select Override Rate ? = Yes & No, and Rates Required ? = Yes, Verify fields are present in Rate

//            loginPage
//                .GoToLoginPage()
//                .Login(_systemUserName, "Passw0rd_!");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
//                .OpenPersonRecord(_personId.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .NavigateToPersonContractsPage();

//            personContractsPage
//                .WaitForPersonContractsPageToLoad()
//                .ClickRefreshButton();

//            var personContractRecords = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
//            Assert.AreEqual(1, personContractRecords.Count);
//            var personContractRecordId = personContractRecords.First();

//            personContractsPage
//                .OpenRecord(careProviderPersonContract1Id);

//            personContractRecordPage
//                .WaitForPersonContractRecordPageToLoad();

//            providersRecordPage
//                .ClickDetailsTab();

//            personContractRecordPage
//                .ClickPersonContractServicesTab()
//                .WaitForPersonContractServicePageToLoad();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .InsertTextOnStartDateForService("08/08/2023")
//                .ClickServiceLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Service", careProviderServiceId);

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRecordPageToLoad();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .ClickPersonContractServiceRatesTab()
//                .WaitForPersonContractServiceRatesTabToLoad();

//            System.Threading.Thread.Sleep(3000);

//            var PersonContractService1 = dbHelper.careProviderPersonContractService.GetByCareProviderPersonContractIdAndSchemeId(careProviderPersonContract1Id).FirstOrDefault();
//            var PersonContractService1Title = (dbHelper.careProviderPersonContractService.GetByID(PersonContractService1, "title")["title"]).ToString();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateHeaderForPersonContractServiceRatePeriod("New")
//                .ValidateRateUnitText(careProviderRateUnitname1)
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ValidateStartDateText("")
//                .ValidateEndDateText("")
//                .ValidateRateText("");

//            #endregion step 1 - 2

//            #region Step 3 save the record

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertRateFielValue("21")
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            var careProviderPersonContractServiceRatePeriodId1 = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(PersonContractService1).FirstOrDefault();

//            #region step 11 Responsible Team, Person Contract Service & Rate Unit fields are non - editable in Edit view

//            personContractRecordPage
//                .ValidateHeaderForPersonContractServiceRatePeriod(PersonContractService1Title + " \\ " + careProviderRateUnitname1 + " \\ 09/09/2023 \\")
//                .ValidateResponsibleTeamLookupIsDisabled()
//                .ValidateRateUnitLookupIsDisabled()
//                .ValidatePersonContractServiceLookupIsDisabled();

//            #endregion step 11

//            personContractRecordPage
//                .ValidateRateValue("21.00")
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname1)
//                .ClickBackButton();

//            #endregion Step 3

//            #region step 5 Verify that Duplicate records cannot be created with the overlapping Start Date and End Date with same Person Contract Service Rate Unit

//            personContractRecordPage
//                .WaitForPersonContractServiceRatesTabToLoad();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateRateUnitText(careProviderRateUnitname1)
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ValidateStartDateText("")
//                .ValidateEndDateText("")
//                .ValidateRateText("");

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertRateFielValue("21")
//                .ClickSaveButton();

//            dynamicDialogPopup
//                .WaitForDynamicDialogPopupToLoad()
//                .ValidateMessage("There already exists record with the same combination of Person Contract Service which overlaps with this one. To save this record change Contract Service or Start / End Dates and/or Time Band Start / Time Band End values to avoid overlapping.")
//                .TapCloseButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ClickBackButton();

//            alertPopup
//                .WaitForAlertPopupToLoad()
//                .TapOKButton();

//            #endregion step 5

//            #region step 19 Charge Per Week Tab

//            personContractRecordPage
//                .WaitForPersonContractServiceRatesTabToLoadAfterRateCreation()
//                .ClickDetailsTab();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .SelectStatus("Completed")
//                .ClickYesRadioButtonForRateVerified()
//                .ClickYesRadioButtonForOverrideRate()
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .ClickPersonContractServiceChargesPerWeekTab()
//                .WaitForPersonContractServiceChargesPerWeekTabToLoad();

//            var PersonContractService1Id = dbHelper.careProviderPersonContractService.GetByPersonContractId(careProviderPersonContract1Id).FirstOrDefault();
//            var careProviderPersonContractServiceChargePerWkId = dbHelper.careProviderPersonContractServiceChargePerWk.GetByCareProviderPersonContractServiceId(PersonContractService1Id).FirstOrDefault();

//            personContractServiceChargesPerWeekPage
//                .WaitForPersonContractServiceChargesPerWeekPageToLoad()
//                .ValidateRecordPresent(careProviderPersonContractServiceChargePerWkId.ToString(), true)
//                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 2, "09/09/2023")
//                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 3, "")
//                .ValidateRecordCellText(careProviderPersonContractServiceChargePerWkId.ToString(), 4, "£21.00");

//            #endregion step 19

//            #region step 20

//            personContractServicesPage
//                .OpenRecord(careProviderPersonContractServiceChargePerWkId.ToString());

//            #region step 21 User should not have option to create / edit / delete / activate / deactivate records from list view or record view

//            personContractServiceChargesPerWeekRecordPage
//                .WaitForPersonContractServiceChargesPerWeekDetailsPageToLoad();

//            #endregion step 21

//            personContractServiceChargesPerWeekRecordPage
//                .ValidateChargePerWeekFieldHavingTextAndDisabled("21.00")
//                .ValidatePersonContractServiceLinkHavingTextAndDisabled("PersonContracts T1");

//           personContractServiceChargesPerWeekRecordPage
//                .ValidatePersonContractServiceHavingTextAndDisabled(PersonContractService1Title);

//            personContractRecordPage
//                .ValidateStartDateText("09/09/2023")
//                .ValidateEndDateText("");

//            #endregion step 20

//            #region step 23 advance search

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .ClickAdvancedSearchButton();

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Charges Per Week")

//                .SelectFilter("1", "Person")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1");

//            lookupPopup
//                .WaitForLookupPopupToLoad()
//                .SelectLookIn("All Active People")
//                .TypeSearchQuery(_person_fullName)
//                .TapSearchButton()
//                .SelectResultElement(_personId);

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceChargePerWkId.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceChargePerWkId.ToString(), 2, PersonContractService1Title);

//            personContractRecordPage
//                .ClickBackButton();

//            #endregion step 23

//            #region step 1 - 2 override ? No

//            #region Care Provider Contract Service
//            var careProviderContractService2 = dbHelper.careProviderContractService.CreateCareProviderContractService(
//              _teamId, _systemUserId, _businessUnitId, "", provider2Id, provider2Id, careProviderContractSchemeId2,
//              careProviderServiceId2, careProviderVATCodeId, careProviderBatchGroupingId2, careProviderRateUnitId1, 1, 1, false, false, false);

//            #endregion Care Provider Contract Service

//            #region Care Provider Person Contract

//            var careProviderPersonContract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, provider2Id, careProviderContractSchemeId2, provider2Id, new DateTime(2023, 6, 8), null);

//            #endregion Care Provider Person Contract

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
//                .OpenPersonRecord(_personId.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .NavigateToPersonContractsPage();

//            personContractsPage
//                .WaitForPersonContractsPageToLoad()
//                .ClickRefreshButton();

//            var personContractRecords2 = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
//            Assert.AreEqual(2, personContractRecords2.Count);
//            var personContractRecordId2 = personContractRecords2.First();
//            var personContractRecordNumber2 = (int)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId2, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]);

//            personContractsPage
//                .OpenRecord(careProviderPersonContract2Id);

//            personContractRecordPage
//                .WaitForPersonContractRecordPageToLoad();

//            providersRecordPage
//                .ClickDetailsTab();

//            personContractRecordPage
//                .ClickPersonContractServicesTab()
//                .WaitForPersonContractServicePageToLoad();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .InsertTextOnStartDateForService("08/08/2023")
//                .ClickServiceLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Service PCS2", careProviderServiceId2);

//            personContractRecordPage
//                .ClickSaveButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .VerifyPersonContractServiceRatesTabNotVisible();

//            #endregion step 1 - 2 override ? No

//            #region Step 2 Contract Service Rate Period - > Rate Unit - > Time and Days ? = Yes

//            #region Care Provider Contract Service

//            var careProviderContractService3 = dbHelper.careProviderContractService.CreateCareProviderContractService(
//              _teamId, _systemUserId, _businessUnitId, "", provider3Id, provider3Id, careProviderContractSchemeId3,
//              careProviderServiceId3, careProviderVATCodeId, careProviderBatchGroupingId3, careProviderRateUnitId2, 1, 1, false, true, true);

//            #endregion Care Provider Contract Service

//            #region Care Provider Person Contract

//            var careProviderPersonContract3Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, provider3Id, careProviderContractSchemeId3, provider3Id, new DateTime(2023, 8, 8), null);

//            #endregion Care Provider Person Contract

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
//                .OpenPersonRecord(_personId.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .NavigateToPersonContractsPage();

//            personContractsPage
//                .WaitForPersonContractsPageToLoad()
//                .ClickRefreshButton();

//            var personContractRecords3 = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
//            Assert.AreEqual(3, personContractRecords3.Count);
//            var personContractRecordId3 = personContractRecords3[2];
//            var personContractRecordNumber3 = (int)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId3, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]);

//            personContractsPage
//                .OpenRecord(careProviderPersonContract3Id);

//            personContractRecordPage
//                .WaitForPersonContractRecordPageToLoad();

//            providersRecordPage
//                .ClickDetailsTab();

//            personContractRecordPage
//                .ClickPersonContractServicesTab()
//                .WaitForPersonContractServicePageToLoad();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .InsertTextOnStartDateForService("08/08/2023")
//                .ClickServiceLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Service PCS3", careProviderServiceId3);

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .ClickSaveButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .ClickPersonContractServiceRatesTab()
//                .WaitForPersonContractServiceRatesTabToLoad();

//            System.Threading.Thread.Sleep(3000);

//            var PersonContractService3 = dbHelper.careProviderPersonContractService.GetByCareProviderPersonContractIdAndSchemeId(careProviderPersonContract3Id).FirstOrDefault();
//            var PersonContractService3Title = (dbHelper.careProviderPersonContractService.GetByID(PersonContractService3, "title")["title"]).ToString();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ValidateStartDateText("")
//                .ValidateEndDateText("")
//                .ValidateRateText("");

//            personContractServiceRatePeriodRecordPage
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSelectAllDays(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForMonday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForTuesday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForWednesday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForThursday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForFriday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSaturday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSunday(true);

//            #endregion Step 2 Contract Service Rate Period - > Rate Unit - > Time and Days ? = Yes

//            #region Step 3 create rate record

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertRateFielValue("11")
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            personContractServiceRatePeriodRecordPage
//                .ValidateTimeBandEndValueByJavaScript("00:00")
//                .ValidateTimeBandStartValueByJavaScript("00:00");

//            personContractRecordPage
//                .ValidateRateValue("11.00")
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("");

//            var careProviderPersonContractServiceRatePeriodId3 = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(PersonContractService3).FirstOrDefault();

//            #endregion Step 3 create rate record

//            #region step 4 bank holiday rate record creation

//            #region BankHolidayChargingCalendar

//            var BankHolidayChargingCalendarId = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_teamId, "ACC_934", "934");

//            foreach (var BankHoildayDates in dbHelper.cpBankHolidayDate.GetByCPBankHolidayChargingCalendarId(BankHolidayChargingCalendarId))
//                dbHelper.cpBankHolidayDate.DeleteCPBankHolidayDateRecord(BankHoildayDates);

//            #endregion BankHolidayChargingCalendar

//            #region Bank Holiday

//            var BankHolidayId = commonMethodsDB.CreateBankHoliday("Diwali 2023", new DateTime(2023, 11, 11), "Diwali 2023");
//            var BankHolidayTypeId = dbHelper.careProviderBankHolidayType.GetByName("Manual").FirstOrDefault();

//            dbHelper.cpBankHolidayDate.CreateCPBankHolidayDate(_teamId, BankHolidayChargingCalendarId, "ACC_934", BankHolidayId, BankHolidayTypeId);

//            #endregion Bank Holiday

//            #region Care Provider Contract Service

//            var careProviderContractService4 = dbHelper.careProviderContractService.CreateCareProviderContractService(
//              _teamId, _systemUserId, _businessUnitId, "", provider4Id, provider4Id, careProviderContractSchemeId4,
//              careProviderServiceId4, careProviderVATCodeId, careProviderBatchGroupingId4, careProviderRateUnitId2, 1, 1, false, true, true);

//            #endregion Care Provider Contract Service

//            #region Care Provider Person Contract

//            var careProviderPersonContract4Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, provider4Id, careProviderContractSchemeId4, provider4Id, new DateTime(2023, 8, 8), null);

//            #endregion Care Provider Person Contract

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
//                .OpenPersonRecord(_personId.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .NavigateToPersonContractsPage();

//            personContractsPage
//                .WaitForPersonContractsPageToLoad()
//                .ClickRefreshButton();

//            var personContractRecords4 = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
//            Assert.AreEqual(4, personContractRecords4.Count);
//            var personContractRecordId4 = personContractRecords4[3];
//            var personContractRecordNumber4 = (int)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId4, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]);

//            personContractsPage
//                .OpenRecord(careProviderPersonContract4Id);

//            personContractRecordPage
//                .WaitForPersonContractRecordPageToLoad();

//            providersRecordPage
//                .ClickDetailsTab();

//            personContractRecordPage
//                .ClickPersonContractServicesTab()
//                .WaitForPersonContractServicePageToLoad();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRecordPageToLoad()
//                .InsertTextOnStartDateForService("08/08/2023")
//                .ClickServiceLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Care Provider Service PCS4", careProviderServiceId4);

//            personContractRecordPage
//                .ClickSaveButton();

//            personContractRecordPage

//                .WaitForPersonContractServiceRecordPageToLoad()
//                .ClickPersonContractServiceRatesTab()
//                .WaitForPersonContractServiceRatesTabToLoad();
//            System.Threading.Thread.Sleep(3000);

//            var PersonContractService4 = dbHelper.careProviderPersonContractService.GetByCareProviderPersonContractIdAndSchemeId(careProviderPersonContract4Id).FirstOrDefault();
//            var PersonContractService4Title = (dbHelper.careProviderPersonContractService.GetByID(PersonContractService4, "title")["title"]).ToString();
//            var PersonContractService4Id = (dbHelper.careProviderPersonContractService.GetByID(PersonContractService4, "careproviderpersoncontractserviceid")["careproviderpersoncontractserviceid"]).ToString();

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ValidateStartDateText("")
//                .ValidateEndDateText("")
//                .ValidateRateText("");

//            personContractServiceRatePeriodRecordPage
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSelectAllDays(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForMonday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForTuesday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForWednesday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForThursday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForFriday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSaturday(true)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSunday(true);

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("11")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);


//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("01:00")
//                .InsertTimeBandEnd("03:00");

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("01:00")
//                .ValidateTimeBandEndValue("03:00");

//            personContractRecordPage
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("10/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934")
//                .ClickBackButton()
//                .WaitForPersonContractServiceRatesTabToLoad()
//                .ValidateFieldOrderAfterRateRecordCreation("Start Date", "End Date", "Rate", "Bank Holiday Rate", "Rate Unit", "Time Band Start", "Time Band End", "Select All Days");

//            #endregion step 4 bank holiday rate record creation

//            #region step 7 Verify that user is able to create multiple Rate period record for same date period with different Time Band Start and Time Band End, Rate Unit, Time and Days ? = Yes.

//            personContractRecordPage
//                .WaitForPersonContractServiceRatesTabToLoad();

//            System.Threading.Thread.Sleep(3000);

//            personContractsPage
//                .ClickNewRecordButton();

//            #region step 14 Validate fields should have the default values as selected:

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ValidatePersonContractServiceText(PersonContractService4Title);

//            #endregion step 14

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("11")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("03:00")
//                .InsertTimeBandEnd("07:00");

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("03:00")
//                .ValidateTimeBandEndValue("07:00");

//            personContractRecordPage
//                .ValidateRateValue("11.00")
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("10/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934")
//                .ClickBackButton()
//                .WaitForPersonContractServiceRatesTabToLoad();

//            #endregion step 7

//            #region step 8 verify error message appears as "Time Band Start must be before Time Band End

//            System.Threading.Thread.Sleep(3000);

//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad()
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateResponsibleTeamText("PersonContracts T1");

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("11")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("07:00")
//                .InsertTimeBandEnd("03:00");

//            personContractRecordPage
//                .ClickSaveButton();

//            dynamicDialogPopup
//                .WaitForDynamicDialogPopupToLoad()
//                .ValidateMessage("Time Band Start must be before Time Band End")
//                .TapCloseButton();

//            #endregion step 8

//            #region step 9 Verify that Start date cannot be after End Date

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("07:00")
//                .InsertTimeBandEnd("08:00");

//            personContractRecordPage
//                .InsertTextOnStartDate("10/09/2023")
//                .InsertTextOnEndtDate("09/09/2023");

//            dynamicDialogPopup
//                .WaitForDynamicDialogPopupToLoad()
//                .ValidateMessage("End Date must be equal or after Start Date")
//                .TapCloseButton();

//            #endregion step 9

//            #region step 17 Verify Time Band Start must be before Time Band End Except the End Time is 0: 00 - which is considered as the Maximum of the same date

//            personContractRecordPage
//                .InsertTextOnEndtDate("12/09/2023")
//                .InsertTextOnStartDate("10/09/2023");

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("08:00")
//                .InsertTimeBandEnd("00:00");

//            personContractRecordPage
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("08:00")
//                .ValidateTimeBandEndValue("00:00");

//            personContractRecordPage
//                .ValidateRateValue("11.00")
//                .ValidateStartDateText("10/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("12/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934");

//            #endregion step 17

//            #region step 13 advance search Rate Min = -999, 999.99 & Max = 999, 999.99

//            personContractRecordPage
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("11.999")
//                .InsertTextOnStartDate("09/09/2023")
//                .ValidateRateValidationMessage("Number of decimal places cannot be greater than 2.")
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("-11.999")
//                .InsertTextOnStartDate("09/09/2023")
//                .ValidateRateValidationMessage("Number of decimal places cannot be greater than 2.")
//                .InsertRateFielValue("-999999.99")
//                .InsertTextOnStartDate("09/09/2023")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("03:00")
//                .InsertTimeBandEnd("07:00");

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("03:00")
//                .ValidateTimeBandEndValue("07:00");

//            personContractRecordPage
//                .ValidateRateValue("-999999.99")
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("10/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934")
//                .ClickBackButton()
//                .WaitForPersonContractServiceRatesTabToLoad();

//            #region step 16 Select all days = yes
//            personContractsPage
//                .ClickNewRecordButton();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoad();

//            personContractServiceRatePeriodRecordPage
//                .ClickYesRadioButtonForSelectAllDays();

//            personContractRecordPage
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateResponsibleTeamText("PersonContracts T1");

//            personContractServiceRatePeriodRecordPage
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSelectAllDays(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForMonday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForTuesday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForWednesday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForThursday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForFriday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSaturday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSunday(false);

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertRateFielValue("999999.99")
//                .InsertTextOnEndtDate("10/09/2023")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("03:00")
//                .InsertTimeBandEnd("07:00");

//            personContractRecordPage
//                .ClickSaveButton();
//            dynamicDialogPopup
//                .WaitForDynamicDialogPopupToLoad()
//                .ValidateMessage("There already exists record with the same combination of Person Contract Service which overlaps with this one. To save this record change Contract Service or Start / End Dates and/or Time Band Start / Time Band End values to avoid overlapping.")
//                .TapCloseButton();
//            personContractRecordPage
//                .InsertTextOnEndtDate("23/09/2023")
//                .InsertTextOnStartDate("16/09/2023")
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);
//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoad();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("03:00")
//                .ValidateTimeBandEndValue("07:00");

//            personContractRecordPage
//                .ValidateRateValue("999999.99")
//                .ValidateStartDateText("16/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("23/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934");

//            personContractServiceRatePeriodRecordPage
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSelectAllDays(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForMonday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForTuesday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForWednesday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForThursday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForFriday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSaturday(false)
//                .ValidateRateAppliesToDays_NoRadioButtonCheckedForSunday(false);


//            personContractRecordPage
//                .ClickBackButton()
//                .WaitForPersonContractServiceRatesTabToLoad();

//            #endregion Step 16

//            #endregion step 13

//            #region step 10 advance search

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .ClickAdvancedSearchButton();

//            // search for Care Provider Rate Unit1 TnD No

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Rate Unit")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1");

//            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(careProviderRateUnitname1).TapSearchButton().SelectResultElement(careProviderRateUnitId1.ToString());
//            advanceSearchPage
//                .ClickAddRuleButton(1)
//                .SelectFilter("2", "Person")
//                .SelectOperator("2", "Equals")
//                .ClickRuleValueLookupButton("2");

//            lookupPopup
//                .WaitForLookupPopupToLoad()
//                .SelectLookIn("All Active People")
//                .TypeSearchQuery(_person_fullName)
//                .TapSearchButton()
//                .SelectResultElement(_personId);
//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId1.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId1.ToString(), 2, PersonContractService1Title);

//            personContractRecordPage
//                .ClickBackButton();
//            //  search for Care Provider Rate Unit1 TnD Yes

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Rate Unit")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1");

//            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(careProviderRateUnitname2).TapSearchButton().SelectResultElement(careProviderRateUnitId2.ToString());

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId3.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId3.ToString(), 2, PersonContractService3Title);

//            personContractRecordPage
//                .ClickBackButton();

//            // search for Rate value = 21

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Rate")
//                .SelectOperator("1", "Equals")
//                .InsertFieldOptionValue("21")
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId1.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId1.ToString(), 2, PersonContractService1Title);

//            personContractRecordPage
//                .ClickBackButton();

//            //  search for select all days = No
//            var careProviderPersonContractServiceRatePeriodId4last = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceIdAndRate(PersonContractService4, "999999.99").FirstOrDefault();
//            var careProviderPersonContractServiceRatePeriodId4first = dbHelper.careProviderPersonContractServiceRatePeriod.GetByCareProviderPersonContractServiceId(PersonContractService4).FirstOrDefault();

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Select All Days")
//                .SelectOperator("1", "Equals")
//                .SelectPicklistRuleValue("1", "No")
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId4first.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId4first.ToString(), 2, PersonContractService4Title);

//            personContractRecordPage
//                .ClickBackButton();

//            //  search for select all days = Yes

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Select All Days")
//                .SelectOperator("1", "Equals")
//                .SelectPicklistRuleValue("1", "Yes")
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId4last.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId4last.ToString(), 2, PersonContractService4Title);

//            personContractRecordPage
//                .ClickBackButton();

//            // search for End date ="10/09/2023"

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "End Date")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1")
//                .InsertFieldOptionValue("10/09/2023");

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId4first.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId4first.ToString(), 2, PersonContractService4Title);

//            personContractRecordPage
//                .ClickBackButton();

//            // search for Start date ="09/09/2023"

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Start Date")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1")
//                .InsertFieldOptionValue("09/09/2023");

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ClickColumnHeader(2)
//                .ResultsPageValidateHeaderCellSortOrdedAscending(2)
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId4first.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId4first.ToString(), 2, PersonContractService4Title);


//            personContractRecordPage
//                .ClickBackButton();

//            //   search for Bank holiday charging calendar = ACC_934

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .SelectRecordType("Person Contract Service Rate Periods")
//                .SelectFilter("1", "Bank Holiday Charging Calendar")
//                .SelectOperator("1", "Equals")
//                .ClickRuleValueLookupButton("1");

//            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("ACC_934").TapSearchButton().SelectResultElement(BankHolidayChargingCalendarId.ToString());

//            advanceSearchPage
//                .WaitForAdvanceSearchPageToLoad()
//                .ClickSearchButton()
//                .WaitForResultsPageToLoad()
//                .ValidateSearchResultRecordPresent(careProviderPersonContractServiceRatePeriodId4last.ToString())
//                .ResultsPageValidateHeaderCellText(2, "Person Contract Service")
//                .ValidateSearchResultRecordCellContent(careProviderPersonContractServiceRatePeriodId4last.ToString(), 2, PersonContractService4Title);

//            #endregion

//            #region step 12 to be implemented Start Date and End Date range lies within the Person Contract Service
//            #endregion step 12

//            #region step 18 user is able to create a new record from Advanced Search successfully

//            advanceSearchPage
//                .WaitForResultsPageToLoad()
//                .ClickNewRecordButton_ResultsPage();

//            personContractRecordPage
//                .WaitForPersonContractServiceRatePageToLoadAfterAdvancedSearch()
//                .ValidateResponsibleTeamText("PersonContracts T1")
//                .ClickPersonContractServiceLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(provider4Name, Guid.Parse(PersonContractService4Id));

//            personContractRecordPage
//                .InsertTextOnStartDate("09/09/2023")
//                .InsertTextOnEndtDate("10/09/2023")
//                .InsertRateFielValue("11");

//            personContractRecordPage
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateRateUnitTextFromAdvancedSearch(careProviderRateUnitname2)
//                .ValidatePersonContractServiceText(PersonContractService4Title);

//            personContractRecordPage
//                .ClickBankHolidayChargingCalendarLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ACC_934", BankHolidayChargingCalendarId);

//            serviceProvisionRateScheduleRecordPage
//                .InsertTimeBandStart("03:00")
//                .InsertTimeBandEnd("07:00");

//            personContractRecordPage
//                .ClickSaveButton()
//                .WaitForPersonContractServiceRatePageToLoadAfterAdvancedSearch();

//            serviceProvisionRateScheduleRecordPage
//                .ValidateTimeBandStartValue("03:00")
//                .ValidateTimeBandEndValue("07:00");

//            personContractRecordPage
//                .ValidateRateValue("11.00")
//                .ValidateStartDateText("09/09/2023")
//                .ValidateRateUnitText(careProviderRateUnitname2)
//                .ValidateEndDateText("10/09/2023")
//                .ValidateBankHolidayChargingCalendarFieldValue("ACC_934")
//                .ClickBackButton();

//            #endregion step 18

//            #endregion

//        }

//        [Description("Method will return the name of all tests and the Description of each one")]
//        [TestMethod]
//        public void GetTestNames()
//        {
//            this.GetAllTestNamesAndDescriptions();
//        }

//    }
//}