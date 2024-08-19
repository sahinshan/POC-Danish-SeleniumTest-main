using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class CaseForm_SDEMapping_UITestCases : FunctionalTest
    {

        internal Guid _businessUnitId;
        internal Guid _teamId;
        internal Guid _authenticationproviderid;
        internal Guid _languageId;
        internal Guid _systemUserId;

        [TestInitialize()]
        public void ClassSetupMethod()
        {

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _businessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _businessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);
            _teamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
            {
                _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            }
            if (_languageId == Guid.Empty)
            {
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
            }
            #endregion

            #region System User - Testing_CDV6_10399

            var systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_10399")[0];

            if (!dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_10399").Any())
            {
                _systemUserId = dbHelper.systemUser.CreateSystemUser("Testing_CDV6_10399", "Testing", "CDV6_10399", "Testing CDV6-10399", "Passw0rd_!", "Testing_CDV6_10399@somemail.com", "Testing_CDV6_10399@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _businessUnitId, _teamId, true);

                //var securityProfileId_1 = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                //var securityProfileId_2 = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, securityProfileId_1);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, securityProfileId_2);
            }

            if (Guid.Empty == _systemUserId)
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_10399").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

            #endregion

        }

        #region Person Automation Form 1

        public string PersonAutomationForm1ID { get { return "c9ef906d-4af3-ea11-a2cd-005056926fe4"; } }
        public Guid WFDecimalDQPersonAutomationForm1 { get { return new Guid("2F7751A9-4AF3-EA11-A2CD-005056926FE4"); } }

        #endregion

        #region Automated UI Test Document 4

        public string AutomatedUITestDocument4ID { get { return "35be6337-b200-ea11-a2c7-005056926fe4"; } }
        public Guid WFDecimalDQIAutomatedUITestDocument4 { get { return new Guid("a769e53e-b200-ea11-a2c7-005056926fe4"); } }

        #endregion

        #region Automated UI Test Document 2

        public Guid WFMultipleChoiceDQIAutomatedUITestDocument2 { get { return new Guid("979a5f7b-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFDecimalDQIAutomatedUITestDocument2 { get { return new Guid("11c98281-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFMultipleResponseDQIAutomatedUITestDocument2 { get { return new Guid("b79edb8b-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFNumericDQIAutomatedUITestDocument2 { get { return new Guid("cdb46792-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFLookupDQIAutomatedUITestDocument2 { get { return new Guid("ddb46792-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFDateDQIAutomatedUITestDocument2 { get { return new Guid("8d3ded9a-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFShortAnswerDQIAutomatedUITestDocument2 { get { return new Guid("e2e56ea2-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFParagraphDQIAutomatedUITestDocument2 { get { return new Guid("2f4747a9-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFPicklistDQIAutomatedUITestDocument2 { get { return new Guid("5598ceaf-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFBooleanDQIAutomatedUITestDocument2 { get { return new Guid("d7d529c2-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFTimeDQIAutomatedUITestDocument2 { get { return new Guid("8dee77c8-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFLocationRow1DQIAutomatedUITestDocument2 { get { return new Guid("77e1c1d8-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFTestDecRow1DQIAutomatedUITestDocument2 { get { return new Guid("7be1c1d8-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFLocationRow2DQIAutomatedUITestDocument2 { get { return new Guid("7fe1c1d8-8500-ea11-a2c7-005056926fe4"); } }
        public Guid WFTestDecRow2DQIAutomatedUITestDocument2 { get { return new Guid("83e1c1d8-8500-ea11-a2c7-005056926fe4"); } }

        #endregion


        #region Previewing document

        [TestProperty("JiraIssueID", "CDV6-9954")]
        [Description("TEST 0001 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Validate that the message 'No SDE Maps have been created for this question.' is displayed in the popup window ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod1()
        {
            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .ValidateNotificationMessagePresent("No SDE Maps have been created for this question.");
        }

        [TestProperty("JiraIssueID", "CDV6-9955")]
        [Description("TEST 0002 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - Validate the popup header text")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod2()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .ValidateHeaderText("SDE Maps for \"WF Decimal\"");
        }

        [TestProperty("JiraIssueID", "CDV6-9956")]
        [Description("TEST 0003 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map From This Question button - Validate that the popup header text is changed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod3()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .ValidateCreatemapPopupHeaderText("Create SDE Maps from the current question to another");
        }

        [TestProperty("JiraIssueID", "CDV6-9957")]
        [Description("TEST 0004 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map From This Question button - Select a different document and a section & question of that document (table question)" +
            "Set data in all remaining fields in the popup - Tap on the Save button - " +
            "Validate that the new map information is displayed in the popup and the correct information is saved in the SDEMap table")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod4()
        {
            var documentID = new Guid("aa62ad58-a700-ea11-a2c7-005056926fe4"); //Automated UI Test Document 3

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().SelectResultElement(documentID.ToString());

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToReload()
                .SelectSectionByText("Section 1")
                .SelectQuestion("Test HQ; Test Dec; 1")
                .TapSaveButton();


            List<Guid> sdeMapIds = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(1, sdeMapIds.Count);
            string sdeMapId = sdeMapIds[0].ToString();

            manageSDEMapsPopup
                .ValidateFromThisQuestionSectionTitle()
                .ValidateFromThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 3")
                .ValidateFromThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateFromThisQuestionMappingLine3(sdeMapId, "Question: Test HQ")
                .ValidateFromThisQuestionMappingLine4(sdeMapId, "Secondary Column: Test Dec")
                .ValidateFromThisQuestionMappingLine5(sdeMapId, "Row: 1")
                .ValidateExecuteThisMappingWhenNewAssessmentIsCreatedButtonVisisble(sdeMapId)
                .ValidateNonConditionalMappingButtonVisisble(sdeMapId)
                .ValidateDeleteSDEMapButtonVisisble(sdeMapId)
                .ValidateEditSDEMapButtonVisisble(sdeMapId);

        }

        [TestProperty("JiraIssueID", "CDV6-9958")]
        [Description("TEST 0005 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map From This Question button - Select a different document and a section & question of that document (non-table question)" +
            "Set data in all remaining fields in the popup - Tap on the Save button - " +
            "Validate that the new map information is displayed in the popup and the correct information is saved in the SDEMap table")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod5()
        {
            var documentID = new Guid("aa62ad58-a700-ea11-a2c7-005056926fe4"); //Automated UI Test Document 3

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().SelectResultElement(documentID.ToString());

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToReload()
                .SelectSectionByText("Section 1")
                .SelectQuestion("WF Decimal")
                .TapSaveButton();


            List<Guid> sdeMapIds = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(1, sdeMapIds.Count);
            string sdeMapId = sdeMapIds[0].ToString();

            manageSDEMapsPopup
                .ValidateFromThisQuestionSectionTitle()
                .ValidateFromThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 3")
                .ValidateFromThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateFromThisQuestionMappingLine3(sdeMapId, "Question: WF Decimal")
                .ValidateExecuteThisMappingWhenNewAssessmentIsCreatedButtonVisisble(sdeMapId)
                .ValidateNonConditionalMappingButtonVisisble(sdeMapId)
                .ValidateDeleteSDEMapButtonVisisble(sdeMapId)
                .ValidateEditSDEMapButtonVisisble(sdeMapId);

        }

        [TestProperty("JiraIssueID", "CDV6-9959")]
        [Description("TEST 0006 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map To This Question button - Select a different document and a section & question of that document (non-table question)" +
            "Set data in all remaining fields in the popup - Tap on the Save button - " +
            "Validate that the new map information is displayed in the popup and the correct information is saved in the SDEMap table")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod6()
        {
            var documentID = new Guid("aa62ad58-a700-ea11-a2c7-005056926fe4"); //Automated UI Test Document 3

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapToThisQuestionButton()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().SelectResultElement(documentID.ToString());

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToReload()
                .SelectSectionByText("Section 1")
                .SelectQuestion("WF Decimal")
                .TapSaveButton();


            List<Guid> sdeMapIds = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(1, sdeMapIds.Count);
            string sdeMapId = sdeMapIds[0].ToString();

            manageSDEMapsPopup
                .ValidateToThisQuestionSectionTitle()
                .ValidateToThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 3")
                .ValidateToThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateToThisQuestionMappingLine3(sdeMapId, "Question: WF Decimal")
                .ValidateExecuteThisMappingWhenNewAssessmentIsCreatedButtonVisisble(sdeMapId)
                .ValidateNonConditionalMappingButtonVisisble(sdeMapId)
                .ValidateDeleteSDEMapButtonVisisble(sdeMapId)
                .ValidateEditSDEMapButtonVisisble(sdeMapId);

        }

        [TestProperty("JiraIssueID", "CDV6-9960")]
        [Description("TEST 0007 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map From This Question button - Select the same document and question (WF Decimal on Automated UI Test Document 4)" +
            "Set data in all remaining fields in the popup - Tap on the Save button - " +
            "Validate that a single record is created in the DB SDE Map table and that the SDE Map popup displays the map twice (From This Question and To This Question)")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod7()
        {
            var documentID = new Guid("35be6337-b200-ea11-a2c7-005056926fe4"); //Automated UI Test Document 4

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().SelectResultElement(documentID.ToString());

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToReload()
                .SelectSectionByText("Section 1")
                .SelectQuestion("WF Decimal")
                .TapSaveButton();


            List<Guid> sdeMapIds = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(1, sdeMapIds.Count);
            string sdeMapId = sdeMapIds[0].ToString();

            manageSDEMapsPopup
                .ValidateFromThisQuestionSectionTitle()
                .ValidateFromThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 4")
                .ValidateFromThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateFromThisQuestionMappingLine3(sdeMapId, "Question: WF Decimal")
                .ValidateToThisQuestionSectionTitle()
                .ValidateToThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 4")
                .ValidateToThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateToThisQuestionMappingLine3(sdeMapId, "Question: WF Decimal")
                .ValidateExecuteThisMappingWhenNewAssessmentIsCreatedButtonVisisble(sdeMapId)
                .ValidateNonConditionalMappingButtonVisisble(sdeMapId)
                .ValidateDeleteSDEMapButtonVisisble(sdeMapId)
                .ValidateEditSDEMapButtonVisisble(sdeMapId);

        }


        [TestProperty("JiraIssueID", "CDV6-9961")]
        [Description("TEST 0008 - Open a test document in preview mode - " +
            "Tap on the manage SDE Maps button for the decimal question - " +
            "Tap on the Create Map From This Question button - Set data in all fields in the popup - Tap on the Save button - " +
            "Validate that the new map information is displayed in the popup and the correct information is saved in the SDEMap table - " +
            "Tap on the delete button - Validate that the map is deleted (both on the DB and the UI)")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod8()
        {
            var documentID = new Guid("aa62ad58-a700-ea11-a2c7-005056926fe4"); //Automated UI Test Document 3

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().SelectResultElement(documentID.ToString());

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToReload()
                .SelectSectionByText("Section 1")
                .SelectQuestion("WF Decimal")
                .TapSaveButton();


            List<Guid> sdeMapIds = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(1, sdeMapIds.Count);
            string sdeMapId = sdeMapIds[0].ToString();

            manageSDEMapsPopup
                .ValidateFromThisQuestionSectionTitle()
                .ValidateFromThisQuestionMappingLine1(sdeMapId, "Document: Automated UI Test Document 3")
                .ValidateFromThisQuestionMappingLine2(sdeMapId, "Section: Section 1")
                .ValidateFromThisQuestionMappingLine3(sdeMapId, "Question: WF Decimal")
                .TapSDEMapDeleteButton(sdeMapId);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to delete this SDE Map?")
                .TapOKButton();

            manageSDEMapsPopup
                .WaitForRefreshIconToBeClose()
                .ValidateNotificationMessagePresent("No SDE Maps have been created for this question.");

            var maps = dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4);
            Assert.AreEqual(0, maps.Count);
        }


        #endregion


        #region Creating document with SDE Map

        [TestProperty("JiraIssueID", "CDV6-9962")]
        [Description("TEST 0009 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Validate that the SDE Map copy page is displayed the first time a user taps on the edit assessment button")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod9()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .ValidateWFMultipleChoiceAnswer("Option 2")
                .ValidateWFDecimalAnswer("19.00")
                .ValidateWFMultipleResponseAnswer("Day 1, Day 3")
                .ValidateWFNumericAnswer("12")
                .ValidateWFLookupAnswer("Luella Abbott")
                .ValidateWFDateAnswer("01/11/2019")
                .ValidateWFShortAnswer("Short Answer 1")
                .ValidateWFParagraphAnswer("Line 1 Line 2 Line 3")
                .ValidateWFPicklistAnswer("Budist")
                .ValidateWFBooleanAnswer("Yes")
                .ValidateWFTimeAnswer("09:45")
                .ValidateWFLocationRow1Answer("Location 1")
                .ValidateWFTestDecRow1Answer("12.34")
                .ValidateWFLocationRow2Answer("Location 2")
                .ValidateWFTestDecRow2Answer("56.78")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-9963")]
        [Description("TEST 0010 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Tap on the Save button - Validate that the user is redirected to the edit assessment page and that all Answers are copied from the original case form")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod10()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("19.00")
                .ValidateWFMultipleResponseOptionChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3)
                .ValidateWFNumericAnswer("12")
                .ValidateWFLookupLookupValue("Luella Abbott")
                .ValidateWFDateAnswer("01/11/2019")
                .ValidateWFShortAnswer("Short Answer 1")
                .ValidateWFParagraphAnswer("Line 1\r\nLine 2\r\nLine 3")
                .ValidateWFPicklistSelectedValue("Budist")
                .ValidateWFBoolean(true)
                .ValidateWFTimeAnswer("09:45")
                .ValidateLocationRow1Answer("Location 1")
                .ValidateTestDecRow1Answer("12.34")
                .ValidateLocationRow2Answer("Location 2")
                .ValidateTestDecRow2Answer("56.78")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-9964")]
        [Description("TEST 0011 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Tap on the Dont Copy button - Validate that the user is redirected to the edit assessment page and that no Answer is copied from the original case form")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod11()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapDontCopyButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionNotSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("")
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionNotChecked(3)
                .ValidateWFNumericAnswer("")
                .ValidateWFLookupLookupValue("")
                .ValidateWFDateAnswer("")
                .ValidateWFShortAnswer("")
                .ValidateWFParagraphAnswer("")
                .ValidateWFPicklistSelectedValue("")
                .ValidateWFBoolean(null)
                .ValidateWFTimeAnswer("")
                .ValidateLocationRow1Answer("")
                .ValidateTestDecRow1Answer("")
                .ValidateLocationRow2Answer("")
                .ValidateTestDecRow2Answer("");

        }

        [TestProperty("JiraIssueID", "CDV6-9965")]
        [Description("TEST 0012 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Tap on the Dont Copy button - Validate that the user is redirected to the edit assessment page and that no Answer is copied from the original case form - " +
            "Close and Reopen the assessment - Validate that the SDE Map copy page is displayed again")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod12()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapDontCopyButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionNotSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("")
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionNotChecked(3)
                .ValidateWFNumericAnswer("")
                .ValidateWFLookupLookupValue("")
                .ValidateWFDateAnswer("")
                .ValidateWFShortAnswer("")
                .ValidateWFParagraphAnswer("")
                .ValidateWFPicklistSelectedValue("")
                .ValidateWFBoolean(null)
                .ValidateWFTimeAnswer("")
                .ValidateLocationRow1Answer("")
                .ValidateTestDecRow1Answer("")
                .ValidateLocationRow2Answer("")
                .ValidateTestDecRow2Answer("")
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-9966")]
        [Description("TEST 0013 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Tap on the Save button - Validate that the user is redirected to the edit assessment page and that all Answers are copied from the original case form - " +
            "Close and Reopen the assessment - Validate that the SDE Map copy page is NOT displayed again")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod13()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("19.00")
                .ValidateWFMultipleResponseOptionChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3)
                .ValidateWFNumericAnswer("12")
                .ValidateWFLookupLookupValue("Luella Abbott")
                .ValidateWFDateAnswer("01/11/2019")
                .ValidateWFShortAnswer("Short Answer 1")
                .ValidateWFParagraphAnswer("Line 1\r\nLine 2\r\nLine 3")
                .ValidateWFPicklistSelectedValue("Budist")
                .ValidateWFBoolean(true)
                .ValidateWFTimeAnswer("09:45")
                .ValidateLocationRow1Answer("Location 1")
                .ValidateTestDecRow1Answer("12.34")
                .ValidateLocationRow2Answer("Location 2")
                .ValidateTestDecRow2Answer("56.78")
                .TapBackButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2EditAssessmentPage
               .WaitForEditAssessmentPageToLoad(caseFormID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9967")]
        [Description("TEST 0014 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Un-check the checkbox for the WF Multiple Choice, WF Decimal; WF Multiple Response; WF Numeric; WF Lookup; WF Date; WF Short Answer - Tap the Save button - " +
            "Validate that all answers are copied except for WF Multiple Choice, WF Decimal; WF Multiple Response; WF Numeric; WF Lookup; WF Date; WF Short Answer")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod14()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapWFMultipleChoiceCheckbox()
                .TapWFDecimalCheckbox()
                .TapWFMultipleResponseCheckbox()
                .TapWFNumericCheckbox()
                .TapWFLookupCheckbox()
                .TapWFDateCheckbox()
                .TapWFShortAnswerCheckbox()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionNotSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("")
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionNotChecked(3)
                .ValidateWFNumericAnswer("")
                .ValidateWFLookupLookupValue("")
                .ValidateWFDateAnswer("")
                .ValidateWFShortAnswer("")
                .ValidateWFParagraphAnswer("Line 1\r\nLine 2\r\nLine 3")
                .ValidateWFPicklistSelectedValue("Budist")
                .ValidateWFBoolean(true)
                .ValidateWFTimeAnswer("09:45")
                .ValidateLocationRow1Answer("Location 1")
                .ValidateTestDecRow1Answer("12.34")
                .ValidateLocationRow2Answer("Location 2")
                .ValidateTestDecRow2Answer("56.78")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-9968")]
        [Description("TEST 0014 - Create a second form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Open the Case Form record - Tap on the Edit Assessment Button - SDE Map copy page is displayed - " +
            "Un-check the checkbox for the WF Paragraph; WF Picklist; WF Boolean; WF Time; Test HQ" +
            "Validate that all answers are copied except for WF Paragraph; WF Picklist; WF Boolean; WF Time; Test HQ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Phoenix_CaseForms_SDEMapping_UITestMethod15()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "QA-CAS-000001-00475567";
            Guid caseid = new Guid("79da57ef-fb3a-e911-a2c5-005056926fe4");
            Guid personID = new Guid("8e01aaa4-06d5-4a2b-ad7d-a04935479b4a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapWFParagraphCheckbox()
                .TapWFPicklistCheckbox()
                .TapWFBooleanCheckbox()
                .TapWFTimeCheckbox()
                .TapWFLocationRow1Checkbox()
                .TapWFTestDecRow1Checkbox()
                .TapWFLocationRow2Checkbox()
                .TapWFTestDecRow2Checkbox()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionSelected(2)
                .ValidateWFMultipleChoiceOptionNotSelected(3)
                .ValidateWFDecimalAnswer("19.00")
                .ValidateWFMultipleResponseOptionChecked(1)
                .ValidateWFMultipleResponseOptionNotChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3)
                .ValidateWFNumericAnswer("12")
                .ValidateWFLookupLookupValue("Luella Abbott")
                .ValidateWFDateAnswer("01/11/2019")
                .ValidateWFShortAnswer("Short Answer 1")
                .ValidateWFParagraphAnswer("")
                .ValidateWFPicklistSelectedValue("")
                .ValidateWFBoolean(null)
                .ValidateWFTimeAnswer("")
                .ValidateLocationRow1Answer("")
                .ValidateTestDecRow1Answer("")
                .ValidateLocationRow2Answer("")
                .ValidateTestDecRow2Answer("")
                ;

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-8361 && https://advancedcsg.atlassian.net/browse/CDV6-8361

        [TestProperty("JiraIssueID", "CDV6-9969")]
        [Description("Open a test document in preview mode - Click on the manage SDE Maps button for the decimal question - " +
            "Click on the Create Map From This Question button - Set Document Category to 'Person Form' - " +
            "Click on the Document Lookup button - On the Lookup Button validate that we cannot search for documents that have the category different than 'Person Form' ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod001()
        {
            var documentID = new Guid("aa62ad58-a700-ea11-a2c7-005056926fe4"); //Automated UI Test Document 3

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .TapCreateMapFromThisQuestionButton()
                .WaitForCreateSDEMapControlsToLoad()
                .SelectDocumentCategory("Person Form")
                .ClickDocumentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automated UI Test Document").TapSearchButton().ValidateResultElementNotPresent(documentID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9970")]
        [Description("Open a test document in preview mode - Click on the manage SDE Maps button for the decimal question (has existing SDE Mapping From This Question) - " +
            "Click on SDE Mapping record edit button - Validate that the SDE Map popup only displays the 'When do you want to run this mapping' and 'Is Conditional' controls ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod002()
        {

            foreach (Guid SDEMapId in dbHelper.sdeMap.GetSDEMApsForDocumentQuestionIdentifier(WFDecimalDQIAutomatedUITestDocument4))
            {
                dbHelper.sdeMap.DeleteSDEMap(SDEMapId);
            }

            var sdeMapId = dbHelper.sdeMap.CreateSDEMap(WFDecimalDQIAutomatedUITestDocument4, WFDecimalDQPersonAutomationForm1, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 4", AutomatedUITestDocument4ID)
                .OpenDocumentRecord(AutomatedUITestDocument4ID);

            documentPage
                .WaitForDocumentPageToLoad()
                .TapPreviewButton();

            automatedUiTestDocument4PreviewPage
                .WaitForPreviewPageToLoad()
                .TapWFDecimalManageSDEMapsButton();

            manageSDEMapsPopup
                .WaitForManageSDEMapsPopupToLoad()
                .ClickEditSDEMapButton(sdeMapId.ToString())
                .WaitForCreateSDEMapControlsToLoadInEditMode();
        }

        [TestProperty("JiraIssueID", "CDV6-9971")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 - Linked person has a 'Person Automation Form 1' form with all answers set - " +
            "Open the case form record - Tap on the edit assessment button - Validate that the SDE Map copy page is displayed the first time a user taps on the edit assessment button")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod003()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-264334";
            Guid caseid = new Guid("9d9b6f7e-3540-e911-a2c5-005056926fe4");
            Guid personID = new Guid("35925337-6dd8-4b89-9932-1598d8a003ec");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .ValidateFromInfoLine1Text(2, "Person Automation Form 1 for Bret Sandoval Starting 01/01/2020 created by CW Forms Test User 1")
                .ValidateFromInfoLine2Text(2, "Document: Person Automation Form 1")
                .ValidateFromInfoLine3Text(2, "Section: Section 1")
                .ValidateFromInfoLine4Text(2, "Question: WF Decimal")
                .ValidateToInfoLine3Text(2, "Section: Section 1")
                .ValidateToInfoLine4Text(2, "Question: WF Decimal")
                .ValidateAnswerLine4Text(2, "8.51")

                .ValidateFromInfoLine1Text(3, "Person Automation Form 1 for Bret Sandoval Starting 01/01/2020 created by CW Forms Test User 1")
                .ValidateFromInfoLine2Text(3, "Document: Person Automation Form 1")
                .ValidateFromInfoLine3Text(3, "Section: Section 1")
                .ValidateFromInfoLine4Text(3, "Question: WF Numeric")
                .ValidateToInfoLine3Text(3, "Section: Section 1")
                .ValidateToInfoLine4Text(3, "Question: WF Numeric")
                .ValidateAnswerLine4Text(3, "12")
                ;


        }

        [TestProperty("JiraIssueID", "CDV6-9972")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 - Linked person has a 'Person Automation Form 1' form with all answers set - " +
            "Open the case form record - Tap on the edit assessment button - On the SDE Map copy page click on the Save button - Wait for the assessment page to load - " +
            "Validate that the answers were copied from the person form document")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod004()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-000005-4816";
            Guid caseid = new Guid("d23d7f3c-5275-ec11-a330-f90a4322a942");
            Guid personID = new Guid("03aac2f1-9368-4331-95c1-18ff034f8f64");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFDecimalAnswer("8.51")
                .ValidateWFNumericAnswer("12");
        }

        [TestProperty("JiraIssueID", "CDV6-9973")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 (WF Decimal question has an sde map set to 'Execute this mapping allways') - " +
            "Linked person has a 'Person Automation Form 1' form with WF Decimal answer set - " +
            "Open the case form record - Tap on the edit assessment button - On the SDE Map copy page click on the Save button - Wait for the assessment page to load - " +
            "Validate that the answers were copied from the person form document - Tap on the back button - wait for the case form page to load - ")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod005()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361670";
            Guid caseid = new Guid("5e59c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("e515b879-c34b-4009-86b1-dbde1acab409");
            Guid caseFormTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2
            DateTime startDate = new DateTime(2019, 10, 15);
            var personFormID = new Guid("f25ab07f-d992-eb11-a323-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(caseFormTypeID, 1);

            //remove all case Forms 
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, caseFormTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }


            //get the Document Question Identifiers
            var questionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1109")[0];

            //Save the decimal answer
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(personFormID, questionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateDecimalAnswer(documentAnswerID, 51.29m);


            //create a new case form
            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, caseFormTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFDecimalAnswer("51.29")
                .TapBackButton();

            //update the decimal answer in the person form
            dbHelper.documentAnswer.UpdateDecimalAnswer(documentAnswerID, 97.31m);

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            //Accoarding to Tevinder: "I think we only implemented first copy when case form is created". Even if we update the person form answer, it will not be copied after the case form was created
            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFDecimalAnswer("51.29");
        }

        [TestProperty("JiraIssueID", "CDV6-9974")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 (SDE Mapping Mode set to 'Copy answers always') - " +
            "Linked person has a 'Person Automation Form 1' form with all answers set - " +
            "Open the case form record - Tap on the edit assessment button - Wait for the assessment page to load - " +
            "Validate that the answers were automatically copied from the person form document")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod006()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361671";
            Guid caseid = new Guid("5f59c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("c5247ed3-8251-47b7-8137-36b9435026eb");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Copy answers always'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 2);

            //remove all Forms that have a start date on 2021-04-01 
            DateTime startDate = new DateTime(2021, 4, 1);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFDecimalAnswer("32.06")
                .ValidateWFNumericAnswer("61");
        }

        [TestProperty("JiraIssueID", "CDV6-9975")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 (WF PickList question with 'Default SDE Enabled' set to Yes) - " +
            "Linked person has a 'Person Automation Form 1' (WF PickList question with 'Default SDE Enabled' set to Yes) form with all answers set - " +
            "Open the case form record - Tap on the edit assessment button - Validate that the SDE Map popup is displayed - " +
            "Validate that the SDE Map popup displays the WF Picklist' answer from the person form")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod007()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361672";
            Guid caseid = new Guid("6059c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("b072b112-511f-4242-9133-03128f7b4d90");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2021-04-01 
            DateTime startDate = new DateTime(2021, 4, 1);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
            .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .ValidateFromInfoLine1Text(2, "Person Automation Form 1 for Jean Moody Starting 01/01/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(2, "Document: Person Automation Form 1")
                .ValidateFromInfoLine3Text(2, "Section: Section 1.1")
                .ValidateFromInfoLine4Text(2, "Question: WF PickList")
                .ValidateToInfoLine3Text(2, "Section: Section 1")
                .ValidateToInfoLine4Text(2, "Question: WF PickList")
                .ValidateAnswerLine4Text(2, "Christian");
        }

        [TestProperty("JiraIssueID", "CDV6-9976")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 (WF PickList question with 'Default SDE Enabled' set to Yes) - " +
            "Linked person has a 'Person Automation Form 1' (WF PickList question with 'Default SDE Enabled' set to Yes) form with all answers set - " +
            "Open the case form record - Tap on the edit assessment button - Validate that the SDE Map popup is displayed - Click on the save button - " +
            "Wait for the edit assessment page to load - Validate that the picklist answer is copied")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenPersonFormAndCaseForm_UITestMethod008()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361672";
            Guid caseid = new Guid("6059c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("b072b112-511f-4242-9133-03128f7b4d90");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2021-04-01 
            DateTime startDate = new DateTime(2021, 4, 1);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
            .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
            .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFPicklistSelectedValue("Christian");
        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8491

        [TestProperty("JiraIssueID", "CDV6-9977")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Linked person has other case records with case forms using the same document type - " +
            "Open the case form record - Tap on the edit assessment button - " +
            "Validate that the SDE Map copy page is displayed the first time a user taps on the edit assessment button - " +
            "Validate that the answers belong to the most recent case form linked any of the person cases")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenCaseFormAndCaseForm_UITestMethod001()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361673";
            Guid caseid = new Guid("6159c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("5bdb11e9-b478-445d-ab85-89a00eaa599a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID, startDate))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .ValidateFromInfoLine1Text(2, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(2, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(2, "Section: Section 1")
                .ValidateFromInfoLine4Text(2, "Question: WF Boolean")
                .ValidateToInfoLine3Text(2, "Section: Section 1")
                .ValidateToInfoLine4Text(2, "Question: WF Boolean")
                .ValidateAnswerLine4Text(2, "Yes");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(3, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(3, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(3, "Section: Section 1")
                .ValidateFromInfoLine4Text(3, "Question: WF Date")
                .ValidateToInfoLine3Text(3, "Section: Section 1")
                .ValidateToInfoLine4Text(3, "Question: WF Date")
                .ValidateAnswerLine4Text(3, "01/04/2021");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(4, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(4, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(4, "Section: Section 1")
                .ValidateFromInfoLine4Text(4, "Question: WF Decimal")
                .ValidateToInfoLine3Text(4, "Section: Section 1")
                .ValidateToInfoLine4Text(4, "Question: WF Decimal")
                .ValidateAnswerLine4Text(4, "33.48");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(5, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(5, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(5, "Section: Section 1")
                .ValidateFromInfoLine4Text(5, "Question: WF Lookup")
                .ValidateToInfoLine3Text(5, "Section: Section 1")
                .ValidateToInfoLine4Text(5, "Question: WF Lookup")
                .ValidateAnswerLine4Text(5, "David Abbott");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(6, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(6, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(6, "Section: Section 1")
                .ValidateFromInfoLine4Text(6, "Question: WF Multiple Choice")
                .ValidateToInfoLine3Text(6, "Section: Section 1")
                .ValidateToInfoLine4Text(6, "Question: WF Multiple Choice")
                .ValidateAnswerLine4Text(6, "Option 3");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(7, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(7, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(7, "Section: Section 1")
                .ValidateFromInfoLine4Text(7, "Question: WF Multiple Response")
                .ValidateToInfoLine3Text(7, "Section: Section 1")
                .ValidateToInfoLine4Text(7, "Question: WF Multiple Response")
                .ValidateAnswerLine4Text(7, "Day 2, Day 3");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(8, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(8, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(8, "Section: Section 1")
                .ValidateFromInfoLine4Text(8, "Question: WF Numeric")
                .ValidateToInfoLine3Text(8, "Section: Section 1")
                .ValidateToInfoLine4Text(8, "Question: WF Numeric")
                .ValidateAnswerLine4Text(8, "45");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(9, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(9, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(9, "Section: Section 1")
                .ValidateFromInfoLine4Text(9, "Question: WF Paragraph")
                .ValidateToInfoLine3Text(9, "Section: Section 1")
                .ValidateToInfoLine4Text(9, "Question: WF Paragraph")
                .ValidateAnswerLine4Text(9, "X A X B");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(10, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(10, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(10, "Section: Section 1")
                .ValidateFromInfoLine4Text(10, "Question: WF PickList")
                .ValidateToInfoLine3Text(10, "Section: Section 1")
                .ValidateToInfoLine4Text(10, "Question: WF PickList")
                .ValidateAnswerLine4Text(10, "Budist");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(11, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(11, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(11, "Section: Section 1")
                .ValidateFromInfoLine4Text(11, "Question: WF Short Answer")
                .ValidateToInfoLine3Text(11, "Section: Section 1")
                .ValidateToInfoLine4Text(11, "Question: WF Short Answer")
                .ValidateAnswerLine4Text(11, "Z 1");
            automatedUITestDocument2SDEMapCopyAnswersPage
                .ValidateFromInfoLine1Text(12, "Automated UI Test Document 2 for Mercado Grady - (1992-07-10 00:00:00) [QA-CAS-000001-00475588] Starting 01/04/2021 created by José Brazeta")
                .ValidateFromInfoLine2Text(12, "Document: Automated UI Test Document 2")
                .ValidateFromInfoLine3Text(12, "Section: Section 1")
                .ValidateFromInfoLine4Text(12, "Question: WF Time")
                .ValidateToInfoLine3Text(12, "Section: Section 1")
                .ValidateToInfoLine4Text(12, "Question: WF Time")
                .ValidateAnswerLine4Text(12, "13:20");
        }

        [TestProperty("JiraIssueID", "CDV6-9978")]
        [Description("Create a form in a Case record - Form type is Automated UI Test Document 2 - " +
            "Linked person has other case records with case forms using the same document type - " +
            "Open the case form record - Tap on the edit assessment button - " +
            "Validate that the SDE Map copy page is displayed the first time a user taps on the edit assessment button - " +
            "Validate that the answers belong to the most recent case form linked any of the person cases")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingBetweenCaseFormAndCaseForm_UITestMethod002()
        {
            Guid caredirectorQATeamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5");
            string caseNumber = "CAS-3-361673";
            Guid caseid = new Guid("6159c6c6-3640-e911-a2c5-005056926fe4");
            Guid personID = new Guid("5bdb11e9-b478-445d-ab85-89a00eaa599a");
            Guid formTypeID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4");

            //set the sde mapping mode to 'Let the user decide what answers to copy'
            dbHelper.document.UpdateSDEMappingModeId(formTypeID, 1);

            //remove all Forms that have a start date on 2019-10-15 
            DateTime startDate = new DateTime(2019, 10, 15);
            foreach (var assessmentID in dbHelper.caseForm.GetCaseFormsByCaseAndFormType(caseid, formTypeID))
            {
                foreach (var caseFormOutcomeID in dbHelper.caseFormOutcome.GetCaseFormOutcomesByCaseFormID(assessmentID))
                    dbHelper.caseFormOutcome.DeleteCaseFormOutcome(caseFormOutcomeID);

                dbHelper.caseForm.DeleteCaseForm(assessmentID);
            }

            Guid caseFormID = dbHelper.caseForm.CreateCaseFormRecord(caredirectorQATeamID, formTypeID, personID, caseid, startDate);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(caseFormID.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapEditAssessmentButton();

            automatedUITestDocument2SDEMapCopyAnswersPage
                .WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
                .TapSaveButton();

            automatedUITestDocument2EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(caseFormID.ToString())
                .ValidateWFMultipleChoiceOptionNotSelected(1)
                .ValidateWFMultipleChoiceOptionNotSelected(2)
                .ValidateWFMultipleChoiceOptionSelected(3)
                .ValidateWFDecimalAnswer("33.48")
                .ValidateWFMultipleResponseOptionNotChecked(1)
                .ValidateWFMultipleResponseOptionChecked(2)
                .ValidateWFMultipleResponseOptionChecked(3)
                .InsertWFNumericValue("45")
                .ValidateWFLookupLookupValue("David Abbott")
                .ValidateWFDateAnswer("01/04/2021")
                .ValidateWFShortAnswer("Z 1")
                .ValidateWFParagraphAnswer("X A\r\nX B")
                .ValidateWFPicklistSelectedValue("Budist")
                .ValidateWFBoolean(true)
                .ValidateWFTimeAnswer("13:20");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10399

        [TestProperty("JiraIssueID", "CDV6-11449")]
        [Description("Open a document - Click on the view mappings button - Wait for the SDE Map List popup to load - " +
            "Validate the pagination information in the 'from this Document to Other Documents' area")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingsPagePagination_UITestMethod01()
        {
            var documentID = new Guid("b055b727-69ae-eb11-a8d1-000d3a0b666b"); //Matt's Test - Screening tool test

            var page1Record = new Guid("dc73955c-8814-48cc-9470-2628f20be63e");
            var page2Record = new Guid("5e9db73a-07c3-4dc6-a506-70a33c23f83e");
            var page6Record = new Guid("9022821c-d055-4e7e-ae3b-d9079fc55c53");

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("Testing_CDV6_10399", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Matt", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .TapViewMappingsButton();

            sdeMapListPopup
                .WaitForSDEMapListPopupToLoad()

                .SwitchToMapsFromIframe()

                .ValidateFromSDEMapCurrentPageNumber("Page 1")
                .ValidateFromSDEMapPaginationInformation("1 - 50")
                .ValidateFromSDEMapRecordVisibility(page1Record.ToString(), true)
                .ValidateFromSDEMapRecordVisibility(page2Record.ToString(), false)
                .ValidateFromSDEMapRecordVisibility(page6Record.ToString(), false);
        }

        [TestProperty("JiraIssueID", "CDV6-11450")]
        [Description("Open a document - Click on the view mappings button - Wait for the SDE Map List popup to load - Navigate to the 2nd page - " +
            "Validate the pagination information in the 'from this Document to Other Documents' area")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingsPagePagination_UITestMethod02()
        {
            var documentID = new Guid("b055b727-69ae-eb11-a8d1-000d3a0b666b"); //Matt's Test - Screening tool test

            var page1Record = new Guid("dc73955c-8814-48cc-9470-2628f20be63e");
            var page2Record = new Guid("5e9db73a-07c3-4dc6-a506-70a33c23f83e");
            var page6Record = new Guid("9022821c-d055-4e7e-ae3b-d9079fc55c53");

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("Testing_CDV6_10399", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Matt", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .TapViewMappingsButton();

            sdeMapListPopup
                .WaitForSDEMapListPopupToLoad()

                .SwitchToMapsFromIframe()
                .ClickFromSDEMapNextPageButton()

                .ValidateFromSDEMapCurrentPageNumber("Page 2")
                .ValidateFromSDEMapPaginationInformation("51 - 100")
                .ValidateFromSDEMapRecordVisibility(page1Record.ToString(), false)
                .ValidateFromSDEMapRecordVisibility(page2Record.ToString(), true)
                .ValidateFromSDEMapRecordVisibility(page6Record.ToString(), false);
        }


        [TestProperty("JiraIssueID", "CDV6-11451")]
        [Description("Open a document - Click on the view mappings button - Wait for the SDE Map List popup to load - " +
            "Validate the pagination information in the 'to this Document to Other Documents' area")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingsPagePagination_UITestMethod03()
        {
            var documentID = new Guid("b055b727-69ae-eb11-a8d1-000d3a0b666b"); //Matt's Test - Screening tool test

            var page1Record = new Guid("dc73955c-8814-48cc-9470-2628f20be63e");
            var page2Record = new Guid("5e9db73a-07c3-4dc6-a506-70a33c23f83e");
            var page3Record = new Guid("7b605d10-c309-4e73-a307-7efa96e8d512");

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("Testing_CDV6_10399", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Matt", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .TapViewMappingsButton();

            sdeMapListPopup
                .WaitForSDEMapListPopupToLoad()

                .SwitchToMapsToIframe()

                .ValidateToSDEMapCurrentPageNumber("Page 1")
                .ValidateToSDEMapPaginationInformation("1 - 50")
                .ValidateToSDEMapRecordVisibility(page1Record.ToString(), true)
                .ValidateToSDEMapRecordVisibility(page2Record.ToString(), false)
                .ValidateToSDEMapRecordVisibility(page3Record.ToString(), false);
        }

        [TestProperty("JiraIssueID", "CDV6-11452")]
        [Description("Open a document - Click on the view mappings button - Wait for the SDE Map List popup to load - Navigate to the 2nd page - " +
            "Validate the pagination information in the 'to this Document to Other Documents' area")]
        [TestMethod]
        [TestCategory("UITest")]
        public void MappingsPagePagination_UITestMethod04()
        {
            var documentID = new Guid("b055b727-69ae-eb11-a8d1-000d3a0b666b"); //Matt's Test - Screening tool test

            var page1Record = new Guid("dc73955c-8814-48cc-9470-2628f20be63e");
            var page2Record = new Guid("5e9db73a-07c3-4dc6-a506-70a33c23f83e");
            var page3Record = new Guid("7b605d10-c309-4e73-a307-7efa96e8d512");

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("Testing_CDV6_10399", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Matt", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .TapViewMappingsButton();

            sdeMapListPopup
                .WaitForSDEMapListPopupToLoad()

                .SwitchToMapsToIframe()
                .ClickToSDEMapNextPageButton()

                .ValidateToSDEMapCurrentPageNumber("Page 2")
                .ValidateToSDEMapPaginationInformation("51 - 100")
                .ValidateToSDEMapRecordVisibility(page1Record.ToString(), false)
                .ValidateToSDEMapRecordVisibility(page2Record.ToString(), true)
                .ValidateToSDEMapRecordVisibility(page3Record.ToString(), false);
        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}
