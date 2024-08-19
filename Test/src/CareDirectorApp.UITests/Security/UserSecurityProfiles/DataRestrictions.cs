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
    public class DataRestrictions : TestBase
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

            //navigate to the settings page
            mainMenu.NavigateToHomePage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

        }


        #region Records owned by the user Team

        [Property("JiraIssueID", "CDV6-4294")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Allow Users' data restriction - " +
            "User is NOT in the list of allowed users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - " +
            "Users should NOT be able to View the record (record has 'Allow User' data restriction but the user do not belong to the list of allowed users)")]
        public void DataRestrictions_TestMethod001()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction");

        }

        [Property("JiraIssueID", "CDV6-4295")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Allow Users' data restriction - " +
            "User is in the list of allowed users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record - " +
            "Users should be able to View the record (User belongs to the list of allowed users)")]
        public void DataRestrictions_TestMethod002()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("7a2f2b7c-7bd2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (2)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction")
                .ValidateSubjectFieldText("Task - Data Restriction");

        }

        [Property("JiraIssueID", "CDV6-4296")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Allow Team' data restriction - " +
            "User Team is NOT in the list of allowed Teams - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - " +
            "Users should NOT be able to View the record (record has 'Allow User' data restriction but the user do not belong to the list of allowed users)")]
        public void DataRestrictions_TestMethod003()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction");

        }

        [Property("JiraIssueID", "CDV6-4297")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Allow Team' data restriction - " +
            "User Team is in the list of allowed Teams - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record - " +
            "Users should be able to View the record (User Team belong to the list of allowed Teams)")]
        public void DataRestrictions_TestMethod004()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("e27191a2-81d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (3)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction")
                .ValidateSubjectFieldText("Task - Data Restriction");

        }

        [Property("JiraIssueID", "CDV6-4298")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Deny User' data restriction - " +
            "User is NOT in the List of Denied Users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record- " +
            "Users should be able to View the record (Record has a 'Deny User' data restriction but the user do not belong to that list)")]
        public void DataRestrictions_TestMethod005()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("50bddef9-82d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Deny User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction")
                .ValidateSubjectFieldText("Task - Data Restriction");

        }

        [Property("JiraIssueID", "CDV6-4299")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User team must be the owner of a Person record - " +
            "Person record has a Task associated that belongs to the user team - " +
            "Record has 'Deny User' data restriction - " +
            "User is in the List of Denied Users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - " +
            "Users should NOT be able to View the record (Record has a 'Deny User' data restriction and the user belong to the list of restricted users)")]
        public void DataRestrictions_TestMethod006()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("cfd1d1b3-78d2-ea11-a2cd-005056926fe4"); //Task - Data Restriction


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("bb10f098-83d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Deny User (2)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction");

        }


        #endregion

        #region Records owned by another Team

        [Property("JiraIssueID", "CDV6-4300")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User Belongs to a Team (A) - " +
            "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
            "Team (A) must be the owner of a Person record - " +
            "Person record has a Case Note associated that belongs to another Team (B) - " +
            "Case Note record is shared with Team (A) with full permissions - " +
            "Record has 'Allow Users' data restriction - " +
            "User is NOT in the list of allowed users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Users should NOT be able to View the record")]
        public void DataRestrictions_TestMethod007()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction and Share");

        }

        [Property("JiraIssueID", "CDV6-4301")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User Belongs to a Team (A) - " +
            "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
            "Team (A) must be the owner of a Person record - " +
            "Person record has a Case Note associated that belongs to another Team (B) - " +
            "Case Note record is shared with Team (A) with full permissions - " +
            "Record has 'Allow Users' data restriction - " +
            "User is in the list of allowed users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record - Users should be able to View the record")]
        public void DataRestrictions_TestMethod008()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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

            //add the security profiles to the team
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("7a2f2b7c-7bd2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (2)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction and Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction and Share")
                .ValidateSubjectFieldText("Task - Data Restriction and Share");

        }

        [Property("JiraIssueID", "CDV6-4302")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User Belongs to a Team (A) - " +
            "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
            "Team (A) must be the owner of a Person record - " +
            "Person record has a Case Note associated that belongs to another Team (B) - " +
            "Case Note record is shared with Team (A) with full permissions - " +
            "Record has 'Allow Team' data restriction - " +
            "User Team is NOT in the list of allowed Teams - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Users should NOT be able to View the record")]
        public void DataRestrictions_TestMethod009()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction and Share");

        }

        [Property("JiraIssueID", "CDV6-4303")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User Belongs to a Team (A) - " +
            "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
            "Team (A) must be the owner of a Person record - " +
            "Person record has a Case Note associated that belongs to another Team (B) - " +
            "Case Note record is shared with Team (A) with full permissions - " +
            "Record has 'Allow Team' data restriction - " +
            "User Team is in the list of allowed Teams - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record - Users should be able to View the record")]
        public void DataRestrictions_TestMethod010()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("e27191a2-81d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (3)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction and Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction and Share")
                .ValidateSubjectFieldText("Task - Data Restriction and Share");

        }

        [Property("JiraIssueID", "CDV6-4304")]
        [Test]
        [Description("UI Test for Sharing Records - " +
            "User Belongs to a Team (A) - " +
            "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
            "Team (A) must be the owner of a Person record - " +
            "Person record has a Case Note associated that belongs to another Team (B) - " +
            "Case Note record is shared with Team (A) with full permissions - " +
            "Record has 'Deny User' data restriction - " +
            "User is NOT in the List of Denied Users - " +
            "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Open the Case Note record - Users should be able to View the record")]
        public void DataRestrictions_TestMethod011()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("50bddef9-82d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Deny User (1)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .TapOnRecord("Task - Data Restriction and Share");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task - Data Restriction and Share")
                .ValidateSubjectFieldText("Task - Data Restriction and Share");

        }

        [Property("JiraIssueID", "CDV6-4305")]
        [Test]
        [Description("UI Test for Sharing Records - " +
           "User Belongs to a Team (A) - " +
           "Team (A) should have a 'Edit Secur. Prof. - Team' security profiles for the 'Tasks' Business Object - " +
           "Team (A) must be the owner of a Person record - " +
           "Person record has a Case Note associated that belongs to another Team (B) - " +
           "Case Note record is shared with Team (A) with full permissions - " +
           "Record has 'Deny User' data restriction - " +
           "User is in the List of Denied Users - " +
           "Access the People page - User Open a Person Record - Navigate to the Person Case Notes related item - Users should NOT be able to View the record")]
        public void DataRestrictions_TestMethod012()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid taskID = new Guid("c9c608e7-15d3-ea11-a2cd-005056926fe4"); //Task - Data Restriction and Share


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


            //update the data restriction to the task record
            Guid dataRestrictionID = new Guid("bb10f098-83d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Deny User (2)
            PlatformServicesHelper.task.UpdateDataRestriction(taskID, dataRestrictionID);



            //Sync the app (necessary to Logout and Login again)
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
                .ValidateRecordNotPresent("Task - Data Restriction and Share");

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
