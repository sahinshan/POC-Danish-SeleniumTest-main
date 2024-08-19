using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;


namespace Phoenix.UITests.QualityAndCompliance
{
    [TestClass]
    public class ReportableEvents_UITestCase : FunctionalTest
    {
        #region Properties

        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _careProviders_EndDate;
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _reportableEventTypeId;
        private Guid _reportableEventTypeSeverityId;
        private Guid _reportableEventTypeRoleId;
        private Guid _reportableEventStatusId;
        private Guid _attachmentId;
        private Guid _personId;
        private string EnvironmentName;
        private string _systemUserName;
        private string _systemUserFirstName;
        private string _systemUserLastName;
        private Guid _systemUserID;
        private Guid reportableeventIdInactive;
        DateTime EndDate = DateTime.Now.Date;
        private Guid _reportableEventImpactId;

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Connection to database

            string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new DBHelper.DatabaseHelper(tenantName);

            #endregion

            #region Environment Name
            EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit
            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];


            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careProviders_BusinessUnitId, "90400", "CareDirectorQA@careworkstempmail.com", "CareProviders", "020 123456");
            _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion



            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("English").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careProviders_TeamId, "English", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personId = dbHelper.person.CreatePersonRecord("", "Jhon", "CDV6-16205_" + currentDate, "Smith", "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId, 7, 2);

            #endregion

            #region Create default system user

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any();
            if (!defaultLoginUserExists)
                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

            if (Guid.Empty == _defaultLoginUserID)
                _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

            #endregion  Create default system user

            #region Team Manager

            dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

            #endregion

            #region Create SystemUser Record

            _systemUserFirstName = "Test_User_CDV6_17292_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_17292_" + _systemUserLastName;
            _systemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            #endregion

            #region Create Reportable Event Type

            var reportableEventTpye = dbHelper.careproviderReportableEventType.GetByName("Accident").Any();
            if (!reportableEventTpye)
            {
                _reportableEventTypeId = dbHelper.careproviderReportableEventType.CreateCareproviderReportableEventTypeRecord("Accident", DateTime.Now, _careProviders_TeamId);
            }
            if (_reportableEventTypeId == Guid.Empty)
            {
                _reportableEventTypeId = dbHelper.careproviderReportableEventType.GetByName("Accident").FirstOrDefault();
            }
            #endregion Create Reportable Event Type

            #region Create Reportable Event Severity

            var reportableEventSeverity = dbHelper.careproviderReportableEventSeverity.GetByName("Major").Any();
            if (!reportableEventSeverity)
            {
                _reportableEventTypeSeverityId = dbHelper.careproviderReportableEventSeverity.CreateCareproviderReportableEventSeverityRecord("Major", DateTime.Now, _careProviders_TeamId);
            }
            if (_reportableEventTypeSeverityId == Guid.Empty)
            {
                _reportableEventTypeSeverityId = dbHelper.careproviderReportableEventSeverity.GetByName("Major").FirstOrDefault();
            }
            #endregion

            #region Create Reportable Event Role

            var reportableEventRole = dbHelper.careproviderReportableEventRole.GetByName("Victim").Any();
            if (!reportableEventRole)
            {
                _reportableEventTypeRoleId = dbHelper.careproviderReportableEventRole.CreateCareProviderReportableEventRoleRecord("Victim", DateTime.Now, _careProviders_TeamId);
            }
            if (_reportableEventTypeRoleId == Guid.Empty)
            {
                _reportableEventTypeRoleId = dbHelper.careproviderReportableEventRole.GetByName("Victim").FirstOrDefault();
            }
            #endregion

            #region Create Reportable Event Status

            var reportableEventStatus = dbHelper.careproviderReportableEventStatus.GetByName("Stage 1").Any();
            if (!reportableEventStatus)
            {
                _reportableEventStatusId = dbHelper.careproviderReportableEventStatus.CreateCareproviderReportableEventStatusRecord("Stage 1", DateTime.Now, _careProviders_TeamId, _reportableEventTypeId);
            }
            if (_reportableEventStatusId == Guid.Empty)
            {
                _reportableEventStatusId = dbHelper.careproviderReportableEventStatus.GetByName("Stage 1").FirstOrDefault();
            }

            #endregion




        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-15868

        [TestProperty("JiraIssueID", "ACC-3175")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            " Click + icon to add new record -> Should take user to fill the details related to attachment and to upload the file -> Fill all mandatory and non mandatory fields and upload a valid file -> Click Save" +
            "File should be uploaded successfully and new attachment record should get displayed under the Attachment Section with correct values")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod01()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            var startDate = DateTime.Now.AddDays(-3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            reportableEventAttachmentsPage
                .WaitForReportableEventAttachmentsPageToLoad()
                .ClickCreateNewRecord();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderreportableeventattachment")
                .ClickOnExpandIcon();

            reportableEventAttchmentsRecordPage
                .WaitForReportableEventAttchmentsRecordPageToLoad()
                .InsertTitle("test")
                .InsertDate(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
                .ClickDocumentType_LookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Discharge Summary").TapSearchButton().SelectResultElement(_documenttypeid.ToString());

            reportableEventAttchmentsRecordPage
                .WaitForReportableEventAttchmentsRecordPageToLoad()
                .ClickSubDocumentType_LookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Confirmed Result").TapSearchButton().SelectResultElement(_documentsubtypeid.ToString());

            reportableEventAttchmentsRecordPage
                .WaitForReportableEventAttchmentsRecordPageToLoad()
                .FileUpload(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad();

            System.Threading.Thread.Sleep(2000);

            var reportableEventRecords = dbHelper.careProviderReportableEventAttachment.GetByReportableEvent(_reportableEventId);
            Assert.AreEqual(1, reportableEventRecords.Count);

            var reportableEventFields = dbHelper.careProviderReportableEventAttachment.GetByTitle(reportableEventRecords[0], "title", "date", "documenttypeid", "documentsubtypeid");
            Assert.AreEqual("test", reportableEventFields["title"]);
            //  Assert.AreEqual(startDate.AddDays(-3), reportableEventFields["date"]);
            Assert.AreEqual(_documenttypeid.ToString(), reportableEventFields["documenttypeid"].ToString());
            Assert.AreEqual(_documentsubtypeid.ToString(), reportableEventFields["documentsubtypeid"].ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3176")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Click + icon to add new record ->Should take user to fill the details related to attachment and to upload the file -> Leave all mandatory fields as blank -> Click Save" +
            "Error message should be displayed against each mandatory field and record should not get created")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod02()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
                .ClickAttachmentLink();

            reportableEventAttachmentsPage
               .WaitForReportableEventAttachmentsPageToLoad()
               .ClickCreateNewRecord();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderreportableeventattachment")
                .ClickOnExpandIcon();

            reportableEventAttchmentsRecordPage
                .WaitForReportableEventAttchmentsRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateTitleFieldErrormessage("Please fill out this field.")
                .ValidateDateFieldErrormessage("Please fill out this field.")
                .ValidateTimeFieldErrormessage("Please fill out this field.")
                .ValidateDocumentTypeErrormessage("Please fill out this field.")
                .ValidateDocumentSubTypeErrormessage("Please fill out this field.")
                .ValidateFileErrormessage("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "ACC-3177")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Should take user to attachments summary page -> Open any of existing record and Click on Delete Icon and give Yes in Confirmation Pop up for deletion ->Attachment record should be deleted successfully")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod03()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            _attachmentId = dbHelper.careProviderReportableEventAttachment.CreateCareProviderReportableEventAttachment(_reportableEventId, _careProviders_TeamId, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt");

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
               .ClickAttachmentLink();

            reportableEventAttachmentsPage
                .WaitForReportableEventAttachmentsPageToLoad()
                .OpenRecord(_attachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderreportableeventattachment")
                .ClickOnExpandIcon();

            reportableEventAttchmentsRecordPage
                .WaitForReportableEventAttchmentsRecordPageToLoad()
                .ClickAdditionalItemsMenuButton()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            reportableEventAttachmentsPage
                .WaitForReportableEventAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var attachmentId = dbHelper.careProviderReportableEventAttachment.GetByReportableEvent(_reportableEventId);
            Assert.AreEqual(0, attachmentId.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3178")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Should take user to attachments summary page -> Click on Delete Icon and give Yes in Confirmation Pop up for deletion -Attachment record should be deleted successfully")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod04()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            _attachmentId = dbHelper.careProviderReportableEventAttachment.CreateCareProviderReportableEventAttachment(_reportableEventId, _careProviders_TeamId, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt");

            loginPage
                  .GoToLoginPage()
                  .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                  .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
               .ClickAttachmentLink();

            reportableEventAttachmentsPage
                .WaitForReportableEventAttachmentsPageToLoad()
                .SelectAttchmentRecord(_attachmentId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("1 item(s) deleted.")
               .TapOKButton();

            reportableEventAttachmentsPage
                 .WaitForReportableEventAttachmentsPageToLoad();

            var attachmentId = dbHelper.careProviderReportableEventAttachment.GetByReportableEvent(_reportableEventId);
            Assert.AreEqual(0, attachmentId.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3179")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Should display new option to upload documents in bulk -> Click Upload Multiple Files option -> Choose Document Type and Sub Type - > Select multiple files and Click Start Upload -> Should get success alert for uploaded files" +
            "And also records should get created under attachment section for each uploaded files with appropriate values")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod05()
        {

            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            loginPage
                 .GoToLoginPage()
                 .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                 .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
               .ClickAttachmentLink();

            reportableEventAttachmentsPage
               .WaitForReportableEventAttachmentsPageToLoad()
               .ClickBulkCreateButton();

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToLoad()
               .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Discharge Summary").TapSearchButton().SelectResultElement(_documenttypeid.ToString());

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToReload()
               .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Confirmed Result").TapSearchButton().SelectResultElement(_documentsubtypeid.ToString());

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToReload()

               .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.txt")
               .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.txt")

               .ValidateAttachedFileNameVisibility(1, true)
               .ValidateAttachedFileNameVisibility(2, true)

               .ClickStartUploadButton();

            reportableEventAttachmentsPage
              .WaitForReportableEventAttachmentsPageToLoad();

        }

        [TestProperty("JiraIssueID", "ACC-3180")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Should display new option to upload documents in bulk -> Click Upload Multiple Files option -> Leave either one or all mandatory fields as blank / not select any file to upload and  Click Start Upload ->" +
            "Should get proper alert to fill all the required details , new attachment records should not get created")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod07()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            _attachmentId = dbHelper.careProviderReportableEventAttachment.CreateCareProviderReportableEventAttachment(_reportableEventId, _careProviders_TeamId, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt");

            loginPage
                  .GoToLoginPage()
                  .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                  .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
               .ClickAttachmentLink();

            reportableEventAttachmentsPage
                .WaitForReportableEventAttachmentsPageToLoad()
                .InsertQuickSearchText("test")
                .ClickQuickSearchButton()
                .ValidateTitleCellText("test");

        }

        [TestProperty("JiraIssueID", "ACC-3181")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Attachments ->" +
            "Should display new option to upload documents in bulk -> Click Upload Multiple Files option -> Leave either one or all mandatory fields as blank / not select any file to upload and  Click Start Upload ->" +
            "Should get proper alert to fill all the required details , new attachment records should not get created")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Attachments (For Reportable Events)")]
        public void ReportableEvents_UITestMethod06()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ClickSubMenu()
               .ClickAttachmentLink();

            reportableEventAttachmentsPage
                 .WaitForReportableEventAttachmentsPageToLoad()
                 .ValidateUploadMultipleButton("Upload Multiple Files")
                 .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickStartUploadButton()
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15832

        [TestProperty("JiraIssueID", "ACC-3182")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Reportable Event Actions ->" +
            "Should display list of existing records if exist and a + icon to create new record -> Click + Icon and Verify all the fields ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Actions")]
        public void ReportableEvents_UITestMethod08()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToReportableEventActions();

            reportableEventActionsPage
                .WaitForReportableEventActionsPageToLoad()
                .ValidateCreateNewRecordButton("New")
                .ClickCreateNewRecord();

            reportableEventActionsRecordPage
                .WaitForReportableEventActionsRecordPageToLoad()
                .ValidateActionIdMandatoryFieldAndNonEditable(true)
                .ValidateResponsibleUserMandatoryFieldAndPreFilled(true)
                .ValidateRelatedReportableEventMandatoryFieldAndPreFilled(true)
                .ValidateResponsibleTeamMandatoryFieldAndPreFilled(true)
                .ValidateActionMandatoryField(true)
                .ValidateStatusMandatoryField(true)
                .ValidateDecisionNonMandatoryField(false)
                .ValidateStartDateMandatoryField(true)
                .ValidateEndDateNonMandatoryField(false)
                .ValidateNextReviewDateNonMandatoryField(false)
                .ValidateDueDateMandatoryNonField(false);
        }

        [TestProperty("JiraIssueID", "ACC-3183")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Open any of the existing reportable event  -> Related Items -> Reportable Event Actions ->" +
            "Should display list of existing records if exist and a + icon to create new record -> Click + Icon and Record should get saved successfully with all the entered details -> " +
            "Should display the newly created record in View page with respective values for each column ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Actions")]
        public void ReportableEvents_UITestMethod09()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            foreach (var CareProviderReportableEventActionId in dbHelper.careProviderReportableEventAction.GetByReportableEventID(_reportableEventId))
                dbHelper.careProviderReportableEventAction.DeleteCareproviderReportableEventAction(CareProviderReportableEventActionId);

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToReportableEventActions();

            reportableEventActionsPage
                .WaitForReportableEventActionsPageToLoad()
                .ValidateCreateNewRecordButton("New")
                .ClickCreateNewRecord();

            reportableEventActionsRecordPage
               .WaitForReportableEventActionsRecordPageToLoad()
               .ClickAssignedUserLookup();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            reportableEventActionsRecordPage
               .WaitForReportableEventActionsRecordPageToLoad()
               .InsertTextActionField("action")
               .InsertTextDecisionField("decision")
               .SelectStatusId("Completed")
               .InsertStartDateField(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertNextReviewDateField(DateTime.Now.AddDays(2).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertDueDateField(DateTime.Now.AddDays(2).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .InsertEndDateField(DateTime.Now.AddDays(2).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);
            var reportableEventAction = dbHelper.careProviderReportableEventAction.GetByReportableEventID(_reportableEventId);
            Assert.AreEqual(1, reportableEventAction.Count);

            reportableEventActionsPage
              .WaitForReportableEventActionsPageToLoad()
              .ValidateRecordData(reportableEventAction[0].ToString(), 3, "action")
              .ValidateRecordData(reportableEventAction[0].ToString(), 4, DateTime.Today.Date.AddDays(-3).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture))
              .ValidateRecordData(reportableEventAction[0].ToString(), 6, "Completed");
        }

        [TestProperty("JiraIssueID", "ACC-3184")]
        [Description("Login CD -> Work Place -> Quality and Compliance -> Select any existing reportable Events -> Related Items -> Reportable Event Actions " +
            "Should display list of existing records with below columns -> Id,Start Date,Action,Status,Default sort order : Start Date DESC")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Actions")]
        public void ReportableEvents_UITestMethod010()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion


            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToReportableEventActions();

            reportableEventActionsPage
               .WaitForReportableEventActionsPageToLoad()
               .ValidateRecordCellText(2, "Action Id")
               .ValidateRecordCellText(3, "Action")
               .ValidateRecordCellText(4, "Start Date")
               .ValidateRecordCellText(5, "Third Party")
               .ValidateRecordCellText(6, "Status")
               .ValidateRecordCellText(7, "Created By")
               .ValidateRecordCellText(8, "Created On")
               .ValidateRecordCellText(9, "Modified By")
               .ValidateRecordCellText(10, "Modified On");
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-16161

        [TestProperty("JiraIssueID", "ACC-3185")]
        [Description("Login CD -> Work Place -> Quality and Compliance -> Select any existing reportable Events -> Related Items -> Reportable Event Actions " +
            "Should display list of existing records with below columns -> Id,Start Date,Action,Status,Default sort order : Start Date DESC")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod011()
        {
            var newReportableEventInactive = dbHelper.careproviderReportableEvent.GetInactiveRecordsByResponsibleUserId(_systemUserID).Any();
            if (!newReportableEventInactive)
            {
                reportableeventIdInactive = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventInactiveRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId, DateTime.Now, DateTime.Now);
            }
            if (reportableeventIdInactive == Guid.Empty)
            {
                reportableeventIdInactive = dbHelper.careproviderReportableEvent.GetByResponsibleUserId(_systemUserID).FirstOrDefault();
            }
            var reportableeventIdInactiveNumber = dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(reportableeventIdInactive, "identifier")["identifier"];

            loginPage
                 .GoToLoginPage()
                 .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                 .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SelectAvailableViewByText("Inactive Records")
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(reportableeventIdInactiveNumber.ToString())
                .OpenReportableEventRecord(reportableeventIdInactive.ToString());

            reportableEventRecordPage
                  .WaitForReportableEventInactiveRecordageToLoad()
                  .ValidateEndDateFieldText(EndDate.ToString("dd/MM/yyyy").Replace('-', '/'));

        }


        [TestProperty("JiraIssueID", "ACC-3186")]
        [Description("Login Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Click + icon to create new record -> Fill all mandatory details and save.Record should be saved successfully and displayed as existing records in view." +
            "Open the same records from view and try to edit the Start Date and Time->Start Date and Time should be in non editable mode.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod012()
        {
            string CurrentDate = commonMethodsHelper.GetCurrentDate();

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .ClickNewRecordButton();

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapGeneralSeverityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Major").TapSearchButton().SelectResultElement(_reportableEventTypeSeverityId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Accident").TapSearchButton().SelectResultElement(_reportableEventTypeId.ToString());

            reportableEventRecordPage
             .WaitForReportableEventRecordPagePageToLoad()
             .InsertStartDate(CurrentDate)
             .TapEventTypeStatusLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Stage 1").TapSearchButton().SelectResultElement(_reportableEventStatusId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .ClickSaveAndCloseButton();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .ClickRefreshButton()
                .WaitForReportableEventPageToLoad();

            System.Threading.Thread.Sleep(1000);

            var reportableEventRecords = dbHelper.careproviderReportableEvent.GetByResponsibleUserId(_systemUserID);

            Assert.AreEqual(1, reportableEventRecords.Count);
            var newReportableEventId = reportableEventRecords.FirstOrDefault();
            var newReportableEventNumber = dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(newReportableEventId, "identifier")["identifier"];

            reportableEventPage
                .SearchReportableEventsRecord(newReportableEventNumber.ToString())
                .OpenReportableEventRecordUsingID(newReportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .ValidateStartDateNonEditable()
                .ValidateStartTimeNonEditable();

        }


        [TestProperty("JiraIssueID", "ACC-3187")]
        [Description("Login Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Click + icon to create new record -> Check for the Impact section" +
            "Impact section should not be displayed until the record get saved.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod013()
        {

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .ClickNewRecordButton();

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .ValidateImpactFieldVisible(false);

        }


        [TestProperty("JiraIssueID", "ACC-3188")]
        [Description("Login Care Provider -> Work Place -> Quality and Compliance -> Reportable Events -> Check the columns " +
            "Should display below column headers with appropriate values Event ID, Responsible user" +
            "Start Date,Event type,General severity,Event status,End date,Status changed date,Status changed by,Created on,Created  by,Modified onModified  by" +
            "and order by Event ID instead of Start Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod014()
        {
            var _reportable_Event1 = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportable_Event2 = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad();

            System.Threading.Thread.Sleep(3000);

            reportableEventPage
                .ValidateEventIdHeaderText("Event Id")
                .ValidateResponsibleUserHeaderText("Responsible User")
                .ValidateResponsibleTeamHeaderText("Responsible Team")
                .ValidateStartDateHeaderText("Start Date")
                .ValidateEventTypeHeaderText("Event Type")
                .ValidateGeneralSeverityHeaderText("General Severity")
                .ValidateEventStatusHeaderText("Event Status")
                .ValidateEndDateHeaderText("End Date")
                .ValidateStatusChangedByHeaderText("Status Changed By")
                .ValidateStatusChangedHeaderText("Status Changed")
                .ValidatecreatedByHeaderText("Created By")
                .ValidatecreatedOnHeaderText("Created On")
                .ValidateModifiedByHeaderText("Modified By")
                .ValidateModifiedOnHeaderText("Modified On");

            reportableEventPage
                .SearchReportableEventsRecord(_systemUserFirstName + " " + _systemUserLastName)
                .WaitForReportableEventPageToLoad()
                .ClickEventIdButton()
                .ValidateRecordInPosition(1, _reportable_Event1.ToString())
                .ValidateRecordInPosition(2, _reportable_Event2.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15699

        [TestProperty("JiraIssueID", "ACC-3189")]
        [Description("Login CD Care Provider -> Work Place -> Quality and Compliance -> Reportable Events" +
            "Should display list of existing records if exist and a + icon to create new record." +
            "Click + Icon and Verify the fields->All the Fields should be verified")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod015()
        {
            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .ValidateNewRecordIcon(true)
                .ClickNewRecordButton();

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad();
            System.Threading.Thread.Sleep(1000);

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
                .ValidateEventIdNonEditable()
                .ValidateResponsibleTeamMandatoryFieldSignVisible(true)
                .ValidateResponsibleUserMandatoryFieldSignVisible(true)
                .ValidateReportableEventSummaryGeneralSeverityField(true, "")
                .ValidateReportableEventSummaryEventTypeField(true, "")
                .ValidateReportableEventSummaryCategoryField(true, "")
                .ValidateReportableEventSummarySubCategoryField(true, "0")
                .ValidateReportableEventContextStartDate(true)
                .ValidateReportableEventContextEndDate(true)
                .ValidateReportableEventCategoryPrimaryCauseField(true, "0")
                .ValidateReportableEventCategoryUnderlyingCauseField(true, "0")
                .ValidateReportableEventManagingEventStatusField(true, "0")
                .ValidateReportableEventManagingStatusChangedField(true, "")
                .ValidateReportableEventManagingStatusChangedByField(true)
                .ValidateReportableentNotesText(true);//notes field is not getting focused
        }


        [TestProperty("JiraIssueID", "ACC-3190")]
        [Description("Login CD -> Work Place -> Quality and Compliance -> Reportable Events" +
            "Click + icon and enter all the details and save->Should display list of  records if exist and a + icon to create new record." +
            "Click + Icon and Verify the fields->All the Fields should be verified")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod016()
        {
            string CurrentDate = commonMethodsHelper.GetCurrentDate();

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .ClickNewRecordButton();

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                 .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserID.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapGeneralSeverityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Major").TapSearchButton().SelectResultElement(_reportableEventTypeSeverityId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapEventTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Accident").TapSearchButton().SelectResultElement(_reportableEventTypeId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .InsertStartDate(CurrentDate);

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .TapEventTypeStatusLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Stage 1").TapSearchButton().SelectResultElement(_reportableEventStatusId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .ClickSaveAndCloseButton();

            //get the record
            System.Threading.Thread.Sleep(1000);

            reportableEventPage
               .WaitForReportableEventPageToLoad()
               .ClickRefreshButton()
               .WaitForReportableEventPageToLoad();


            var reportableEventRecords = dbHelper.careproviderReportableEvent.GetByResponsibleUserId(_systemUserID);

            Assert.AreEqual(1, reportableEventRecords.Count);
            var newReportableEventId = reportableEventRecords.FirstOrDefault();

            reportableEventPage
                .SearchReportableEventsRecord(_systemUserFirstName + " " + _systemUserLastName)
                .OpenReportableEventRecordUsingID(newReportableEventId.ToString());
        }


        [TestProperty("JiraIssueID", "ACC-3191")]
        [Description("Login CD -> Work Place -> Quality and Compliance -> Reportable Events" +
            "Should display list of  records if exist and a + icon to create new record." +
            "Click + Icon and Verify the fields->All the Fields should be verified")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        public void ReportableEvents_UITestMethod017()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad();
            System.Threading.Thread.Sleep(3000);

            reportableEventPage
                .ValidateEventIdHeaderText("Event Id")
                .ValidateResponsibleUserHeaderText("Responsible User")
                .ValidateResponsibleTeamHeaderText("Responsible Team")
                .ValidateStartDateHeaderText("Start Date")
                .ValidateEventTypeHeaderText("Event Type")
                .ValidateGeneralSeverityHeaderText("General Severity")
                .ValidateEventStatusHeaderText("Event Status")
                .ValidateEndDateHeaderText("End Date")
                .ValidateStatusChangedByHeaderText("Status Changed By")
                .ValidateStatusChangedHeaderText("Status Changed")
                .ValidatecreatedByHeaderText("Created By")
                .ValidatecreatedOnHeaderText("Created On")
                .ValidateModifiedByHeaderText("Modified By")
                .ValidateModifiedOnHeaderText("Modified On");

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-17292

        [TestProperty("JiraIssueID", "ACC-3192")]
        [Description("Verify the new section Injuries in reportable event impact Pre Requisites:Reportable Event Exist already")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod18()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            var _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("English").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            #endregion

            #region Provider

            Guid _providerID;
            int _providerNumber;

            if (!dbHelper.provider.GetProviderByName("Testing_CDV6_17292").Any())
                dbHelper.provider.CreateProvider("Testing_CDV6_17292", _careDirectorQA_TeamId, 3);
            _providerID = dbHelper.provider.GetProviderByName("Testing_CDV6_17292")[0];
            _providerNumber = (int)(dbHelper.provider.GetProviderByID(_providerID, "providernumber")["providernumber"]);

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = dbHelper.person.CreatePersonRecord("", "Jhon", "CDV6-16205_" + currentDate, "Smith", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .NavigateToImpactsTab();

            reportableEventImpactsPage
                .WaitForReportableEventImpactsPageToLoad()
                .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                .TapInternalPersonOrganisationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personID.ToString());

            reportableEventImpactRecordPages
                  .WaitForReportableEventImpactsRecordPageToLoad()
                  .SelectImpactTypeByText("Injury")
                  .ValidateInjuriesTextHeader("Injuries")
                  .ValidateSeverityOfInjuriesMandatoryField(true);

            //Step2->Select any value other then people record for "Internal Person/Organization" -> Select "Impact Type" as "Injury"Should not display a section "Injuries"
            reportableEventImpactRecordPages
                .TapInternalPersonOrganisationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("Providers").TypeSearchQuery(_providerNumber.ToString()).TapSearchButton().SelectResultElement(_providerID.ToString());

            reportableEventImpactRecordPages
                  .WaitForReportableEventImpactsRecordPageToLoad()
                  .SelectImpactTypeByText("Injury")
                  .ValidateSeverityOfInjuriesMandatoryField(false)

              //Step 3
              .TapInternalPersonOrganisationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personID.ToString());

            reportableEventImpactRecordPages
                 .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectImpactTypeByText("Other")
                 .ValidateSeverityOfInjuriesMandatoryField(false);

        }



        [TestProperty("JiraIssueID", "ACC-3193")]
        [Description("Verify Person Body Maps under reportable event impact Pre Requisites:Reportable Event exist Reportable Event Impact exist")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod19()
        {

            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("English").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = dbHelper.person.CreatePersonRecord("", "Jhon", "CDV6-16205_" + currentDate, "Smith", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            var providerid = new Guid("24f2bf7e-1958-ec11-a32d-f90a4322a942");

            #endregion

            #region create impact record

            if (!dbHelper.careProviderReportableEventImpact.GetByReportableEventID(_reportableEventId).Any())
            {
                _reportableEventImpactId = dbHelper.careProviderReportableEventImpact.CreateCareproviderReportableEventImpactRecord(_reportableEventId, 1, _personID, _reportableEventTypeSeverityId, DateTime.Now, _careDirectorQA_TeamId, "person", "Jhon Smith", _reportableEventTypeRoleId);
            }
            if (_reportableEventImpactId == Guid.Empty)
            {
                _reportableEventImpactId = dbHelper.careproviderReportableEvent.GetByResponsibleUserId(_systemUserID).FirstOrDefault();
            }

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .NavigateToImpactsTab();

            reportableEventImpactsPage
                .WaitForReportableEventImpactsPageToLoad()
                .OpenRecord(_reportableEventImpactId.ToString());

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                .ValidateImpactTypeFieldText("1")
                .WaitForReportableEventImpactsPersonBodyMapsPageToLoad()
                .ValidatePersonBodyMapsTitleVisible(true)
                .ClickCreatePersonBodyMapRecordButton();

            reportableEventImpactPersonBodymapRecordPages
                .WaitForReportableEventImpactsPersonBodyMapsRecordPageToLoad()
                .ValidateReportableEventImpactPersonFieldText("Jhon Smith")
                .ValidateReportableEventImpactFieldText(_reportableEventImpactId, "Injury Jhon Smith")
                .ValidateDateOfEventFieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"));

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-17294

        [TestProperty("JiraIssueID", "ACC-3194")]
        [Description("Verify the creation of reportable event impact record Pre Requisites:Reportable Event Exist already")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod20()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("English").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            #endregion

            #region Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = dbHelper.person.CreatePersonRecord("", "Jhon", "CDV6-16205_" + currentDate, "Smith", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();


            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToImpactsTab();

            reportableEventImpactsPage
                        .WaitForReportableEventImpactsPageToLoad()
                        .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                .ValidateImpactRecordReportableEventField("Accident")
                .ValidateIsExternalPersonOrOrganisationField(false)
                .SelectIsExternalPersonOrOrganisation(true)
                .ValidatExternaPersonOrganisationFieldText("")
                 .SelectIsExternalPersonOrOrganisation(false)
                 .ValidatInternalPersonOrganisationFieldText("")
                 .ValidatRoleInEventFieldText("")
                 .ValidateImpactTypeFieldText("")
                 .ValidatNotesField()
            //fill all the mandatory and non mandatory fields and click save and close
               .TapInternalPersonOrganisationLookupButton();

            //lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Major").TapSearchButton().SelectResultElement(_reportableEventTypeSeverityId.ToString());
            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personID.ToString());

            reportableEventImpactRecordPages
                  .WaitForReportableEventImpactsRecordPageToLoad()
                  .SelectImpactTypeByText("Injury")
                  .ValidateInjuriesTextHeader("Injuries")
                  .ValidateSeverityOfInjuriesMandatoryField(true)
                  .TapSeverityOfInjuriesLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Major").TapSearchButton().SelectResultElement(_reportableEventTypeSeverityId.ToString());

            reportableEventImpactRecordPages
                 .WaitForReportableEventImpactsRecordPageToLoad()
                 .TapRoleInEvevntLookupButton();


            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Victim").TapSearchButton().SelectResultElement(_reportableEventTypeRoleId.ToString());

            reportableEventImpactRecordPages
                 .WaitForReportableEventImpactsRecordPageToLoad()
                .ClickSaveButton();

        }


        [TestProperty("JiraIssueID", "ACC-3195")]
        [Description("Verify the Is External Person / Organisation ? = Yes option in reportable event impact creationPre Requisites:Reportable Event Exist already")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod21()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .NavigateToImpactsTab();

            reportableEventImpactsPage
                .WaitForReportableEventImpactsPageToLoad()
                .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectIsExternalPersonOrOrganisation(true)
                  .ValidatExternaPersonOrganisationFieldText("")
                 .ValidatExternaPersonOrganisationContactFieldText("")
                 .ValidatInternalPersonOrganisationField(false);

        }


        [TestProperty("JiraIssueID", "ACC-3196")]
        [Description("Verify the Is External Person / Organisation ? = No option in reportable event impact creationPre Requisites:Reportable Event Exist already")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod22()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .NavigateToImpactsTab();

            reportableEventImpactsPage
                .WaitForReportableEventImpactsPageToLoad()
                .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectIsExternalPersonOrOrganisation(false)
                  .ValidatInternalPersonOrganisationFieldText("")
                 .ValidatExternalPersonOrganisationField(false)
                 .ValidatExternalPersonOrganisationContactField(false);

        }

        [TestProperty("JiraIssueID", "ACC-3197")]
        [Description("Verify the view of reportable event impact records Pre Requisites:Reportable Event Exist already")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_UITestMethod23()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = dbHelper.person.CreatePersonRecord("", "Jhon", "CDV6-16205_" + currentDate, "Smith", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var _reportableEventImpactId_Injury = dbHelper.careProviderReportableEventImpact.CreateCareproviderReportableEventImpactRecord(_reportableEventId, 1, _personID, _reportableEventTypeSeverityId, DateTime.Now, _careDirectorQA_TeamId, "person", "Jhon smith", _reportableEventTypeRoleId);
            var _reportableEventImpactId_NearMiss = dbHelper.careProviderReportableEventImpact.CreateCareproviderReportableEventImpactRecord(_reportableEventId, 2, _personID, _reportableEventTypeSeverityId, DateTime.Now, _careDirectorQA_TeamId, "person", "Jhon smith", _reportableEventTypeRoleId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();

            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
                .WaitForReportableEventRecordPagePageToLoad()
                .NavigateToImpactsTab();

            reportableEventImpactsPage
                .WaitForReportableEventImpactsPageToLoad()
                .ValidateInternalPersonOrganisationHeaderText("Internal Person/Organisation")
                .ValidateExternalPersonOrganisationHeaderText("External Person/Organisation Name")
                .ValidateRoleInEventHeaderText("Role in Event")
                .ValidateImpactTypeHeaderText("Impact Type")
                    .ValidateRecordInPosition(1, _reportableEventImpactId_Injury.ToString())
                    .ValidateRecordInPosition(2, _reportableEventImpactId_NearMiss.ToString());


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-17299

        [TestProperty("JiraIssueID", "ACC-3198")]
        [Description("Verify the new section Risk for Reportable event impact,Login Care Provider -> Work Place -> Quality and Compliance -> Reportable Events  -> Open any of the existing record -> Try to create new impact for the selected record -> Select Impact Type = Risk AND Is External Person/Organisation = NO,Should display new section Risk under “Involved Party” section and a non mandatory look up field Related Risk to select active organizational risks , default value should be blank.Fill other mandatory details of impact record and save.Impact Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_VerifyRiskReportableEventImapct()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
            var _risknumber = (int)dbHelper.organisationalRisk.GetByOrganisationalRiskID(_organisationalRisk1ID, "risknumber")["risknumber"];
            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();


            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToImpactsTab();

            reportableEventImpactsPage
                        .WaitForReportableEventImpactsPageToLoad()
                        .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectImpactTypeByText("Risk")
                  .SelectIsExternalPersonOrOrganisation(false)
                  .ValidateRelatedRiskField(true)
            .TapInternalPersonOrganisationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            reportableEventImpactRecordPages
                      .WaitForReportableEventImpactsRecordPageToLoad()
                      .TapRoleInEvevntLookupButton();


            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Victim").TapSearchButton().SelectResultElement(_reportableEventTypeRoleId.ToString());

            reportableEventImpactRecordPages
                 .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectImpactTypeByText("Risk")
                 .TapRelatedRiskLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery(_risknumber.ToString()).TapSearchButton().SelectResultElement(_organisationalRisk1ID.ToString());

            reportableEventImpactRecordPages
                             .WaitForReportableEventImpactsRecordPageToLoad()
                             .ClickSaveButton();

            reportableEventImpactsPage
                        .WaitForReportableEventImpactsPageToLoad()
                        .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                .SelectImpactTypeByText("Risk")
                .TapRelatedRiskLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery(_risknumber.ToString()).TapSearchButton().ValidateResultElementNotPresent(_organisationalRisk1ID.ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3199")]
        [Description("Verify the availability of  Risk section for Reportable event impact when the Impact Type is not Risk AND Is External Person/Organisation is not Yes,Login Care Provider -> Work Place -> Quality and Compliance -> Reportable Events  -> Open any of the existing record -> Try to create new impact for the selected record -> Select Impact Type = other than Risk AND Is External Person/Organisation = Yes,Should not display new section Risk under “Involved Party” section and a non mandatory look up field Related Risk to select active organizational risks ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Impacts")]
        public void ReportableEvents_VerifyRiskNotDisplayedReportableEventImapct()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            loginPage
             .GoToLoginPage()
             .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
             .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();


            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToImpactsTab();

            reportableEventImpactsPage
                        .WaitForReportableEventImpactsPageToLoad()
                        .ClickCreateNewRecord();

            reportableEventImpactRecordPages
                .WaitForReportableEventImpactsRecordPageToLoad()
                 .SelectImpactTypeByText("Injury")
                  .SelectIsExternalPersonOrOrganisation(true)
                  .ValidateRelatedRiskField(false);

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-16731

        [TestProperty("JiraIssueID", "ACC-3200")]
        [Description("Verify the fields of Reportable Event Behavior . Pre-Requistie:Reportable Events Exist.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Behaviours")]
        public void ReportableEvents_VerifyFieldsReportableEvent()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
            var _risknumber = (int)dbHelper.organisationalRisk.GetByOrganisationalRiskID(_organisationalRisk1ID, "risknumber")["risknumber"];

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();


            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToBehavioursTab();

            reportableEventBehaviourPage
                .WaitForReportableEventBehaviourPageToLoad()
                .ClickNewRecordButton();

            reportableEventBehaviourRecordPages
                .WaitForReportableEventBehaviourRecordPageToLoad()
                .VerifySequence_Field("1,100")
                .ValidateBehaviourActionTypeField(true)
                .ValidateBehaviourTypeField(true)
                .VerifyFrequency_Field("1,100")
                .ValidateReportableEventSeverityField(true)
                .ValidateReportableEventCommentsField(true);

        }

        [TestProperty("JiraIssueID", "ACC-3201")]
        [Description("Verify the creation of Reportable Event Behavior . Pre-Requistie:Reportable Events Exist.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Quality and Compliance")]
        [TestProperty("Screen1", "Reportable Events")]
        [TestProperty("Screen2", "Reportable Event Behaviours")]
        public void ReportableEvents_VerifyCreationOfReportableEvent()
        {
            #region Create Reportable Event

            var _reportableEventId = dbHelper.careproviderReportableEvent.CreateCareproviderReportableEventRecord(_systemUserID, _reportableEventTypeId, _reportableEventTypeSeverityId, _reportableEventStatusId, DateTime.Now, _systemUserID, _careProviders_TeamId);
            var _reportableEventNumber = (int)(dbHelper.careproviderReportableEvent.GetByCareproviderReportableEventID(_reportableEventId, "identifier")["identifier"]);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careProviders_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
            var _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careProviders_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
            var _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

            #endregion

            #region Get Reportable Event Count

            var ReportableCount = dbHelper.careproviderReportableEvent.GetInactiveRecords();

            #endregion

            #region Organizational Risk Type

            var riskTypeExists = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").Any();
            if (!riskTypeExists)
                dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "ImpactRiskTest", new DateTime(2022, 1, 1));
            var _riskTypeId = dbHelper.organisationalRiskType.GetByName("ImpactRiskTest").First();

            #endregion

            #region organisation risk

            var _organisationalRisk1ID = dbHelper.organisationalRisk.CreateOrganisationalRisk(_careProviders_TeamId, _riskTypeId, 1, 1, DateTime.Now.Date, null, 1, 1, "Testing CDV6-17299", null, 1, 1);

            #endregion

            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
            var _risknumber = (int)dbHelper.organisationalRisk.GetByOrganisationalRiskID(_organisationalRisk1ID, "risknumber")["risknumber"];

            var CPreportableeventbehaviouractiontype = dbHelper.CPReportableEventBehaviourActionType.CreateCPReportableEventBehaviourActionType(_systemUserID, _careProviders_TeamId, "TestAutomation14", 001, "Gov-001", DateTime.Now);
            var careproviderreportableeventbehaviourtype = dbHelper.CareproviderReportableEventBehaviourType.CreateCareproviderReportableEventBehaviourTypeRecord(_careProviders_TeamId, DateTime.Today, "TestBehaviourautomation11", _careProviders_BusinessUnitId, CPreportableeventbehaviouractiontype);

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReportableEventPage();


            reportableEventPage
                .WaitForReportableEventPageToLoad()
                .SearchReportableEventsRecord(_reportableEventNumber.ToString())
                .OpenReportableEventRecord(_reportableEventId.ToString());

            reportableEventRecordPage
               .WaitForReportableEventRecordPagePageToLoad()
               .NavigateToBehavioursTab();

            reportableEventBehaviourPage
                .WaitForReportableEventBehaviourPageToLoad()
                .ClickNewRecordButton();

            reportableEventBehaviourRecordPages
                .WaitForReportableEventBehaviourRecordPageToLoad()
                .InsertSequence_Field("100")
                .TapBehaviourActionTypeLookup();



            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("TestAutomation14").TapSearchButton().SelectResultElement(CPreportableeventbehaviouractiontype.ToString());

            reportableEventBehaviourRecordPages
               .WaitForReportableEventBehaviourRecordPageToLoad()
               .InsertFrequency_Field("100")
               .TapSeverityLookup();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Major").TapSearchButton().SelectResultElement(_reportableEventTypeSeverityId.ToString());

            reportableEventBehaviourRecordPages
              .WaitForReportableEventBehaviourRecordPageToLoad()
               .TapBehaviourTypeLookup();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("TestBehaviourautomation11").TapSearchButton().SelectResultElement(careproviderreportableeventbehaviourtype.ToString());

            reportableEventBehaviourRecordPages
               .WaitForReportableEventBehaviourRecordPageToLoad()
               .ClickSaveAndCloseButton();
            System.Threading.Thread.Sleep(2000);
            var careproviderreportableeventbehaviour = dbHelper.CareproviderReportableEventBehaviour.GetByeventId(_reportableEventId);
            Assert.AreEqual(1, careproviderreportableeventbehaviour.Count);

        }


        #endregion

    }

}


