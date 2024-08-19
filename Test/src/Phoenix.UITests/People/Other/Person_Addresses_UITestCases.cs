using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Addresses_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;


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

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonAddress BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PersonAddress T1", null, _businessUnitId, "907678", "PersonAddressT1@careworkstempmail.com", "PersonAddress T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PersonAddressUser1

                _systemUsername = "PersonAddressUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PersonAddress", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-3731

        [TestProperty("JiraIssueID", "CDV6-25423")]
        [Description("Step(s) 1 to 8 from the original test CDV6-5299")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Addresses_UITestMethod01()
        {
            #region Person

            var _firstName = "Karl";
            var _lastName = commonMethodsHelper.GetCurrentDateTimeString();
            var _personFullName = _firstName + " " + _lastName;
            var addresstypeid = 6; //Home
            var personID = commonMethodsDB.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 1),
                _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Person Address

            var personAddressId = dbHelper.personAddress.GetByPersonId(personID).First();

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("PersonAddressUser1", "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAddressPage();

            personAddressesPage
                .WaitForPersonAddressesPageToLoad()
                .ValidateRecordCellText(personAddressId, 2, "20/10/2020")
                .ValidateRecordCellText(personAddressId, 4, "Home");

            #endregion

            #region Step 3

            personAddressesPage
                .ClickAddNewButton();

            personAddressRecordPage
                .WaitForPersonAddressRecordPageToLoad()
                .SelectAddresstype("Home")
                .InsertTextOnStartDate("20/10/2020");

            #endregion

            #region Step 4

            personAddressRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();

            #endregion

            #region Step 6

            personAddressRecordPage
                .InsertTextOnStartDate("18/10/2020")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();

            #endregion

            #region Step 7 and 8

            personAddressRecordPage
                .InsertTextOnStartDate("21/10/2020")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();


            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25424")]
        [Description("Step(s) 9 to last step from the original test CDV6-5299")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Addresses_UITestMethod02()
        {
            #region Person

            var _firstName = "Karl";
            var _lastName = commonMethodsHelper.GetCurrentDateTimeString();
            var _personFullName = _firstName + " " + _lastName;
            var addresstypeid = 6; //Home
            var personID = commonMethodsDB.CreatePersonRecord("", _firstName, "", _lastName, "", new DateTime(2000, 1, 1),
                _ethnicityId, _teamId, new DateTime(2020, 9, 21), addresstypeid, 1, "9876543210", "", "", "",
                "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Person Address

            var personAddressId = dbHelper.personAddress.GetByPersonId(personID).First();

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login("PersonAddressUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), _personFullName)
                .SelectAddressType("Business")
                .InsertStartDateOfAddress("21/10/2020");

            #endregion

            #region Step 10

            personRecordEditPage
                .TapSaveAndCloseButton();

            #endregion

            #region Step 11

            addressActionPopUp
                .WaitForAddressActionPopUpToLoad()
                .SelectViewByText("Create new address record")
                .TapOkButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), _personFullName);

            #endregion

            #region Step 12

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), _personFullName)
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAddressPage();

            personAddressesPage
                .WaitForPersonAddressesPageToLoad();

            #endregion

            #region Step 13

            personAddressesPage
                .OpenRecord(personAddressId.ToString());

            personAddressRecordPage
                .WaitForPersonAddressRecordPageToLoad()
                .ValidateAddresstypeSelectedText("Home")
                .ValidateStartDateText("21/09/2020")
                .ValidateEndDateText("21/10/2020");

            var personAddressInactive = (bool)(dbHelper.personAddress.GetPersonAddressById(personAddressId, "inactive")["inactive"]);
            Assert.IsTrue(personAddressInactive);

            #endregion

            #region Step 14

            personAddressRecordPage
                .ClickBackButton();

            personAddressesPage
                .WaitForPersonAddressesPageToLoad()
                .ClickAddNewButton();

            personAddressRecordPage
                .WaitForPersonAddressRecordPageToLoad()
                .SelectAddresstype("Home")
                .InsertTextOnStartDate("21/10/2020");

            #endregion

            #region Step 15

            personAddressRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();

            #endregion

            #region Step 16

            personAddressRecordPage
                .InsertTextOnStartDate("20/10/2020");

            #endregion

            #region Step 17

            personAddressRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();

            #endregion

            #region Step 18

            personAddressRecordPage
                .InsertTextOnStartDate("22/10/2020");

            #endregion

            #region Step 19 and 20

            personAddressRecordPage
                .ClickSaveAndCloseButton();

            personAddressesPage
                .WaitForPersonAddressesPageToLoad()
                .ClickRefreshButton();

            var allPersonAddresses = dbHelper.personAddress.GetByPersonId(personID);
            Assert.AreEqual(3, allPersonAddresses.Count);

            #endregion

            #region Step 21

            var newPersonAddressId = dbHelper.personAddress.GetPersonAddress(personID, 6, new DateTime(2020, 10, 22)).First();

            personAddressesPage
                .OpenRecord(newPersonAddressId.ToString());

            personAddressRecordPage
                .WaitForPersonAddressRecordPageToLoad()
                .SelectAddresstype("Business");

            #endregion

            #region Step 22

            personAddressRecordPage
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Address Dates overlap with another Address of the same Type").TapCloseButton();

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
