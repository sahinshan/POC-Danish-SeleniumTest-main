using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_PrimarySupportReasons_UITestCases : FunctionalTest
    {
        public object CW_Forms_Test_User_1 { get; private set; }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12000")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason without mandatory fields" +
                     "-validating Error popup is displayed ")]
        [TestMethod]
        public void Person_PrimarySupportReasons_UITestMethod01()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickSaveButton()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidatePrimarySupportFieldErrorMessage("Please fill out this field.")
                .ValidatestartDateFieldErrorMessage("Please fill out this field.");      

                   }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12001")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with Futhur date in both start and end start " +
                     "-validating Error popup is displayed")]
        [TestMethod]
       
        public void Person_PrimarySupportReasons_UITestMethod02()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate("12/08/2030")
                .ClickSaveButton();

            alertPopup
              .WaitForAlertPopupToLoad()
              .ValidateAlertText("Start Date cannot be in the future")
              .TapOKButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate("13/08/2030")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be in the future")
                .TapOKButton();
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12005")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with previous date in  start daye and end start is lesser than the start date " +
                     "- validating error message is displayed ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod03()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString()); 

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate("11/08/2021")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Start Date cannot be later than End Date")
                .TapOKButton();


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12009")]
        [Description("Testing persons primary support reasons - Saving Primary support reason with all valid details -validating person primary support reason is created")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod04()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString()); 

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()                
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordMessageVisibile(false); 
            
        }
        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12012")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with all valid details " +
                     "-validating person primary support reason is created is visible in person Time line page")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod05()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//for date formate

            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());              


            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();


            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(primarySupportReasonRecords.FirstOrDefault().ToString());
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12019")]
        [Description("Testing persons primary support reasons" +
                     " - Saving Primary support reason with past date in  start date and end date null  " +
                     "- validating primary support reason is created ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod06()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate("11/08/2021")
                .ClickSaveAndCloseButton();


            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12023")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with same date for both start date and end date   " +
                     "- validating primary support reason record is created ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod07()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();


            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12026")]
        [Description("Testing persons primary support reasons - " +
            "         Saving Primary support reason with same date for both start date and end date  " +
                      " - validating primary support reason record is created is visible in audit page")]
        [TestMethod]       
                        
        public void Person_PrimarySupportReasons_UITestMethod08()
        {
            
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);         
           


            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();


            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .OpenPersonPrimarySupportReasonRecord(primarySupportReasonRecords[0].ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickMenuButton()
                .ClickAuditButton();

            auditListPage
                .WaitForAuditListPageToLoad("personprimarysupportreason");

            var updateAudits = dbHelper.audit.GetAuditByRecordID(primarySupportReasonRecords[0] , 1); //get all update operations
            Assert.AreEqual(1, updateAudits.Count);            

            auditListPage
                .ValidateRecordPresent(updateAudits.First().ToString())
                .ClickOnAuditRecord(updateAudits.First().ToString());

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12027")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with past date in  start date and end date null  " +
                     "- validating primary support reason is created ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod09()
        {
            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();
            
            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickActivePrimarySupportReasonRelatedViewOption()                
                //.WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false);

            personPrimarySupportReasonPage                
                .ClickInactivePrimarySupportReasonsRelatedViewOtion()
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(true);


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12157")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with current datefor both start date and end date " +
                     "- validating primary support reason created is Export to excel ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod15()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var advancedId = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4");//advanced
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime date = Convert.ToDateTime(currentdate);

            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            //creating new primarysupportReason
            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(personID, advancedId, primarySupportReasonType, date, date);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();
               
            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .SelectPersonPrimarySupportReasonRecord(primarySupportReasonRecords[0].ToString())
                .ClickExportToExcel();

            exportDataPopup
               .WaitForExportDataPopupToLoad()
               .SelectRecordsToExport("Selected Records")
               .SelectExportFormat("Csv (comma separated with quotes)")
               .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "PersonPrimarySupportReasons.csv");
            Assert.IsTrue(fileExists);

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12028")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with current datefor both start date and end date " +
                     "- validating primary support reason is created can be assigned to other team ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod10()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            
            var careDirectorQAID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");//CareDirector QA
            var advancedId = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4");//advanced

            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date
            DateTime date = Convert.ToDateTime(currentdate);        

            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            //creating new primarysupportReason
           Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(personID, advancedId, primarySupportReasonType,date,date);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickAssignButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPrimarySupportToLoad()
                .ResponsibleTeamIdTapSearchButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(careDirectorQAID.ToString());

            assignRecordPopup
                .TapOkButton();

            System.Threading.Thread.Sleep(3000);

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()                
                .ValidateRecordCellText(Record.ToString(), 5, "CareDirector QA");

           

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12034")]
        [Description("Testing persons primary support reasons" +
                     " - Saving Primary support reason with current date for both start date and end date" +
                     " - validating primary support reason created is deleted ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod14()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care

            var careDirectorQAID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");//CareDirector QA
            var advancedId = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4");//advanced

            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date
            DateTime date = Convert.ToDateTime(currentdate);

            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            //creating new primarysupportReason
            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(personID, advancedId, primarySupportReasonType, date, date);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickDeletedButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                    .ValidateNoRecordLabelVisibile(true);



        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12031")]
        [Description("Testing persons primary support reasons " +
                     "- Creating Primary support reason with Past date for both start date and end date " +
                     "- validating primary support reason created is modified with current date ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod11()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care            
            var advancedId = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4");//advanced

            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date
           // DateTime date = Convert.ToDateTime(currentdate);
            DateTime date = new DateTime(2021, 8, 14);
            

            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            //creating new primarysupportReason
            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(personID, advancedId, primarySupportReasonType, date, date);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickRecord();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordModificationPageToLoad()                
                .InsertStartDate(currentdate)                
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateRecordCellText(Record.ToString(), 3, currentdate)
                .ValidateRecordCellText(Record.ToString(), 4, currentdate);    

            


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12032")]
        [Description("Testing persons primary support reasons - " +
                      "Creating Primary support reason with current date for both start date and end date " +
                       "- validating primary support reason created is Shared to other team ")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod12()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care           
            var advancedId = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4");//advanced

            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date
            DateTime date = Convert.ToDateTime(currentdate);

            Guid userID = new Guid("0795e01b-1f59-44cf-9769-a83da8bfb546");
            string userFullName = "Test User 1987";
           // Guid caseFormID = new Guid("82309bf9 - daff - eb11 - a327 - f90a4322a942");
            Guid shareToID = new Guid("b70011e6-eaff-eb11-a327-f90a4322a942");
           

            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }
            //creating new primarysupportReason
            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(personID, advancedId, primarySupportReasonType, date, date);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                 .WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickRecord();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordModificationPageToLoad()
                .ClickShareRecordButton();

            shareRecordPopup
                .WaitForShareRecordPopupToLoad()
                .SearchForUserRecord(userFullName);

            shareRecordResultsPopup
                .WaitForShareRecordResultsPopupToLoad()
                .TapAddUserButton(userID.ToString());

            shareRecordPopup
                .WaitForResultsPopupToClose();
               
               
             






        }
        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-11949
        [TestProperty("JiraIssueID", "CDV6-12033")]
        [Description("Testing persons primary support reasons -" +
                     " Creating Primary support reason  via advance search option" +
                      " - validating primary support reason is created")]
        [TestMethod]

        public void Person_PrimarySupportReasons_UITestMethod13()
        {

            Guid personID = new Guid("8b8a5abc-076a-406d-b5fc-bbbdd17aeed6"); //Vance Sullivan
            string personNumber = "324156";
            var primarySupportReasonType = new Guid("ff31de97-8a66-e911-a2c5-005056926fe4");//child in care
            var careDirectorQAID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");//CareDirector QA
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date                                                                                


            //delete existing records
            foreach (var recordId in dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(personID))
            {
                dbHelper.personPrimarySupportReason.DeletePersonPrimarySupportReason(recordId);
            }


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");                

            homePage
                .ClickAdvanceSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Primary Support Reasons")
                .WaitForAdvanceSearchPageToLoad()
                .SelectSavedView("Inactive Primary Support Reasons View")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();                     
                                      
            personPrimarySupportReasonRecordPage 
               .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()               
               .ClickPersonID_LookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("All Active People")
               .TypeSearchQuery("Vance Sullivan")
               .TapSearchButton()
               .SelectResultElement(personID.ToString());

             personPrimarySupportReasonRecordPage
               .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
               .ClickOwnerIDLookupButton();

           lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup View")
               .TypeSearchQuery("caredirector QA")
               .TapSearchButton()
               .SelectResultElement(careDirectorQAID.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .ClickPrimarySupportReasonLookupButton();                
           
            lookupPopup
                .WaitForLookupPopupToLoad()                
                .TypeSearchQuery("child in care")
                .TapSearchButton()
                .SelectResultElement(primarySupportReasonType.ToString());           
                
            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                 .WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false);
                



        }
        #endregion

        









    }
}
