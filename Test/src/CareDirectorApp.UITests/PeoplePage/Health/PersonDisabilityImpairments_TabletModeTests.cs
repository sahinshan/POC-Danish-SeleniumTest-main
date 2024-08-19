using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Health
{
    /// <summary>
    /// This class contains all test methods for Person Allergy validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonDisabilityImpairments_TabletModeTests : TestBase
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

            //if the cases of a injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases of a review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToPeoplePage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region Person Disability/Impairments page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6923")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the Person Disability/Impairments area (do not contains Disability/Impairments records) - Validate the page content")]
        public void PersonDisabilityImpairments_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any PersonAllergy for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(personID))
                this.PlatformServicesHelper.personDisabilityImpairments.DeletePersonDisabilityImpairments(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6924")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person AlleDisability/Impairments area (person contains Disability/Impairments records) - Validate the page content")]
        public void PersonDisabilityImpairments_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid disabilityID = new Guid("ef86d8e5-09a4-ea11-a2cd-005056926fe4");
            Guid imparimentID = new Guid("fee5ac39-0aa4-ea11-a2cd-005056926fe4");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad();

            personDisabilityImpairmentsPage

                .ValidateDisabilityCellText("Deaf", disabilityID.ToString())
                .ValidateImpairmentCellText("", disabilityID.ToString())
                .ValidateSeverityCellText("Moderate", disabilityID.ToString())
                .ValidateStartDateCellText("01/05/2020", disabilityID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", disabilityID.ToString())
                .ValidateCreatedOnCellText("01/06/2020 14:15", disabilityID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", disabilityID.ToString())
                .ValidateModifiedOnCellText("01/06/2020 14:24", disabilityID.ToString());

                personDisabilityImpairmentsPage
                .ValidateDisabilityCellText("", imparimentID.ToString())
                .ValidateImpairmentCellText("Dementia", imparimentID.ToString())
                .ValidateSeverityCellText("Mild", imparimentID.ToString())
                .ValidateStartDateCellText("02/05/2020", imparimentID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", imparimentID.ToString())
                .ValidateCreatedOnCellText("01/06/2020 14:17", imparimentID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", imparimentID.ToString())
                .ValidateModifiedOnCellText("01/06/2020 14:17", imparimentID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6925")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Disability record - Validate that the Person Disability record page is displayed")]
        public void PersonDisabilityImpairments_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("01/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:15:08");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6926")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Disability record - Validate that the Person Disability record page field titles are displayed")]
        public void PersonDisabilityImpairments_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("01/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:15:08")
                .ValidateDisabilityFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateSeverityFieldTitleVisible(true)
                .ValidateCVIRecievedDateFieldTitleVisible(true)
                .ValidateOnsetDateFieldTitleVisible(true)

                .ValidateImpairmentFieldTitleVisible(true)
                .ValidateDiagnosisDateFieldTitleVisible(true)
                .ValidateNotifiedDateFieldTitleVisible(true)
                .ValidateRegisteredDisabilityNoFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6927")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Disability record - Validate that the Person Disability record page fields are correctly displayed")]
        public void PersonDisabilityImpairments_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("01/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:15:08")
                .ValidateDisabilityFieldText("Deaf")
                .ValidateStartDateFieldText("01/05/2020")
                .ValidateSeverityFieldText("Moderate")
                .ValidateCVIRecievedDateFieldText("02/05/2020")
                .ValidateOnsetDateFieldText("05/05/2020")

                .ValidateImpairmentFieldText("")
                .ValidateDiagnosisDateFieldText("01/09/2019")
                .ValidateNotifiedDateFieldText("04/05/2020")
                .ValidateRegisteredDisabilityNoFieldText("9876");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6928")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Disability record (with only the mandatory information set) - Validate that the Person Disability record page fields are correctly displayed")]
        public void PersonDisabilityImpairments_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("03/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:18:32")
                .ValidateDisabilityFieldText("Mental health needs: Other")
                .ValidateStartDateFieldText("03/05/2020")
                .ValidateSeverityFieldText("")
                .ValidateCVIRecievedDateFieldText("")
                .ValidateOnsetDateFieldText("")

                .ValidateImpairmentFieldText("")
                .ValidateDiagnosisDateFieldText("")
                .ValidateNotifiedDateFieldText("")
                .ValidateRegisteredDisabilityNoFieldText("");
        }





        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6929")]
        [Description("UI Test for Dashboards - 0007 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Impairment record - Validate that the Person Impairment record page is displayed")]
        public void PersonDisabilityImpairments_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("02/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:17:22");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6930")]
        [Description("UI Test for Dashboards - 0008 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Impairment record - Validate that the Person Impairment record page field titles are displayed")]
        public void PersonDisabilityImpairments_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("02/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:17:22")
                .ValidateDisabilityFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateSeverityFieldTitleVisible(true)
                .ValidateCVIRecievedDateFieldTitleVisible(true)
                .ValidateOnsetDateFieldTitleVisible(true)

                .ValidateImpairmentFieldTitleVisible(true)
                .ValidateDiagnosisDateFieldTitleVisible(true)
                .ValidateNotifiedDateFieldTitleVisible(true)
                .ValidateRegisteredDisabilityNoFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6931")]
        [Description("UI Test for Dashboards - 0009 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Impairment record - Validate that the Person Impairment record page fields are correctly displayed")]
        public void PersonDisabilityImpairments_TestMethod09()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("02/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:17:22")
                .ValidateDisabilityFieldText("")
                .ValidateStartDateFieldText("02/05/2020")
                .ValidateSeverityFieldText("Mild")
                .ValidateCVIRecievedDateFieldText("03/05/2020")
                .ValidateOnsetDateFieldText("06/05/2020")

                .ValidateImpairmentFieldText("Dementia")
                .ValidateDiagnosisDateFieldText("01/05/2020")
                .ValidateNotifiedDateFieldText("05/05/2020")
                .ValidateRegisteredDisabilityNoFieldText("567");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6932")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the Person Disability/Impairments area - Open a person Impairment record (with only the mandatory information set) - Validate that the Person Impairment record page fields are correctly displayed")]
        public void PersonDisabilityImpairments_TestMethod10()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("04/05/2020");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad("DISABILITY/IMPAIRMENT: Disability/Impairment Type for Mr Pavel MCNamara created by Mobile Test User 1 on 01/06/2020 14:18:43")
                .ValidateDisabilityFieldText("")
                .ValidateStartDateFieldText("04/05/2020")
                .ValidateSeverityFieldText("")
                .ValidateCVIRecievedDateFieldText("")
                .ValidateOnsetDateFieldText("")

                .ValidateImpairmentFieldText("VISION")
                .ValidateDiagnosisDateFieldText("")
                .ValidateNotifiedDateFieldText("")
                .ValidateRegisteredDisabilityNoFieldText("");
        }



        #endregion

        #region Sync Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6933")]
        [Description("UI Test for Dashboards - 0015 - Create a new person Disability using the main APP web services" +
            "Navigate to the Person Disability/Impairments area - open the Person Disability/Impairment record - validate that all fields are correctly synced")]
        public void PersonDisabilityImpairments_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid disabilityType = new Guid("63df1209-df39-e911-a2c5-005056926fe4"); //Deaf
            int disabilityimpairmenttypeid = 1; //Disability
            int disabilityseverityid = 1; //Mild
            DateTime diagnosisdate = new DateTime(2020, 5, 1);
            DateTime notifieddate = new DateTime(2020, 5, 2);
            DateTime startdate = new DateTime(2020, 5, 3); 
            DateTime? enddate = null;
            DateTime cvireceiveddate = new DateTime(2020, 5, 4);
            DateTime onsetdate = new DateTime(2020, 5, 5);
            DateTime reviewdate = new DateTime(2020, 5, 6);


            //remove any PersonAllergy for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personDisabilityImpairments.GetPersonDisabilityImpairmentsByPersonID(personID))
                this.PlatformServicesHelper.personDisabilityImpairments.DeletePersonDisabilityImpairments(recordID);

            this.PlatformServicesHelper.personDisabilityImpairments.CreatePersonDisabilityImpairments(mobileTeam1, personID, disabilityType, null, "123", diagnosisdate, notifieddate, startdate, enddate, cvireceiveddate, onsetdate, reviewdate, disabilityimpairmenttypeid, disabilityseverityid);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonDisabilitiesIcon_RelatedItems();

            personDisabilityImpairmentsPage
                .WaitForPersonDisabilityImpairmentsPageToLoad()
                .TapOnRecord("Deaf");

            personDisabilityImpairmentRecordPage
                .WaitForPersonDisabilityImpairmentRecordPageToLoad()
                .ValidateDisabilityFieldText("Deaf")
                .ValidateStartDateFieldText("03/05/2020")
                .ValidateSeverityFieldText("Mild")
                .ValidateCVIRecievedDateFieldText("04/05/2020")
                .ValidateOnsetDateFieldText("05/05/2020")

                .ValidateImpairmentFieldText("")
                .ValidateDiagnosisDateFieldText("01/05/2020")
                .ValidateNotifiedDateFieldText("02/05/2020")
                .ValidateRegisteredDisabilityNoFieldText("123");
        }


        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
