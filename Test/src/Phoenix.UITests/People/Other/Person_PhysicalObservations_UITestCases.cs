using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_PhysicalObservations_UITestCases : FunctionalTest
    {

        #region Print to PDF for Observation charts https://advancedcsg.atlassian.net/browse/CDV6-3733

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (NEWS), From and To Dates - Tap on the Generate button - " +
            "Validate that a docx file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24515")]
        public void Person_PhysicalObservations_UITestMethod01()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = PDF" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (NEWS), From and To Dates - Tap on the Generate button - " +
            "Validate that no docx or pdf file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24516")]
        public void Person_PhysicalObservations_UITestMethod02()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.pdf");
            Assert.IsTrue(fileExists); //because of this option in chrome "plugins.always_open_pdf_externally" the pdf files are downloaded by default
            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsFalse(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Neurological), From and To Dates - Tap on the Generate button - " +
            "Validate that a docx file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24517")]
        public void Person_PhysicalObservations_UITestMethod03()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Neurological")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "*.docx", 10);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = PDF" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Neurological), From and To Dates - Tap on the Generate button - " +
            "Validate that no docx or pdf file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24518")]
        public void Person_PhysicalObservations_UITestMethod04()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Neurological")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            System.Threading.Thread.Sleep(2000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.pdf");
            Assert.IsTrue(fileExists); //we had to change this assert as we force the browser to download all files including PDFs
            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsFalse(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Visual Assessment), From and To Dates - Tap on the Generate button - " +
            "Validate that a docx file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24519")]
        public void Person_PhysicalObservations_UITestMethod05()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";

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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Visual Assessment")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            bool fileExists = fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "*.docx", 15);
            Assert.IsTrue(fileExists);
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = PDF" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Visual Assessment), From and To Dates - Tap on the Generate button - " +
            "Validate that no docx or pdf file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24520")]
        public void Person_PhysicalObservations_UITestMethod06()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Visual Assessment")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();


            bool fileExists = fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "*.pdf", 10);
            Assert.IsTrue(fileExists); //the browser settings is downloading pdf files as well
            fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsFalse(fileExists);
        }





        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (NEWS), From and To Dates - Tap on the Generate button - " +
            "Validate that the downloaded docx file contains info regarding all available significant events")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24521")]
        public void Person_PhysicalObservations_UITestMethod07()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            System.Threading.Thread.Sleep(3000);

            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, "Respiratory Rate", "SpO2%", "Temperature", "Blood Pressure", "Heart Rate", "Blood Glucose", "Capillary Refill Time", "TOTAL MEWS SCORE", "", "", "");
            msWordHelper.ValidateWordsPresent(filePath, "02/07", "10:00", "40.00", "90.00", "39.00", "4.00");

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Neurological), From and To Dates - Tap on the Generate button - " +
            "Validate that the downloaded docx file contains info regarding all available significant events")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24522")]
        public void Person_PhysicalObservations_UITestMethod08()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Neurological")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();


            fileIOHelper.WaitForFileToExist(this.DownloadsDirectory, "*.docx", 10);
            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, "COMA SCALE", "VITAL SIGNS", "PUPILS", "LIMBS MOVEMENT", "Eyes open", "Best verbal/grimace", "Best motor (Best arm)");
            //msWordHelper.ValidateWordsPresent(filePath, "02/07", "07:00", "9.00", "37.50", "1.00", "2.00", "03/07", "10:00", "35.00", "7.00", "8.00");
        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3733 - " +
            "ObservationCharts.PrintFormat = Word" +
            "Open Person Record -> Navigate to The Physical Observations sub-section - Tap on the Generate Physical Observation Chart button - Select Chart Type (Visual Assessment), From and To Dates - Tap on the Generate button - " +
            "Validate that the downloaded docx file contains info regarding all available significant events with (records should not be ordered)")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24523")]
        public void Person_PhysicalObservations_UITestMethod09()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("ObservationCharts.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            Guid personID = new Guid("dd212110-70ca-ea11-a2cd-005056926fe4"); //Ms Mariana Braletia
            string personNumber = "504704";


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
                .NavigateToPhysicalObservationsPage();

            personPhysicalObservationsPage
                .WaitForPersonPhysicalObservationsPageToLoad()
                .TapGeneratePhysicalObservationChartButton();

            personPhysicalObservationsGenerateChartPopup
                .WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
                .SelectChartTypeByText("Visual Assessment")
                .InsertFromDate("01/07/2020")
                .InsertToDate("05/07/2020")
                .TapGenerateButton();

            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, "Talking in sentences, not just moans and groans", "Breathing is noisy", "Breathing is between 11 to 25 per minute", "Breathing is below 10 per minute or above 25 per minute", "08:00", "14:30");
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
