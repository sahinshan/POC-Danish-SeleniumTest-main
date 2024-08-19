using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.DailyCare
{
    [TestClass]
    public class FoodAndFluid_UITestCases : FunctionalTest
    {
        #region Private Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _defaultTeam;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private List<Guid> securityProfilesList;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("FAF BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("FAF T1", null, _businessUnitId, "907678", "FoodAndFluidT1@careworkstempmail.com", "Food And Fluid T1", "020 123456");
                _defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true)[0];

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
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (Edit)").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First());
                securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search").First());

                #endregion

                #region Create System User Record

                _systemUserName = "fafrostereduser1";
                _systemUserFullName = "Food And Fluid Rostered User 1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Food And Fluid", "Rostered User 1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

                #endregion

                #region Team Member

                commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-9188

        [TestProperty("JiraIssueID", "ACC-9233")]
        [Description("Step(s) 1 to 4 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1 - 4 Verify General Section and Save record with empty mandatory fields (Consent Given not inserted)

            //Step1
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            //Step 2-3
            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateResponsibleTeamLookupButtonVisibility(true)
                .ValidatePreferencesText("No preferences recorded, please check with Joao.")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 14, "FAF " + _currentDateSuffix); //Food and Fluid = 14

            #endregion

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateandTimeOccurred = DateTime.Now;

            //Step 4
            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateResponsibleTeamLookupButtonVisibility(true)
                .ValidateResponsibleTeamLinkText("FAF T1")
                .ValidatePersonLookupButtonVisibility(true)
                .ValidatePersonLinkText(person_fullName)
                .ValidatePreferencesText("FAF " + _currentDateSuffix)
                .ValidateDateAndTimeOccurred_DateText(dateandTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(dateandTimeOccurred.ToString("HH:mm"))
                .ValidateConsentGivenSelectedText("");

            foodAndFluidRecordPage
                .ValidateTopPageNotificationVisibility(false)
                .ValidateConsentGivenErrorLabelVisibility(false)
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")

                .ValidateConsentGivenErrorLabelVisibility(true)
                .ValidateConsentGivenErrorLabelText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9234")]
        [Description("Step 5 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Meal Type

            var BreakfastMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Breakfast").First();
            var LunchMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Lunch").First();
            var DinnerMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Dinner").First();
            var SnackMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Snack").First();
            var OtherMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Other").First();

            #endregion

            #region Alert and Hazard Type

            var AlertTypeId = commonMethodsDB.CreateAlertAndHazardType(_teamId, "ACC2136", new DateTime(2024, 1, 1), true);

            #endregion

            #region Step 5 - Verify Details Section and Save record with empty mandatory fields (Consent Given = Yes)

            //Step 5
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ValidateSwallowAssessmentRatingVisibility(false)
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Alert and Hazard

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(personId, _teamId, 1,
                2, AlertTypeId, null, 6, null, new DateTime(2024, 7, 1), null, "ACC2136");

            #endregion

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ValidateSwallowAssessmentRatingVisibility(true)
                .ValidateSwallowAssessmentRatingText("6 Soft & Bite-sized - SB6")
                .ClickSaveButton()

                .ValidateMealTypeRequiredErrorLabelText("Please fill out this field.")
                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(BreakfastMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateMealTypeOtherVisibility(false)
                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(LunchMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateMealTypeOtherVisibility(false)
                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(DinnerMealTypeId);
            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateMealTypeOtherVisibility(false)
                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(SnackMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateMealTypeOtherVisibility(false)
                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(OtherMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateMealTypeOtherVisibility(true)
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateMealTypeOtherErrorLabelVisibility(true)
                .ValidateMealTypeOtherErrorLabelText("Please fill out this field.")
                .InsertTextOnMealTypeOther("Other Meal Type")
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateMealTypeOtherErrorLabelVisibility(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9235")]
        [Description("Step 6 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Food Amount Offered

            var foodAmountOfferedId1 = dbHelper.foodAmountOffered.GetByName("Bowl").First();
            var foodAmountOfferedId2 = dbHelper.foodAmountOffered.GetByName("Dinner plate").First();
            var foodAmountOfferedId3 = dbHelper.foodAmountOffered.GetByName("Other").First();
            var foodAmountOfferedId4 = dbHelper.foodAmountOffered.GetByName("Side plate").First();

            #endregion

            #region Food Amount Eaten

            var foodAmountEatenId1 = dbHelper.foodAmountEaten.GetByName("All").First();
            var foodAmountEatenId2 = dbHelper.foodAmountEaten.GetByName("Most").First();
            var foodAmountEatenId3 = dbHelper.foodAmountEaten.GetByName("Half").First();
            var foodAmountEatenId4 = dbHelper.foodAmountEaten.GetByName("Some").First();
            var foodAmountEatenId5 = dbHelper.foodAmountEaten.GetByName("None").First();

            #endregion

            #region Step 6 - Verify Food Section and Save record with empty mandatory fields (Consent Given = Yes)

            //Step 6
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ClickSaveButton()
                .ValidateAmountOfFoodOfferedErrorLabelVisibility(false)
                .ValidateAmountOfFoodEatenErrorLabelVisibility(false)

                .InsertTextOnTypeOfFood("Type of Food . . .\r\n Food1")
                .ClickSaveButton()

                .ValidateAmountOfFoodOfferedErrorLabelVisibility(true)
                .ValidateAmountOfFoodOfferedErrorLabelText("Please fill out this field.")
                .ValidateAmountOfFoodEatenErrorLabelVisibility(true)
                .ValidateAmountOfFoodEatenErrorLabelText("Please fill out this field.")
                .ClickSaveButton()

                .ValidateMealTypeRequiredErrorLabelText("Please fill out this field.")
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFoodOfferedErrorLabelVisibility(false)
                .ValidateAmountOfFoodOtherVisibility(false)
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFoodOtherVisibility(false)
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId4);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFoodOtherVisibility(false)
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId3);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFoodOtherVisibility(true)
                .ValidateAmountOfFoodOfferedErrorLabelVisibility(false)
                .InsertTextOnAmountOfFoodOther("Other food . . .");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFoodEatenErrorLabelVisibility(false)
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId3);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId4);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId5);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateAmountOfFoodOfferedErrorLabelVisibility(false)
                .ValidateAmountOfFoodOtherErrorLabelVisibility(false)
                .ValidateAmountOfFoodEatenErrorLabelVisibility(false)
                .ValidateMandatoryFieldIsVisible("Amount of Food Offered")
                .ValidateMandatoryFieldIsVisible("Amount of Food Other")
                .ValidateMandatoryFieldIsVisible("Amount of Food Eaten");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9236")]
        [Description("Step 7 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod04()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Type of Fluids

            var typeOfFluidId1 = dbHelper.typeOfFluid.GetByName("Alcoholic drink").First();
            var typeOfFluidId2 = dbHelper.typeOfFluid.GetByName("Coffee").First();
            var typeOfFluidId3 = dbHelper.typeOfFluid.GetByName("Fizzy drink").First();
            var typeOfFluidId4 = dbHelper.typeOfFluid.GetByName("Fortified drink").First();
            var typeOfFluidId5 = dbHelper.typeOfFluid.GetByName("Fruit Juice / Smoothie").First();
            var typeOfFluidId6 = dbHelper.typeOfFluid.GetByName("Milk/Milkshake").First();
            var typeOfFluidId7 = dbHelper.typeOfFluid.GetByName("Other").First();
            var typeOfFluidId8 = dbHelper.typeOfFluid.GetByName("Squash").First();
            var typeOfFluidId9 = dbHelper.typeOfFluid.GetByName("Tea").First();
            var typeOfFluidId10 = dbHelper.typeOfFluid.GetByName("Water").First();

            #endregion

            #region Fluid Amount Offered

            var fluidAmountOfferedId1 = dbHelper.fluidAmountOffered.GetByName("Large glass").First();
            var fluidAmountOfferedId2 = dbHelper.fluidAmountOffered.GetByName("Mug").First();
            var fluidAmountOfferedId3 = dbHelper.fluidAmountOffered.GetByName("Other").First();
            var fluidAmountOfferedId4 = dbHelper.fluidAmountOffered.GetByName("Small glass").First();
            var fluidAmountOfferedId5 = dbHelper.fluidAmountOffered.GetByName("Tea cup").First();

            #endregion

            #region Step 7 - Verify Fluid Section and Save record with empty mandatory fields (Consent Given = Yes)

            //Step 7
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ClickSaveButton()
                .ValidateAmountOfFluidOfferedErrorLabelVisibility(false)
                .ValidateAmountOfFluidDrankErrorLabelVisibility(false)

                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickSaveButton()

                .ValidateAmountOfFluidOfferedErrorLabelVisibility(true)
                .ValidateAmountOfFluidOfferedErrorLabelText("Please fill out this field.")
                .ValidateAmountOfFluidDrankErrorLabelVisibility(true)
                .ValidateAmountOfFluidDrankErrorLabelText("Please fill out this field.")
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId3);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId4);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId5);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId6);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId8);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId9);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId10);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(false)
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId7);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidOtherVisibility(true)
                .ClickSaveButton()

                .ValidateAmountOfFluidOfferedErrorLabelVisibility(true)
                .ValidateAmountOfFluidOfferedErrorLabelText("Please fill out this field.")
                .ValidateAmountOfFluidDrankErrorLabelVisibility(true)
                .ValidateAmountOfFluidDrankErrorLabelText("Please fill out this field.")
                .ValidateTypeofFluidOtherErrorLabelVisibility(true)
                .ValidateTypeofFluidOtherErrorLabelText("Please fill out this field.")

                .InsertTextOnTypeOfFluidOther("Other fluid . . .");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFluidOtherVisibility(false)
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFluidOtherVisibility(false)
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId4);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFluidOtherVisibility(false)
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId5);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFluidOtherVisibility(false)
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId3);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAmountOfFluidOtherVisibility(true)
                .ClickSaveButton()

                .ValidateAmountOfFluidOtherErrorLabelVisibility(true)
                .ValidateAmountOfFluidOtherErrorLabelText("Please fill out this field.");

            foodAndFluidRecordPage
                .InsertTextOnAmountOfFluidOther("Medium glass")
                .InsertTextOnAmountOfFluidDrank("60")
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateTypeofFluidOtherErrorLabelVisibility(false)
                .ValidateAmountOfFluidOfferedErrorLabelVisibility(false)
                .ValidateAmountOfFluidOtherErrorLabelVisibility(false)
                .ValidateAmountOfFluidDrankErrorLabelVisibility(false)
                .ValidateMandatoryFieldIsVisible("Type of Fluid Other")
                .ValidateMandatoryFieldIsVisible("Amount of Fluid Offered")
                .ValidateMandatoryFieldIsVisible("Amount of Fluid Other")
                .ValidateMandatoryFieldIsVisible("Amount of Fluid Drank (ml)");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9237")]
        [Description("Step 8 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod05()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Non-Oral Fluids Delivery

            var nonOralFluidDeliveryId1 = dbHelper.nonOralFluidDelivery.GetByName("NG - Nasogastric Tube").First();
            var nonOralFluidDeliveryId2 = dbHelper.nonOralFluidDelivery.GetByName("PEG - Percutaneous Endoscopic Gastronomy Tube").First();
            var nonOralFluidDeliveryId3 = dbHelper.nonOralFluidDelivery.GetByName("RIG - Radiologically Inserted Gastronomy Tube").First();
            var nonOralFluidDeliveryId4 = dbHelper.nonOralFluidDelivery.GetByName("Subcutaneous Infusion").First();

            #endregion

            #region Step 8 - Verify Non-Oral Section and Save record with empty mandatory fields (Consent Given = Yes)

            //Step 8
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var expiryDate = DateTime.Now.AddDays(1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ClickSaveButton()
                .ValidateAmountGivenErrorLabelVisibility(false)
                .ValidateWhatWasGivenErrorLabelVisibility(false)
                .ValidateFluidRateErrorLabelVisibility(false)
                .ValidateExpiryDateErrorLabelVisibility(false)

                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickSaveButton()

                .ValidateAmountGivenErrorLabelVisibility(true)
                .ValidateAmountGivenErrorLabelText("Please fill out this field.")
                .ValidateWhatWasGivenErrorLabelVisibility(true)
                .ValidateWhatWasGivenErrorLabelText("Please fill out this field.")
                .ValidateFluidRateErrorLabelVisibility(true)
                .ValidateFluidRateErrorLabelText("Please fill out this field.")
                .ValidateExpiryDateErrorLabelVisibility(true)
                .ValidateExpiryDateErrorLabelText("Please fill out this field.")
                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId3);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId4);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnFluidAmountGiven("a")
                .ValidateAmountGivenErrorLabelVisibility(true)
                .ValidateAmountGivenErrorLabelText("Please enter a value between 0 and 2147483647.")
                .InsertTextOnFluidAmountGiven("60")
                .ValidateAmountGivenErrorLabelVisibility(false)
                .InsertTextOnWhatWasGivenTextField("Other Fluid . . .")
                .InsertTextOnFluidRate("a")
                .ValidateFluidRateErrorLabelVisibility(true)
                .ValidateFluidRateErrorLabelText("Please enter a value between 0 and 2147483647.")
                .InsertTextOnFluidRate("60")
                .ValidateFluidRateErrorLabelVisibility(false)
                .InsertTextOnExpirydate(expiryDate.ToString("dd'/'/MM'/'yyyy"))
                .InsertTextOnAmountOfFluidDrank("60")
                .ValidateFluidGivenBySystemUserLinkText(_systemUserFullName)
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateAmountGivenErrorLabelVisibility(false)
                .ValidateWhatWasGivenErrorLabelVisibility(false)
                .ValidateFluidRateErrorLabelVisibility(false)
                .ValidateExpiryDateErrorLabelVisibility(false)
                .ValidateMandatoryFieldIsVisible("Amount Given (ml)")
                .ValidateMandatoryFieldIsVisible("What Was Given?")
                .ValidateMandatoryFieldIsVisible("Rate (ml/hr)")
                .ValidateMandatoryFieldIsVisible("Expiry Date")
                .ValidateMandatoryFieldIsVisible("Given By");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9238")]
        [Description("Step 9, 11 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod06()
        {
            #region Create System User Record

            var _systemUser2Name = "fafrostereduser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Food And Fluid", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);
            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("No equipment")[0];
            var equipment2Id = dbHelper.careEquipment.GetByName("Double-handled mug")[0];
            var equipment3Id = dbHelper.careEquipment.GetByName("Other")[0];
            var equipment4Id = dbHelper.careEquipment.GetByName("Bed pan")[0];
            var equipment5Id = dbHelper.careEquipment.GetByName("Walking Stick")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Unhappy")[0];
            var careWellbeing2Id = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #region Care Assistances Needed

            var careAssistanceNeeded1Id = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];
            var careAssistanceNeeded2Id = dbHelper.careAssistanceNeeded.GetByName("Independent")[0];

            #endregion

            #region Activities of Daily Living (ADL) / Domain of Need

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Step 9, 11 - Verify Additional Information Section, Care Needs Section and Save record with empty mandatory fields (Consent Given = Yes)

            //Step 9, 11
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var DateAndTimeOccurred = DateTime.Now.AddDays(-2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")

                .ClickSaveButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()

                .ValidateLocationErrorLabelVisibility(true)
                .ValidateEquipmentRequiredErrorLabelVisibility(true)
                .ValidateWellbeingErrorLabelVisibility(true)
                .ValidateAssistanceNeededErrorLabelVisibility(true)

                .ValidateLocationErrorLabelText("Please fill out this field.")
                .ValidateEquipmentRequiredErrorLabelText("Please fill out this field.")
                .ValidateWellbeingErrorLabelText("Please fill out this field.")
                .ValidateAssistanceNeededErrorLabelText("Please fill out this field.")

                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(false)
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(other_LocationId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLocationIfOtherVisibility(true)
                .ClickEquipmentsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .ValidateResultElementPresent(equipment1Id)
                .ValidateResultElementPresent(equipment2Id)
                .ValidateResultElementPresent(equipment3Id)
                .TypeSearchQuery("Bed pan")
                .TapSearchButton()
                .ValidateResultElementNotPresent(equipment4Id)
                .TypeSearchQuery("Walking Stick")
                .TapSearchButton()
                .ValidateResultElementNotPresent(equipment5Id)
                .TypeSearchQuery("")
                .TapSearchButton()
                .AddElementToList(equipment1Id)
                .AddElementToList(equipment2Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateEquipmentIfOtherVisibility(false)
                .ClickEquipmentsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment3Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateEquipmentIfOtherVisibility(true)
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing2Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(false)
                .ClickWellbeingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careWellbeing1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateActionTakenVisibility(true)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded2Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(false)
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateAssistanceAmountVisibility(true)

                .InsertTextOnTotalTimeSpentWithPerson("a")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(true)
                .ValidateTotalTimeSpentWithPersonErrorLabelText("Please enter a value between 1 and 1140.")
                .InsertTextOnTotalTimeSpentWithPerson("60")
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false)
                .ClickSaveButton();

            foodAndFluidRecordPage
                .ValidateLocationIfOtherErrorLabelVisibility(true)
                .ValidateEquipmentIfOtherErrorLabelVisibility(true)
                .ValidateActionTakenErrorLabelVisibility(true)
                .ValidateAssistanceAmountErrorLabelVisibility(true)
                .InsertTextOnLocationIfOther("Other Location")
                .InsertTextOnEquipmentIfOther("Other Equipment")
                .InsertTextOnActionTaken("Action Taken")
                .SelectAssistanceAmount("Some")
                .ClickSaveButton()

                .ValidateTopPageNotificationVisibility(true)
                .ValidateTopPageNotificationText("Some data is not correct. Please review the data in the Form.")
                .ValidateLocationErrorLabelVisibility(false)
                .ValidateLocationIfOtherErrorLabelVisibility(false)
                .ValidateEquipmentRequiredErrorLabelVisibility(false)
                .ValidateEquipmentIfOtherErrorLabelVisibility(false)
                .ValidateWellbeingErrorLabelVisibility(false)
                .ValidateActionTakenErrorLabelVisibility(false)
                .ValidateAssistanceNeededErrorLabelVisibility(false)
                .ValidateAssistanceAmountErrorLabelVisibility(false)
                .ValidateTotalTimeSpentWithPersonErrorLabelVisibility(false);

            foodAndFluidRecordPage
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUserId, _systemUserFullName)
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser2Name)
                .TapSearchButton()
                .AddElementToList(_systemUser2Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUserId, _systemUserFullName)
                .ValidateStaffRequired_SelectedElementLinkTextBeforeSave(_systemUser2Id, "Food And Fluid Rostered User 2");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAdditionalNotes("Additional Notes")
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePlanNeedDomainId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickIncludeInNextHandover_YesRadioButton()
                .ClickIncludeInNextHandover_NoRadioButton()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickFlagRecordForHandover_NoRadioButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9239")]
        [Description("Step 10, 12 from the original test - ACC-4032")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4032_UITestMethod07()
        {
            #region Create System User Record

            var _systemUser2Name = "fafrostereduser2";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "Food And Fluid", "Rostered User 2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 3);

            commonMethodsDB.CreateTeamMember(_defaultTeam, _systemUser2Id, new DateTime(2020, 1, 1), null);

            #endregion

            #region Food Amount Offered

            var foodAmountOfferedId1 = dbHelper.foodAmountOffered.GetByName("Other").First();

            #endregion

            #region Food Amount Eaten

            var foodAmountEatenId1 = dbHelper.foodAmountEaten.GetByName("All").First();

            #endregion

            #region Type of Fluids

            var typeOfFluidId1 = dbHelper.typeOfFluid.GetByName("Other").First();

            #endregion

            #region Fluid Amount Offered

            var fluidAmountOfferedId1 = dbHelper.fluidAmountOffered.GetByName("Other").First();

            #endregion

            #region Non-Oral Fluids Delivery

            var nonOralFluidDeliveryId1 = dbHelper.nonOralFluidDelivery.GetByName("NG - Nasogastric Tube").First();

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];
            var other_LocationId = dbHelper.carePhysicalLocation.GetByName("Other")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Other")[0];
            var equipment2Id = dbHelper.careEquipment.GetByName("No equipment")[0];
            var equipment3Id = dbHelper.careEquipment.GetByName("Double-handled mug")[0];

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

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Preferences

            dbHelper.cpPersonCarePreferences.CreateCpPersonCarePreferences(personId, _teamId, 14, "FAF " + _currentDateSuffix); //Food and Fluid = 14

            #endregion

            #region Meal Type

            var OtherMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Other").First();

            #endregion

            #region Alert and Hazard Type

            var AlertTypeId = commonMethodsDB.CreateAlertAndHazardType(_teamId, "ACC9188", new DateTime(2024, 1, 1), true);

            #endregion

            #region Alert and Hazard

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(personId, _teamId, 1,
                2, AlertTypeId, null, 6, null, new DateTime(2024, 7, 1), null, "ACC9188");

            #endregion

            #region Step 10, 12 - Verify Person's Voice Section, Handover Details Section and Save record all fields (Consent Given = Yes)

            //Step 10, 12
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var DateAndTimeOccurred = DateTime.Now.AddDays(-2);
            var ExpiryDate = DateTime.Now.AddDays(1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()

                .InsertTextOnDateAndTimeOccurred_Date(DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time(DateAndTimeOccurred.ToString("07:00"))
                .SelectConsentGiven("Yes")

                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(OtherMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnMealTypeOther("Other Meal Type")
                .InsertTextOnTypeOfFood("Type of Food . . .\r\n Food1")
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAmountOfFoodOther("Other food . . .")
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTypeOfFluidOther("Other fluid . . .")
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAmountOfFluidOther("Small glass")
                .InsertTextOnAmountOfFluidDrank("60")
                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnFluidAmountGiven("10")
                .InsertTextOnWhatWasGivenTextField("Other fluid was given")
                .InsertTextOnFluidRate("1")
                .InsertTextOnExpirydate(ExpiryDate.ToString("dd'/'MM'/'yyyy"))
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .AddElementToList(other_LocationId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnLocationIfOther("Other Location")
                .ClickEquipmentsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment1Id)
                .AddElementToList(equipment2Id)
                .AddElementToList(equipment3Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnEquipmentIfOther("Other Equipment")
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnActionTaken("Action Taken")
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("15")
                .ClickStaffRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUser2Name)
                .AddElementToList(_systemUser2Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAdditionalNotes("Additional Notes")
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePlanNeedDomainId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ClickBackButton();

            var cpPersonFoodAndFluidRecords = dbHelper.cpPersonFoodAndFluid.GetByPersonId(personId);
            Assert.AreEqual(1, cpPersonFoodAndFluidRecords.Count);
            var cpPersonFoodAndFluidRecordId = cpPersonFoodAndFluidRecords[0];

            foodAndFluidPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(cpPersonFoodAndFluidRecordId, true)
                .OpenRecord(cpPersonFoodAndFluidRecordId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateDateAndTimeOccurred_DateText(DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(DateAndTimeOccurred.ToString("07:00"))
                .ValidatePersonLinkText(person_fullName)
                .ValidateResponsibleTeamLinkText("FAF T1")
                .ValidatePreferencesText("FAF " + _currentDateSuffix)
                .ValidateConsentGivenSelectedText("Yes");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateSwallowAssessmentRatingText("6 Soft & Bite-sized - SB6")
                .ValidateMealTypeLinkText("Other")
                .ValidateMealTypeOtherText("Other Meal Type");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFoodText("Type of Food . . .\r\n Food1")
                .ValidateAmountOfFoodOfferedLinkText("Other")
                .ValidateAmountOfFoodOtherText("Other food . . .")
                .ValidateAmountOfFoodEatenLinkText("All");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidLinkText("Other")
                .ValidateTypeOfFluidOtherText("Other fluid . . .")
                .ValidateAmountOfFluidOfferedLinkText("Other")
                .ValidateAmountOfFluidOtherText("Small glass")
                .ValidateAmountOfFluidDrankText("60");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateNonOralFluidDeliveryLinkText("NG - Nasogastric Tube")
                .ValidateFluidAmountGivenText("10")
                .ValidateWhatWasGivenText("Other fluid was given")
                .ValidateFluidRateText("1")
                .ValidateExpirydateText(ExpiryDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateFluidGivenBySystemUserLinkText(_systemUserFullName);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateLocation_SelectedElementLinkText(other_LocationId, "Other")
                .ValidateLocationIfOtherText("Other Location")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Other")
                .ValidateEquipment_SelectedElementLinkText(equipment2Id, "No equipment")
                .ValidateEquipment_SelectedElementLinkText(equipment3Id, "Double-handled mug")
                .ValidateEquipmentIfOtherText("Other Equipment")
                .ValidateWellbeingLinkText("Unhappy")
                .ValidateActionTakenText("Action Taken")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonText("15")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateStaffRequired_SelectedElementLinkText(_systemUser2Id, "Food And Fluid Rostered User 2")
                .ValidateAdditionalNotesText("Additional Notes");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Joao had a Other Meal Type.\r\n" +
                "Joao was offered a Other food... of Type of Food... Food1, and ate All of it.\r\n" +
                "Joao was offered a Small glass of Other fluid..., and drank 60 millilitres.\r\n" +
                "Food And Fluid Rostered User 1 gave Joao 10 millilitres of Other fluid was given via a NG - Nasogastric Tube at a rate of 1 ml/hr.\r\n" +
                "Joao was in the Living Room and Other Location.\r\nJoao used the following equipment: Other Equipment, No equipment and Double-handled mug.\r\n" +
                "Joao came across as Unhappy.\r\nThe action taken was: Action Taken.\r\nJoao required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Joao was assisted by 2 colleague(s).\r\n" +
                "Overall I spent 15 minutes with Joao.\r\n" +
                "We would like to note that: Additional Notes.");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLinkedAdlCategories_SelectedElementLinkText(_carePlanNeedDomainId, "Acute");

            foodAndFluidRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked();

            attachmentsForFoodAndFluid
                .WaitForPageToLoad(true)
                .ValidateHeaderCellText("Title")
                .ValidateHeaderCellText("Document Type")
                .ValidateHeaderCellText("Document Sub Type")
                .ValidateHeaderCellText("Date");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickFlagRecordForHandover_YesRadioButton()
                .ClickSaveButton()
                .WaitForPageToLoad();

            handoverCommentsPage
                .WaitForHandoverCommentsToLoad("cppersonfoodandfluid");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9240")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Absent)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food and Fluid")]
        public void FoodAndFluid_ACC2136_UITestMethod01()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Absent")
                .InsertTextOnReasonForAbsence("Went to the hospital")
                .ClickSaveAndCloseButton();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonFoodAndFluid = dbHelper.cpPersonFoodAndFluid.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonFoodAndFluid.Count);
            var cpAllPersonFoodAndFluidId = allPersonFoodAndFluid[0];

            foodAndFluidPage
                .OpenRecord(cpAllPersonFoodAndFluidId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Joao.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Absent")
                .ValidateReasonForAbsenceText("Went to the hospital");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9241")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Declined)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food and Fluid")]
        public void FoodAndFluid_ACC2136_UITestMethod02()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("08:45")
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Declined")
                .InsertTextOnReasonConsentDeclined("Did not want to talk")
                .InsertTextOnEncouragementGiven("Explained the benefits of food and fluid")
                .ClickSaveAndCloseButton();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonFoodAndFluid = dbHelper.cpPersonFoodAndFluid.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonFoodAndFluid.Count);
            var cpAllPersonFoodAndFluidId = allPersonFoodAndFluid[0];

            foodAndFluidPage
                .OpenRecord(cpAllPersonFoodAndFluidId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText("08:45")
                .ValidatePreferencesText("No preferences recorded, please check with Joao.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Declined")
                .ValidateReasonConsentDeclinedText("Did not want to talk")
                .ValidateEncouragementGivenText("Explained the benefits of food and fluid")
                .ValidateCareProvidedWithoutConsent_NoRadioButtonChecked();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-9242")]
        [Description("Set data in all mandatory fields and save the record (Consent Given = No & Non-consent Detail = Deferred)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food and Fluid")]
        public void FoodAndFluid_ACC2136_UITestMethod03()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Joao";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Care Periods

            var carePeriodId = commonMethodsDB.CreateCareProviderCarePeriodSetup(_defaultTeam, "7 AM", new TimeSpan(7, 0, 0));

            #endregion


            #region Set data in all mandatory fields and save the record

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var dateOccurred = DateTime.Now.AddDays(-1);
            var deferredToDate = DateTime.Now.AddDays(2);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred_Date(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .SelectConsentGiven("No")
                .SelectNonConsentDetail("Deferred")
                .InsertDeferredToDate(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .SelectDeferredToTimeOrShift("Time")
                .InsertDeferredToTime("08:45")
                .ClickSaveAndCloseButton();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var allPersonFoodAndFluid = dbHelper.cpPersonFoodAndFluid.GetByPersonId(personId);
            Assert.AreEqual(1, allPersonFoodAndFluid.Count);
            var cpAllPersonFoodAndFluidId = allPersonFoodAndFluid[0];

            foodAndFluidPage
                .OpenRecord(cpAllPersonFoodAndFluidId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Joao.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Time")
                .ValidateDeferredToTimeText("08:45");

            foodAndFluidRecordPage
                .SelectDeferredToTimeOrShift("Shift")
                .ClickDeferredToShiftLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("7 AM", carePeriodId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .OpenRecord(cpAllPersonFoodAndFluidId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidatePersonLinkText(person_fullName)
                .ValidateDateAndTimeOccurred_DateText(dateOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidatePreferencesText("No preferences recorded, please check with Joao.")
                .ValidateConsentGivenSelectedText("No")
                .ValidateNonConsentDetailSelectedText("Deferred")
                .ValidateDeferredToDateText(deferredToDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeferredToTimeOrShiftSelectedText("Shift")
                .ValidateDeferredToShiftLinkText("7 AM");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-9189

        [TestProperty("JiraIssueID", "ACC-4073")]
        [Description("All step(s) from the test - ACC-4073")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Care Plan")]
        [TestProperty("Screen", "Food And Fluid")]
        public void FoodAndFluid_ACC4073_UITestMethod01()
        {
            #region Food Amount Offered

            var foodAmountOfferedId1 = dbHelper.foodAmountOffered.GetByName("Bowl").First();

            #endregion

            #region Food Amount Eaten

            var foodAmountEatenId1 = dbHelper.foodAmountEaten.GetByName("Most").First();

            #endregion

            #region Type of Fluids

            var typeOfFluidId1 = dbHelper.typeOfFluid.GetByName("Fruit Juice / Smoothie").First();

            #endregion

            #region Fluid Amount Offered

            var fluidAmountOfferedId1 = dbHelper.fluidAmountOffered.GetByName("Mug").First();

            #endregion

            #region Non-Oral Fluids Delivery

            var nonOralFluidDeliveryId1 = dbHelper.nonOralFluidDelivery.GetByName("NG - Nasogastric Tube").First();

            #endregion

            #region Care Physical Locations 

            var livingroom_LocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Equipment 

            var equipment1Id = dbHelper.careEquipment.GetByName("Double-handled mug")[0];

            #endregion

            #region Care Wellbeing

            var careWellbeing1Id = dbHelper.careWellbeing.GetByName("Happy")[0];

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

            var firstName = "Chris";
            var lastName = _currentDateSuffix;
            var person_fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];

            #endregion

            #region Meal Type

            var LunchMealTypeId = dbHelper.foodAndFluidMealType.GetByName("Lunch").First();

            #endregion

            #region Alert and Hazard Type

            var AlertTypeId = commonMethodsDB.CreateAlertAndHazardType(_teamId, "ACC9189", new DateTime(2024, 2, 2), true);

            #endregion

            #region Alert and Hazard

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(personId, _teamId, 1,
                2, AlertTypeId, null, 6, null, new DateTime(2024, 7, 1), null, "ACC9189");

            #endregion

            #region Step 1 - 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToFoodAndFluidPage();

            foodAndFluidPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var DateAndTimeOccurred = DateTime.Now.AddDays(-2);
            var ExpiryDate = DateTime.Now.AddDays(1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()

                .InsertTextOnDateAndTimeOccurred_Date(DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time(DateAndTimeOccurred.ToString("07:00"))
                .SelectConsentGiven("Yes")

                .ClickMealTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(LunchMealTypeId);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnTypeOfFood("Semi Solid Food")
                .ClickAmountOfFoodOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFoodEatenLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(foodAmountEatenId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickTypeOfFluidLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(typeOfFluidId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAmountOfFluidOfferedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(fluidAmountOfferedId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnAmountOfFluidDrank("50")
                .ClickNonOralFluidDeliveryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(nonOralFluidDeliveryId1);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .InsertTextOnFluidAmountGiven("10")
                .InsertTextOnWhatWasGivenTextField("Fluid was given")
                .InsertTextOnFluidRate("1")
                .InsertTextOnExpirydate(ExpiryDate.ToString("dd'/'MM'/'yyyy"))
                .ClickLocationLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(livingroom_LocationId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickEquipmentsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(equipment1Id)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careWellbeing1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(careAssistanceNeeded1Id);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .InsertTextOnTotalTimeSpentWithPerson("30")
                .InsertTextOnAdditionalNotes("Additional Notes")
                .ClickLinkedActivitiesOfDailyLivingLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(_carePlanNeedDomainId)
                .TapOKButton();

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ClickBackButton();

            var cpPersonFoodAndFluidRecords = dbHelper.cpPersonFoodAndFluid.GetByPersonId(personId);
            Assert.AreEqual(1, cpPersonFoodAndFluidRecords.Count);
            var cpPersonFoodAndFluidRecordId = cpPersonFoodAndFluidRecords[0];

            foodAndFluidPage
                .WaitForPageToLoad()
                .ValidateRecordPresent(cpPersonFoodAndFluidRecordId, true);

            //wait for main menu to load and click advanced search button

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .ClickAdvancedSearchButton();

            //wait for advanced search page to load and select Food and Fluid Record Type

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Food And Fluid")
                .ValidateSelectFieldOption("Person")
                .SelectFilter("1", "Person")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Date and Time Occurred")
                .SelectFilter("1", "Consent Given?")
                .SelectFilter("1", "Non-consent Detail")
                .SelectFilter("1", "Care provided without consent?")
                .SelectFilter("1", "Meal Type")
                .SelectFilter("1", "Amount of Food Offered")
                .SelectFilter("1", "Amount of Food Eaten")
                .SelectFilter("1", "Amount of Fluid Offered")
                .SelectFilter("1", "Amount of Fluid Drank (ml)")
                .SelectFilter("1", "Delivery Method")
                .SelectFilter("1", "Amount Given (ml)")
                .SelectFilter("1", "Rate (ml/hr)")
                .SelectFilter("1", "Given By")
                .SelectFilter("1", "Wellbeing")
                .SelectFilter("1", "Assistance Amount?")
                .SelectFilter("1", "Flag record for handover");

            advanceSearchPage
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(personNumber.ToString())
                .TapSearchButton()
                .SelectResultElement(personId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(cpPersonFoodAndFluidRecordId.ToString());

            advanceSearchPage
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Responsible Team")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("FAF T1")
                .TapSearchButton()
                .SelectResultElement(_teamId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(12)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(12)
                .ValidateSearchResultRecordPresent(cpPersonFoodAndFluidRecordId.ToString())
                .ClickBackButton_ResultsPage();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Amount of Fluid Drank (ml)")
                .SelectOperator("1", "Is Greater Than or Equal To")
                .InsertRuleValueText("1", "50")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(12)
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(12)
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(cpPersonFoodAndFluidRecordId.ToString())
                .OpenRecord(cpPersonFoodAndFluidRecordId.ToString());

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateDateAndTimeOccurred_DateText(DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy"))
                .ValidateDateAndTimeOccurred_TimeText(DateAndTimeOccurred.ToString("07:00"))
                .ValidatePersonLinkText(person_fullName)
                .ValidateResponsibleTeamLinkText("FAF T1")
                .ValidatePreferencesText("No preferences recorded, please check with Chris.")
                .ValidateConsentGivenSelectedText("Yes");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateSwallowAssessmentRatingText("6 Soft & Bite-sized - SB6")
                .ValidateMealTypeLinkText("Lunch");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFoodText("Semi Solid Food")
                .ValidateAmountOfFoodOfferedLinkText("Bowl")
                .ValidateAmountOfFoodEatenLinkText("Most");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateTypeOfFluidLinkText("Fruit Juice / Smoothie")
                .ValidateAmountOfFluidOfferedLinkText("Mug")
                .ValidateAmountOfFluidDrankText("50");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateNonOralFluidDeliveryLinkText("NG - Nasogastric Tube")
                .ValidateFluidAmountGivenText("10")
                .ValidateWhatWasGivenText("Fluid was given")
                .ValidateFluidRateText("1")
                .ValidateExpirydateText(ExpiryDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateFluidGivenBySystemUserLinkText(_systemUserFullName);

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLocation_SelectedElementLinkText(livingroom_LocationId, "Living Room")
                .ValidateEquipment_SelectedElementLinkText(equipment1Id, "Double-handled mug")
                .ValidateWellbeingLinkText("Happy")
                .ValidateAssistanceNeededLinkText("Asked For Help")
                .ValidateAssistanceAmountSelectedText("Some")
                .ValidateTotalTimeSpentWithPersonText("30")
                .ValidateStaffRequired_SelectedElementLinkText(_systemUserId, _systemUserFullName)
                .ValidateAdditionalNotesText("Additional Notes");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateCareNoteText("Chris had a Lunch.\r\n" +
                "Chris was offered a Bowl of Semi Solid Food, and ate Most of it.\r\n" +
                "Chris was offered a Mug of Fruit Juice / Smoothie, and drank 50 millilitres.\r\n" +
                "Food And Fluid Rostered User 1 gave Chris 10 millilitres of Fluid was given via a NG - Nasogastric Tube at a rate of 1 ml/hr.\r\n" +
                "Chris was in the Living Room.\r\n" +
                "Chris used the following equipment: Double-handled mug.\r\n" +
                "Chris came across as Happy.\r\n" +
                "Chris required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + DateAndTimeOccurred.ToString("dd'/'MM'/'yyyy") + " 07:00:00.\r\n" +
                "Chris was assisted by 1 colleague(s).\r\n" +
                "Overall I spent 30 minutes with Chris.\r\n" +
                "We would like to note that: Additional Notes.");

            foodAndFluidRecordPage
                .WaitForPageToLoad()
                .ValidateLinkedAdlCategories_SelectedElementLinkText(_carePlanNeedDomainId, "Acute");

            foodAndFluidRecordPage
                .ValidateIncludeInNextHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
                .ValidateFlagRecordForHandover_NoRadioButtonChecked()
                .ValidateIncludeInNextHandover_YesRadioButtonNotChecked();

            #endregion

        }

        #endregion
    }
}