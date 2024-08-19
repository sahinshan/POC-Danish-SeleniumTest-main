using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class PersonalMoneyAccount_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");


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

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonalMoneyAccount BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PersonalMoneyAccount T1", null, _businessUnitId, "907678", "PersonalMoneyAccountT1@careworkstempmail.com", "PersonalMoneyAccount T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "PersonalMoneyAccountUser1";
                _systemUserFullName = "Personal Money Account User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Personal Money Account", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4393

        [TestProperty("JiraIssueID", "ACC-4460")]
        [Description("Step(s) 1 to 4 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod01()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Bank Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);
            var careProviderPersonalMoneyAccount2Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Inactive Account", _teamId, true);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .ValidateHeaderCellText(2, "Person")
                .ValidateHeaderCellText(3, "Account Type")
                .ValidateHeaderCellText(4, "Account Name")
                .ValidateHeaderCellText(5, "Created On")
                .ValidateHeaderCellText(6, "Created By")
                .ValidateRecordPresent(careProviderPersonalMoneyAccount1Id, true)
                .ValidateRecordCellText(careProviderPersonalMoneyAccount1Id, 2, person_fullName)
                .ValidateRecordCellText(careProviderPersonalMoneyAccount1Id, 3, "Bank Account")
                .ValidateRecordCellText(careProviderPersonalMoneyAccount1Id, 4, "Default Account");

            #endregion

            #region Step 2

            personalMoneyAccountsPage
                .ValidateRecordPresent(careProviderPersonalMoneyAccount2Id, false)
                .SelectView("Active")
                .SelectView("Inactive");

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .ValidateRecordPresent(careProviderPersonalMoneyAccount1Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccount2Id, true);

            #endregion

            #region Step 3

            personalMoneyAccountsPage
                .SelectView("Active")
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ValidatePageHeaderText("Personal Money Account:\r\n" + person_fullName + " Bank Account Default Account")
                .ClickBackButton();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .ClickNewRecordButton();

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateResponsibleTeamLinkText("PersonalMoneyAccount T1")
                .ClickAccountTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickCloseButton();

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .InsertTextOnAccountName("Second account");

            #endregion

            #region Step 4

            personalMoneyAccountRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .SelectView("Inactive");

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount2Id);

            personalMoneyAccountRecordPage
                .WaitForInactivePersonalMoneyAccountRecordPageToLoad()
                .ValidatePersonLookupButtonDisabled(true)
                .ValidateAccountTypeLookupButtonDisabled(true)
                .ValidateInactive_YesRadioButtonDisabled(true)
                .ValidateInactive_NoRadioButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateAccountNameFieldDisabled(true)
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4461")]
        [Description("Step(s) 5 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod02()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Bank Account")[0];

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryTypeId = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .ClickNewRecordButton();

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ValidatePersonalMoneyAccountDetailsTabVisible(false)
                .ClickAccountTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Bank Account", careProviderPersonalMoneyAccountTypeId);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .InsertTextOnAccountName("First Account")
                .ClickSaveButton()
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ValidatePersonalMoneyAccountDetailsTabVisible(true)
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var careProviderPersonalMoneyAccountId = dbHelper.careProviderPersonalMoneyAccount.GetByPersonId(personId)[0];
            var careProviderPersonalMoneyAccountDetailId = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccountId, new DateTime(2023, 1, 1), careProviderPersonalMoneyAccountEntryTypeId, 1000, null, false, _teamId, "", 1000, null);
            var accountDetailNumber = (dbHelper.careProviderPersonalMoneyAccountDetail.GetByID(careProviderPersonalMoneyAccountDetailId, "accountdetailnumber")["accountdetailnumber"]).ToString();

            personalMoneyAccountDetailsPage
                .ClickRefreshButton()
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 2, person_fullName + " Bank Account First Account")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 3, accountDetailNumber.ToString())
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 4, "01/01/2023")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 5, "Starting Balance")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 6, "")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 7, "£1,000.00")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 8, "£1,000.00")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 9, "")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 10, "")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetailId, 11, "No");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4462")]
        [Description("Step(s) 6 to 8 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod03()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Bank Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false);

            #endregion

            #region Step 7

            personalMoneyAccountDetailRecordPage
                .ValidatePersonalMoneyAccountLinkText(person_fullName + " Bank Account Default Account")
                .ValidateDateText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateEntryTypeLinkText("Starting Balance")
                .ValidateAmountFieldLabelToolTip("User records the value associated to the entry type. Deposits must have a positive value and withdrawals must have a negative value.")
                .ValidateAmountText("")
                .InsertTextOnAmount("99999999999999999")
                .ValidateAmountFieldErrorLabelText("Please enter a value between -999999.99 and 999999.99.")
                .ValidateCancellation_NoRadioButtonChecked()

                .ValidateResponsibleTeamLinkText("PersonalMoneyAccount T1")
                .ValidateIDText("")
                .ValidateReferenceText("")
                .ValidateRunningBalanceText("")
                .InsertTextOnRunningBalance("999999999999999")
                .ValidateRunningBalanceFieldErrorLabelText("Please enter a value between -999999.99 and 999999.99.");

            #endregion

            #region Step 8

            personalMoneyAccountDetailRecordPage
                .InsertTextOnAmount("1000")
                .InsertTextOnRunningBalance("1000")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id)[0];
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, new DateTime(2023, 1, 1), careProviderPersonalMoneyAccountEntryType2Id, 100, null, false, _teamId, "", 1100, null);

            personalMoneyAccountDetailsPage
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail1Id)
                .SelectRecord(careProviderPersonalMoneyAccountDetail2Id)
                .ClickCancelButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("One and only one record needs to be selected.").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4463")]
        [Description("Step(s) 9 to 10 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod04()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("1000")
                .ClickSaveButton()
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The new record has been successfully saved and the new Running Balance is £1,000.00")
                .TapCloseButton();

            personalMoneyAccountDetailRecordPage
                .ClickBackButton();

            #endregion

            #region Step 10

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var careProviderPersonalMoneyAccountDetailId = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id)[0];

            personalMoneyAccountDetailsPage
                .SelectRecord(careProviderPersonalMoneyAccountDetailId)
                .ClickCancelButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?")
                .TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4464")]
        [Description("Step(s) 11 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod05()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0];

            #endregion

            #region Personal Money Account Detail

            var careProviderPersonalMoneyAccountDetailId = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, new DateTime(2023, 1, 1), careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", false, "");

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,000.00")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveButton()
                .ValidateEntryTypeFieldErrorLabelText("Please fill out this field.")
                .ValidateAmountFieldErrorLabelText("Please fill out this field.")
                .ValidateCashTakenByFieldErrorLabelText("Please fill out this field.")
                .ValidateObservedByFieldErrorLabelText("Please fill out this field.");

            personalMoneyAccountDetailRecordPage
                .InsertTextOnAmount("-50")
                .ClickEntryTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Cash Taken", careProviderPersonalMoneyAccountEntryType2Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveAndCloseButton()
                .ValidateCashTakenByFieldErrorLabelText("Please fill out this field.")
                .ValidateObservedByFieldErrorLabelText("Please fill out this field.")
                .ClickCashTakenByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickObservedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The new record has been successfully saved and the new Running Balance is £950.00")
                .TapCloseButton();

            personalMoneyAccountDetailRecordPage
                .ClickBackButton();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4465")]
        [Description("Step(s) 12 from the original test - Scenario where PMA Type = Cash and Entry Type = Starting Balance [Code = 1] or Entry Type = Discrepancy [Code = 2]. In both case we will use a positive Amount. For a negative amount the 2 fields will be mandatory")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod06()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0];

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", false, "");

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("1000")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var accountDetails1Id = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id)[0];

            personalMoneyAccountDetailsPage
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,000.00")
                .TapCancelButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As the amounts do not agree, you must first record a \"Discrepancy\" record to indicate what the difference is, before entering another record. NOTE: If the cash in hand is lower, then record a value with a minus (-)")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("500")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var accountDetails2Id = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id).Where(c => c != accountDetails1Id).FirstOrDefault();

            personalMoneyAccountDetailsPage
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,500.00")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("-200")
                .ClickEntryTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Cash Taken", careProviderPersonalMoneyAccountEntryType2Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveAndCloseButton()
                .ValidateCashTakenByFieldErrorLabelText("Please fill out this field.")
                .ValidateObservedByFieldErrorLabelText("Please fill out this field.");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4466")]
        [Description("Step(s) 13 to 15 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod07()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", false, "");

            #endregion

            #region Step 13 and 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("1000")
                .ClickCashTakenByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickObservedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are not permitted to record the same User in “Cash Taken By” and “Observed By”. Please action as necessary.")
                .TapCloseButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false);

            #endregion

            #region Step 15

            personalMoneyAccountDetailRecordPage
                .ClickObservedByClearButton()
                .InsertTextOnDate(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"));

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("You are not permitted to record a date later than today. Please action as necessary.").TapOKButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4467")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod08()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];

            #endregion

            #region Personal Money Account Detail


            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-390));
            var date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-340));
            var date3 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMonths(-1));
            var date4 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-4));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date2, careProviderPersonalMoneyAccountEntryType2Id, 100, null, false, _teamId, "", 1100, null);
            var careProviderPersonalMoneyAccountDetail3Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date3, careProviderPersonalMoneyAccountEntryType2Id, 50, null, false, _teamId, "", 1150, null);
            var careProviderPersonalMoneyAccountDetail4Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date4, careProviderPersonalMoneyAccountEntryType2Id, 10, null, false, _teamId, "", 1160, null);

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", true, "");

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectView("Active")
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail1Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail2Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail3Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail4Id, true)
                ;

            personalMoneyAccountDetailsPage
                .SelectView("Detail records (Past 7 days)")
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail1Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail2Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail3Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail4Id, true);

            personalMoneyAccountDetailsPage
                .SelectView("Detail records (Past Calendar Month)")
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail1Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail2Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail3Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail4Id, date4.Month == date3.Month ? true : false); //if the current data is in the first 3 days of the month, then date 4 also lands on the previous month

            personalMoneyAccountDetailsPage
                .SelectView("Detail records (Past Year)")
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail1Id, false)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail2Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail3Id, true)
                .ValidateRecordPresent(careProviderPersonalMoneyAccountDetail4Id, true);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4468")]
        [Description("Step(s) 17 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod09()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];

            #endregion

            #region Personal Money Account Detail

            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-30));
            var date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-15));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date2, careProviderPersonalMoneyAccountEntryType2Id, 100, null, false, _teamId, "PMDA 2", 1100, null);

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", true, "");

            #endregion

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,100.00")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("100")
                .InsertTextOnReference("PMDA 2")
                .ClickEntryTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Deposit", careProviderPersonalMoneyAccountEntryType2Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickCashTakenByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickObservedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_systemUserName, _systemUserId);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessageContainsText("Duplicate records are not allowed where Entry Type + Reference are identical.").TapCloseButton(); //there is currently an open bug because no message is being displayed - https://advancedcsg.atlassian.net/browse/ACC-4426

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4469")]
        [Description("Step(s) 18 to 19 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod10()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountType1Id = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];
            var careProviderPersonalMoneyAccountType2Id = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Bank Account")[0];
            var careProviderPersonalMoneyAccountType3Id = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Savings Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountType1Id, "First Account", _teamId);
            var careProviderPersonalMoneyAccount2Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountType2Id, "Second Account", _teamId);
            var careProviderPersonalMoneyAccount3Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountType3Id, "Third Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0]; // Code: 1
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Discrepancy")[0]; // Code: 2

            var careProviderPersonalMoneyAccountEntryType3Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0]; // Code: 3
            var careProviderPersonalMoneyAccountEntryType4Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Returned")[0]; // Code: 4
            var careProviderPersonalMoneyAccountEntryType5Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Deposited")[0]; // Code: 5
            var careProviderPersonalMoneyAccountEntryType6Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0]; // Code: 6
            var careProviderPersonalMoneyAccountEntryType7Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Withdrawal (by card)")[0]; // Code: 7

            #endregion

            #region Personal Money Account Detail

            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-30));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount2Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail3Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount3Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", true, "");

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,000.00")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickEntryTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .TypeSearchQuery("1").TapSearchButton().ValidateResultElementNotPresent(careProviderPersonalMoneyAccountEntryType1Id)
                .TypeSearchQuery("2").TapSearchButton().ValidateResultElementNotPresent(careProviderPersonalMoneyAccountEntryType2Id)

                .TypeSearchQuery("3").TapSearchButton().ValidateResultElementPresent(careProviderPersonalMoneyAccountEntryType3Id)
                .TypeSearchQuery("4").TapSearchButton().ValidateResultElementPresent(careProviderPersonalMoneyAccountEntryType4Id)
                .TypeSearchQuery("5").TapSearchButton().ValidateResultElementPresent(careProviderPersonalMoneyAccountEntryType5Id)
                .TypeSearchQuery("6").TapSearchButton().ValidateResultElementPresent(careProviderPersonalMoneyAccountEntryType6Id)
                .TypeSearchQuery("7").TapSearchButton().ValidateResultElementPresent(careProviderPersonalMoneyAccountEntryType7Id)
                .ClickCloseButton();

            #endregion

            #region Step 19

            //Cash Account

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ValidateRunningBalanceFieldDisabled(true)
                .ClickBackButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccountDetail1Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateRunningBalanceFieldDisabled(true)
                .ClickBackButton();

            //Bank Account

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad(false)
                .ClickBackButton();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount2Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad(true)
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccountDetail2Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateRunningBalanceFieldDisabled(false)
                .ClickBackButton();

            //Savings Account

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad(false)
                .ClickBackButton();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount3Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad(true)
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccountDetail3Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateRunningBalanceFieldDisabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4470")]
        [Description("Step(s) 20 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod11()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];

            #endregion

            #region Personal Money Account Detail


            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-390));
            var date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-340));
            var date3 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMonths(-1));
            var date4 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-4));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date2, careProviderPersonalMoneyAccountEntryType2Id, 100, null, false, _teamId, "", 1100, null);
            var careProviderPersonalMoneyAccountDetail3Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date3, careProviderPersonalMoneyAccountEntryType2Id, 50, null, false, _teamId, "", 1150, null);
            var careProviderPersonalMoneyAccountDetail4Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date4, careProviderPersonalMoneyAccountEntryType2Id, 10, null, false, _teamId, "", 1160, null);

            #endregion

            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", true, "");

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectView("Active")
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetail1Id, 3, "1")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetail2Id, 3, "2")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetail3Id, 3, "3")
                .ValidateRecordCellText(careProviderPersonalMoneyAccountDetail4Id, 3, "4")
                ;


            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4471")]
        [Description("Step(s) 21 to 24 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod12()
        {
            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];

            #endregion


            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "false", "", true, "");

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ValidateEntryTypeLinkText("Starting Balance")
                .ValidateEntryTypeLookupButtonDisabled(true);

            #endregion

            #region Step 22

            personalMoneyAccountDetailRecordPage
                .InsertTextOnAmount("1500.75")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton();

            var personalMoneyAccountDetail1ID = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id)[0];

            personalMoneyAccountDetailsPage
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,500.75")
                .TapCancelButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As the amounts do not agree, you must first record a \"Discrepancy\" record to indicate what the difference is, before entering another record. NOTE: If the cash in hand is lower, then record a value with a minus (-)");

            #endregion

            #region Step 23

            confirmDynamicDialogPopup
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ValidateEntryTypeLinkText("Discrepancy")
                .ValidateEntryTypeLookupButtonDisabled(true);

            #endregion

            #region Step 24

            personalMoneyAccountDetailRecordPage
                .InsertTextOnAmount("68.05")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton();

            var personalMoneyAccountDetail2ID = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id).Where(c => c != personalMoneyAccountDetail1ID).FirstOrDefault();

            personalMoneyAccountDetailsPage
                .SelectRecord(personalMoneyAccountDetail2ID)
                .SelectRecord(personalMoneyAccountDetail1ID)
                .ClickCancelButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("One and only one record needs to be selected.").TapOKButton();

            personalMoneyAccountDetailsPage
                .SelectRecord(personalMoneyAccountDetail2ID) //the 2nd click on the checkbox will actually deselect the record
                .ClickCancelButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?")
                .TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Cancellation of this record is not allowed.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4472")]
        [Description("Step(s) 25 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod13()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0];
            var careProviderPersonalMoneyAccountEntryType3Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Returned")[0];
            var careProviderPersonalMoneyAccountEntryType4Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Deposited")[0];
            var careProviderPersonalMoneyAccountEntryType5Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];
            var careProviderPersonalMoneyAccountEntryType6Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Withdrawal (by card)")[0];

            #endregion

            #region Personal Money Account Detail


            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-100));
            var date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-90));
            var date3 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-80));
            var date4 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-70));
            var date5 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-60));
            var date6 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-50));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date2, careProviderPersonalMoneyAccountEntryType2Id, -10, _systemUserId, false, _teamId, "", 990, _systemUserId);
            var careProviderPersonalMoneyAccountDetail3Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date3, careProviderPersonalMoneyAccountEntryType3Id, 20, null, false, _teamId, "", 1010, null);
            var careProviderPersonalMoneyAccountDetail4Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date4, careProviderPersonalMoneyAccountEntryType4Id, 40, null, false, _teamId, "", 1050, null);
            var careProviderPersonalMoneyAccountDetail5Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date5, careProviderPersonalMoneyAccountEntryType5Id, 10, null, false, _teamId, "", 1060, null);
            var careProviderPersonalMoneyAccountDetail6Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date6, careProviderPersonalMoneyAccountEntryType6Id, -30, _systemUserId, false, _teamId, "", 1030, _systemUserId);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail6Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail5Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail4Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail3Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail2Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton();

            var allPersonalMoneyAccountDetails = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id);
            Assert.AreEqual(11, allPersonalMoneyAccountDetails.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4473")]
        [Description("Step(s) 26 to 27 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod14()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0];
            var careProviderPersonalMoneyAccountEntryType3Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Deposit")[0];

            #endregion

            #region Personal Money Account Detail

            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-100));
            var date2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-90));

            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);
            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date2, careProviderPersonalMoneyAccountEntryType2Id, -15.50m, _systemUserId, false, _teamId, "", 984.5m, _systemUserId);

            #endregion

            #region Step 26

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail2Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?").TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var allPersonalMoneyAccountDetails = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id);
            Assert.AreEqual(3, allPersonalMoneyAccountDetails.Count);
            var careProviderPersonalMoneyAccountDetail3Id = allPersonalMoneyAccountDetails.Where(c => c != careProviderPersonalMoneyAccountDetail1Id && c != careProviderPersonalMoneyAccountDetail2Id).FirstOrDefault();

            personalMoneyAccountDetailsPage
                .OpenRecord(careProviderPersonalMoneyAccountDetail3Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateAmountText("15.50")
                .ValidateRunningBalanceText("1000.00")
                .ValidateCancellation_YesRadioButtonChecked()
                .ValidateAmountFieldDisabled(true)
                .ValidateRunningBalanceFieldDisabled(true)
                .ClickBackButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccountDetail2Id);

            System.Threading.Thread.Sleep(1000);
            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateAmountText("-15.50")
                .ValidateRunningBalanceText("984.50")
                .ValidateCancellation_YesRadioButtonChecked()
                .ValidateAmountFieldDisabled(true)
                .ValidateRunningBalanceFieldDisabled(true);

            #endregion

            #region Step 27

            //this step is alreay covered by other tests in this class

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4474")]
        [Description("Step(s) 28 from the original test - Add attachment for a Personal Money Account record")]
        [TestMethod]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod15()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Step 28

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .NavigateToAttachmentsPage();

            attachmentsForPersonalMoneyAccountPage
                .WaitForAttachmentsForPersonalMoneyAccountPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderpersonalmoneyaccountattachment")
                .ClickOnExpandIcon();

            attachmentForPersonalMoneyAccountRecordPage
                .WaitForAttachmentForPersonalMoneyAccountRecordPageToLoad()
                .ValidateResponsibleTeamLinkText("PersonalMoneyAccount T1")
                .ValidatePersonalMoneyAccountLinkText(person_fullName + " Cash Account Default Account")
                .InsertTitle("Att 1")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            attachmentsForPersonalMoneyAccountPage
                .WaitForAttachmentsForPersonalMoneyAccountPageToLoad()
                .ClickRefreshButton()
                .WaitForAttachmentsForPersonalMoneyAccountPageToLoad();

            var allAttachments = dbHelper.careProviderPersonalMoneyAccountAttachment.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id);
            Assert.AreEqual(1, allAttachments.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4475")]
        [Description("Step(s) 28 from the original test - Add attachment for a Personal Money Account Details record")]
        [TestMethod]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod16()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];

            #endregion

            #region Personal Money Account Detail

            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-100));
            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);

            #endregion

            #region Step 28

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccountDetail1Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad()
                .NavigateToAttachmentsPage();

            attachmentsForPersonalMoneyAccountDetailPage
                .WaitForAttachmentsForPersonalMoneyAccountDetailPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cppersonalmoneyaccountdetailattachment")
                .ClickOnExpandIcon();

            attachmentForPersonalMoneyAccountDetailRecordPage
                .WaitForAttachmentForPersonalMoneyAccountDetailRecordPageToLoad()
                .ValidateResponsibleTeamLinkText("PersonalMoneyAccount T1")
                .ValidatePersonalMoneyAccountDetailLinkText(person_fullName + " Cash Account Default Account 1 Starting Balance")
                .InsertTitle("Att 1")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            attachmentsForPersonalMoneyAccountDetailPage
                .WaitForAttachmentsForPersonalMoneyAccountDetailPageToLoad()
                .ClickRefreshButton()
                .WaitForAttachmentsForPersonalMoneyAccountDetailPageToLoad();

            var allAttachments = dbHelper.cpPersonalMoneyAccountDetailAttachment.GetByPersonalMoneyAccountDetailId(careProviderPersonalMoneyAccountDetail1Id);
            Assert.AreEqual(1, allAttachments.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4476")]
        [Description("Step(s) 29 to 31 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod17()
        {
            #region Step 29

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Personal Money Accounts")
                .SelectFilter("1", "Account Type");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Personal Money Account Details")
                .SelectFilter("1", "Amount");

            #endregion

            #region Step 30

            //step 30 is already covered by other tests in this class

            #endregion

            #region Step 31

            //this step was already tested in the test method PersonalMoneyAccount_UITestMethod14

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4477")]
        [Description("Step(s) 32 to 33 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod18()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Deposited")[0];
            var careProviderPersonalMoneyAccountEntryType3Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Taken")[0];

            #endregion

            #region Step 32

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("As this is the first record for this Account, you are required to record an opening balance amount. This is even if the amount should be £0")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnDate("01/09/2023")
                .InsertTextOnAmount("1000")
                .ClickSaveAndCloseButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad();

            var personalMoneyAccountDetails = dbHelper.careProviderPersonalMoneyAccountDetail.GetByPersonalMoneyAccountId(careProviderPersonalMoneyAccount1Id);
            Assert.AreEqual(1, personalMoneyAccountDetails.Count);
            var careProviderPersonalMoneyAccountDetail1Id = personalMoneyAccountDetails[0];

            #endregion

            #region Step 33

            #region Personal Money Account Detail

            var careProviderPersonalMoneyAccountDetail2Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, new DateTime(2023, 1, 2), careProviderPersonalMoneyAccountEntryType2Id, 7.57m, null, false, _teamId, "", 1007.57m, null);
            var careProviderPersonalMoneyAccountDetail3Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, new DateTime(2023, 1, 3), careProviderPersonalMoneyAccountEntryType2Id, 6.45m, null, false, _teamId, "", 1014.02m, null);
            var careProviderPersonalMoneyAccountDetail4Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, new DateTime(2023, 1, 3), careProviderPersonalMoneyAccountEntryType3Id, -10.94m, _systemUserId, false, _teamId, "", 1003.08m, _systemUserId);

            #endregion

            personalMoneyAccountDetailsPage
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .SelectRecord(careProviderPersonalMoneyAccountDetail3Id)
                .ClickCancelButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will create a record which is the reverse of the record selected in order to cancel it.  Do you wish to continue?")
                .TapOKButton();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();


            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £996.63")
                .TapOKButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4478")]
        [Description("Step(s) 34 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Personal Money Accounts")]
        [TestProperty("Screen", "Personal Money Accounts")]
        public void PersonalMoneyAccount_UITestMethod19()
        {
            #region System Settings

            commonMethodsDB.CreateSystemSetting("AllowPersonalMoneyAccountObserveSelf", "true", "", true, "");

            #endregion

            #region Person

            var firstName = "Daris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Personal Money Account Type

            var careProviderPersonalMoneyAccountTypeId = dbHelper.careProviderPersonalMoneyAccountType.GetByName("Cash Account")[0];

            #endregion

            #region Personal Money Account

            var careProviderPersonalMoneyAccount1Id = dbHelper.careProviderPersonalMoneyAccount.CreateCareProviderPersonalMoneyAccount(personId, careProviderPersonalMoneyAccountTypeId, "Default Account", _teamId);

            #endregion

            #region Personal Money Account Entry Type

            var careProviderPersonalMoneyAccountEntryType1Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Starting Balance")[0];
            var careProviderPersonalMoneyAccountEntryType2Id = dbHelper.careProviderPersonalMoneyAccountEntryType.GetByName("Cash Deposited")[0];

            #endregion

            #region Personal Money Account Detail


            var date1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10));
            var careProviderPersonalMoneyAccountDetail1Id = dbHelper.careProviderPersonalMoneyAccountDetail.CreateCareProviderPersonalMoneyAccountDetail(careProviderPersonalMoneyAccount1Id, date1, careProviderPersonalMoneyAccountEntryType1Id, 1000, null, false, _teamId, "", 1000, null);

            #endregion

            #region Step 34

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonalMoneyAccountsPage();

            personalMoneyAccountsPage
                .WaitForPersonalMoneyAccountsPageToLoad()
                .OpenRecord(careProviderPersonalMoneyAccount1Id);

            personalMoneyAccountRecordPage
                .WaitForPersonalMoneyAccountRecordPageToLoad()
                .ClickPersonalMoneyAccountDetailsTab();

            personalMoneyAccountDetailsPage
                .WaitForPersonalMoneyAccountDetailsPageToLoad()
                .ClickNewRecordButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please count the Running Balance to check if it agrees with the cash in hand? £1,000.00")
                .TapOKButton();

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .ClickEntryTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Cash Deposited", careProviderPersonalMoneyAccountEntryType2Id);

            personalMoneyAccountDetailRecordPage
                .WaitForPersonalMoneyAccountDetailRecordPageToLoad(false, false)
                .InsertTextOnAmount("5.99")
                .ValidateAmountFieldErrorLabelText("Please enter a value between 6 and 8.")
                .InsertTextOnAmount("8.00")
                .ValidateAmountFieldErrorLabelVisibility(false)
                .InsertTextOnAmount("8.01")
                .ValidateAmountFieldErrorLabelText("Please enter a value between 6 and 8.");

            #endregion

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }

}













