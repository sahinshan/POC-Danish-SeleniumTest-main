using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.DailyCareBOs
{

    /// <summary>
    /// This class contains Automated UI test scripts for Daily Care BO
    /// </summary>
    [TestClass]
    public class DailyCareBO_ACC_3600_UITestCases : FunctionalTest
    {
        #region Properties
        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private string _systemUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid defaultTeamId;

        #endregion

        [TestInitialize()]
        public void DailyCare_MobilityBO_SetupTest()
        {

            try
            {

                #region Environment Name

                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                defaultTeamId = (Guid)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "defaultteamid")["defaultteamid"];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Business Unit
                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DailyCareBU");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DailyCareTeam", null, _businessUnitId, "907678", "DailyCareTeam@careworkstempmail.com", "DailyCareTeam", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _teamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser 

                _systemUsername = "DailyCareUser4";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DailyCare", "User4", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


                #region Team membership

                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

                #endregion

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8501

        [TestProperty("JiraIssueID", "ACC-3600")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3600 - " +
            "Verify that user can create attachments for Mobility BO")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [DeploymentItem("Files\\video.mp4"), DeploymentItem("Files\\ImageFile.jpeg"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Mobility")]
        public void DailyCareBO_ACC3600_UITestMethod001()
        {
            #region Person

            var firstName = "Andre";
            var lastName = currentTimeString;
            var addresstypeid = 6; //Home

            _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
            "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
            var _personFullName = firstName + " " + lastName;

            #endregion

            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Document Type

            var _documentTypeId1 = commonMethodsDB.CreateAttachDocumentType(_teamId, "Daily Care Support Information", new DateTime(2019, 1, 1));
            var _documentTypeId2 = commonMethodsDB.CreateAttachDocumentType(_teamId, "Policy", new DateTime(2022, 7, 20));

            #endregion

            #region Document Sub Type

            var _documentSubTypeId1 = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Daily Care Support Documentation", new DateTime(2019, 1, 1), _documentTypeId1);
            var _documentSubTypeId2 = commonMethodsDB.CreateAttachDocumentSubType(_teamId, "Accident/Incident", new DateTime(2022, 7, 20), _documentTypeId2);

            #endregion

            #region CarePhysicalLocation

            var _carePhysicalLocationFrom = dbHelper.carePhysicalLocation.GetByName("Bathroom").FirstOrDefault();
            var _carePhysicalLocationTo = dbHelper.carePhysicalLocation.GetByName("Bedroom").FirstOrDefault();

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();

            #endregion

            #region careAssistanceNeededId


            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();

            #endregion

            #region Person Mobility Record

            var dateTimeOcurred = todayDate.ToUniversalTime().AddHours(-1);
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityId1 = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _teamId, _businessUnitId, dateTimeOcurred, 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 10, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5,
                "Andre moved from the Bathroom to the Bedroom, approximately 10 metres.\r\nAndre used the following equipment: No equipment.\r\nAndre came across as Happy.\r\nAndre required assistance: Independent. Amount given:.\r\n" +
"This care was given at " + dateTimeOcurred.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\nAndre was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with Andre.\r\n");

            #endregion

            #region Mobility Attachments

            var Date1 = todayDate.ToUniversalTime().AddHours(1);

            var _mobilityAttachmentId1 = dbHelper.cpMobilityAttachment.CreateCpMobilityAttachment(_teamId, _personID, "Test Mobility " + currentTimeString, Date1, _documentTypeId1, _documentSubTypeId1, _personMobilityId1, TestContext.DeploymentDirectory + "\\video.mp4", "Caption " + currentTimeString);

            var Date2 = todayDate.AddDays(-3).ToUniversalTime().AddHours(1);
            var _mobilityAttachmentId2 = dbHelper.cpMobilityAttachment.CreateCpMobilityAttachment(_teamId, _personID, "Record 2", Date2, _documentTypeId2, _documentSubTypeId2, _personMobilityId1, TestContext.DeploymentDirectory + "\\video.mp4", "Mobility Attachment 2");

            var Date3 = todayDate.ToUniversalTime().AddHours(-2);
            var _mobilityAttachmentId3 = dbHelper.cpMobilityAttachment.CreateCpMobilityAttachment(_teamId, _personID, "Record 3", Date3, _documentTypeId1, _documentSubTypeId1, _personMobilityId1, TestContext.DeploymentDirectory + "\\ImageFile.jpeg", "Mobility Attachment 3");

            #endregion

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            //Similar verification as ACC-3603

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad();

            #endregion

            #region Step 4

            personMobilityPage
                .ValidateRecordIsDisplayed(_personMobilityId1);

            personMobilityPage
                .OpenRecord(_personMobilityId1);

            #endregion

            #region Step 5
            //Verify Resident Section

            personMobilityRecordPage
                .WaitForPageToLoad()
                .VerifyResidentVoiceSectionIsDisplayed(true);

            #endregion

            #region Step 6, 7

            //Verify Attachment can be added
            mobilityAttachmentsPage
                .WaitForPageToLoad(true)
                .OpenRecord(_mobilityAttachmentId1);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpmobilityattachment")
                .ClickOnExpandIcon();

            mobilityAttachmentRecordPage
                .WaitForPageToLoad()
                .ValidateTitleText("Test Mobility " + currentTimeString)
                .ValidateDateText(Date1.ToString("dd'/'MM'/'yyyy"))
                .ValidateDate_TimeText(Date1.ToString("HH:mm"))
                .ValidateCaptionText("Caption " + currentTimeString)
                .ValidateFileLinkText("video.mp4");

            #endregion

            #region Step 8

            mobilityAttachmentRecordPage
                .ClickBackButton();

            mobilityAttachmentsPage
                .WaitForPageToLoad(true)
                .ValidateColumnIsSortedByDescendingOrder(5)
                .ValidateColumnsSortOrderDescending(5, "Date")
                .ValidateRecordIsPresent(1, _mobilityAttachmentId1)
                .ValidateRecordIsPresent(2, _mobilityAttachmentId3)
                .ValidateRecordIsPresent(3, _mobilityAttachmentId2);

            #endregion

            #region Step 9

            personMobilityRecordPage
                .WaitForPageToLoad()
                .NavigateToAttachmentsPage();

            mobilityAttachmentsPage
                .WaitForPageToLoad()
                .ValidateRecordPosition(1, _mobilityAttachmentId1.ToString())
                .ValidateRecordPosition(2, _mobilityAttachmentId3.ToString())
                .ValidateRecordPosition(3, _mobilityAttachmentId2.ToString());

            mobilityAttachmentsPage
                .SearchRecord("Policy")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_mobilityAttachmentId2, true)
                .ValidateRecordPresent(_mobilityAttachmentId1, false)
                .ValidateRecordPresent(_mobilityAttachmentId3, false);

            mobilityAttachmentsPage
                .SearchRecord("Daily Care Support Documentation")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_mobilityAttachmentId1, true)
                .ValidateRecordPresent(_mobilityAttachmentId3, true)
                .ValidateRecordPresent(_mobilityAttachmentId2, false);

            mobilityAttachmentsPage
                .SearchRecord("Accident/Incident")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_mobilityAttachmentId2, true)
                .ValidateRecordPresent(_mobilityAttachmentId1, false)
                .ValidateRecordPresent(_mobilityAttachmentId3, false);

            mobilityAttachmentsPage
                .SearchRecord("Daily Care Support Information")
                .WaitForPageToLoad()
                .ValidateRecordPresent(_mobilityAttachmentId1, true)
                .ValidateRecordPresent(_mobilityAttachmentId3, true)
                .ValidateRecordPresent(_mobilityAttachmentId2, false);

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Attachments (For Mobility)")
                .SelectFilter("1", "Attachment (For Mobility)")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Test Mobility " + currentTimeString, _mobilityAttachmentId1);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_mobilityAttachmentId1.ToString())
                .ValidateSearchResultRecordNotPresent(_mobilityAttachmentId2.ToString())
                .ValidateSearchResultRecordNotPresent(_mobilityAttachmentId3.ToString());

            advanceSearchPage
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Attachments (For Mobility)")
                .ClickDeleteButton()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(7)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(7)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_mobilityAttachmentId1.ToString())
                .ValidateSearchResultRecordPresent(_mobilityAttachmentId2.ToString())
                .ValidateSearchResultRecordPresent(_mobilityAttachmentId3.ToString());

            #endregion


        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
