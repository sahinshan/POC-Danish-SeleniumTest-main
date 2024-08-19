using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People.Health
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Person_RecordsOfDNAR_UITestCases : FunctionalTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-11951

        [TestProperty("JiraIssueID", "CDV6-12611")]
        [Description("Verify that user is not able to create, authorise the Record of DNAR only if 'Can Authorise DNAR?' = 'No' ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases01()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var userid = dbHelper.systemUser.GetSystemUserByUserName("SecurityTestUserAdmin2")[0];
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("SecurityTestUserAdmin2", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .ValidateResponsibleClinicianDetailsVisibility(false)

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateDNAROrderCompletedByErrorLabelText("Please fill out this field.")
                .ValidateDNAROrderCompletedByLookupButtonDisabled(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-12612")]
        [Description("Try to save record without entering mandatory fields ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases02()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta


            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .ValidateResponsibleClinicianDetailsVisibility(true)

                .ClickSaveAndCloseButton()

                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateAdultCPRDecisionErrorLabelText("Please fill out this field.")
                .ValidateDiscussedDecisionErrorLabelText("Please fill out this field.")
                .ValidateReviewDNARErrorLabelText("Please fill out this field.")

                .ValidateDNAROrderCompletedByLinkFieldText("CW Forms Test User 1")
                .ValidateCompletedByRegistrationNumberFieldText("REG_99090_A")
                .ValidateDateTimeCompletedFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePositionFieldText("Dr")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickDNAROrderCompletedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("José Brazeta").TapSearchButton().SelectResultElement(systemUserID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            personRecordsOfDNARPage
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ValidatePersonLinkFieldText("Janice Horne")
                .ValidateDateTimeOfDNARFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateAgeStatusSelectedText("Adult")

                .ValidateAdultCPRDecisionSelectedText("Yes")

                .ValidateInterestHarmCPRBenefitYesOptionChecked(false)
                .ValidateInterestHarmCPRBenefitNoOptionChecked(true)
                .ValidateNaturalDeathYesOptionChecked(false)
                .ValidateNaturalDeathNoOptionChecked(true)
                .ValidateDiscussedDecisionSelectedText("Yes")
                .ValidateDiscussedWithNextOfKinSelectedText("")
                .ValidateNextOfKinsInformationFieldText("")
                .ValidateCPRRefusedYesOptionChecked(false)
                .ValidateCPRRefusedNoOptionChecked(true)
                .ValidateOtherYesOptionChecked(false)
                .ValidateOtherNoOptionChecked(true)
                .ValidateAdditionalInformationFieldText("")
                .ValidateNameOfNOKLinkFieldText("")

                .ValidateDNAROrderCompletedByLinkFieldText("José Brazeta")
                .ValidateCompletedByRegistrationNumberFieldText("REG_99091_B")
                .ValidateDateTimeCompletedFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePositionFieldText("Doc")

                .ValidateSeniorResponsibleClinicianWithOversightFieldText("")
                .ValidateClinicianRegistrationNumberFieldText("")
                .ValidateDateTimeOfOversightFieldText("", "")
                .ValidateClinicianPositionFieldText("")

                .ValidateReviewDNARSelectedText("No review")

                .ValidateCancelledDecisionYesOptionChecked(false)
                .ValidateCancelledDecisionNoOptionChecked(true)

                .ValidateReviewAdditionalCommentsFieldText("")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-12630")]
        [Description("Validate that newly created DNAR records are visible in the person Timeline")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases03()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta


            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(dnarRecords[0].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-12631")]
        [Description("'Record of DNAR' icon should not be displayed in the Person banner if there is no active confirmed DNAR record exists for a Person")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases04()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta


            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            personRecordsOfDNARPage
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ValidatePersonTopBannerDNARActiveIconVisibility(false)
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-12633")]
        [Description("Validate that a user is prevented from creating a new DNAR record when an active one already exists - " +
            "Open existing record and cancel it - Validate that the record gets disabled")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases05()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1


            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An active DNAR is still in place for this Person. If you wish to commence a new DNAR you must first undertake cancellation of decision on previous DNAR.")
                .TapCloseButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickCancelledDecisionYesRadioButton()
                .ClickCancelledByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Forms Test User 1").TapSearchButton().SelectResultElement(systemUserID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .SelectView("Inactive Records")
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForInactivePersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ");
        }

        [TestProperty("JiraIssueID", "CDV6-12636")]
        [Description("Validate that a user can create a new DNAR record when a canceled one already exists - " +
            "For the newly created DNAR record user can Confirm it - DNAR Icon should be displayed in the person top banner")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases06()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1
            var systemUserID2 = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickCancelledDecisionYesRadioButton()
                .ClickCancelledByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Forms Test User 1").TapSearchButton().SelectResultElement(systemUserID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForInactivePersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickBackButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("No")
                .SelectRefusingTreatment("Yes")
                .SelectDiscussedDecision("No")
                .InsertReasonDecisionNotDiscussed("Welfare Attorney to be discussed")
                .SelectReviewDNAR("Concerns are raised")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(2, dnarRecords.Count);

            dnarRecords = dbHelper.personDNAR.GetByPersonID(personID, false);
            Assert.AreEqual(1, dnarRecords.Count);

            personRecordsOfDNARPage
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickSeniorResponsibleClinicianWithOversightLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("José Brazeta").TapSearchButton().SelectResultElement(systemUserID2.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .InsertDateTimeOfOversight(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .OpenRecordsOfDNARRecord(dnarRecords[0].ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ValidateSeniorResponsibleClinicianWithOversightLinkFieldText("José Brazeta")
                .ValidateClinicianRegistrationNumberFieldText("REG_99091_B")
                .ValidateDateTimeOfOversightFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateClinicianPositionFieldText("Doc")

                .ValidatePersonTopBannerDNARActiveIconVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12648")]
        [Description("Print a completed DNAR record")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases07()
        {
            var systemSettingId = dbHelper.systemSetting.GetSystemSettingIdByName("MailMerge.PrintFormat")[0];
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingId, "Word");

            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1
            var systemUserID2 = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("No")
                .SelectRefusingTreatment("Yes")
                .SelectDiscussedDecision("No")
                .InsertReasonDecisionNotDiscussed("Welfare Attorney to be discussed")
                .SelectReviewDNAR("Concerns are raised")
                .ClickSeniorResponsibleClinicianWithOversightLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("José Brazeta").TapSearchButton().SelectResultElement(systemUserID2.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .InsertDateTimeOfOversight(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ValidateSeniorResponsibleClinicianWithOversightLinkFieldText("José Brazeta")
                .ValidateClinicianRegistrationNumberFieldText("REG_99091_B")
                .ValidateDateTimeOfOversightFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateClinicianPositionFieldText("Doc")
                .ClickPrintDNAR();

            System.Threading.Thread.Sleep(4000);

            var fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "DNAR Template V6.docx");
            Assert.IsTrue(fileExists);
        }

        [TestProperty("JiraIssueID", "CDV6-12655")]
        [Description("Related Views records should be displayed in Descending order by Date Created ON")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases08()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1
            var systemUserID2 = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickCancelledDecisionYesRadioButton()
                .ClickCancelledByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Forms Test User 1").TapSearchButton().SelectResultElement(systemUserID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForInactivePersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickBackButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("No")
                .SelectRefusingTreatment("Yes")
                .SelectDiscussedDecision("No")
                .InsertReasonDecisionNotDiscussed("Welfare Attorney to be discussed")
                .SelectReviewDNAR("Concerns are raised")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(2, dnarRecords.Count);

            var activeDNARRecords = dbHelper.personDNAR.GetByPersonID(personID, false);
            Assert.AreEqual(1, activeDNARRecords.Count);

            var inactiveDNARRecords = dbHelper.personDNAR.GetByPersonID(personID, true);
            Assert.AreEqual(1, inactiveDNARRecords.Count);

            personRecordsOfDNARPage
                .ValidateRecordPositionVisible(activeDNARRecords[0].ToString(), 1)
                .SelectView("Inactive Records")
                .ValidateRecordPositionVisible(inactiveDNARRecords[0].ToString(), 1);
        }

        [TestProperty("JiraIssueID", "CDV6-12656")]
        [Description("Export DNAR records to excel and csv files")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases09()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1
            var systemUserID2 = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickCancelledDecisionYesRadioButton()
                .ClickCancelledByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW Forms Test User 1").TapSearchButton().SelectResultElement(systemUserID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForInactivePersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ")
                .ClickBackButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")

                .SelectAdultCPRDecision("No")
                .SelectRefusingTreatment("Yes")
                .SelectDiscussedDecision("No")
                .InsertReasonDecisionNotDiscussed("Welfare Attorney to be discussed")
                .SelectReviewDNAR("Concerns are raised")
                .ClickSaveAndCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(2, dnarRecords.Count);

            var activeDNARRecords = dbHelper.personDNAR.GetByPersonID(personID, false);
            Assert.AreEqual(1, activeDNARRecords.Count);

            var inactiveDNARRecords = dbHelper.personDNAR.GetByPersonID(personID, true);
            Assert.AreEqual(1, inactiveDNARRecords.Count);

            personRecordsOfDNARPage
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Excel")
                .ClickExportButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "RecordsofDNAR.xlsx");
            Assert.IsTrue(fileExists);

            exportDataPopup
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(2000);

            fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "RecordsofDNAR.csv");
            Assert.IsTrue(fileExists);

            var fileLines = fileIOHelper.OpenFileAndReadAllLines(DownloadsDirectory, "RecordsofDNAR.csv");
            Assert.IsTrue(fileLines[1].Contains("Concerns are raised"));
            //Assert.IsTrue(fileLines[2].Contains("No review"));

            exportDataPopup
                .TapCloseButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .SelectView("Inactive Records")
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Current Page")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(2000);

            fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "RecordsofDNAR (1).csv");
            Assert.IsTrue(fileExists);
            fileLines = fileIOHelper.OpenFileAndReadAllLines(DownloadsDirectory, "RecordsofDNAR (1).csv");
            Assert.IsTrue(fileLines[1].Contains("No review"));

        }

        [TestProperty("JiraIssueID", "CDV6-12657")]
        [Description("Delete a DNAR record")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases10()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveButton()
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ");

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            personRecordsOfDNARRecordPage
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad();

            dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(0, dnarRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12659")]
        [Description("Advanced search of DNAR records")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases11()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForPersonRecordsOfDNARRecordPageToLoad("New")
                .SelectAdultCPRDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectReviewDNAR("No review")
                .ClickSaveButton()
                .WaitForPersonRecordsOfDNARRecordPageToLoad("Person DNAR for Janice Horne created by ");

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Records of DNAR")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(personNumber).TapSearchButton().SelectResultElement(personID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(dnarRecords[0].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-12660")]
        [Description("Create DNAR record for person younger than 16 years old")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases12()
        {
            var personID = new Guid("bc8fd2df-fd45-4da5-9d6c-a60dad97bc45"); //Clara Cardenas
            var personNumber = "380153";
            var motherPersonID = new Guid("088a2bf1-7bc7-4184-943a-c6b37da1df8f"); //Luisa Knapp

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .ClickNewRecordButton();

            personRecordsOfDNARRecordPage
                .WaitForChildRecordsOfDNARRecordPageToLoad("New")
                .SelectChildCPRDecision("Yes")
                .SelectParentsAgreedToDecision("Yes")
                .SelectDiscussedDecision("Yes")
                .SelectDiscussedWithParent("No")
                .ClickParentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").SelectResultElement(motherPersonID.ToString());

            personRecordsOfDNARRecordPage
                .WaitForChildRecordsOfDNARRecordPageToLoad("New")
                .SelectReviewDNAR("Review On")
                .InsertReviewDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertReviewAdditionalComments("Need to Review daily")
                .ClickSaveButton()
                .WaitForChildRecordsOfDNARRecordPageToLoad("Person DNAR for Clara Cardenas created by ");

            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            System.Threading.Thread.Sleep(1200);

            personRecordsOfDNARRecordPage
                .ValidatePersonLinkFieldText("Clara Cardenas")
                .ValidateDateTimeOfDNARFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateAgeStatusSelectedText("Child")

                .ValidateChildCPRDecisionSelectedText("Yes")
                .ValidateParentsAgreedToDecisionSelectedText("Yes")
                .ValidateChildInvolvedInDecisionMakingSelectedText("")

                .ValidateInterestHarmCPRBenefitYesOptionChecked(false)
                .ValidateInterestHarmCPRBenefitNoOptionChecked(true)
                .ValidateNaturalDeathYesOptionChecked(false)
                .ValidateNaturalDeathNoOptionChecked(true)
                .ValidateDiscussedDecisionSelectedText("Yes")
                .ValidateDiscussedWithNextOfKinSelectedText("")
                .ValidateNextOfKinsInformationFieldText("")
                .ValidateCPRRefusedYesOptionChecked(false)
                .ValidateCPRRefusedNoOptionChecked(true)
                .ValidateOtherYesOptionChecked(false)
                .ValidateOtherNoOptionChecked(true)
                .ValidateAdditionalInformationFieldText("")
                .ValidateDiscussedWithParent("No")
                .ValidateParentLinkFieldText("Luisa Knapp")

                .ValidateDNAROrderCompletedByLinkFieldText("CW Forms Test User 1")
                .ValidateCompletedByRegistrationNumberFieldText("REG_99090_A")
                .ValidateDateTimeCompletedFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePositionFieldText("Dr")

                .ValidateSeniorResponsibleClinicianWithOversightFieldText("")
                .ValidateClinicianRegistrationNumberFieldText("")
                .ValidateDateTimeOfOversightFieldText("", "")
                .ValidateClinicianPositionFieldText("")

                .ValidateReviewDNARSelectedText("Review On")
                .ValidateReviewDateValue(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))

                .ValidateCancelledDecisionYesOptionChecked(false)
                .ValidateCancelledDecisionNoOptionChecked(true)

                .ValidateReviewAdditionalCommentsFieldText("Need to Review daily")
                ;
        }


        [TestProperty("JiraIssueID", "CDV6-13672")]
        [Description("Verify the View DNAR record in Web")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases13()
        {

            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";

            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .validateViewDropDown("Active Records")
                .validateViewDropDown("Inactive Records");



        }

        [TestProperty("JiraIssueID", "CDV6-13674")]
        [Description("Verify the View Active Records in DNAR in Web")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases14()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");//José Brazeta
            var systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").FirstOrDefault();

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }
            var personDNARid = dbHelper.personDNAR.CreatePersonDNARRecord(ownerid, "test", false, personID, "test", new DateTime(2000, 1, 2), systemUserId, new DateTime(2000, 1, 2));

            loginPage
                 .GoToLoginPage()
                 .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .SelectView("Active Records");



            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            System.Threading.Thread.Sleep(1200);


        }

        [TestProperty("JiraIssueID", "CDV6-13675")]
        [Description("Verify the View InActive Records in DNAR in Web")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_RecordsOfDNAR_UITestCases15()
        {
            var personID = new Guid("34cb8793-d3e9-4ffa-9269-70f8b2dbe066"); //Janice Horne
            var personNumber = "380151";
            var ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");//José Brazeta
            var systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").FirstOrDefault();

            //remove the person DNAR records linked to the person
            foreach (var recordID in dbHelper.personDNAR.GetByPersonID(personID))
            {
                dbHelper.personDNAR.DeletePersonDNAR(recordID);
            }
            var personDNARid = dbHelper.personDNAR.CreateInactivePersonDNARRecord(ownerid, "test", true, personID, "test", new DateTime(2000, 1, 2), new DateTime(2000, 1, 2), true, systemUserId, systemUserId, new DateTime(2000, 1, 2)); new DateTime(2000, 1, 2);
            dbHelper.personDNAR.UpdateCancelDecision(personDNARid, true);

            loginPage
             .GoToLoginPage()
             .Login("CW_Forms_Test_User_1", "Passw0rd_!")
             .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecordsOfDNARPage();

            personRecordsOfDNARPage
                .WaitForPersonRecordsOfDNARPageToLoad()
                .SelectView("Inactive Records");



            var dnarRecords = dbHelper.personDNAR.GetByPersonID(personID);
            Assert.AreEqual(1, dnarRecords.Count);

            System.Threading.Thread.Sleep(1200);


        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}
