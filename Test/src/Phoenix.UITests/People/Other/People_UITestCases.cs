using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class People_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;


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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("People BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("People T1", null, _businessUnitId, "907678", "PeopleT1@careworkstempmail.com", "People T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PeopleUser1

                _systemUsername = "PeopleUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "People", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("Person - Secure Fields (Edit)")[0];
                commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1800

        [TestProperty("JiraIssueID", "ACC-1908")]
        [Description("Step(s) 1 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            var _firstName1 = "Gilbert";
            var _lastName1 = "A_" + currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            dbHelper.person.UpdateGenderId(_person1ID, 1);//male

            #endregion

            #region Person 2

            var _firstName2 = "Ivan";
            var _lastName2 = "A_" + currentDateTimeString;
            var _person2ID = commonMethodsDB.CreatePersonRecord(_firstName2, _lastName2, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person2Number = dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];

            #endregion

            #region Person 3

            var _firstName3 = "Seth" + currentDateTimeString;
            var _lastName3 = "B_" + currentDateTimeString;
            var _person3ID = commonMethodsDB.CreatePersonRecord(_firstName3, _lastName3, _ethnicityId, _teamId, new DateTime(1987, 1, 1));

            #endregion

            #region Person 4

            var _firstName4 = "Seth" + currentDateTimeString;
            var _lastName4 = "C_" + currentDateTimeString;
            var _person4ID = commonMethodsDB.CreatePersonRecord(_firstName4, _lastName4, _ethnicityId, _teamId, new DateTime(1990, 1, 1));

            #endregion

            #region Person 5

            var _firstName5 = "Seth" + currentDateTimeString;
            var _lastName5 = "D_" + currentDateTimeString;
            var _person5ID = commonMethodsDB.CreatePersonRecord(_firstName5, _lastName5, _ethnicityId, _teamId, new DateTime(1992, 1, 1));
            dbHelper.person.UpdateDeceased(_person5ID, true, new DateTime(2023, 1, 1));

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            //search by first name
            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName("Seth" + currentDateTimeString)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordNotPresent(_person1ID)
                .ValidatePersonRecordNotPresent(_person2ID)
                .ValidatePersonRecordPresent(_person3ID)
                .ValidatePersonRecordPresent(_person4ID);

            //search by last name
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertLastName("A_" + currentDateTimeString)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordPresent(_person1ID)
                .ValidatePersonRecordPresent(_person2ID)
                .ValidatePersonRecordNotPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID);

            //search by Id
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person2Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordNotPresent(_person1ID)
                .ValidatePersonRecordPresent(_person2ID)
                .ValidatePersonRecordNotPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID);

            //search by DOB
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .ValidateDOBFromDisabled(true)
                .ValidateDOBToDisabled(true)
                .InsertDOB("01/01/2000")
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()

                .ClickOnTableHeaderIdLink() //sort ascending
                .WaitForPeoplePageToLoad()
                .ClickOnTableHeaderIdLink() //sort descending
                .WaitForPeoplePageToLoad()

                .ValidatePersonRecordPresent(_person1ID)
                .ValidatePersonRecordPresent(_person2ID)
                .ValidatePersonRecordNotPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID);

            //search by Gender
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertLastName("A_" + currentDateTimeString)
                .SelectStatedGender("Male")
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordPresent(_person1ID)
                .ValidatePersonRecordNotPresent(_person2ID)
                .ValidatePersonRecordNotPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID);

            //search by Deceased
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName("Seth" + currentDateTimeString)
                .ClickDeceasedYesRadioButton()
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordNotPresent(_person1ID)
                .ValidatePersonRecordNotPresent(_person2ID)
                .ValidatePersonRecordNotPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID)
                .ValidatePersonRecordPresent(_person5ID);

            //search by DOB From & DOB To
            peoplePage
                .ClickClearFiltersButton()
                .ClickDoNotUseViewFilterCheckbox()
                .ClickUseRangeCheckbox()
                .InsertFirstName("Seth" + currentDateTimeString)
                .InsertDOBFrom("15/12/1986")
                .InsertDOBTo("15/12/1988")
                .ValidateDOBDisabled(true)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordNotPresent(_person1ID)
                .ValidatePersonRecordNotPresent(_person2ID)
                .ValidatePersonRecordPresent(_person3ID)
                .ValidatePersonRecordNotPresent(_person4ID);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1910")]
        [Description("Step(s) 2 to 8 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region CP Bands

            var _cpBandGreenId = commonMethodsDB.CreateCPBand("Green", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Person 1

            var _firstName1 = "Gilbert";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];

            #endregion


            #region Step 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            //search by first name
            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Gilbert " + currentDateTimeString);

            #endregion

            #region Step 3

            personRecordEditPage
                .InsertFloorLevel("1")
                .InsertPropertyKeySafe("Property Key Id")
                .InsertAccessInstructions("instructions ...")
                .SelectHasLift("No")
                .InsertMedicalKeySafe("Medical Key Id");


            #endregion

            #region Step 4

            personRecordEditPage
                .InsertFloorLevel("-1")
                .ValidateFloorLevelErrorLabelVisible(false)
                .InsertFloorLevel("30")
                .ValidateFloorLevelErrorLabelVisible(false)
                .InsertFloorLevel("-2")
                .ValidateFloorLevelErrorLabelVisible(true)
                .InsertFloorLevel("31")
                .ValidateFloorLevelErrorLabelVisible(true)
                .InsertFloorLevel("1");

            personRecordEditPage
                .InsertPropertyKeySafe("1234567890123456789")
                .InsertMedicalKeySafe("1234567890123456789");

            #endregion

            #region Step 5

            personRecordEditPage
                .SelectSmoker("No")
                .ValidateBandingFieldLookupButtonByIsVisible(10) //Validate field is visible by position/index
                                                                 //.ValidateNotesFieldIsVisible(18)
                .ValidateMandatoryFieldIsDisplayed("Banding", false)
                .ClickBandingFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Green").TapSearchButton().SelectResultElement(_cpBandGreenId.ToString());

            #endregion

            #region Step 6

            personRecordEditPage
                .SelectSmoker("Yes")
                .SelectSmoker("Not known")
                .SelectHasLift("Yes")
                .SelectHasLift("Not known");

            #endregion

            #region Step 7

            personRecordEditPage
                .SelectPersonType("Person We Support")
                .SelectPreferredInvoiceDeliveryMethod("Email")
                .InsertPrimaryEmail(currentDateTimeString + "@somemail.com")
                .InsertBillingEmail(currentDateTimeString + "@somemail.com")
                .SelectLivesInASmokingHousehold("No")
                .SelectPets("No Pets")
                .ClickSaveButton();

            addressActionPopUp.WaitForAddressActionPopUpToLoad().SelectViewByText("Create new address record").TapOkButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), _firstName1 + " " + _lastName1)
                .ClickCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            #endregion

            #region Step 8 (new person record)

            personRecordPage
                .TapBackButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Carlton")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectPersonType("Person We Support")
                .SelectStatedGender("Male")
                .InsertDOB("01/01/2000")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Irish").TapSearchButton().SelectResultElement(_ethnicityId.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("XYZ1")
                .SelectPreferredInvoiceDeliveryMethod("Email")
                .InsertPrimaryEmail(currentDateTimeString + "@somemail.com")
                .InsertBillingEmail(currentDateTimeString + "@somemail.com")
                .SelectLivesInASmokingHousehold("No")
                .SelectPets("No Pets")
                .InsertFloorLevel("1")
                .InsertPropertyKeySafe("Property Key Id")
                .InsertAccessInstructions("instructions ...")
                .SelectHasLift("No")
                .InsertMedicalKeySafe("Medical Key Id")
                .SelectSmoker("No")
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord("People T1", _teamId);

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickBandingFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Green").TapSearchButton().SelectResultElement(_cpBandGreenId.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .TapSaveAndCloseButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickBackButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName("Carlton")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton();

            var newPersonId = dbHelper.person.GetByFirstAndLastName("Carlton", currentDateTimeString)[0];

            peoplePage
                .ValidatePersonRecordPresent(newPersonId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1911")]
        [Description("Step(s) 9 to 13 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen1", "People")]
        [TestProperty("Screen2", "Advanced Search")]
        public void People_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];

            #endregion

            #region Person 2

            var _firstName2 = "Jhon";
            var _lastName2 = currentDateTimeString;
            var _person2ID = commonMethodsDB.CreatePersonRecord(_firstName2, _lastName2, _ethnicityId, _teamId, new DateTime(1995, 1, 1));
            var _person2Number = dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .InsertFloorLevel("abc")
                .ValidateFloorLevelErrorLabelVisible(true)
                .ValidateFloorLevelErrorLabelText("Please enter a value between -1 and 30.");

            #endregion

            #region Step 10

            personRecordEditPage
                .ClickCloseButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("People")
                .SelectFilterInsideOptGroup("1", "Floor Level")
                .SelectFilterInsideOptGroup("1", "Property Key Safe")
                .SelectFilterInsideOptGroup("1", "Access Instructions")
                .SelectFilterInsideOptGroup("1", "Has Lift?");

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .SelectLivesAlone("With parents");

            #endregion

            #region Step 12

            personRecordEditPage
                .SelectPersonType("Person We Support")
                .SelectPreferredInvoiceDeliveryMethod("Email")
                .InsertPrimaryEmail(currentDateTimeString + "@somemail.com")
                .InsertBillingEmail(currentDateTimeString + "@somemail.com")
                .SelectLivesInASmokingHousehold("No")
                .SelectPets("No Pets")
                .ClickSaveButton();

            addressActionPopUp.WaitForAddressActionPopUpToLoad().SelectViewByText("Create new address record").TapOkButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .InsertStartDateOfAddress("17/05/2023")
                .ClickSaveButton();

            addressActionPopUp.WaitForAddressActionPopUpToLoad().SelectViewByText("Update existing address record").TapOkButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .ClickCloseButton();

            #endregion

            #region Step 13

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("People")

                .SelectFilterInsideOptGroup("1", "Last Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", currentDateTimeString)

                .ClickAddRuleButton(1)

                .SelectRecordType("People")
                .SelectFilterInsideOptGroup("2", "Lives Alone")
                .SelectOperator("2", "Equals")
                .ClickRuleValueLookupButton("2");

            lookupPopup.WaitForOptionSetLookupPopupToLoad().SelectResult("With parents");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_person1ID.ToString())
                .ValidateSearchResultRecordNotPresent(_person2ID.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1912")]
        [Description("Step(s) 14 to 19 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_person1ID, 2);
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .SelectPersonType("Prospect")
                .SelectPersonType("Referral")
                .SelectPersonType("Person We Support")
                .SelectPersonType("Contact");

            #endregion

            #region Step 15

            personRecordEditPage
                .ClickResponsibleStaffMemberLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId);

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString);

            #endregion

            #region Step 16

            personRecordEditPage
                .ClickPaysByDirectDebit_NoRadioButton()
                .ClickSuspendDebtorInvoices_YesRadioButton()
                .ValidateSuspendDebtorInvoicesReasonLookupButtonDisabled(false)
                .InsertTextOnDebtorNumber1("DB1")
                .ClickAnonymousForBilling_NoRadioButton();

            #endregion

            #region Step 17, Step 19

            //Step 17 - The code to verify the fields is deleted as per https://advancedcsg.atlassian.net/browse/ACC-8417
            //because the fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.
            //Its not possible to activate the BM programmatically, therefore, the fields are not displayed to the user. 

            personRecordEditPage
                .ClickPaysByDirectDebit_YesRadioButton()
                .ClickSaveButton()
                .ClickCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad()
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")

                .ValidateFieldNameCellText(5, "Pays by Direct Debit?")
                .ValidateNewValueCellText(5, "Yes")
                .ValidateOldValueCellText(5, "No")
                .TapCloseButton();

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("Person - Secure Fields (Edit)")[0];
            var userSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserId, _securityProfileid)[0];
            dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            auditListPage
                .WaitForAuditListPageToLoad()
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")

                .ValidateFieldNameCellText(5, "Pays by Direct Debit?")
                .ValidateNewValueCellText(5, "****")
                .ValidateOldValueCellText(5, "****")
                .TapCloseButton();

            #endregion

            #region Step 18

            //The code to verify the fields is deleted as per https://advancedcsg.atlassian.net/browse/ACC-8417
            //because the fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.

            #endregion

        }


        //Commenting the below test code as per https://advancedcsg.atlassian.net/browse/ACC-8417
        //because the fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.
        //Its not possible to activate the BM programmatically, therefore, the fields are not displayed to the user. 

        //[TestProperty("JiraIssueID", "ACC-1913")]
        //[Description("Step(s) 20 from the original jira test")]
        //[TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestMethod()]
        //[TestProperty("BusinessModule1", "Person")]
        //[TestProperty("Screen1", "People")]
        //public void People_UITestMethod005()
        //{
        //    var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

        //    #region System User PeopleUser1

        //    var _systemUsername2 = "PeopleUser2";
        //    var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "People", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
        //    var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("Person - Secure Fields (View)")[0];
        //    commonMethodsDB.CreateUserSecurityProfile(_systemUser2Id, _securityProfileid);

        //    #endregion

        //    #region Personal Money Account Transaction Type

        //    var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];

        //    #endregion

        //    #region Person 1

        //    var _firstName1 = "Wade";
        //    var _lastName1 = currentDateTimeString;
        //    var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
        //    var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
        //    dbHelper.person.UpdateFinanceDetails(_person1ID, true, "98765", "AD Ref", "123", "BA Name", transactionTypeId);

        //    #endregion

        //    #region Step 20

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_systemUsername2, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .ClickDoNotUseViewFilterCheckbox()
        //        .InsertID(_person1Number.ToString())
        //        .ClickSearchButton()
        //        .WaitForPeoplePageToLoad()
        //        .OpenPersonRecord(_person1ID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad()
        //        .TapEditButton();

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
        //        .ValidatePropertyKeySafeFieldDisabled(true)
        //        .ValidateAccessInstructionsFieldDisabled(true)
        //        .ValidatePaysByDirectDebitFieldDisabled(true)
        //        .ValidateBankAccountNumberFieldDisabled(true)
        //        .ValidateAUDDISRefFieldDisabled(true)
        //        .ValidateBankAccountSortCodeFieldDisabled(true)
        //        .ValidateBankAccountNameFieldDisabled(true)
        //        .ValidateTransactionTypeFieldDisabled(true)

        //        .ValidatePaysByDirectDebit_YesRadioButtonChecked()
        //        .ValidateBankAccountNumberText("98765")
        //        .ValidateAUDDISRefText("AD Ref")
        //        .ValidateBankAccountSortCodeText("123")
        //        .ValidateBankAccountNameText("BA Name")
        //        .ValidateTransactionTypeLinkFieldText("New Transaction")
        //        ;

        //    #endregion

        //}

        [TestProperty("JiraIssueID", "ACC-1914")]
        [Description("Step(s) 19 from the original jira test (user do not have the secure fields profile)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod006()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User PeopleUser1

            var _newSystemUsername = "PeopleUser3";
            var _newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_newSystemUsername, "People", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Personal Money Account Transaction Type

            var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];

            #endregion

            #region Person 1

            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
            dbHelper.person.UpdateFinanceDetails(_person1ID, true, "98765", "AD Ref", "123", "BA Name", transactionTypeId);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_newSystemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
                .ValidatePaysByDirectDebitSecureFieldDisplayed()
                .ValidatePropertyKeySafeSecureFieldDisplayed()
                .ValidateAccessInstructionsSecureFieldDisplayed();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1915")]
        [Description("Step(s) 19 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod007()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User PeopleUser1

            var _newSystemUsername = "PeopleUser3";
            var _newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_newSystemUsername, "People", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            #endregion

            #region Personal Money Account Transaction Type

            var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];

            #endregion

            #region Person 1

            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
            dbHelper.person.UpdateFinanceDetails(_person1ID, true, "98765", "AD Ref", "123", "BA Name", transactionTypeId);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_newSystemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertID(_person1Number.ToString())
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_person1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("person")
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")

                //Commenting the below test code as per https://advancedcsg.atlassian.net/browse/ACC-8417
                //because the fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.
                //Its not possible to activate the BM programmatically, therefore, the fields are not displayed to the user.

                //.ValidateFieldNameCellText(1, "Bank Account Number")
                //.ValidateNewValueCellText(1, "****")
                //.ValidateOldValueCellText(1, "****")

                //.ValidateFieldNameCellText(2, "AUDDIS Ref")
                //.ValidateNewValueCellText(2, "****")
                //.ValidateOldValueCellText(2, "****")

                //.ValidateFieldNameCellText(3, "Bank Account Sort Code")
                //.ValidateNewValueCellText(3, "****")
                //.ValidateOldValueCellText(3, "****")

                //.ValidateFieldNameCellText(4, "Bank Account Name")
                //.ValidateNewValueCellText(4, "****")
                //.ValidateOldValueCellText(4, "****")

                .ValidateFieldNameCellText(1, "Pays by Direct Debit?")
                .ValidateNewValueCellText(1, "****")
                .ValidateOldValueCellText(1, "****")

                //.ValidateFieldNameCellText(6, "Transaction Type")
                //.ValidateNewValueCellText(6, "****")
                //.ValidateOldValueCellText(6, "****")

                .TapCloseButton();


            #endregion

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4895

        //Commenting lines of code as per https://advancedcsg.atlassian.net/browse/ACC-8417
        //because the bank fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.
        [TestProperty("JiraIssueID", "ACC-4927")]
        [Description("Step(s) 20 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "Advanced Search")]
        public void People_UITestMethod008()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            //var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];
            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
            dbHelper.person.UpdateSuspendDebtorInvoicesField(_person1ID, false);
            //dbHelper.person.UpdateFinanceDetails(_person1ID, true, "98675423", "AD Ref", "123456", "BA Name", transactionTypeId);

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            //wait for click advanced search button
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("People")
                .ClickSelectFilterFieldOption("1")
                .SelectFilter("1", "Suspend Debtor Invoices?")
                .SelectFilter("1", "Suspend Debtor Invoices Reason")
                .SelectFilter("1", "Pays by Direct Debit?")
                //.SelectFilter("1", "Bank Account Name")
                //.SelectFilter("1", "Bank Account Sort Code")
                //.SelectFilter("1", "AUDDIS Ref")
                //.SelectFilter("1", "Transaction Type")
                .SelectFilter("1", "Banding")
                //.SelectFilter("1", "Bank Account Number")
                .SelectFilter("1", "Suspend Debtor Invoices?")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "No")
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Id")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", _person1Number.ToString())
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_person1ID.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4928")]
        [Description("Step(s) 21 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("BusinessModule2", "System Features")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod009()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];
            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;
            var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
            dbHelper.person.UpdateFinanceDetails(_person1ID, true, "75423012", "DA Ref", "456321", "DA Name", transactionTypeId);
            dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_person1ID, 2);
            dbHelper.person.UpdatePreferredTime(_person1ID, 2);

            #endregion

            #region Person 2

            var _firstName2 = "Matt";
            var _lastName2 = currentDateTimeString;
            var _person2ID = commonMethodsDB.CreatePersonRecord(_firstName2, _lastName2, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
            var _person2Number = dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];
            dbHelper.person.UpdateFinanceDetails(_person2ID, true, "42311234", "CA Ref", "654123", "CA Name", transactionTypeId);
            dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_person2ID, 2);
            dbHelper.person.UpdatePreferredTime(_person2ID, 2);

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            //navigate to people page
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            // and search for _lastName1
            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertLastName(_lastName1)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordPresent(_person1ID)
                .ValidatePersonRecordPresent(_person2ID);

            //select person record 1 and person record 2 and click bulk edit button
            peoplePage
                .SelectPersonRecord(_person1ID.ToString())
                .SelectPersonRecord(_person2ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            Guid _auditReasonId = commonMethodsDB.CreateErrorManagementReason("Care coordinator change", new DateTime(2020, 1, 1), 3, _teamId, false);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateCheckBox("allowemail")
                .ClickYesRadioButtonField("allowemail")
                .ClickUpdateCheckBox("allowmail")
                .ClickYesRadioButtonField("allowmail")
                .ClickUpdateCheckBox("allowphone")
                .ClickYesRadioButtonField("allowphone")
                .ClickUpdateCheckBox("anonymousforbilling")
                .ClickYesRadioButtonField("anonymousforbilling")
                .ClickUpdateCheckBox("personpreferreddocumentsdeliverymethodid")
                .SelectValueInPicklistField("personpreferreddocumentsdeliverymethodid", "Email")
                .ClickUpdateCheckBox("preferredcontacttimeid")
                .SelectValueInPicklistField("preferredcontacttimeid", "Anytime")
                .ClickUpdateCheckBox("cpsuspenddebtorinvoices")
                .ClickYesRadioButtonField("cpsuspenddebtorinvoices")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Care coordinator change").TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SelectPersonRecord(_person1ID.ToString()) //this step is just to make sure the update took place and we are in the people page again
                .SelectPersonRecord(_person2ID.ToString()); //this step is just to make sure the update took place and we are in the people page again


            var fields1 = dbHelper.person.GetPersonById(_person1ID, "allowemail", "allowmail", "allowphone", "anonymousforbilling", "personpreferreddocumentsdeliverymethodid", "preferredcontacttimeid", "cpsuspenddebtorinvoices");
            Assert.AreEqual(true, ((bool)fields1["allowemail"]));
            Assert.AreEqual(true, ((bool)fields1["allowmail"]));
            Assert.AreEqual(true, ((bool)fields1["allowphone"]));
            Assert.AreEqual(true, ((bool)fields1["anonymousforbilling"]));
            Assert.AreEqual(1, ((int)fields1["personpreferreddocumentsdeliverymethodid"]));
            Assert.AreEqual(4, ((int)fields1["preferredcontacttimeid"]));
            Assert.AreEqual(true, ((bool)fields1["cpsuspenddebtorinvoices"]));

            var fields2 = dbHelper.person.GetPersonById(_person2ID, "allowemail", "allowmail", "allowphone", "anonymousforbilling", "personpreferreddocumentsdeliverymethodid", "preferredcontacttimeid", "cpsuspenddebtorinvoices");
            Assert.AreEqual(true, ((bool)fields2["allowemail"]));
            Assert.AreEqual(true, ((bool)fields2["allowmail"]));
            Assert.AreEqual(true, ((bool)fields2["allowphone"]));
            Assert.AreEqual(true, ((bool)fields2["anonymousforbilling"]));
            Assert.AreEqual(1, ((int)fields2["personpreferreddocumentsdeliverymethodid"]));
            Assert.AreEqual(4, ((int)fields2["preferredcontacttimeid"]));
            Assert.AreEqual(true, ((bool)fields2["cpsuspenddebtorinvoices"]));

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4929")]
        [Description("Step(s) 24 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Person")]
        [TestProperty("Screen1", "People")]
        public void People_UITestMethod010()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person 1

            var _firstName1 = "Wade";
            var _lastName1 = currentDateTimeString;

            #endregion

            #region Step 24

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            //navigate to people page
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName(_firstName1 + _lastName1)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertFirstName(_firstName1)
                .InsertLastName(_lastName1)
                .SelectStatedGender("Male")
                .ClickEthnicityLookupButton();

            //pick ethnicity from lookup popup window
            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Irish").TapSearchButton().SelectResultElement(_ethnicityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertDOB("01/01/1975")
                .SelectAddressType("Home")
                .ClickResponsibleTeamLookupButton();

            //pick team from lookup popup window
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord("People T1", _teamId);

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectPersonType("Prospect")
                .ValidateMandatoryFieldIsDisplayed("Pets?", false)
                .ValidateMandatoryFieldIsDisplayed("Lives In A Smoking Household?", false)
                .SelectPersonType("Referral")
                .ValidateMandatoryFieldIsDisplayed("Pets?", false)
                .ValidateMandatoryFieldIsDisplayed("Lives In A Smoking Household?", false)
                .SelectPersonType("Contact")
                .ValidateMandatoryFieldIsDisplayed("Pets?", false)
                .ValidateMandatoryFieldIsDisplayed("Lives In A Smoking Household?", false)
                .SelectPersonType("Person We Support")
                .ValidateMandatoryFieldIsDisplayed("Pets?", false) //In the story ACC-7221 this field was set as option if Person Type = Person We Support
                .ValidateMandatoryFieldIsDisplayed("Lives In A Smoking Household?", false)//In the story ACC-7221 this field was set as option if Person Type = Person We Support
                .SelectPetsFieldValue("Has Pets")
                .SelectLivesInSmokinHouseholdFieldValue("No");

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .TapSaveAndCloseButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickBackButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName(_firstName1)
                .InsertLastName(_lastName1)
                .ClickSearchButton();

            var newPersonId = dbHelper.person.GetByFirstAndLastName(_firstName1, _lastName1)[0];

            peoplePage
                .ValidatePersonRecordPresent(newPersonId)
                .OpenPersonRecord(newPersonId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(newPersonId.ToString(), "Wade " + currentDateTimeString)
                .ValidatePersonTypeSelectedText("Person We Support")
                .ValidatePetsFieldSelectedValue("Has Pets")
                .ValidateLivesInASmokingHouseholdFieldSelectedValue("No");

            #endregion
        }

        //Commenting the below test code as per https://advancedcsg.atlassian.net/browse/ACC-8417
        //because the fields are displayed only when Direct Debit BM is active and the BM is not active in the test environment.

        //[TestProperty("JiraIssueID", "ACC-4930")]
        //[Description("Step(s) 25 from the original jira test")]
        //[TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestMethod()]
        //[TestProperty("BusinessModule1", "Person")]
        //[TestProperty("Screen1", "People")]
        //public void People_UITestMethod011()
        //{
        //    var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

        //    #region Person 1

        //    var _firstName1 = "Wade";
        //    var _lastName1 = currentDateTimeString;
        //    var _person1ID = commonMethodsDB.CreatePersonRecord(_firstName1, _lastName1, _ethnicityId, _teamId, new DateTime(2000, 1, 1));
        //    var _person1Number = dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
        //    dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_person1ID, 2); //Letter

        //    #endregion

        //    #region Step 25

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_systemUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .ClickDoNotUseViewFilterCheckbox()
        //        .InsertID(_person1Number.ToString())
        //        .ClickSearchButton()
        //        .WaitForPeoplePageToLoad()
        //        .OpenPersonRecord(_person1ID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad()
        //        .TapEditButton();

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
        //        .SelectPersonType("Contact")
        //        .ClickResponsibleStaffMemberLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId);

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
        //        .ClickPaysByDirectDebit_NoRadioButton()
        //        .ClickSuspendDebtorInvoices_YesRadioButton()
        //        .InsertTextOnDebtorNumber1("DB 1")
        //        .ClickAnonymousForBilling_NoRadioButton();

        //    var transactionTypeId = dbHelper.personalMoneyAccountTransactionType.GetByName("New Transaction")[0];

        //    personRecordEditPage
        //        .ClickPaysByDirectDebit_YesRadioButton()
        //        .InsertTextOnBankAccountNumber("12341212")
        //        .InsertTextOnAUDDISRef("AUDDIS Ref Code")
        //        .InsertTextOnBankAccountName("A12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678Z")
        //        .ClickTransactionTypeLookupButton();

        //    lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("New Transaction").TapSearchButton().ValidateResultElementPresent(transactionTypeId).SelectResultElement(transactionTypeId);

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString);
        //    //.InsertTextOnBankAccountSortCode("12-12-"); //this field JS keeps causing issues with the test. gonna deactivate this validation (JB)

        //    Thread.Sleep(1000);

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString);
        //    //.ValidateBankAccountSortCodeFormErrorText("Only numbers are allowed in this field"); //this field JS keeps causing issues with the test. gonna deactivate this validation (JB)

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString);
        //    //.ValidateBankAccountSortCodeText("__-__-__"); //this field JS keeps causing issues with the test. gonna deactivate this validation (JB)

        //    personRecordEditPage
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
        //        .InsertTextOnBankAccountSortCode("12-12-12")
        //        .ClickSaveButton()
        //        .WaitForEditPersonRecordPopupToLoad(_person1ID.ToString(), "Wade " + currentDateTimeString)
        //        .ValidateBankAccountNumberText("12341212")
        //        .ValidateBankAccountSortCodeText("12-12-12");
        //    #endregion
        //}

        #endregion

    }
}

