using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-11132
    ///
    /// </summary>
    [TestClass]
    public class SystemUser_RelatedItem_EmergencyContacts_UITestCases : FunctionalTest
    {
        private Guid _environmentId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private Guid _systemUserId01;
        private string _systemUserId01UserName;
        private Guid _systemUserId02;
        private string _systemUserId02UserName;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _titleId;
        private Guid _titleId1;
        private Guid adminUserId;
        private string adminUsername;
        private string partialStringSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {
                #region Environment

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion  Business Unit

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion Team

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                //#region To delete System user :Automation_Testing_SystemUser_EmergencyContacts01, Address,Emergency contacts & Aliases Record related to the Person


                //int systemUser = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01").Count();
                //if (systemUser == 1)
                //{ //To delete System user Address
                //    foreach (var systemUserId in dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01"))
                //    {
                //        foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(systemUserId))
                //        {
                //            dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);
                //        }

                //        foreach (var systemUserAddressId in dbHelper.systemUserAddress.GetBySystemUserAddressId(systemUserId))
                //        {
                //            dbHelper.systemUser.UpdateLinkedAddress(systemUserId, null);

                //            dbHelper.systemUserAddress.DeleteSystemUserAddress_AdoNetDirectConnection(systemUserAddressId);
                //        }

                //        foreach (var userApplicationId in dbHelper.userApplication.GetUserApplicationBySystemUserId(systemUserId))
                //        {
                //            dbHelper.userApplication.DeleteUserApplication(userApplicationId);
                //        }

                //        foreach (var TeamMemberId in dbHelper.teamMember.GetTeamMemberByUserAndTeamID(systemUserId, _careProviders_TeamId))
                //        {
                //            dbHelper.teamMember.DeleteTeamMember_AdoNetDirectConnection(TeamMemberId);
                //        }

                //        foreach (var systemUserAliasId in dbHelper.systemUserAlias.GetBySystemUserAliasId(systemUserId))
                //        {
                //            dbHelper.systemUserAlias.DeleteSystemUserAlias(systemUserAliasId);
                //        }

                //        foreach (var systemUserEmergencyContactId in dbHelper.systemUserEmergencyContacts.GetBySystemUserId(systemUserId))
                //        {
                //            dbHelper.systemUserEmergencyContacts.DeleteSystemUserEmergencyContacts(systemUserEmergencyContactId);
                //        }

                //        dbHelper.systemUser.DeleteSystemUser_AdoNetDirectConnection(systemUserId);
                //    }

                //}



                //#endregion To delete System user :Automation_Testing_SystemUser_EmergencyContacts01, Address ,Emergency contacts& Aliases Record related to the Person


                //#region To delete System user :Automation_Testing_SystemUser_EmergencyContacts02, Address,Emergency contacts & Aliases Record related to the Person


                //int systemUser02 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts02").Count();
                //if (systemUser02 == 1)
                //{ //To delete System user Address
                //    foreach (var systemUserId in dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts02"))
                //    {
                //        foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(systemUserId))
                //        {
                //            dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);
                //        }

                //        foreach (var systemUserAddressId in dbHelper.systemUserAddress.GetBySystemUserAddressId(systemUserId))
                //        {
                //            dbHelper.systemUser.UpdateLinkedAddress(systemUserId, null);

                //            dbHelper.systemUserAddress.DeleteSystemUserAddress_AdoNetDirectConnection(systemUserAddressId);
                //        }

                //        foreach (var userApplicationId in dbHelper.userApplication.GetUserApplicationBySystemUserId(systemUserId))
                //        {
                //            dbHelper.userApplication.DeleteUserApplication(userApplicationId);
                //        }

                //        foreach (var TeamMemberId in dbHelper.teamMember.GetTeamMemberByUserAndTeamID(systemUserId, _careProviders_TeamId))
                //        {
                //            dbHelper.teamMember.DeleteTeamMember_AdoNetDirectConnection(TeamMemberId);
                //        }

                //        foreach (var systemUserAliasId in dbHelper.systemUserAlias.GetBySystemUserAliasId(systemUserId))
                //        {
                //            dbHelper.systemUserAlias.DeleteSystemUserAlias(systemUserAliasId);
                //        }

                //        foreach (var systemUserEmergencyContactId in dbHelper.systemUserEmergencyContacts.GetBySystemUserId(systemUserId))
                //        {
                //            dbHelper.systemUserEmergencyContacts.DeleteSystemUserEmergencyContacts(systemUserEmergencyContactId);
                //        }

                //        dbHelper.systemUser.DeleteSystemUser_AdoNetDirectConnection(systemUserId);
                //    }

                //}



                //#endregion To delete System user :Automation_Testing_SystemUser_EmergencyContacts02, Address ,Emergency contacts& Aliases Record related to the Person


                #region Create System User 1 Record

                _systemUserId01UserName = "U_EC01_" + partialStringSuffix;
                _systemUserId01 = commonMethodsDB.CreateSystemUserRecord(_systemUserId01UserName, "U_EC01", partialStringSuffix, "Summer2013@", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                //var newSystemUser01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01").Any();
                //if (!newSystemUser01)
                //    dbHelper.systemUser.CreateSystemUser("Automation_Testing_SystemUser_EmergencyContacts01", "Automation_Testing", "SystemUser", "EmergencyContacts01", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                //_systemUserId01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01")[0];

                #endregion  Create SystemUser01 Record

                #region Create System User 2 Record

                _systemUserId02UserName = "U_EC02_" + partialStringSuffix;
                _systemUserId02 = commonMethodsDB.CreateSystemUserRecord(_systemUserId02UserName, "U_EC02", partialStringSuffix, "Summer2013@", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);


                //var newSystemUser02 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts02").Any();
                //if (!newSystemUser02)
                //    dbHelper.systemUser.CreateSystemUser("Automation_Testing_SystemUser_EmergencyContacts02", "Automation_Testing02", "SystemUser", "EmergencyContacts02", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                //_systemUserId02 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts02")[0];
                //_systemUserId02UserName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId02, "username")["username"];


                #endregion  Create SystemUser01 Record

                #region Create SystemUser Record

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2" + partialStringSuffix).Any();
                if (!adminUserExists)
                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2" + partialStringSuffix, "CW", "Admin Test User 2" + partialStringSuffix, "CW Admin Test User 2", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2" + partialStringSuffix).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);
                adminUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "username")["username"];

                #endregion

                #region Title Dr

                var demographicsTitleId = dbHelper.demographicsTitle.GetByName("Dr.").Any();
                if (!demographicsTitleId)
                    dbHelper.demographicsTitle.CreateDemographicsTitle("Dr.", DateTime.Now, _careProviders_TeamId);
                _titleId = dbHelper.demographicsTitle.GetByName("Dr.")[0];

                #endregion Title Dr

                #region Title Miss

                var demographicsTitleId1 = dbHelper.demographicsTitle.GetByName("Miss.").Any();
                if (!demographicsTitleId1)
                    dbHelper.demographicsTitle.CreateDemographicsTitle("Miss.", DateTime.Now, _careProviders_TeamId);
                _titleId1 = dbHelper.demographicsTitle.GetByName("Miss.").FirstOrDefault();

                #endregion Title Miss

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13423

        [TestProperty("JiraIssueID", "ACC-3246")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "Clisck add new record button and validate the Emergency contact screen" +
            "Validate all the Mandatory fields and optional fields and its value" + "Click save button and validate the notification messages" +
            "Enter all the mandatory fields and optional fields and save the record" + "Validate the record saved" + "Click save and close button" +
            "Validate the list of created record listed in the page and column header")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod001()
        {

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            #endregion Step 1

            #region Step 2

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_EmergencyContacts();

            #endregion Step 2

            #region Step 3

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickAddNewButton();

            #endregion Step 3

            #region Step 4

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .ValidateSystemUserEmergencyContactsRecordPageTitle("New")
                .ValidateSystemUser_Editable(true)
                .ValidateSystemUser_LinkField("U_EC01 " + partialStringSuffix)
                .ValidateSystemUser_MandatoryField()
                .ValidateTitle_Field()
                .ValidateTitle_Field_Text("")
                .ValidateFirstName_Field()
                .ValidateFirstName_Field_Text("")
                .ValidateLastName_Field()
                .ValidateLastName_Field_Text("")
                .ValidateStartDate_Field()
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .ValidateStartDate_Field_Text(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNextOfKin_MandatoryField()
                .ValidateNextOfKin_NoOption()
                .ValidateContactTelehonePrimary_Field()
                .ValidateContactTelehonePrimary_Field_Text("")
                .ValidateContactTelehonePrimary_MandatoryField()
                .ValidateContactTelehoneOther1_Field()
                .ValidateContactTelehoneOther1_Field_Text("")
                .ValidateContactTelehoneOther2_Field()
                .ValidateContactTelehoneOther2_Field_Text("")
                .ValidateContactTelehoneOther3_Field()
                .ValidateContactTelehoneOther3_Field_Text("")
                .ValidateEndDate_Field()
                .ValidateEndDate_Field_Text("")

            #endregion Step 4 

            #region Step 5

                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephonePrimaryFieldErrorLabelText("Please fill out this field.")
                .ValidateFirstNameFieldErrorLabelText("Please fill out this field.")
                .ValidateLastNameFieldErrorLabelText("Please fill out this field.");

            #endregion Step 5

            #region Step 6

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertEndDate(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();


            System.Threading.Thread.Sleep(3000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(1, emergencyContacts.Count);
            var newContact = emergencyContacts[0];

            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3");


            Assert.AreEqual(_titleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Adam", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Michel", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("123456", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("000", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("111", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("222", systemUserEmergencyContactsFields["contacttelephoneother3"]);

            #endregion Step 6 

            #region Step 7 and Step 8

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateColumnHeader(2, "First Name", "First Name")
                .ValidateColumnHeader(3, "Last Name", "Last Name")
                .ValidateColumnHeader(4, "Contact Telephone (Primary)", "Contact Telephone (Primary)")
                .ValidateColumnHeader(5, "Next of Kin", "Next of Kin")
                .ValidateColumnHeader(6, "Start Date", "Start Date")
                .ValidateColumnHeader(7, "End Date", "End Date");

            #endregion Step 7 and Step 8
        }

        [TestProperty("JiraIssueID", "ACC-3249")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "Click add new record button and validate the Emergency contact screen" +
           "Select different user from the system user look up button. " + "Enter all the Mandatory and optional field" + "Click on save and close button" +
            "Validate the current user has no records available" + "Navigate to another user and validate the record is available" +
            "Validate System user field is not editable" + "Validate the telephone conatct fields should accept only the numbers ,-and()")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod002()
        {
            #region Step 9 

            loginPage
              .GoToLoginPage()
              .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                 .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ValidateSystemUser_LinkField("U_EC01 " + partialStringSuffix)
                 .ClickSystemUserLoopUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectViewByText("Lookup View")
               .TypeSearchQuery("U_EC02_" + partialStringSuffix)
               .TapSearchButton()
               .SelectResultElement(_systemUserId02.ToString());

            #endregion Step 9

            #region Step 10

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Marie")
               .InsertLastName("Samuale")
               .InsertContactTelephonePrimary("12345600")
               .InsertContactTelephoneOther1("333")
               .InsertContactTelephoneOther2("444")
               .InsertContactTelephoneOther3("555")
               .InsertEndDate(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ValidateNoRecordMessageVisibile(true);


            System.Threading.Thread.Sleep(3000);

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ClickRefreshButton();

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId02);
            Assert.AreEqual(1, emergencyContacts.Count);
            var newContact = emergencyContacts[0];

            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3");


            Assert.AreEqual(_titleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Marie", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Samuale", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("12345600", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("333", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("444", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("555", systemUserEmergencyContactsFields["contacttelephoneother3"]);


            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserId02UserName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .OpenRecord(newContact.ToString());

            #endregion Step 10

            #region Step 11

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .ValidateSystemUser_Editable(false);

            #endregion Step 11

            #region Step 12

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .InsertContactTelephonePrimary("qweerrt")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephonePrimaryFieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephonePrimary("&^%$#@!*")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephonePrimaryFieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephonePrimary("-()")
                .ClickSaveButton()
                .InsertContactTelephoneOther1("qweerrt")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther1FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther1("&^%$#@!*")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther1FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther1("-()")
                .ClickSaveButton()
                .InsertContactTelephoneOther2("qweerrt")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther2FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther2("&^%$#@!*")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther2FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther2("-()")
                .ClickSaveButton()
                .InsertContactTelephoneOther3("qweerrt")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther3FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther3("&^%$#@!*")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactTelephoneOther3FieldErrorLabelText("Please enter a valid phone number")
                .InsertContactTelephoneOther3("-()")
                .ClickSaveButton();

            #endregion Step 12

        }

        [TestProperty("JiraIssueID", "ACC-3251")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "OPen the existing emergency Contact record" +
            "Validate the edit fields and update" + "Validate the record is updated and saved succeefully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod003()
        {
            Guid emergencyContact = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "12345", "111", "222", "333", 1, DateTime.Now, DateTime.Now.AddDays(30));
            var enddate = DateTime.Now.AddDays(25).Date;

            #region Step 13

            loginPage
                 .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .OpenRecord(emergencyContact.ToString());

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Miss.")
              .TapSearchButton()
              .SelectResultElement(_titleId1.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Cesseli")
               .InsertLastName("Siann")
               .InsertContactTelephonePrimary("09876")
               .InsertContactTelephoneOther1("999")
               .InsertContactTelephoneOther2("888")
               .InsertContactTelephoneOther3("777")
               .InsertEndDate(enddate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(1, emergencyContacts.Count);
            var newContact = emergencyContacts[0];

            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3", "enddate");


            Assert.AreEqual(_titleId1.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Cesseli", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Siann", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("09876", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("999", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("888", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("777", systemUserEmergencyContactsFields["contacttelephoneother3"]);
            Assert.AreEqual(enddate, systemUserEmergencyContactsFields["enddate"]);

            #endregion Step 13

        }

        [TestProperty("JiraIssueID", "ACC-3253")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" +
            "Create new Emergency contact record where the start date is lesser than the Date of birth of the person" +
            "Validate user should be able to create a record and save")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod004()
        {


            var dob = DateTime.Now.AddYears(-5).Date;
            var startdate = DateTime.Now.AddYears(-7).Date;
            var enddate = DateTime.Now.AddDays(30).Date;

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToDetailsPage()
               .InsertBirthDate(dob.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertStartDate(startdate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertEndDate(enddate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();


            System.Threading.Thread.Sleep(3000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(1, emergencyContacts.Count);
            var newContact = emergencyContacts[0];

            var systemUserEmergencyContactsFields = dbHelper.systemUserEmergencyContacts.GetByID(emergencyContacts[0], "titleid", "firstname", "lastname", "contacttelephoneprimary", "contacttelephoneother1", "contacttelephoneother2", "contacttelephoneother3", "startdate", "enddate");


            Assert.AreEqual(_titleId.ToString(), systemUserEmergencyContactsFields["titleid"].ToString());
            Assert.AreEqual("Adam", systemUserEmergencyContactsFields["firstname"]);
            Assert.AreEqual("Michel", systemUserEmergencyContactsFields["lastname"]);
            Assert.AreEqual("123456", systemUserEmergencyContactsFields["contacttelephoneprimary"]);
            Assert.AreEqual("000", systemUserEmergencyContactsFields["contacttelephoneother1"]);
            Assert.AreEqual("111", systemUserEmergencyContactsFields["contacttelephoneother2"]);
            Assert.AreEqual("222", systemUserEmergencyContactsFields["contacttelephoneother3"]);
            Assert.AreEqual(startdate, systemUserEmergencyContactsFields["startdate"]);
            Assert.AreEqual(enddate, systemUserEmergencyContactsFields["enddate"]);


            #endregion Step 14
        }

        [TestProperty("JiraIssueID", "ACC-3254")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact record with Next of kin to yes" +
            "Create another record with Next of kin to yes " + "Validate the error message dispalyed" + "Change the next of kin to No option and save the record" +
            "Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod005()
        {
            Guid emergencyContact = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "12345", "111", "222", "333", 1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(35));

            var startdate = DateTime.Now.AddYears(-7).Date;
            var enddate = DateTime.Now.AddDays(30).Date;

            #region Step 15 and Step 16

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertStartDate(startdate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertEndDate(enddate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickNextOfKin_YesOption()
               .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for Next of Kin with dates that overlaps, please correct as necessary.")
                .TapCloseButton();

            systemUserEmergencyContactsRecordPage
              .WaitForSystemUserEmergencyContactsRecordPageToLoad()
              .ClickNextOfKin_NoOption()
              .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(2, emergencyContacts.Count);

            #endregion Step 15 & Step 16


        }

        [TestProperty("JiraIssueID", "ACC-3255")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact record with Next of kin to yes and end date as any date before the current date" +
           "Create another record with Next of kin to yes " + "Validate user should be able to create an Emergency contact record with Next of kim to Yes option with null end date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod006()
        {
            Guid emergencyContact = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "12345", "111", "222", "333", 1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-2));

            var startdate = DateTime.Now.AddDays(-1).Date;

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Dr.")
               .TapSearchButton()
               .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertStartDate(startdate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickNextOfKin_YesOption()
               .ClickSaveButton();


            System.Threading.Thread.Sleep(3000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(2, emergencyContacts.Count);

            #endregion Step 17 

        }

        [TestProperty("JiraIssueID", "ACC-3256")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact record with Next of kin to yes" +
          "Create another record with overlapping start dates " + "Validate the error message dispalyed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod007()
        {
            Guid emergencyContact = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "12345", "111", "222", "333", 1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-2));

            var startdate = DateTime.Now.AddMonths(-5).Date;

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                 .ClickTitleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Dr.")
              .TapSearchButton()
              .SelectResultElement(_titleId.ToString());

            systemUserEmergencyContactsRecordPage
               .WaitForSystemUserEmergencyContactsRecordPageToLoad()
               .InsertFirstName("Adam")
               .InsertLastName("Michel")
               .InsertContactTelephonePrimary("123456")
               .InsertContactTelephoneOther1("000")
               .InsertContactTelephoneOther2("111")
               .InsertContactTelephoneOther3("222")
               .InsertStartDate(startdate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickNextOfKin_YesOption()
               .ClickSaveButton();

            dynamicDialogPopup
               .WaitForDynamicDialogPopupToLoad()
               .ValidateMessage("There is already a record for Next of Kin with dates that overlaps, please correct as necessary.");


            #endregion Step 18

        }

        [TestProperty("JiraIssueID", "ACC-3257")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact records" +
         "Validate the record positions" + "Validate the start date sort")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod008()
        {
            Guid emergencyContact01 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "123450", "111", "222", "333", 1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-2));
            Guid emergencyContact02 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Sam", _titleId, "Mike", "12345890", "111", "222", "333", 1, DateTime.Now, null);


            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(2, emergencyContacts.Count);


            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .ValidateRecordPosition(emergencyContacts[1].ToString(), 1)
               .ValidateRecordPosition(emergencyContacts[0].ToString(), 2);

            #endregion Step 19

            #region Step 20

            System.Threading.Thread.Sleep(3000);

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .ValidateRecordCellText(emergencyContacts[1].ToString(), 6, DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
              .ValidateRecordCellText(emergencyContacts[0].ToString(), 6, DateTime.Now.AddMonths(-5).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
              .ClickStartDateSort()
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .ValidateRecordCellText(emergencyContacts[0].ToString(), 6, DateTime.Now.AddMonths(-5).Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
              .ValidateRecordCellText(emergencyContacts[1].ToString(), 6, DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));

            #endregion Step 20 




        }

        [TestProperty("JiraIssueID", "ACC-3258")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact records" +
        "Validate the quick search functionality for First name, Last name and Contact telephone primary ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod009()
        {
            Guid emergencyContact01 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "123450", "111", "222", "333", 1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-2));
            Guid emergencyContact02 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Sam", _titleId, "Mike", "12345890", "111", "222", "333", 1, DateTime.Now, null);

            #region Step 22

            loginPage
                .GoToLoginPage()
                .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .InsertQuickSearchText("Mike")
              .ClickQuickSearchButton()
              .ValidateRecordCellText(emergencyContact02.ToString(), 4, "Mike");

            System.Threading.Thread.Sleep(3000);

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .InsertQuickSearchText("Williams")
              .ClickQuickSearchButton();

            System.Threading.Thread.Sleep(3000);

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .ValidateRecordCellText(emergencyContact01.ToString(), 3, "Williams");

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .InsertQuickSearchText("12345890")
                .ClickQuickSearchButton();

            System.Threading.Thread.Sleep(3000);

            systemUserEmergencyContactsPage
                 .WaitForSystemUserEmergencyContactsPageToLoad()
                 .ValidateRecordCellText(emergencyContact02.ToString(), 5, "12345890");

            #endregion Step 22
        }

        [TestProperty("JiraIssueID", "ACC-3259")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact record with Next of kin to yes" +
         "Create another record with Next of kin to yes " + "Validate the error message dispalyed" + "Change the next of kin to No option and save the record" +
         "Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod010()
        {
            Guid emergencyContact01 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "123450", "111", "222", "333", 1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-2));
            Guid emergencyContact02 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Sam", _titleId, "Mike", "12345890", "111", "222", "333", 1, DateTime.Now, null);

            #region Step 21

            loginPage
               .GoToLoginPage()
               .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .ClickSelectAllCheckBox()
              .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad()
              .ValidateNoRecordMessageVisibile(true);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            var emergencyContacts = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_systemUserId01);
            Assert.AreEqual(0, emergencyContacts.Count);

            #endregion Step 21

        }

        [TestProperty("JiraIssueID", "ACC-3260")]
        [Description("Open the System User and Navigate to Related items and Emergency contact" + "System user should have Emergency contact records" +
        "Delete the created record and validate")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Emergency Contacts")]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod011()
        {
            Guid emergencyContact01 = dbHelper.systemUserEmergencyContacts.CreateSystemUser_EmergencyContacts(_careProviders_TeamId, _systemUserId01, _careProviders_BusinessUnitId, "Williams", _titleId, "George", "123450", "111", "222", "333", 1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-2));

            #region Step 23

            loginPage
               .GoToLoginPage()
               .Login(adminUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .OpenRecord(emergencyContact01.ToString());

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .InsertLastName("Updated QA")
                .ClickSaveAndCloseButton();

            systemUserEmergencyContactsPage
              .WaitForSystemUserEmergencyContactsPageToLoad();

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserId01UserName)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
               .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .OpenRecord(emergencyContact01.ToString());

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .NavigateToMenuSubPage_Aduit();

            auditListPage
             .WaitForAuditListPageToLoad("systemuseremergencycontacts");

            System.Threading.Thread.Sleep(3000);

            auditListPage
                .WaitForAuditListPageToLoad("systemuseremergencycontacts")
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("CW Admin Test User 2" + partialStringSuffix);

            #endregion Step 23

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



