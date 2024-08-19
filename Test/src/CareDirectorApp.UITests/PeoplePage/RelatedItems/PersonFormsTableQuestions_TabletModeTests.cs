using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Person Forms (containing table questions) to validate the app while it is displayed in tablet mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonFormsTableQuestions_TabletModeTests : TestBase
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



        #region Assessment window


        #region Open Assessment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7248")]
        [Description("UI Test for Assessment records - 0001 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record page is displayed")]
        public void PersonForms_EditAssessment_TestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "03/06/2020";

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7249")]
        [Description("UI Test for Assessment records - 0002 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - " +
            "Validate that the assessment is displayed but only the table question titles are visible")]
        public void PersonForms_EditAssessment_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "03/06/2020";

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")
                .ValidateTestHQTableTitleVisible(true)
                .ValidateTestHQ_Location_HeaderTitleVisible(false)
                .ValidateTestHQ_TestDec_HeaderTitleVisible(false)
                
                .ValidateTablePQ_TableTitleVisible(true)
                .ValidateTablePQ_Question1SubHeading_HeaderTitleVisible(false)
                .ValidateTablePQ_ContributionNotes_HeaderTitleVisible(false)
                .ValidateTablePQ_Role_HeaderTitleVisible(false)

                .ValidateTestQPC_TableTitleVisible(true)
                .ValidateTestQPC_Outcome_HeaderTitleVisible(false)
                .ValidateTestQPC_TypeOfInvolvement_HeaderTitleVisible(false)
                .ValidateTestQPC_WFTime_HeaderTitleVisible(false)
                .ValidateTestQPC_Who_HeaderTitleVisible(false)

                .ValidateWFTableWithUnlimitedRows_TableTitleVisible(true)
                .ValidateWFUnlimitedRowsTableSubHeading_HeaderTitleVisible(true)
                .ValidateWFUnlimitedRowsTable_DateBecameInvolved_HeaderTitleVisible(false)
                .ValidateWFUnlimitedRowsTable_ReasonForAssessment_HeaderTitleVisible(false);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7250")]
        [Description("UI Test for Assessment records - 0003 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Tap on each table title" +
            "Validate that the assessment is displayed and all table question titles are visible")]
        public void PersonForms_EditAssessment_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "03/06/2020";

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .TapTestQPC_TableTitle()
                .TapTablePQ_TableTitle()
                .TapTestHQ_TableTitle()


                .ValidateTestHQTableTitleVisible(true)
                .ValidateTestHQ_Location_HeaderTitleVisible(true)
                .ValidateTestHQ_TestDec_HeaderTitleVisible(true)

                .ValidateTablePQ_TableTitleVisible(true)
                .ValidateTablePQ_Question1SubHeading_HeaderTitleVisible(true)
                .ValidateTablePQ_ContributionNotes_HeaderTitleVisible(true)
                .ValidateTablePQ_Role_HeaderTitleVisible(true)

                .ValidateTestQPC_TableTitleVisible(true)
                .ValidateTestQPC_Outcome_HeaderTitleVisible(true)
                .ValidateTestQPC_TypeOfInvolvement_HeaderTitleVisible(true)
                .ValidateTestQPC_WFTime_HeaderTitleVisible(true)
                .ValidateTestQPC_Who_HeaderTitleVisible(true)

                .ValidateWFTableWithUnlimitedRows_TableTitleVisible(true)
                .ValidateWFUnlimitedRowsTableSubHeading_HeaderTitleVisible(true)
                .ValidateWFUnlimitedRowsTable_DateBecameInvolved_HeaderTitleVisible(true)
                .ValidateWFUnlimitedRowsTable_ReasonForAssessment_HeaderTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7251")]
        [Description("UI Test for Assessment records - 0004 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record answer fields are correctly displayed")]
        public void PersonForms_EditAssessment_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "03/06/2020";

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 03/06/2020 created by Mobile Test User 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("02/06/2020", "1")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 1", "1")
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("03/06/2020", "2")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 2", "2")


                .TapTestQPC_TableTitle()
                .ValidateTestQPC_Outcome_Row1_FieldText("O 1")
                .ValidateTestQPC_TypeOfinvolvement_Row1_FieldText("I 1")
                .ValidateTestQPC_WFTime_Row2_FieldText("09:50")
                .ValidateTestQPC_Who_Row2_FieldText("W 1")


                .TapTablePQ_TableTitle()
                .ValidateTablePQ_ContributionNotes_Row1_FieldText("value 1\nvalue 2")
                .ValidateTablePQ_Role_Row2_FieldText("R 1")
                .ValidateTablePQ_ContributionNotes_Row3_FieldText("value 3\nvalue 4")
                .ValidateTablePQ_Role_Row4_FieldText("R 2")

                .TapTestHQ_TableTitle()
                .ValidateTestHQ_Location_Row1_FieldText("L 1")
                .ValidateTestHQ_TestDec_Row1_FieldText("10.56")
                .ValidateTestHQ_Location_Row2_FieldText("L 2")
                .ValidateTestHQ_TestDec_Row2_FieldText("21.48");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7252")]
        [Description("UI Test for Assessment records - 0005 - " +
            "Navigate to the person Forms area - Open a person Form record (no answer is set in the assessment) - Tap on the edit assessment button - " +
            "Validate that the Assessment record answer fields are correctly displayed")]
        public void PersonForms_EditAssessment_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "04/06/2020";

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 04/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Mr Pavel MCNamara Starting 04/06/2020 created by Mobile Test User 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("", "1")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("", "1")

                .TapTestQPC_TableTitle()
                .ValidateTestQPC_Outcome_Row1_FieldText("")
                .ValidateTestQPC_TypeOfinvolvement_Row1_FieldText("")
                .ValidateTestQPC_WFTime_Row2_FieldText("")
                .ValidateTestQPC_Who_Row2_FieldText("")

                .TapTablePQ_TableTitle()
                .ValidateTablePQ_ContributionNotes_Row1_FieldText("")
                .ValidateTablePQ_Role_Row2_FieldText("")
                .ValidateTablePQ_ContributionNotes_Row3_FieldText("")
                .ValidateTablePQ_Role_Row4_FieldText("")

                .TapTestHQ_TableTitle()
                .ValidateTestHQ_Location_Row1_FieldText("")
                .ValidateTestHQ_TestDec_Row1_FieldText("")
                .ValidateTestHQ_Location_Row2_FieldText("")
                .ValidateTestHQ_TestDec_Row2_FieldText("");
        }

        #endregion


        #region Update Assessment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7253")]
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

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form (Table Questions)");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveButton()
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();



            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")

                .TapTestHQ_TableTitle()
                .InsertTestHQ_Location_Row1_Answer("L 1")
                .InsertTestHQ_TestDec_Row1_Answer("5.12")
                .InsertTestHQ_Location_Row2_Answer("L 2")
                .InsertTestHQ_TestDec_Row2_Answer("9.74")

                .TapTablePQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTablePQ_ContributionNotes_Row1_Answer("v 1\nv 2")
                .InsertTablePQ_Role_Row2_Answer("R 1")
                .InsertTablePQ_ContributionNotes_Row3_Answer("v 3\nv 4")
                .InsertTablePQ_Role_Row4_Answer("R 2")

                .TapTestQPC_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTestQPC_Outcome_Row1_Answer("O 1")
                .InsertTestQPC_TypeOfinvolvement_Row1_Answer("I 1")
                .InsertTestQPC_WFTime_Row2_Answer("08:50")
                .InsertTestQPC_Who_Row2_Answer("W 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertWFTableWithUnlimitedRows_DateBecomeInvolved_Answer("02/06/2020", "1")
                .TapWFTableWithUnlimitedRows_ReasonForAssessment_Picklist("1");

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapWFTableWithUnlimitedRows_AddNewRowButton()
                .InsertWFTableWithUnlimitedRows_DateBecomeInvolved_Answer("03/06/2020", "2")
                .TapWFTableWithUnlimitedRows_ReasonForAssessment_Picklist("2");

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();


            mobilePersonFormTableQuestionsEditAssessmentPage
               .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                
                .TapWFTableWithUnlimitedRows_TableTitle()
                .TapTestQPC_TableTitle()
                .TapTablePQ_TableTitle()
                .TapTestHQ_TableTitle()


                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTestHQ_Location_Row1_FieldText("L 1")
                .ValidateTestHQ_TestDec_Row1_FieldText("5.12")
                .ValidateTestHQ_Location_Row2_FieldText("L 2")
                .ValidateTestHQ_TestDec_Row2_FieldText("9.74")

                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTablePQ_ContributionNotes_Row1_FieldText("v 1\nv 2")
                .ValidateTablePQ_Role_Row2_FieldText("R 1")
                .ValidateTablePQ_ContributionNotes_Row3_FieldText("v 3\nv 4")
                .ValidateTablePQ_Role_Row4_FieldText("R 2")

                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTestQPC_Outcome_Row1_FieldText("O 1")
                .ValidateTestQPC_TypeOfinvolvement_Row1_FieldText("I 1")
                .ValidateTestQPC_WFTime_Row2_FieldText("08:50")
                .ValidateTestQPC_Who_Row2_FieldText("W 1")

                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("02/06/2020", "1")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 1", "1")
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("03/06/2020", "2")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 2", "2");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7254")]
        [Description("UI Test for Assessment records - 0009 - Open existing person form record - " +
            "Tap on the edit assessment button - set data in all answers - Save and re-open the assessment - Update all answers - Save and re-open the assessment a second time" +
            "Validate that all answers are correctly saved after the update")]
        public void PersonForms_EditAssessment_TestMethod09()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("8dc85dfa-c1a4-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Table Questions)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Table Questions)", assessmentstatusid, startdate, duedate, reviewdate);


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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            //SET THE INITIAL ANSWERS

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                
                .TapTestHQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTestHQ_Location_Row1_Answer("XL 1")
                .InsertTestHQ_TestDec_Row1_Answer("15.12")
                .InsertTestHQ_Location_Row2_Answer("XL 2")
                .InsertTestHQ_TestDec_Row2_Answer("19.74")

                .TapTablePQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTablePQ_ContributionNotes_Row1_Answer("Xv 1\nv 2")
                .InsertTablePQ_Role_Row2_Answer("XR 1")
                .InsertTablePQ_ContributionNotes_Row3_Answer("Xv 3\nv 4")
                .InsertTablePQ_Role_Row4_Answer("XR 2")

                .TapTestQPC_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTestQPC_Outcome_Row1_Answer("XO 1")
                .InsertTestQPC_TypeOfinvolvement_Row1_Answer("XI 1")
                .InsertTestQPC_WFTime_Row2_Answer("18:50")
                .InsertTestQPC_Who_Row2_Answer("XW 1")
                .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            //UPDATE ACTION WILL TAKE PLACE HERE

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")

                .TapTestHQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTestHQ_Location_Row1_Answer("L 1")
                .InsertTestHQ_TestDec_Row1_Answer("5.12")
                .InsertTestHQ_Location_Row2_Answer("L 2")
                .InsertTestHQ_TestDec_Row2_Answer("9.74")

                .TapTablePQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTablePQ_ContributionNotes_Row1_Answer("v 1\nv 2")
                .InsertTablePQ_Role_Row2_Answer("R 1")
                .InsertTablePQ_ContributionNotes_Row3_Answer("v 3\nv 4")
                .InsertTablePQ_Role_Row4_Answer("R 2")

                .TapTestQPC_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertTestQPC_Outcome_Row1_Answer("O 1")
                .InsertTestQPC_TypeOfinvolvement_Row1_Answer("I 1")
                .InsertTestQPC_WFTime_Row2_Answer("08:50")
                .InsertTestQPC_Who_Row2_Answer("W 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .InsertWFTableWithUnlimitedRows_DateBecomeInvolved_Answer("02/06/2020", "1")
                .TapWFTableWithUnlimitedRows_ReasonForAssessment_Picklist("1");

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapWFTableWithUnlimitedRows_AddNewRowButton()
                .ScrollFormToVerticalEnd()
                .InsertWFTableWithUnlimitedRows_DateBecomeInvolved_Answer("03/06/2020", "2")
                .TapWFTableWithUnlimitedRows_ReasonForAssessment_Picklist("2");

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();


            mobilePersonFormTableQuestionsEditAssessmentPage
               .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOnSaveAndCloseButton();


            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            // VALIDATE THE UPDATE ACTIONS

            mobilePersonFormTableQuestionsEditAssessmentPage
                .WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Table Questions) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                
                .TapTestHQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTestHQ_Location_Row1_FieldText("L 1")
                .ValidateTestHQ_TestDec_Row1_FieldText("5.12")
                .ValidateTestHQ_Location_Row2_FieldText("L 2")
                .ValidateTestHQ_TestDec_Row2_FieldText("9.74")
                
                .TapTablePQ_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTablePQ_ContributionNotes_Row1_FieldText("v 1\nv 2")
                .ValidateTablePQ_Role_Row2_FieldText("R 1")
                .ValidateTablePQ_ContributionNotes_Row3_FieldText("v 3\nv 4")
                .ValidateTablePQ_Role_Row4_FieldText("R 2")

                .TapTestQPC_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTestQPC_Outcome_Row1_FieldText("O 1")
                .ValidateTestQPC_TypeOfinvolvement_Row1_FieldText("I 1")
                .ValidateTestQPC_WFTime_Row2_FieldText("08:50")
                .ValidateTestQPC_Who_Row2_FieldText("W 1")

                .TapWFTableWithUnlimitedRows_TableTitle()
                .TapOpenHierarchyButton()
                .TapSection2HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("02/06/2020", "1")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 1", "1")
                .ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText("03/06/2020", "2")
                .ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText("Reason 2", "2");

        }


        #endregion


        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
