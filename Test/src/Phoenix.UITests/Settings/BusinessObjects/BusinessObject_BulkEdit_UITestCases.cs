using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.BusinessObjects
{
    /// <summary>
    /// WARNING: Before running the tests it is necessary to setup some data manually
    /// 1 - Activate the bulk edit for the Person business object and the following business object fields only: allowemail, dateofbirth, maritalstatusid, preferredcontacttimeid, secondaryemail, telephone1, title
    /// 2 - Deactivate Bulk edit for the Case business object 
    /// 3 - Activate Bulk edit for the Person Case Note business object BUT deactivate bulk edit for all business object fields
    /// 
    ///
    /// </summary>
    [TestClass]
    public class BusinessObject_BulkEdit_UITestCases : FunctionalTest
    {

        #region Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _auditReasonId;
        private Guid _systemUserId;
        private string _systemUsername;

        #endregion


        [TestInitialize()]
        public void TestSetup()
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("BulkEdit BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("BulkEdit T1", null, _businessUnitId, "907678", "BulkEdit@careworkstempmail.com", "BulkEdit T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Error Management Reason

                _auditReasonId = commonMethodsDB.CreateErrorManagementReason("Care coordinator change", new DateTime(2020, 1, 1), 3, _teamId, false);

                #endregion

                #region System User BulkEditUser3

                _systemUsername = "BulkEditUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "BulkEdit", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }


        #region Business Objects

        [TestProperty("JiraIssueID", "CDV6-24417")]
        [Description("Business Objects - Bulk Edit UI Test 001 - Navigate to the Business Objects page - Open a Business Object record - Validate that the 'Is Bulk Edit Enabled?' option is Visible ")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod001()
        {
            Guid personBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("Person")
                .ClickQuickSearchButton()
                .OpenRecord(personBusinessObjectID.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .ValidateIsBulkEditEnabledFieldVisible();

        }

        [TestProperty("JiraIssueID", "CDV6-24418")]
        [Description("Business Objects - Bulk Edit UI Test 002 - Navigate to the Business Objects page - Open a Business Object record - Navigate to the Business Object fields section - Open a field record - Validate that the 'Is Bulk Edit Enabled?' option is Visible ")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod002()
        {

            Guid personBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("person")[0];
            Guid addressline1BusinessObjectFieldID = dbHelper.businessObjectField.GetBusinessObjectFieldByName("addressline1", personBusinessObjectID)[0];

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("Person")
                .ClickQuickSearchButton()
                .OpenRecord(personBusinessObjectID.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .NavigateToBusinessObjectFieldsSubArea();

            businessObjectFieldsSubPage
                .WaitForBusinessObjectFieldsSubPageToLoad()
                .InsertQuickSearchText("addressline1")
                .ClickQuickSearchButton()
                .OpenRecord(addressline1BusinessObjectFieldID.ToString());

            businessObjectFieldRecordPage
                .WaitForBusinessObjectFieldRecordPageToLoad()
                .ValidateIsBulkEditEnabledFieldVisible()
                ;


        }

        #endregion

        #region Person Record (bulk edit enabled)

        [TestProperty("JiraIssueID", "CDV6-24419")]
        [Description("Business Objects - Bulk Edit UI Test 003 - Navigate to the People area (person business object has the bulk edit option enabled) - Validate that the bulk edit button is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod003()
        {
            Guid personBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapTopBannerMenuButton()
                .WaitForBulkEditButtonVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-24420")]
        [Description("Business Objects - Bulk Edit UI Test 004 - Navigate to the People area (person business object has the bulk edit option enabled) - tap on the bulk edit button (without selecting any record) - validate that an error message is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod004()
        {
            Guid personBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You must select at least 2 records.")
                .TapCloseButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24421")]
        [Description("Business Objects - Bulk Edit UI Test 005 - Navigate to the People area (person business object has the bulk edit option enabled) - Select one person record - Tap on the bulk edit button - validate that an error message is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod005()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .SelectPersonRecord(personRecord1ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You must select at least 2 records.")
                .TapCloseButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24422")]
        [Description("Business Objects - Bulk Edit UI Test 006 - Navigate to the People area (person business object has the bulk edit option enabled) - Select two person records - Tap on the bulk edit button - validate that the bulk edit pop-up is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod006()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            _firstName = "Jhon";
            var personRecord2ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .SelectPersonRecord(personRecord1ID.ToString())
                .SelectPersonRecord(personRecord2ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2");
        }

        [TestProperty("JiraIssueID", "CDV6-24423")]
        [Description("Business Objects - Bulk Edit UI Test 007 - Navigate to the People area (person business object has the bulk edit option enabled) - Select two person records - Tap on the bulk edit button - validate all displayed field titles")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod007()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            _firstName = "Jhon";
            var personRecord2ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .SelectPersonRecord(personRecord1ID.ToString())
                .SelectPersonRecord(personRecord2ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ValidateFieldTitle("allowemail", "Allow Email")
                .ValidateFieldTitle("allowmail", "Allow Mail")
                .ValidateFieldTitle("allowphone", "Allow Phone")
                .ValidateFieldTitle("excludefromdbs", "Exclude from Demographic Batch Service")
                .ValidateFieldTitle("preferredcontacttimeid", "Preferred Time");
        }

        [TestProperty("JiraIssueID", "CDV6-24424")]
        [Description("Business Objects - Bulk Edit UI Test 008 - Navigate to the People area (person business object has the bulk edit option enabled) - Select two person records - Tap on the bulk edit button - Update a boolean field and save the update - Validate that the update was saved in the database")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod008()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            _firstName = "Jhon";
            var personRecord2ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .SelectPersonRecord(personRecord1ID.ToString())
                .SelectPersonRecord(personRecord2ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateCheckBox("allowemail")
                .ClickYesRadioButtonField("allowemail")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Care coordinator change").TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SelectPersonRecord(personRecord1ID.ToString()) //this step is just to make sure the update took place and we are in the people page again
                .SelectPersonRecord(personRecord2ID.ToString()); //this step is just to make sure the update took place and we are in the people page again


            var fields = dbHelper.person.GetPersonById(personRecord1ID, "allowemail");
            Assert.AreEqual(true, ((bool)fields["allowemail"]));

            fields = dbHelper.person.GetPersonById(personRecord2ID, "allowemail", "dateofbirth", "maritalstatusid", "preferredcontacttimeid", "secondaryemail", "telephone1", "title");
            Assert.AreEqual(true, ((bool)fields["allowemail"]));
        }

        [TestProperty("JiraIssueID", "CDV6-24425")]
        [Description("Business Objects - Bulk Edit UI Test 011 - Navigate to the People area (person business object has the bulk edit option enabled) - Select two person records - Tap on the bulk edit button - Update a Pick-list field and save the update - Validate that the update was saved in the database")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod011()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            _firstName = "Jhon";
            var personRecord2ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .SelectPersonRecord(personRecord1ID.ToString())
                .SelectPersonRecord(personRecord2ID.ToString())
                .TapTopBannerMenuButton()
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateCheckBox("preferredcontacttimeid")
                .SelectValueInPicklistField("preferredcontacttimeid", "Morning")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Care coordinator change").TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2")
                .ClickUpdateButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SelectPersonRecord(personRecord1ID.ToString()) //this step is just to make sure the update took place and we are in the people page again
                .SelectPersonRecord(personRecord2ID.ToString()); //this step is just to make sure the update took place and we are in the people page again


            var fields = dbHelper.person.GetPersonById(personRecord1ID, "preferredcontacttimeid");
            Assert.AreEqual(1, ((int)fields["preferredcontacttimeid"]));

            fields = dbHelper.person.GetPersonById(personRecord2ID, "preferredcontacttimeid");
            Assert.AreEqual(1, ((int)fields["preferredcontacttimeid"]));
        }

        #endregion

        #region Person Case Notes Record (bulk edit disabled)


        [TestProperty("JiraIssueID", "CDV6-24426")]
        [Description("Business Objects - Bulk Edit UI Test 020 - Open a person record and navigate to the Person Case Notes  area (Person Case Notes business object has the bulk edit option DISABLED) - Select two Person Case Note records - Validate that the bulk edit button is not displayed in the top toolbar area")]
        [TestCategory("UITest")]
        [TestMethod]
        public void BusinessObject_BulkEdit_UITestMethod021()
        {
            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDateTime;
            var personRecord1ID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);

            #endregion

            #region Person Case Note

            var personTask1ID = dbHelper.task.CreatePersonTask(personRecord1ID, _firstName + " " + _lastName, "T 1", "note 1", _teamId);
            var personTask2ID = dbHelper.task.CreatePersonTask(personRecord1ID, _firstName + " " + _lastName, "T 2", "note 2", _teamId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("BulkEditUser3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_lastName)
                .OpenPersonRecord(personRecord1ID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .SelectPersonTaskRecord(personTask1ID.ToString())
                .SelectPersonTaskRecord(personTask2ID.ToString())
                .ValidateBulkEditButtonVisibility(false);
        }


        #endregion



    }
}
