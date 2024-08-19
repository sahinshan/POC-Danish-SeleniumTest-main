using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class Conversations_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid defaultTeamId;
        private List<Guid> securityProfilesList;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion


        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("ConversationsBU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("ConversationsT1", null, _businessUnitId, "907678", "ConversationsT1@careworkstempmail.com", "Conversations T1", "020 123456");

                #endregion

                #region Security Profiles

                securityProfilesList = new List<Guid>();

                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (View)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First());

                #endregion

                #region Create System User Record

                _systemUserName = "ConversationUser1";
                _systemUserFullName = "Conversation User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Conversation", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

                #endregion

                #region Default Team

                //get the default team for the tenant
                defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

                #endregion

                #region Team Member

                //link the user with the default team so that we can access all reference data in the system
                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2020, 1, 1), null);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-9142

        [TestProperty("JiraIssueID", "ACC-9165")]
        [Description("Step(s) 1 to 6 from the original test - ACC-3896")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Residential Care")]
        [TestProperty("Screen", "Conversations")]
        public void DailyCare_ACC3896_UITestMethod01()
        {
            #region Care Physical Locations 

            var bathroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Bathroom")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("OK")[0];
            var careWellbeing3Id = dbHelper.careWellbeing.GetByName("Very Unhappy")[0];
            var careWellbeing4Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];
            var careAssistanceNeeded2Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];
            var careAssistanceNeeded3Id = dbHelper.careAssistanceNeeded.GetByName("Physical Assistance")[0];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false);

            #endregion

            #region Step 2

            mainMenu
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToPersonConversationsPage();

            conversationsPage
                .WaitForPageToLoad();

            #endregion

            #region Step 3, 4

            conversationsPage
                .ValidateHeaderCellIsDisplayed("Date and Time Occurred")
                .ValidateHeaderCellIsDisplayed("Assistance Needed?")
                .ValidateHeaderCellIsDisplayed("Wellbeing")
                .ValidateHeaderCellIsDisplayed("Notes")
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now;

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidatePersonLinkText(personFullName)
                .ValidateDateAndTimeOccurred_DateText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(dateandTimeOccurred.ToString("HH:mm"))
                .ValidateResponsibleTeamLinkText("ConversationsT1")
                .ValidatePreferencesText("No preferences recorded, please check with Erik.")
                .ClickCloseDrawerButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 12, "Conversations " + _currentDateSuffix); //Conversations = 12

            #endregion

            conversationsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            dateandTimeOccurred = DateTime.Now;

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidatePersonLinkText(personFullName)
                .ValidateDateAndTimeOccurred_DateText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(dateandTimeOccurred.ToString("HH:mm"))
                .ValidateResponsibleTeamLinkText("ConversationsT1")
                .ValidatePreferencesText("Conversations " + _currentDateSuffix);

            #endregion

            #region Step 5

            conversationRecordPage
                .InsertTextOnNotesTextarea("Conversation Notes 1. . . .\r\n Conversation Notes 2. . \r\n\r\n Conversation Notes3");

            #endregion

            #region Step 6

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateMandatoryFieldIsVisible("Location")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bathroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateLocationIfOtherVisible(true)
                .InsertTextOnLocationIfOtherTextareaField("other location ...");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateMandatoryFieldIsVisible("Wellbeing")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing4Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateActionTakenIsVisible(false)
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing1Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 1 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing2Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 2 ...")
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing3Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateActionTakenIsVisible(true)
                .InsertTextOnActionTaken("action taken 3 ...");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careAssistanceNeeded1Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("Some")
                .SelectAssistanceAmountFromPicklist("Fair")
                .SelectAssistanceAmountFromPicklist("A Lot")
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careAssistanceNeeded2Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateAssistanceAmountPicklistVisible(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careAssistanceNeeded3Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateAssistanceAmountPicklistVisible(true)
                .SelectAssistanceAmountFromPicklist("Fair")
                .SelectAssistanceAmountFromPicklist("A Lot")
                .SelectAssistanceAmountFromPicklist("Some");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnTotalTimesSpentWithPersonMinutes("30");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnAdditionalnotes("additional notes ...\r\n addtional notes 2...");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9166")]
        [Description("Step(s) 7 to 8 from the original test - ACC-3896")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Residential Care")]
        [TestProperty("Screen", "Conversations")]
        public void DailyCare_ACC3896_UITestMethod02()
        {
            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToPersonConversationsPage();

            conversationsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateLinkedAdlCategoriesLookupButtonIsVisible(true)
                .ClickLinkedAdlCategoriesLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePlanNeedDomainId)
                .TapOKButton();

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ValidateLinkedAdlCategories_SelectedElementFieldTextBeforeSave(_carePlanNeedDomainId.ToString(), "Acute\r\n");

            #endregion

            #region Step 8

            conversationRecordPage
                .ClickIsincludeinnexthandover_YesOption()
                .ClickIsincludeinnexthandover_NoOption()
                .ClickFlagrecordforhandover_YesOption()
                .ClickFlagrecordforhandover_NoOption();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-9167")]
        [Description("Step(s) 9 to 11 from the original test - ACC-3896")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Residential Care")]
        [TestProperty("Screen", "Conversations")]
        public void DailyCare_ACC3896_UITestMethod03()
        {
            #region Care Physical Locations 

            var bathroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Bathroom")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Erik";
            var lastName = _currentDateSuffix;
            var personFullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 12, "Conversations " + _currentDateSuffix); //Conversations = 12

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, true, false)
                .NavigateToPersonConversationsPage();

            conversationsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now.AddDays(-2);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .SetDateAndTimeOccurred(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"), "07:00")
                .InsertTextOnNotesTextarea("Conversation Notes 1. . . .\r\n Conversation Notes 2. . \r\n\r\n Conversation Notes3")
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(bathroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnLocationIfOtherTextareaField("other location ...");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()                
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing1Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnActionTaken("action taken 1 ...");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ClickAssistanceNeededLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careAssistanceNeeded1Id);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .SelectAssistanceAmountFromPicklist("Some");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnTotalTimesSpentWithPersonMinutes("30");

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .InsertTextOnAdditionalnotes("additional notes ...\r\n addtional notes 2...");

            conversationRecordPage
                .ClickLinkedAdlCategoriesLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePlanNeedDomainId)
                .TapOKButton();

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ClickSaveAndCloseButton();

            conversationsPage
                .WaitForPageToLoad();

            var personConversationRecords = dbHelper.personConversations.GetByPersonId(personId);
            Assert.AreEqual(1, personConversationRecords.Count);
            var personConversationRecordId = personConversationRecords[0];

            conversationsPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(personConversationRecordId, true);

            //verify field values are saved. verify carenote field text
            conversationsPage
                .OpenRecord(personConversationRecordId);

            conversationRecordPage
                .WaitForRecordToLoadInDrawerMode()
                .ClickExpandIcon();

            conversationRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(personFullName)
                .ValidateDateAndTimeOccurred_DateText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("07:00")
                .ValidateResponsibleTeamLinkText("ConversationsT1")
                //.ValidatePreferencesText("Conversations " + _currentDateSuffix)
                .ValidateNotesText("Conversation Notes 1. . . .\r\n Conversation Notes 2. . \r\n\r\n Conversation Notes3")
                .ValidateLocation_SelectedElementLinkText(bathroom_LocationId, "Bathroom")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherTextareaFieldText("other location ...")
                .ValidateWellbeingLinkText("Unhappy")
                .VerifyActionTaken_HasPainReliefBeenOfferedText("action taken 1 ...")
                .ValidateAssistanceneededLinkText("Asked For Help")
                .ValidateAssistanceAmountPicklistSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonMinutesText("30")
                .ValidateStaffRequiredSelectedOptionText(_systemUserId, _systemUserFullName)
                .ValidateAdditionalnotesText("additional notes ...\r\n addtional notes 2...")
                .ValidateLinkedAdlCategories_SelectedElementLinkText(_carePlanNeedDomainId.ToString(), "Acute")
                .ValidateCarenoteText("Erik engaged in a conversation: Conversation Notes 1.... Conversation Notes 2.. Conversation Notes3.\r\n" +
                "Erik was in the Bathroom and other location....\r\n" +
                "Erik came across as Unhappy.\r\n" +
                "The action taken was: action taken 1....\r\n" +
                "Erik required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + dateandTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Erik was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Erik.")
                .ValidateIsincludeinnexthandover_NoOptionChecked()
                .ValidateIsincludeinnexthandover_YesOptionNotChecked()
                .ValidateFlagrecordforhandover_NoOptionChecked()
                .ValidateFlagrecordforhandover_YesOptionNotChecked();

            #endregion

            #region Step 9

            conversationRecordPage
                .ClickFlagrecordforhandover_YesOption();

            handoverCommentsPage
                .WaitForHandoverCommentsToLoad();

            #endregion

            #region Step 10

            handoverCommentsPage
                .ClickNewRecordButton();

            handoverCommentRecordPage
                .WaitForPageToLoad()
                .InsertTextOnHandoverComments("Handover Comments . . .")
                .ClickSaveButton();
            
            handoverCommentRecordPage
                .WaitForPageToLoad()
                .ValidateHandoverCommentsText("Handover Comments . . .");

            #endregion

        }

        #endregion

    }

}













