using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    ///  
    /// </summary>
    [TestClass]
    public class Provider_WebsiteMessages_UITestCases : FunctionalTest
    {

        #region Website Messages Attachment https://advancedcsg.atlassian.net/browse/CDV6-9267

        #region View Record

        [TestProperty("JiraIssueID", "CDV6-24985")]
        [Description("Open Provider Record -> Navigate to the Website Messages sub-section - " +
            "Validate that a user is redirected to the Website Messages sub page")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod01()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteMessageId = new Guid("cbc1943b-a9a6-eb11-a323-005056926fe4"); //ProviderPortal01


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .ValidateProviderWebsiteMessagesRecordPresent(websiteMessageId.ToString())
                .ValidateRecordCellText(websiteMessageId.ToString(), 2, "Abby Cotterell")
                .ValidateRecordCellText(websiteMessageId.ToString(), 3, "Mr Constantine McDonnald Staff Member of ProviderPortal01")
                .ValidateRecordCellText(websiteMessageId.ToString(), 4, "ProviderPortal01")
                .ValidateRecordCellText(websiteMessageId.ToString(), 5, "José Brazeta")
                .ValidateRecordCellText(websiteMessageId.ToString(), 6, "26/04/2021 17:05:44");

        }

        [TestProperty("JiraIssueID", "CDV6-24986")]
        [Description("Open Provider Record -> Navigate to the Website Messages sub-section - Open a Website Message record - " +
           "Validate that a user is redirected to the Website Message page - Validate that all fields values are correctly displayed")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod02()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteMessageId = new Guid("cbc1943b-a9a6-eb11-a323-005056926fe4"); //ProviderPortal01


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .OpenProviderWebsiteMessagesRecord(websiteMessageId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ValidateFromLinkFieldText("Abby Cotterell")
                .ValidateToLinkFieldText("Mr Constantine McDonnald Staff Member of ProviderPortal01")
                .ValidateRegardingLinkFieldText("ProviderPortal01")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateReadYesOptionChecked(true)
                .ValidateReadNoOptionChecked(false)
                .ValidateMessageText("Message Text ....");
        }

        [TestProperty("JiraIssueID", "CDV6-24987")]
        [Description("Open Provider Record -> Navigate to the Website Messages sub-section - Open a Website Message record - " +
           "Wait for the Website Message page to load - Click on the back button - Validate that the user is redirected to the Website Messages sub-section page")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod03()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteMessageId = new Guid("cbc1943b-a9a6-eb11-a323-005056926fe4"); //ProviderPortal01


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .OpenProviderWebsiteMessagesRecord(websiteMessageId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickBackButton();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad();
        }

        #endregion

        #region Create Record

        [TestProperty("JiraIssueID", "CDV6-24988")]
        [Description("Open Provider Record -> Navigate to the Website Messages sub-section - " +
            "Tap on the add button - Wait for the Website Message page to load - Set data in all fields - Tap on the save button" +
            "Validate that the Website Message record is saved and is associated with the provider")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod04()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID))
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .ClickAddNewRecordButton();

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .InsertMessage("Website Message text goes here ...")
                .ClickFromLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Security Test User Admin").TapSearchButton().SelectResultElement(systemUserID.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickToLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Website Users").TypeSearchQuery("ProviderPortalUser3@somemail.com").TapSearchButton().SelectResultElement(websiteUserId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickSaveButton()
                .WaitForProviderWebsiteMessageRecordPageToLoad("");


            var websiteMessage = dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websiteMessage.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-24980")]
        [Description(
            "Open Provider Record -> Navigate to the Website Messages sub-section - " +
            "Tap on the add button - Wait for the Website Message page to load - Set data in all fields - Tap on the save and close button - " +
            "Reopen the newly created record - Validate that all fields are correctly set.")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod05()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID))
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var userid = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1")[0];
            dbHelper.systemUser.UpdateDefaultTeam(userid, teamid);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .ClickAddNewRecordButton();

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .InsertMessage("Website Message text goes here ...")
                .ClickFromLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Security Test User Admin").TapSearchButton().SelectResultElement(systemUserID.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickToLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Website Users").TypeSearchQuery("ProviderPortalUser3@somemail.com").TapSearchButton().SelectResultElement(websiteUserId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickSaveAndCloseButton();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad();

            var websiteMessage = dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websiteMessage.Count);

            providerWebsiteMessagesPage
                .OpenProviderWebsiteMessagesRecord(websiteMessage[0].ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ValidateFromLinkFieldText("Security Test User Admin")
                .ValidateToLinkFieldText("Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateRegardingLinkFieldText("ProviderPortal01")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateReadYesOptionChecked(false)
                .ValidateReadNoOptionChecked(true)
                .ValidateMessageText("Website Message text goes here ...")
                ;
        }

        #endregion

        #region Update Record

        [TestProperty("JiraIssueID", "CDV6-24990")]
        [Description(
            "Open Provider Record -> Navigate to the Website Messages sub-section - " +
            "Open an existing Website Message record - Wait for the record page to load - Update the Message field - Tap on the save and close button - " +
            "Reopen the newly created record - Validate that the message field is updated.")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod06()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID))
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message Text ...", false);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .OpenProviderWebsiteMessagesRecord(websiteMessageId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .InsertMessage("Message Text ... updated")
                .ClickReadFieldYesRadioButton()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .OpenProviderWebsiteMessagesRecord(websiteMessageId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ValidateFromLinkFieldText("Security Test User Admin")
                .ValidateToLinkFieldText("Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateRegardingLinkFieldText("ProviderPortal01")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateReadYesOptionChecked(true)
                .ValidateReadNoOptionChecked(false)
                .ValidateMessageText("Message Text ... updated");
        }

        #endregion

        #region Delete Record

        [TestProperty("JiraIssueID", "CDV6-24991")]
        [Description(
            "Open Provider Record -> Navigate to the Website Messages sub-section - " +
            "Open an existing Website Message record - Wait for the record page to load - Click on the delete button - Confirm the delete operation - " +
            "Validate that the record is removed")]
        [TestMethod, TestCategory("UITest")]
        public void Provider_WebsiteMessages_UITestMethod07()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var providerNumber = "2731";
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID))
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message Text ...", false);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToWebsiteMessagesPage();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad()
                .OpenProviderWebsiteMessagesRecord(websiteMessageId.ToString());

            providerWebsiteMessageRecordPage
                .WaitForProviderWebsiteMessageRecordPageToLoad("")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            providerWebsiteMessagesPage
                .WaitForProviderWebsiteMessagesPageToLoad();

            var websiteMessage = dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(0, websiteMessage.Count);

        }

        #endregion

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod, TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
