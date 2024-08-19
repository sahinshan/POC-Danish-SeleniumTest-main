using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("Files\\Portal -  Case Identification.Zip"), DeploymentItem("chromedriver.exe"), DeploymentItem("Files\\Person Address.Zip"), DeploymentItem("Files\\Automation - MASH Episode Form 1.Zip")]
    public class PersonRecord_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid;
        private Guid _productLanguageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private string _systemUsername;
        private Guid _systemUserId;

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
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

                _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("People BU4");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("People T4", null, _businessUnitId, "907678", "PeopleT4@careworkstempmail.com", "People T4", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User

                _systemUsername = "PeopleUser4";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "People", "User4", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-1987

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-1987 - " +
            "Open Person Record -> Click Edit -> Change Home Phone number -> Save and Close - " +
            "Validate that the Person Grid is automatically refreshed with the new phone number")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24541")]
        public void Person_GridRefresh_UITestMethod01()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Dorian", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            dbHelper.person.UpdatePersonHomePhone(personID, "012345");

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
            .TapTopBannerToogleButton()
            .ValidateTopBannerHomePhone("Home:\r\n012345") //validate that the old value is present
            .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Dorian " + currentDateTimeString)
                .InsertHomePhone("987654321")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTopBannerToogleButton()
                .ValidateTopBannerHomePhone("Home:\r\n987654321"); //validate that the new value is present

        }

        #endregion

        #region Story : https://advancedcsg.atlassian.net/browse/CDV6-3730

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "Navigate to the People screen - Select 2 person records - Tap on the Mail Merge button - Validate that the mail Merge popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24524")]
        public void Person_PrintToPDFForLetters_UITestMethod01()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad();
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "MailMerge.PrintFormat = PDF - Navigate to the People screen - Select 2 person records - " +
            "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
            "Set Create Activity = No - Tap on the OK button - " +
            "Validate that a new popup is displayed containing the generated pdf file")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24525")]
        public void Person_PrintToPDFForLetters_UITestMethod02()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion


            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "PDF", "...", false, null);

            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            var fileExists = fileIOHelper.WaitForFileToExist(DownloadsDirectory, "PersonAddress.pdf", 10);
            Assert.IsTrue(fileExists);
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "MailMerge.PrintFormat = PDF - Navigate to the People screen - Select 2 person records - " +
            "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
            "Set Create Activity = No - Tap on the OK button - " +
            "Validate that a file is downloaded into the browser downloads folder if the browser settings are set to download pdf files")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24526")]
        public void Person_PrintToPDFForLetters_UITestMethod03()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "PDF", "...", false, null);

            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            var fileExists = fileIOHelper.WaitForFileToExist(DownloadsDirectory, "*.pdf", 10);
            Assert.IsTrue(fileExists);
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "MailMerge.PrintFormat = Word - Navigate to the People screen - Select 2 person records - " +
            "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
            "Set Create Activity = No - Tap on the OK button - " +
            "Validate that the word file is automatically downloaded into the browsers download directory")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24527")]
        public void Person_PrintToPDFForLetters_UITestMethod04()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);

            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            var fileExists = fileIOHelper.WaitForFileToExist(DownloadsDirectory, "PersonAddress.docx", 10);
            Assert.IsTrue(fileExists);
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "MailMerge.PrintFormat = Word - Navigate to the People screen - Select 2 person records - " +
            "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
            "Set Create Activity = No - Tap on the OK button - " +
            "Validate that the word document only contains the address info for the selected files")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24528")]
        public void Person_PrintToPDFForLetters_UITestMethod05()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            #region System Setting

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);

            #endregion

            #region Marital Status

            var MaritalStatus = commonMethodsDB.CreateMaritalStatus("Married", new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region Language


            var LanguageId = commonMethodsDB.CreateLanguage("English", _teamId, "", "", new DateTime(2000, 1, 1), null);

            #endregion

            #region Address Property Type

            var AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];

            #endregion

            #region Person

            var AddressTypeId_Primary = 7;
            var AccommodationStatusId = 1;
            var LivesAloneTypeId = 1;
            var GenderId = 1;
            var dob = new DateTime(2000, 1, 2);
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", dob, _ethnicityId, MaritalStatus, LanguageId, AddressPropertyType, _teamId, "PROPERTY NAME", "PROPERTY NO", "CNT1", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", dob, _ethnicityId, MaritalStatus, LanguageId, AddressPropertyType, _teamId, "PROPERTY NAME", "PROPERTY NO", "CNT2", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            #endregion



            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            fileIOHelper.WaitForFileToExist(DownloadsDirectory, "PersonAddress.docx", 10);

            object fileName = Path.Combine(DownloadsDirectory + "\\PersonAddress.docx");
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
            Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: true, Visible: false);
            aDoc.Activate();

            object findText = "CNT2";
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            //execute find
            bool result1Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            findText = "CNT1";
            bool result2Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            findText = "Portugal";
            bool result3Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            aDoc.Close();

            Assert.IsTrue(result1Found);
            Assert.IsTrue(result2Found);
            Assert.IsFalse(result3Found);
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
            "MailMerge.PrintFormat = Word - Navigate to the People screen - Select 'People I Created' view - Select 2 records in the list of results- " +
            "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
            "Set Create Activity = No - Set Merge to All records from selected view - Tap on the OK button - " +
            "Validate that the word document contains the address info for all records in the selected view (not only the ones selected by the user)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24529")]
        public void Person_PrintToPDFForLetters_UITestMethod06()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);

            #region Marital Status

            var MaritalStatus = commonMethodsDB.CreateMaritalStatus("Married", new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region Language

            var LanguageId = commonMethodsDB.CreateLanguage("English", _teamId, "", "", new DateTime(2000, 1, 1), null);

            #endregion

            #region Address Property Type

            var AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];

            #endregion

            #region Person

            var AddressTypeId_Primary = 7;
            var AccommodationStatusId = 1;
            var LivesAloneTypeId = 1;
            var GenderId = 1;
            var dob = new DateTime(2000, 1, 2);
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", dob, _ethnicityId, MaritalStatus, LanguageId, AddressPropertyType, _teamId, "PROPERTY NAME", "PROPERTY NO", "CNT1", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", dob, _ethnicityId, MaritalStatus, LanguageId, AddressPropertyType, _teamId, "PROPERTY NAME", "PROPERTY NO", "CNT2", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var personID3 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", dob, _ethnicityId, MaritalStatus, LanguageId, AddressPropertyType, _teamId, "PROPERTY NAME", "PROPERTY NO", "Portugal", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SelectAvailableViewByText("My Team Records")
                .WaitForPeoplePageToLoad()
                .ClickOnTableHeaderIdLink() //sort ascendingly
                .WaitForPeoplePageToLoad()
                .ClickOnTableHeaderIdLink() //sort descendingly
                .WaitForPeoplePageToLoad()
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapAllRecordsFromSelectedViewRadioButton()
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            object fileName = Path.Combine(DownloadsDirectory + "\\PersonAddress.docx");
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
            Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: true, Visible: false);
            aDoc.Activate();

            object findText = "CNT2";
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            //execute find
            bool result1Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            findText = "CNT1";
            bool result2Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            findText = "Portugal";
            bool result3Found = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

            aDoc.Close();

            Assert.IsTrue(result1Found);
            Assert.IsTrue(result2Found);
            Assert.IsTrue(result3Found);


        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
           "MailMerge.PrintFormat = PDF - Navigate to the People screen - Select 'People I Created' view - Select 2 records in the list of results- " +
           "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
           "Set Merge to Selected records on current page - Set Create Activity = Yes - Set Create As Completed Activity - Insert an Activity Subject - Tap on the OK button - " +
           "Validate that a Letter records is created for both person records with the specified subject, responsible team and the link to the pdf file")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24530")]
        public void Person_PrintToPDFForLetters_UITestMethod07()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "PDF", "...", false, null);

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityYesRadioButton()
                .TapCompletedActivityRadioButton()
                .InsertActivitySubject("Letter 1")
                .ClickOKButton();
            //.ValidatePDFPopupIsOpen();

            System.Threading.Thread.Sleep(3000);


            var records = dbHelper.letter.GetLetterByPersonID(personID1);
            Assert.AreEqual(1, records.Count);
            var fields = dbHelper.letter.GetLetterByID(records[0], "ownerid", "subject", "regardingid", "regardingidtablename", "regardingidname", "fileid", "senderid", "senderidtablename", "senderidname", "statusid");
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Letter 1", (string)fields["subject"]);
            Assert.AreEqual(personID1.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
            Assert.AreEqual("Mariana " + currentDateTimeString, (string)fields["regardingidname"]);
            Assert.IsNotNull(fields["fileid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["senderid"].ToString());
            Assert.AreEqual("systemuser", (string)fields["senderidtablename"]);
            Assert.AreEqual("People User4", (string)fields["senderidname"]);
            Assert.AreEqual(2, (int)fields["statusid"]); //Completed

            records = dbHelper.letter.GetLetterByPersonID(personID2);
            Assert.AreEqual(1, records.Count);
            fields = dbHelper.letter.GetLetterByID(records[0], "ownerid", "subject", "regardingid", "regardingidtablename", "regardingidname", "fileid", "senderid", "senderidtablename", "senderidname", "statusid");
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Letter 1", (string)fields["subject"]);
            Assert.AreEqual(personID2.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
            Assert.AreEqual("Juliana " + currentDateTimeString, (string)fields["regardingidname"]);
            Assert.IsNotNull(fields["fileid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["senderid"].ToString());
            Assert.AreEqual("systemuser", (string)fields["senderidtablename"]);
            Assert.AreEqual("People User4", (string)fields["senderidname"]);
            Assert.AreEqual(2, (int)fields["statusid"]); //Completed
        }

        [Description("Test for story https://advancedcsg.atlassian.net/browse/CDV6-3730 - " +
           "MailMerge.PrintFormat = PDF - Navigate to the People screen - Select 'People I Created' view - Select 2 records in the list of results- " +
           "Tap on the Mail Merge button - Wait for the Mail Merge popup to be displayed - Set Mail Merge Template = Person Address - " +
           "Set Merge to Selected records on current page - Set Create Activity = Yes - Set Create As Open Activity - Insert an Activity Subject - Tap on the OK button - " +
           "Validate that a Letter records is created for both person records with the specified subject, responsible team and the link to the pdf file")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24531")]
        public void Person_PrintToPDFForLetters_UITestMethod08()
        {
            #region Mail Merge Template

            commonMethodsDB.ImportMailMergeTemplate("Person Address.Zip");

            #endregion

            commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "PDF", "...", false, null);

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID1 = dbHelper.person.CreatePersonRecord("", "Mariana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personID2 = dbHelper.person.CreatePersonRecord("", "Juliana", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentDateTimeString)
                .SelectPersonRecord(personID1.ToString())
                .SelectPersonRecord(personID2.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityYesRadioButton()
                .TapOpenActivityRadioButton()
                .InsertActivitySubject("Letter 1")
                .ClickOKButton();
            //.ValidatePDFPopupIsOpen();

            System.Threading.Thread.Sleep(3000);

            var records = dbHelper.letter.GetLetterByPersonID(personID1);
            Assert.AreEqual(1, records.Count);
            var fields = dbHelper.letter.GetLetterByID(records[0], "ownerid", "subject", "regardingid", "regardingidtablename", "regardingidname", "fileid", "senderid", "senderidtablename", "senderidname", "statusid");
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Letter 1", (string)fields["subject"]);
            Assert.AreEqual(personID1.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
            Assert.AreEqual("Mariana " + currentDateTimeString, (string)fields["regardingidname"]);
            Assert.IsNotNull(fields["fileid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["senderid"].ToString());
            Assert.AreEqual("systemuser", (string)fields["senderidtablename"]);
            Assert.AreEqual("People User4", (string)fields["senderidname"]);
            Assert.AreEqual(1, (int)fields["statusid"]); //In Progress


            records = dbHelper.letter.GetLetterByPersonID(personID2);
            Assert.AreEqual(1, records.Count);
            fields = dbHelper.letter.GetLetterByID(records[0], "ownerid", "subject", "regardingid", "regardingidtablename", "regardingidname", "fileid", "senderid", "senderidtablename", "senderidname", "statusid");
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Letter 1", (string)fields["subject"]);
            Assert.AreEqual(personID2.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
            Assert.AreEqual("Juliana " + currentDateTimeString, (string)fields["regardingidname"]);
            Assert.IsNotNull(fields["fileid"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["senderid"].ToString());
            Assert.AreEqual("systemuser", (string)fields["senderidtablename"]);
            Assert.AreEqual("People User4", (string)fields["senderidname"]);
            Assert.AreEqual(1, (int)fields["statusid"]); //In Progress
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8334

        [Description("Open Person Record (person has no Case records) - Click on the Edit button - Click on the Run Workflow button - " +
            "Select the 'Portal - Case Identification' workflow - Wait for the Async workflow to run - Validate that a new case record is created for the person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24532")]
        public void CustomWorkflowCaseIdentification_UITestMethod01()
        {
            #region Contact Source

            commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Contact Reason

            commonMethodsDB.CreateContactReason(new Guid("3784785b-9750-e911-a2c5-005056926fe4"), _teamId, "Advice/Consultation", new DateTime(2000, 1, 1), 110000000, 1, true);

            #endregion

            #region Workflow

            var workflowid = commonMethodsDB.CreateWorkflowIfNeeded("Portal -  Case Identification", "Portal -  Case Identification.Zip");

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Rena", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion


            //Remove any existing workflow job
            foreach (var jobid in this.dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(jobid);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString)
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString);


            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //At this point there should be 1 case linked to the person record
            var cases = dbHelper.Case.GetCasesByPersonID(personID);
            Assert.AreEqual(1, cases.Count);
        }

        [Description("Open Person Record (person has one closed Case record) - Click on the Edit button - Click on the Run Workflow button - " +
            "Select the 'Portal - Case Identification' workflow - Wait for the Async workflow to run - Validate that a new case record is created for the person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24533")]
        public void CustomWorkflowCaseIdentification_UITestMethod02()
        {
            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Workflow

            var workflowid = commonMethodsDB.CreateWorkflowIfNeeded("Portal -  Case Identification", "Portal -  Case Identification.Zip");

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Rena", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status

            var _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Social Care", new DateTime(2020, 1, 1), 110000000, 2, true);

            commonMethodsDB.CreateContactReason(new Guid("3784785b-9750-e911-a2c5-005056926fe4"), _teamId, "Advice/Consultation", new DateTime(2000, 1, 1), 110000000, 1, true);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case

            //create an inactive case
            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            //Remove any existing workflow job
            foreach (var jobid in this.dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(jobid);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString)
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString);


            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //At this point there should be 1 case linked to the person record
            var cases = dbHelper.Case.GetActiveCasesByPersonID(personID);
            Assert.AreEqual(1, cases.Count);
        }

        [Description("Open Person Record (person has one active Case record) - Click on the Edit button - Click on the Run Workflow button - " +
            "Select the 'Portal - Case Identification' workflow - Wait for the Async workflow to run - Validate that no new case records are created for the person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24534")]
        public void CustomWorkflowCaseIdentification_UITestMethod03()
        {
            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Workflow

            var workflowid = commonMethodsDB.CreateWorkflowIfNeeded("Portal -  Case Identification", "Portal -  Case Identification.Zip");

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Rena", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Social Care", new DateTime(2020, 1, 1), 110000000, 2, true);

            commonMethodsDB.CreateContactReason(new Guid("3784785b-9750-e911-a2c5-005056926fe4"), _teamId, "Advice/Consultation", new DateTime(2000, 1, 1), 110000000, 1, true);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            //Remove any existing workflow job
            foreach (var jobid in this.dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid))
                dbHelper.workflowJob.DeleteWorkflowJob(jobid);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString)
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(workflowid.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Rena " + currentDateTimeString);


            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowid);
            Assert.AreEqual(1, newWorkflowJobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobs[0]);


            //At this point there should be 1 case linked to the person record
            var cases = dbHelper.Case.GetActiveCasesByPersonID(personID);
            Assert.AreEqual(1, cases.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-6142

        [Description("Person record with linked Task, Case Note and Contact - Person record linked to a Case record - " +
            "Case record with linked Tasks, Status History, Involvements and LAC Episodes - " +
            "Case record is restricted with allow user type restriction - Person Task Case Note and Contact is restricted with allow user type restriction - " +
            "Login with a system user account that is included in the data restriction used above - Open the person record - Open the timeline tab - " +
            "Validate that all records are visible to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24535")]
        public void RestrictedRecordsInTimeline_UITestMethod01()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_teamId, "Contact Centre", currentDate.AddYears(-1), true);

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId, 110000000, true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_teamId, "Routine");

            #endregion

            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_teamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Contact

            var contactid = dbHelper.contact.CreateContact(_teamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId, _contactStatus, _systemUserId, personID, "person", "Ivan " + currentDateTimeString, currentDate, "...", 1, 1);

            #endregion

            #region Person Task

            var persontaskid = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);

            #endregion

            #region Person Case Note

            var personcasenoteid = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "CN 01", "notes ...", personID, currentDate);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Case

            var caserecordid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, caserecordid, "", personID, currentDate);

            #endregion

            #region LAC Review Type

            var lacReviewTypeId = commonMethodsDB.CreateLACReviewType(_teamId, "Default", currentDate);

            #endregion

            #region LAC Review

            var lacEpisodereviewID = dbHelper.lacReview.CreateLACReview(_teamId, lacEpisodeID, lacReviewTypeId, currentDate);

            #endregion

            #region Case Involvements

            var caseInvolvements = dbHelper.CaseInvolvement.GetByCaseID(caserecordid);
            Assert.AreEqual(2, caseInvolvements.Count);
            var involvementid1 = caseInvolvements[0];
            var involvementid2 = caseInvolvements[1];

            #endregion

            #region Task

            var casetaskid = dbHelper.task.CreateTask("Task 01", "notes ...", _teamId, null, null, null, null, null, null, caserecordid, personID, null, caserecordid, "", "case");

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24535", 1, _teamId);

            #endregion

            #region User Restricted Data Access

            commonMethodsDB.CreateUserRestrictedDataAccess(dataRestrictionId, _systemUserId, currentDate, null, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(personID, "person", dataRestrictionId);
            dbHelper.RestrictRecord(contactid, "contact", dataRestrictionId);
            dbHelper.RestrictRecord(persontaskid, "task", dataRestrictionId);
            dbHelper.RestrictRecord(personcasenoteid, "personcasenote", dataRestrictionId);

            dbHelper.RestrictRecord(caserecordid, "case", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodeID, "lacepisode", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodereviewID, "lacreview", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid1, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid2, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(casetaskid, "task", dataRestrictionId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(contactid.ToString())
                .ValidateRecordPresent(persontaskid.ToString())
                .ValidateRecordPresent(personcasenoteid.ToString())
                .ValidateRecordPresent(caserecordid.ToString())
                .ValidateRecordPresent(lacEpisodereviewID.ToString())
                .ValidateRecordPresent(involvementid1.ToString())
                .ValidateRecordPresent(involvementid2.ToString())
                .ValidateRecordPresent(casetaskid.ToString())
                .ValidateRecordPresent(contactid.ToString())
                ;
        }

        [Description("Person record with linked Task, Case Note and Contact - Person record linked to a Case record - " +
            "Case record with linked Tasks, Status History, Involvements and LAC Episodes - " +
            "Case record is restricted with allow user type restriction - Person Task Case Note and Contact is restricted with allow user type restriction - " +
            "Login with a system user account that is NOT included in the data restriction used above - Open the person record - Open the timeline tab - " +
            "Validate that none of the records are visible to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24536")]
        public void RestrictedRecordsInTimeline_UITestMethod02()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_teamId, "Contact Centre", currentDate.AddYears(-1), true);

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId, 110000000, true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_teamId, "Routine");

            #endregion

            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_teamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Contact

            var contactid = dbHelper.contact.CreateContact(_teamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId, _contactStatus, _systemUserId, personID, "person", "Ivan " + currentDateTimeString, currentDate, "...", 1, 1);

            #endregion

            #region Person Task

            var persontaskid = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);

            #endregion

            #region Person Case Note

            var personcasenoteid = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "CN 01", "notes ...", personID, currentDate);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Case

            var caserecordid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, caserecordid, "", personID, currentDate);

            #endregion

            #region LAC Review Type

            var lacReviewTypeId = commonMethodsDB.CreateLACReviewType(_teamId, "Default", currentDate);

            #endregion

            #region LAC Review

            var lacEpisodereviewID = dbHelper.lacReview.CreateLACReview(_teamId, lacEpisodeID, lacReviewTypeId, currentDate);

            #endregion

            #region Case Involvements

            var caseInvolvements = dbHelper.CaseInvolvement.GetByCaseID(caserecordid);
            Assert.AreEqual(2, caseInvolvements.Count);
            var involvementid1 = caseInvolvements[0];
            var involvementid2 = caseInvolvements[1];

            #endregion

            #region Task

            var casetaskid = dbHelper.task.CreateTask("Task 01", "notes ...", _teamId, null, null, null, null, null, null, caserecordid, personID, null, caserecordid, "", "case");

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24536", 1, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(contactid, "contact", dataRestrictionId);
            dbHelper.RestrictRecord(persontaskid, "task", dataRestrictionId);
            dbHelper.RestrictRecord(personcasenoteid, "personcasenote", dataRestrictionId);

            dbHelper.RestrictRecord(caserecordid, "case", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodeID, "lacepisode", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodereviewID, "lacreview", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid1, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid2, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(casetaskid, "task", dataRestrictionId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordNotPresent(contactid.ToString())
                .ValidateRecordNotPresent(persontaskid.ToString())
                .ValidateRecordNotPresent(personcasenoteid.ToString())
                .ValidateRecordNotPresent(caserecordid.ToString())
                .ValidateRecordNotPresent(lacEpisodereviewID.ToString())
                .ValidateRecordNotPresent(involvementid1.ToString())
                .ValidateRecordNotPresent(involvementid2.ToString())
                .ValidateRecordNotPresent(casetaskid.ToString())
                .ValidateRecordNotPresent(contactid.ToString())

                .ValidateRestrictedRecordCardVisibility("1", true)
                .ValidateRestrictedRecordCardVisibility("2", true)
                .ValidateRestrictedRecordCardVisibility("3", true)
                .ValidateRestrictedRecordCardVisibility("4", true)
                .ValidateRestrictedRecordCardVisibility("6", true)
                .ValidateRestrictedRecordCardVisibility("7", true)
                .ValidateRestrictedRecordCardVisibility("8", true)

                .ValidateRestrictedRecordCardVisibility("1", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("2", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("3", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("4", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("6", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("7", "Restricted Record: Please contact People T4 with any inquiries")
                .ValidateRestrictedRecordCardVisibility("8", "Restricted Record: Please contact People T4 with any inquiries")
                ;
        }

        [Description("Person record with 2 linked Task - Only person task 1 is restricted with allow user type restriction - " +
            "Login with a system user account that is included in the data restriction used above - Open the person record - Open the timeline tab - " +
            "Validate that both task records are visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24537")]
        public void RestrictedRecordsInTimeline_UITestMethod03()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Task

            var persontaskid1 = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);
            var persontaskid2 = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 02", "subject ...", _teamId);

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24537", 1, _teamId);

            #endregion

            #region User Restricted Data Access

            commonMethodsDB.CreateUserRestrictedDataAccess(dataRestrictionId, _systemUserId, currentDate, null, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(persontaskid1, "task", dataRestrictionId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(persontaskid1.ToString())
                .ValidateRecordPresent(persontaskid2.ToString());
        }

        [Description("Person record with 2 linked Task - Only person task 1 is restricted with allow user type restriction - " +
            "Login with a system user account that is NOT included in the data restriction used above - Open the person record - Open the timeline tab - " +
            "Validate that only 1 task record is visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24538")]
        public void RestrictedRecordsInTimeline_UITestMethod04()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Task

            var persontaskid1 = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);
            var persontaskid2 = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 02", "subject ...", _teamId);

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24538", 1, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(persontaskid1, "task", dataRestrictionId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(persontaskid2.ToString())

                .ValidateRecordNotPresent(persontaskid1.ToString())
                .ValidateRestrictedRecordCardVisibility("2", true)
                .ValidateRestrictedRecordCardVisibility("2", "Restricted Record: Please contact People T4 with any inquiries")
                ;
        }

        [Description("Person record with linked Task, Case Note and Contact - Person record linked to a Case record - " +
            "Case record with linked Tasks, Status History, Involvements and LAC Episodes - " +
            "Case record is restricted with allow user type restriction - Person Task Case Note and Contact is restricted with allow user type restriction - " +
            "Login with a system user account that is included in the data restriction used above - Open the person record - Open the timeline tab - Filter by LAC Review records - " +
            "Validate that all LAC Review records are visible to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24539")]
        public void RestrictedRecordsInTimeline_UITestMethod05()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_teamId, "Contact Centre", currentDate.AddYears(-1), true);

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId, 110000000, true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_teamId, "Routine");

            #endregion

            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_teamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Contact

            var contactid = dbHelper.contact.CreateContact(_teamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId, _contactStatus, _systemUserId, personID, "person", "Ivan " + currentDateTimeString, currentDate, "...", 1, 1);

            #endregion

            #region Person Task

            var persontaskid = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);

            #endregion

            #region Person Case Note

            var personcasenoteid = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "CN 01", "notes ...", personID, currentDate);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Case

            var caserecordid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, caserecordid, "", personID, currentDate);

            #endregion

            #region LAC Review Type

            var lacReviewTypeId = commonMethodsDB.CreateLACReviewType(_teamId, "Default", currentDate);

            #endregion

            #region LAC Review

            var lacEpisodereviewID = dbHelper.lacReview.CreateLACReview(_teamId, lacEpisodeID, lacReviewTypeId, currentDate);

            #endregion

            #region Case Involvements

            var caseInvolvements = dbHelper.CaseInvolvement.GetByCaseID(caserecordid);
            Assert.AreEqual(2, caseInvolvements.Count);
            var involvementid1 = caseInvolvements[0];
            var involvementid2 = caseInvolvements[1];

            #endregion

            #region Task

            var casetaskid = dbHelper.task.CreateTask("Task 01", "notes ...", _teamId, null, null, null, null, null, null, caserecordid, personID, null, caserecordid, "", "case");

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24539", 1, _teamId);

            #endregion

            #region User Restricted Data Access

            commonMethodsDB.CreateUserRestrictedDataAccess(dataRestrictionId, _systemUserId, currentDate, null, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(personID, "person", dataRestrictionId);
            dbHelper.RestrictRecord(contactid, "contact", dataRestrictionId);
            dbHelper.RestrictRecord(persontaskid, "task", dataRestrictionId);
            dbHelper.RestrictRecord(personcasenoteid, "personcasenote", dataRestrictionId);

            dbHelper.RestrictRecord(caserecordid, "case", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodeID, "lacepisode", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodereviewID, "lacreview", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid1, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid2, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(casetaskid, "task", dataRestrictionId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()

                .SelectFilterBy("LAC Review")
                .ClickApplyButton()

                .ValidateRecordPresent(lacEpisodereviewID.ToString())

                .ValidateRecordNotPresent(contactid.ToString())
                .ValidateRecordNotPresent(persontaskid.ToString())
                .ValidateRecordNotPresent(personcasenoteid.ToString())
                .ValidateRecordNotPresent(caserecordid.ToString())
                .ValidateRecordNotPresent(involvementid1.ToString())
                .ValidateRecordNotPresent(involvementid2.ToString())
                .ValidateRecordNotPresent(casetaskid.ToString())
                .ValidateRecordNotPresent(contactid.ToString());
        }

        [Description("Person record with linked Task, Case Note and Contact - Person record linked to a Case record - " +
            "Case record with linked Tasks, Status History, Involvements and LAC Episodes - " +
            "Case record is restricted with allow user type restriction - Person Task Case Note and Contact is restricted with allow user type restriction - " +
            "Login with a system user account that is NOT included in the data restriction used above - Open the person record - Open the timeline tab - Filter by LAC Review records - " +
            "Validate that none of the records are visible to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24540")]
        public void RestrictedRecordsInTimeline_UITestMethod06()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Ivan", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_teamId, "Contact Centre", currentDate.AddYears(-1), true);

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId, 110000000, true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_teamId, "Routine");

            #endregion

            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_teamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Contact

            var contactid = dbHelper.contact.CreateContact(_teamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId, _contactStatus, _systemUserId, personID, "person", "Ivan " + currentDateTimeString, currentDate, "...", 1, 1);

            #endregion

            #region Person Task

            var persontaskid = dbHelper.task.CreatePersonTask(personID, "Ivan " + currentDateTimeString, "Task 01", "subject ...", _teamId);

            #endregion

            #region Person Case Note

            var personcasenoteid = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "CN 01", "notes ...", personID, currentDate);

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), "‘ANONYMOUS’", _teamId);

            #endregion

            #region Case

            var caserecordid = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region LAC Episode

            var lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(_teamId, caserecordid, "", personID, currentDate);

            #endregion

            #region LAC Review Type

            var lacReviewTypeId = commonMethodsDB.CreateLACReviewType(_teamId, "Default", currentDate);

            #endregion

            #region LAC Review

            var lacEpisodereviewID = dbHelper.lacReview.CreateLACReview(_teamId, lacEpisodeID, lacReviewTypeId, currentDate);

            #endregion

            #region Case Involvements

            var caseInvolvements = dbHelper.CaseInvolvement.GetByCaseID(caserecordid);
            Assert.AreEqual(2, caseInvolvements.Count);
            var involvementid1 = caseInvolvements[0];
            var involvementid2 = caseInvolvements[1];

            #endregion

            #region Task

            var casetaskid = dbHelper.task.CreateTask("Task 01", "notes ...", _teamId, null, null, null, null, null, null, caserecordid, personID, null, caserecordid, "", "case");

            #endregion

            #region Data Restriction

            var dataRestrictionId = commonMethodsDB.CreateDataRestrictionRecord("DR_CDV6_24540", 1, _teamId);

            #endregion

            #region Restriction records

            dbHelper.RestrictRecord(contactid, "contact", dataRestrictionId);
            dbHelper.RestrictRecord(persontaskid, "task", dataRestrictionId);
            dbHelper.RestrictRecord(personcasenoteid, "personcasenote", dataRestrictionId);

            dbHelper.RestrictRecord(caserecordid, "case", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodeID, "lacepisode", dataRestrictionId);
            dbHelper.RestrictRecord(lacEpisodereviewID, "lacreview", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid1, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(involvementid2, "CaseInvolvement", dataRestrictionId);
            dbHelper.RestrictRecord(casetaskid, "task", dataRestrictionId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapTimeLineTab();


            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()

                .SelectFilterBy("LAC Review")
                .ClickApplyButton()

                .ValidateRecordNotPresent(contactid.ToString())
                .ValidateRecordNotPresent(persontaskid.ToString())
                .ValidateRecordNotPresent(personcasenoteid.ToString())
                .ValidateRecordNotPresent(caserecordid.ToString())
                .ValidateRecordNotPresent(lacEpisodereviewID.ToString())
                .ValidateRecordNotPresent(involvementid1.ToString())
                .ValidateRecordNotPresent(involvementid2.ToString())
                .ValidateRecordNotPresent(casetaskid.ToString())
                .ValidateRecordNotPresent(contactid.ToString())

                .ValidateRestrictedRecordCardVisibility("1", true) //only message visible
                .ValidateRestrictedRecordCardVisibility("2", false)
                .ValidateRestrictedRecordCardVisibility("3", false)
                .ValidateRestrictedRecordCardVisibility("4", false)
                .ValidateRestrictedRecordCardVisibility("5", false)
                .ValidateRestrictedRecordCardVisibility("6", false)
                .ValidateRestrictedRecordCardVisibility("7", false)
                .ValidateRestrictedRecordCardVisibility("8", false)

                .ValidateRestrictedRecordCardVisibility("1", "Restricted Record: Please contact People T4 with any inquiries")
                ;
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-7315

        [Description("Open Person Record (person has no Case records) - Click on the Edit button - Click on the Run Workflow button - " +
            "Select the 'Portal - Create Initial Assessment' workflow - Wait for the Async workflow to run - " +
            "Validate that a new Case and Case Form records are automatically created for the person")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24572")]
        public void SampleWorkflowsToPopulateToDolist_UITestMethod01()
        {
            Guid personID = new Guid("8b5b834b-63b6-47aa-8aca-5ed3e12c8889"); //Noemi Bartlett
            string personNumber = "200177";
            var PortalCreateInitialAssessmentWFId = new Guid("76b54cb7-406d-eb11-a309-005056926fe4"); //Portal - Create Initial Assessment

            foreach (var caseid in dbHelper.Case.GetCasesByPersonID(personID))
            {
                foreach (var caseformid in dbHelper.caseForm.GetCaseFormByCaseID(caseid))
                    dbHelper.caseForm.DeleteCaseForm(caseformid);

                foreach (var caseinvolvementid in dbHelper.CaseInvolvement.GetByCaseID(caseid))
                    dbHelper.CaseInvolvement.DeleteCaseInvolvement(caseinvolvementid);

                foreach (var caseinvolvementid in dbHelper.CaseStatusHistory.GetByCaseID(caseid))
                    dbHelper.CaseStatusHistory.DeleteCaseStatusHistory(caseinvolvementid);

                dbHelper.Case.DeleteCase(caseid);
            }

            //remove all financial assessments
            foreach (var financialassessmentid in dbHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID.ToString()))
                dbHelper.financialAssessment.DeleteFinancialAssessment(financialassessmentid);

            //remove any portal task linked to the person
            foreach (var addressid in dbHelper.portalTask.GetByTargetUserId(personID))
                dbHelper.portalTask.DeletePortalTask(addressid);


            //Remove any existing workflow job
            foreach (var jobid in this.dbHelper.workflowJob.GetWorkflowJobByWorkflowId(PortalCreateInitialAssessmentWFId))
                dbHelper.workflowJob.DeleteWorkflowJob(jobid);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Noemi Bartlett")
                .ClickRunOnDemandWorkflowButton();

            lookupPopup
                .WaitForLookupPopupToLoad("Workflows")
                .SelectResultElement(PortalCreateInitialAssessmentWFId.ToString());

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Workflow job was successfully created for the on - demand workflow you've selected")
                .TapCloseButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Noemi Bartlett");


            //at this point we just have 1 workflow job created for the initial assessment
            var wfjobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(PortalCreateInitialAssessmentWFId);
            Assert.AreEqual(1, wfjobs.Count);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(wfjobs[0].ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(wfjobs[0]);


            //at this point we should have 1 case for the person
            var cases = dbHelper.Case.GetCasesByPersonID(personID);
            Assert.AreEqual(1, cases.Count);

            //validate the case fields
            var caseFields = dbHelper.Case.GetCaseByID(cases[0], "contactreasonid", "contactsourceid", "personawareofcontactid");
            Assert.AreEqual(new Guid("3784785b-9750-e911-a2c5-005056926fe4"), caseFields["contactreasonid"]);//Advice/Consultation
            Assert.AreEqual(new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"), caseFields["contactsourceid"]);//‘ANONYMOUS’
            Assert.AreEqual(1, caseFields["personawareofcontactid"]);//Advice/Consultation


            //at this point we should have 1 case form for the case
            var caseForms = dbHelper.caseForm.GetCaseFormByCaseID(cases[0]);
            Assert.AreEqual(1, caseForms.Count);

            //Validate the case form fields
            var caseFormFields = dbHelper.caseForm.GetCaseFormByID(caseForms[0], "documentid", "assessmentstatusid");
            Assert.AreEqual(new Guid("55000ca4-da3b-e911-a2c5-005056926fe4"), caseFormFields["documentid"]);//Initial Assessment
            Assert.AreEqual(1, caseFormFields["assessmentstatusid"]);//In Progress

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8562

        [Description("Open a person record in Edit mode - Tap on the Copy Record Link button - Validate that the Copy To Clipboard Dynamic Dialog Popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24573")]
        public void Person_CopyPageHyperlink_UITestMethod01()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Gilbert", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            string businessObjectName = "person";
            string expectedHyperlink = string.Format("https://phoenixqa.careworks.ie/Default.aspx?ReturnUrl=..%2Fpages%2Feditpage.aspx%3Fid%3D{0}%26type%3D{1}", personID.ToString(), businessObjectName);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Gilbert " + currentDateTimeString)
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad();

        }

        [Description("Open a person record in Edit mode - Tap on the Copy Record Link button - Wait for the Copy To Clipboard Dynamic Dialog Popup to be displayed - " +
            "Validate that the page link is generated and displayed in the text area of the dialog.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24574")]
        public void Person_CopyPageHyperlink_UITestMethod02()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Gilbert", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            string businessObjectName = "person";
            string expectedHyperlink = string.Format(appURL + "/pages/Default.aspx?ReturnUrl=%2Fpages%2Feditpage.aspx%3Fid%3D{0}%26type%3D{1}", personID.ToString(), businessObjectName);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Gilbert " + currentDateTimeString)
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad()
                .ValidateTextAreaLink(expectedHyperlink);

        }

        [Description("Open a person record in Edit mode - Tap on the Copy Record Link button - Wait for the Copy To Clipboard Dynamic Dialog Popup to be displayed - " +
            "Click on the Copy button - Validate that the Hyperlink is copied to the clipboard")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24575")]
        public void Person_CopyPageHyperlink_UITestMethod03()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Gilbert", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            string businessObjectName = "person";
            string expectedHyperlink = string.Format(appURL + "/pages/Default.aspx?ReturnUrl=%2Fpages%2Feditpage.aspx%3Fid%3D{0}%26type%3D{1}", personID.ToString(), businessObjectName);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Gilbert " + currentDateTimeString)
                .ClickCopyRecordLinkButton();

            copyToClipboardDynamicDialogPopup
                .WaitForCopyToClipboardDynamicDialogPopupToLoad()
                .TapCopyButton();

            string clipboardText = System.Windows.Forms.Clipboard.GetText();
            Assert.AreEqual(expectedHyperlink, clipboardText);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-7377

        [Description("Login on Caredirector - Enter a person record Hyperlink in the browser URL - Validate that the user is redirected to the person record page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("JiraIssueID", "CDV6-24576")]
        public void Case_OpenHyperlink_UITestMethod01()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Gilbert", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            #endregion

            string businessObjectName = "person";
            string personRecordHyperlink = string.Format("{0}/pages/Default.aspx?ReturnUrl=..%2Fpages%2Feditpage.aspx%3Fid%3D{1}%26type%3D{2}", appURL, personID.ToString(), businessObjectName);

            //Login
            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            //open the hyperlink
            personRecordPage
                .OpenPersonRecordHyperlink(personRecordHyperlink)
                .WaitForPersonRecordPageToLoadFromHyperlink("Gilbert " + currentDateTimeString)
                .TapEditButton();

            //validate that we can navigate to the person edit details page
            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "Gilbert " + currentDateTimeString);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10239

        [Description("Open Person Record (Person profile is linked to a website user with status approved and email verified = Yes ) - " +
            "Validate that the 'Website User Approved' icon is displayed in the person top banner.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24577")]
        public void ShowIconInBannerForPersonLinkedToPortalProfile_UITestMethod01()
        {
            Guid personID = new Guid("21c65d8c-e0af-4d11-8a98-1adb27a6dd30"); //Lamont Stevens
            string personNumber = "23176";

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .ValidateWebsiteUserApprovedIconVisibility(true)
                .ValidateWebsiteUserWaitingForApprovalIconVisibility(false);
        }

        [Description("Open Person Record (Person profile is linked to a website user with status is 'Waiting for Approval' and email verified = Yes ) - " +
            "Validate that the 'Website User Waiting for Approval' icon is displayed in the person top banner.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24578")]
        public void ShowIconInBannerForPersonLinkedToPortalProfile_UITestMethod02()
        {
            Guid personID = new Guid("f202760a-2c81-4064-8374-6719833e42ea"); //Carey Ortega
            string personNumber = "410938";

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .ValidateWebsiteUserApprovedIconVisibility(false)
                .ValidateWebsiteUserWaitingForApprovalIconVisibility(true);
        }

        [Description("Open Person Record (Person profile is linked to a website user with status is 'Approved' and email verified = No ) - " +
            "Validate that the 'Website User Approved' icon is displayed in the person top banner.")]
        [TestMethod]
        [TestCategory("UITest")]
        [TestProperty("JiraIssueID", "CDV6-24579")]
        public void ShowIconInBannerForPersonLinkedToPortalProfile_UITestMethod03()
        {
            Guid personID = new Guid("4da62e52-0eb2-460b-b78c-0cd70119c07e"); //Carla Ward
            string personNumber = "151789";

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .ValidateWebsiteUserApprovedIconVisibility(true)
                .ValidateWebsiteUserWaitingForApprovalIconVisibility(false);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10370

        [TestProperty("JiraIssueID", "CDV6-24679")]
        [Description("Edit a person Record - Set an email address with 64 characters long - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod01()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse98765432109876543210987654321098765432109876@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse98765432109876543210987654321098765432109876@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24680")]
        [Description("Edit a person Record - Set an email address with uppercase and lowercase letters - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod02()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("GerardHouse@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("GerardHouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24681")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters and digits - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod03()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("GerardHouse007@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("GerardHouse007@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24682")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and special characters - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod04()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("G!e#r$a%r&dH'o*u+s-e0/0=7?98_76{5|6@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("G!e#r$a%r&dH'o*u+s-e0/0=7?98_76{5|6@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24683")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and special characters (spacial characters are set sequentially) - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod05()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("GerardHouse!#$%&'*+-/=?_{|007@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("GerardHouse!#$%&'*+-/=?_{|007@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24684")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and special characters (spacial characters are set sequentially in the beginning of the string) - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod06()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("!#$%&'*+-/=?_{|GerardHouse007@mail.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("!#$%&'*+-/=?_{|GerardHouse007@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24685")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character ( - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod07()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard(House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24686")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character ) - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod08()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard)House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24687")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character , - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod09()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard,House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24688")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character : - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod10()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard:House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24689")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character ; - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod11()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard;House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24690")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character < - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod12()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard<House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24691")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character > - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod13()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard>House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24692")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and an addition @ character - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod14()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard@House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24693")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character [ - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod15()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard[House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24694")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character \\ - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod16()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard\\House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24695")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and the invalid character ] - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod17()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("Gerard]House007@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24696")]
        [Description("Edit a person Record - Set an email without Recipient Name - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod18()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("@mail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24697")]
        [Description("Edit a person Record - Set an email without the @ symbol - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod19()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhousemail.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24698")]
        [Description("Edit a person Record - Set an email without the domain name - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod20()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24699")]
        [Description("Edit a person Record - Set an email without the top level domain - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod21()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@mail")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24700")]
        [Description("Edit a person Record - Set an email without the top level domain - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod22()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@mail.")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24701")]
        [Description("Edit a person Record - Set an email with and invalid special character in the domain name - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod23()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@ma+il.com")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24702")]
        [Description("Edit a person Record - Set an email with and invalid special character in the top level domain - Tap on the save and close button - " +
            "Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod24()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@mail.co+m")
                .TapSaveAndCloseButton()
                .ValidatePrimaryEmailErrorLabelVisibility(true)
                .ValidatePrimaryEmailErrorLabelText("Please enter a valid email")
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24703")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and hyphens and underscores in the domain name - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod25()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@M-a-i_L_007.com")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@M-a-i_L_007.com", fields["primaryemail"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24704")]
        [Description("Edit a person Record - Set an email address with uppercase letters, lowercase letters, digits and hyphens and underscores in the top domain level - Tap on the save and close button - " +
            "Validate that the user can save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void EmailAddressValidation_UITestMethod26()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "William", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            dbHelper.person.UpdatePrimaryEmail(personID, "gerardhouse@mail.com");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), "William " + currentDateTimeString)
                .InsertPrimaryEmail("gerardhouse@mail.C_o_me-r-C007e")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            var fields = dbHelper.person.GetPersonById(personID, "primaryemail");
            Assert.AreEqual("gerardhouse@mail.C_o_me-r-C007e", fields["primaryemail"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10366

        [TestProperty("JiraIssueID", "CDV6-24705")]
        [Description("'DateOfBirth' Duplicate Detection Condition for 'HCC - Last Name and DOB' Duplicate Detection Rule has 'Ignore Blank Values' set to Yes - " +
            "Create a new person record with last name set to 'Usk', DOB And Age set to 'Unknown', null DOB - " +
            "Click on the save button - Wait for the 'POTENTIAL DUPLICATES' popup to load" +
            "Validate that all person records with matching Last name are detected as duplicates")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void DuplicateDetection_UITestMethod01()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Ethnicity

            var ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "White", new DateTime(2000, 1, 1));

            #endregion

            #region Person

            var personRecord1 = dbHelper.person.CreatePersonRecord("", "Donald", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personRecord2 = dbHelper.person.CreatePersonRecord("", "Figgs", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personRecord3 = dbHelper.person.CreatePersonRecord("", "Fiona", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            #endregion

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_LastName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("LastName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Last Name and DOB", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("LastName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_LastName);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("D")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectStatedGender("Male")
                .SelectDOBAndAge("Unknown")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("White").TapSearchButton().SelectResultElement(ethnicity.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("XYZ1")
                .TapSaveAndCloseButton();

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .ValidateDuplicateRecordVisibility(personRecord1.ToString(), true)
                .ValidateDuplicateRecordVisibility(personRecord2.ToString(), true)
                .ValidateDuplicateRecordVisibility(personRecord3.ToString(), true);

        }

        [TestProperty("JiraIssueID", "CDV6-24706")]
        [Description("'DateOfBirth' Duplicate Detection Condition for 'HCC - Last Name and DOB' Duplicate Detection Rule has 'Ignore Blank Values' set to No - " +
            "Create a new person record with last name set to 'Usk', DOB And Age set to 'Unknown', null DOB - " +
            "Click on the save button - Wait for the 'POTENTIAL DUPLICATES' popup to load" +
            "Validate that all person records with matching Last name and null DOB are detected as duplicates")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void DuplicateDetection_UITestMethod02()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Ethnicity

            var ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "White", new DateTime(2000, 1, 1));

            #endregion

            #region Person

            var personRecord1 = dbHelper.person.CreatePersonRecord("", "Donald", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personRecord2 = dbHelper.person.CreatePersonRecord("", "Figgs", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord2, 3, null, 30);
            var personRecord3 = dbHelper.person.CreatePersonRecord("", "Fiona", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord3, 3, null, 38);

            #endregion

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_LastName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("LastName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Last Name and DOB", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("LastName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_LastName);

            #endregion

            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, false);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("D")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectStatedGender("Male")
                .SelectDOBAndAge("Unknown")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("White").TapSearchButton().SelectResultElement(ethnicity.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("XYZ1")
                .TapSaveAndCloseButton();

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .ValidateDuplicateRecordVisibility(personRecord1.ToString(), false)
                .ValidateDuplicateRecordVisibility(personRecord2.ToString(), true)
                .ValidateDuplicateRecordVisibility(personRecord3.ToString(), true);

        }

        [TestProperty("JiraIssueID", "CDV6-24707")]
        [Description("'DateOfBirth' Duplicate Detection Condition for 'HCC - Last Name and DOB' Duplicate Detection Rule has 'Ignore Blank Values' set to No - " +
            "Create a new person record with last name set to 'Usk', DOB And Age set to 'DOB', DOB set to '25/12/1946' - " +
            "Click on the save button - Wait for the 'POTENTIAL DUPLICATES' popup to load" +
            "Validate that all person records with matching Last name and DOB are detected as duplicates")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void DuplicateDetection_UITestMethod03()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Ethnicity

            var ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "White", new DateTime(2000, 1, 1));

            #endregion

            #region Person

            var personRecord1 = dbHelper.person.CreatePersonRecord("", "Donald", "", currentDateTimeString, "", new DateTime(1946, 12, 25), _ethnicityId, _teamId, 7, 2);
            var personRecord2 = dbHelper.person.CreatePersonRecord("", "Figgs", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord2, 3, null, 30);
            var personRecord3 = dbHelper.person.CreatePersonRecord("", "Fiona", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord3, 3, null, 38);

            #endregion

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_LastName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("LastName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Last Name and DOB", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("LastName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_LastName);

            #endregion

            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, false);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("D")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectStatedGender("Male")
                .SelectDOBAndAge("DOB")
                .InsertDOB("25/12/1946")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("White").TapSearchButton().SelectResultElement(ethnicity.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("XYZ1")
                .TapSaveAndCloseButton();

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .ValidateDuplicateRecordVisibility(personRecord1.ToString(), true)
                .ValidateDuplicateRecordVisibility(personRecord2.ToString(), false)
                .ValidateDuplicateRecordVisibility(personRecord3.ToString(), false);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10367

        [TestProperty("JiraIssueID", "CDV6-24708")]
        [Description("'DateOfBirth' Duplicate Detection Condition for 'HCC - Last Name and DOB' Duplicate Detection Rule has 'Ignore Blank Values' set to Yes - " +
            "Create a new person record with last name set to 'Ulm', DOB And Age set to 'DOB', DOB set to '01/01/1982' - " +
            "Click on the save button - Wait for the 'POTENTIAL DUPLICATES' popup to load" +
            "Click on the matching existing person record - Wait for the 'EDIT DUPLICATED DATA' section to be displayed - " +
            "Validate that the DOB data is displayed in the correct format")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void DuplicateDetection_DOBAndAgeGroup_UITestMethod01()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Ethnicity

            var ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "White", new DateTime(2000, 1, 1));

            #endregion

            #region Person

            var personRecord1 = dbHelper.person.CreatePersonRecord("", "Jhon", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);

            #endregion

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_LastName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("LastName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Last Name and DOB", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("LastName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_LastName);

            #endregion

            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("jhon")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectStatedGender("Male")
                .SelectDOBAndAge("DOB")
                .InsertDOB("01/01/1982")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("White").TapSearchButton().SelectResultElement(ethnicity.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("W2 6TZ")
                .TapSaveAndCloseButton();

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .CllickDuplicateRecord(personRecord1.ToString())
                .ClickUpdateExistingButton()

                .WaitForEditDuplicatesPopupToLoad()
                .ValidateNewRecordDOBValue("01/01/1982")
                .ValidateExistingRecordDOBValue("01/01/1982")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24709")]
        [Description("'DateOfBirth' Duplicate Detection Condition for 'HCC - Last Name and DOB' Duplicate Detection Rule has 'Ignore Blank Values' set to Yes - " +
            "Create a new person record with last name set to 'Ulm', DOB And Age set to 'DOB', DOB set to '01/01/1982' - " +
            "Click on the save button - Wait for the 'POTENTIAL DUPLICATES' popup to load" +
            "Click on the matching existing person record - Wait for the 'EDIT DUPLICATED DATA' section to be displayed - " +
            "Validate that the radio buttons for the existing record are selected by default")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void DuplicateDetection_DOBAndAgeGroup_UITestMethod02()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Ethnicity

            var ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "White", new DateTime(2000, 1, 1));

            #endregion

            #region Person

            var personRecord1 = dbHelper.person.CreatePersonRecord("", "Jhon", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);

            #endregion

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_LastName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("LastName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Last Name and DOB", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("LastName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_LastName);

            #endregion

            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("jhon")
                .InsertLastName(currentDateTimeString)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectStatedGender("Male")
                .SelectDOBAndAge("DOB")
                .InsertDOB("01/01/1982")
                .ClickEthnicityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("White").TapSearchButton().SelectResultElement(ethnicity.ToString());

            personRecordEditPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAddressType("Home")
                .InsertPostCode("W2 6TZ")
                .TapSaveAndCloseButton();

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .CllickDuplicateRecord(personRecord1.ToString())
                .ClickUpdateExistingButton()

                .WaitForEditDuplicatesPopupToLoad()
                .ValidateNewRecordDOBChecked(false)
                .ValidateNewRecordAgeGroupChecked(false)
                .ValidateExistingRecordDOBChecked(true)
                .ValidateExistingRecordAgeGroupChecked(true);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10462

        [TestProperty("JiraIssueID", "CDV6-24710")]
        [Description("Testing server side rules for MASH forms - Scenario 1 - MASH Episode 'Date/Time Contact Received' and 'Date/Time Initial MASH Rating' should match - " +
            "'Adult Mental Health' should be set to 'Non-Disclosable' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Document

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 4; //Not Initialized


            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");

            //update Date/Time Contact Received
            dbHelper.mashEpisode.UpdateDateTimeContactReceived(mashEpisodeID, new DateTime(2021, 6, 10, 11, 12, 0));

            //update Date/Time Initial MASH Rating
            dbHelper.mashEpisode.UpdateDateTimeInitialMASHRating(mashEpisodeID, new DateTime(2021, 6, 10, 11, 12, 0));

            //set Adult Mental Health to 'Non-Disclosable'
            dbHelper.mashEpisode.UpdateAdultMentalHealth(mashEpisodeID, 3);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 1").TapOKButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24711")]
        [Description("Testing server side rules for MASH forms - Scenario 1 - MASH Episode 'Date/Time Contact Received' and 'Date/Time Initial MASH Rating' should NOT match - " +
            "'Adult Mental Health' should be set to 'Non-Disclosable' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 4; //Not Initialized


            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");

            //update Date/Time Contact Received
            dbHelper.mashEpisode.UpdateDateTimeContactReceived(mashEpisodeID, new DateTime(2021, 6, 10, 11, 12, 0));

            //update Date/Time Initial MASH Rating
            dbHelper.mashEpisode.UpdateDateTimeInitialMASHRating(mashEpisodeID, new DateTime(2021, 6, 10, 11, 14, 0));

            //set Adult Mental Health to 'Non-Disclosable'
            dbHelper.mashEpisode.UpdateAdultMentalHealth(mashEpisodeID, 3);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24712")]
        [Description("Testing server side rules for MASH forms - Scenario 1 - MASH Episode 'Date/Time Contact Received' and 'Date/Time Initial MASH Rating' should match - " +
            "'Adult Mental Health' should be set to 'Yes' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 4; //Not Initialized

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");

            //update Date/Time Contact Received
            dbHelper.mashEpisode.UpdateDateTimeContactReceived(mashEpisodeID, new DateTime(2021, 6, 10, 11, 12, 0));

            //update Date/Time Initial MASH Rating
            dbHelper.mashEpisode.UpdateDateTimeInitialMASHRating(mashEpisodeID, new DateTime(2021, 6, 10, 11, 12, 0));

            //set Adult Mental Health to 'Yes'
            dbHelper.mashEpisode.UpdateAdultMentalHealth(mashEpisodeID, 1);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24713")]
        [Description("Testing server side rules for MASH forms - Scenario 2 - MASH Episode Form 'Status' set to 'In Progress' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 1; //In Progress

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 2").TapOKButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24714")]
        [Description("Testing server side rules for MASH forms - Scenario 2 - MASH Episode Form 'Not Initialized' set to 'In Progress' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod005()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion


            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 4; //Not Initialized


            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24715")]
        [Description("Testing server side rules for MASH forms - Scenario 3 - Person linled to MASH Episode Form has 'DOB' greater than '01/03/2005' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod006()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 1; //In Progress

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 3");

            //update person dob
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2005, 3, 4));


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 3").TapOKButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24716")]
        [Description("Testing server side rules for MASH forms - Scenario 3 - Person linled to MASH Episode Form has 'DOB' smaller than '01/03/2005' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void MASHForm_EnableServerSideRules_UITestMethod007()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = new DateTime(2021, 6, 16);
            var assessmentstatusid = 1; //In Progress


            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1308")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(mashEpisodeFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 3");

            //update person dob
            dbHelper.person.UpdateDateOfBirth(personID, new DateTime(2005, 3, 1));


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10781

        [TestProperty("JiraIssueID", "CDV6-24717")]
        [Description("Open a Mash Episode form record (Form Type set to 'Automation - MASH Episode Form 1') - Mash Episode has no value set for 'Date/Time Case Recorded' - " +
            "Validate that the Question 'Mash Episode DateTime Case Recorded' has no value")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WriteBackField_DateTime_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = DateTime.Now.Date;
            var assessmentstatusid = 1; //In Progress

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //update mash episode Date/Time Case Recorded
            dbHelper.mashEpisode.UpdateCaseRecordedDatetime(mashEpisodeID, null);


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString())
                .ValidateMashEpisodeDateTimeCaseRecordedValue("", "");
        }

        [TestProperty("JiraIssueID", "CDV6-24718")]
        [Description("Open a Mash Episode form record (Form Type set to 'Automation - MASH Episode Form 1') - Mash Episode has a date value set for 'Date/Time Case Recorded' - " +
            "Validate that the Question 'Mash Episode DateTime Case Recorded' has the same date as the one set for 'Date/Time Case Recorded' ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WriteBackField_DateTime_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = DateTime.Now.Date;
            var assessmentstatusid = 1; //In Progress

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //update mash episode Date/Time Case Recorded
            dbHelper.mashEpisode.UpdateCaseRecordedDatetime(mashEpisodeID, new DateTime(2021, 6, 23, 10, 30, 0));


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString())
                .ValidateMashEpisodeDateTimeCaseRecordedValue("23/06/2021", "10:30");
        }

        [TestProperty("JiraIssueID", "CDV6-24719")]
        [Description("Open a Mash Episode Form record (Form Type set to 'Automation - MASH Episode Form 1') - Mash Episode has a date value set for 'Date/Time Case Recorded' - " +
            "Update the value for the 'Mash Episode DateTime Case Recorded' question - Save, Close and Re-open the Mash Episode Form - Validate that the date is correctly saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WriteBackField_DateTime_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion


            var startDate = DateTime.Now.Date;
            var assessmentstatusid = 1; //In Progress


            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //update mash episode Date/Time Case Recorded
            dbHelper.mashEpisode.UpdateCaseRecordedDatetime(mashEpisodeID, new DateTime(2021, 6, 23, 10, 30, 0));


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString())
                .InsertMashEpisodeDateTimeCaseRecordedValue("22/06/2021", "15:29")
                .TapSaveAndCloseButton();

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString())
                .ValidateMashEpisodeDateTimeCaseRecordedValue("22/06/2021", "15:29");
        }

        [TestProperty("JiraIssueID", "CDV6-24720")]
        [Description("Open a Mash Episode Form record (Form Type set to 'Automation - MASH Episode Form 1') - Mash Episode has a date value set for 'Date/Time Case Recorded' - " +
            "Update the value for the 'Mash Episode DateTime Case Recorded' question - Save and Close the Mash Episode Form - Navigate back to the mash episode - " +
            "Validate that the Date/Time Case Recorded field is correctly updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void WriteBackField_DateTime_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Workflow

            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Automation - MASH Episode Form 1", "Automation - MASH Episode Form 1.Zip");

            #endregion

            #region Person

            var personID = dbHelper.person.CreatePersonRecord("", "Blade", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Mash Status

            var mashstatuscategoryid = 1; //New Episode
            var mashStatusId = commonMethodsDB.CreateMashStatus("New Episode", mashstatuscategoryid, new DateTime(2000, 1, 1), _teamId);

            #endregion

            #region MASH Episode

            var mashEpisodeID = dbHelper.mashEpisode.CreateMashEpisode(personID, "person", "Blade" + currentDateTimeString, personID, new DateTime(2021, 6, 10), mashStatusId, _teamId, _systemUserId, 1);

            #endregion

            var startDate = DateTime.Now.Date;
            var assessmentstatusid = 1; //In Progress

            //Create a new MASH Episode form
            var mashEpisodeFormID = dbHelper.mashEpisodeForm.CreateMASHEpisodeFormRecord(_teamId, documentid, mashEpisodeID, personID, startDate, assessmentstatusid);

            //update mash episode Date/Time Case Recorded
            dbHelper.mashEpisode.UpdateCaseRecordedDatetime(mashEpisodeID, new DateTime(2021, 6, 23, 10, 30, 0));


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser4", "Passw0rd_!")
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
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .NavigateToMASHEpisodeFormsPage();

            personMASHEpisodeFormsPage
                .WaitForPersonMASHEpisodeFormsPageToLoad()
                .OpenPersonMASHEpisodeFormRecord(mashEpisodeFormID.ToString());

            personMASHEpisodeFormRecordPage
                .WaitForPersonMASHEpisodeFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationMASHEpisodeForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(mashEpisodeFormID.ToString())
                .InsertMashEpisodeDateTimeCaseRecordedValue("22/06/2021", "15:29")
                .TapSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToMASHEpisodesPage();

            personMASHEpisodesPage
                .WaitForPersonMASHEpisodesPageToLoad()
                .OpenPersonMASHEpisodeRecord(mashEpisodeID.ToString());

            personMASHEpisodeRecordPage
                .WaitForPersonMASHEpisodeRecordPageToLoad()
                .ValidateDateTimeCaseRecorded("22/06/2021", "15:29");
            ;


        }

        #endregion

        //#region  https://advancedcsg.atlassian.net/browse/CDV6-14012

        //[TestProperty("JiraIssueID", "ACC-3170")]
        //[Description("Login CD Web -> Work Place -> My Work -> People -> Check the options in the View-> Should have a new view My Pinned People along with the existing views" +
        //    "Select My Pinned People-> Should display only the people records pinned to the logged in user")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule", "Person")]
        //[TestProperty("Screen", "People")]
        //public void PersonRecord_UITestMethod01()
        //{

        //    var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

        //    #region Person

        //    var _personID = dbHelper.person.CreatePersonRecord("", "Anson", "", currentDateTimeString, "", new DateTime(1982, 1, 1), _ethnicityId, _teamId, 7, 2);
        //    var _personNumber = dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"].ToString();

        //    #endregion

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("PeopleUser4", "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .ValidateMyPinnedPeopleOption("My Pinned People")
        //        .SearchPersonRecord(_personNumber.ToString(), _personID.ToString())
        //        .SelectPersonRecord(_personID.ToString())
        //        .ClickAdditionalItemsMenuButton()
        //        .ClickPinToMe();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .SelectAvailableViewByText("My Pinned People");

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .ValidatePersonRecord(_personID.ToString(), _personID.ToString());
        //}

        //#endregion


    }
}
