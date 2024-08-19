using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Collections.Generic;

namespace CareDirectorApp.UITests.Security
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [Category("Mobile_Security_Online")]
    public class SharedRecords : TestBase
    {
        static UIHelper uIHelper;

     

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("SecurityTestUserAdmin", "Passw0rd_!");

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
                   .InsertUserName("MobileTestUserSecurity")
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
                    .InsertUserName("MobileTestUserSecurity")
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

            //close the lookup popup if it is open
            lookupPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the home page
            mainMenu.NavigateToHomePage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

        }







        #region Sharing records with a User


        #region User has Security Profiles


        #region Sharing record with View Access


        [Property("JiraIssueID", "CDV6-4266")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - Team' security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person Task record with test User (A) - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod001()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share
            

            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4267")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person Task record with test User (A) - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod002()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        #endregion

        #region Sharing record with View and Edit Access

        [Property("JiraIssueID", "CDV6-4268")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - BU'  security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod003()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4269")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Edit the description field - tap on the save button - validate that the record field is saved")]
        public void SharedRecords_TestMethod004()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");

            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            var fields = PlatformServicesHelper.task.GetTaskByID(taskID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4270")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "validate that the delete button is not displayed")]
        public void SharedRecords_TestMethod005()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForDeleteButtonNotVisible();
            
        }

        #endregion

        #region Sharing record with View and Delete Access

        [Property("JiraIssueID", "CDV6-4271")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person case note record with test User (A) - Record is shared with View and Delete Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Validate that the save and delete buttons are not displayed")]
        public void SharedRecords_TestMethod006()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();

        }

        [Property("JiraIssueID", "CDV6-4272")]
        [Test]
        [Description("UI Test for Sharing Records - " +
           "User (A) has 'Edit Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
           "User (B) shares a person case note record with test User (A) - Record is shared with View and Delete Access - " +
           "User (A) Team is NOT the owner of the Task record - " +
           "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
           "Validate that the save and save and close buttons are not displayed - Validate that the delete is NOT displayed")]
        public void SharedRecords_TestMethod007()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()

                .WaitForDeleteButtonNotVisible();

        }

        #endregion


        #endregion


        #region Team has security profiles


        #region Sharing record with View Access


        [Property("JiraIssueID", "CDV6-4273")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person Task record with test User (A) - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod008()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the team
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4274")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person Task record with test User (A) - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod009()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        #endregion

        #region Sharing record with View and Edit Access

        [Property("JiraIssueID", "CDV6-4275")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - BU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod010()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4276")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Edit the description field - tap on the save button - validate that the record field is saved")]
        public void SharedRecords_TestMethod011()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");

            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            var fields = PlatformServicesHelper.task.GetTaskByID(taskID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4277")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "validate that the delete button is not displayed")]
        public void SharedRecords_TestMethod012()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForDeleteButtonNotVisible();

        }

        #endregion

        #region Sharing record with View and Delete Access

        [Property("JiraIssueID", "CDV6-4278")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person case note record with test User (A) - Record is shared with View and Delete Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Validate that the save and delete buttons are not displayed")]
        public void SharedRecords_TestMethod013()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();

        }

        [Property("JiraIssueID", "CDV6-4279")]
        [Test]
        [Description("UI Test for Sharing Records - " +
           "Team has 'Edit Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
           "User (B) shares a person case note record with test User (A) - Record is shared with View and Delete Access - " +
           "User (A) Team is NOT the owner of the Task record - " +
           "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
           "Validate that the save and save and close buttons are not displayed - Validate that the delete is NOT displayed")]
        public void SharedRecords_TestMethod014()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", userId, "systemuser", "Mobile Test User Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")

                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()

                .WaitForDeleteButtonNotVisible();

        }

        #endregion


        #endregion


        #endregion




        #region Sharing records with a Team


        #region User has Security Profiles


        #region Sharing record with View Access


        [Property("JiraIssueID", "CDV6-4280")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - Team' security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person Task record with User (A) Team - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod015()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user team
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, false, false);





            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4281")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person Task record with User (A) team - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod016()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        #endregion

        #region Sharing record with View and Edit Access

        [Property("JiraIssueID", "CDV6-4282")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - BU'  security profiles for the 'Person Tasks' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod017()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4283")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Edit the description field - tap on the save button - validate that the record field is saved")]
        public void SharedRecords_TestMethod018()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");

            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            var fields = PlatformServicesHelper.task.GetTaskByID(taskID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4284")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "validate that the delete button is not displayed")]
        public void SharedRecords_TestMethod019()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForDeleteButtonNotVisible();

        }

        #endregion

        #region Sharing record with View and Delete Access

        [Property("JiraIssueID", "CDV6-4285")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User (A) has 'View Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person case note record with test User (A) Team - Record is shared with View and Delete Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Validate that the save and delete buttons are not displayed")]
        public void SharedRecords_TestMethod020()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();

        }

        [Property("JiraIssueID", "CDV6-4286")]
        [Test]
        [Description("UI Test for Sharing Records - " +
           "User (A) has 'Edit Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
           "User (B) shares a person case note record with test User (A) Team - Record is shared with View and Delete Access - " +
           "User (A) Team is NOT the owner of the Task record - " +
           "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
           "Validate that the save and save and close buttons are not displayed - Validate that the delete is NOT displayed")]
        public void SharedRecords_TestMethod021()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")

                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()

                .WaitForDeleteButtonNotVisible();

        }

        #endregion


        #endregion

        #region Team has Security Profiles

        #region Sharing record with View Access


        [Property("JiraIssueID", "CDV6-4287")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person Task record with test User (A) Team - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod022()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the team
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4288")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person Task record with test User (A) Team - Record is shared with View Access only - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod023()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        #endregion

        #region Sharing record with View and Edit Access

        [Property("JiraIssueID", "CDV6-4289")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - BU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "APP should display the Person Task record - User should NOT be able to edit or delete the record")]
        public void SharedRecords_TestMethod024()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .WaitForDeleteButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForSaveButtonNotVisible(); ;
        }

        [Property("JiraIssueID", "CDV6-4290")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Edit the description field - tap on the save button - validate that the record field is saved")]
        public void SharedRecords_TestMethod025()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");

            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .ValidateSubjectFieldText("Task Share")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            var fields = PlatformServicesHelper.task.GetTaskByID(taskID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4291")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'Edit Secur. Prof. - Team'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person task record with test User (A) Team - Record is shared with View and Edit Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "validate that the delete button is not displayed")]
        public void SharedRecords_TestMethod026()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, true, false, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForDeleteButtonNotVisible();

        }

        #endregion

        #region Sharing record with View and Delete Access

        [Property("JiraIssueID", "CDV6-4292")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team has 'View Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
            "User (B) shares a person case note record with test User (A) Team - Record is shared with View and Delete Access - " +
            "User (A) Team is NOT the owner of the Task record - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
            "Validate that the save and delete buttons are not displayed")]
        public void SharedRecords_TestMethod027()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();

        }

        [Property("JiraIssueID", "CDV6-4293")]
        [Test]
        [Description("UI Test for Sharing Records - " +
           "Team has 'Edit Secur. Prof. - PCBU'  security profiles for the 'Person Case Notes' Business Object - " +
           "User (B) shares a person case note record with test User (A) Team - Record is shared with View and Delete Access - " +
           "User (A) Team is NOT the owner of the Task record - " +
           "Access the People page - User Open a Person Record - Navigate to the Person Tasks related item - Open the Task record - " +
           "Validate that the save and save and close buttons are not displayed - Validate that the delete is NOT displayed")]
        public void SharedRecords_TestMethod028()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("d369b679-4ed2-ea11-a2cd-005056926fe4"); //Task Share


            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);



            //remove all shares from the Task record
            foreach (var shareID in PlatformServicesHelper.recordLevelAccess.GetRecordLevelAccessByRecordID(taskID))
                PlatformServicesHelper.recordLevelAccess.DeleteRecordLevelAccess(shareID);

            //share the record with mobile user
            PlatformServicesHelper.recordLevelAccess.CreateRecordLevelAccess(taskID, "task", "Task Share", teamID, "team", "Mobile Team Security", true, false, true, false);




            //Sync the app
            mainMenu
               .Logout();

            //login againt
            loginPage
                   .WaitForBasicLoginPageToLoad()
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

            //if the offline mode warning is displayed, then close it
            warningPopup.TapNoButtonIfPopupIsOpen();

            //wait for the homepage to load
            homePage
                .WaitForHomePageToLoad();




            //go back to the people page
            mainMenu.
                NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Juliana Arlinghton", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Juliana Arlinghton")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task Share")

                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible()

                .WaitForDeleteButtonNotVisible();

        }

        #endregion

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
