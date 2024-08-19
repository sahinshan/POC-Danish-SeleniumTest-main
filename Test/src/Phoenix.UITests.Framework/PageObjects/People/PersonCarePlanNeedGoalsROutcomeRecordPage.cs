using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCarePlanNeedGoalsROutcomeRecordPage : CommonMethods
    {
        public PersonCarePlanNeedGoalsROutcomeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
       
        readonly By personIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By personCarePlanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplan&')]");
        readonly By personCarePlanNeedGoalROutcomeRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplangoal&')]");
        
        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");

        readonly By createRecordButton = By.Id("TI_NewRecordButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By NeedsTab = By.Id("CWNavGroup_Needs");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_CaseNotes']");
       
        readonly By planAgreed_YesRadioButton = By.Id("CWField_planagreed_1");

        readonly By GoalOutcomeDescription_Field = By.Id("CWField_description");
        readonly By agreedTime_Field = By.Id("CWField_agreementdatetime_Time");
        readonly By carePlanAgreedById_LookupButton = By.Id("CWLookupBtn_careplanagreedbyid");
        readonly By carePlanFamilyInvolvedId = By.Id("CWField_careplanfamilyinvolvedid");
        readonly By familyNotInvolvedReasonId_LookupButton = By.Id("CWLookupBtn_familynotinvolvedreasonid");
        readonly By reason_TextBox = By.Id("CWField_reason");
        readonly By startDate_Button = By.Id("CWField_startdate_DatePicker");

        readonly By authorise_Button = By.Id("TI_AuthoriseButton");

        readonly By endDate_Field = By.XPath("//input[@name='CWField_enddate']");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");
        readonly By reviewFrequency_Field = By.Id("CWField_reviewfrequencyid");


        readonly By carePlanFamilyInvolvedId_Field = By.Id("CWField_careplanfamilyinvolvedid"); 
        readonly By familyInvolvedCarePlanId_Field = By.Id("CWFamilyInvolvedInCarePlanId");
        readonly By carePlanFamilyInvolvedErrorMessage = By.XPath("//span[text()='Please fill out this field.']");


        readonly By DomainOfNeed_LookUP = By.XPath("//button[@id='CWLookupBtn_careplanneeddomainid']");
        readonly By GoalOutcomeType_LookUP = By.XPath("//button[@id='CWLookupBtn_careplangoaltypeid']");
        readonly By ResponsibleUser_LookUP = By.XPath("//button[@id='CWLookupBtn_responsibleuserid']");

        readonly By SaveAndClose_Button = By.Id("TI_SaveAndCloseButton");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By copyCarePlan_Button = By.Id("TI_CopyCarePlanButton");

        readonly By careCoordinatorId_LookupButton = By.Id("CWLookupBtn_CWCareCoordinatorId");
        readonly By careResponsibleTeamId_LookupButton = By.XPath("//div/button[@id='CWLookupBtn_CWResponsibleTeamId']");
        readonly By carePlanTypeId_LookupButton = By.Id("CWLookupBtn_careplantypeid"); 
        readonly By caseId_LookupButton = By.Id("CWLookupBtn_CWCaseId");

        readonly By copy_Button = By.Id("CWCopy");
        readonly By Interventions_Link = By.Id("CWNavGroup_Interventions");
        readonly By NeedofDomain_Field = By.Id("CWField_careplanneeddomainid_cwname");
        readonly By GoalOutcome_Field = By.Id("CWField_careplangoaltypeid_cwname");
        #region Field Labels

        readonly By carePlanFieldLabel = By.XPath("//li[@id='CWLabelHolder_careplantypeid']/label");

        readonly By planAgreed_Label = By.XPath("//li[@id='CWLabelHolder_planagreed']/label");

        readonly By planAlsoAgreedBy_Label = By.XPath("//label[text()='Plan Also Agreed By']");

        readonly By familyInvolvedInCarePlan_Label = By.Id("CWFamilyInvolvedInCarePlanLabel");

        readonly By agreeWithPerson_Label = By.XPath("//label[text()='Agreed with Person or Legal Representative']");


        #endregion

        #region Fields

        readonly By carePlanField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_careplantypeid_Link']");
        readonly By carePlanLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_careplantypeid']");

        #endregion


        public PersonCarePlanNeedGoalsROutcomeRecordPage WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            
            WaitForElement(personCarePlanNeedGoalROutcomeRecordIFrame);
            SwitchToIframe(personCarePlanNeedGoalROutcomeRecordIFrame);

            WaitForElement(pageHeader);

              return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage InsertGoalOutcomeDescription(string Description)
        {
            WaitForElementToBeClickable(GoalOutcomeDescription_Field);
            SendKeys(GoalOutcomeDescription_Field, Description);
            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage TapDomainOfNeedLookUpButton()
        {
            WaitForElementToBeClickable(DomainOfNeed_LookUP);
            Click(DomainOfNeed_LookUP);
            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage TapGoalOutcomeTypeLookUpButton()
        {
            WaitForElementToBeClickable(GoalOutcomeType_LookUP);
            Click(GoalOutcomeType_LookUP);
            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage TapResonsibleUserLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_LookUP);
            Click(ResponsibleUser_LookUP);
            return this;
        }

        
        public PersonCarePlanNeedGoalsROutcomeRecordPage TapSaveNCloseButton()
        {
           
            Click(saveAndCloseButton);
            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage TapInterventionsLink()
        {

            Click(Interventions_Link);
            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage ValidateNeedOfDomainField(string ExpectedText)
        {
            ValidateElementText(NeedofDomain_Field, ExpectedText);

            return this;
        }

        public PersonCarePlanNeedGoalsROutcomeRecordPage ValidateGoalOutcomeField(string ExpectedText)
        {
            ValidateElementText(GoalOutcome_Field, ExpectedText);

            return this;
        }


    }
}
