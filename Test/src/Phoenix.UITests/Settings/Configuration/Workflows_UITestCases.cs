using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.FormsManagement
{
    [TestClass]
    public class Workflows_UITestCases : FunctionalTest
    {

        [TestInitialize]
        public void TestInitializationMethod()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                //commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-9096

        [TestProperty("JiraIssueID", "CDV6-24789")]
        [Description("Open a Published child workflow record - Click on the unpublish button - " +
            "Confirm the upublish operation - Validate that the record gets unpublished")]
        [TestCategory("UITest")]
        [TestMethod]
        public void UnpublishChildWF_UITestMethod001()
        {
            var workflowID = new Guid("419c0f4c-1196-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish

            dbHelper.workflow.UpdatePublishedField(workflowID, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9096 - Child WF - UI Test to unpublish", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()
                .TapUnpublishButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to Unpublish this Workflow?").TapOKButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad();

            var fields = dbHelper.workflow.GetWorkflowByID(workflowID, "published");
            Assert.AreEqual(false, fields["published"]);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9097

        [TestProperty("JiraIssueID", "CDV6-24790")]
        [Description("Open an unpublished Workflow record with 1 condition with 1 inner action - Validate that is possible to add a new Condition before the existing one")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod001()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"0\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddConditionLink_Level1("1", "1");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .SelectFilter("0", "Subject")
                .SelectOperator("0", "Equals")
                .SelectTargetType("0", "Value")
                .InsertTargetInput("0", "Random Text")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Visibility("1", "2", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "4", true)
                .ValidateExistingConditionLink_Level1Text("1", "2", "Subject Equals [Random Text]")
                .ValidateExistingConditionLink_Level1Text("1", "4", "Subject Equals [Some text]")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24791")]
        [Description("Open an unpublished Workflow record with 1 condition with 1 inner action - Validate that is possible to add a new Action before the existing one")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod002()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"0\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddActionLink_Level1("1", "1");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .SelectAction("Get Initiating User")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingActionLink_Level1Visibility("1", "2", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "4", true)
                .ValidateExistingActionLink_Level1Text("1", "2", "Business Data: Get Initiating User")
                .ValidateExistingConditionLink_Level1Text("1", "4", "Subject Equals [Some text]")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24792")]
        [Description("Open an unpublished Workflow record with 1 condition with 1 inner action - Validate that is possible to add a new Condition after the existing one")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod003()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"0\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddConditionLink_Level1("1", "3");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .SelectFilter("0", "Subject")
                .SelectOperator("0", "Equals")
                .SelectTargetType("0", "Value")
                .InsertTargetInput("0", "Random Text")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Visibility("1", "2", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "4", true)
                .ValidateExistingConditionLink_Level1Text("1", "2", "Subject Equals [Some text]")
                .ValidateExistingConditionLink_Level1Text("1", "4", "Subject Equals [Random Text]")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24793")]
        [Description("Open an unpublished Workflow record with 1 condition with 1 inner action - Validate that is possible to add a new Action after the existing one")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod004()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"0\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddActionLink_Level1("1", "3");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .SelectAction("Get Initiating User")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Visibility("1", "2", true)
                .ValidateExistingActionLink_Level1Visibility("1", "4", true)
                .ValidateExistingConditionLink_Level1Text("1", "2", "Subject Equals [Some text]")
                .ValidateExistingActionLink_Level1Text("1", "4", "Business Data: Get Initiating User")
                ;

        }

        [TestProperty("JiraIssueID", "CDV6-24794")]
        [Description("Open an unpublished Workflow record with 2 conditions with 1 inner action each - Validate that is possible to add a new Condition between the existing ones")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod005()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"fdafe314-9726-4f3b-928b-0e8c43fb7dea\" ProcessOrder=\"0\">          <GroupCondition Id=\"fd43175a-e8b4-4709-be42-97a6c07931b2\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Random Text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"261c6ff3-1430-4f69-b3bb-decf245c446a\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Random Text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"931f64c5-f9fd-452f-8a81-009c71f6bb5a\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"1\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddConditionLink_Level1("1", "3");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .SelectFilter("0", "Subject")
                .SelectOperator("0", "Equals")
                .SelectTargetType("0", "Value")
                .InsertTargetInput("0", "Middle Text")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Visibility("1", "2", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "4", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "6", true)
                .ValidateExistingConditionLink_Level1Text("1", "2", "Subject Equals [Random Text]")
                .ValidateExistingConditionLink_Level1Text("1", "4", "Subject Equals [Middle Text]")
                .ValidateExistingConditionLink_Level1Text("1", "6", "Subject Equals [Some text]");

        }

        [TestProperty("JiraIssueID", "CDV6-24795")]
        [Description("Open an unpublished Workflow record with 2 conditions with 1 inner action each - Validate that is possible to add a new Action between the existing ones")]
        [TestCategory("UITest")]
        [TestMethod]
        public void AllowActionsBeforeConditions_UITestMethod006()
        {
            var workflowID = new Guid("3c60929e-9697-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-9097 (UI Testing)

            dbHelper.workflow.UpdatePublishedField(workflowID, false);

            var wf_xml = "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"fdafe314-9726-4f3b-928b-0e8c43fb7dea\" ProcessOrder=\"0\">          <GroupCondition Id=\"fd43175a-e8b4-4709-be42-97a6c07931b2\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Random Text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"261c6ff3-1430-4f69-b3bb-decf245c446a\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Random Text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"931f64c5-f9fd-452f-8a81-009c71f6bb5a\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"51115798-e1f6-487b-971b-eb51d299edb5\" ProcessOrder=\"1\">          <GroupCondition Id=\"b9b09808-a28a-4f1f-8680-f76979ba62be\" Name=\"Subject &lt;i&gt;Equals&lt;/i&gt; [Some text]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"e553ca42-dd9c-4ba4-b7ff-216bda04ff56\" ConditionOperatorType=\"1\" RelationshipParental=\"false\" LeftElement=\"subject\" RightValue=\"Some text\" />            </Conditions>            <ThenActions>              <WorkflowAction Id=\"10d11b8f-4a1e-418f-acb1-1b9e6bb08c61\" Name=\"Business Data: Get Initiating User\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />            </ThenActions>          </GroupCondition>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>";

            dbHelper.workflow.UpdateWorkflowXmlField(workflowID, wf_xml);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-9097 (UI Testing)", workflowID.ToString())
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickAddActionLink_Level1("1", "3");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .SelectAction("Get Initiating User")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Visibility("1", "2", true)
                .ValidateExistingActionLink_Level1Visibility("1", "4", true)
                .ValidateExistingConditionLink_Level1Visibility("1", "6", true)
                .ValidateExistingConditionLink_Level1Text("1", "2", "Subject Equals [Random Text]")
                .ValidateExistingActionLink_Level1Text("1", "4", "Business Data: Get Initiating User")
                .ValidateExistingConditionLink_Level1Text("1", "6", "Subject Equals [Some text]");

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9078

        [TestProperty("JiraIssueID", "CDV6-24796")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Validate that the 'Date Fields' field is visible - " +
            "Validate that the 'Execute before (in days)' field is not visible - " +
            "Validate that the 'Execute after (in days)' field is not visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod001()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24797")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - " +
            "Validate that the 'Execute before (in days)' field is visible - " +
            "Validate that the 'Execute after (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod002()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24798")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - Insert 'Execute before (in days)' value  - " +
            "Validate that the 'Execute after (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod003()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertExecuteBefore("1")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24799")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - Insert 'Execute after (in days)' value  - " +
            "Validate that the 'Execute before (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod004()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertExecuteAfter("1")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24800")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - Insert 'Execute before (in days)' value  - " +
            "Validate that the 'Execute after (in days)' field is NOT visible - Remove the 'Execute before (in days)' value - " +
            "Validate that the 'Execute after (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod005()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertExecuteBefore("1")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(false)

                .InsertExecuteBefore("")
                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24801")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - Insert 'Execute after (in days)' value  - " +
            "Validate that the 'Execute before (in days)' field is NOT visible - Remove the 'Execute after (in days)' value - " +
            "Validate that the 'Execute before (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod006()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()

                .InsertExecuteAfter("1")
                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(true)

                .InsertExecuteAfter("")
                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24802")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - " +
            "Set 'Type' to Wait - Set 'Date Fields' to PhoneCallDate - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is visible - " +
            "Remove Set 'Date Fields' value - " +
            "Validate that the 'Execute before (in days)' and 'Execute after (in days)' fields are not visible ")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod007()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-9078")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .ClickDateFieldsLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("PhoneCallDate").TapSearchButton().SelectResultElement(businessObjectFieldID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true)

                .ClickDateFieldsRemoveButton()
                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(false)
                ;
        }




        [TestProperty("JiraIssueID", "CDV6-24803")]
        [Description("Open existing Workflow record ('Record Type' = PhoneCall ; Type = 'Wait' ; empty 'Date Fields') - Navigate to the Details page - " +
            "Validate that the 'Execute before (in days)' field is NOT visible" +
            "Validate that the 'Execute after (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod008()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            dbHelper.workflow.UpdateWaitFields(workflowID, null, null, null);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24804")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; empty 'Execute before' and 'Execute after' fields) - " +
            "Navigate to the Details page - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod009()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, null, null, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24805")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; 'Execute before' = 2  ; empty 'Execute after' fields) - " +
            "Navigate to the Details page - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod010()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, 2, null, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24806")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; empty 'Execute before'  ; 'Execute after' = 2) - " +
            "Navigate to the Details page - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod011()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, null, 2, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24807")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; 'Execute before' = 2  ; empty 'Execute after' fields) - " +
            "Navigate to the Details page - Remove the 'Execute before (in days)' value - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod012()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, 2, null, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .InsertExecuteBefore("")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24808")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; empty 'Execute before'  ; 'Execute after' = 2) - " +
            "Navigate to the Details page - Remove the 'Execute after (in days)' value - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod013()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, null, 2, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .InsertExecuteAfter("")

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(true)
                .ValidateExecuteAfterFieldVisibility(true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24809")]
        [Description("Open existing Workflow record ('Record Type' = 'PhoneCall' ; 'Type' = 'Wait' ; 'Date Fields' = 'PhoneCallDate' ; empty 'Execute before'  ; 'Execute after' = 2) - " +
            "Navigate to the Details page - remove the value from the 'Date Fields' - " +
            "Validate that the 'Execute before (in days)' field is visible" +
            "Validate that the 'Execute after (in days)' field is NOT visible")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod014()
        {
            var workflowID = new Guid("498b2800-d1a8-eb11-a323-005056926fe4"); //WF CDV6-9078
            var businessObjectFieldID = new Guid("936caba9-f4a5-e811-80dc-0050560502cc"); //PhoneCallDate

            dbHelper.workflow.UpdateWaitFields(workflowID, null, 2, businessObjectFieldID);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()

                .ClickDetailsTab()
                .WaitForDetailsTablToLoad()

                .ClickDateFieldsRemoveButton()

                .ValidateDateFieldsVisibility(true)
                .ValidateExecuteBeforeFieldVisibility(false)
                .ValidateExecuteAfterFieldVisibility(false)
                ;
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10341

        [TestProperty("JiraIssueID", "CDV6-24810")]
        [Description("Open a workflow job with a trigger date set to the future - Click on the execute button - Validate that the workflow job is not executed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod015()
        {
            var workflowID = new Guid("69691345-fba8-eb11-a323-005056926fe4"); //WF CDV6-9078 (publised - Execute After)

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string callerIdTableName = "person";
            string callerIDName = "Adolfo Abbott";

            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string regardingName = "Adolfo Abbott";

            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

            DateTime phonecallDate = DateTime.Now.Date;

            //delete all phone call records for the person
            foreach (var phonecall in dbHelper.phoneCall.GetPhoneCallByRegardingID(regardingID))
                dbHelper.phoneCall.DeletePhoneCall(phonecall);


            //delete all workflow job records for the workflow
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);

            //Create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);

            //we should have 1 workflow job created at this point
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078 (publised - Execute After)")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()
                .NavigateToWorkflowJobsPage();

            workflowJobsPage
                .WaitForWorkflowJobsPageToLoad()
                .OpenWorkflowJobRecord(workflowJobIds[0].ToString());

            workflowJobRecordPage
                .WaitForWorkflowJobRecordPageToLoad()
                .ClickExecuteButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("A workflow of type 'Wait' cannot be manually executed").TapCloseButton();

            workflowJobRecordPage
                .WaitForWorkflowJobRecordPageToLoad()
                .ClickBackButton();

            workflowJobsPage
                .WaitForWorkflowJobsPageToLoad();

            //We executed a workflow job with a trigger date set to the future. the workflow should not have been executed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24811")]
        [Description("Open a workflow job with a trigger date set to the past - Click on the execute button - Validate that the workflow job is executed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflow_UITestMethod016()
        {
            var workflowID = new Guid("69691345-fba8-eb11-a323-005056926fe4"); //WF CDV6-9078 (publised - Execute After)

            //ARRANGE 
            string subject = "WF Testing - CDV6-9078 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string callerIdTableName = "person";
            string callerIDName = "Adolfo Abbott";

            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
            string regardingName = "Adolfo Abbott";

            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

            DateTime phonecallDate = DateTime.Now.Date.AddDays(-5);

            //delete all phone call records for the person
            foreach (var phonecall in dbHelper.phoneCall.GetPhoneCallByRegardingID(regardingID))
                dbHelper.phoneCall.DeletePhoneCall(phonecall);


            //delete all workflow job records for the workflow
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);

            //Create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);

            //we should have 1 workflow job created at this point
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);



            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF CDV6-9078 (publised - Execute After)")
                .OpenWorkflowRecord(workflowID.ToString());

            workflowRecordPage
                .WaitForWorkflowRecordPageToLoad()
                .NavigateToWorkflowJobsPage();

            workflowJobsPage
                .WaitForWorkflowJobsPageToLoad()
                .OpenWorkflowJobRecord(workflowJobIds[0].ToString());

            workflowJobRecordPage
                .WaitForWorkflowJobRecordPageToLoad()
                .ClickExecuteButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("A workflow of type 'Wait' cannot be manually executed").TapCloseButton();

            workflowJobRecordPage
                .WaitForWorkflowJobRecordPageToLoad()
                .ClickBackButton();

            workflowJobsPage
                .WaitForWorkflowJobsPageToLoad();


            workflowJobsPage
                .WaitForWorkflowJobsPageToLoad();

            //We executed a workflow job with a trigger date set to the future. the workflow should not have been executed
            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID, "subject");
            Assert.AreEqual("WF Testing - CDV6-9078 - Execute After - Scenario 1", fields["subject"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8948 && https://advancedcsg.atlassian.net/browse/CDV6-10775

        [TestProperty("JiraIssueID", "CDV6-24812")]
        [Description("Open a Workflow with invalid references for lookups and picklists - click on the validate button - Validate that all invalid references are highlighted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod001()
        {
            var workflowid = new Guid("7d831c70-bca9-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 (failing references)

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 (failing references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickValidateWorkflowButton()

                .ValidationErrorMessage_Level1_Visibility(2, true)
                .ValidateValidationErrorMessage_Level1Text(2, "Missing Reference Data: AdviceINVALID, AssessmentINVALID")

                .ValidationErrorMessage_Level2_Visibility(2, true)
                .ValidateValidationErrorMessage_Level2Text(2, "Missing Reference Data: Core AssessmentINVALID, Crisis support callINVALID")

                .ValidationErrorMessage_Level1_Visibility(4, true)
                .ValidateValidationErrorMessage_Level1Text(4, "Missing Reference Data: Record five, ASGLB 9224 - (09/04/2004) [CAS-000005-1446] INVALID, ASGLB 9255, Scalp - (19/03/2005) [CAS-000005-1445] INVALID")

                .ValidationErrorMessage_Level1_Visibility(6, true)
                .ValidateValidationErrorMessage_Level1Text(6, "Missing Reference Data: Jeffery FowlerINVALID")

                .ValidationErrorMessage_Level2_Visibility(6, true)
                .ValidateValidationErrorMessage_Level2Text(6, "Missing Reference Data: Alyson AbbottINVALIID")

                .ValidationErrorMessage_Level1_Visibility(8, false);
        }

        [TestProperty("JiraIssueID", "CDV6-24813")]
        [Description("Open a Workflow with invalid references for lookups and picklists - click on the publish button - Validate that an alert message is displayed warning the user about missing reference errors")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod002()
        {
            var workflowid = new Guid("7d831c70-bca9-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 (failing references)

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 (failing references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .TapPublishButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("There are missing reference errors. Do you still want to publish this Workflow?").TapCancelButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24814")]
        [Description("Open a Workflow with valid references for lookups and picklists - click on the validate button - Validate that no error is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod003()
        {
            var workflowid = new Guid("685452ee-bfa9-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 (correct references)

            //unpublish the workflow
            dbHelper.workflow.UpdatePublishedField(workflowid, false);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 (correct references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickValidateWorkflowButton()

                .ValidationErrorMessage_Level1_Visibility(2, false)

                .ValidationErrorMessage_Level2_Visibility(2, false)

                .ValidationErrorMessage_Level1_Visibility(4, false)

                .ValidationErrorMessage_Level1_Visibility(6, false)

                .ValidationErrorMessage_Level2_Visibility(6, false)

                .ValidationErrorMessage_Level1_Visibility(8, false);

        }

        [TestProperty("JiraIssueID", "CDV6-24815")]
        [Description("Open a Workflow with valid references for lookups and picklists - click on the publish button - Validate that no alert is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod004()
        {
            var workflowid = new Guid("685452ee-bfa9-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 (correct references)

            //unpublish the workflow
            dbHelper.workflow.UpdatePublishedField(workflowid, false);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 (correct references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .TapPublishButton()
                .WaitForPublishedWorkflowRecordPageToLoad();


        }

        [TestProperty("JiraIssueID", "CDV6-24816")]
        [Description("Open a Workflow with invalid references inside workflow actions - click on the validate button - Validate that all invalid references for Actions are highlighted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod005()
        {
            var workflowid = new Guid("7d831c70-bca9-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 (failing references)

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 (failing references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickValidateWorkflowButton();
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(10, true)
                .ValidateValidationErrorMessage_Level1Text(10, "Missing Reference Data: Berta Klein (Invalid)CareDirector QA (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(12, true)
                .ValidateValidationErrorMessage_Level1Text(12, "Missing Reference Data: Shane Lowe (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(14, true)
                .ValidateValidationErrorMessage_Level1Text(14, "Missing Reference Data: Acute & PICU (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(16, true)
                .ValidateValidationErrorMessage_Level1Text(16, "Missing Reference Data: April Bauer (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(18, true)
                .ValidateValidationErrorMessage_Level1Text(18, "Missing Reference Data: Custom workflow reference does not exist.");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(20, false);
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(22, true)
                .ValidateValidationErrorMessage_Level1Text(22, "Missing Reference Data: Assessment (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(24, true)
                .ValidateValidationErrorMessage_Level1Text(24, "Missing Reference Data: Rufus English (invalid)");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-24817")]
        [Description("Open a Pre-Sync Workflow with invalid references inside workflow actions - click on the validate button - Validate that all invalid references for Actions are highlighted")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WorkflowValidation_UITestMethod006()
        {
            var workflowid = new Guid("ffb38715-ccc2-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8948 - Pre-Sync (failing references)

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8948 - Pre-Sync (failing references)")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickValidateWorkflowButton();
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(2, true)
                .ValidateValidationErrorMessage_Level1Text(2, "Missing Reference Data: Mobile Data Restriction - Allow User (1 INVALID)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(4, true)
                .ValidateValidationErrorMessage_Level1Text(4, "Missing Reference Data: Adolfo Abbott (Invalid)Abby Cotterell (Invalid)Assessment (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(6, true)
                .ValidateValidationErrorMessage_Level1Text(6, "Missing Reference Data: Care Plan User_02 (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(8, true)
                .ValidateValidationErrorMessage_Level1Text(8, "Missing Reference Data: CareDirector QA (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(10, true)
                .ValidateValidationErrorMessage_Level1Text(10, "Missing Reference Data: Provider Portal (Invalid)CW Admin Access (Invalid)");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(12, true)
                .ValidateValidationErrorMessage_Level1Text(12, "Missing Reference Data: Custom workflow reference does not exist.");
            workflowRecordPage
                .ValidationErrorMessage_Level1_Visibility(14, true)
                .ValidateValidationErrorMessage_Level1Text(14, "Missing Reference Data: Vicki Horn (Invalid)");
            ;
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8710

        [TestProperty("JiraIssueID", "CDV6-24818")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Actions with custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod001()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()

                .ValidateExistingActionLink_Level1Visibility("1", "2", true)
                .ValidateExistingActionLink_Level1Text("1", "2", "Send Initial Email")

                .ValidateExistingActionLink_Level1Visibility("1", "4", true)
                .ValidateExistingActionLink_Level1Text("1", "4", "Send Final Email")

                .ValidateExistingActionLink_Level1Visibility("1", "6", true)
                .ValidateExistingActionLink_Level1Text("1", "6", "Get Current Date 1")

                .ValidateExistingActionLink_Level1Visibility("1", "8", true)
                .ValidateExistingActionLink_Level1Text("1", "8", "Create Phone Call Record 1")

                .ValidateExistingActionLink_Level1Visibility("1", "12", true)
                .ValidateExistingActionLink_Level1Text("1", "12", "Update Phone Call Record 1")

                .ValidateExistingActionLink_Level1Visibility("1", "14", true)
                .ValidateExistingActionLink_Level1Text("1", "14", "Change created phone call record status")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24819")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Actions without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod002()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()

                .ValidateExistingActionLink_Level1Visibility("1", "10", true)
                .ValidateExistingActionLink_Level1Text("1", "10", "Date Helpers: Get Current Date")

                .ValidateExistingActionLink_Level1Visibility("1", "16", true)
                .ValidateExistingActionLink_Level1Text("1", "16", "Business Data: Get Initiating User");
        }

        [TestProperty("JiraIssueID", "CDV6-24820")]
        [Description("Open a published workflow record - " +
            "Validate that Actions with custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod003()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()

                .ValidateExistingActionLink_Level1Visibility("1", "1", true)
                .ValidateExistingActionLink_Level1Text("1", "1", "Send Initial Email")

                .ValidateExistingActionLink_Level1Visibility("1", "2", true)
                .ValidateExistingActionLink_Level1Text("1", "2", "Send Final Email")

                .ValidateExistingActionLink_Level1Visibility("1", "3", true)
                .ValidateExistingActionLink_Level1Text("1", "3", "Get Current Date 1")

                .ValidateExistingActionLink_Level1Visibility("1", "4", true)
                .ValidateExistingActionLink_Level1Text("1", "4", "Create Phone Call Record 1")

                .ValidateExistingActionLink_Level1Visibility("1", "6", true)
                .ValidateExistingActionLink_Level1Text("1", "6", "Update Phone Call Record 1")

                .ValidateExistingActionLink_Level1Visibility("1", "7", true)
                .ValidateExistingActionLink_Level1Text("1", "7", "Change created phone call record status")

                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24821")]
        [Description("Open a published workflow record - " +
            "Validate that Actions without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod004()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()

                .ValidateExistingActionLink_Level1Visibility("1", "5", true)
                .ValidateExistingActionLink_Level1Text("1", "5", "Date Helpers: Get Current Date")

                .ValidateExistingActionLink_Level1Visibility("1", "8", true)
                .ValidateExistingActionLink_Level1Text("1", "8", "Business Data: Get Initiating User")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24822")]
        [Description("Open an unpublished workflow record - Click on a action with a custom title - Wait for the workflow action popup to load - " +
            "Validate that the Custom Title elements are displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod005()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "4");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ValidateActionSelectedValue("Send Email")
                .ValidateCustomTitleCheckboxChecked(true)
                .ValidateCustomTitleTextboxDisabled(false)
                .ValidateCustomTitleText("Send Final Email");
        }

        [TestProperty("JiraIssueID", "CDV6-24823")]
        [Description("Open an unpublished workflow record - Click on a action without a custom title - Wait for the workflow action popup to load - " +
            "Validate that the Custom Title field is displayed as empty")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod006()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "10");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ValidateActionSelectedValue("Get Current Date")
                .ValidateCustomTitleCheckboxChecked(false)
                .ValidateCustomTitleTextboxDisabled(true)
                .ValidateCustomTitleText("");
        }

        [TestProperty("JiraIssueID", "CDV6-24824")]
        [Description("Open an published workflow record - Click on a action with a custom title - Wait for the workflow action popup to load - " +
            "Validate that the Custom Title elements are displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod007()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "2");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()

                .ValidateActionPicklistDisabled(true)
                .ValidateActionSelectedValue("Send Email")
                .ValidateCustomTitleCheckboxDisabled(true)
                .ValidateCustomTitleCheckboxChecked(true)
                .ValidateCustomTitleTextboxDisabled(true)
                .ValidateCustomTitleText("Send Final Email");
        }

        [TestProperty("JiraIssueID", "CDV6-24825")]
        [Description("Open an published  workflow record - Click on a action without a custom title - Wait for the workflow action popup to load - " +
            "Validate that the Custom Title field is displayed as empty")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod008()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "5");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()

                .ValidateActionPicklistDisabled(true)
                .ValidateActionSelectedValue("Get Current Date")
                .ValidateCustomTitleCheckboxDisabled(true)
                .ValidateCustomTitleCheckboxChecked(false)
                .ValidateCustomTitleTextboxDisabled(true)
                .ValidateCustomTitleText("");
        }

        [TestProperty("JiraIssueID", "CDV6-24826")]
        [Description("Open an unpublished workflow record - Click on a action with a custom title - Wait for the workflow action popup to load - " +
            "Change the Custom Title text - Save the changes - Validate that the action text is updated in the workflow tab")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod009()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "4");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .InsertCustomTitle("Send Final Email UPDATED")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingActionLink_Level1Text("1", "4", "Send Final Email UPDATED");
        }

        [TestProperty("JiraIssueID", "CDV6-24827")]
        [Description("Open an unpublished workflow record - Click on a action without a custom title - Wait for the workflow action popup to load - " +
            "Validate that the Custom Title field is displayed as empty")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod010()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "10");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickCustomTitleCheckbox()
                .InsertCustomTitle("Get Current Date UPDATED")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingActionLink_Level1Text("1", "10", "Get Current Date UPDATED");
        }

        [TestProperty("JiraIssueID", "CDV6-24828")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Conditions that reference actions with custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod011()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()

                .ValidateExistingConditionLink_Level1Visibility("1", "18", true)
                .ValidateExistingConditionLink_Level1Text("1", "18", "[Create Phone Call Record 1].Subject Equals [Value 1]")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24829")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Conditions that reference actions without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod012()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()

                .ValidateExistingConditionLink_Level1Visibility("1", "20", true)
                .ValidateExistingConditionLink_Level1Text("1", "20", "[Date Helpers: Get Current Date].Current Date Equals [01/06/2021]");
        }

        [TestProperty("JiraIssueID", "CDV6-24830")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Conditions that reference actions withh custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod013()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()

                .ValidateExistingConditionLink_Level1Visibility("1", "9", true)
                .ValidateExistingConditionLink_Level1Text("1", "9", "[Create Phone Call Record 1].Subject Equals [Value 1]")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24831")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Conditions that reference actions without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod014()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");
            dbHelper.workflow.UpdatePublishedField(workflowid, true);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForPublishedWorkflowRecordPageToLoad()

                .ValidateExistingConditionLink_Level1Visibility("1", "10", true)
                .ValidateExistingConditionLink_Level1Text("1", "10", "[Date Helpers: Get Current Date].Current Date Equals [01/06/2021]");
        }

        [TestProperty("JiraIssueID", "CDV6-24832")]
        [Description("Open an unpublished workflow record - " +
            "Click on a Condition that has a reference to an actions with a custom title  - Wait for the Workflow Condition to load")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod015()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingConditionLink_Level1("1", "18");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObject_ConditionTitleText(1, "Create Phone Call Record 1");
        }

        [TestProperty("JiraIssueID", "CDV6-24833")]
        [Description("Open an unpublished workflow record - " +
            "Validate that Conditions that reference actions without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod016()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingConditionLink_Level1("1", "20");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObject_ConditionTitleText(1, "Date Helpers: Get Current Date");
        }

        [TestProperty("JiraIssueID", "CDV6-24834")]
        [Description("Open an unpublished workflow record - Click on a action with a custom title (that is referenced in a workflow condition) - " +
            "Wait for the workflow action popup to load - Change the Custom Title text - Save the changes - " +
            "Validate that the action text is updated in the workflow tab as well as for all conditions referencing the action")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod017()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "8");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .InsertCustomTitle("Create Phone Call Record 1 UPDATED")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingActionLink_Level1Text("1", "8", "Create Phone Call Record 1 UPDATED")
                .ValidateExistingConditionLink_Level1Text("1", "18", "[Create Phone Call Record 1].Subject Equals [Value 1]")
                .ClickExistingConditionLink_Level1("1", "18");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObject_ConditionTitleText(1, "Create Phone Call Record 1 UPDATED") //we need to save the workflow condition (even though we changed nothing) for the condition name to be updated
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Text("1", "18", "[Create Phone Call Record 1 UPDATED].Subject Equals [Value 1]");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-24835")]
        [Description("Open an unpublished workflow record - Click on a action without a custom title (that is referenced in a workflow condition) - " +
            "Wait for the workflow action popup to load - Change the Custom Title text - Save the changes - " +
            "Validate that the action text is updated in the workflow tab as well as for all conditions referencing the action")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod018()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "10");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickCustomTitleCheckbox()
                .InsertCustomTitle("Get Current Date 2 NEW TITLE")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingActionLink_Level1Text("1", "10", "Get Current Date 2 NEW TITLE")
                .ValidateExistingConditionLink_Level1Text("1", "20", "[Date Helpers: Get Current Date].Current Date Equals [01/06/2021]")
                .ClickExistingConditionLink_Level1("1", "20");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObject_ConditionTitleText(1, "Get Current Date 2 NEW TITLE")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ValidateExistingConditionLink_Level1Text("1", "20", "[Get Current Date 2 NEW TITLE].Current Date Equals [01/06/2021]");

        }

        [TestProperty("JiraIssueID", "CDV6-24836")]
        [Description("Open an unpublished workflow record - Click on a action with a custom title (that is referenced in a workflow condition) - " +
            "Wait for the workflow action popup to load - Change the Custom Title text - Save the changes - " +
            "Click on the workflow condition that references the updated workflow action - Wait for the Workflow Conditions popup to load - " +
            "Validate that the Related business Objects picklist contains the updated condition name")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod019()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "8");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .InsertCustomTitle("Create Phone Call Record 1 UPDATED")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingConditionLink_Level1("1", "18");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObjectsContainsElement("Create Phone Call Record 1 UPDATED");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-24837")]
        [Description("Open an unpublished workflow record - Click on a action without a custom title (that is referenced in a workflow condition) - " +
            "Wait for the workflow action popup to load - Change the Custom Title text - Save the changes - " +
            "Click on the workflow condition that references the updated workflow action - Wait for the Workflow Conditions popup to load - " +
            "Validate that the Related business Objects picklist contains the updated condition name")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod020()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "10");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickCustomTitleCheckbox()
                .InsertCustomTitle("Get Current Date 2 NEW TITLE")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingConditionLink_Level1("1", "20");

            workflowConditionsPopup
                .WaitForWorkflowConditionsPopupToLoad()
                .ValidateRelatedBusinessObjectsContainsElement("Get Current Date 2 NEW TITLE");
            ;
        }

        [TestProperty("JiraIssueID", "CDV6-24838")]
        [Description("Open an unpublished workflow record - Click on Send Email workflow action (action has email fields set using local values with custom titles) - " +
            "Wait for the workflow action popup to load - Click on the Set Properties button - Validate that the fields set with local values with custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod021()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "22");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickSetPropertiesButton();

            workflowSendEmailPropertiesPage
                .WaitForWorkflowSendEmailPropertiesPageToLoad()
                .ValidateRegardingFieldLocalValueText("{Regarding (Create Phone Call Record 1)}\r\nRemove");
        }

        [TestProperty("JiraIssueID", "CDV6-24839")]
        [Description("Open an unpublished workflow record - Click on Send Email workflow action (action has email fields set using local values without custom titles) - " +
            "Wait for the workflow action popup to load - Click on the Set Properties button - " +
            "Validate that the fields set with local values without custom titles are correctly displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod022()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "22");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickSetPropertiesButton();

            workflowSendEmailPropertiesPage
                .WaitForWorkflowSendEmailPropertiesPageToLoad()
                .ValidateDueDateFieldLocalValueText("{Current Date (Date Helpers: Get Current Date)}\r\nRemove");
        }

        [TestProperty("JiraIssueID", "CDV6-24840")]
        [Description("Open an unpublished workflow record - Click on Send Email workflow action - " +
            "Wait for the workflow action popup to load - Click on the Set Properties button - " +
            "Wait for the Workflow Send Email Properties Page to load - " +
            "Validate that Actions with and without Custom Titles are displayed 'Custom Fields Tool' BO Picklist")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod023()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "22");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickSetPropertiesButton();

            workflowSendEmailPropertiesPage
                .WaitForWorkflowSendEmailPropertiesPageToLoad()
                .ValidateBusinessObjectPicklistContainsElement("Create Phone Call Record 1")
                .ValidateBusinessObjectPicklistContainsElement("Date Helpers: Get Current Date");
        }

        [TestProperty("JiraIssueID", "CDV6-24841")]
        [Description("Open an unpublished workflow record - Click on Send Email workflow action - " +
            "Wait for the workflow action popup to load - Click on the Set Properties button - " +
            "Wait for the Workflow Send Email Properties Page to load - Select the Category fields - " +
            "On the Custom Field Tool set the business object pointing to a workflow action with a custom title - select a matching field - " +
            "Add the field to the Category field for the email - Validate that the Category Field local value name matches the action with the custom title")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod024()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "22");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickSetPropertiesButton();

            workflowSendEmailPropertiesPage
                .WaitForWorkflowSendEmailPropertiesPageToLoad()
                .ClickCategoryFieldFormControlArea()
                .SelectBusinessObjectPicklistElement("Create Phone Call Record 1")
                .SelectBusinessObjectFieldsPicklistElement("Category")
                .ClickSelectButton()
                .SelectFieldToAdd("Category (Create Phone Call Record 1)")
                .ClickAddButton()
                .ValidateCategoryFieldLocalValueText("{Category (Create Phone Call Record 1)}\r\nRemove");

            workflowSendEmailPropertiesPage
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad();

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24842")]
        [Description("Open an unpublished workflow record - Click on a action with a custom title (that is referenced inside a send email action) - " +
            "Wait for the workflow action popup to load - Change the Custom Title text - Save the changes" +
            "Click on Send Email workflow action - " +
            "Wait for the workflow action popup to load - Validate that the reference to the action with custom title was updated")]
        [TestCategory("UITest")]
        [TestMethod]
        public void OptionalTextboxForActionName_UITestMethod025()
        {
            var workflowid = new Guid("d44fcc56-01c5-eb11-a323-005056926fe4"); //WF Automated Testing - CDV6-8710
            dbHelper.workflow.UpdatePublishedField(workflowid, false);
            dbHelper.workflow.UpdateWorkflowXmlField(workflowid, "<WorkflowRule   RunOrdered=\"false\">    <WorkflowStep Id=\"1\" Name=\"New Step\">      <ActionsOrConditions>        <WorkflowActionOrCondition Id=\"f8b05fc6-50e0-41a9-961e-d5e05898abe0\" ProcessOrder=\"0\">          <Action Id=\"6152bdf9-0bb8-43ab-a15d-ce215cb0bb8c\" Name=\"Send Initial Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"fa6cf2df-1421-4747-a8c1-15344676486c\" ProcessOrder=\"1\">          <Action Id=\"8ce13ab3-cc69-4c41-8041-e49f074dc448\" Name=\"Send Final Email\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"8988 8988\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"8988 8988\" TableName=\"systemuser\" ValueCustom=\"cee7c3eb-ea91-eb11-a323-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"637c6045-b737-47f8-a413-63e350fa8f76\" ProcessOrder=\"2\">          <Action Id=\"80c2226c-9f26-4ef7-9f27-43888ef835a2\" Name=\"Get Current Date 1\" CustomTitle=\"true\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"0679e273-938c-4179-b084-0317d7fa1cd8\" ProcessOrder=\"3\">          <Action Id=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" Name=\"Create Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\">            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"In Progress\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"directionid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Incoming\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"phone call 2\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"506a4734-4649-47e5-86d0-3e35a2ca6f5a\" ProcessOrder=\"4\">          <Action Id=\"66de758a-7307-432f-b790-1253323b7aab\" Name=\"Date Helpers: Get Current Date\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"2235122f-5e6c-eb11-93cc-747827229445\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"06f496ec-a38c-4ec4-bac7-49f877ee289f\" ProcessOrder=\"5\">          <Action Id=\"d0b86654-9ab4-4bad-907b-3b8b90079b00\" Name=\"Update Phone Call Record 1\" CustomTitle=\"true\" ActionType=\"2\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\">            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"responsibleuserid\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"38d8b170-73e4-416d-beb9-6f55748672a8\" ProcessOrder=\"6\">          <Action Id=\"c7483e41-2ee8-4c77-a4c7-cbd28ac54ee9\" Name=\"Change created phone call record status\" CustomTitle=\"true\" ActionType=\"6\" ValueCustom=\"true\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"c0f14dae-dd98-41dd-b017-a1ed0b5b9901\" ProcessOrder=\"7\">          <Action Id=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" Name=\"Business Data: Get Initiating User\" CustomTitle=\"false\" ActionType=\"8\" ValueCustom=\"9ffe7312-691d-e911-97ca-d89ef34c4720\" />        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"1a0cd638-957d-4d1b-a671-2bf3d9fae3fc\" ProcessOrder=\"8\">          <GroupCondition Id=\"2bfcbaa3-70c2-493f-921c-d8ad59e75ab2\" Name=\"[Create Phone Call Record 1].Subject &lt;i&gt;Equals&lt;/i&gt; [Value 1]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"06fc8609-1ee6-49b7-86b3-2ac1d5eefefd\" ConditionOperatorType=\"1\" BusinessObject=\"49353aab-f3a5-e811-80dc-0050560502cc\" RelationshipParental=\"true\" LeftElement=\"subject\" RightValue=\"Value 1\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"7b95dfa2-a8a3-48dd-83f1-c483fdda95ba\" ProcessOrder=\"9\">          <GroupCondition Id=\"2d305e66-91d3-44c7-a674-b69913fb7835\" Name=\"[Date Helpers: Get Current Date].Current Date &lt;i&gt;Equals&lt;/i&gt; [01/06/2021]\" ConditionGroupByType=\"And\">            <Conditions>              <WorkflowCondition Id=\"ff77eb00-c88a-4572-a42f-27e2ecac5959\" ConditionOperatorType=\"1\" RelationshipParental=\"true\" LeftElement=\"CurrentDate\" RightValue=\"2021-6-1\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </Conditions>          </GroupCondition>        </WorkflowActionOrCondition>        <WorkflowActionOrCondition Id=\"f5392e9c-35e6-462d-b7fa-c7bc37b36483\" ProcessOrder=\"10\">          <Action Id=\"1cfa8336-8c12-4191-9f8f-29aa4f466e4d\" Name=\"Send Email 3\" CustomTitle=\"true\" ActionType=\"4\">            <WorkflowActionField TargetElement=\"emailfromid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Aaron Kirk\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"emailtoid\" Operator=\"1\">              <WorkflowActionValue Type=\"2\">                <WorkflowActionValue DisplayName=\"Aaron Kirk\" TableName=\"systemuser\" ValueCustom=\"30ff227c-48c7-ea11-a2cd-005056926fe4\" />              </WorkflowActionValue>            </WorkflowActionField>            <WorkflowActionField TargetElement=\"subject\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"Email 3\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"regardingid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"regardingid\" Action=\"1bb19b86-5ef7-4bff-aa72-971ff35e96f8\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"duedate\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"CurrentDate\" Action=\"66de758a-7307-432f-b790-1253323b7aab\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"informationbythirdparty\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscasenote\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"ownerid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"CareDirector QA\" ValueCustom=\"b6060dfa-7333-43b2-a662-3d9cadab12e5\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"responsibleuserid\" Operator=\"1\">              <WorkflowActionValue ValueElement=\"systemuserid\" Action=\"91bf9dff-6bea-448a-8e45-1cd7c2c83898\" ActionField=\"InitiatingUser\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"statusid\" Operator=\"1\">              <WorkflowActionValue DisplayName=\"Draft\" ValueCustom=\"1\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"inactive\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"issignificantevent\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>            <WorkflowActionField TargetElement=\"iscloned\" Operator=\"1\">              <WorkflowActionValue ValueCustom=\"false\" />            </WorkflowActionField>          </Action>        </WorkflowActionOrCondition>      </ActionsOrConditions>    </WorkflowStep>  </WorkflowRule>");

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .SearchWorkflowRecord("WF Automated Testing - CDV6-8710")
                .OpenWorkflowRecord(workflowid.ToString());

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "8");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .InsertCustomTitle("Create Phone Call Record 1 UPDATED")
                .ClickSaveAndCloseButton();

            workflowRecordPage
                .WaitForUnpublishedWorkflowRecordPageToLoad()
                .ClickExistingActionLink_Level1("1", "22");

            workflowActionPopup
                .WaitForWorkflowActionPopupToLoad()
                .ClickSetPropertiesButton();

            workflowSendEmailPropertiesPage
                .WaitForWorkflowSendEmailPropertiesPageToLoad()
                .ValidateRegardingFieldLocalValueText("{Regarding (Create Phone Call Record 1 UPDATED)}\r\nRemove");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10952

        [TestProperty("JiraIssueID", "CDV6-24843")]
        [Description("Create a new phone call record so that the workflow 'WF Automated Testing - CDV6-10952' can be triggered - " +
            "Validate that a new workflow job is created with a trigger date 2 days after the phone call date field - " +
            "Update the phone call date field - Validate that the old workflow job is deleted and a new one is created with a correct trigger date")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflowDateFieldChanges_UITestMethod001()
        {
            var workflowID = new Guid("b2628112-7add-eb11-a325-005056926fe4"); //WF Automated Testing - CDV6-10952

            //ARRANGE 
            string subject = "WF Testing - CDV6-10952 - Execute After - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = new Guid("f90539e6-93eb-40ff-9e05-7d279de8e4ca");
            string callerIdTableName = "person";
            string callerIDName = "Deirdre Hernandez";

            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = new Guid("f90539e6-93eb-40ff-9e05-7d279de8e4ca");
            string regardingName = "Deirdre Hernandez";  //40927

            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

            DateTime phonecallDate = commonMethodsHelper.GetDatePartWithoutCulture();


            //delete all phone call records for the person
            foreach (var phonecall in dbHelper.phoneCall.GetPhoneCallByRegardingID(regardingID))
                dbHelper.phoneCall.DeletePhoneCall(phonecall);


            //delete all workflow job records for the workflow
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);

            //Create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);

            //we should have 1 workflow job created at this point
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);

            //Assert that the trigger date for the workflow job is correct
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(phonecallDate.AddDays(2), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            var newPhoneCallDate = phonecallDate.AddDays(1);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("40927", regardingID.ToString())
                .OpenPersonRecord(regardingID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallID.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("WF Testing - CDV6-10952 - Execute After - Scenario 1")
                .InsertPhoneCallDate(newPhoneCallDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();


            //we should have 1 workflow job created at this point
            workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);

            //Assert that the trigger date for the workflow job is correct
            workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(newPhoneCallDate.AddDays(2), ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());
        }

        [TestProperty("JiraIssueID", "CDV6-24844")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - Set a Description - " +
            "Set 'Type' to Wait - Click on the Save and Close button - " +
            "Validate that the user is prevented from saving the workflow (Date Field should be mandatory)")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflowDateFieldChanges_UITestMethod002()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .InsertName("WF Testing CDV6-10952")
                .InsertDescription("Testing CDV6-10952")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")
                .TapSaveButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateDateFieldErrorLabelVisibility(true)
                .ValidateDateFieldErrorLabelText("Please fill out this field.")
                ;
        }


        [TestProperty("JiraIssueID", "CDV6-24845")]
        [Description("Navigate to the workflows page - Add a new Workflow record - Set 'Record Type' to PhoneCall - Set 'Scope' to Organization - Set a Description - " +
            "Set 'Type' to Wait - Validate that the 'Record Is Deleted' option is hidden ")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflowDateFieldChanges_UITestMethod003()
        {
            var businessObjectID = new Guid("49353aab-f3a5-e811-80dc-0050560502cc"); //PhoneCall

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToWorkflowSection();

            workflowsPage
                .WaitForWorkflowsPageToLoad()
                .ClickNewRecordButton();

            workflowRecordPage
                .WaitForDetailsTablToLoad()

                .ValidateRecordIsDeletedOptionVisibility(true)

                .InsertName("WF Testing CDV6-10952")
                .InsertDescription("Testing CDV6-10952")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("phonecall").TapSearchButton().SelectResultElement(businessObjectID.ToString());

            workflowRecordPage
                .WaitForDetailsTablToLoad()
                .SelectScope("Organization")
                .SelectType("Wait")

                .ValidateRecordIsDeletedOptionVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24846")]
        [Description("Create a new phone call record so that the workflow 'WF Automated Testing - CDV6-10952 (Empty Dates)' can be triggered - " +
            "Validate that a new workflow job is created with a trigger date equal to the phone call record date - " +
            "Update the phone call date field - Validate that the old workflow job is deleted and a new one is created with a trigger date matching the new phone call date")]
        [TestCategory("UITest")]
        [TestMethod]
        public void WaitWorkflowDateFieldChanges_UITestMethod004()
        {
            var workflowID = new Guid("ec6c49ad-81dd-eb11-a325-005056926fe4"); //WF Automated Testing - CDV6-10952 (Empty Dates)

            //ARRANGE 
            string subject = "WF Testing - CDV6-10952 - Empty Dates - Scenario 1";
            string description = "Sample Description ...";
            Guid callerID = new Guid("f90539e6-93eb-40ff-9e05-7d279de8e4ca");
            string callerIdTableName = "person";
            string callerIDName = "Deirdre Hernandez";

            Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
            string recipientIdTableName = "systemuser";
            string recipientIDName = "José Brazeta";

            string phoneNumber = "0987654321234";

            Guid regardingID = new Guid("f90539e6-93eb-40ff-9e05-7d279de8e4ca");
            string regardingName = "Deirdre Hernandez";  //40927

            Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

            DateTime phonecallDate = DateTime.Now.Date;


            //delete all phone call records for the person
            foreach (var phonecall in dbHelper.phoneCall.GetPhoneCallByRegardingID(regardingID))
                dbHelper.phoneCall.DeletePhoneCall(phonecall);


            //delete all workflow job records for the workflow
            foreach (var wfjobid in dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID))
                dbHelper.workflowJob.DeleteWorkflowJob(wfjobid);

            //Create the phone call record
            Guid phoneCallID = dbHelper.phoneCall.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName,
                recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);

            //we should have 1 workflow job created at this point
            var workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);

            //Assert that the trigger date for the workflow job is correct
            var workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(phonecallDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());

            var newPhoneCallDate = phonecallDate.AddDays(1);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID("40927", regardingID.ToString())
                .OpenPersonRecord(regardingID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPhoneCallsPage();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad()
                .OpenPersonPhoneCallRecord(phoneCallID.ToString());

            personPhoneCallRecordPage
                .WaitForPersonPhoneCallRecordPageToLoad("WF Testing - CDV6-10952 - Empty Dates - Scenario 1")
                .InsertPhoneCallDate(newPhoneCallDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickSaveAndCloseButton();

            personPhoneCallsPage
                .WaitForPersonPhoneCallsPageToLoad();


            //we should have 1 workflow job created at this point
            workflowJobIds = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID);
            Assert.AreEqual(1, workflowJobIds.Count);

            //Assert that the trigger date for the workflow job is correct
            workflowJobFields = dbHelper.workflowJob.GetWorkflowJobByID(workflowJobIds[0], "triggerdate");
            Assert.AreEqual(newPhoneCallDate, ((DateTime)workflowJobFields["triggerdate"]).ToLocalTime());
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
