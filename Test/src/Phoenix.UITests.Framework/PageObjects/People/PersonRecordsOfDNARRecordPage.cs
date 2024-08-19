using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordsOfDNARRecordPage : CommonMethods
    {

        public PersonRecordsOfDNARRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=persondnar')]");


        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        
        readonly By personTopBanner = By.XPath("//*[@id='CWBannerHolder']");
        readonly By personTopBanner_DNARActiveIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='DNAR Active'][@alt='DNAR Active']");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By printDNAR = By.Id("TI_PrintRecordButton");

        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div/span[text()='General']");
        readonly By Person_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personid']/label");
        readonly By DateTimeOfDNAR_FieldTitle = By.XPath("//*[@id='CWLabelHolder_dnardatetime']/label");
        readonly By ResponsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By AgeStatus_FieldTitle = By.XPath("//*[@id='CWLabelHolder_dnaragestatusid']/label");

        readonly By DoNotAttemptResuscitation_SectionTitle = By.XPath("//*[@id='CWSection_DoNotAttemptResuscitationSection']/fieldset/div/span[text()='Do Not Attempt Resuscitation']");
        readonly By DoNotAttemptResuscitation_Instructions = By.XPath("//*[@id='CWSection_DoNotAttemptResuscitationSection']/fieldset/div/div[@class='instructions']");
        readonly By AdultCPRDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_adultcprdecisionid']/label");
        readonly By RefusingTreatment_FieldTitle = By.XPath("//*[@id='CWLabelHolder_refusingtreatmentid']/label");
        readonly By ChildCPRDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_childcprdecisionid']/label");
        readonly By ChildInvolvedInDecisionMaking_FieldTitle = By.XPath("//*[@id='CWLabelHolder_childinvolvedindecisionmakingid']/label");
        readonly By ParentsAgreedToDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_parentsagreedtodecisionid']/label");


        readonly By SummaryReason_SectionTitle = By.XPath("//*[@id='CWSection_SummaryReasonSection']/fieldset/div/span[text()='Summary/Reason']");
        readonly By SummaryReason_Instructions = By.XPath("//*[@id='CWSection_SummaryReasonSection']/fieldset/div/div[@class='instructions']");
        readonly By InterestHarmCPRBenefit_FieldTitle = By.XPath("//*[@id='CWLabelHolder_interestharmcprbenefit']/label");
        readonly By NaturalDeath_FieldTitle = By.XPath("//*[@id='CWLabelHolder_naturaldeath']/label");
        readonly By DiscussedDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_discusseddecisionid']/label");
        readonly By DiscussedWithNextOfKin_FieldTitle = By.XPath("//*[@id='CWLabelHolder_discussedwithnextofkinid']/label");
        readonly By NextOfKinsInformation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_nextofkinsinformation']/label");
        readonly By CPRRefused_FieldTitle = By.XPath("//*[@id='CWLabelHolder_cprrefused']/label");
        readonly By Other_FieldTitle = By.XPath("//*[@id='CWLabelHolder_other']/label");
        readonly By AdditionalInformation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_additionalinformation']/label");
        readonly By NextOfKin_FieldTitle = By.XPath("//*[@id='CWLabelHolder_nextofkinid']/label");

        #region Child

        readonly By DiscussedWithParent_FieldTitle = By.XPath("//*[@id='CWLabelHolder_discussedwithparentid']/label");
        readonly By Parent_FieldTitle = By.XPath("//*[@id='CWLabelHolder_parentid']/label");


        #endregion

        readonly By ProfessionalCompletedDNAROrderDetails_SectionTitle = By.XPath("//*[@id='CWSection_ProfessionalCompletedDNAROrderDetailsSection']/fieldset/div/span[text()='Professional Completed DNAR Order Details']");
        readonly By DNAROrderCompletedBy_FieldTitle = By.XPath("//*[@id='CWLabelHolder_dnarcompletedbyid']/label");
        readonly By RegistrationNumber_FieldTitle = By.XPath("//*[@id='CWLabelHolder_completedbyregistrationnumber']/label");
        readonly By DateTimeCompleted_FieldTitle = By.XPath("//*[@id='CWLabelHolder_completeddate']/label");
        readonly By Position_FieldTitle = By.XPath("//*[@id='CWLabelHolder_completedbyposition']/label");

        readonly By ResponsibleClinicianDetails_SectionTitle = By.XPath("//*[@id='CWSection_ResponsibleClinicianDetails']/fieldset/div/span[text()='Responsible Clinician Details']");
        readonly By ResponsibleClinicianDetails_Instructions = By.XPath("//*[@id='CWSection_ResponsibleClinicianDetails']/fieldset/div/div[@class='instructions']");
        readonly By SeniorResponsibleClinicianWithOversight_FieldTitle = By.XPath("//*[@id='CWLabelHolder_seniorresponsibleclinicianid']/label");
        readonly By ClinicianRegistrationNumber_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clinicianregistrationnumber']/label");
        readonly By DateTimeOfOversight_FieldTitle = By.XPath("//*[@id='CWLabelHolder_oversightdate']/label");
        readonly By ClinicianPosition_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clinicianposition']/label");

        readonly By Review_SectionTitle = By.XPath("//*[@id='CWSection_ReviewSection']/fieldset/div/span[text()='Review']");
        readonly By ReviewDNAR_FieldTitle = By.XPath("//*[@id='CWLabelHolder_dnarreviewdecisionid']/label");

        readonly By CancellationOfDecision_SectionTitle = By.XPath("//*[@id='CWSection_CancellationOfDecisionSection']/fieldset/div/span[text()='Cancellation of Decision']");
        readonly By CancelledDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_canceldecision']/label");
        readonly By CancelledByRegistrationNumber_FieldTitle = By.XPath("//*[@id='CWLabelHolder_cancelledbyregistrationnumber']/label");
        readonly By CancelledOn_FieldTitle = By.XPath("//*[@id='CWLabelHolder_cancelleddate']/label");
        readonly By CancelledBy_FieldTitle = By.XPath("//*[@id='CWLabelHolder_cancelledbyid']/label");
        readonly By CancelledByPosition_FieldTitle = By.XPath("//*[@id='CWLabelHolder_cancelledbyposition']/label");

        readonly By Comments_SectionTitle = By.XPath("//*[@id='CWSection_CommentsSection']/fieldset/div/span[text()='Comments']");
        readonly By ReviewAdditionalComments_FieldTitle = By.XPath("//*[@id='CWLabelHolder_additionalcomments']/label");

        #endregion


        #region Fields

        readonly By Person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By Person_RemoveButton = By.XPath("//*[@id='CWClearLookup_personid']");
        readonly By Person_LookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Person_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_personid']/label/span");

        readonly By DateTimeOfDNAR_DateField = By.XPath("//*[@id='CWField_dnardatetime']");
        readonly By DateTimeOfDNAR_TimeField = By.XPath("//*[@id='CWField_dnardatetime_Time']");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By responsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By AgeStatus_Field = By.XPath("//*[@id='CWField_dnaragestatusid']");

        #region Adult

        readonly By AdultCPRDecision_Field = By.XPath("//*[@id='CWField_adultcprdecisionid']");
        readonly By AdultCPRDecision_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_adultcprdecisionid']/label/span");
        readonly By RefusingTreatment_Field = By.XPath("//*[@id='CWField_refusingtreatmentid']");

        #endregion

        #region Child

        readonly By ChildCPRDecision_Field = By.XPath("//*[@id='CWField_childcprdecisionid']");
        readonly By ChildCPRDecision_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_childcprdecisionid']/label/span");
        readonly By ChildInvolvedInDecisionMaking_Field = By.XPath("//*[@id='CWField_childinvolvedindecisionmakingid']");
        readonly By ParentsAgreedToDecision_Field = By.XPath("//*[@id='CWField_parentsagreedtodecisionid']");
        readonly By HasCourtMadeOrder_Field = By.XPath("//*[@id='CWField_hascourtmadeorderid']");

        #endregion

        readonly By InterestHarmCPRBenefit_YesRadioButton = By.XPath("//*[@id='CWField_interestharmcprbenefit_1']");
        readonly By InterestHarmCPRBenefit_NoRadioButton = By.XPath("//*[@id='CWField_interestharmcprbenefit_0']");
        readonly By NaturalDeath_YesRadioButton = By.XPath("//*[@id='CWField_naturaldeath_1']");
        readonly By NaturalDeath_NoRadioButton = By.XPath("//*[@id='CWField_naturaldeath_0']");
        readonly By DiscussedDecision_Picklist = By.XPath("//*[@id='CWField_discusseddecisionid']");
        readonly By ReasonDecisionNotDiscussed_Field = By.XPath("//*[@id='CWField_reasondecisionnotdiscussed']");
        readonly By DiscussedDecision_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_discusseddecisionid']/label/span");
        readonly By DiscussedWithNextOfKin_Picklist = By.XPath("//*[@id='CWField_discussedwithnextofkinid']");
        readonly By NextOfKinsInformation_Field = By.XPath("//*[@id='CWField_nextofkinsinformation']");
        readonly By CPRRefused_YesRadioButton = By.XPath("//*[@id='CWField_cprrefused_1']");
        readonly By CPRRefused_NoRadioButton = By.XPath("//*[@id='CWField_cprrefused_0']");
        readonly By Other_YesRadioButton = By.XPath("//*[@id='CWField_other_1']");
        readonly By Other_NoRadioButton = By.XPath("//*[@id='CWField_other_0']");
        readonly By AdditionalInformation_Field = By.XPath("//*[@id='CWField_additionalinformation']");
        readonly By NameOfNOK_LinkField = By.XPath("//*[@id='CWField_nextofkinid_Link']");
        readonly By NameOfNOK_RemoveButton = By.XPath("//*[@id='CWClearLookup_nextofkinid']");
        readonly By NameOfNOK_LookupButton = By.XPath("//*[@id='CWLookupBtn_nextofkinid']");

        #region Child

        readonly By DiscussedWithParent_Picklist = By.XPath("//*[@id='CWField_discussedwithparentid']");
        readonly By Parent_LinkField = By.XPath("//*[@id='CWField_parentid_Link']");
        readonly By Parent_RemoveButton = By.XPath("//*[@id='CWClearLookup_parentid']");
        readonly By Parent_LookupButton = By.XPath("//*[@id='CWLookupBtn_parentid']");
        readonly By Parent_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_parentid']/label/span");


        #endregion

        readonly By DNAROrderCompletedBy_LinkField = By.XPath("//*[@id='CWField_dnarcompletedbyid_Link']");
        readonly By DNAROrderCompletedBy_RemoveButton = By.XPath("//*[@id='CWClearLookup_dnarcompletedbyid']");
        readonly By DNAROrderCompletedBy_LookupButton = By.XPath("//*[@id='CWLookupBtn_dnarcompletedbyid']");
        readonly By DNAROrderCompletedBy_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_dnarcompletedbyid']/label/span");
        readonly By CompletedByRegistrationNumber_Field = By.XPath("//*[@id='CWField_completedbyregistrationnumber']");
        readonly By DateTimeCompleted_DateField = By.XPath("//*[@id='CWField_completeddate']");
        readonly By DateTimeCompleted_TimeField = By.XPath("//*[@id='CWField_completeddate_Time']");
        readonly By Position_Field = By.XPath("//*[@id='CWField_completedbyposition']");

        readonly By SeniorResponsibleClinicianWithOversight_LinkField = By.XPath("//*[@id='CWField_seniorresponsibleclinicianid_Link']");
        readonly By SeniorResponsibleClinicianWithOversight_Field = By.Id("CWField_seniorresponsibleclinicianid_cwname");
        readonly By SeniorResponsibleClinicianWithOversight_RemoveButton = By.XPath("//*[@id='CWClearLookup_seniorresponsibleclinicianid']");
        readonly By SeniorResponsibleClinicianWithOversight_LookupButton = By.XPath("//*[@id='CWLookupBtn_seniorresponsibleclinicianid']");
        readonly By ClinicianRegistrationNumber_Field = By.XPath("//*[@id='CWField_clinicianregistrationnumber']");
        readonly By DateTimeOfOversight_DateField = By.XPath("//*[@id='CWField_oversightdate']");
        readonly By DateTimeOfOversight_TimeField = By.XPath("//*[@id='CWField_oversightdate_Time']");
        readonly By ClinicianPosition_Field = By.XPath("//*[@id='CWField_clinicianposition']");

        readonly By ReviewDNAR_Field = By.XPath("//*[@id='CWField_dnarreviewdecisionid']");
        readonly By ReviewDNAR_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_dnarreviewdecisionid']/label/span");
        readonly By ReviewDate_Field = By.XPath("//*[@id='CWField_reviewdate']");
        readonly By ReviewDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_reviewdate']/label/span");


        readonly By CancelledDecision_YesRadioButton = By.XPath("//*[@id='CWField_canceldecision_1']");
        readonly By CancelledDecision_NoRadioButton = By.XPath("//*[@id='CWField_canceldecision_0']");
        readonly By CancelledByRegistrationNumber_Field = By.XPath("//*[@id='CWField_cancelledbyregistrationnumber']");
        readonly By CancelledOn_DateField = By.XPath("//*[@id='CWField_cancelleddate']");
        readonly By CancelledOn_TimeField = By.XPath("//*[@id='CWField_cancelleddate_Time']");
        readonly By CancelledBy_LinkField = By.XPath("//*[@id='CWField_cancelledbyid_Link']");
        readonly By CancelledBy_LookupButton = By.XPath("//*[@id='CWLookupBtn_cancelledbyid']");
        readonly By CancelledBy_RemoveButton = By.XPath("//*[@id='CWClearLookup_cancelledbyid']");
        readonly By CancelledByPosition_Field = By.XPath("//*[@id='CWField_cancelledbyposition']");

        readonly By ReviewAdditionalComments_Field = By.XPath("//*[@id='CWField_additionalcomments']");

        #endregion


        public PersonRecordsOfDNARRecordPage WaitForPersonRecordsOfDNARRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);
            
            this.WaitForElement(personTopBanner);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
            this.WaitForElement(DateTimeOfDNAR_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(AgeStatus_FieldTitle);
            this.WaitForElement(DoNotAttemptResuscitation_SectionTitle);
            this.WaitForElement(DoNotAttemptResuscitation_Instructions);
            this.WaitForElement(AdultCPRDecision_FieldTitle);
            this.WaitForElement(RefusingTreatment_FieldTitle);
            this.WaitForElement(SummaryReason_SectionTitle);
            this.WaitForElement(SummaryReason_Instructions);
            this.WaitForElement(InterestHarmCPRBenefit_FieldTitle);
            this.WaitForElement(NaturalDeath_FieldTitle);
            this.WaitForElement(DiscussedDecision_FieldTitle);
            this.WaitForElement(DiscussedWithNextOfKin_FieldTitle);
            this.WaitForElement(NextOfKinsInformation_FieldTitle);
            this.WaitForElement(CPRRefused_FieldTitle);
            this.WaitForElement(Other_FieldTitle);
            this.WaitForElement(AdditionalInformation_FieldTitle);
            this.WaitForElement(NextOfKin_FieldTitle);
            this.WaitForElement(ProfessionalCompletedDNAROrderDetails_SectionTitle);
            this.WaitForElement(DNAROrderCompletedBy_FieldTitle);
            this.WaitForElement(RegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeCompleted_FieldTitle);
            this.WaitForElement(Position_FieldTitle);
            this.WaitForElement(ResponsibleClinicianDetails_SectionTitle);
            this.WaitForElement(ResponsibleClinicianDetails_Instructions);
            this.WaitForElement(SeniorResponsibleClinicianWithOversight_FieldTitle);
            this.WaitForElement(ClinicianRegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeOfOversight_FieldTitle);
            this.WaitForElement(ClinicianPosition_FieldTitle);
            this.WaitForElement(Review_SectionTitle);
            this.WaitForElement(ReviewDNAR_FieldTitle);
            this.WaitForElement(CancellationOfDecision_SectionTitle);
            this.WaitForElement(CancelledDecision_FieldTitle);
            this.WaitForElement(Comments_SectionTitle);
            this.WaitForElement(ReviewAdditionalComments_FieldTitle);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Record of DNAR:\r\n" + TaskTitle);

            this.WaitForElementVisible(Person_LinkField);

            return this;
        }
        public PersonRecordsOfDNARRecordPage WaitForInactivePersonRecordsOfDNARRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
            this.WaitForElement(DateTimeOfDNAR_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(AgeStatus_FieldTitle);
            this.WaitForElement(DoNotAttemptResuscitation_SectionTitle);
            this.WaitForElement(DoNotAttemptResuscitation_Instructions);
            this.WaitForElement(AdultCPRDecision_FieldTitle);
            this.WaitForElement(RefusingTreatment_FieldTitle);
            this.WaitForElement(SummaryReason_SectionTitle);
            this.WaitForElement(SummaryReason_Instructions);
            this.WaitForElement(InterestHarmCPRBenefit_FieldTitle);
            this.WaitForElement(NaturalDeath_FieldTitle);
            this.WaitForElement(DiscussedDecision_FieldTitle);
            this.WaitForElement(DiscussedWithNextOfKin_FieldTitle);
            this.WaitForElement(NextOfKinsInformation_FieldTitle);
            this.WaitForElement(CPRRefused_FieldTitle);
            this.WaitForElement(Other_FieldTitle);
            this.WaitForElement(AdditionalInformation_FieldTitle);
            this.WaitForElement(NextOfKin_FieldTitle);
            this.WaitForElement(ProfessionalCompletedDNAROrderDetails_SectionTitle);
            this.WaitForElement(DNAROrderCompletedBy_FieldTitle);
            this.WaitForElement(RegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeCompleted_FieldTitle);
            this.WaitForElement(Position_FieldTitle);
            this.WaitForElement(ResponsibleClinicianDetails_SectionTitle);
            this.WaitForElement(ResponsibleClinicianDetails_Instructions);
            this.WaitForElement(SeniorResponsibleClinicianWithOversight_FieldTitle);
            this.WaitForElement(ClinicianRegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeOfOversight_FieldTitle);
            this.WaitForElement(ClinicianPosition_FieldTitle);
            this.WaitForElement(Review_SectionTitle);
            this.WaitForElement(ReviewDNAR_FieldTitle);
            this.WaitForElement(CancellationOfDecision_SectionTitle);
            this.WaitForElement(CancelledDecision_FieldTitle);
            this.WaitForElement(Comments_SectionTitle);
            this.WaitForElement(ReviewAdditionalComments_FieldTitle);



            WaitForElementNotVisible(saveButton, 5);
            WaitForElementNotVisible(saveAndCloseButton, 5);

            ValidateElementTextContainsText(pageHeader, "Record of DNAR:\r\n" + TaskTitle);

            ValidateElementDisabled(Person_LookupButton);
            ValidateElementDisabled(DateTimeOfDNAR_DateField);
            ValidateElementDisabled(DateTimeOfDNAR_TimeField);
            ValidateElementDisabled(responsibleTeam_LookupButton);
            ValidateElementDisabled(AgeStatus_Field);
            ValidateElementDisabled(AdultCPRDecision_Field);
            ValidateElementDisabled(RefusingTreatment_Field);
            ValidateElementDisabled(InterestHarmCPRBenefit_YesRadioButton);
            ValidateElementDisabled(InterestHarmCPRBenefit_NoRadioButton);
            ValidateElementDisabled(NaturalDeath_YesRadioButton);
            ValidateElementDisabled(NaturalDeath_NoRadioButton);
            ValidateElementDisabled(DiscussedDecision_Picklist);
            ValidateElementDisabled(DiscussedWithNextOfKin_Picklist);
            ValidateElementDisabled(NextOfKinsInformation_Field);
            ValidateElementDisabled(CPRRefused_YesRadioButton);
            ValidateElementDisabled(CPRRefused_NoRadioButton);
            ValidateElementDisabled(Other_YesRadioButton);
            ValidateElementDisabled(Other_NoRadioButton);
            ValidateElementDisabled(AdditionalInformation_Field);
            ValidateElementDisabled(NameOfNOK_LookupButton);
            ValidateElementDisabled(DNAROrderCompletedBy_LookupButton);
            ValidateElementDisabled(CompletedByRegistrationNumber_Field);
            ValidateElementDisabled(DateTimeCompleted_DateField);
            ValidateElementDisabled(DateTimeCompleted_TimeField);
            ValidateElementDisabled(Position_Field);
            ValidateElementDisabled(SeniorResponsibleClinicianWithOversight_LookupButton);
            ValidateElementDisabled(ClinicianRegistrationNumber_Field);
            ValidateElementDisabled(DateTimeOfOversight_DateField);
            ValidateElementDisabled(DateTimeOfOversight_TimeField);
            ValidateElementDisabled(ClinicianPosition_Field);
            ValidateElementDisabled(ReviewDNAR_Field);
            ValidateElementDisabled(CancelledDecision_YesRadioButton);
            ValidateElementDisabled(CancelledDecision_NoRadioButton);
            ValidateElementDisabled(ReviewAdditionalComments_Field);


            return this;
        }
        public PersonRecordsOfDNARRecordPage WaitForChildRecordsOfDNARRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(personTopBanner);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
            this.WaitForElement(DateTimeOfDNAR_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(AgeStatus_FieldTitle);
            this.WaitForElement(DoNotAttemptResuscitation_SectionTitle);
            this.WaitForElement(DoNotAttemptResuscitation_Instructions);
            this.WaitForElementNotVisible(AdultCPRDecision_FieldTitle, 3);
            this.WaitForElementNotVisible(RefusingTreatment_FieldTitle, 3);
            this.WaitForElement(ChildCPRDecision_FieldTitle);
            this.WaitForElement(SummaryReason_SectionTitle);
            this.WaitForElement(SummaryReason_Instructions);
            this.WaitForElement(InterestHarmCPRBenefit_FieldTitle);
            this.WaitForElement(NaturalDeath_FieldTitle);
            this.WaitForElement(DiscussedDecision_FieldTitle);
            this.WaitForElementNotVisible(DiscussedWithNextOfKin_FieldTitle, 3);
            this.WaitForElement(NextOfKinsInformation_FieldTitle);
            this.WaitForElement(CPRRefused_FieldTitle);
            this.WaitForElement(Other_FieldTitle);
            this.WaitForElement(AdditionalInformation_FieldTitle);
            this.WaitForElementNotVisible(NextOfKin_FieldTitle, 3);
            this.WaitForElement(DiscussedWithParent_FieldTitle);
            this.WaitForElement(Parent_FieldTitle);
            this.WaitForElement(ProfessionalCompletedDNAROrderDetails_SectionTitle);
            this.WaitForElement(DNAROrderCompletedBy_FieldTitle);
            this.WaitForElement(RegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeCompleted_FieldTitle);
            this.WaitForElement(Position_FieldTitle);
            this.WaitForElement(ResponsibleClinicianDetails_SectionTitle);
            this.WaitForElement(ResponsibleClinicianDetails_Instructions);
            this.WaitForElement(SeniorResponsibleClinicianWithOversight_FieldTitle);
            this.WaitForElement(ClinicianRegistrationNumber_FieldTitle);
            this.WaitForElement(DateTimeOfOversight_FieldTitle);
            this.WaitForElement(ClinicianPosition_FieldTitle);
            this.WaitForElement(Review_SectionTitle);
            this.WaitForElement(ReviewDNAR_FieldTitle);
            this.WaitForElement(CancellationOfDecision_SectionTitle);
            this.WaitForElement(CancelledDecision_FieldTitle);
            this.WaitForElement(Comments_SectionTitle);
            this.WaitForElement(ReviewAdditionalComments_FieldTitle);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Record of DNAR:\r\n" + TaskTitle);

            this.WaitForElementVisible(Person_LinkField);

            return this;
        }



        public PersonRecordsOfDNARRecordPage ValidatePersonTopBannerDNARActiveIconVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(personTopBanner_DNARActiveIcon);
            }
            else
            {
                WaitForElementNotVisible(personTopBanner_DNARActiveIcon, 3);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateResponsibleClinicianDetailsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleClinicianDetails_SectionTitle);
                WaitForElementVisible(ResponsibleClinicianDetails_Instructions);
                WaitForElementVisible(SeniorResponsibleClinicianWithOversight_FieldTitle);
                WaitForElementVisible(ClinicianRegistrationNumber_FieldTitle);
                WaitForElementVisible(DateTimeOfOversight_FieldTitle);
                WaitForElementVisible(ClinicianPosition_FieldTitle);
            }
            else 
            {
                WaitForElementNotVisible(ResponsibleClinicianDetails_SectionTitle, 3);
                WaitForElementNotVisible(ResponsibleClinicianDetails_Instructions, 3);
                WaitForElementNotVisible(SeniorResponsibleClinicianWithOversight_FieldTitle, 3);
                WaitForElementNotVisible(ClinicianRegistrationNumber_FieldTitle, 3);
                WaitForElementNotVisible(DateTimeOfOversight_FieldTitle, 3);
                WaitForElementNotVisible(ClinicianPosition_FieldTitle, 3);
            }

            return this;
        }


        public PersonRecordsOfDNARRecordPage ValidateDNAROrderCompletedByLookupButtonDisabled(bool ExpectDisabled)
        {
            if(ExpectDisabled)
                ValidateElementDisabled(DNAROrderCompletedBy_LookupButton);
            else
                ValidateElementNotDisabled(DNAROrderCompletedBy_LookupButton);

            return this;
        }



        public PersonRecordsOfDNARRecordPage SelectAgeStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(AgeStatus_Field);
            SelectPicklistElementByText(AgeStatus_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectAdultCPRDecision(string TextToSelect)
        {
            WaitForElementToBeClickable(AdultCPRDecision_Field);
            SelectPicklistElementByText(AdultCPRDecision_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectChildCPRDecision(string TextToSelect)
        {
            WaitForElementToBeClickable(ChildCPRDecision_Field);
            SelectPicklistElementByText(ChildCPRDecision_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectChildInvolvedInDecisionMaking(string TextToSelect)
        {
            WaitForElementToBeClickable(ChildInvolvedInDecisionMaking_Field);
            SelectPicklistElementByText(ChildInvolvedInDecisionMaking_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectParentsAgreedToDecision(string TextToSelect)
        {
            WaitForElementToBeClickable(ParentsAgreedToDecision_Field);
            SelectPicklistElementByText(ParentsAgreedToDecision_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectHasCourtMadeOrder(string TextToSelect)
        {
            WaitForElementToBeClickable(HasCourtMadeOrder_Field);
            SelectPicklistElementByText(HasCourtMadeOrder_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectRefusingTreatment(string TextToSelect)
        {
            WaitForElementToBeClickable(RefusingTreatment_Field);
            SelectPicklistElementByText(RefusingTreatment_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectDiscussedDecision(string TextToSelect)
        {
            WaitForElementToBeClickable(DiscussedDecision_Picklist);
            SelectPicklistElementByText(DiscussedDecision_Picklist, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectDiscussedWithNextOfKin(string TextToSelect)
        {
            WaitForElementToBeClickable(DiscussedWithNextOfKin_Picklist);
            SelectPicklistElementByText(DiscussedWithNextOfKin_Picklist, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectReviewDNAR(string TextToSelect)
        {
            WaitForElementToBeClickable(ReviewDNAR_Field);
            SelectPicklistElementByText(ReviewDNAR_Field, TextToSelect);

            return this;
        }
        public PersonRecordsOfDNARRecordPage SelectDiscussedWithParent(string TextToSelect)
        {
            WaitForElementToBeClickable(DiscussedWithParent_Picklist);
            SelectPicklistElementByText(DiscussedWithParent_Picklist, TextToSelect);

            return this;
        }




        public PersonRecordsOfDNARRecordPage InsertDateTimeOfDNAR(string Date, string Time)
        {
            WaitForElementToBeClickable(DateTimeOfDNAR_DateField);
            SendKeys(DateTimeOfDNAR_DateField, Date);
            WaitForElementToBeClickable(DateTimeOfDNAR_TimeField);
            SendKeys(DateTimeOfDNAR_TimeField, Time);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertNextOfKinsInformation(string TextToInsert)
        {
            WaitForElementToBeClickable(NextOfKinsInformation_Field);
            SendKeys(NextOfKinsInformation_Field, TextToInsert);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertReasonDecisionNotDiscussed(string TextToInsert)
        {
            WaitForElementToBeClickable(ReasonDecisionNotDiscussed_Field);
            SendKeys(ReasonDecisionNotDiscussed_Field, TextToInsert);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertAdditionalInformation(string TextToInsert)
        {
            SendKeys(AdditionalInformation_Field, TextToInsert);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertDateTimeCompleted(string Date, string Time)
        {
            SendKeys(DateTimeCompleted_DateField, Date);
            SendKeysWithoutClearing(DateTimeCompleted_DateField, Keys.Tab);
            SendKeys(DateTimeCompleted_TimeField, Time);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertDateTimeOfOversight(string Date, string Time)
        {
            SendKeys(DateTimeOfOversight_DateField, Date);
            SendKeysWithoutClearing(DateTimeOfOversight_DateField, Keys.Tab);
            SendKeys(DateTimeOfOversight_TimeField, Time);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertReviewAdditionalComments(string TextToInsert)
        {
            SendKeys(ReviewAdditionalComments_Field, TextToInsert);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertCancelledOn(string Date, string Time)
        {
            SendKeys(CancelledOn_DateField, Date);
            SendKeysWithoutClearing(CancelledOn_DateField, Keys.Tab);
            SendKeys(CancelledOn_TimeField, Time);

            return this;
        }
        public PersonRecordsOfDNARRecordPage InsertReviewDate(string TextToInsert)
        {
            SendKeys(ReviewDate_Field, TextToInsert);

            return this;
        }




        public PersonRecordsOfDNARRecordPage ClickPersonRemoveButton()
        {
            Click(Person_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickPersonLookupButton()
        {
            Click(Person_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickInterestHarmCPRBenefitYesRadioButton()
        {
            Click(InterestHarmCPRBenefit_YesRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickInterestHarmCPRBenefitNoRadioButton()
        {
            Click(InterestHarmCPRBenefit_NoRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickNaturalDeathYesRadioButton()
        {
            Click(NaturalDeath_YesRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickNaturalDeathNoRadioButton()
        {
            Click(NaturalDeath_NoRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCPRRefusedYesRadioButton()
        {
            Click(CPRRefused_YesRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCPRRefusedNoRadioButton()
        {
            Click(CPRRefused_NoRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickOtherYesRadioButton()
        {
            Click(Other_YesRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickOtherNoRadioButton()
        {
            Click(Other_NoRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickNameOfNOKRemoveButton()
        {
            Click(NameOfNOK_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickNameOfNOKLookupButton()
        {
            Click(NameOfNOK_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickDNAROrderCompletedByRemoveButton()
        {
            Click(DNAROrderCompletedBy_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickDNAROrderCompletedByLookupButton()
        {
            Click(DNAROrderCompletedBy_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickSeniorResponsibleClinicianWithOversightRemoveButton()
        {
            Click(SeniorResponsibleClinicianWithOversight_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickSeniorResponsibleClinicianWithOversightLookupButton()
        {
            Click(SeniorResponsibleClinicianWithOversight_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCancelledDecisionYesRadioButton()
        {
            Click(CancelledDecision_YesRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCancelledDecisionNoRadioButton()
        {
            Click(CancelledDecision_NoRadioButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickDeleteButtonOnInactiveRecord()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickPrintDNAR()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(printDNAR);
            Click(printDNAR);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCancelledByLookupButton()
        {
            Click(CancelledBy_LookupButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickCancelledByRemoveButton()
        {
            Click(CancelledBy_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickParentRemoveButton()
        {
            Click(Parent_RemoveButton);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ClickParentLookupButton()
        {
            Click(Parent_LookupButton);

            return this;
        }

        public PersonRecordsOfDNARRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(shareRecordButton);
            WaitForElementVisible(assignRecordButton);

            return this;
        }

        public PersonRecordsOfDNARRecordPage ValidateDateTimeOfDNARFieldText(string ExpectedDate)
        {
            ValidateElementValue(DateTimeOfDNAR_DateField, ExpectedDate);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDateTimeOfDNARFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeOfDNAR_DateField, ExpectedDate);
            ValidateElementValue(DateTimeOfDNAR_TimeField, ExpectedTime);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateAgeStatusSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AgeStatus_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateAdultCPRDecisionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AdultCPRDecision_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateChildCPRDecisionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ChildCPRDecision_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateChildInvolvedInDecisionMakingSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ChildInvolvedInDecisionMaking_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateParentsAgreedToDecisionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ParentsAgreedToDecision_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateHasCourtMadeOrderSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(HasCourtMadeOrder_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateRefusingTreatmentSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(RefusingTreatment_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDiscussedDecisionSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(DiscussedDecision_Picklist, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDiscussedWithNextOfKinSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(DiscussedWithNextOfKin_Picklist, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateNextOfKinsInformationFieldText(string ExpectedText)
        {
            ValidateElementText(NextOfKinsInformation_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateAdditionalInformationFieldText(string ExpectedText)
        {
            ValidateElementText(AdditionalInformation_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCompletedByRegistrationNumberFieldText(string ExpectedText)
        {
            ValidateElementValue(CompletedByRegistrationNumber_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDateTimeCompletedFieldText(string ExpectedDate)
        {
            ValidateElementValue(DateTimeCompleted_DateField, ExpectedDate);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDateTimeCompletedFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeCompleted_DateField, ExpectedDate);
            ValidateElementValue(DateTimeCompleted_TimeField, ExpectedTime);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidatePositionFieldText(string ExpectedText)
        {
            ValidateElementValue(Position_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateClinicianRegistrationNumberFieldText(string ExpectedText)
        {
            ValidateElementValue(ClinicianRegistrationNumber_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDateTimeOfOversightFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeOfOversight_DateField, ExpectedDate);
            ValidateElementValue(DateTimeOfOversight_TimeField, ExpectedTime);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateClinicianPositionFieldText(string ExpectedText)
        {
            ValidateElementValue(ClinicianPosition_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateReviewDNARSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ReviewDNAR_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateReviewDateValue(string ExpectedText)
        {
            ValidateElementValue(ReviewDate_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateReviewAdditionalCommentsFieldText(string ExpectedText)
        {
            ValidateElementText(ReviewAdditionalComments_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledByRegistrationNumberFieldText(string ExpectedText)
        {
            ValidateElementText(CancelledByRegistrationNumber_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledOnFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(CancelledOn_DateField, ExpectedDate);
            ValidateElementValue(CancelledOn_TimeField, ExpectedTime);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(CancelledBy_LinkField, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledByPositionFieldText(string ExpectedText)
        {
            ValidateElementText(CancelledByPosition_Field, ExpectedText);
            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDiscussedWithParent(string ExpectedText)
        {
            ValidatePicklistSelectedText(DiscussedWithParent_Picklist, ExpectedText);
            return this;
        }



        public PersonRecordsOfDNARRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Person_LinkField, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateParentLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Parent_LinkField, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateNameOfNOKLinkFieldText(string ExpectedText)
        {
            ValidateElementText(NameOfNOK_LinkField, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDNAROrderCompletedByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(DNAROrderCompletedBy_LinkField, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateSeniorResponsibleClinicianWithOversightLinkFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(SeniorResponsibleClinicianWithOversight_LinkField);
            MoveToElementInPage(SeniorResponsibleClinicianWithOversight_LinkField);
            ValidateElementText(SeniorResponsibleClinicianWithOversight_LinkField, ExpectedText);

            return this;
        }

        public PersonRecordsOfDNARRecordPage ValidateSeniorResponsibleClinicianWithOversightFieldText(string ExpectedText)
        {
            WaitForElementVisible(SeniorResponsibleClinicianWithOversight_Field);
            MoveToElementInPage(SeniorResponsibleClinicianWithOversight_Field);
            ValidateElementText(SeniorResponsibleClinicianWithOversight_Field, ExpectedText);

            return this;
        }



        public PersonRecordsOfDNARRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            WaitForElementVisible(messageArea);
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidatePersonErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Person_FieldErrorLabel);
            ValidateElementText(Person_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateAdultCPRDecisionErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(AdultCPRDecision_FieldErrorLabel);
            ValidateElementText(AdultCPRDecision_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateChildCPRDecisionErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ChildCPRDecision_FieldErrorLabel);
            ValidateElementText(ChildCPRDecision_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDiscussedDecisionErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(DiscussedDecision_FieldErrorLabel);
            ValidateElementText(DiscussedDecision_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateReviewDNARErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ReviewDNAR_FieldErrorLabel);
            ValidateElementText(ReviewDNAR_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateReviewDateErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(ReviewDate_FieldErrorLabel);
            ValidateElementText(ReviewDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(responsibleTeam_FieldErrorLabel);
            ValidateElementText(responsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateDNAROrderCompletedByErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(DNAROrderCompletedBy_FieldErrorLabel);
            ValidateElementText(DNAROrderCompletedBy_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateParentErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Parent_FieldErrorLabel);
            ValidateElementText(Parent_FieldErrorLabel, ExpectedText);

            return this;
        }




        public PersonRecordsOfDNARRecordPage ValidateInterestHarmCPRBenefitYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(InterestHarmCPRBenefit_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(InterestHarmCPRBenefit_YesRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateInterestHarmCPRBenefitNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(InterestHarmCPRBenefit_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(InterestHarmCPRBenefit_NoRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateNaturalDeathYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(NaturalDeath_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(NaturalDeath_YesRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateNaturalDeathNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(NaturalDeath_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(NaturalDeath_NoRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCPRRefusedYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(CPRRefused_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CPRRefused_YesRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCPRRefusedNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(CPRRefused_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CPRRefused_NoRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateOtherYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(Other_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Other_YesRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateOtherNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(Other_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Other_NoRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledDecisionYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(CancelledDecision_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CancelledDecision_YesRadioButton);
            }

            return this;
        }
        public PersonRecordsOfDNARRecordPage ValidateCancelledDecisionNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(CancelledDecision_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CancelledDecision_NoRadioButton);
            }

            return this;
        }



    }
}
