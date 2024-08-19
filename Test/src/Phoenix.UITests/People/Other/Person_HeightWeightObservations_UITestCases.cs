using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_HeightWeightObservations_UITestCases : FunctionalTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-25003")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person Height & Weight Observations case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("UITest")]
        public void HeightWeightObservationsCaseNote_Cloning_UITestMethod01()
        {
            var personID = new Guid("f4f051f0-de2f-4721-974b-0da92f5fedbc"); //Selma Ellis
            var personNumber = "109858";
            var caseID = new Guid("6de3f3dd-3540-e911-a2c5-005056926fe4");//CAS-3-297734
            var controlCaseID = new Guid("af2f7da3-e93a-e911-a2c5-005056926fe4"); //CAS-3-212576
            var HeightWeightObservationID = new Guid("5bd10ea3-17df-eb11-a325-005056926fe4"); //05/07/2021 
            var HeightWeightObservationCaseNoteID = new Guid("ea3094bb-17df-eb11-a325-005056926fe4"); //Person Height And Weight Case Note 001 

            //remove all cloned case notes for the case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(caseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);

            //remove all cloned case notes for the control case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(controlCaseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToHeightWeightObservationsPage();

            personHeightWeightObservationsPage
                .WaitForPersonHeightWeightObservationsPageToLoad()
                .OpenPersonHeightWeightObservationRecord(HeightWeightObservationID.ToString());

            personHeightWeighObservationRecordPage
                .WaitForPersonHeightWeighObservationRecordPageToLoad()
                .NavigateToPersonHeightWeighObservationCaseNotesArea();

            personHeightAndWeightCaseNotesPage
                .WaitForPersonHeightAndWeightCaseNotesPageToLoad()
                .OpenPersonHeightAndWeightCaseNoteRecord(HeightWeightObservationCaseNoteID.ToString());

            personHeightAndWeightCaseNoteRecordPage
                .WaitForPersonHeightAndWeightCaseNoteRecordPageToLoad()
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var activityreasonid = new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"); //Assessment
            var responsibleuserid = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta    
            var activitypriorityid = new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"); //Normal
            var activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            var casenotedate = new DateTime(2021, 7, 5, 8, 25, 0, DateTimeKind.Utc);
            var activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            var statusid = 1; //Open
            var activityoutcomeid = new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"); //Completed
            var significanteventcategoryid = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4"); //Category 1
            var significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);
            var significanteventsubcategoryid = new Guid("641f471b-1b52-e911-a2c5-005056926fe4"); //Sub Cat 1_2


            Assert.AreEqual("Person Height And Weight Case Note 001", fields["subject"]);
            Assert.AreEqual("<p>Person Height And Weight Case Note Description</p>", fields["notes"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(activityreasonid, fields["activityreasonid"]);
            Assert.AreEqual(responsibleuserid, fields["responsibleuserid"]);
            Assert.AreEqual(activitypriorityid, fields["activitypriorityid"]);
            Assert.AreEqual(activitycategoryid, fields["activitycategoryid"]);
            Assert.AreEqual(casenotedate.ToLocalTime(), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(activitysubcategoryid, fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(activityoutcomeid, fields["activityoutcomeid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(significanteventcategoryid, fields["significanteventcategoryid"]);
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(significanteventsubcategoryid, fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(HeightWeightObservationCaseNoteID, fields["clonedfromid"]);

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
