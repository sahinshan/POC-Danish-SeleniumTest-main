using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Phoenix.UITests.Settings.DataManagement
{
    [TestClass]
    public class FileDestructionGDPR_UITestCases : FunctionalTest
    {
        public Guid FileDestructionScheduleJob { get { return new Guid("fff2e6ca-b1ee-ea11-a2cc-0050569231cf"); } }


        #region issue https://advancedcsg.atlassian.net/browse/CDV6-4742

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Navigate to the File Destruction GDPR page - validate that the page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24427")]
        public void FileDestructionGDPR_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad();
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Navigate to the File Destruction GDPR page - Tap on the Add button - Validate that the File Destruction (GDPR) New record page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24428")]
        public void FileDestructionGDPR_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New");
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Navigate to the File Destruction GDPR page - Tap on the Add button - Tap the save button - " +
            "Validate that the user is prevented from saving the record without a 'Record to be deleted' field")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24429")]
        public void FileDestructionGDPR_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ValidateNotificationAreaMessageVisible("Some data is not correct. Please review the data in the Form.")
                .RecordToBeDeletedAreaMessageVisible("Please fill out this field.")
                .SurrogateRecordAreaMessageVisible("Please fill out this field.")
                .ScheduleDateForDestructionDateAreaMessageVisible("Please fill out this field.")
                .ScheduleDateForDestructionTimeAreaMessageVisible("Please fill out this field.");



        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - " +
            "Navigate to the File Destruction GDPR page - Tap on the Add button - Select Person Record (A) as the person to be deleted - Select the a Surrogate person record - " +
            "Set the 'Schedule date for destruction' for today - Save the File Destruction record - Execute the File Destruction Schedule Job - " +
            "Validate that the person record is NOT deleted after executing the job (First Approved By field is not set)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24430")]
        public void FileDestructionGDPR_UITestMethod04()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //Get the person by its id and assure that the person is still present in DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(1, fields.Count);



        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - " +
            "Navigate to the File Destruction GDPR page - Tap on the Add button - Select Person Record (A) as the person to be deleted - Select the a Surrogate person record - " +
            "Set the 'Schedule date for destruction' for today - Save the File Destruction record - Click on the First Approved By button - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is NOT deleted after executing the job (Second Approved By field is not set)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24431")]
        public void FileDestructionGDPR_UITestMethod05()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //Get the person by its id and assure that the person is still present in DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(1, fields.Count);



        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - " +
            "Navigate to the File Destruction GDPR page - Tap on the Add button - Select Person Record (A) as the person to be deleted - Select the a Surrogate person record - " +
            "Set the 'Schedule date for destruction' for today - Save the File Destruction record - Click on the First Approved By button - Click on the Second Approved By button" +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24432")]
        public void FileDestructionGDPR_UITestMethod06()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);


        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Task record and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24433")]
        public void FileDestructionGDPR_UITestMethod07()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a task record associated with the person
            Guid taskID = dbHelper.task.CreatePersonTask(_personToDelete, FirstName + " ToDelete", "Person Task 01", "Task Notes ...", careDirectorTeamID);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no task record exist
            List<Guid> tasks = dbHelper.task.GetTaskByPersonID(_personToDelete);
            Assert.AreEqual(0, tasks.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Professional Record (A) - Create a new Task record and associate it with the Professional - " +
            "Create a new File Destruction record (for the professional) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the Professional record is deleted after executing the job")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24434")]
        public void FileDestructionGDPR_UITestMethod08()
        {
            Guid surrogateProfessionalID = new Guid("46632d10-47f3-ea11-a2cd-005056926fe4"); //Mr Jhon Silva - 7270

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogateProfessionalID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid professiontypeid = new Guid("961F03E7-6F3A-E911-A2C5-005056926FE4"); //doctor

            //Create person record to delete
            Guid _professionalToDelete = dbHelper.professional.CreateProfessional(careDirectorTeamID, professiontypeid, "Mr", FirstName, "ToDelete");

            //create a task record associated with the person
            Guid taskID = dbHelper.task.CreateProfessionalTask(_professionalToDelete, FirstName + " ToDelete", "Professional Task 01", "Task Notes ...", careDirectorTeamID);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Professionals").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_professionalToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Professionals").TypeSearchQuery("Jhon").TapSearchButton().SelectResultElement(surrogateProfessionalID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            var fields = dbHelper.professional.GetProfessionalByID(_professionalToDelete, "professionalid");
            Assert.AreEqual(0, fields.Count);

            //validate that no task record exist
            List<Guid> tasks = dbHelper.task.GetTaskByPersonID(_professionalToDelete);
            Assert.AreEqual(0, tasks.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Person Form and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24435")]
        public void FileDestructionGDPR_UITestMethod09()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a Person Form record associated with the person
            Guid documentID = new Guid("c9ef906d-4af3-ea11-a2cd-005056926fe4");//Person Automation Form 1
            Guid personFormID = dbHelper.personForm.CreatePersonForm(careDirectorTeamID, _personToDelete, documentID, DateTime.Now.Date);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no task record exist
            var forms = dbHelper.personForm.GetPersonFormByPersonID(_personToDelete);
            Assert.AreEqual(0, forms.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create two Person Form and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24436")]
        public void FileDestructionGDPR_UITestMethod09_1()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a Person Form record associated with the person
            Guid documentID = new Guid("c9ef906d-4af3-ea11-a2cd-005056926fe4");//Person Automation Form 1
            Guid personFormID1 = dbHelper.personForm.CreatePersonForm(careDirectorTeamID, _personToDelete, documentID, DateTime.Now.Date);
            Guid personFormID2 = dbHelper.personForm.CreatePersonForm(careDirectorTeamID, _personToDelete, documentID, DateTime.Now.Date.AddDays(-1));


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no task record exist
            var forms = dbHelper.personForm.GetPersonFormByPersonID(_personToDelete);
            Assert.AreEqual(0, forms.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Social Care Case and associate it with the Person - Associate an adult safeguarding record with the case - " +
            "Create an Allegation record related to the adult safeguarding and an allegation investigator related to the allegation record" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24437")]
        public void FileDestructionGDPR_UITestMethod10()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a Social Care Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;
            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);

            //create adult Safeguarding record
            Guid adultsafeguardingcategoryofabuseid = new Guid("1f43e25d-bbb2-e911-a2c6-005056926fe4"); //Emotional
            Guid adultsafeguardingstatusid = new Guid("187b2938-2d86-ea11-a2cd-005056926fe4"); //Concern
            DateTime startdate = new DateTime(2020, 7, 1);

            Guid safeguardingID = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(careDirectorTeamID, responsibleuserid, caseID, "", _personToDelete, adultsafeguardingcategoryofabuseid, adultsafeguardingstatusid, startdate);

            //create an allegation record
            Guid allegedabuserid = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string allegedabuseridtablename = "person";
            string allegedabuseridname = "Mr Adolfo Abbott";
            Guid allegationcategoryid = new Guid("A3D97FA0-2D86-EA11-A2CD-005056926FE4"); //Disctiminatory Abuse
            DateTime allegationdate = DateTime.Now.Date;

            Guid allegationID = dbHelper.allegation.CreateAllegation(safeguardingID, careDirectorTeamID, _personToDelete, "person", FirstName + " ToDelete", allegedabuserid, allegedabuseridtablename, allegedabuseridname, _personToDelete, allegationcategoryid, allegationdate);


            //create allegation investigator
            Guid allegationInvestigator = dbHelper.allegationInvestigator.CreateAllegationInvestigator(careDirectorTeamID, responsibleuserid, allegationID, allegationdate);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no Adult Safeguarding record exist
            var adultSafeguardings = dbHelper.AdultSafeguarding.GetAdultSafeguardingByCaseID(caseID);
            Assert.AreEqual(0, adultSafeguardings.Count);

            //validate that no Allegations record exist
            var allegations = dbHelper.allegation.GetAllegationByAdultSafeguardingID(safeguardingID);
            Assert.AreEqual(0, allegations.Count);

            //validate that no Allegation investigator record exist
            var investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(allegationID);
            Assert.AreEqual(0, investigators.Count);

        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Social Care Case and associate it with the Person - Associate 2 adult safeguarding records with the case - " +
            "Create 2 Allegation records related to each adult safeguarding and 2 allegation investigators related to each allegation record" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24438")]
        public void FileDestructionGDPR_UITestMethod11()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a Social Care Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);

            //create adult Safeguarding record
            Guid adultsafeguardingcategoryofabuseid = new Guid("1f43e25d-bbb2-e911-a2c6-005056926fe4"); //Emotional
            Guid adultsafeguardingstatusid = new Guid("187b2938-2d86-ea11-a2cd-005056926fe4"); //Concern
            DateTime startdate1 = new DateTime(2020, 7, 1);
            DateTime startdate2 = new DateTime(2020, 7, 2);

            Guid safeguardingID1 = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(careDirectorTeamID, responsibleuserid, caseID, "", _personToDelete, adultsafeguardingcategoryofabuseid, adultsafeguardingstatusid, startdate1);
            Guid safeguardingID2 = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(careDirectorTeamID, responsibleuserid, caseID, "", _personToDelete, adultsafeguardingcategoryofabuseid, adultsafeguardingstatusid, startdate2);

            //create an allegation record
            Guid allegedabuserid = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string allegedabuseridtablename = "person";
            string allegedabuseridname = "Mr Adolfo Abbott";
            Guid allegationcategoryid = new Guid("A3D97FA0-2D86-EA11-A2CD-005056926FE4"); //Disctiminatory Abuse

            DateTime allegationdate1 = new DateTime(2020, 7, 1);
            DateTime allegationdate2 = new DateTime(2020, 7, 2);

            Guid allegationID1 = dbHelper.allegation.CreateAllegation(safeguardingID1, careDirectorTeamID, _personToDelete, "person", FirstName + " ToDelete", allegedabuserid, allegedabuseridtablename, allegedabuseridname, _personToDelete, allegationcategoryid, allegationdate1);
            Guid allegationID2 = dbHelper.allegation.CreateAllegation(safeguardingID1, careDirectorTeamID, _personToDelete, "person", FirstName + " ToDelete", allegedabuserid, allegedabuseridtablename, allegedabuseridname, _personToDelete, allegationcategoryid, allegationdate2);
            Guid allegationID3 = dbHelper.allegation.CreateAllegation(safeguardingID2, careDirectorTeamID, _personToDelete, "person", FirstName + " ToDelete", allegedabuserid, allegedabuseridtablename, allegedabuseridname, _personToDelete, allegationcategoryid, allegationdate1);
            Guid allegationID4 = dbHelper.allegation.CreateAllegation(safeguardingID2, careDirectorTeamID, _personToDelete, "person", FirstName + " ToDelete", allegedabuserid, allegedabuseridtablename, allegedabuseridname, _personToDelete, allegationcategoryid, allegationdate2);


            //create allegation investigator
            Guid allegationInvestigator1 = dbHelper.allegationInvestigator.CreateAllegationInvestigator(careDirectorTeamID, responsibleuserid, allegationID1, allegationdate1);
            Guid allegationInvestigator2 = dbHelper.allegationInvestigator.CreateAllegationInvestigator(careDirectorTeamID, responsibleuserid, allegationID2, allegationdate2);
            Guid allegationInvestigator3 = dbHelper.allegationInvestigator.CreateAllegationInvestigator(careDirectorTeamID, responsibleuserid, allegationID3, allegationdate1);
            Guid allegationInvestigator4 = dbHelper.allegationInvestigator.CreateAllegationInvestigator(careDirectorTeamID, responsibleuserid, allegationID4, allegationdate2);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no Adult Safeguarding record exist
            var adultSafeguardings = dbHelper.AdultSafeguarding.GetAdultSafeguardingByCaseID(caseID);
            Assert.AreEqual(0, adultSafeguardings.Count);

            //validate that no Allegations record exist
            var allegations = dbHelper.allegation.GetAllegationByAdultSafeguardingID(safeguardingID1);
            Assert.AreEqual(0, allegations.Count);
            allegations = dbHelper.allegation.GetAllegationByAdultSafeguardingID(safeguardingID2);
            Assert.AreEqual(0, allegations.Count);

            //validate that no Allegation investigator record exist
            var investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(allegationID1);
            Assert.AreEqual(0, investigators.Count);
            investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(allegationID2);
            Assert.AreEqual(0, investigators.Count);
            investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(allegationID3);
            Assert.AreEqual(0, investigators.Count);
            investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(allegationID4);
            Assert.AreEqual(0, investigators.Count);

        }


        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate one health appointment with the case - " +
            "Create a additional health professional record associated with the health appointment" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24439")]
        public void FileDestructionGDPR_UITestMethod12()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = DateTime.Now.Date;

            //remove all health appointments
            foreach (Guid recordID in dbHelper.healthAppointment.GetHealthAppointmentByHealthProfessionalID(healthprofessionalid, appointmentStartDate))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    dbHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                dbHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //create Health Appointtment 
            Guid MobileTeam1_Teamid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid appointmentDataformId = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid mobile_test_user_1userid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid MobileTestClinicTeam_Teamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = true;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                MobileTeam1_Teamid, _personToDelete, FirstName + " ToDelete", appointmentDataformId, contacttypeid, healthappointmentreasonid, "Assessment", caseID, mobile_test_user_1userid,
                MobileTestClinicTeam_Teamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);


            //Create additional health professional
            Guid healthprofessionalid2 = new Guid("3ab63b6a-5d9e-e911-a2c6-005056926fe4"); //Mobile Test User 2
            Guid additionalProfessionalRecord = dbHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(MobileTeam1_Teamid, healthAppointmentID, healthprofessionalid2, _personToDelete, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no health appointment records exist
            var appointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

            //validate that no additional health professional records exist
            var additionalProfessional = dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointmentID);
            Assert.AreEqual(0, additionalProfessional.Count);



        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate 2 health appointments with the case - " +
            "Create one additional health professional record associated with each health appointment" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24440")]
        public void FileDestructionGDPR_UITestMethod13()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = DateTime.Now.Date;

            //remove all health appointments
            foreach (Guid recordID in dbHelper.healthAppointment.GetHealthAppointmentByHealthProfessionalID(healthprofessionalid, appointmentStartDate))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    dbHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseactionid in dbHelper.caseAction.GetByHealthAppointmentId(recordID))
                    dbHelper.caseAction.DeleteCaseAction(caseactionid);

                dbHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //create Health Appointtment 
            Guid MobileTeam1_Teamid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid appointmentDataformId = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid mobile_test_user_1userid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid MobileTestClinicTeam_Teamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = true;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID1 = dbHelper.healthAppointment.CreateHealthAppointment(
                MobileTeam1_Teamid, _personToDelete, FirstName + " ToDelete", appointmentDataformId, contacttypeid, healthappointmentreasonid, "Assessment", caseID, mobile_test_user_1userid,
                MobileTestClinicTeam_Teamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            //Create additional health professional
            Guid healthprofessionalid2 = new Guid("3ab63b6a-5d9e-e911-a2c6-005056926fe4"); //Mobile Test User 2
            Guid additionalProfessionalRecord1 = dbHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(MobileTeam1_Teamid, healthAppointmentID1, healthprofessionalid2, _personToDelete, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);


            //Create the 2nd health appointment
            startTime = new TimeSpan(10, 10, 0);
            endTime = new TimeSpan(10, 15, 0);
            Guid healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment(
                MobileTeam1_Teamid, _personToDelete, FirstName + " ToDelete", appointmentDataformId, contacttypeid, healthappointmentreasonid, "Assessment", caseID, mobile_test_user_1userid,
                MobileTestClinicTeam_Teamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            //Create the 2nd additional health professional record
            Guid additionalProfessionalRecord2 = dbHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(MobileTeam1_Teamid, healthAppointmentID2, healthprofessionalid2, _personToDelete, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no health appointment records exist
            var appointments = dbHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

            //validate that no additional health professional records exist
            var additionalProfessional = dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointmentID1);
            Assert.AreEqual(0, additionalProfessional.Count);
            additionalProfessional = dbHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointmentID2);
            Assert.AreEqual(0, additionalProfessional.Count);



        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate one Task with the Case - Set the Significant Event option to Yes - " +
            "One Significant Event record should be automatically created and associated with the person" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24441")]
        public void FileDestructionGDPR_UITestMethod14()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //create Task record 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;
            Guid significanteventcategoryid = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4"); //Category 1
            Guid significanteventsubcategoryid = new Guid("0575c209-1b52-e911-a2c5-005056926fe4"); //Sub Cat 1_1
            Guid caseTaskID = dbHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, responsibleuserid, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, _personToDelete, date, caseID, "ToDelete, " + FirstName, "case", IsSignificantEvent, significanteventdate, significanteventcategoryid, significanteventsubcategoryid);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no Task records exist
            var appointments = dbHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate 2 Tasks with the Case - Set the Significant Event option to Yes - " +
            "Two Significant Event record should be automatically created and associated with the person" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24442")]
        public void FileDestructionGDPR_UITestMethod15()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //create Task record 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            DateTime date2 = new DateTime(2020, 5, 21, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;
            Guid significanteventcategoryid = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4"); //Category 1
            Guid significanteventsubcategoryid = new Guid("0575c209-1b52-e911-a2c5-005056926fe4"); //Sub Cat 1_1

            Guid caseTaskID1 = dbHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, responsibleuserid, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, _personToDelete, date1, caseID, "ToDelete, " + FirstName, "case", IsSignificantEvent, significanteventdate, significanteventcategoryid, significanteventsubcategoryid);
            Guid caseTaskID2 = dbHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, responsibleuserid, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, _personToDelete, date2, caseID, "ToDelete, " + FirstName, "case", IsSignificantEvent, significanteventdate, significanteventcategoryid, significanteventsubcategoryid);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no Task records exist
            var appointments = dbHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate one Case Form with the Case - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24443")]
        public void FileDestructionGDPR_UITestMethod16()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //Create Case Form
            Guid documentid = new Guid("B84088FA-3AB2-EA11-A2CD-005056926FE4"); //Mobile - Case Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = DateTime.Now.Date;

            Guid caseFormID = dbHelper.caseForm.CreateCaseForm(OwnerID, _personToDelete, FirstName + " ToDelete", responsibleuserid, caseID, FirstName + " ToDelete", documentid, "Mobile - Case Form", assessmentstatusid, startdate, null, null);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no case form records exist
            var appointments = dbHelper.caseForm.GetCaseFormByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a Community Clinic Case and associate it with the Person - Associate two Case Form with the Case - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24444")]
        public void FileDestructionGDPR_UITestMethod17()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a community Clinic Case record associated with the person
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");//Mobile Test Clinic Team
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("ABC1E00A-43C2-E811-80DC-0050560502CC"); //Waiting List - First Appointment Required
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid administrativecategoryid = new Guid("17ce9b3d-2140-e911-a2c5-005056926fe4"); //NHS Patient
            Guid servicetyperequestedid = new Guid("ad34df09-9e50-e911-a2c5-005056926fe4"); //Advice and Consultation
            Guid dataformid = new Guid("7E82EE28-2EB7-E811-80DC-0050560502CC"); //Community Health Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime requestreceiveddatetime = new DateTime(2020, 7, 2);
            DateTime startdatetime = new DateTime(2020, 7, 3);
            DateTime caseaccepteddatetime = new DateTime(2020, 7, 4);
            string presentingneeddetails = "presenting needs ...";

            Guid caseID = dbHelper.Case.CreateCommunityHealthCaseRecord(careDirectorTeamID, _personToDelete, contactreceivedbyid, communityandclinicteamid, responsibleuserid, casestatusid, contactreasonid, administrativecategoryid, servicetyperequestedid, dataformid, contactsourceid, contactreceiveddatetime, requestreceiveddatetime, startdatetime, caseaccepteddatetime, presentingneeddetails);


            //Create Case Form
            Guid documentid = new Guid("B84088FA-3AB2-EA11-A2CD-005056926FE4"); //Mobile - Case Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate1 = DateTime.Now.Date;
            DateTime startdate2 = DateTime.Now.Date.AddDays(-1);

            Guid caseFormID1 = dbHelper.caseForm.CreateCaseForm(OwnerID, _personToDelete, FirstName + " ToDelete", responsibleuserid, caseID, FirstName + " ToDelete", documentid, "Mobile - Case Form", assessmentstatusid, startdate1, null, null);
            Guid caseFormID2 = dbHelper.caseForm.CreateCaseForm(OwnerID, _personToDelete, FirstName + " ToDelete", responsibleuserid, caseID, FirstName + " ToDelete", documentid, "Mobile - Case Form", assessmentstatusid, startdate2, null, null);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no case record exist
            var cases = dbHelper.Case.GetCasesByPersonID(_personToDelete);
            Assert.AreEqual(0, cases.Count);

            //validate that no case form records exist
            var appointments = dbHelper.caseForm.GetCaseFormByCaseID(caseID);
            Assert.AreEqual(0, appointments.Count);

        }


        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a New Person Record (B) - Create a relationship between person A and person B" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24445")]
        public void FileDestructionGDPR_UITestMethod18()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];


            //Create person record (B) to delete
            Guid _personRelated = dbHelper.person.CreatePersonRecord("", FirstName, "", "Relationship", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);


            //Create the person relationship
            Guid relationshipType = new Guid("aa8eee7b-4899-e811-9962-461ca8e2ff4b"); //Friend
            DateTime startDate = new DateTime(2020, 6, 19);

            Guid relationship1 = dbHelper.personRelationship.CreatePersonRelationship(OwnerID,
                _personToDelete1, FirstName + " ToDelete",
                relationshipType, "Friend",
                _personRelated, FirstName + " Relationship",
                relationshipType, "Friend",
                startDate, "desc ...", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);




            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no relationship exist for the person
            var relationships = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);
            relationships = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personRelated);
            Assert.AreEqual(0, relationships.Count);


        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a New Person Record (B) - Create a New Person Record (C)" +
            "Create a relationship between person A and person B - Create a relationship between person A and person C" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24446")]
        public void FileDestructionGDPR_UITestMethod19()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];


            //Create person record (B) 
            Guid _personRelated1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "Relationship1", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create person record (C) 
            Guid _personRelated2 = dbHelper.person.CreatePersonRecord("", FirstName, "", "Relationship2", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);


            //Create the person relationship
            Guid relationshipType = new Guid("aa8eee7b-4899-e811-9962-461ca8e2ff4b"); //Friend
            DateTime startDate = new DateTime(2020, 6, 19);

            Guid relationship1 = dbHelper.personRelationship.CreatePersonRelationship(OwnerID,
                _personToDelete1, FirstName + " ToDelete",
                relationshipType, "Friend",
                _personRelated1, FirstName + " Relationship1",
                relationshipType, "Friend",
                startDate, "desc ...", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            Guid relationship2 = dbHelper.personRelationship.CreatePersonRelationship(OwnerID,
                _personToDelete1, FirstName + " ToDelete",
                relationshipType, "Friend",
                _personRelated2, FirstName + " Relationship2",
                relationshipType, "Friend",
                startDate, "desc ...", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);




            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no relationship exist for the person
            var relationships = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);
            relationships = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personRelated1);
            Assert.AreEqual(0, relationships.Count);
            relationships = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personRelated2);
            Assert.AreEqual(0, relationships.Count);


        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a New Person MHA Legal Status Record - Create a Court Date and Outcome record linked to the MHA Legal Status" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24447")]
        public void FileDestructionGDPR_UITestMethod20()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];



            //Create MHA Legal Status record
            Guid mhaSection = new Guid("ac2edd91-b994-e911-a2c6-005056926fe4"); //Adhoc Section
            Guid SecurityTestUserAdmin_userid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid mhaLegalStatusID = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(OwnerID, _personToDelete1, mhaSection, SecurityTestUserAdmin_userid, DateTime.Now);


            //Create MHA Court Date Outcome
            dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(OwnerID, mhaLegalStatusID, _personToDelete1, DateTime.Now);




            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);

            System.Threading.Thread.Sleep(3000);

            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no MHA legal status exist for the person
            var relationships = dbHelper.personMHALegalStatus.GetPersonMHALegalStatusByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);

            //validate that no court date outcome exists for the person
            relationships = dbHelper.mhaCourtDateOutcome.GetMHACourtDateOutcomeByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);


        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create Two New Person MHA Legal Status Record - Create two Court Date and Outcome record linked to each MHA Legal Status" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24448")]
        public void FileDestructionGDPR_UITestMethod21()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];



            //Create MHA Legal Status record
            Guid mhaSection = new Guid("ac2edd91-b994-e911-a2c6-005056926fe4"); //Adhoc Section
            Guid SecurityTestUserAdmin_userid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin

            Guid mhaLegalStatusID1 = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(OwnerID, _personToDelete1, mhaSection, SecurityTestUserAdmin_userid, DateTime.Now);
            Guid mhaLegalStatusID2 = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(OwnerID, _personToDelete1, mhaSection, SecurityTestUserAdmin_userid, DateTime.Now);


            //Create MHA Court Date Outcome
            dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(OwnerID, mhaLegalStatusID1, _personToDelete1, DateTime.Now);
            dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(OwnerID, mhaLegalStatusID1, _personToDelete1, DateTime.Now.AddDays(-1));
            dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(OwnerID, mhaLegalStatusID2, _personToDelete1, DateTime.Now);
            dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(OwnerID, mhaLegalStatusID2, _personToDelete1, DateTime.Now.AddDays(-1));




            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no MHA legal status exist for the person
            var relationships = dbHelper.personMHALegalStatus.GetPersonMHALegalStatusByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);

            //validate that no court date outcome exists for the person
            relationships = dbHelper.mhaCourtDateOutcome.GetMHACourtDateOutcomeByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);


        }


        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Service Provision linked to a Cost Per Week record, Financial Assessment, FA Contribution, Person Financial Details, Charges, Charge Descriptions and Charge Totals" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24449")]
        public void FileDestructionGDPR_UITestMethod22()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];



            //Create a new service provision
            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid responsibleuserid = new Guid("C074C7A5-74A9-E911-A2C6-005056926FE4");
            Guid serviceprovisionstatusid = new Guid("D5776E36-2E40-E911-A2C4-0050569231CF");
            Guid serviceelement1id = new Guid("596495AE-E997-E911-A2C6-005056926FE4");
            Guid serviceelement2id = new Guid("E6CEF2DC-4B93-E911-A2C6-005056926FE4");
            Guid financeclientcategoryid = new Guid("2BFB7048-6E5F-E911-A2C5-005056926FE4");
            Guid glcodeid = new Guid("3448E764-F697-E911-A2C6-005056926FE4");
            Guid rateunitid = new Guid("E62BD0B5-952B-E911-AAA3-18037329EFCA");
            Guid serviceprovisionstartreasonid = new Guid("E87DFE17-4C93-E911-A2C6-005056926FE4");
            Guid serviceprovisionendreasonid = new Guid("CEA1462A-4C93-E911-A2C6-005056926FE4");
            Guid purchasingteamid = new Guid("DCC46EAF-EB97-E911-A2C6-005056926FE4");
            Guid serviceprovidedid = new Guid("2763B27E-7ED4-E911-A2C7-005056926FE4");
            Guid providerid = new Guid("732F5735-3CCA-E911-A2C7-005056926FE4");
            Guid authorisedbysystemuserid = new Guid("C074C7A5-74A9-E911-A2C6-005056926FE4");
            Guid placementroomtypeid = new Guid("755766A1-2D40-E911-A2C4-0050569231CF");
            DateTime actualstartdate = new DateTime(2019, 7, 1);
            DateTime actualenddate = new DateTime(2019, 10, 20);
            DateTime authorisationdate = new DateTime(2019, 10, 24);

            Guid serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(ownerid, responsibleuserid, _personToDelete1, serviceprovisionstatusid, serviceelement1id, serviceelement2id,
                financeclientcategoryid, glcodeid, rateunitid, serviceprovisionstartreasonid, serviceprovisionendreasonid, purchasingteamid, serviceprovidedid,
                providerid, authorisedbysystemuserid, placementroomtypeid, actualstartdate, actualenddate, authorisationdate);


            //Crete service provision cost per week record
            Guid costPerWeekID = dbHelper.serviceProvisionCostPerWeek.CreateServiceProvisionCostPerWeek(ownerid, serviceProvisionID, _personToDelete1, actualstartdate, actualenddate, 250m);


            //Create Financial Assessment
            int financialassessmentcategoryid = 1;
            Guid financialassessmentstatusid = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //draft
            Guid chargingruleid = new Guid("12E55931-57AD-E911-A2C6-005056926FE4");
            Guid incomesupporttypeid = new Guid("3d5a5907-d74b-e911-a2c4-0050569231cf");
            Guid authorisedbyid = new Guid("C074C7A5-74A9-E911-A2C6-005056926FE4");
            Guid financialassessmenttypeid = new Guid("d81f2d0b-6788-e911-9bfe-1803731f3ee3");
            DateTime startdate = new DateTime(2019, 10, 14);
            DateTime enddate = new DateTime(2019, 10, 20);

            Guid financialAssessmentID = dbHelper.financialAssessment.CreateFinancialAssessment(
                ownerid, responsibleuserid, _personToDelete1, financialassessmentstatusid, chargingruleid,
                incomesupporttypeid, authorisedbyid, financialassessmenttypeid, startdate, enddate, authorisationdate, financialassessmentcategoryid, 94.75m);


            //create FA Contribution
            Guid contributiontypeid = new Guid("042A3687-B240-E911-A947-989096B30F07");
            Guid recoverymethodid = new Guid("4D4EB331-1B40-E911-A947-989096B30F07");
            Guid debtorbatchgroupingid = new Guid("748013F6-74BE-E911-A2C7-005056926FE4");

            Guid faContributionID = dbHelper.faContribution.CreateFAContribution(financialAssessmentID, serviceProvisionID, _personToDelete1, ownerid, contributiontypeid, recoverymethodid, debtorbatchgroupingid, _personToDelete1, "person", " ToDelete", startdate, enddate);



            //create person financial detail 1
            Guid financialDetailID1 = new Guid("d9658c37-59e4-e911-a2c7-005056926fe4"); //Disablement Pension - Variable
            Guid FrequencyOfReceiptId = new Guid("2D0AE4B7-5734-E911-80DC-0050560502CC"); //Per Week
            int financialDetailType1 = 3; //Benefit & Income
            Guid personFinancialDetailID1 = dbHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, _personToDelete1, financialDetailID1, FrequencyOfReceiptId, startdate, enddate, financialDetailType1, 321.55m, true, false, false, false);

            Guid financialDetailID2 = new Guid("b77d63e9-eabd-e911-a2c7-005056926fe4"); //Savings Account
            int financialDetailType2 = 2; //Asset
            Guid personFinancialDetailID2 = dbHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, _personToDelete1, financialDetailID2, FrequencyOfReceiptId, startdate, enddate, financialDetailType2, 3000m, true, false, false, false);



            //update the FA status to Ready for Authorisation
            financialassessmentstatusid = new Guid("783AACF4-B286-E911-9AFD-D89EF34C460F"); //Ready for Authorisation
            dbHelper.financialAssessment.UpdateFinancialAssessmentStatus(financialAssessmentID, financialassessmentstatusid);

            //update the FA status to Authorized
            financialassessmentstatusid = new Guid("655977FB-B286-E911-9AFD-D89EF34C460F"); //Authorized
            dbHelper.financialAssessment.UpdateFinancialAssessmentStatus(financialAssessmentID, financialassessmentstatusid);


            //create financial assessment charge
            Guid scheduletypeid = new Guid("31BC2FBA-AB41-E911-9AFB-D89EF34C460F");
            Guid financialAssessmentChargeID1 = dbHelper.financialAssessmentCharge.CreateFinancialAssessmentCharge(OwnerID, financialAssessmentID, scheduletypeid, startdate, enddate, 100m, 100m, 100m);


            //create a new financial assessment charge detail 
            Guid financialAssessmentChargeDetailID1 = dbHelper.financialAssessmentChargeDetail.CreateFinancialAssessmentChargeDetail(ownerid, financialAssessmentID, financialAssessmentChargeID1, FrequencyOfReceiptId, 90m, 5m, 95m, 1);
            Guid financialAssessmentChargeDetailID2 = dbHelper.financialAssessmentChargeDetail.CreateFinancialAssessmentChargeDetail(ownerid, financialAssessmentID, financialAssessmentChargeID1, FrequencyOfReceiptId, 30m, 7m, 40m, 2);

            // create a new financial assessment charge total
            Guid financialAssessmentChargeTotalID1 = dbHelper.financialAssessmentChargeTotal.CreateFinancialAssessmentChargeTotal(ownerid, financialAssessmentID, financialAssessmentChargeID1, 1, 95m);
            Guid financialAssessmentChargeTotalID2 = dbHelper.financialAssessmentChargeTotal.CreateFinancialAssessmentChargeTotal(ownerid, financialAssessmentID, financialAssessmentChargeID1, 2, 30m);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no serviceprovision exist for the person
            var records = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personToDelete1);
            Assert.AreEqual(0, records.Count);

            //validate that no financial assessments exists for the person
            records = dbHelper.financialAssessment.GetFinancialAssessmentByPersonID(_personToDelete1.ToString());
            Assert.AreEqual(0, records.Count);

        }


        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Service Provision linked to a Cost Per Week record, finance transaction, finance invoice, finance invoice batch, finance extract" +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24450")]
        public void FileDestructionGDPR_UITestMethod23()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;


            //Create person record (A) to delete
            Guid _personToDelete1 = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete1, "personnumber");
            int _personToDelete1Number = (int)fields["personnumber"];



            //Create a new service provision
            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid responsibleuserid = new Guid("C074C7A5-74A9-E911-A2C6-005056926FE4");
            Guid serviceprovisionstatusid = new Guid("D5776E36-2E40-E911-A2C4-0050569231CF");
            Guid serviceelement1id = new Guid("596495AE-E997-E911-A2C6-005056926FE4");
            Guid serviceelement2id = new Guid("E6CEF2DC-4B93-E911-A2C6-005056926FE4");
            Guid financeclientcategoryid = new Guid("2BFB7048-6E5F-E911-A2C5-005056926FE4");
            Guid glcodeid = new Guid("3448E764-F697-E911-A2C6-005056926FE4");
            Guid rateunitid = new Guid("E62BD0B5-952B-E911-AAA3-18037329EFCA");
            Guid serviceprovisionstartreasonid = new Guid("E87DFE17-4C93-E911-A2C6-005056926FE4");
            Guid serviceprovisionendreasonid = new Guid("CEA1462A-4C93-E911-A2C6-005056926FE4");
            Guid purchasingteamid = new Guid("DCC46EAF-EB97-E911-A2C6-005056926FE4");
            Guid serviceprovidedid = new Guid("2763B27E-7ED4-E911-A2C7-005056926FE4");
            Guid providerid = new Guid("732F5735-3CCA-E911-A2C7-005056926FE4");
            Guid authorisedbysystemuserid = new Guid("C074C7A5-74A9-E911-A2C6-005056926FE4");
            Guid placementroomtypeid = new Guid("755766A1-2D40-E911-A2C4-0050569231CF");
            DateTime actualstartdate = new DateTime(2019, 7, 1);
            DateTime actualenddate = new DateTime(2019, 10, 20);
            DateTime authorisationdate = new DateTime(2019, 10, 24);

            Guid serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(ownerid, responsibleuserid, _personToDelete1, serviceprovisionstatusid, serviceelement1id, serviceelement2id,
                financeclientcategoryid, glcodeid, rateunitid, serviceprovisionstartreasonid, serviceprovisionendreasonid, purchasingteamid, serviceprovidedid,
                providerid, authorisedbysystemuserid, placementroomtypeid, actualstartdate, actualenddate, authorisationdate);


            //Crete service provision cost per week record
            dbHelper.serviceProvisionCostPerWeek.CreateServiceProvisionCostPerWeek(ownerid, serviceProvisionID, _personToDelete1, actualstartdate, actualenddate, 250m);



            //Create Finance Extract
            Guid extractnameid = new Guid("462EFF11-F543-E911-9AFB-D89EF34C460F");
            Guid financeextractsetupid = new Guid("030A2BA0-2C72-E911-A2C5-005056926FE4");
            DateTime runon = new DateTime(2019, 1, 1);
            DateTime completedon = new DateTime(2019, 1, 1);
            DateTime originalrunon = new DateTime(2019, 1, 1);

            Guid financeExtract1 = dbHelper.financeExtract.CreateFinanceExtract(ownerid, extractnameid, financeextractsetupid, runon, completedon, originalrunon, "2019/2020", 10m, 11m, 12m, 1m, 0, 1, 1, 2, 2, 2, 5, 29);



            //Create finance Invoice Batch
            Guid FinanceInvoiceBatchSetupId = new Guid("2FC972C4-9C84-EA11-A2CD-005056926FE4");
            Guid PaymentTypeId = new Guid("9CD7FDE2-4429-E911-80DC-0050560502CC");
            Guid ProviderBatchGroupingId = new Guid("EC4FEB4F-8C83-E911-A2C5-005056926FE4");
            DateTime RunOnTime = new DateTime(2019, 1, 1);
            DateTime PeriodStartDate = new DateTime(2019, 1, 1);
            DateTime PeriodEndDate = new DateTime(2019, 1, 5);

            Guid financeInvoiceBatch1 = dbHelper.financeInvoiceBatch.CreateFinanceInvoiceBatch(ownerid, FinanceInvoiceBatchSetupId, serviceelement1id, PaymentTypeId, ProviderBatchGroupingId, RunOnTime, PeriodStartDate, PeriodEndDate, 7672, 2, 2, 1, 1, 249.97m, 254.97m, 5.00m);



            //create finance invoice
            Guid invoicebatchid = new Guid("E4332E37-848C-EA11-A2CD-005056926FE4");
            Guid invoicestatusid = new Guid("9AB5E21A-7607-EA11-A2C9-0050569231CF");
            Guid providerorpersonid = new Guid("732F5735-3CCA-E911-A2C7-005056926FE4");
            string providerorpersonidtablename = "provider";
            string providerorpersonidname = "Tulip Home Care Services";
            string creditorreferencenumber = "P.CN.001";
            DateTime invoicedate = new DateTime(2019, 1, 1);
            DateTime transactionsupto = new DateTime(2019, 1, 5);
            decimal netamount = 249.97m;
            decimal vatamount = 5.00m;
            decimal grossamount = 254.97m;
            Guid financeInvoiceID = dbHelper.financeInvoice.CreateFinanceInvoice(ownerid, invoicebatchid, invoicestatusid, serviceelement1id, PaymentTypeId, financeExtract1, providerorpersonid, providerorpersonidtablename, providerorpersonidname, creditorreferencenumber, invoicedate, transactionsupto, netamount, vatamount, grossamount);


            //create finance transaction
            Guid vatcodeid = new Guid("B075805C-5A29-E911-80DC-0050560502CC");
            Guid financeinvoicebatchsetupid = new Guid("2FC972C4-9C84-EA11-A2CD-005056926FE4");
            Guid paymenttypecodeid = new Guid("9CD7FDE2-4429-E911-80DC-0050560502CC");
            DateTime startdate = new DateTime(2019, 1, 1);
            DateTime enddate = new DateTime(2019, 1, 5);
            decimal totalunits = 0.00m;
            string glcode = "phc -80023";
            string vatreference = "E20190701";
            string invoiceno = "S1243";

            Guid financeTransaction = dbHelper.financeTransaction.CreateFinanceTransaction(ownerid, _personToDelete1, vatcodeid, serviceProvisionID, providerid, serviceelement1id,
                rateunitid, financeinvoicebatchsetupid, serviceelement2id, financeclientcategoryid, paymenttypecodeid, financeInvoiceID, invoicestatusid,
                financeInvoiceBatch1, financeExtract1, startdate, enddate, netamount, vatamount, grossamount, totalunits, glcode, vatreference, invoiceno, _personToDelete1Number);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDelete1Number.ToString()).TapSearchButton().SelectResultElement(_personToDelete1.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete1, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no MHA legal status exist for the person
            var relationships = dbHelper.personMHALegalStatus.GetPersonMHALegalStatusByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);

            //validate that no court date outcome exists for the person
            relationships = dbHelper.mhaCourtDateOutcome.GetMHACourtDateOutcomeByPersonID(_personToDelete1);
            Assert.AreEqual(0, relationships.Count);


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10528

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Draft email and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24451")]
        public void FileDestructionGDPR_UITestMethod24()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid systemUserId = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //SecurityTestUserAdmin
            Guid regardingSystemUserId = new Guid("30FF227C-48C7-EA11-A2CD-005056926FE4"); //Aaron Kirk

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a new draft email
            Guid emailid = dbHelper.email.CreateEmail(OwnerID, _personToDelete, systemUserId, systemUserId, "Security Test User Admin", "systemuser", _personToDelete, "person", FirstName + " ToDelete", "Email Test CDV6-10528", "...", 1);

            //Create email to
            dbHelper.emailTo.CreateEmailTo(emailid, regardingSystemUserId, "systemuser", "Aaron Kirk");



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no email record exists
            var emailFields = dbHelper.email.GetEmailByID(emailid, "personid");
            Assert.AreEqual(0, emailFields.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Pending Send email and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24452")]
        public void FileDestructionGDPR_UITestMethod25()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid systemUserId = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //SecurityTestUserAdmin
            Guid regardingSystemUserId = new Guid("30FF227C-48C7-EA11-A2CD-005056926FE4"); //Aaron Kirk

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];

            //create a new draft email
            Guid emailid = dbHelper.email.CreateEmail(OwnerID, _personToDelete, systemUserId, systemUserId, "Security Test User Admin", "systemuser", _personToDelete, "person", FirstName + " ToDelete", "Email Test CDV6-10528", "...", 4);

            //Create email to
            dbHelper.emailTo.CreateEmailTo(emailid, regardingSystemUserId, "systemuser", "Aaron Kirk");



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no email record exists
            var emailFields = dbHelper.email.GetEmailByID(emailid, "personid");
            Assert.AreEqual(0, emailFields.Count);
        }

        [Description("Jira Issue: https://advancedcsg.atlassian.net/browse/CDV6-4742 - " +
            "Create a New Person Record (A) - Create a new Website User record and associate it with the Person - " +
            "Create a new File Destruction record (for the person) and approve it - " +
            "Execute the File Destruction Schedule Job - " +
            "Validate that the person record is deleted after executing the job (and all related records)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24453")]
        public void FileDestructionGDPR_UITestMethod26()
        {
            Guid surrogatePersonID = new Guid("2cf6bc97-a4f2-ea11-a2cd-005056926fe4"); //Ms Joana Joakina - 505532
            string surrogatePersonNumber = "505532";

            //delete any file destruction for the person used as a surrogate
            foreach (Guid fileDestructionID in dbHelper.fileDestruction.GetFileDestructionByDefaultRecordId(surrogatePersonID))
                dbHelper.fileDestruction.DeleteFileDestruction(fileDestructionID);

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;
            Guid careDirectorTeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            Guid systemUserId = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //SecurityTestUserAdmin
            Guid regardingSystemUserId = new Guid("30FF227C-48C7-EA11-A2CD-005056926FE4"); //Aaron Kirk

            //Create person record to delete
            Guid _personToDelete = dbHelper.person.CreatePersonRecord("", FirstName, "", "ToDelete", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);
            var fields = dbHelper.person.GetPersonById(_personToDelete, "personnumber");
            int _personToDeleteNumber = (int)fields["personnumber"];



            //Create a new website user record
            var websiteID = new Guid("1fcb4bf9-0b24-eb11-a2cd-005056926fe4"); //Automation - Web Site 09
            var securityProfile = new Guid("f8a59cae-2dbe-eb11-a323-005056926fe4"); //CW Full Access
            var websiteUserID = dbHelper.websiteUser.CreateWebsiteUser(websiteID, FirstName + " ToDelete", FirstName + "@mail.com", "qwertyuiop", true, 1, _personToDelete, "person", FirstName + " ToDelete", securityProfile);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickFileDestructionAreaButton()
                .ClickFileDestructionGDPRButton();

            fileDestructionGDPRPage
                .WaitForFileDestructionGDPRPageToLoad()
                .ClickAddButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .InsertScheduleDateForDestruction_Date(DateTime.Now.ToString("dd/MM/yyyy"))
                .InsertScheduleDateForDestruction_Time("00:30")
                .ClickRecordsToBeDeletedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personToDeleteNumber.ToString()).TapSearchButton().SelectResultElement(_personToDelete.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSurrogateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(surrogatePersonNumber).TapSearchButton().SelectResultElement(surrogatePersonID.ToString());

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickFirstApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad()
                .ClickSecondApprovedByButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("This action will approve the destruction of this record. Once a record has been destroyed it cannot be retrieved. To continue click OK.")
                .TapOKButton();

            fileDestructionGDPRRecordPage
                .WaitForFileDestructionGDPRRecordPageToLoad();




            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "File Destructions Job" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(FileDestructionScheduleJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(FileDestructionScheduleJob);



            //validate that the person is no longer present in the DB
            fields = dbHelper.person.GetPersonById(_personToDelete, "personid");
            Assert.AreEqual(0, fields.Count);

            //validate that no email record exists
            var emailFields = dbHelper.websiteUser.GetByID(websiteUserID, "ProfileId");
            Assert.AreEqual(0, emailFields.Count);
        }

        #endregion

    }


}
