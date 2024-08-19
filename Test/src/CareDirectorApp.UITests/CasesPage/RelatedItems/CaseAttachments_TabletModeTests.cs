using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Case Attachments validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class CaseAttachments_TabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Attachment (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>case</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Attachment (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


        #endregion

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

            //if the cases case attachment injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases case attachment review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToCasesPage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region Case Attachments page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6523")]
        [Description("UI Test for Case Attachments - 0001 - " +
            "Navigate to the Case Attachments area (case do not contains case attachment records) - Validate the page content")]
        public void CaseAttachments_TestMethod01()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6524")]
        [Description("UI Test for Case Attachments - 0002 - " +
            "Navigate to the Case Attachments area (case contains case attachment records) - Validate the page content")]
        public void CaseAttachments_TestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseAttachmentID = new Guid("d43b00d8-99b0-ea11-a2cd-005056926fe4");

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .ValidateTitleCellText("Attachment 1", caseAttachmentID.ToString())
                .ValidateDateCellText("01/06/2020 07:20", caseAttachmentID.ToString())
                .ValidateDocumentTypeCellText("Bladder Scan", caseAttachmentID.ToString())
                .ValidateDocumentSubTypeCellText("Bladder Scan Results", caseAttachmentID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseAttachmentID.ToString())
                .ValidateCreatedOnCellText("17/06/2020 13:55", caseAttachmentID.ToString())
                .ValidateModifiedByCellText("CW Forms Test User 1", caseAttachmentID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6525")]
        [Description("UI Test for Case Attachments - 0003 - " +
            "Navigate to the Case Attachments area - Open a cases case attachment record - Validate that the case attachment record page is displayed")]
        public void CaseAttachments_TestMethod03()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteTitle = "Attachment 1";

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord(CaseNoteTitle);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6526")]
        [Description("UI Test for Case Attachments - 0004 - " +
            "Navigate to the Case Attachments area - Open a cases case attachment record - Validate that the case attachment record page field titles are displayed")]
        public void CaseAttachments_TestMethod04()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteTitle = "Attachment 1";

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord(CaseNoteTitle);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .ValidateTitleFieldTitleVisible(true)
                .ValidateDocumentTypeFieldTitleVisible(true)
                .ValidateDocumentSubTypeFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFileFieldTitleVisible(true)

                .ValidateTakePictureButtonVisible(true)
                .ValidateUploadFileButtonVisible(true)
                .ValidateDownloadFileButtonVisible(true)
                .ValidateDeleteAttachmentFileButtonVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6527")]
        [Description("UI Test for Case Attachments - 0005 - " +
            "Navigate to the Case Attachments area - Open a cases case attachment record - Validate that the case attachment record page fields are correctly displayed")]
        public void CaseAttachments_TestMethod05()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteTitle = "Attachment 1";

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord(CaseNoteTitle);

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .ValidateTitleFieldText(CaseNoteTitle)
                .ValidateDocumentTypeFieldText("Bladder Scan")
                .ValidateDocumentSubTypeFieldText("Bladder Scan Results")
                .ValidateDateFieldText("01/06/2020", "07:20")
                .ValidateResponsibleTeamReadonlyFieldText("Mobile Team 1")
                .ValidateAttachmentFileName("Capture.PNG");
        }



        #endregion

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6528")]
        [Description("UI Test for Case Attachments - 0007 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void CaseAttachments_TestMethod07()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .ValidateTitleFieldTitleVisible(true)
                .ValidateDocumentTypeFieldTitleVisible(true)
                .ValidateDocumentSubTypeFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFileFieldTitleVisible(true)
                .ValidateTakePictureButtonVisible(true)
                .ValidateUploadFileButtonVisible(true)
                .ValidateDownloadFileButtonVisible(false)
                .ValidateDeleteAttachmentFileButtonVisible(false);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6529")]
        [Description("UI Test for Case Attachments - 0008 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Validate that the new record page is displayed but the delete button is not displayed")]
        public void CaseAttachments_TestMethod08()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6530")]
        [Description("UI Test for Case Attachments - 0009 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void CaseAttachments_TestMethod09()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("CareDirector").TapSearchButtonQuery().TapOnRecord("Name: CareDirector QA");

            caseAttachmentRecordPage
               .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
               .TapOnSaveButton()
               .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1");


            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);

            Assert.AreEqual(1, caseAttachments.Count);

            var fields = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentByID(caseAttachments[0], "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "OwnerId", "Title", "Inactive", "Date", "DocumentTypeId", "DocumentSubTypeId", "FileId", "CaseId", "PersonId");

            var datefield = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentByID(caseAttachments[0], "Date");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["Date"]);
            string Date = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid careDirectorQA = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA team
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(careDirectorQA, (Guid)fields["ownerid"]);
            Assert.AreEqual("Attachment 1", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(date.ToString("dd'/'MM'/'yyyy HH:mm"), Date);
            Assert.AreEqual(documentType, (Guid)fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, (Guid)fields["documentsubtypeid"]);
            Assert.AreEqual(false, (bool)fields.ContainsKey("fileid"));
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);


        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6531")]
        [Description("UI Test for Case Attachments - 0010 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void CaseAttachments_TestMethod10()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("CareDirector").TapSearchButtonQuery().TapOnRecord("Name: CareDirector QA");

            caseAttachmentRecordPage
               .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
               .TapOnSaveAndCloseButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad();


            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);

            Assert.AreEqual(1, caseAttachments.Count);

            var fields = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentByID(caseAttachments[0], "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "OwnerId", "Title", "Inactive", "Date", "DocumentTypeId", "DocumentSubTypeId", "FileId", "CaseId", "PersonId");

            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid careDirectorQA = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA team
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(careDirectorQA, (Guid)fields["ownerid"]);
            Assert.AreEqual("Attachment 1", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(date, ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, (Guid)fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, (Guid)fields["documentsubtypeid"]);
            Assert.AreEqual(false, (bool)fields.ContainsKey("fileid"));
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6532")]
        [Description("UI Test for Case Attachments - 0011 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - " +
            "Re-Open the record - Validate that all fields are correctly set after saving the record")]
        public void CaseAttachments_TestMethod11()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("CareDirector").TapSearchButtonQuery().TapOnRecord("Name: CareDirector QA");

            caseAttachmentRecordPage
               .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
               .TapOnSaveAndCloseButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad();


            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(1, caseAttachments.Count);

            caseAttachmentsPage
                .TapOnRecord("Attachment 1");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .ValidateTitleFieldText("Attachment 1")
                .ValidateDocumentTypeFieldText("Bladder Scan")
                .ValidateDocumentSubTypeFieldText("Bladder Scan Results")
                .ValidateDateFieldText("20/05/2020", "09:00")
                .ValidateResponsibleTeamReadonlyFieldText("CareDirector QA");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6533")]
        [Description("UI Test for Case Attachments - 0012 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data in all fields - Leave the responsible team with the default value - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void CaseAttachments_TestMethod12()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton()
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1");


            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);

            Assert.AreEqual(1, caseAttachments.Count);

            var fields = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentByID(caseAttachments[0], "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "OwnerId", "Title", "Inactive", "Date", "DocumentTypeId", "DocumentSubTypeId", "FileId", "CaseId", "PersonId");

            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual("Attachment 1", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(date, ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(documentType, (Guid)fields["documenttypeid"]);
            Assert.AreEqual(documentSubType, (Guid)fields["documentsubtypeid"]);
            Assert.AreEqual(false, (bool)fields.ContainsKey("fileid"));
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6534")]
        [Description("UI Test for Case Attachments - 0013 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data only in mandatory fields except for Title- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void CaseAttachments_TestMethod13()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Title' is required")
                .TapOnOKButton();

            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(0, caseAttachments.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6535")]
        [Description("UI Test for Case Attachments - 0014 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data only in mandatory fields except for Document Type- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void CaseAttachments_TestMethod14()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Type' is required")
                .TapOnOKButton();

            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(0, caseAttachments.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6536")]
        [Description("UI Test for Case Attachments - 0015 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data only in mandatory fields except for Document Sub Type- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void CaseAttachments_TestMethod15()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Document Sub Type' is required")
                .TapOnOKButton();

            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(0, caseAttachments.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6537")]
        [Description("UI Test for Case Attachments - 0016 - " +
            "Navigate to the Case Attachments area - Tap on the add button - Set data only in mandatory fields except for Date - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void CaseAttachments_TestMethod16()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnAddNewRecordButton();

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .InsertTitle("Attachment 1")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Description: Bladder Scan Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENTS (FOR CASE)")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Date' is required")
                .TapOnOKButton();

            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(0, caseAttachments.Count);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6538")]
        [Description("UI Test for Case Attachments - 0017 - Create a new cases case attachment using the main APP web services" +
            "Navigate to the Case Attachments area - open the case attachment record - validate that all fields are correctly synced")]
        public void CaseAttachments_TestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results
            DateTime attachmentDate = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            Guid caseAttachmentID = this.PlatformServicesHelper.caseAttachment.CreateCaseAttachment("Attachment 1", mobileTeam1, attachmentDate, documentType, documentSubType, caseID, personID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord("Attachment 1");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .ValidateTitleFieldText("Attachment 1")
                .ValidateDocumentTypeFieldText("Bladder Scan")
                .ValidateDocumentSubTypeFieldText("Bladder Scan Results")
                .ValidateDateFieldText("20/05/2020", "09:00")
                .ValidateResponsibleTeamReadonlyFieldText("Mobile Team 1");

        }



        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6539")]
        [Description("UI Test for Case Attachments - 0019 - Create a new cases case attachment using the main APP web services" +
            "Navigate to the Case Attachments area - open the case attachment record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void CaseAttachments_TestMethod19()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results
            DateTime attachmentDate = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            Guid caseAttachmentID = this.PlatformServicesHelper.caseAttachment.CreateCaseAttachment("Attachment 1", mobileTeam1, attachmentDate, documentType, documentSubType, caseID, personID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord("Attachment 1");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .InsertTitle("Attachment 1 updated")
                .TapDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Clinical Chemistry Results");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .TapDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Confirmed");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .InserDate("01/06/2020", "09:30")
                .ValidateResponsibleTeamLookupButtonVisible(false)
                .ValidateResponsibleTeamLRemoveButtonVisible(false)
                .TapOnSaveAndCloseButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad();


            var caseAttachments = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(1, caseAttachments.Count);


            Guid updated_documentType = new Guid("F351B824-70F6-E911-A2C7-005056926FE4"); //Clinical Chemistry Results
            Guid updated_documentSubType = new Guid("A1451CA0-70F6-E911-A2C7-005056926FE4"); //Confirmed
            DateTime updated_attachmentDate = new DateTime(2020, 6, 1, 9, 30, 0);

            var fields = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentByID(caseAttachments[0], "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "OwnerId", "Title", "Inactive", "Date", "DocumentTypeId", "DocumentSubTypeId", "FileId", "CaseId", "PersonId");

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual("Attachment 1 updated", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(updated_attachmentDate, ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(updated_documentType, (Guid)fields["documenttypeid"]);
            Assert.AreEqual(updated_documentSubType, (Guid)fields["documentsubtypeid"]);
            Assert.AreEqual(false, (bool)fields.ContainsKey("fileid"));
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
        }



        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6540")]
        [Description("UI Test for Case Attachments - 0020 - Create a new cases case attachment using the main APP web services" +
            "Navigate to the Case Attachments area - open the case attachment record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void CaseAttachments_TestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid documentType = new Guid("6A3EE339-6FF6-E911-A2C7-005056926FE4"); //Bladder Scan
            Guid documentSubType = new Guid("0309C96F-71F6-E911-A2C7-005056926FE4"); //Bladder Scan Results
            DateTime attachmentDate = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any cases case attachment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID))
                this.PlatformServicesHelper.caseAttachment.DeleteCaseAttachment(recordID);

            Guid caseAttachmentID = this.PlatformServicesHelper.caseAttachment.CreateCaseAttachment("Attachment 1", mobileTeam1, attachmentDate, documentType, documentSubType, caseID, personID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAttachmentsIcon_RelatedItems();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad()
                .TapOnRecord("Attachment 1");

            caseAttachmentRecordPage
                .WaitForCaseAttachmentRecordPageToLoad("ATTACHMENT (FOR CASE): Attachment 1")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            caseAttachmentsPage
                .WaitForCaseAttachmentsPageToLoad();

            var records = this.PlatformServicesHelper.caseAttachment.GetCaseAttachmentsByCaseID(caseID);
            Assert.AreEqual(0, records.Count);
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
