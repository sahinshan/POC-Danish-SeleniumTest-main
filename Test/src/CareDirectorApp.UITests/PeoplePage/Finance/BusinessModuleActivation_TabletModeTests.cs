//using System;
//using NUnit.Framework;
//using CareDirectorApp.TestFramework;
//using CareDirectorApp.TestFramework.PageObjects;
//using Xamarin.UITest.Configuration;
//using System.Collections.Generic;

//namespace CareDirectorApp.UITests.People.Finance
//{
//    /// <summary>
//    /// https://advancedcsg.atlassian.net/browse/CDV6-4877
//    /// 
//    /// https://advancedcsg.atlassian.net/browse/CDV6-4911
//    /// 
//    /// Tests for the activation and deactivation of the finance business module 
//    /// </summary>
//    [TestFixture]
//    [Category("Mobile_TabletMode_Online")]
//    public class FinanceBusinessModuleActivation_TabletModeTests : TestBase
//    {
//        static UIHelper uIHelper;

//        internal Guid CareDirectorApp_ApplicationID { get; set; }
//        internal Guid FinancialAssessment_BusinessModuleID { get; set; }
//        internal Guid ApplicationLinkedBusinessModule { get; set; }

//        internal Guid Mobile_test_user_1_userid { get; set; }
//        internal List<Guid> UserDevices { get; set; }


//        [TestFixtureSetUp]
//        public void ClassInitializationMethod()
//        {
//            if (this.IgnoreTestFixtureSetUp)
//                return;

//            //authenticate a user against the platform services
//            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");

//            //start the APP
//            uIHelper = new UIHelper();
//            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

//            //set Financial Assessment linked business module to inactive
//            CareDirectorApp_ApplicationID = PlatformServicesHelper.application.GetApplicationByDisplayName("CareDirector App")[0];
//            FinancialAssessment_BusinessModuleID = PlatformServicesHelper.businessModule.GetBusinessModuleByName("Financial Assessment")[0];
//            ApplicationLinkedBusinessModule = PlatformServicesHelper.applicationLinkedBusinessModule.GetApplicationLinkedBusinessModule(CareDirectorApp_ApplicationID, FinancialAssessment_BusinessModuleID)[0];
//            PlatformServicesHelper.applicationLinkedBusinessModule.UpdateInactiveField(ApplicationLinkedBusinessModule, true);

//            //set full sync Required to True for the user devices
//            Mobile_test_user_1_userid = PlatformServicesHelper.systemUser.GetSystemUserByUserName("mobile_test_user_1")[0];
//            UserDevices = PlatformServicesHelper.userDevice.GetUserDeviceByUserID(Mobile_test_user_1_userid);
//            PlatformServicesHelper.userDevice.UpdateFullSyncRequired(UserDevices, true);

//            //refresh the cache
//            PlatformServicesHelper.RefreshObjectCache("applicationlinkedbusinessmodule");


//            //set the default URL
//            this.SetDefaultEndpointURL();

//            //Login with test user account
//            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
//            if (changeUserButtonVisible)
//            {
//                //Login with test user account
//                loginPage
//                    .WaitForBasicLoginPageToLoad()
//                    .TapChangeUserButton();

//                warningPopup
//                    .WaitForWarningPopupToLoad()
//                    .TapOnYesButton();

//                loginPage
//                   .WaitForLoginPageToLoad()
//                   .InsertUserName("Mobile_Test_User_1")
//                   .InsertPassword("Passw0rd_!")
//                   .TapLoginButton();

//                //if the offline mode warning is displayed, then close it
//                warningPopup.TapNoButtonIfPopupIsOpen();

//                //wait for the homepage to load
//                homePage
//                    .WaitForHomePageToLoad();
//            }
//            else
//            {
//                //Login with test user account
//                loginPage
//                    .WaitForBasicLoginPageToLoad()
//                    .InsertUserName("Mobile_Test_User_1")
//                    .InsertPassword("Passw0rd_!")
//                    .TapLoginButton();

//                //Set the PIN Code
//                pinPage
//                    .WaitForPinPageToLoad()
//                    .TapButton1()
//                    .TapButton2()
//                    .TapButton3()
//                    .TapButton4()
//                    .TapButtonOK()
//                    .WaitForConfirmationPinPageToLoad()
//                    .TapButton1()
//                    .TapButton2()
//                    .TapButton3()
//                    .TapButton4()
//                    .TapButtonOK();

//                //wait for the homepage to load
//                homePage
//                    .WaitForHomePageToLoad();
//            }
//        }

//        [SetUp]
//        public void TestInitializationMethod()
//        {
//            if (this.IgnoreSetUp)
//                return;

//            //navigate to the settings 
//            mainMenu
//                .NavigateToSettingsPage();

//            //wait for any sync operation to finish
//            settingsPage
//                .WaitForSyncProcessToFinish();

//            //navigate to the people page
//            mainMenu.NavigateToPeoplePage();

//        }

//        #region Person Case Notes page

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        //[Test]
//        //[Property("JiraIssueID", "CDV6-6725")]
//        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-4911 - " +
//        //    "Set the Financial Assessment Business Module to inactive in the ApplicationLinkedBusinessModule table - set the full sync required for the user device - " +
//        //    "Login in the app - navigate to the people page - open a person record - validate that the Finance area is not displayed in the app" +
//        //    "Set the Financial Assessment Business Module to active in the ApplicationLinkedBusinessModule table - set the full sync required for the user device - " +
//        //    "Logout and Login in the app - navigate to the people page - open a person record - Navigate to the person financial details page - Validate that the page is displayed")]
//        //public void FinanceBusinessModuleActivation_TestMethod1()
//        //{
//        //    Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

//        //    // at this point the financial assessment business module should be inactive. Login and validate that the finance are is not visible

//        //    peoplePage
//        //        .WaitForPeoplePageToLoad()
//        //        .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

//        //    personPage
//        //        .WaitForPersonPageToLoad("Maria Tsatsouline")
//        //        .TapRelatedItemsButton()
//        //        .ValidateFinanceAreaNotVisible_RelatedItems();

//        //    //activate the Financial Assessment linked business module
//        //    PlatformServicesHelper.applicationLinkedBusinessModule.UpdateInactiveField(ApplicationLinkedBusinessModule, false);

//        //    //set full sync Required to True for the user devices
//        //    PlatformServicesHelper.userDevice.UpdateFullSyncRequired(UserDevices, true);

//        //    //refresh the cache
//        //    PlatformServicesHelper.RefreshObjectCache("applicationlinkedbusinessmodule");


//        //    //logout and login again
//        //    mainMenu
//        //        .Logout();

//        //    //login again with the same user
//        //    loginPage
//        //        .WaitForBasicLoginPageToLoad()
//        //        .InsertPassword("Passw0rd_!")
//        //        .TapLoginButton();

//        //    //wait for the homepage to load
//        //    homePage
//        //        .WaitForHomePageToLoad();

//        //    //navigate to the settings 
//        //    mainMenu
//        //        .NavigateToSettingsPage();

//        //    //wait for any sync operation to finish
//        //    settingsPage
//        //        .WaitForSyncProcessToFinish();


//        //    // at this point the financial assessment business module should be visible
//        //    mainMenu
//        //        .NavigateToPeoplePage();

//        //    peoplePage
//        //        .WaitForPeoplePageToLoad()
//        //        .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

//        //    personPage
//        //        .WaitForPersonPageToLoad("Maria Tsatsouline")
//        //        .TapRelatedItemsButton()
//        //        .TapFinanceArea_RelatedItems()
//        //        .TapFinanceDetailsIcon_RelatedItems();

//        //    personFinancialDetailsPage
//        //        .WaitForPersonFinancialDetailsPageToLoad();

//        //}


//        #endregion

//        //[Description("Method will return the name of all tests and the Description of each one")]
//        //[Test]
//        //public void GetTestNames()
//        //{
//        //    this.GetAllTestNamesAndDescriptions();
//        //}
//    }
//}
