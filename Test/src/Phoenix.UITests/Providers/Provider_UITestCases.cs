using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    ///  
    /// </summary>
    [TestClass]
    public class Provider_UITestCases : FunctionalTest
    {

        #region Website Messages Attachment https://advancedcsg.atlassian.net/browse/CDV6-10462

        [TestProperty("JiraIssueID", "CDV6-24979")]
        [Description("Testing server side rules for provider forms - Scenario 1 - Provider Main Phone and Other Phone should match - " +
            "Email should be set to 'mail1@somemail.com' - Start date should be greater than '01/06/2021' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod001()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 4; //Not Initialized


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");


            //update main phone
            dbHelper.provider.UpdateProviderMainPhone(providerID, "987654321");

            //update other phone
            dbHelper.provider.UpdateProviderOtherPhone(providerID, "987654321");

            //update email
            dbHelper.provider.UpdateProviderEmail(providerID, "mail1@somemail.com");

            //update start date
            dbHelper.provider.UpdateStartDate(providerID, new DateTime(2021, 6, 2));


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 1").TapOKButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24980")]
        [Description("Testing server side rules for provider forms - Scenario 1 - Provider Main Phone and Other Phone should match - " +
            "Email should be set to 'mail1@somemail.com' - Start date should be smaller than '01/06/2021' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod002()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 4; //Not Initialized


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");


            //update main phone
            dbHelper.provider.UpdateProviderMainPhone(providerID, "987654321");

            //update other phone
            dbHelper.provider.UpdateProviderOtherPhone(providerID, "987654321");

            //update email
            dbHelper.provider.UpdateProviderEmail(providerID, "mail1@somemail.com");

            //update start date
            dbHelper.provider.UpdateStartDate(providerID, new DateTime(2021, 6, 1));


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24981")]
        [Description("Testing server side rules for provider forms - Scenario 1 - Provider Main Phone and Other Phone should match - " +
            "Email should be set to 'someothermail@somemail.com' - Start date should be greater than '01/06/2021' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod003()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 4; //Not Initialized


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");


            //update main phone
            dbHelper.provider.UpdateProviderMainPhone(providerID, "987654321");

            //update other phone
            dbHelper.provider.UpdateProviderOtherPhone(providerID, "987654321");

            //update email
            dbHelper.provider.UpdateProviderEmail(providerID, "someothermail@somemail.com");

            //update start date
            dbHelper.provider.UpdateStartDate(providerID, new DateTime(2021, 6, 2));


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24982")]
        [Description("Testing server side rules for provider forms - Scenario 1 - Provider Main Phone and Other Phone should NOT match - " +
            "Email should be set to 'mail1@somemail.com' - Start date should be greater than '01/06/2021' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod004()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 4; //Not Initialized


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 1");


            //update main phone
            dbHelper.provider.UpdateProviderMainPhone(providerID, "987654321");

            //update other phone
            dbHelper.provider.UpdateProviderOtherPhone(providerID, "987654322");

            //update email
            dbHelper.provider.UpdateProviderEmail(providerID, "mail1@somemail.com");

            //update start date
            dbHelper.provider.UpdateStartDate(providerID, new DateTime(2021, 6, 2));


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24983")]
        [Description("Testing server side rules for provider forms - Scenario 2 - Provider Form status is set to 'In Progress' - " +
            "Rule should be triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod005()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 1; //In Progress


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10462 - Scenario 2").TapOKButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24984")]
        [Description("Testing server side rules for provider forms - Scenario 2 - Provider Form status is set to 'Not Initialized' - " +
            "Rule should NOT BE triggered when the assessment is loaded")]
        [TestMethod, TestCategory("UITest")]
        public void ProviderForm_EnableServerSideRules_UITestMethod006()
        {
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var providerID = new Guid("12e38328-f7cd-eb11-a325-005056926fe4"); //Service Provider Automation 1
            var providerNumber = "2869";
            var documentid = new Guid("c9b8b496-f7cd-eb11-a325-005056926fe4"); //Automation - Provider Form 1
            var startDate = new DateTime(2021, 5, 1);
            var assessmentStatus = 4; //Not Initialized


            //remove all Provider Forms for the case record
            foreach (var providerformid in dbHelper.providerForm.GetProviderFormByProviderID(providerID))
                dbHelper.providerForm.DeleteProviderForm(providerformid);

            //Create a new provider form
            var providerFormID = dbHelper.providerForm.CreateProviderFormRecord(OwnerID, documentid, providerID, startDate, assessmentStatus);

            //get the Document Question Identifier for 'WF Short Answer'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1307")[0];

            //set the answer for the question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(providerFormID, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateShortAnswer(documentAnswerID, "Testing CDV6-10462 - Scenario 2");


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage.
                WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerNumber, providerID.ToString())
                .OpenProviderRecord(providerID.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToProviderFormsPage();

            providerFormsPage
                .WaitForProviderFormsPageToLoad()
                .OpenProviderFormRecord(providerFormID.ToString());

            providerFormRecordPage
                .WaitForProviderFormRecordPageToLoad()
                .ClickEditAssessmentButton();

            automationProviderForm1EditAssessmentPage
                .WaitForEditAssessmentPageToLoad(providerFormID.ToString());
        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod, TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
