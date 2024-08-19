using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Collections.Generic;

namespace CareDirectorApp.UITests.People.Finance
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-5131
    /// 
    /// Tests for the activation and deactivation of the finance business module 
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonFinancialDetailAttachments_OfflineTabletModeTests : TestBase
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

            //this._app.Repl();

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

            ///close the lookup popup if it is open
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6761")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Open a person financial detail record - navigate to the Attachments page - Validate that the page is displayed")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("df30bc95-c804-eb11-a2cd-005056926fe4"); //AE (Multiple Rates)

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad();


        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6762")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Open a person financial detail record (person financial detail with no Attachment records) - navigate to the Attachments page - " +
            "Validate that the no records message is present")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("4c2bb7b8-c804-eb11-a2cd-005056926fe4"); //Additional Personal Allowance - Variable

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Additional Personal Allowance - Variable, 01/10/2020,")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6763")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Open a person financial detail record (PFD has an Attachment record record) - navigate to the Attachments page - " +
            "Validate that the record is displayed")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("df30bc95-c804-eb11-a2cd-005056926fe4"); //AE (Multiple Rates)
            Guid personFinancialDetailAttachmentID = new Guid("172c5706-be12-eb11-a2cd-005056926fe4"); //Attachment 1

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .ValidateTitleCellText("Attachment 1", personFinancialDetailAttachmentID.ToString())
                .ValidateDateCellText("01/10/2020 08:00", personFinancialDetailAttachmentID.ToString())
                .ValidateDocumentTypeCellText("Bladder Scan", personFinancialDetailAttachmentID.ToString())
                .ValidateDocumentSubTypeCellText("Bladder Scan Results", personFinancialDetailAttachmentID.ToString())
                .ValidateCreatedByCellText("José Brazeta", personFinancialDetailAttachmentID.ToString())
                .ValidateCreatedOnCellText("20/10/2020 11:21", personFinancialDetailAttachmentID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", personFinancialDetailAttachmentID.ToString())
                .ValidateModifiedOnCellText("20/10/2020 11:23", personFinancialDetailAttachmentID.ToString());
        }


        #region Open Existing Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6764")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person Financial Detail has Attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the Attachment record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("df30bc95-c804-eb11-a2cd-005056926fe4"); //AE (Multiple Rates)
            Guid personFinancialDetailAttachmentID = new Guid("172c5706-be12-eb11-a2cd-005056926fe4"); //Attachment 1

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(personFinancialDetailAttachmentID.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .ValidateTitleFieldTitleVisible(true)
                .ValidateDocumentTypeFieldTitleVisible(true)
                .ValidateDocumentSubTypeFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFileFieldTitleVisible(true)

                .ValidateTitleFieldText("Attachment 1")
                .ValidateDocumentTypeLookupEntryFieldText("Bladder Scan")
                .ValidateDocumentSybTypeLookupEntryFieldText("Bladder Scan Results")
                .ValidateDateFieldText("01/10/2020", "08:00")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateAttachmentNameFieldText("Document.docx")

                .ValidateTakePictureFileButtonVisible(true) //if in the business object the ValidFileExtensions field has a value, then the button is not displayed
                .ValidateGalleryFileButtonVisible(true)
                .ValidateDownloadFileButtonVisible(true)
                .ValidateDeleteFileButtonVisible(true);
        }


        #endregion

        #region Update Records

        [Test]
        [Property("JiraIssueID", "CDV6-6765")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Change the Title, Document Type, Document Sub Type, Date fields - Tap on the Save button - " +
            "Validate that the record is correctly updated")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod05()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .InsertTitle("Attachment 1 Updated")
                .InsertDate("20/10/2020", "13:30")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Doc Type").TapSearchButtonQuery().TapOnRecord("Doc Type 1");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Doc Sub Type").TapSearchButtonQuery().TapOnRecord("Doc Sub Type 1");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .TapOnSaveAndCloseButton();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var newdocumentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Doc Type 1")[0];
            var newdocumentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Doc Sub Type 1")[0];
            var fields = PlatformServicesHelper.personFinancialDetailAttachment.GetByID(pfdAttachmentid, "ownerid", "title", "date", "documenttypeid", "documentsubtypeid", "personfinancialdetailid", "personid");

            var datefield = this.PlatformServicesHelper.personFinancialDetailAttachment.GetByID(pfdAttachmentid, "date");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["date"]);
            string date = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");



            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual("Attachment 1 Updated", fields["title"]);
            Assert.AreEqual(new DateTime(2020, 10, 20, 13, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), date);
            Assert.AreEqual(newdocumentType, fields["documenttypeid"]);
            Assert.AreEqual(newdocumentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(personFinancialDetailID, fields["personfinancialdetailid"]);
            Assert.AreEqual(personID, fields["personid"]);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6766")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Remove the value from the Title field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod06()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .InsertTitle("")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Title' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6767")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Remove the value from the Document Type field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod07()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .TapDocumentTypeRemoveButton()
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Type' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6768")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Remove the value from the Document Sub Type field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod08()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .TapDocumentSubTypeRemoveButton()
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Sub Type' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6769")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Remove the value from the Date field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod09()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .InsertDate("", "")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Date' is required").TapOnOKButton();

        }

        #endregion

        #region Creating records

        [Test]
        [Property("JiraIssueID", "CDV6-6770")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with no attachment records - Open the person financial detail record - navigate to the Attachments page - " +
            "Tap on the Add new record button - Set the Title, Document Type, Document Sub Type, Date fields - Tap on the Save button - " +
            "Validate that the record is correctly saved")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod14()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile team 1")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .InsertTitle("Attachment 1")
                .InsertDate("20/10/2020", "13:30")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan Results");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1");

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();



            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];

            var attachments = PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(personFinancialDetailID);
            Assert.AreEqual(1, attachments.Count);

            var fields = PlatformServicesHelper.personFinancialDetailAttachment.GetByID(attachments[0], "ownerid", "title", "date", "documenttypeid", "documentsubtypeid", "personfinancialdetailid", "personid");

            var datefield = this.PlatformServicesHelper.personFinancialDetailAttachment.GetByID(attachments[0], "date");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["date"]);
            string Date = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual("Attachment 1", fields["title"]);
            Assert.AreEqual(new DateTime(2020, 10, 20, 13, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), Date);
            Assert.AreEqual(documentType, fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, fields["documentsubtypeid"]);
            Assert.AreEqual(personFinancialDetailID, fields["personfinancialdetailid"]);
            Assert.AreEqual(personID, fields["personid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6771")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with no attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Tap on the Add new record button - Set data in all fields except for the Title field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod15()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                //.InsertTitle("Attachment 1")
                .InsertDate("20/10/2020", "13:30")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan Results");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Title' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6772")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with no attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Tap on the Add new record button - Set data in all fields except for the Document Type field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod16()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .InsertTitle("Attachment 1")
                .InsertDate("20/10/2020", "13:30")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan Results");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Type' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6773")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with no attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Tap on the Add new record button - Set data in all fields except for the Document Sub Type field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod17()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .InsertTitle("Attachment 1")
                .InsertDate("20/10/2020", "13:30")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Sub Type' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6774")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with no attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Tap on the Add new record button - Set data in all fields except for the Date field - Tap on the Save button - " +
            "Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod18()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .InsertTitle("Attachment 1")
                //.InsertDate("20/10/2020", "13:30")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bladder").TapSearchButtonQuery().TapOnRecord("Bladder Scan Results");

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Date' is required").TapOnOKButton();

        }

        #endregion

        #region Delete

        [Test]
        [Property("JiraIssueID", "CDV6-6775")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5131" +
            "Person has a person financial detail with an attachment record - Open the person financial detail record - navigate to the Attachments page - " +
            "Open the attachment record - Tap on the delete button - Confirm the delete operation - " +
            "Validate that the record is deleted")]
        public void PersonFinancialDetailAttachments_OfflineTestMethod19()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
            {
                foreach (var attachmentid in PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(pfdID))
                    PlatformServicesHelper.personFinancialDetailAttachment.DeletePersonFinancialDetailAttachment(attachmentid);

                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);
            }

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            var documentType = PlatformServicesHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Bladder Scan")[0];
            var documentSubType = PlatformServicesHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Bladder Scan Results")[0];
            var pfdAttachmentid = PlatformServicesHelper.personFinancialDetailAttachment.CreatePersonFinancialDetailAttachment("Attachment 1", ownerid, startdate, documentType, documentSubType, personFinancialDetailID, personID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .NavigateToPersonFinancialDetailsAttachmentsPage();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad()
                .TapOnRecord(pfdAttachmentid.ToString());

            personFinancialDetailAttachmentRecordPage
                .WaitForPersonFinancialDetailAttachmentRecordPageToLoad("ATTACHMENT (FOR PERSON FINANCIAL DETAIL): Attachment 1")
                .TapOnDeleteButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?").TapOnYesButton();

            personFinancialDetailAttachmentsPage
                .WaitForPersonFinancialDetailAttachmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var attachments = PlatformServicesHelper.personFinancialDetailAttachment.GetByPersonFinancialDetailId(personFinancialDetailID);
            Assert.AreEqual(0, attachments.Count);
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
