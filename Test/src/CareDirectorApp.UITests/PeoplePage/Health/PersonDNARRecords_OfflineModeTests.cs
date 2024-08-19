using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using AtlassianServiceAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace CareDirectorApp.UITests.People.Health
{
    /// <summary>
    /// This class contains all test methods for Person DNAR Records while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonDNARRecords_OfflineModeTests : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");

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
                   .InsertUserName("Mobile_Test_User_1")
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
                    .InsertUserName("Mobile_Test_User_1")
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

            //if the APP is in offline mode change it to online mode
            settingsPage.SetTheAppInOnlineMode();
        }

        #region Person Health->DNAR Records page
        [Test]
        [Property("JiraIssueID", "CDV6-13673")]
        [Description("Verify the View DNAR record in Mobile App" +
            "Login CD App -> Work Place -> People -> Health -> Records of DNAR.Should display existing DNAR records if any.Verify the View options.Should display 2 Options Active Records and Inactive Records")]
        public void PersonDNARRecords_OfflineTestMethod01()
        {
            var p1 = PlatformServicesHelper.person.GetPersonByName("Pavel", "", "MCNamara", "personid");
            Guid personID1 = (Guid)p1["personid"];

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
               .WaitForPeoplePageToLoad("My Pinned People")
               .TypeInSearchTextBox("MCNamara")
               .TapSearchButton()
               .TapOnPersonRecordButton("Pavel MCNamara", personID1.ToString());

           
            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthArea_RecordsOfDNAR_RelatedItems();

            personDNARRecordPage
                .WaitForPersonDNARRecordPageToLoad("RECORDS OF DNAR")
                .TapViewOptionsLookupButton();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personDNARRecordPage
                .WaitForPersonDNARRecordPageToLoad("RECORDS OF DNAR")
                .TapInactiveViewOptionsLookupButton();

            pickList
                .WaitForPickListToLoad()
                .ScrollDownPicklist(1)
                .TapOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-13676")]
        [Description("Verify the View  “Active Records“ of DNAR in App.Pre Requisites:Person with existing Active DNAR record.")]
        public void PersonVerifyDNARActiveRecords_OfflineTestMethod02()
        {

            var p1 = PlatformServicesHelper.person.GetPersonByName("Pavel", "", "MCNamara", "personid");
            Guid personID1 = (Guid)p1["personid"];
            //Guid personDNARRecordId = this.PlatformServicesHelper.personDNAR.GetPersonDNARIdForPerson(personID1, false)[0];

            foreach (Guid personDNARRecordid in this.PlatformServicesHelper.personDNAR.GetPersonDNARIdForPerson(personID1))
                this.PlatformServicesHelper.personDNAR.DeletePersonDNAR(personDNARRecordid);

            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            Guid Mobile_test_user_1_userid = PlatformServicesHelper.systemUser.GetSystemUserByUserName("mobile_test_user_1")[0];
            DateTime completeddate = DateTime.Now;
            DateTime DNARDateTime = DateTime.Now;

            Guid personDNARRecordId = PlatformServicesHelper.personDNAR.CreatePersonDNARRecord(OwnerID,"DNAR Record Test",false,personID1,"additional information" ,completeddate, Mobile_test_user_1_userid, DNARDateTime);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
               .WaitForPeoplePageToLoad("My Pinned People")
               .TypeInSearchTextBox("MCNamara")
               .TapSearchButton()
               .TapOnPersonRecordButton("Pavel MCNamara", personID1.ToString());


            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthArea_RecordsOfDNAR_RelatedItems();

            personDNARRecordPage
                .WaitForPersonDNARRecordPageToLoad("RECORDS OF DNAR")
                .TapOnActiveDNARRecord("Pavel MCNamara",personDNARRecordId.ToString());

            personDNARActiveNInactiveRecordPage
               .WaitForPersoDNARActiveINactiveRecordPageToLoad()
               .ValidatePersonFieldText("Pavel MCNamara")
               .ValidateCancelledDecisionFieldText("No");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-13677")]
        [Description("Verify the View  “InActive Records“ of DNAR in App.Pre Requisites:Person with existing InActive DNAR record.")]
        public void PersonVerifyDNARActiveRecords_OfflineTestMethod03()
        {

            var p1 = PlatformServicesHelper.person.GetPersonByName("Pavel", "", "MCNamara", "personid");
            Guid personID1 = (Guid)p1["personid"];
            //Guid personDNARRecordId = this.PlatformServicesHelper.personDNAR.GetPersonDNARIdForPerson(personID1, false)[0];

            foreach (Guid personDNARRecordid in this.PlatformServicesHelper.personDNAR.GetPersonDNARIdForPerson(personID1))
                this.PlatformServicesHelper.personDNAR.DeletePersonDNAR(personDNARRecordid);

            Guid OwnerID = this.PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            Guid Mobile_test_user_1_userid = PlatformServicesHelper.systemUser.GetSystemUserByUserName("mobile_test_user_1")[0];
            DateTime completeddate = DateTime.Now;
            DateTime DNARDateTime = DateTime.Now;
            var basedate = DateTime.Now.Date;
            DateTime cancelleddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            // DateTime cancelleddate=DateTime.Now.Date;

            Guid personDNARInactiveRecordId = PlatformServicesHelper.personDNAR.CreatePersonDNARRecord(OwnerID, "DNAR Record Test", false, personID1, "additional information",cancelleddate,completeddate,true, Mobile_test_user_1_userid, Mobile_test_user_1_userid, DNARDateTime);
            PlatformServicesHelper.personDNAR.UpdateCancelDecision(personDNARInactiveRecordId, true);

            var datefield = PlatformServicesHelper.personDNAR.GetPersonDNARByID(personDNARInactiveRecordId, "cancelleddate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();
            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["cancelleddate"]);
            string CancelledDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
               .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID1.ToString());


            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthArea_RecordsOfDNAR_RelatedItems();

            personDNARRecordPage
                .WaitForPersonDNARRecordPageToLoad("RECORDS OF DNAR")
                .TapViewOptionsLookupButton();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personDNARRecordPage
                .WaitForPersonDNARRecordPageToLoad("RECORDS OF DNAR")
                .TapOnActiveDNARRecord("Pavel MCNamara",personDNARInactiveRecordId.ToString());

            personDNARActiveNInactiveRecordPage
                .WaitForPersoDNARActiveINactiveRecordPageToLoad()
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateCancelledDecisionFieldText("Yes")
                .ValidateCancelledByFieldText("Mobile Test User 1")
                .ValidateCancelledOnFieldText(CancelledDate);


        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
#endregion