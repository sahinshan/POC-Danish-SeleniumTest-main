using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class Repositioning : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("WC BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("WC T1", null, _businessUnitId, "907678", "RepositioningT1@careworkstempmail.com", "Welfare Checks T1", "020 123456");

                #endregion

                #region Create SystemUser Record

                _systemUserName = "reposuser1";
                _systemUserFullName = "Repositioning User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Repositioning", "User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8367

        [TestProperty("JiraIssueID", "ACC-3424")]
        [Description("Step(s) 1 to 5 from the original test - ACC-3424")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Person Repositioning")]
        public void Repositioning_ACC3424_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Andre";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Type Of Pressure Relieving Equipment In Use (Specialist Mattress Id)

            var _specialistMattressId = dbHelper.specialistmattress.GetByName("Not applicable")[0];

            #endregion

            #region Describe Skin Condition

            var rash_SkinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Rash")[0];
            var blisters_SkinConditionId = dbHelper.careProviderCarePlanSkinCondition.GetByName("Blister(s)")[0];

            #endregion

            #endregion

            #region Care Physical Locations 

            var Livingroom_CarePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Well Being

            var WellbeingId = dbHelper.careWellbeing.GetByName("Very Happy")[0];

            #endregion

            #region Equipment

            var NoEquipmentId = dbHelper.careEquipment.GetByName("No equipment")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Create System User Record

            var systemUserName2 = "reposuser2";
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(systemUserName2, "Repositioning", "User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Step 1 to 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRepositioningPage();

            personRepositioningPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();


            var dateAndTimeOccurred = DateTime.Now.AddDays(-2);
            var time = dateAndTimeOccurred.ToUniversalTime();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .SelectConsentGivenPicklistValueByText("Yes")
                .SetDateOccurred(dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(time.ToString("HH:mm"))

                .ValidateCareNoteText("There were no new concerns with Andre's skin.\r\n" +
                "This care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\n" +
                "Andre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .SelectStartingPosition("Lying")
                .ValidateCareNoteText("Andre was Lying.\r\nThere were no new concerns with Andre's skin.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
               .SelectConfirmRepositioned("Repositioned")
               .SelectRepositionedToPosition("Sitting")
               .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nThere were no new concerns with Andre's skin.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .SelectRepositionedToSide("Right side")
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nThere were no new concerns with Andre's skin.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ClickTypeOfPressureRelievingEquipmentInUseLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Not applicable", _specialistMattressId);

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nNo pressure relieving equipment was in use.\r\nThere were no new concerns with Andre's skin.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .ClickAreThereAnyNewConcernsWithThePersonsSkin_YesRadioButton()
                .InsertTextOnWhereOnTheBodyField("Hand")
                .ClickDescribeSkinConditionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton("Rash", rash_SkinConditionId.ToString())
                .SelectElementAndClickAddRecordsButton("Blister(s)", blisters_SkinConditionId.ToString())
                .ClickOkButton();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .SelectAreTheyComfortable("Comfortable")
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
               .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Living Room").TapSearchButton().AddElementToList(Livingroom_CarePhysicalLocationId)
                .TapOKButton();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nAndre was in the Living Room.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
               .ClickWellbeingLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Very Happy", WellbeingId);

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectElementAndClickAddRecordsButton("No equipment", NoEquipmentId.ToString())
                .ClickOkButton();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nAndre was in the Living Room.\r\nAndre used the following equipment: No equipment.\r\nAndre came across as Very Happy.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).");

            personRepositioningRecordPage
                .ClickAssistanceNeededLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Independent", careAssistanceNeededId);

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .InsertTotalTimeSpentWithClientMinutes("30")
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nAndre was in the Living Room.\r\nAndre used the following equipment: No equipment.\r\nAndre came across as Very Happy.\r\nAndre did not require any assistance.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Andre.");

            personRepositioningRecordPage
                .InsertTextInAdditionalNotesTextArea(_currentDateSuffix + " repositioning for Andre did not require any equipment and it was done independently")
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nAndre was in the Living Room.\r\nAndre used the following equipment: No equipment.\r\nAndre came across as Very Happy.\r\nAndre did not require any assistance.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 1 colleague(s).\r\nOverall, I spent 30 minutes with Andre.\r\nWe would like to note that: " + _currentDateSuffix + " repositioning for Andre did not require any equipment and it was done independently.");

            personRepositioningRecordPage
                .ClickStaffRequiredLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(systemUserName2)
                .TapSearchButton()
                .ClickAddSelectedButton(systemUser2Id.ToString());

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .SelectIncludeInNextHandoverOption(false)
                .SelectFlagRecordForHandoverOption(false)
                .ClickSave()
                .WaitForPageToLoad();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Andre was Lying.\r\nAndre was repositioned to a Sitting position.\r\nAndre was repositioned to their Right side.\r\nAndre was Comfortable.\r\nNo pressure relieving equipment was in use.\r\nThe following new skin concerns were noted on Andre's Hand: Rash and Blister(s).\r\nAndre was in the Living Room.\r\nAndre used the following equipment: No equipment.\r\nAndre came across as Very Happy.\r\nAndre did not require any assistance.\r\nThis care was given at " + dateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\nAndre was assisted by 2 colleague(s).\r\nOverall, I spent 30 minutes with Andre.\r\nWe would like to note that: " + _currentDateSuffix + " repositioning for Andre did not require any equipment and it was done independently.");


            #endregion

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }

}













