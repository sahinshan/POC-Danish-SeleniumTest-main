using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Settings.FormsManagement
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Documents_UITestCases : FunctionalTest
    {
        private string _applicationName = "Citizen Portal";

        #region https://advancedcsg.atlassian.net/browse/CDV6-8300

        [TestProperty("JiraIssueID", "CDV6-24604")]
        [Description("Open a Document with Availability set to 'All Applications' - " +
            "Validate that the Application Access module is not available under the document menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentApplicationAccessLinkVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24605")]
        [Description("Open a Document with Availability set to 'Internal Use Only' - " +
            "Validate that the Application Access module is not available under the document menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "Internal Use Only"
            dbHelper.document.UpdateAvailability(documentID, 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentApplicationAccessLinkVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24606")]
        [Description("Open a Document with Availability set to 'Specific Applications' - " +
            "Validate that the Application Access module is available under the document menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod003()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentApplicationAccessLinkVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24607")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access - " +
            "validate that the Document Application Access sub page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod004()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24608")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access (Document has 1 record) - " +
            "Validate that the Document Application Access record is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod005()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal


            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            //remove all Document Application Access records for the document
            foreach (var daaID in dbHelper.documentApplicationAccess.GetByDocumentId(documentID))
                dbHelper.documentApplicationAccess.DeleteDocumentApplicationAccess(daaID);

            //create a new Document Application Access records for the document
            var documentApplicationAccessID = dbHelper.documentApplicationAccess.CreateDocumentApplicationAccess(applicationID, documentID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ValidateRecordPresent(documentApplicationAccessID.ToString())
                .ValidateRecordApplicationCellText(documentApplicationAccessID.ToString(), _applicationName)
                .ValidateRecordCanEditCellText(documentApplicationAccessID.ToString(), "No")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24609")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access (Document has 1 record) - " +
            "Open the Document Application Access record  - Validate the record fields")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod006()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal


            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            //remove all Document Application Access records for the document
            foreach (var daaID in dbHelper.documentApplicationAccess.GetByDocumentId(documentID))
                dbHelper.documentApplicationAccess.DeleteDocumentApplicationAccess(daaID);

            //create a new Document Application Access records for the document
            var documentApplicationAccessID = dbHelper.documentApplicationAccess.CreateDocumentApplicationAccess(applicationID, documentID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ClickOnRecord(documentApplicationAccessID.ToString());

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ValidiateDocumentFieldLinkText("Automation - Document Application Access")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(false)
                .ValidiateCanEditNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24610")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access (Document has 1 record) - " +
            "Open the Document Application Access record  - Update the Can Edit Field - Save, Close and Reopen the record - Validate that the Can Edit field is correctly updated")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod007()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal


            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            //remove all Document Application Access records for the document
            foreach (var daaID in dbHelper.documentApplicationAccess.GetByDocumentId(documentID))
                dbHelper.documentApplicationAccess.DeleteDocumentApplicationAccess(daaID);

            //create a new Document Application Access records for the document
            var documentApplicationAccessID = dbHelper.documentApplicationAccess.CreateDocumentApplicationAccess(applicationID, documentID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ClickOnRecord(documentApplicationAccessID.ToString());

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ClickOnRecord(documentApplicationAccessID.ToString());

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ValidiateDocumentFieldLinkText("Automation - Document Application Access")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(true)
                .ValidiateCanEditNoOptionChecked(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24611")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access - Tap on the add new record button - " +
            "Wait for the Document Application Access new record page to load - Set date in all fields - Tap in the save and close button - Validate that the new record is saved.")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod008()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal


            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            //remove all Document Application Access records for the document
            foreach (var daaID in dbHelper.documentApplicationAccess.GetByDocumentId(documentID))
                dbHelper.documentApplicationAccess.DeleteDocumentApplicationAccess(daaID);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ClickOnNewRecordButton();

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ClickApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText(_applicationName);

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad();

            var records = dbHelper.documentApplicationAccess.GetByDocumentId(documentID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.documentApplicationAccess.GetByID(records[0], "documentid", "applicationid", "canedit");
            Assert.AreEqual(documentID, fields["documentid"]);
            Assert.AreEqual(applicationID, fields["applicationid"]);
            Assert.AreEqual(true, fields["canedit"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24612")]
        [Description("Open a Document with Availability set to 'Specific Applications' - Navigate to the Document Application Access (Document has 1 record) - " +
            "Open the Document Application Access record  - Click on the delete button - confirm the delete operation - validate that the record is deleted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentApplicationAccess_UITestMethod009()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal


            //set Availability to "Specific Applications"
            dbHelper.document.UpdateAvailability(documentID, 3);

            //remove all Document Application Access records for the document
            foreach (var daaID in dbHelper.documentApplicationAccess.GetByDocumentId(documentID))
                dbHelper.documentApplicationAccess.DeleteDocumentApplicationAccess(daaID);

            //create a new Document Application Access records for the document
            var documentApplicationAccessID = dbHelper.documentApplicationAccess.CreateDocumentApplicationAccess(applicationID, documentID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickMenuButton()
                .ClickDocumentApplicationAccessLink();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad()
                .ClickOnRecord(documentApplicationAccessID.ToString());

            documentApplicationAccessRecordPage
                .WaitForDocumentApplicationAccessRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            documentApplicationAccessPage
                .WaitForDocumentApplicationAccessPageToLoad();

            System.Threading.Thread.Sleep(3000);


            var records = dbHelper.documentApplicationAccess.GetByDocumentId(documentID);
            Assert.AreEqual(0, records.Count);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8301

        [TestProperty("JiraIssueID", "CDV6-24613")]
        [Description("Open a Document record - Validate that the Availability field is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentsByApplications_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ValidateAvailabilityFieldVisibility(true)
                .ValidateAvailabilityFieldSelectedText("All Applications");
        }

        [TestProperty("JiraIssueID", "CDV6-24614")]
        [Description("Open a Document record - Update the selected value for the Availability field - Save and close the document record - " +
            "Validate that the Availability field is correctly saved")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentsByApplications_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access

            //set Availability to "All Applications"
            dbHelper.document.UpdateAvailability(documentID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .SelectAvailability("Specific Applications")
                .ClickSaveAndCloseButton();
            System.Threading.Thread.Sleep(2000);

            documentsPage
                .WaitForDocumentsPageToLoad();

            var fields = dbHelper.document.GetDocumentByID(documentID, "availabilityid");
            Assert.AreEqual(3, fields["availabilityid"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8307

        [TestProperty("JiraIssueID", "CDV6-24615")]
        [Description("Open a Document Section with Availability set to 'All Applications' - " +
            "Validate that the Application Access module is not available under the document section menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionApplicationAccessLinkVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24616")]
        [Description("Open a Document Section with Availability set to 'Internal Use Only' - " +
            "Validate that the Application Access module is not available under the document section menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionApplicationAccessLinkVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24617")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - " +
            "Validate that the Application Access module is available under the document section menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod003()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionApplicationAccessLinkVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24618")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access - " +
            "validate that the Document Section Application Access sub page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod004()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24619")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access (Document Section has 1 record) - " +
            "Validate that the Document Section Application Access record is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod005()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID))
                dbHelper.documentSectionApplicationAccess.DeleteDocumentSectionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionApplicationAccessID = dbHelper.documentSectionApplicationAccess.CreateDocumentSectionApplicationAccess(applicationID, sectionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ValidateRecordPresent(documentSectionApplicationAccessID.ToString())
                .ValidateRecordApplicationCellText(documentSectionApplicationAccessID.ToString(), _applicationName)
                .ValidateRecordCanEditCellText(documentSectionApplicationAccessID.ToString(), "No")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24620")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access (Document Section has 1 record) - " +
            "Open the Document Section Application Access record  - Validate the record fields")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod006()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID))
                dbHelper.documentSectionApplicationAccess.DeleteDocumentSectionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionApplicationAccessID = dbHelper.documentSectionApplicationAccess.CreateDocumentSectionApplicationAccess(applicationID, sectionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionApplicationAccessID.ToString());

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ValidiateSectionFieldLinkText("Section 1")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(false)
                .ValidiateCanEditNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24621")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access (Document Section has 1 record) - " +
            "Open the Document Section Application Access record  - Update the Can Edit Field - Save, Close and Reopen the record - Validate that the Can Edit field is correctly updated")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod007()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID))
                dbHelper.documentSectionApplicationAccess.DeleteDocumentSectionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionApplicationAccessID = dbHelper.documentSectionApplicationAccess.CreateDocumentSectionApplicationAccess(applicationID, sectionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionApplicationAccessID.ToString());

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionApplicationAccessID.ToString());

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ValidiateSectionFieldLinkText("Section 1")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(true)
                .ValidiateCanEditNoOptionChecked(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24622")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access - Tap on the add new record button - " +
            "Wait for the Document Section Application Access new record page to load - Set date in all fields - Tap in the save and close button - Validate that the new record is saved.")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod008()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID))
                dbHelper.documentSectionApplicationAccess.DeleteDocumentSectionApplicationAccess(dsaaID);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ClickOnNewRecordButton();

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ClickApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText(_applicationName);

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad();

            var records = dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.documentSectionApplicationAccess.GetByID(records[0], "documentsectionid", "applicationid", "canedit");
            Assert.AreEqual(sectionID, fields["documentsectionid"]);
            Assert.AreEqual(applicationID, fields["applicationid"]);
            Assert.AreEqual(true, fields["canedit"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24623")]
        [Description("Open a Document Section with Availability set to 'Specific Applications' - Navigate to the Document Section Application Access (Document Section has 1 record) - " +
            "Open the Document Section Application Access record  - Click on the delete button - confirm the delete operation - validate that the record is deleted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionApplicationAccess_UITestMethod009()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID))
                dbHelper.documentSectionApplicationAccess.DeleteDocumentSectionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionApplicationAccessID = dbHelper.documentSectionApplicationAccess.CreateDocumentSectionApplicationAccess(applicationID, sectionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionApplicationAccessLink();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionApplicationAccessID.ToString());

            documentSectionApplicationAccessRecordPage
                .WaitForDocumentSectionApplicationAccessRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            documentSectionApplicationAccessPage
                .WaitForDocumentSectionApplicationAccessPageToLoad();


            var records = dbHelper.documentSectionApplicationAccess.GetByDocumentSectionId(sectionID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8308

        [TestProperty("JiraIssueID", "CDV6-24624")]
        [Description("Open a Document Section record - Validate that the Availability field is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentSectionsByApplications_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ValidateAvailabilityFieldVisibility(true)
                .ValidateAvailabilityFieldSelectedText("All Applications");
        }

        [TestProperty("JiraIssueID", "CDV6-24625")]
        [Description("Open a Document Section record - Update the selected value for the Availability field - Save and close the document record - " +
            "Validate that the Availability field is correctly saved")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentSectionsByApplications_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1

            //set Availability to "All Applications"
            dbHelper.documentSection.UpdateAvailability(sectionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .SelectAvailability("Specific Applications")
                .ClickSaveAndCloseButton();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad();

            var fields = dbHelper.documentSection.GetDocumentSectionByID(sectionID, "availabilityid");
            Assert.AreEqual(3, fields["availabilityid"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8305

        [TestProperty("JiraIssueID", "CDV6-24626")]
        [Description("Open a Document Section Question with Availability set to 'All Applications' - " +
            "Validate that the Application Access module is not available under the Document Section Question menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionQuestionApplicationAccessLinkVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-24627")]
        [Description("Open a Document Section Question with Availability set to 'Internal Use Only' - " +
            "Validate that the Application Access module is not available under the Document Section Question menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 2);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionQuestionApplicationAccessLinkVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24628")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - " +
            "Validate that the Application Access module is available under the Document Section Question menu Related Items")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod003()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ValidateDocumentSectionQuestionApplicationAccessLinkVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24629")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access - " +
            "validate that the Document Section Question Application Access sub page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod004()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-24630")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access (Document Section Question has 1 record) - " +
            "Validate that the Document Section Question Application Access record is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod005()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID))
                dbHelper.documentSectionQuestionApplicationAccess.DeleteDocumentSectionQuestionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionQuestionApplicationAccessID = dbHelper.documentSectionQuestionApplicationAccess.CreateDocumentSectionQuestionApplicationAccess(applicationID, sectionQuestionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ValidateRecordPresent(documentSectionQuestionApplicationAccessID.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24631")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access (Document Section Question has 1 record) - " +
            "Open the Document Section Question Application Access record  - Validate the record fields")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod006()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID))
                dbHelper.documentSectionQuestionApplicationAccess.DeleteDocumentSectionQuestionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionQuestionApplicationAccessID = dbHelper.documentSectionQuestionApplicationAccess.CreateDocumentSectionQuestionApplicationAccess(applicationID, sectionQuestionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionQuestionApplicationAccessID.ToString());

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ValidiateQuestionFieldLinkText("WF Date; Order:1")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(false)
                .ValidiateCanEditNoOptionChecked(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24632")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access (Document Section Question has 1 record) - " +
            "Open the Document Section Question Application Access record  - Update the Can Edit Field - Save, Close and Reopen the record - Validate that the Can Edit field is correctly updated")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod007()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID))
                dbHelper.documentSectionQuestionApplicationAccess.DeleteDocumentSectionQuestionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionQuestionApplicationAccessID = dbHelper.documentSectionQuestionApplicationAccess.CreateDocumentSectionQuestionApplicationAccess(applicationID, sectionQuestionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionQuestionApplicationAccessID.ToString());

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionQuestionApplicationAccessID.ToString());

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ValidiateQuestionFieldLinkText("WF Date; Order:1")
                .ValidiateApplicationFieldLinkText(_applicationName)
                .ValidiateCanEditYesOptionChecked(true)
                .ValidiateCanEditNoOptionChecked(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24633")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access - Tap on the add new record button - " +
            "Wait for the Document Section Question Application Access new record page to load - Set date in all fields - Tap in the save and close button - Validate that the new record is saved.")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod008()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID))
                dbHelper.documentSectionQuestionApplicationAccess.DeleteDocumentSectionQuestionApplicationAccess(dsaaID);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ClickOnNewRecordButton();

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ClickApplicationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText(_applicationName);

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ClickCanEditYesRadioButton()
                .ClickSaveAndCloseButton();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad();

            var records = dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.documentSectionQuestionApplicationAccess.GetByID(records[0], "DocumentSectionQuestionId", "applicationid", "canedit");
            Assert.AreEqual(sectionQuestionID, fields["documentsectionquestionid"]);
            Assert.AreEqual(applicationID, fields["applicationid"]);
            Assert.AreEqual(true, fields["canedit"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24634")]
        [Description("Open a Document Section Question with Availability set to 'Specific Applications' - Navigate to the Document Section Question Application Access (Document Section Question has 1 record) - " +
            "Open the Document Section Question Application Access record  - Click on the delete button - confirm the delete operation - validate that the record is deleted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentSectionQuestionApplicationAccess_UITestMethod009()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var applicationID = new Guid("256e6219-1925-eb11-a2ce-0050569231cf"); //Consumer Portal
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 3);

            //remove all Document Section Application Access records for the document
            foreach (var dsaaID in dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID))
                dbHelper.documentSectionQuestionApplicationAccess.DeleteDocumentSectionQuestionApplicationAccess(dsaaID);

            //create a new Document section Application Access records for the document
            var documentSectionQuestionApplicationAccessID = dbHelper.documentSectionQuestionApplicationAccess.CreateDocumentSectionQuestionApplicationAccess(applicationID, sectionQuestionID, false);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ClickMenuButton()
                .ClickDocumentSectionQuestionApplicationAccessLink();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
                .ClickOnRecord(documentSectionQuestionApplicationAccessID.ToString());

            documentSectionQuestionApplicationAccessRecordPage
                .WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            documentSectionQuestionApplicationAccessPage
                .WaitForDocumentSectionQuestionApplicationAccessPageToLoad();


            var records = dbHelper.documentSectionQuestionApplicationAccess.GetByDocumentSectionQuestionId(sectionQuestionID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8306

        [TestProperty("JiraIssueID", "CDV6-24635")]
        [Description("Open a Document Section Question record - Validate that the Availability field is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentSectionQuestionsByApplications_UITestMethod001()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .ValidateAvailabilityFieldVisibility(true)
                .ValidateAvailabilityFieldSelectedText("All Applications");
        }

        [TestProperty("JiraIssueID", "CDV6-24636")]
        [Description("Open a Document Section Question record - Update the selected value for the Availability field - Save and close the document record - " +
            "Validate that the Availability field is correctly saved")]
        [TestCategory("UITest")]
        [TestMethod]
        public void FacilitatingAccessToDocumentSectionQuestionsByApplications_UITestMethod002()
        {
            var documentID = new Guid("c3934caa-1d62-eb11-a308-005056926fe4"); //Automation - Document Application Access
            var sectionID = new Guid("559c26bf-1d62-eb11-a308-005056926fe4"); //Section 1
            var sectionQuestionID = new Guid("b2ed4cf2-4962-eb11-a308-005056926fe4"); //WF Date

            //set Availability to "All Applications"
            dbHelper.documentSectionQuestion.UpdateAvailability(sectionQuestionID, 1);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automation - Document Application Access", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickSectionsTab();

            documentSectionsSubPage
                .WaitForDocumentSectionsSubPageToLoad()
                .ClickOnRecord(sectionID.ToString());

            documentSectionRecordPage
                .WaitForDocumentSectionRecordPageToLoad()
                .ClickQuestionsTab();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad()
                .ClickOnRecord(sectionQuestionID.ToString());

            documentSectionQuestionRecordPage
                .WaitForDocumentSectionQuestionRecordPageToLoad()
                .SelectAvailability("Specific Applications")
                .ClickSaveAndCloseButton();

            documentSectionQuestionsSubPage
                .WaitForDocumentSectionQuestionsSubPageToLoad();

            var fields = dbHelper.documentSectionQuestion.GetDocumentSectionQuestionByID(sectionQuestionID, "availabilityid");
            Assert.AreEqual(3, fields["availabilityid"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8813

        [TestProperty("JiraIssueID", "CDV6-24637")]
        [Description("Open a Document record - Click on the Print Templates tab - Validate that the print templates sub page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod001()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24638")]
        [Description("Open a Document record (document has linked print templates) - Click on the Print Templates tab - " +
            "Wait for the Print Templates sub page to load - Validate that all linked print template records are displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod002()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2
            var printTemplate1Id = new Guid("90f67cc7-f595-eb11-a323-005056926fe4"); //Caredirector Default Template
            var printTemplate2Id = new Guid("273814d4-f595-eb11-a323-005056926fe4"); //Portal Default Template


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad()

                .ValidateRecordNameCellText(printTemplate1Id.ToString(), "Caredirector Default Template")
                .ValidateRecordLanguageCellText(printTemplate1Id.ToString(), "English (UK)")
                .ValidateRecordCreatedByCellText(printTemplate1Id.ToString(), "José Brazeta")
                .ValidateRecordCreatedOnCellText(printTemplate1Id.ToString(), "05/04/2021 11:00:42")

                .ValidateRecordNameCellText(printTemplate2Id.ToString(), "Portal Default Template")
                .ValidateRecordLanguageCellText(printTemplate2Id.ToString(), "English (UK)")
                .ValidateRecordCreatedByCellText(printTemplate2Id.ToString(), "José Brazeta")
                .ValidateRecordCreatedOnCellText(printTemplate2Id.ToString(), "05/04/2021 11:01:06");
        }

        [TestProperty("JiraIssueID", "CDV6-24639")]
        [Description("Open a Document record (document has linked print templates) - Click on the Print Templates tab - " +
            "Wait for the Print Templates sub page to load - Open a print template record - Validate that the print template record page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod003()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2
            var printTemplate1Id = new Guid("90f67cc7-f595-eb11-a323-005056926fe4"); //Caredirector Default Template
            var printTemplate2Id = new Guid("273814d4-f595-eb11-a323-005056926fe4"); //Portal Default Template

            var careDirectorApplicationId = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            var careDirectorAppApplicationId = new Guid("bd0afb56-3065-e611-80cf-0050560502cc"); //CareDirector App



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad()
                .ClickOnRecord(printTemplate1Id.ToString());

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ValidateDocumentFieldLinkText("Automated UI Test Document 2")
                .ValidateNameFieldValue("Caredirector Default Template")
                .ValidateFileLinkFieldText("Color Checker.docx (12.6 KB)")
                .ValidateValidForPrintHistoryOnCloseYesRadioButtonChecked(false)
                .ValidateValidForPrintHistoryOnCloseNoRadioButtonChecked(true)
                .ValidateLanguageLinkFieldText("English (UK)")
                .ValidateProtectDocumentYesRadioButtonChecked(false)
                .ValidateProtectDocumentNoRadioButtonChecked(true)
                .ValidateAllowBlankOutputYesRadioButtonChecked(false)
                .ValidateAllowBlankOutputNoRadioButtonChecked(true)
                .ValidateDefaultForApplicationsAddedRecordText(careDirectorApplicationId.ToString(), "CareDirector\r\nRemove")
                .ValidateDefaultForApplicationsAddedRecordText(careDirectorAppApplicationId.ToString(), "CareDirector App\r\nRemove");
        }

        [TestProperty("JiraIssueID", "CDV6-24640")]
        [Description("Open a Document record (document has linked print templates) - Click on the Print Templates tab - " +
            "Wait for the Print Templates sub page to load - Click on the add new record button - Wait for the new record page to load - " +
            "Set data in all fields - Click on the save and close button - Re-Open the saved record - Validate that all data was correctly saved")]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod004()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2

            var CareDirectorDataImportServiceApplicationId = new Guid("e4a92783-f4bb-e811-9c00-1866da1e4209"); //CareDirector DataImport Service
            var CareDirectorEmailServiceApplicationId = new Guid("9a6c8a33-31ef-e811-80dc-0050560502cc"); //CareDirector Email Service

            foreach (var printTemplateId in dbHelper.documentPrintTemplate.GetByDocumentIDAndName(documentID, "Automated UI Test Document 2 - New Print Template"))
            {
                foreach (var linkedappId in dbHelper.documentPrintTemplateLinkedApplication.GetBDocumentPrintTemplateId(printTemplateId))
                    dbHelper.documentPrintTemplateLinkedApplication.DeleteDocumentPrintTemplateLinkedApplication(linkedappId);

                dbHelper.documentPrintTemplate.DeleteDocumentPrintTemplate(printTemplateId);
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
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad()
                .ClickOnNewRecordButton();

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .InsertName("Automated UI Test Document 2 - New Print Template")
                .SelectFile(TestContext.DeploymentDirectory + "\\Document.docx")
                .ClickDefaultForApplicationsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(CareDirectorDataImportServiceApplicationId.ToString())
                .AddElementToList(CareDirectorEmailServiceApplicationId.ToString())
                .TapOKButton();

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ClickSaveAndCloseButton();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad();

            var records = dbHelper.documentPrintTemplate.GetByDocumentIDAndName(documentID, "Automated UI Test Document 2 - New Print Template");
            Assert.AreEqual(1, records.Count);

            documentPrintTemplatesPage
                .ClickOnRecord(records[0].ToString());

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ValidateDocumentFieldLinkText("Automated UI Test Document 2")
                .ValidateNameFieldValue("Automated UI Test Document 2 - New Print Template")
                .ValidateFileLinkFieldText("Document.docx (11.58 KB)")
                .ValidateValidForPrintHistoryOnCloseYesRadioButtonChecked(false)
                .ValidateValidForPrintHistoryOnCloseNoRadioButtonChecked(true)
                .ValidateLanguageLinkFieldText("English (UK)")
                .ValidateProtectDocumentYesRadioButtonChecked(false)
                .ValidateProtectDocumentNoRadioButtonChecked(true)
                .ValidateAllowBlankOutputYesRadioButtonChecked(false)
                .ValidateAllowBlankOutputNoRadioButtonChecked(true)
                .ValidateDefaultForApplicationsAddedRecordText(CareDirectorDataImportServiceApplicationId.ToString(), "CareDirector DataImport Service\r\nRemove")
                .ValidateDefaultForApplicationsAddedRecordText(CareDirectorEmailServiceApplicationId.ToString(), "CareDirector Email Service\r\nRemove");
        }

        [TestProperty("JiraIssueID", "CDV6-24641")]
        [Description("Open a Document record (document has linked print templates) - Click on the Print Templates tab - " +
            "Wait for the Print Templates sub page to load - Click on the add new record button - Wait for the new record page to load - " +
            "Set data in all fields - Click on the save and close button - Select the newly created - Click on the delete button - confirm the delete - " +
            "Validate that the record is removed")]
        [TestCategory("UITest")]
        [DeploymentItem("Files\\Document.docx"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod005()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2

            var CareDirectorDataImportServiceApplicationId = new Guid("e4a92783-f4bb-e811-9c00-1866da1e4209"); //CareDirector DataImport Service
            var CareDirectorEmailServiceApplicationId = new Guid("9a6c8a33-31ef-e811-80dc-0050560502cc"); //CareDirector Email Service

            foreach (var printTemplateId in dbHelper.documentPrintTemplate.GetByDocumentIDAndName(documentID, "Automated UI Test Document 2 - New Print Template"))
            {
                foreach (var linkedappId in dbHelper.documentPrintTemplateLinkedApplication.GetBDocumentPrintTemplateId(printTemplateId))
                    dbHelper.documentPrintTemplateLinkedApplication.DeleteDocumentPrintTemplateLinkedApplication(linkedappId);

                dbHelper.documentPrintTemplate.DeleteDocumentPrintTemplate(printTemplateId);
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
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad()
                .ClickOnNewRecordButton();

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .InsertName("Automated UI Test Document 2 - New Print Template")
                .SelectFile(TestContext.DeploymentDirectory + "\\Document.docx")
                .ClickDefaultForApplicationsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .AddElementToList(CareDirectorDataImportServiceApplicationId.ToString())
                .AddElementToList(CareDirectorEmailServiceApplicationId.ToString())
                .TapOKButton();

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ClickSaveAndCloseButton();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad();

            var records = dbHelper.documentPrintTemplate.GetByDocumentIDAndName(documentID, "Automated UI Test Document 2 - New Print Template");
            Assert.AreEqual(1, records.Count);

            documentPrintTemplatesPage
                .ClickOnRecord(records[0].ToString());

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad();

            records = dbHelper.documentPrintTemplate.GetByDocumentIDAndName(documentID, "Automated UI Test Document 2 - New Print Template");
            Assert.AreEqual(0, records.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-24642")]
        [Description("Open a Document record (document has linked print templates) - Click on the Print Templates tab - " +
            "Wait for the Print Templates sub page to load - Open a print template record - Click on the Print Preview button - " +
            "Validate that the linked word document is downloaded")]
        [TestCategory("UITest")]
        [TestMethod]
        public void DocumentPrintTemplates_UITestMethod006()
        {
            var documentID = new Guid("0380796e-8500-ea11-a2c7-005056926fe4"); //Automated UI Test Document 2
            var printTemplate1Id = new Guid("90f67cc7-f595-eb11-a323-005056926fe4"); //Caredirector Default Template
            var printTemplate2Id = new Guid("273814d4-f595-eb11-a323-005056926fe4"); //Portal Default Template

            var careDirectorApplicationId = new Guid("393e0925-f418-e511-80cf-00505605009e"); //CareDirector
            var careDirectorAppApplicationId = new Guid("bd0afb56-3065-e611-80cf-0050560502cc"); //CareDirector App



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDocumentsSection();

            documentsPage
                .WaitForDocumentsPageToLoad()
                .SearchDocumentRecord("Automated UI Test Document 2", documentID.ToString())
                .OpenDocumentRecord(documentID.ToString());

            documentPage
                .WaitForDocumentPageToLoad()
                .ClickPrintTemplatesTab();

            documentPrintTemplatesPage
                .WaitForDocumentPrintTemplatesPageToLoad()
                .ClickOnRecord(printTemplate1Id.ToString());

            documentPrintTemplateRecordPage
                .WaitForDocumentPrintTemplateRecordPageToLoad()
                .ClickPreviewButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Automated UI Test Document 2.docx");
            Assert.IsTrue(fileExists);
        }

        #endregion


        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}
