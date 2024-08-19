using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Person Forms validations while the app is displaying in tablet mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonForms_TabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");

            //start the APP
            uIHelper = new UIHelper();
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .TapChangeUserButton();

                warningPopup
                    .WaitForWarningPopupToLoad()
                    .TapOnYesButton();

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName("Mobile_Test_User_1")
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

                //if the offline mode warning is displayed, then close it
                warningPopup.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .InsertUserName("Mobile_Test_User_1")
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                pinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
        }

        [SetUp]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            //if the cases Form injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases Form review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToPeoplePage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region person Forms page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7213")]
        [Description("UI Test for Person Form records - 0001 - " +
            "Navigate to the person Forms area (do not contains Form records) - Validate the page content")]
        public void PersonForms_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7214")]
        [Description("UI Test for Person Form records - 0002 - " +
            "Navigate to the person Forms area (person contains Form records) - Validate the page content")]
        public void PersonForms_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid personFormID = new Guid("dc59e270-d3a4-ea11-a2cd-005056926fe4");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .ValidateFormTypeCellText("Mobile - Person Form", personFormID.ToString())
                .ValidateResponsibleUserCellText("Mobile Test User 1", personFormID.ToString())
                .ValidateStartDateCellText("01/06/2020", personFormID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", personFormID.ToString())
                .ValidateCreatedOnCellText("02/06/2020 14:17", personFormID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", personFormID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7215")]
        [Description("UI Test for Person Form records - 0003 - " +
            "Navigate to the person Forms area - Open a person Form record - Validate that the Form record page is displayed")]
        public void PersonForms_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7216")]
        [Description("UI Test for Person Form records - 0004 - " +
            "Navigate to the person Forms area - Open a person Form record - Validate that the Form record page field titles are displayed")]
        public void PersonForms_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateDueDateFieldTitleVisible(true)
                .ValidateReviewDateFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)

                .ValidateCompletedByFieldTitleVisible(false)
                .ValidateCompletionDateFieldTitleVisible(false)
                .ValidateSignedOffByFieldTitleVisible(true)
                .ValidateSignedOffDateFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7217")]
        [Description("UI Test for Person Form records - 0005 - " +
            "Navigate to the person Forms area - Open a person Form record - Validate that the Form record page fields are correctly displayed")]
        public void PersonForms_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("Mobile Test User 1")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateDueDateFieldText("30/06/2020")
                .ValidateReviewDateFieldText("23/06/2020")
                .ValidatePersonFieldText("Pavel MCNamara")

                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7218")]
        [Description("UI Test for Person Form records - 0006 - " +
            "Navigate to the person Forms area - Open a person Form record (with only the mandatory information set) - Validate that the Form record page fields are correctly displayed")]
        public void PersonForms_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "02/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 02/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("02/06/2020")
                .ValidateDueDateFieldText("")
                .ValidateReviewDateFieldText("")
                .ValidatePersonFieldText("Pavel MCNamara")

                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");
        }

        #endregion

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7219")]
        [Description("UI Test for Person Form records - 0007 - " +
            "Navigate to the person Forms area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void PersonForms_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .ValidateFormTypeFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateDueDateFieldTitleVisible(true)
                .ValidateReviewDateFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)

                .ValidateCompletedByFieldTitleVisible(false)
                .ValidateCompletionDateFieldTitleVisible(false)
                .ValidateSignedOffByFieldTitleVisible(true)
                .ValidateSignedOffDateFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7220")]
        [Description("UI Test for Person Form records - 0008 - " +
            "Navigate to the person Forms area - Tap on the add button - Validate that the new record page is displayed but the delete button is not displayed")]
        public void PersonForms_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7221")]
        [Description("UI Test for Person Form records - 0009 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonForms_TestMethod09()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .InsertDueDate("02/06/2020")
                .InsertReviewDate("03/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveButton()
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.AreEqual(duedate, (DateTime)fields["duedate"]);
            Assert.AreEqual(reviewdate, (DateTime)fields["reviewdate"]);
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7222")]
        [Description("UI Test for Person Form records - 0010 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonForms_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .InsertDueDate("02/06/2020")
                .InsertReviewDate("03/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.AreEqual(duedate, (DateTime)fields["duedate"]);
            Assert.AreEqual(reviewdate, (DateTime)fields["reviewdate"]);
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7223")]
        [Description("UI Test for Person Form records - 0011 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - " +
            "Re-Open the record - Validate that all fields are correctly set after saving the record")]
        public void PersonForms_TestMethod11()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .InsertDueDate("02/06/2020")
                .InsertReviewDate("03/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("Mobile Test User 1")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateDueDateFieldText("02/06/2020")
                .ValidateReviewDateFieldText("03/06/2020")
                .ValidatePersonFieldText("Maria Tsatsouline")

                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7224")]
        [Description("UI Test for Person Form records - 0012 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data only in mandatory fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonForms_TestMethod12()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("duedate"));
            Assert.IsFalse(fields.ContainsKey("reviewdate"));
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7225")]
        [Description("UI Test for Person Form records - 0013 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data only in mandatory fields except for Form Type - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonForms_TestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapOnSaveAndCloseButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Form Type' is required")
                .TapOnOKButton();

            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(0, forms.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7226")]
        [Description("UI Test for Person Form records - 0014 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data only in mandatory fields except for Start Date - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonForms_TestMethod14()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Start Date' is required")
                .TapOnOKButton();

            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(0, forms.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7227")]
        [Description("UI Test for Person Form records - 0022 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data only in mandatory fields - Remove the selected Responsible Team " +
            "Tap on the Save button - Validate that after the record is saved the responsible team is set automatically")]
        public void PersonForms_TestMethod22()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapResponsibleTeamRemoveButton()
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("duedate"));
            Assert.IsFalse(fields.ContainsKey("reviewdate"));
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7228")]
        [Description("UI Test for Person Form records - 0015 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - validate that all fields are correctly synced")]
        public void PersonForms_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("Mobile Test User 1")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateDueDateFieldText("02/06/2020")
                .ValidateReviewDateFieldText("03/06/2020")
                .ValidatePersonFieldText("Maria Tsatsouline")

                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7229")]
        [Description("UI Test for Person Form records - 0016 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonForms_TestMethod16()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDueDate("")
                .InsertReviewDate("")
                .TapOnSaveAndCloseButton();

            personFormsPage
              .WaitForPersonFormsPageToLoad();


            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(personFormID, "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("duedate"));
            Assert.IsFalse(fields.ContainsKey("reviewdate"));
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7230")]
        [Description("UI Test for Person Form records - 0017 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonForms_TestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertStartDate("04/06/2020")
                .InsertDueDate("05/06/2020")
                .InsertReviewDate("06/06/2020")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(personFormID, "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            startdate = new DateTime(2020, 6, 4);
            duedate = new DateTime(2020, 6, 5);
            reviewdate = new DateTime(2020, 6, 6);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.AreEqual(duedate, (DateTime)fields["duedate"]);
            Assert.AreEqual(reviewdate, (DateTime)fields["reviewdate"]);
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7231")]
        [Description("UI Test for Person Form records - 0020 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonForms_TestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            var records = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        #region Assessment window


        #region Open Assessment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7232")]
        [Description("UI Test for Assessment records - 0001 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record page is displayed")]
        public void PersonForms_EditAssessment_TestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7233")]
        [Description("UI Test for Assessment records - 0002 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record page field titles are displayed")]
        public void PersonForms_EditAssessment_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldTitleVisible(true)
                .ValidateDecimalFieldTitleVisible(true)
                .ValidateMultipleChoiceFieldTitleVisible(true)
                .ValidateMultipleResponseFieldTitlesVisible(true)

                .ValidateNumericFieldTitleVisible(true)
                .ValidateParagraphExplanationFieldTitleVisible(true)
                .ValidateFrequencyFieldTitleVisible(true)
                .ValidateShortAnswerFieldTitleVisible(true)

                .ValidateTitleFieldVisible(true)
                .ValidateAuthorisationFieldTitleVisible(true)
                .ValidateTimeQuestionFieldTitleVisible(true)
                .ValidateYesNoFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7234")]
        [Description("UI Test for Assessment records - 0003 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record answer fields are correctly displayed")]
        public void PersonForms_EditAssessment_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("01/06/2020")
                .ValidateDecimalFieldText("19.25")
                .ValidateMultipleChoiceFieldText("MC3")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("54")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Daily")
                .ValidateShortAnswerFieldText("V 1")
                .ValidateSignatureFieldVisible(true)

                .ValidateTimeQuestionFieldText("08:30")
                .ValidateYesNoFieldText("Yes");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7235")]
        [Description("UI Test for Assessment records - 0004 - " +
            "Navigate to the person Forms area - Open a person Form record (no answer is set in the assessment) - Tap on the edit assessment button - Validate that the Assessment record answer fields are correctly displayed")]
        public void PersonForms_EditAssessment_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "02/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 02/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 02/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("")
                .ValidateDecimalFieldText("")
                .ValidateMultipleChoiceFieldText("")
                .ValidateMultipleResponseOption1FieldText("No")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("No")
                .ValidateNumericFieldText("")
                .ValidateParagraphExplanationFieldText("")
                .ValidateFrequencyFieldText("")
                .ValidateShortAnswerFieldText("")
                .ValidateSignatureFieldVisible(true)
                .ValidateTimeQuestionFieldText("")
                .ValidateYesNoFieldText("No");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7236")]
        [Description("UI Test for Assessment records - 0005 - " +
            "Navigate to the person Forms area - Open a person Form record (no answer is set in the assessment) - Tap on the edit assessment button - " +
            "Tap on the hierarchy button - Validate that the hierarchy menu is displayed - Tap on the hierarchy button a second time - Validate that the hierarchy menu is closed ")]
        public void PersonForms_EditAssessment_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .ValidateHierarchyAreaVisibility(true)
                .TapOpenHierarchyButton()
                .ValidateHierarchyAreaVisibility(false);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7237")]
        [Description("UI Test for Assessment records - 0006 - " +
            "Navigate to the person Forms area - Open a person Form record (no answer is set in the assessment) - Tap on the edit assessment button - " +
            "Tap on the collapse all button - validate that all sections are collapsed - Tap on the expand button - validate that all sections are expanded")]
        public void PersonForms_EditAssessment_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapCollapseAllButton()
                .ValidateDateOfBirthFieldTitleVisible(false)
                .ValidateTimeQuestionFieldTitleVisible(false)
                .ValidateYesNoFieldTitleVisible(false)
                .TapExpandAllButton()
                .ValidateDateOfBirthFieldTitleVisible(true)
                .ValidateTimeQuestionFieldTitleVisible(true)
                .ValidateYesNoFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7238")]
        [Description("UI Test for Assessment records - 0007 - " +
            "Navigate to the person Forms area - Open a person Form record (no answer is set in the assessment) - Tap on the edit assessment button - " +
            "Tap on the hierarchy button - Tap on Section 1.1 on the hierarchy window - " +
            "Validate that the APP automatically scrolls to section 1.1")]
        public void PersonForms_EditAssessment_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton() //close hierarchy area
                .ValidateTimeQuestionFieldTitleVisibleWithoutScrolling(true)
                .ValidateTimeQuestionFieldTextWithoutScrolling("08:30");
        }

        #endregion


        #region Update Assessment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7239")]
        [Description("UI Test for Assessment records - 0008 - Create and save a new person form record - " +
            "After saving tap on the edit assessment button - set data in all answers - Save and re-open the assessment " +
            "Validate that all answers are correctly saved")]
        public void PersonForms_EditAssessment_TestMethod08()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .InsertDueDate("02/06/2020")
                .InsertReviewDate("03/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveButton()
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();



            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOpenHierarchyButton()
               .TapSection2HierarchyWindow()//there is an issue with scrolling to the answers. we have to scroll to the end of the document and then set the answers from the end to the beginning
               .TapOpenHierarchyButton()
               .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .InsertTimeQuestionAnswer("15:25")
               .InsertShortAnswer("V 1")
               .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value 1\nvalue 2")
                .InsertNumericAnswer("96")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("10.52")
                .InsertDateOfBirthAnswer("10/06/2020")
                .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("10/06/2020")
                .ValidateDecimalFieldText("10.52")
                .ValidateMultipleChoiceFieldText("MC3")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("96")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Hourly")
                .ValidateShortAnswerFieldText("V 1")
                .ValidateSignatureFieldVisible(true)
                .ValidateTimeQuestionFieldText("15:25")
                .ValidateYesNoFieldText("Yes");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7240")]
        [Description("UI Test for Assessment records - 0009 - Open existing person form record - " +
            "Tap on the edit assessment button - set data in all answers - Save and re-open the assessment - Update all answers - Save and re-open the assessment a second time" +
            "Validate that all answers are correctly saved after the update")]
        public void PersonForms_EditAssessment_TestMethod09()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            //SET THE INITIAL ANSWERS

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOpenHierarchyButton()
               .TapSection2HierarchyWindow()//there is an issue with scrolling to the answers. we have to scroll to the end of the document and then set the answers from the end to the beginning
               .TapOpenHierarchyButton()
               .InsertTimeQuestionAnswer("9:25")
               .InsertShortAnswer("A 1")
               .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value a\nvalue b")
                .InsertNumericAnswer("96")
                .TapMultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(0).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("0.52")
                .InsertDateOfBirthAnswer("01/01/2020");


            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            //UPDATE ACTION WILL TAKE PLACE HERE

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOpenHierarchyButton()
               .TapSection2HierarchyWindow()//there is an issue with scrolling to the answers. we have to scroll to the end of the document and then set the answers from the end to the beginning
               .TapOpenHierarchyButton()
               .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .InsertTimeQuestionAnswer("15:25")
               .InsertShortAnswer("V 1")
               .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value 1\nvalue 2")
                .InsertNumericAnswer("96")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("10.52")
                .InsertDateOfBirthAnswer("10/06/2020");


            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            // VALIDATE THE UPDATE ACTIONS

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("10/06/2020")
                .ValidateDecimalFieldText("10.52")
                .ValidateMultipleChoiceFieldText("MC3")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("Yes")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("96")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Hourly")
                .ValidateShortAnswerFieldText("V 1")
                .ValidateSignatureFieldVisible(true)
                .ValidateTimeQuestionFieldText("15:25")
                .ValidateYesNoFieldText("Yes")
               ;

        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7241")]
        [Description("UI Test for Assessment records - 0010 - Open existing person form record - " +
            "Tap on the edit assessment button - Set data in the Decimal question - Leave the Date of Birth question empty (this question is mandatory) - " +
            "tap on the save button - validate that the user is prevented from saving the record with an empty mandatory question")]
        public void PersonForms_EditAssessment_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline",
                mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("10.52")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The question 'Date of Birth' is mandatory and must be answered before saving").TapOnOKButton();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7242")]
        [Description("UI Test for Assessment records - 0011 - Open existing person form record - " +
            "Tap on the edit assessment button - Set data in the Date of Birth  question - " +
            "Tap on the Save button - Close and Re-Open the record - Remove the Date of Birth question - tap on the save button - validate that the user is prevented from saving the record")]
        public void PersonForms_EditAssessment_TestMethod11()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline",
                mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDateOfBirthAnswer("01/01/2000")
                .InsertDecimalAnswer("10.52")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .InsertDateOfBirthAnswer("")
               .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The question 'Date of Birth' is mandatory and must be answered before saving").TapOnOKButton();
        }


        #endregion


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-6136

        [Test]
        [Property("JiraIssueID", "CDV6-7243")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Open a person Form record (Source Case is set) - Validate that the Source Case Form field is displayed")]
        public void PersonForms_SourceCase_TestMethod001()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateSourceCaseFieldTitleVisible(true)
                .ValidateSourceCaseLookupEntryFieldText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7244")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Open a person Form record (Source Case is NOT set) - Validate that the Source Case Form field is displayed empty")]
        public void PersonForms_SourceCase_TestMethod002()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara 
            string personFormStartDate = "02/06/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 02/06/2020 created by Mobile Test User 1")
                .ValidateSourceCaseFieldTitleVisible(true)
                .ValidateSourceCaseLookupEntryFieldText("")
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7245")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - Tap on the Source Case lookup button - " +
            "Select a Case record linked to the person - Save the record - Validate that the source case information is correctly saved")]
        public void PersonForms_SourceCase_TestMethod003()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            var caseid = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4");

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapSourceCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("CAS-000004").TapSearchButtonQuery().TapOnRecord("CAS-000004-6176");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "caseid");
            Assert.AreEqual(caseid, (Guid)fields["caseid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7246")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - do not select a Source Case - " +
            "Save the record - Validate that the source case field in the DB has no data")]
        public void PersonForms_SourceCase_TestMethod004()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "caseid");
            Assert.AreEqual(false, fields.ContainsKey("caseid"));
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7247")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - Tap on the Source Case lookup button - " +
            "Search for a Case Record that do not belong to the person - Validate that no information is displayed")]
        public void PersonForms_SourceCase_TestMethod005()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            var caseid = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4");

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapSourceCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .InsertSearchQuery("CAS-000004-616")
                .TapSearchButtonQuery()
                .ValidateElementNotPresent("CAS-000004-6160")
                .ValidateElementNotPresent("CAS-000004-6161")
                .ValidateElementNotPresent("CAS-000004-6162")
                .ValidateElementNotPresent("CAS-000004-6163")
                .ValidateElementNotPresent("CAS-000004-6164")
                .ValidateElementNotPresent("CAS-000004-6165")
                .ValidateElementNotPresent("CAS-000004-6166")
                .ValidateElementNotPresent("CAS-000004-6167")
                .ValidateElementNotPresent("CAS-000004-6168")
                .ValidateElementNotPresent("CAS-000004-6169");

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
