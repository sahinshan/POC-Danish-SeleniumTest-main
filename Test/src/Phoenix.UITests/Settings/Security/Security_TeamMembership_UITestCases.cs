using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-788
    ///
    /// </summary>
    [TestClass]
    public class Security_TeamMembership_UITestCases : FunctionalTest
    {

        [TestInitialize()]
        public void SetupMethod()
        {
            try
            {
                var userid = dbHelper.systemUser.GetSystemUserByUserName("SecurityTestUserTeamMembership").First();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        [Description("Security UI Test 001 - Start Date and End Date fields are added to Team Member records. - " +
            "Open a System User Record - Navigate to the Teams Sub Area - Tap on the add button - " +
            "Validate that the Start date and end date fields are visible")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24500")]
        public void Security_TeamMembership_UITestMethod1()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4");//Security Test User Team Membership

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad();
        }


        [Description("Security UI Test 002 - Team Member records are set Inactive = Yes when End Date is populated.. - " +
            "Open a System User Record - Navigate to the Teams Sub Area - Open a Team Member record - Set the End date and save the record" +
            "Validate that the team member record becomes inactive after saving it")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24501")]
        public void Security_TeamMembership_UITestMethod2()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertEndDateValue(DateTime.Now.Date.ToString("dd/MM/yyyy"))
                .TapSaveAndCloseButton();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad();

            System.Threading.Thread.Sleep(3000);

            bool inactive = (bool)dbHelper.teamMember.GetTeamMemberByID(teamMemberID, "inactive")["inactive"];
            Assert.IsTrue(inactive);
        }

        //in the new menu structure the Workplace menu no longer exists
        //[Description("Security UI Test 003 - Team Based access is restricted to Team Members with Inactive = No. - " +
        //    "Login with a user that has a team member record with a end date in the past. associated team gives user access to provider records" +
        //    "Validate that user cannot access the providers link because the team member record has a past end dates")]
        //[TestCategory("UITest")]
        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-24502")]
        //public void Security_TeamMembership_UITestMethod3()
        //{
        //    Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
        //    Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

        //    //remove any team member record for the combination of user and team
        //    foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
        //        dbHelper.teamMember.DeleteTeamMember(tmID);

        //    //create a new team member record (with past start and end dates)
        //    Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.AddDays(-2).Date, DateTime.Now.AddDays(-1).Date);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("SecurityTestUserTeamMembership", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad(true, true, false, true, true, true)
        //        .ClickWorkplaceMenu()
        //        .WaitForMyWorkLeftMenuLinkVisible()
        //        .WaitForProvidersLinkNotVisible();

        //}

        //in the new menu structure the Workplace menu no longer exists
        //[Description("Security UI Test 004 - Team Based access is restricted to Team Members with Inactive = No. - " +
        //    "Login with a user that has a team member record with no end date. associated team gives user access to provider records" +
        //    "Validate that user can access the providers link because the team member record has a past end dates")]
        //[TestCategory("UITest")]
        //[TestMethod]
        //[TestProperty("JiraIssueID", "CDV6-24503")]
        //public void Security_TeamMembership_UITestMethod4()
        //{
        //    Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
        //    Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

        //    //remove any team member record for the combination of user and team
        //    foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
        //        dbHelper.teamMember.DeleteTeamMember(tmID);

        //    //create a new team member record (with past start and end dates)
        //    Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.AddDays(-2).Date, null);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("SecurityTestUserTeamMembership", "Passw0rd_!")
        //        .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad(true, true, false, true, true, true)
        //        .ClickWorkplaceMenu()
        //        .WaitForMyWorkLeftMenuLinkVisible()
        //        .WaitForProvidersLinkVisible();

        //}

        [Description("Security UI Test 005 - End Date cannot be a future date - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Open a Team Member record - Set a future date in the End date field - Save the record" +
            "Validate that the user is prevented from saving the record with a future end date")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24504")]
        public void Security_TeamMembership_UITestMethod5()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertEndDateValue(DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy"))
                .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be in future.")
                .TapOKButton();

            var hasEndDate = dbHelper.teamMember.GetTeamMemberByID(teamMemberID, "EndDate").ContainsKey("enddate");
            Assert.IsFalse(hasEndDate);
        }

        [Description("Security UI Test 006 - End Date cannot be a future date - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Open a Team Member record - Set a today date in the End date field - Save the record" +
            "Validate that the record is saved and become inactive")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24505")]
        public void Security_TeamMembership_UITestMethod6()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertEndDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .TapSaveAndCloseButton();

            systemUserTeamMemberSubPage
               .WaitForSystemUserTeamMemberSubPageToLoad();


            var fields = dbHelper.teamMember.GetTeamMemberByID(teamMemberID, "enddate", "inactive");

            var enddate = (DateTime)fields["enddate"];
            var inactive = (bool)fields["inactive"];

            Assert.AreEqual(DateTime.Now.Date, enddate);
            Assert.IsTrue(inactive);
        }

        [Description("Security UI Test 007 - End Date cannot be before Start Date - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Open a Team Member record - Set a end date prior to the start date - Save the record" +
            "Validate that the user is prevented from saving the record with a end date prior to the start date")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24506")]
        public void Security_TeamMembership_UITestMethod7()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.Date, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertEndDateValue(DateTime.Now.AddDays(-2).Date.ToString("dd/MM/yyyy"))
                .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be before Start Date.")
                .TapOKButton();

            var hasEndDate = dbHelper.teamMember.GetTeamMemberByID(teamMemberID, "EndDate").ContainsKey("enddate");
            Assert.IsFalse(hasEndDate);

        }

        [Description("Security UI Test 008 - Start Date will default to Created On - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Click on the add button" +
            "Validate that the start date is set by default to Today´s date")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24507")]
        public void Security_TeamMembership_UITestMethod8()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .ValidateStartDateValue(DateTime.Now.ToString("dd/MM/yyyy"));

        }

        [Description("Security UI Test 009 - Start Date will default to Created On - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Click on the add button - select a team record - click the save and close button" +
            "validate that the record is saved and the start date is defaulted to today´s date")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24508")]
        public void Security_TeamMembership_UITestMethod9()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("Bridgend - Adoption")
                .TapSearchButton()
                .SelectResultElement(teamId.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .TapSaveAndCloseButton();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad();

            System.Threading.Thread.Sleep(2000);
            Guid teammemberid = this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId)[0];
            var startdate = (DateTime)this.dbHelper.teamMember.GetTeamMemberByID(teammemberid, "startdate")["startdate"];
            Assert.AreEqual(DateTime.Now.Date, startdate);
        }

        [Description("Security UI Test 010 - Inactive Team Memberships records cannot be reactivated - " +
           "Open a System User Record - Navigate to the Teams Sub Area - open an inactive team membership record" +
            "Validate that no activate button is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24509")]
        public void Security_TeamMembership_UITestMethod10()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, DateTime.Now.Date, DateTime.Now.Date);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .ValidateActivateButtonNotVisible();
        }

        [Description("Security UI Test 011 - Team Membership record of the Team set as the default team for the user cannot be ended - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Open the default team membership for the default team - insert an end date - click on the save button" +
            "Validate that the user is prevented from saving the record")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24510")]
        public void Security_TeamMembership_UITestMethod11()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamMemberID = new Guid("60a648bd-7a90-ea11-a2cd-005056926fe4"); // membership id for the default team (Caredirector QA)


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .OpenContributionRecord(teamMemberID.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertEndDateValue(DateTime.Now.ToString("dd/MM/yyyy"))
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Team Membership record of the Team set as the default team for the user cannot be ended")
                .TapCloseButton();



        }

        [Description("Security UI Test 012 - A user cannot have a Team Membership record for the same Team with overlapping dates - " +
           "Open a System User Record - Navigate to the Teams Sub Area - Tap on the add button - select a start and end date that overlap with a team membership record for the same team - " +
            "Tap on the save button - Validate that the user is prevented from saving the record." +
            "R1 dates (02/05/2020 - 06/05/2020)" +
            "R2 dates (01/05/2020 - 03/05/2020)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24511")]
        public void Security_TeamMembership_UITestMethod12()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, new DateTime(2020, 5, 2), new DateTime(2020, 5, 6));

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertStartDateValue("01/05/2020")
                .InsertEndDateValue("03/05/2020")
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("Bridgend - Adoption")
                .TapSearchButton()
                .SelectResultElement(teamId.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .TapSaveButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for this user and team with dates that overlap. Please correct as necessary.")
                .TapCloseButton();


        }

        [Description("Security UI Test 013 - A user cannot have a Team Membership record for the same Team with overlapping dates - " +
          "Open a System User Record - Navigate to the Teams Sub Area - Tap on the add button - select a start and end date that overlap with a team membership record for the same team - " +
           "Tap on the save button - Validate that the user is prevented from saving the record." +
           "R1 dates (02/05/2020 - 06/05/2020)" +
           "R2 dates (03/05/2020 - 07/05/2020)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24512")]
        public void Security_TeamMembership_UITestMethod13()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, new DateTime(2020, 5, 2), new DateTime(2020, 5, 6));

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertStartDateValue("03/05/2020")
                .InsertEndDateValue("07/05/2020")
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("Bridgend - Adoption")
                .TapSearchButton()
                .SelectResultElement(teamId.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .TapSaveButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for this user and team with dates that overlap. Please correct as necessary.")
                .TapCloseButton();


        }

        [Description("Security UI Test 014 - A user cannot have a Team Membership record for the same Team with overlapping dates - " +
        "Open a System User Record - Navigate to the Teams Sub Area - Tap on the add button - select a start and end date that overlap with a team membership record for the same team - " +
         "Tap on the save button - Validate that the user is prevented from saving the record." +
         "R1 dates (02/05/2020 - 06/05/2020)" +
         "R2 dates (03/05/2020 - 05/05/2020)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24513")]
        public void Security_TeamMembership_UITestMethod14()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, new DateTime(2020, 5, 2), new DateTime(2020, 5, 6));

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertStartDateValue("03/05/2020")
                .InsertEndDateValue("05/05/2020")
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("Bridgend - Adoption")
                .TapSearchButton()
                .SelectResultElement(teamId.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .TapSaveButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for this user and team with dates that overlap. Please correct as necessary.")
                .TapCloseButton();


        }

        [Description("Security UI Test 015 - A user cannot have a Team Membership record for the same Team with overlapping dates - " +
        "Open a System User Record - Navigate to the Teams Sub Area - Tap on the add button - select a start and end date that do not overlap with a team membership record for the same team - " +
         "Tap on the save button - Validate that the record is saved" +
         "R1 dates (02/05/2020 - 06/05/2020)" +
         "R2 dates (07/05/2020 - 08/05/2020)")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24514")]
        public void Security_TeamMembership_UITestMethod15()
        {
            Guid userid = new Guid("1761cfeb-7990-ea11-a2cd-005056926fe4"); //Security Test User Team Membership
            Guid teamId = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde"); //Bridgend - Adoption

            //remove any team member record for the combination of user and team
            foreach (Guid tmID in this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId))
                dbHelper.teamMember.DeleteTeamMember(tmID);

            //create a new team member record
            Guid teamMemberID = dbHelper.teamMember.CreateTeamMember(teamId, userid, new DateTime(2020, 5, 2), new DateTime(2020, 5, 6));

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("Security Test User Team Membership")
                .ClickSearchButton()
                .OpenRecord(userid.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .InsertStartDateValue("07/05/2020")
                .InsertEndDateValue("08/05/2020")
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery("Bridgend - Adoption")
                .TapSearchButton()
                .SelectResultElement(teamId.ToString());

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .TapSaveAndCloseButton();

            systemUserTeamMemberSubPage
               .WaitForSystemUserTeamMemberSubPageToLoad();

            int totalMembershipRecordsForUserAndTeam = this.dbHelper.teamMember.GetTeamMemberByUserAndTeamID(userid, teamId).Count();
            Assert.AreEqual(2, totalMembershipRecordsForUserAndTeam);
        }
    }
}
