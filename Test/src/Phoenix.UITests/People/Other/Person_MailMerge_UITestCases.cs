using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_MailMerge_UITestCases : FunctionalTest
    {

        #region Jira Issue ID:  https://advancedcsg.atlassian.net/browse/CDV6-4917

        #region Properties

        private Guid _person1ID;
        private int _person1Number;

        private Guid _person2ID;
        private int _person2Number;

        private string _personLastName;

        #endregion

        #region Create Audit Validations

        private void CreatePersonRecord()
        {
            #region Business Unit

            if (!dbHelper.businessUnit.GetByName("CareDirector QA").Any())
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            if (!dbHelper.team.GetTeamIdByName("CareDirector QA").Any())
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region Person

            var _personFirstName = "Test_CDV6_4917";
            var _personLastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            _person1ID = dbHelper.person.CreatePersonRecord("", _personFirstName, "", _personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _person1Number = (int)dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];
            #endregion
        }

        private void CreateTwoPersonRecords()
        {
            #region Business Unit

            if (!dbHelper.businessUnit.GetByName("CareDirector QA").Any())
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            if (!dbHelper.team.GetTeamIdByName("CareDirector QA").Any())
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            _personLastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Person 1

            var _person1FirstName = "Test_CDV6_4917_P1";

            _person1ID = dbHelper.person.CreatePersonRecord("", _person1FirstName, "", _personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _person1Number = (int)dbHelper.person.GetPersonById(_person1ID, "personnumber")["personnumber"];

            #endregion

            #region Person 2

            var _person2FirstName = "Test_CDV6_4917_P2";

            _person2ID = dbHelper.person.CreatePersonRecord("", _person2FirstName, "", _personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _person2Number = (int)dbHelper.person.GetPersonById(_person2ID, "personnumber")["personnumber"];

            #endregion
        }


        [TestProperty("JiraIssueID", "CDV6-20377")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4917 - " +
            "Login in Caredirector V6 - Navigate to the People page - Select one Person Record - Tap on the Mail Merge button - " +
            "Select 'Person Address' as the mail merge template - Set 'Selected records on current page' for Merge - " +
            "Set 'No' for Create Activity - Tap on the OK button - " +
            "Validate that a Audit record is created of type 'Mail Merge' - " +
            "Validate that the Comments field in the audit record contains the Merge template name")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_MailMerge_UITestMethod001()
        {
            Guid systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName("MailMerge.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, "Word");

            CreatePersonRecord();

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_person1Number.ToString())
                .SelectPersonRecord(_person1ID.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "PersonAddress.docx");
            Assert.IsTrue(fileExists);


            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 27, //Mail Merge
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _person1ID.ToString(),
                ParentTypeName = "person",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Mail Merge", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("CW Forms Test User 1", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person Address", auditResponseData.GridData[0].cols[3].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-20378")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4917 - " +
            "Login in Caredirector V6 - Navigate to the People page - Select multiple Person Record - Tap on the Mail Merge button - " +
            "Select 'Person Address' as the mail merge template - Set 'Selected records on current page' for Merge - " +
            "Set 'No' for Create Activity - Tap on the OK button - " +
            "Validate that a Audit record is created of type 'Mail Merge' for each person record - " +
            "Validate that the Comments field in the audit records contains the Merge template name")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_MailMerge_UITestMethod002()
        {
            Guid systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName("MailMerge.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, "Word");

            CreateTwoPersonRecords();


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_personLastName)
                .SelectPersonRecord(_person1ID.ToString())
                .SelectPersonRecord(_person2ID.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "PersonAddress.docx");
            Assert.IsTrue(fileExists);


            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 27, //Mail Merge
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _person1ID.ToString(),
                ParentTypeName = "person",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie); //get the audit for person 1

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Mail Merge", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("CW Forms Test User 1", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person Address", auditResponseData.GridData[0].cols[3].Text);


            auditSearch.ParentId = _person2ID.ToString();
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie); //get the audit for person 2

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Mail Merge", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("CW Forms Test User 1", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person Address", auditResponseData.GridData[0].cols[3].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-20379")]
        [Description("Related development Issue https://advancedcsg.atlassian.net/browse/CDV6-4917 - " +
            "Login in Caredirector V6 - Navigate to the People page - Select multiple Person Record - Tap on the Mail Merge button - " +
            "Select 'Person Address' as the mail merge template - Set 'Selected records on current page' for Merge - " +
            "Set 'Yes' for Create Activity - Select 'Open Activity' for Create As - Insert an Activity Subject - Tap on the OK button - " +
            "Validate that a Audit record is created of type 'Mail Merge' for each person record - " +
            "Validate that the Comments field in the audit records contains the Merge template name")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_MailMerge_UITestMethod003()
        {
            Guid systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName("MailMerge.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, "Word");

            CreateTwoPersonRecords();


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(_personLastName)
                .SelectPersonRecord(_person1ID.ToString())
                .SelectPersonRecord(_person2ID.ToString())
                .TapMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Person Address")
                .TapCreateActivityYesRadioButton()
                .TapOpenActivityRadioButton()
                .InsertActivitySubject("Mail Merge - Validate Audits")
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "PersonAddress.docx");
            Assert.IsTrue(fileExists);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 27, //Mail Merge
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = _person1ID.ToString(),
                ParentTypeName = "person",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie); //get the audit for person 1

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Mail Merge", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("CW Forms Test User 1", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person Address", auditResponseData.GridData[0].cols[3].Text);


            auditSearch.ParentId = _person2ID.ToString();
            auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie); //get the audit for person 2

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Mail Merge", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("CW Forms Test User 1", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person Address", auditResponseData.GridData[0].cols[3].Text);
        }

        #endregion


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
