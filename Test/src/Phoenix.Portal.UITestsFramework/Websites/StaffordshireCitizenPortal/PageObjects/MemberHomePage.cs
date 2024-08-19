using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class MemberHomePage : CommonMethods
    {
        public MemberHomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By PageTitle = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Welcome to the Social Care Portal']");

        readonly By financialAssessmentsArea = By.XPath("//*[@id='CWFinancialAssessments']");


        #region My Assessments

        readonly By assessmentsArea = By.XPath("//*[@id='CWCaseForms']");

        readonly By WidgetTitle_MyAssessmentWidget = By.XPath("//mosaic-col[@id='CWCaseForms']/mosaic-card/mosaic-card-header/div/div[text()='My Assessments']");

        By MyAssessmentRecord_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion[@id='CWCaseForms_CWAccordianContainer']/mosaic-accordion-item[@label='" + AssessmentName + "']");
        By MyAssessmentName_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion[@id='CWCaseForms_CWAccordianContainer']/mosaic-accordion-item/div/a/div/div[text()='" + AssessmentName + "']");
        By MyAssessmentViewDetailsButton_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/div/div/mosaic-accordion-header-items/mosaic-button");
        By MyAssessmentViewDetailsButtonById_AssessmentWidget(string AssessmentId) => By.XPath("//*[@id='CWCaseForms_CWViewButton_" + AssessmentId + "']");
        By MyAssessmentStartDate_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/mosaic-collapse/div/p[2]");
        By MyAssessmentStatus_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/mosaic-collapse/div/p[3]");

        readonly By MyAssessmentsLoadMoreRecordsButton = By.XPath("//*[@id='CWCaseForms_CWLoadMoreButton']");



        #endregion

        #region Attachments

        readonly By personAttachmentsArea = By.XPath("//*[@id='CWPersonAttachments']");

        readonly By WidgetTitle_AttachmentWidget = By.XPath("//mosaic-col[@id='CWPersonAttachments']/mosaic-card/mosaic-card-header/div/div[text()='My Attachments']");

        By AttachmentRecord_AttachmentWidget(string AttachmentName) => By.XPath("//mosaic-accordion[@id='CWPersonAttachments_CWAccordianContainer']/mosaic-accordion-item[@label='" + AttachmentName + "']");
        By AttachmentName_AttachmentWidget(string AttachmentName) => By.XPath("//mosaic-accordion[@id='CWPersonAttachments_CWAccordianContainer']/mosaic-accordion-item/div/a/div/div[text()='" + AttachmentName + "']");
        By AttachmentViewDetailsButton_AttachmentWidget(string AttachmentName) => By.XPath("//mosaic-accordion-item[@label='" + AttachmentName + "']/div/div/mosaic-accordion-header-items/mosaic-button");
        By AttachmentCreatedOn_AttachmentWidget(string AttachmentName) => By.XPath("//mosaic-accordion-item[@label='" + AttachmentName + "']/mosaic-collapse/div/p[@class='mc-accordion__content-para']");

        readonly By AttachmentAddNewRecordButton = By.XPath("//*[@id='CWAddNewButton_CWPersonAttachments']");

        By AttachmentRecordViewButton(string RecordID) => By.XPath("//*[@id='CWPersonAttachments_CWViewButton_" + RecordID + "']");



        #endregion

        #region My To Do List

        readonly By personToDoListArea = By.XPath("//*[@id='CWToDo']");

        readonly By WidgetTitle_MyToDoListWidget = By.XPath("//*[@id='CWToDo']/mosaic-card/mosaic-card-header/div/div[text()='My To Do List']");

        By recordTitle_MyToDoListWidget(string ListName) => By.XPath("//*[@id='CWToDo_CWAccordianContainer']/mosaic-accordion-item/div/a/div/div[text()='" + ListName + "']");
        By DueDate_MyToDoListWidget(string ListName) => By.XPath("//mosaic-accordion-item[@label='" + ListName + "']/mosaic-collapse/div/p[@class='mc-accordion__content-para']");
        By ViewButton_MyToDoListWidget(string RecordID) => By.XPath("//*[@id='CWToDo_CWViewButton_" + RecordID + "']");

        #endregion

        #region On-Demand Workflows

        readonly By OnDemandWorkflow_Area = By.XPath("//*[@id='CWOnDemandWorkflow']");
        readonly By OnDemandWorkflow_AreaTitle = By.XPath("//*[@id='CWOnDemandWorkflow']/*/*/*/div[text()='Create a New Process']");
        By OnDemandWorkflow_RecordText(int RecordPosition) => By.XPath("//*[@id='CWOnDemandWorkflow_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/div/a/div/div");
        By OnDemandWorkflow_RecordDescription(int RecordPosition) => By.XPath("//*[@id='CWOnDemandWorkflow_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/mosaic-collapse/div/p");
        By OnDemandWorkflow_ViewButton(string OnDemandWorkflowId) => By.XPath("//*[@id='CWOnDemandWorkflow_CWViewButton_" + OnDemandWorkflowId + "']");

        #endregion

        #region Health Appointments

        readonly By HealthAppointmentsArea = By.XPath("//*[@id='CWHealthAppointments']");

        readonly By WidgetTitle_HealthAppointmentsWidget = By.XPath("//*[@id='CWHealthAppointments']/mosaic-card/mosaic-card-header/div/div[text()='My Health Appointments']");

        readonly By UpcomingHealthAppointmentsButton = By.XPath("//*[@id='HealthAppointment_CWShowHealthAppointments']/button");

        readonly By PastHealthAppointmentsButton = By.XPath("//*[@id='PastHealthAppointment_CWShowPastHealthAppointments']/button");


        #region Upcoming appointments

        readonly By HealthAppointmentsNoRecordsMessage = By.XPath("//*[@id='HealthAppointment_CWList']/mosaic-empty-status/div/h4");

        By HealthAppointmentRecordPosition_HealthAppointmentsWidget(int Position, string HealthAppointmentId) => By.XPath("//*[@id='HealthAppointment_CWList']/mosaic-card[" + Position + "]/mosaic-card-body/div/mosaic-button[@id='CWViewMoreButton_" + HealthAppointmentId + "']");
        By HealthAppointmentDateAndTime_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWViewMoreButton_" + HealthAppointmentId + "']/parent::div/p/mosaic-title/strong");
        By HealthAppointmentsStatus_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentStatus_" + HealthAppointmentId + "']/span");
        By HealthAppointmentsScheduledColor_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentStatus_" + HealthAppointmentId + "'][@color='info']");
        By HealthAppointmentsAttendedColor_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentStatus_" + HealthAppointmentId + "'][@color='success']");
        By HealthAppointmentsCancelledColor_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentStatus_" + HealthAppointmentId + "'][@color='warning']");
        By HealthAppointmentsDuration_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentDuration_" + HealthAppointmentId + "']");
        By HealthAppointmentsLocation_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentLocation_" + HealthAppointmentId + "']");
        By HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWHealthAppointmentCard_" + HealthAppointmentId + "']/mosaic-card-body/div/p[4]");
        By ViewMoreButton_HealthAppointmentsWidget(string HealthAppointmentId) => By.XPath("//*[@id='CWViewMoreButton_" + HealthAppointmentId + "']");


        By CardTitle_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/header/div/h5");
        By HealthAppointmentsDateAndTime_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog//*[@id='CWHealthAppointmentTitle_" + HealthAppointmentId + "']/strong");
        By HealthAppointmentsStatus_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p/mosaic-badge/span");
        By HealthAppointmentsScheduledColor_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p/mosaic-badge[@color='info']");
        By HealthAppointmentsCancelledColor_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p/mosaic-badge[@color='warning']");
        By HealthAppointmentsAttendedColor_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p/mosaic-badge[@color='success']");
        By HealthAppointmentsDuration_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p[2]");
        By HealthAppointmentsLocation_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p[3]");
        By HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p[4]");
        By HealthAppointmentsAppointmentReason_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p[5]");
        By HealthAppointmentsContactType_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/main/div/p[6]");
        By HealthAppointmentsOKButton_HealthAppointmentsCard(string HealthAppointmentId) => By.XPath("//mosaic-dialog[@id='CWHealthAppointment_" + HealthAppointmentId + "']/div/div/div/footer/mosaic-button");

        #endregion




        #region Past Appointments

        readonly By PastHealthAppointmentsNoRecordsMessage = By.XPath("//*[@id='PastHealthAppointment_CWList']/mosaic-empty-status/div/h4");

        By PastHealthAppointmentRecordPosition_HealthAppointmentsWidget(int Position, string HealthAppointmentId) => By.XPath("//*[@id='PastHealthAppointment_CWList']/mosaic-card[" + Position + "]/mosaic-card-body/div/mosaic-button[@id='CWViewMoreButton_" + HealthAppointmentId + "']");
        
        #endregion



        #endregion

        #region Person Assessments

        readonly By personAssessmentsArea = By.XPath("//*[@id='CWPersonForms']");

        readonly By WidgetTitle_PersonAssessmentWidget = By.XPath("//mosaic-col[@id='CWPersonForms']/mosaic-card/mosaic-card-header/div/div[text()='My Assessments']");

        By PersonAssessmentRecord_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion[@id='CWPersonForms_CWAccordianContainer']/mosaic-accordion-item[@label='" + AssessmentName + "']");
        By PersonAssessmentName_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion[@id='CWPersonForms_CWAccordianContainer']/mosaic-accordion-item/div/a/div/div[text()='" + AssessmentName + "']");
        By PersonAssessmentViewDetailsButton_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/div/div/mosaic-accordion-header-items/mosaic-button");
        By PersonAssessmentViewDetailsButtonById_AssessmentWidget(string AssessmentId) => By.XPath("//*[@id='CWPersonForms_CWViewButton_" + AssessmentId + "']");
        By PersonAssessmentStartDate_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/mosaic-collapse/div/p[2]");
        By PersonAssessmentStatus_AssessmentWidget(string AssessmentName) => By.XPath("//mosaic-accordion-item[@label='" + AssessmentName + "']/mosaic-collapse/div/p[3]");

        readonly By PersonAssessmentsLoadMoreRecordsButton = By.XPath("//*[@id='CWPersonForms_CWLoadMoreButton']");



        #endregion

        #region My Consents

        readonly By personConsentsArea = By.XPath("//*[@id='CWPersonConsent']");

        readonly By WidgetTitle_ConsentWidget = By.XPath("//*[@id='CWPersonConsent']/mosaic-card/mosaic-card-header/div/div[text()='My Consents']");

        By ConsentRecord_ConsentWidget(int RecordPosition) => By.XPath("//*[@id='CWPersonConsent_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/div/a");
        By ConsentName_ConsentWidget(int RecordPosition, string ConsentName) => By.XPath("//*[@id='CWPersonConsent_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/div/a/div/div[text()='" + ConsentName + "']");
        By ConsentType_ConsentWidget(int RecordPosition) => By.XPath("//*[@id='CWPersonConsent_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/mosaic-collapse/div/p[1]");
        By StartDate_ConsentWidget(int RecordPosition) => By.XPath("//*[@id='CWPersonConsent_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/mosaic-collapse/div/p[2]");
        By EndDate_ConsentWidget(int RecordPosition) => By.XPath("//*[@id='CWPersonConsent_CWAccordianContainer']/mosaic-accordion-item[" + RecordPosition + "]/mosaic-collapse/div/p[3]");

        #endregion


        public MemberHomePage WaitForMemberHomePageToLoad()
        {
            this.WaitForBrowserWindowTitle("Home");

            WaitForElement(PageTitle, 15);

            WaitForElementVisible(assessmentsArea);
            //WaitForElementVisible(financialAssessmentsArea);
            WaitForElementVisible(personAttachmentsArea);
            WaitForElementVisible(personToDoListArea);
            WaitForElementVisible(HealthAppointmentsArea);
            WaitForElementVisible(personAssessmentsArea);
            WaitForElementVisible(personConsentsArea);

            WaitForElement(WidgetTitle_AttachmentWidget);
            WaitForElement(WidgetTitle_MyAssessmentWidget);
            WaitForElement(WidgetTitle_MyToDoListWidget);
            WaitForElement(WidgetTitle_HealthAppointmentsWidget);
            WaitForElement(UpcomingHealthAppointmentsButton);
            WaitForElement(PastHealthAppointmentsButton);
            WaitForElement(WidgetTitle_PersonAssessmentWidget);
            WaitForElement(WidgetTitle_ConsentWidget);

            WaitForElement(AttachmentAddNewRecordButton);

            WaitForElement(OnDemandWorkflow_Area);
            WaitForElement(OnDemandWorkflow_AreaTitle);

            return this;
        }

        public MemberHomePage WaitForMemberHomePageToLoad(bool AttachmentWidgetVisible, bool MyAssessmentWidgetVisible, bool MyToDoListWidgetVisible)
        {
            this.WaitForBrowserWindowTitle("Home - Consumer Portal");

            WaitForElement(PageTitle);

            WaitForElementVisible(assessmentsArea);
            WaitForElementVisible(financialAssessmentsArea);
            WaitForElementVisible(personAttachmentsArea);
            WaitForElementVisible(personToDoListArea);

            if (AttachmentWidgetVisible)
            {
                WaitForElement(AttachmentAddNewRecordButton);
                WaitForElement(WidgetTitle_AttachmentWidget);
            }
            if (MyAssessmentWidgetVisible)
                WaitForElement(WidgetTitle_MyAssessmentWidget);
            if (MyToDoListWidgetVisible)
                WaitForElement(WidgetTitle_MyToDoListWidget);



            return this;
        }

        public MemberHomePage WaitForMemberHomePageToLoad(bool AttachmentWidgetVisible, bool MyAssessmentWidgetVisible, bool MyToDoListWidgetVisible, bool FinancialAssessmentsVisible)
        {
            this.WaitForBrowserWindowTitle("Home");

            WaitForElement(PageTitle);

            WaitForElementVisible(assessmentsArea);
            
            if(FinancialAssessmentsVisible)
                WaitForElementVisible(financialAssessmentsArea);

            WaitForElementVisible(personAttachmentsArea);
            WaitForElementVisible(personToDoListArea);

            if (AttachmentWidgetVisible)
            {
                WaitForElement(AttachmentAddNewRecordButton);
                WaitForElement(WidgetTitle_AttachmentWidget);
            }
            if (MyAssessmentWidgetVisible)
                WaitForElement(WidgetTitle_MyAssessmentWidget);
            if (MyToDoListWidgetVisible)
                WaitForElement(WidgetTitle_MyToDoListWidget);



            return this;
        }



        #region Attachments

        public MemberHomePage ValidateAttachmentRecordDisplayed(string AttachmentName)
        {
            WaitForElement(AttachmentRecord_AttachmentWidget(AttachmentName));
            WaitForElement(AttachmentName_AttachmentWidget(AttachmentName));
            WaitForElement(AttachmentViewDetailsButton_AttachmentWidget(AttachmentName));

            return this;
        }
        public MemberHomePage ValidateAttachmentRecordNotDisplayed(string AttachmentName)
        {
            WaitForElementNotVisible(AttachmentRecord_AttachmentWidget(AttachmentName), 5);
            WaitForElementNotVisible(AttachmentName_AttachmentWidget(AttachmentName), 5);
            WaitForElementNotVisible(AttachmentViewDetailsButton_AttachmentWidget(AttachmentName), 5);

            return this;
        }
        public MemberHomePage ClickAttachmentRecord(string AttachmentName)
        {
            WaitForElementToBeClickable(AttachmentRecord_AttachmentWidget(AttachmentName));
            Click(AttachmentRecord_AttachmentWidget(AttachmentName));

            return this;
        }
        public MemberHomePage ValidateAttachmentCreatedOnText(string AttachmentName, string ExpectedCreatedOnText)
        {
            ValidateElementText(AttachmentCreatedOn_AttachmentWidget(AttachmentName), ExpectedCreatedOnText);

            return this;
        }
        public MemberHomePage ValidateAttachmentCreatedOnInformationVisibility(string AttachmentName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(AttachmentCreatedOn_AttachmentWidget(AttachmentName));
            else
                WaitForElementNotVisible(AttachmentCreatedOn_AttachmentWidget(AttachmentName), 5);


            return this;
        }

        public MemberHomePage ClickAttachmentAddNewRecordButton()
        {
            Click(AttachmentAddNewRecordButton);

            return this;
        }

        public MemberHomePage ClickAttachmentViewButton(string RecordID)
        {
            Click(AttachmentRecordViewButton(RecordID));

            return this;
        }

        #endregion

        #region Assessments

        public MemberHomePage ValidateAssessmentRecordDisplayed(string AssessmentName)
        {
            WaitForElement(MyAssessmentRecord_AssessmentWidget(AssessmentName));
            WaitForElement(MyAssessmentName_AssessmentWidget(AssessmentName));
            WaitForElement(MyAssessmentViewDetailsButton_AssessmentWidget(AssessmentName));

            return this;
        }
        public MemberHomePage ValidateAssessmentRecordNotDisplayed(string AssessmentName)
        {
            WaitForElementNotVisible(MyAssessmentRecord_AssessmentWidget(AssessmentName), 5);
            WaitForElementNotVisible(MyAssessmentName_AssessmentWidget(AssessmentName), 5);
            WaitForElementNotVisible(MyAssessmentViewDetailsButton_AssessmentWidget(AssessmentName), 5);

            return this;
        }
        public MemberHomePage ValidateAssessmentStartDateText(string AssessmentName, string ExpectedStartDateText)
        {
            ValidateElementText(MyAssessmentStartDate_AssessmentWidget(AssessmentName), ExpectedStartDateText);

            return this;
        }
        public MemberHomePage ValidateAssessmentStartDateInformationVisibility(string AssessmentName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(MyAssessmentStartDate_AssessmentWidget(AssessmentName));
            else
                WaitForElementNotVisible(MyAssessmentStartDate_AssessmentWidget(AssessmentName), 5);


            return this;
        }
        public MemberHomePage ValidateAssessmentStatusText(string AssessmentName, string ExpectedStatusText)
        {
            ValidateElementText(MyAssessmentStatus_AssessmentWidget(AssessmentName), ExpectedStatusText);

            return this;
        }
        public MemberHomePage ValidateAssessmentStatusInformationVisibility(string AssessmentName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(MyAssessmentStatus_AssessmentWidget(AssessmentName));
            else
                WaitForElementNotVisible(MyAssessmentStatus_AssessmentWidget(AssessmentName), 5);


            return this;
        }
        public MemberHomePage ValidateAssessmentViewDetailsButtonVisibility(string AssessmentId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElement(MyAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId));
            else
                WaitForElementNotVisible(MyAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId), 5);

            return this;
        }

        public MemberHomePage ClickMyAssessmentsLoadMoreRecordsButton()
        {
            Click(MyAssessmentsLoadMoreRecordsButton);


            return this;
        }
        public MemberHomePage ClickAssessmentRecord(string AssessmentName)
        {
            Click(MyAssessmentRecord_AssessmentWidget(AssessmentName));

            return this;
        }
        public MemberHomePage ClickAssessmentViewDetailsButton(string AssessmentId)
        {
            Click(MyAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId));

            return this;
        }

        #endregion

        #region My To Do List

        public MemberHomePage ValidateMyToDoListRecordDisplayed(string RecordName)
        {
            WaitForElement(recordTitle_MyToDoListWidget(RecordName));

            return this;
        }
        public MemberHomePage ValidateRecordNotDisplayed(string RecordName)
        {
            WaitForElementNotVisible(recordTitle_MyToDoListWidget(RecordName), 5);

            return this;
        }
        public MemberHomePage ClickMyToDoListRecord(string RecordName)
        {
            Click(recordTitle_MyToDoListWidget(RecordName));

            return this;
        }
        public MemberHomePage ValidateMyToDoListDueDateText(string MyToDoListName, string ExpectedStartDateText)
        {
            ValidateElementText(DueDate_MyToDoListWidget(MyToDoListName), ExpectedStartDateText);

            return this;
        }
        public MemberHomePage ValidateMyToDoListDueDateVisibility(string MyToDoListName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(DueDate_MyToDoListWidget(MyToDoListName));
            else
                WaitForElementNotVisible(DueDate_MyToDoListWidget(MyToDoListName), 5);


            return this;
        }
        public MemberHomePage ClickMyToDoListViewRecordButton(string RecordId)
        {
            WaitForElementToBeClickable(ViewButton_MyToDoListWidget(RecordId));
            Click(ViewButton_MyToDoListWidget(RecordId));

            return this;
        }

        #endregion

        #region On-Demand Workflows

        public MemberHomePage ValidateOnDemandWorkflowRecordDisplayed(int RecordPosition, string OnDemandWorkflowId)
        {
            WaitForElementVisible(OnDemandWorkflow_RecordText(RecordPosition));
            WaitForElementVisible(OnDemandWorkflow_ViewButton(OnDemandWorkflowId));

            return this;
        }
        public MemberHomePage ValidateOnDemandWorkflowRecordNotDisplayed(int RecordPosition, string OnDemandWorkflowId)
        {
            WaitForElementNotVisible(OnDemandWorkflow_RecordText(RecordPosition), 5);
            WaitForElementNotVisible(OnDemandWorkflow_ViewButton(OnDemandWorkflowId), 5);

            return this;
        }
        public MemberHomePage ValidateOnDemandWorkflowRecordDescriptionDisplayed(int RecordPosition)
        {
            WaitForElementVisible(OnDemandWorkflow_RecordDescription(RecordPosition));

            return this;
        }
        public MemberHomePage ValidateOnDemandWorkflowRecordDescriptionNotDisplayed(int RecordPosition)
        {
            WaitForElementNotVisible(OnDemandWorkflow_RecordDescription(RecordPosition), 5);

            return this;
        }

        public MemberHomePage ClickOnDemandWorkflowViewButton(string OnDemandWorkflowId)
        {
            Click(OnDemandWorkflow_ViewButton(OnDemandWorkflowId));

            return this;
        }
        public MemberHomePage ClickOnDemandWorkflowRecordText(int RecordPosition)
        {
            Click(OnDemandWorkflow_RecordText(RecordPosition));

            return this;
        }


        public MemberHomePage ValidateOnDemandWorkflowText(int RecordPosition, string ExpectedText)
        {
            ValidateElementText(OnDemandWorkflow_RecordText(RecordPosition), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateOnDemandWorkflowRecordDescription(int RecordPosition, string ExpectedText)
        {
            ValidateElementText(OnDemandWorkflow_RecordDescription(RecordPosition), ExpectedText);

            return this;
        }

        #endregion

        #region Health Appointments

        public MemberHomePage ClickUpcomingHealthAppointmentsButton()
        {
            Click(UpcomingHealthAppointmentsButton);

            return this;
        }
        public MemberHomePage ClickPastHealthAppointmentsButton()
        {
            Click(PastHealthAppointmentsButton);

            return this;
        }

        public MemberHomePage ValidateHealthAppointmentNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(HealthAppointmentsNoRecordsMessage);
            }
            else
            {
                WaitForElementNotVisible(HealthAppointmentsNoRecordsMessage, 7);
            }

            return this;
        }

        public MemberHomePage ValidateHealthAppointmentPosition(string HealthAppointmentId, int ExpectPosition)
        {
            WaitForElementVisible(HealthAppointmentRecordPosition_HealthAppointmentsWidget(ExpectPosition, HealthAppointmentId));

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentVisibility(string HealthAppointmentId, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(HealthAppointmentDateAndTime_HealthAppointmentsWidget(HealthAppointmentId));
                WaitForElementVisible(HealthAppointmentsStatus_HealthAppointmentsWidget(HealthAppointmentId));
                WaitForElementVisible(HealthAppointmentsDuration_HealthAppointmentsWidget(HealthAppointmentId));
                WaitForElementVisible(HealthAppointmentsLocation_HealthAppointmentsWidget(HealthAppointmentId));
                WaitForElementVisible(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsWidget(HealthAppointmentId));
                WaitForElementVisible(ViewMoreButton_HealthAppointmentsWidget(HealthAppointmentId));
            }
            else
            {
                WaitForElementNotVisible(HealthAppointmentDateAndTime_HealthAppointmentsWidget(HealthAppointmentId), 3);
                WaitForElementNotVisible(HealthAppointmentsStatus_HealthAppointmentsWidget(HealthAppointmentId), 3);
                WaitForElementNotVisible(HealthAppointmentsDuration_HealthAppointmentsWidget(HealthAppointmentId), 3);
                WaitForElementNotVisible(HealthAppointmentsLocation_HealthAppointmentsWidget(HealthAppointmentId), 3);
                WaitForElementNotVisible(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsWidget(HealthAppointmentId), 3);
                WaitForElementNotVisible(ViewMoreButton_HealthAppointmentsWidget(HealthAppointmentId), 3);
            }

            return this;
        }


        public MemberHomePage ValidateHealthAppointmentScheduled(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsScheduledColor_HealthAppointmentsWidget(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsAttendedColor_HealthAppointmentsWidget(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsCancelledColor_HealthAppointmentsWidget(HealthAppointmentId), 7);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentAttended(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsAttendedColor_HealthAppointmentsWidget(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsScheduledColor_HealthAppointmentsWidget(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsCancelledColor_HealthAppointmentsWidget(HealthAppointmentId), 7);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCancelled(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsCancelledColor_HealthAppointmentsWidget(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsScheduledColor_HealthAppointmentsWidget(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsAttendedColor_HealthAppointmentsWidget(HealthAppointmentId), 7);

            return this;
        }



        public MemberHomePage ValidateHealthAppointmentDateAndTimeText(string HealthAppointmentId, string ExpectedText)
        {
            string dateText = GetElementText(HealthAppointmentDateAndTime_HealthAppointmentsWidget(HealthAppointmentId));
            Assert.AreEqual(ExpectedText, DateTime.Parse(dateText).ToString("dd MMM yyyy, HH:mm"));
            //ValidateElementText(HealthAppointmentDateAndTime_HealthAppointmentsWidget(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentStatusText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsStatus_HealthAppointmentsWidget(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentDurationText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsDuration_HealthAppointmentsWidget(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentLocationText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsLocation_HealthAppointmentsWidget(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentTeamAndLeadProfessionalText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsWidget(HealthAppointmentId), ExpectedText);

            return this;
        }

        public MemberHomePage ClickHealthAppointmentViewMoreButton(string HealthAppointmentId)
        {
            Click(ViewMoreButton_HealthAppointmentsWidget(HealthAppointmentId));

            return this;
        }



        public MemberHomePage WaitForHealthAppointmentCardToLoad(string HealthAppointmentId)
        {
            WaitForElementVisible(CardTitle_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsDateAndTime_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsStatus_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsDuration_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsLocation_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsAppointmentReason_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsContactType_HealthAppointmentsCard(HealthAppointmentId));
            WaitForElementVisible(HealthAppointmentsOKButton_HealthAppointmentsCard(HealthAppointmentId));

            return this;
        }
        public MemberHomePage WaitForHealthAppointmentCardToGetHidden(string HealthAppointmentId)
        {
            WaitForElementNotVisible(CardTitle_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsDateAndTime_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsStatus_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsDuration_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsLocation_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsAppointmentReason_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsContactType_HealthAppointmentsCard(HealthAppointmentId), 3);
            WaitForElementNotVisible(HealthAppointmentsOKButton_HealthAppointmentsCard(HealthAppointmentId), 3);

            return this;
        }

        public MemberHomePage ValidateHealthAppointmentCardDateAndTimeText(string HealthAppointmentId, string ExpectedText)
        {
            string dateText = GetElementText(HealthAppointmentsDateAndTime_HealthAppointmentsCard(HealthAppointmentId));
            Assert.AreEqual(ExpectedText, DateTime.Parse(dateText).ToString("dd MMM yyyy, HH:mm"));

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardStatusText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsStatus_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardDurationText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsDuration_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardLocationText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsLocation_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardTeamAndLeadProfessionalText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsTeamAndLeadProfessional_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardAppointmentReasonText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsAppointmentReason_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardContactTypeText(string HealthAppointmentId, string ExpectedText)
        {
            ValidateElementText(HealthAppointmentsContactType_HealthAppointmentsCard(HealthAppointmentId), ExpectedText);

            return this;
        }

        public MemberHomePage ClickHealthAppointmentCardOKButton(string HealthAppointmentId)
        {
            Click(HealthAppointmentsOKButton_HealthAppointmentsCard(HealthAppointmentId));

            return this;
        }


        public MemberHomePage ValidateHealthAppointmentCardScheduledColor(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsScheduledColor_HealthAppointmentsCard(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsCancelledColor_HealthAppointmentsCard(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsAttendedColor_HealthAppointmentsCard(HealthAppointmentId), 7);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardAttendedColor(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsAttendedColor_HealthAppointmentsCard(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsCancelledColor_HealthAppointmentsCard(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsScheduledColor_HealthAppointmentsCard(HealthAppointmentId), 7);

            return this;
        }
        public MemberHomePage ValidateHealthAppointmentCardCancelledColor(string HealthAppointmentId)
        {
            WaitForElementVisible(HealthAppointmentsCancelledColor_HealthAppointmentsCard(HealthAppointmentId));

            WaitForElementNotVisible(HealthAppointmentsScheduledColor_HealthAppointmentsCard(HealthAppointmentId), 7);
            WaitForElementNotVisible(HealthAppointmentsAttendedColor_HealthAppointmentsCard(HealthAppointmentId), 7);

            return this;
        }


        #region Past

        public MemberHomePage ValidatePastHealthAppointmentNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(PastHealthAppointmentsNoRecordsMessage);
            }
            else
            {
                WaitForElementNotVisible(PastHealthAppointmentsNoRecordsMessage, 7);
            }

            return this;
        }

        public MemberHomePage ValidatePastHealthAppointmentPosition(string HealthAppointmentId, int ExpectPosition)
        {
            WaitForElementVisible(PastHealthAppointmentRecordPosition_HealthAppointmentsWidget(ExpectPosition, HealthAppointmentId));

            return this;
        }

        #endregion

        #endregion

        #region Person Assessments

        public MemberHomePage ValidatePersonAssessmentRecordDisplayed(string AssessmentName)
        {
            WaitForElement(PersonAssessmentRecord_AssessmentWidget(AssessmentName));
            WaitForElement(PersonAssessmentName_AssessmentWidget(AssessmentName));
            WaitForElement(PersonAssessmentViewDetailsButton_AssessmentWidget(AssessmentName));

            return this;
        }
        public MemberHomePage ValidatePersonAssessmentRecordNotDisplayed(string AssessmentName)
        {
            WaitForElementNotVisible(PersonAssessmentRecord_AssessmentWidget(AssessmentName), 5);
            WaitForElementNotVisible(PersonAssessmentName_AssessmentWidget(AssessmentName), 5);
            WaitForElementNotVisible(PersonAssessmentViewDetailsButton_AssessmentWidget(AssessmentName), 5);

            return this;
        }
        public MemberHomePage ValidatePersonAssessmentStartDateText(string AssessmentName, string ExpectedStartDateText)
        {
            ValidateElementText(PersonAssessmentStartDate_AssessmentWidget(AssessmentName), ExpectedStartDateText);

            return this;
        }
        public MemberHomePage ValidatePersonAssessmentStartDateInformationVisibility(string AssessmentName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(PersonAssessmentStartDate_AssessmentWidget(AssessmentName));
            else
                WaitForElementNotVisible(PersonAssessmentStartDate_AssessmentWidget(AssessmentName), 5);


            return this;
        }
        public MemberHomePage ValidatePersonAssessmentStatusText(string AssessmentName, string ExpectedStatusText)
        {
            ValidateElementText(PersonAssessmentStatus_AssessmentWidget(AssessmentName), ExpectedStatusText);

            return this;
        }
        public MemberHomePage ValidatePersonAssessmentStatusInformationVisibility(string AssessmentName, bool ExpectVisible)
        {

            if (ExpectVisible)
                WaitForElement(PersonAssessmentStatus_AssessmentWidget(AssessmentName));
            else
                WaitForElementNotVisible(PersonAssessmentStatus_AssessmentWidget(AssessmentName), 5);


            return this;
        }
        public MemberHomePage ValidatePersonAssessmentViewDetailsButtonVisibility(string AssessmentId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElement(PersonAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId));
            else
                WaitForElementNotVisible(PersonAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId), 5);

            return this;
        }

        public MemberHomePage ClickPersonAssessmentsLoadMoreRecordsButton()
        {
            Click(PersonAssessmentsLoadMoreRecordsButton);


            return this;
        }
        public MemberHomePage ClickPersonAssessmentRecord(string AssessmentName)
        {
            Click(PersonAssessmentRecord_AssessmentWidget(AssessmentName));

            return this;
        }
        public MemberHomePage ClickPersonAssessmentViewDetailsButton(string AssessmentId)
        {
            Click(PersonAssessmentViewDetailsButtonById_AssessmentWidget(AssessmentId));

            return this;
        }

        #endregion

        #region My Consents

        public MemberHomePage ValidateMyConsentRecordVisibility(int RecordPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ConsentRecord_ConsentWidget(RecordPosition));
            else
                WaitForElementNotVisible(ConsentRecord_ConsentWidget(RecordPosition), 7);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordNameVisibility(int RecordPosition, string ConsentName, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ConsentName_ConsentWidget(RecordPosition, ConsentName));
            else
                WaitForElementNotVisible(ConsentName_ConsentWidget(RecordPosition, ConsentName), 7);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordConsentTypeVisibility(int RecordPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ConsentType_ConsentWidget(RecordPosition));
            else
                WaitForElementNotVisible(ConsentType_ConsentWidget(RecordPosition), 7);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordStartDateVisibility(int RecordPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StartDate_ConsentWidget(RecordPosition));
            else
                WaitForElementNotVisible(StartDate_ConsentWidget(RecordPosition), 7);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordEndDateVisibility(int RecordPosition, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EndDate_ConsentWidget(RecordPosition));
            else
                WaitForElementNotVisible(EndDate_ConsentWidget(RecordPosition), 7);

            return this;
        }


        public MemberHomePage ValidateMyConsentRecordConsentTypeText(int RecordPosition, string ExpectedText)
        {
            ValidateElementText(ConsentType_ConsentWidget(RecordPosition), ExpectedText);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordStartDateText(int RecordPosition, string ExpectedText)
        {
            ValidateElementText(StartDate_ConsentWidget(RecordPosition), ExpectedText);

            return this;
        }

        public MemberHomePage ValidateMyConsentRecordEndDateText(int RecordPosition, string ExpectedText)
        {
            ValidateElementText(EndDate_ConsentWidget(RecordPosition), ExpectedText);

            return this;
        }


        public MemberHomePage ClickOnMyConsentRecord(int RecordPosition)
        {
            WaitForElementToBeClickable(ConsentRecord_ConsentWidget(RecordPosition));

            Click(ConsentRecord_ConsentWidget(RecordPosition));

            return this;
        }

        #endregion
    }

}
