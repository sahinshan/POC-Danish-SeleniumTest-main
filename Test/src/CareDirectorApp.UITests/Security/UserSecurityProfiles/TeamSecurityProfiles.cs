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
    public class TeamSecurityProfiles : TestBase
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
            mainMenu.NavigateToSettingsPage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

        }



        #region Mobile Security - Team Sec. Profiles



        #region View Secur. Prof. - Team

        [Property("JiraIssueID", "CDV6-4231")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team has a 'View Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Validate that the person case note record is visible")]
        public void TeamSecurityProfiles_TestMethod1()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 01")
                .ValidateAddNewRecordButtonNotVisible();
        }

        [Property("JiraIssueID", "CDV6-4232")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team has a 'View Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to the user team - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Validate that the record is displayed but it is not editable")]
        public void TeamSecurityProfiles_TestMethod2()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }

        [Property("JiraIssueID", "CDV6-4233")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team has a 'View Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team (user do not belong to that team) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Person case note record that belongs to a different team must not be visible to the user")]
        public void TeamSecurityProfiles_TestMethod3()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordNotPresent("Case Note 05");
        }

        #endregion

        #region View Secur. Prof. - BU


        [Property("JiraIssueID", "CDV6-4234")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - Validate that the Add button is not displayed")]
        public void TeamSecurityProfiles_TestMethod4()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 01")
                .ValidateAddNewRecordButtonNotVisible();
        }


        [Property("JiraIssueID", "CDV6-4235")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different Business Unit (user do not belong to that team or Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is NOT displayed")]
        public void TeamSecurityProfiles_TestMethod5()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordNotPresent("Case Note 02");

        }


        [Property("JiraIssueID", "CDV6-4236")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed")]
        public void TeamSecurityProfiles_TestMethod6()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 03");

        }


        [Property("JiraIssueID", "CDV6-4237")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Open the case note record - Validate that the record is displayed; save buttons should not be displayed; delete button should not be displayed")]
        public void TeamSecurityProfiles_TestMethod7()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }


        [Property("JiraIssueID", "CDV6-4238")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Open the case note record - Validate that the record is displayed; save buttons should not be displayed; delete button should not be displayed")]
        public void TeamSecurityProfiles_TestMethod8()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .TapOnRecord("Case Note 03");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 03")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }


        #endregion

        #region View Secur. Prof. - PCBU


        [Property("JiraIssueID", "CDV6-4239")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - Validate that the Add button is not displayed")]
        public void TeamSecurityProfiles_TestMethod9()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 01")
                .ValidateAddNewRecordButtonNotVisible();
        }


        [Property("JiraIssueID", "CDV6-4240")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed")]
        public void TeamSecurityProfiles_TestMethod10()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 03");
        }


        [Property("JiraIssueID", "CDV6-4241")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user belongs to a parent of that Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed")]
        public void TeamSecurityProfiles_TestMethod11()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 04");
        }


        [Property("JiraIssueID", "CDV6-4242")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different Business Unit (user do not belong to that team or Business Unit or a parent of the business unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed")]
        public void TeamSecurityProfiles_TestMethod12()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordNotPresent("Case Note 02");
        }


        [Property("JiraIssueID", "CDV6-4243")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user belongs to a parent of that Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Tap on the case note record" +
            "Validate that the person case note record is displayed; save buttons are not visible; delete button is not visible")]
        public void TeamSecurityProfiles_TestMethod13()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 04")
                .TapOnRecord("Case Note 04");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 04")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region View Secur. Prof. - Org


        [Property("JiraIssueID", "CDV6-4244")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the Person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - Validate that the Add button is not displayed")]
        public void TeamSecurityProfiles_TestMethod14()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 01")
                .ValidateAddNewRecordButtonNotVisible();
        }


        [Property("JiraIssueID", "CDV6-4245")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - ")]
        public void TeamSecurityProfiles_TestMethod15()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 03");
        }


        [Property("JiraIssueID", "CDV6-4246")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user belongs to a parent of that Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - ")]
        public void TeamSecurityProfiles_TestMethod16()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 04");
        }


        [Property("JiraIssueID", "CDV6-4247")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different Business Unit (user do not belong to that team or Business Unit or a parent of the business unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Validate that the person case note record is displayed - ")]
        public void TeamSecurityProfiles_TestMethod17()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 02");
        }


        [Property("JiraIssueID", "CDV6-4248")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different Business Unit (user do not belong to that team or Business Unit or a parent of the business unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Tap on the case note record" +
            "Validate that the person case note record is displayed; save buttons are not visible; delete button is not visible")]
        public void TeamSecurityProfiles_TestMethod18()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateRecordPresent("Case Note 02")
                .TapOnRecord("Case Note 02");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 02")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }

        #endregion



        #region Edit Secur. Prof. - Team


        [Property("JiraIssueID", "CDV6-4249")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the record - Tap on the save and close button - Validate that the record is correctly saved")]
        public void TeamSecurityProfiles_TestMethod20()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("0f0bfd4d-01cd-ea11-a2cd-005056926fe4"); //Case Note 01
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 01")
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }


        [Property("JiraIssueID", "CDV6-4250")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page -  " +
            "Validate that add new record button is displayed - Tap on the Add Button - Validate that the user is redirected to the new record page")]
        public void TeamSecurityProfiles_TestMethod21()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .TapOnAddNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES");
        }


        [Property("JiraIssueID", "CDV6-4251")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Team' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the record - Tap on the save and close button - Validate that the record is correctly saved")]
        public void TeamSecurityProfiles_TestMethod22()
        {
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Team Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 03")
                .TapOnRecord("Case Note 03");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 03")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
                
        }

        #endregion

        #region Edit Secur. Prof. - BU

        [Property("JiraIssueID", "CDV6-4252")]
        [Test]
        [Description("(UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod24()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("0f0bfd4d-01cd-ea11-a2cd-005056926fe4"); //Case Note 01
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 01")
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4253")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit but not to the team) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod25()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("ac7c0e95-91cd-ea11-a2cd-005056926fe4"); //Case Note 03
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 03")
                .TapOnRecord("Case Note 03");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 03")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4254")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - BU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user do not belong to the Business Unit ) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Validate that the save and delete buttons are not visible")]
        public void TeamSecurityProfiles_TestMethod26()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("e2c0a00d-96cd-ea11-a2cd-005056926fe4"); //Case Note 04
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (BU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 04")
                .TapOnRecord("Case Note 04");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 04")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();


        }

        #endregion

        #region Edit Secur. Prof. - PCBU

        [Property("JiraIssueID", "CDV6-4255")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod28()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("0f0bfd4d-01cd-ea11-a2cd-005056926fe4"); //Case Note 01
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 01")
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4256")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit but not to the team) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod29()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("ac7c0e95-91cd-ea11-a2cd-005056926fe4"); //Case Note 03
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 03")
                .TapOnRecord("Case Note 03");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 03")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4257")]
        [Test]
        [Description("(UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user do not belong to the Business Unit ) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod30()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("e2c0a00d-96cd-ea11-a2cd-005056926fe4"); //Case Note 04
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 04")
                .TapOnRecord("Case Note 04");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 04")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4258")]
        [Test]
        [Description("UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - PCBU' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different (and not a child) Business Unit (user do not belong to the Business Unit ) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Validate that the save and delete buttons are not visible")]
        public void TeamSecurityProfiles_TestMethod31()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("4c986cd9-01cd-ea11-a2cd-005056926fe4"); //Case Note 02
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (PCBU Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 02")
                .TapOnRecord("Case Note 02");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 02")
                .WaitForSaveButtonNotVisible()
                .WaitForSaveAndCloseButtonNotVisible()
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Edit Secur. Prof. - Org

        [Property("JiraIssueID", "CDV6-4259")]
        [Test]
        [Description("(31) UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "User Team must be the owner of a Person Case Note record (associated with the person record) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod33()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("0f0bfd4d-01cd-ea11-a2cd-005056926fe4"); //Case Note 01
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 01")
                .TapOnRecord("Case Note 01");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 01")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4260")]
        [Test]
        [Description("(32) UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in the same Business Unit (user belongs to the Business Unit but not to the team) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod34()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("ac7c0e95-91cd-ea11-a2cd-005056926fe4"); //Case Note 03
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 03")
                .TapOnRecord("Case Note 03");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 03")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4261")]
        [Test]
        [Description("(33) UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a child Business Unit (user do not belong to the Business Unit ) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod35()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("e2c0a00d-96cd-ea11-a2cd-005056926fe4"); //Case Note 04
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 04")
                .TapOnRecord("Case Note 04");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 04")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
        }

        [Property("JiraIssueID", "CDV6-4262")]
        [Test]
        [Description("(34) UI Test for User Security Profiles - " +
            "Team should have a 'View Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Team should have a 'Edit Secur. Prof. - Org' security profiles for the 'Person Case Notes' Business Object - " +
            "Person record has a Case Note associated that belongs to a different team in a different (and not a child) Business Unit (user do not belong to the Business Unit ) - " +
            "Open a person case note - Tap on the related items menu - Navigate to the Person Case Notes page - Open the case note record - " +
            "Edit the description field - Tap on the save and close button - validate that the record was correctly saved")]
        public void TeamSecurityProfiles_TestMethod36()
        {
            string descriptionFieldUpdatedValue = DateTime.Now.ToString("yyyyMMddhhmmss");
            Guid personID = new Guid("364b63a6-f4cc-ea11-a2cd-005056926fe4"); //Juliana Arlinghton 
            Guid userId = new Guid("845A5604-CBCC-EA11-A2CD-005056926FE4"); //Mobile Test User Security 
            Guid teamID = new Guid("2d2c6a3d-f6cc-ea11-a2cd-005056926fe4"); //Mobile Team Security
            Guid personCaseNoteID = new Guid("4c986cd9-01cd-ea11-a2cd-005056926fe4"); //Case Note 02
            List<Guid> securityProfilesToAdd = new List<Guid>();

            //remove all security profiles for the user 
            foreach (Guid recordID in this.PlatformServicesHelper.userSecurityProfile.GetUserSecurityProfileByUserID(userId))
                this.PlatformServicesHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in PlatformServicesHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(teamID))
                PlatformServicesHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW CareDirector User")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org View)")[0]);
            securityProfilesToAdd.Add(PlatformServicesHelper.securityProfile.GetSecurityProfileByName("CW Activities (Org Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                PlatformServicesHelper.teamSecurityProfile.CreateTeamSecurityProfile(teamID, securityProfileId);


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
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes")
                .ValidateRecordPresent("Case Note 02")
                .TapOnRecord("Case Note 02");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 02")
                .InsertDescription(descriptionFieldUpdatedValue)
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad("All Case Notes");

            var fields = PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "notes");
            Assert.AreEqual("<div>" + descriptionFieldUpdatedValue + "</div>", (string)fields["notes"]);
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
