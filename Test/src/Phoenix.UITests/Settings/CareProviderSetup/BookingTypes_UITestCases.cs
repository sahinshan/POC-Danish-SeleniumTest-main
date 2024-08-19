using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class BookingTypes_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private TimeZone _localZone;
        private string _tenantName;
        private string EnvironmentName;
        private string _currentDateTimeSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Booking Types");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Booking Types T1", null, _businessUnitId, "907643", "BookingTypesT1@careworkstempmail.com", "BookingTypes T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region System User

                _systemUsername = "BookingTypeUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "BookingTypeUser", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-5901

        [TestProperty("JiraIssueID", "ACC-5945")]
        [Description("Step(s) 1 to 9 from the original jira test ACC-5900")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Types")]
        public void BookingTypes_UITestMethod001()
        {
            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName = "BTC1_5900_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName)
                .SelectBookingTypeClass("Booking (to location)")
                .ValidateWorkingContractedTimeSelectedText("Count full booking length")
                .ClickSaveButton();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to location)")
                .ValidateClashActionsTabVisibility(true)
                .NavigateToClashActionsTab();

            var cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName).First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            var cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            Console.WriteLine("Total Actions Count:" + cpBookingTypeClashActionLists.Count);

            var first = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[0], "name")["name"];
            var second = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[1], "name")["name"];
            var third = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[2], "name")["name"];
            var fourth = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[3], "name")["name"];
            var fifth = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[4], "name")["name"];
            var sixth = (string)dbHelper.cpBookingTypeClashAction.GetCPBookingTypeClashActionByID(cpBookingTypeClashActionLists[5], "name")["name"];

            Console.WriteLine("Total Actions Count:" + first.ToString());
            Console.WriteLine("Total Actions Count:" + second.ToString());
            Console.WriteLine("Total Actions Count:" + third.ToString());
            Console.WriteLine("Total Actions Count:" + fourth.ToString());
            Console.WriteLine("Total Actions Count:" + fifth.ToString());
            Console.WriteLine("Total Actions Count:" + sixth.ToString());

            bookingTypeClashActionsPage
                .OpenRecord(cpBookingTypeClashActionLists[0].ToString());

            #endregion

            #region Step 5

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ValidateBookingTypeFieldIsMandatory(true)
                .ValidateBookingTypeClassFieldIsMandatory(true)
                .ValidateGlobalFieldIsMandatory(false)
                .ValidateThisBookingFieldIsMandatory(true);

            #endregion

            #region Step 6

            bookingTypeClashActionRecordPage
                .ValidateBookingTypeLinkText(bookingTypeName);

            #endregion

            #region Step 7

            bookingTypeClashActionRecordPage
                .ValidateBookingTypeLookupButtonIsDisabled(true)
                .ValidateBookingTypeRemoveButtonVisibility(false);

            #endregion

            #region Step 9

            bookingTypeClashActionRecordPage
                .ClickThisBookingDropDown()
                .ValidateThisBookingDropDownText("Allow")
                .ValidateThisBookingDropDownText("Warn Only")
                .ValidateThisBookingDropDownText("Prevent")
                .ClickThisBookingDropDown();

            #endregion



        }

        [TestProperty("JiraIssueID", "ACC-5946")]
        [Description("Step(s) 10 to 20 from the original jira test ACC-5900")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Types")]
        public void BookingTypes_UITestMethod002()
        {
            #region Step 10 for BTC - 1 & Step 14 to 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName_1 = "BTC-1_5900_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName_1)
                .SelectBookingTypeClass("Booking (to location)")
                .ValidateWorkingContractedTimeSelectedText("Count full booking length")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to location)")
                .NavigateToClashActionsTab();

            var cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName_1).First();
            var cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            var BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            var BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            var BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            var BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            var BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            var BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent")

                .OpenRecord(BookingToLocationId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()

                .ValidateBookingTypeClassPickListIsDisabled(true)
                .ValidateGlobalPickListIsDisabled(true)
                .ValidateThisBookingPickListIsDisabled(false)

                .SelectThisBooking("Warn Only")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Warn Only");

            #endregion

            #region Step 11 for BTC - 2 & Step 14 to 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName_2 = "BTC-2_5900_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName_2)
                .SelectBookingTypeClass("Booking (to internal care activity)")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Booking Charge Type has not been recorded, but it will need a value before Finance Transactions for charging can be correctly generated")
                .TapCloseButton();

            bookingTypeRecordPage
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to internal care activity)")
                .NavigateToClashActionsTab();

            cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName_2).First();
            cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent")

                .OpenRecord(BookingToLocationId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()

                .ValidateBookingTypeClassPickListIsDisabled(true)
                .ValidateGlobalPickListIsDisabled(true)
                .ValidateThisBookingPickListIsDisabled(false)

                .SelectThisBooking("Prevent")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Prevent");

            #endregion

            #region Step 12 for BTC - 3 & Step 14 to 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName_3 = "BTC-3_5900_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName_3)
                .SelectBookingTypeClass("Booking (to external care activity)")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Booking Charge Type has not been recorded, but it will need a value before Finance Transactions for charging can be correctly generated")
                .TapCloseButton();

            bookingTypeRecordPage
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to external care activity)")
                .NavigateToClashActionsTab();

            cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName_3).First();
            cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent")

                .OpenRecord(BookingToLocationId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()

                .ValidateBookingTypeClassPickListIsDisabled(true)
                .ValidateGlobalPickListIsDisabled(true)
                .ValidateThisBookingPickListIsDisabled(false)

                .SelectThisBooking("Prevent")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Prevent");

            #endregion

            #region Step 13 for BTC - 4 & Step 14 to 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName_4 = "BTC-4_5900_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName_4)
                .SelectBookingTypeClass("Booking (to internal non-care booking e.g. annual leave, training)")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (to internal non-care booking e.g. annual leave, training)")
                .NavigateToClashActionsTab();

            cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName_4).First();
            cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent")

                .OpenRecord(BookingToLocationId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .SelectThisBooking("Warn Only")

                .ValidateBookingTypeClassPickListIsDisabled(true)
                .ValidateGlobalPickListIsDisabled(true)
                .ValidateThisBookingPickListIsDisabled(false)

                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Warn Only");

            #endregion

            #region Step 17

            bookingTypeClashActionsPage
                .ClickNewRecordButton();

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ValidateBookingTypeLinkText(bookingTypeName_4)
                .SelectBookingTypeClass("Booking (to internal care activity)")
                .SelectThisBooking("Prevent")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An action for this booking type class already exists.")
                .TapCloseButton();

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 18

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ClickNewRecordButton();

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ValidateBookingTypeLinkText(bookingTypeName_4)
                .SelectBookingTypeClass("Booking (to location)")
                .SelectThisBooking("Prevent")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An action for this booking type class already exists.")
                .TapCloseButton();

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 20

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateCellText(1, 2, "Booking (Service User non-care booking)")
                .ValidateCellText(2, 2, "Booking (to external care activity)")
                .ValidateCellText(3, 2, "Booking (to internal care activity)")
                .ValidateCellText(4, 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateCellText(5, 2, "Booking (to location)")
                .ValidateCellText(6, 2, "Booking (To Service User)");

            #endregion

            #region Step 20

            bookingTypeClashActionsPage
                .ValidateDeleteRecordButtonIsPresent(false)
                .OpenRecord(BookingToLocationId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .ValidateDeleteRecordButtonIsPresent(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5904

        [TestProperty("JiraIssueID", "ACC-5902")]
        [Description("Step(s) 1 to 6 from the original jira test ACC-5902")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Types")]
        public void BookingTypes_UITestMethod003()
        {
            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName_1 = "BTC-1_5902_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnName(bookingTypeName_1)
                .SelectBookingTypeClass("Booking (to location)")
                .ValidateWorkingContractedTimeFieldVisibility(true)
                .ValidateGeneralAndStaffSectionPosition()
                .ValidateWorkingContractedTimeSelectedText("Count full booking length")
                .ValidateWorkingContractedTimeDropDownText("Count full booking length")
                .ValidateWorkingContractedTimeDropDownText("Cap Booking length")
                .ValidateWorkingContractedTimeDropDownText("Don’t include in \"Working\" hours");

            #endregion

            #region Step 4

            bookingTypeRecordPage
                .ValidateCapDurationMinutesFieldVisibility(false)
                .SelectWorkingContractedTime("Don’t include in \"Working\" hours")
                .ValidateCapDurationMinutesFieldVisibility(false)

                .SelectWorkingContractedTime("Cap Booking length")
                .ValidateCapDurationMinutesFieldVisibility(true);

            #endregion

            #region Step 5

            bookingTypeRecordPage
                .InsertTextOnCapDurationMinutes("-1")
                .ValidateCapDurationMinutesFormErrorText("Please enter a value between 0 and 1440.")
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            bookingTypeRecordPage
                .InsertTextOnCapDurationMinutes("1441")
                .ValidateCapDurationMinutesFormErrorText("Please enter a value between 0 and 1440.")
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.");

            bookingTypeRecordPage
                .InsertTextOnCapDurationMinutes("1000")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidatePageHeaderTitle(bookingTypeName_1);

            #endregion

            #region Step 6

            bookingTypeRecordPage
                .ClickBackButton();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .SelectBookingTypeClass("Booking (to location)")
                .ValidateWorkingContractedTimeFieldVisibility(true)

                .SelectBookingTypeClass("Booking (to internal care activity)")
                .ValidateWorkingContractedTimeFieldVisibility(true)

                .SelectBookingTypeClass("Booking (to external care activity)")
                .ValidateWorkingContractedTimeFieldVisibility(true)

                .SelectBookingTypeClass("Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateWorkingContractedTimeFieldVisibility(true)

                .SelectBookingTypeClass("Booking (To Service User)")
                .ValidateWorkingContractedTimeFieldVisibility(true)

                .SelectBookingTypeClass("Booking (Service User non-care booking)")
                .ValidateWorkingContractedTimeFieldVisibility(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6494

        [TestProperty("JiraIssueID", "ACC-6487")]
        [Description("Step(s) 1 to 6 from the original jira test ACC-6487")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Types")]
        public void BookingTypes_ACC_6487_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            #endregion

            #region Step 3

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName = "BTC5_6487_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad();

            #endregion

            #region Step 4

            bookingTypeRecordPage
                .InsertTextOnName(bookingTypeName)
                .SelectBookingTypeClass("Booking (To Service User)")
                .ValidateWorkingContractedTimeSelectedText("Count full booking length")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Booking Charge Type has not been recorded, but it will need a value before Finance Transactions for charging can be correctly generated")
                .TapCloseButton();

            bookingTypeRecordPage
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (To Service User)")
                .ValidateClashActionsTabVisibility(true)
                .NavigateToClashActionsTab();

            #endregion

            #region Step 5

            var cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName).First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            var cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            var BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            var BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            var BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            var BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            var BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            var BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Allow")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Allow");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent");

            #endregion

            #region Step 6

            bookingTypeClashActionsPage
                .OpenRecord(BookingToInternalCareActivityId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .SelectThisBooking("Warn Only")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Warn Only");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-6495")]
        [Description("Step(s) 1 to 6 from the original jira test ACC-6495")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Booking Types")]
        public void BookingTypes_ACC_6495_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            #endregion

            #region Step 3

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .ClickNewRecordButton();

            var bookingTypeName = "BTC6_6495_" + _currentDateTimeSuffix;

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad();

            #endregion

            #region Step 4

            bookingTypeRecordPage
                .InsertTextOnName(bookingTypeName)
                .SelectBookingTypeClass("Booking (Service User non-care booking)")
                .ValidateWorkingContractedTimeSelectedText("Count full booking length")
                .ValidateIspersonabsence_NoOptionChecked()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .ValidateBookingTypeClassSelectedText("Booking (Service User non-care booking)")
                .ValidateClashActionsTabVisibility(true)
                .NavigateToClashActionsTab();

            #endregion

            #region Step 5

            var cpBookingTypeId = dbHelper.cpBookingType.GetByName(bookingTypeName).First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            var cpBookingTypeClashActionLists = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeId(cpBookingTypeId);
            Assert.AreEqual(6, cpBookingTypeClashActionLists.Count);

            var BookingToLocationId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to location)").First();
            var BookingToInternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal care activity)").First();
            var BookingToExternalCareActivityId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to external care activity)").First();
            var BookingToInternalNonCareBookingEGAnnualLeaveTrainingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (to internal non-care booking e.g. annual leave, training)").First();
            var BookingToServiceUserId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (To Service User)").First();
            var BookingServiceUserNonCareBookingId = dbHelper.cpBookingTypeClashAction.GetByCPBookingTypeIdAndContainsName(cpBookingTypeId, "Booking (Service User non-care booking)").First();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad();

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 2, "Booking (to location)")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToLocationId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 2, "Booking (to external care activity)")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToExternalCareActivityId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 2, "Booking (to internal non-care booking e.g. annual leave, training)")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalNonCareBookingEGAnnualLeaveTrainingId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 2, "Booking (To Service User)")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToServiceUserId.ToString(), 4, "Prevent");

            bookingTypeClashActionsPage
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 2, "Booking (Service User non-care booking)")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingServiceUserNonCareBookingId.ToString(), 4, "Prevent");

            #endregion

            #region Step 6

            bookingTypeClashActionsPage
                .OpenRecord(BookingToInternalCareActivityId.ToString());

            bookingTypeClashActionRecordPage
                .WaitForBookingTypeClashActionRecordPageToLoad()
                .SelectThisBooking("Allow")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateThisBookingSelectedText("Allow")
                .ClickBackButton();

            bookingTypeClashActionsPage
                .WaitForBookingTypeClashActionsPageToLoad()
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 2, "Booking (to internal care activity)")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 3, "Prevent")
                .ValidateRecordCellContent(BookingToInternalCareActivityId.ToString(), 4, "Allow");

            #endregion

        }

        #endregion

    }
}
