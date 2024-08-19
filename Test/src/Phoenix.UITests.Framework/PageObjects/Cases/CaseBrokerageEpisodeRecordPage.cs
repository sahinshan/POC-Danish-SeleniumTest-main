using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodeRecordPage : CommonMethods
    {
        public CaseBrokerageEpisodeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisode')]");
        readonly By pasueEpisodeIFrame = By.Id("iframe_BrokerageEpisodePausePeriodDialog");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By Refresh_Button = By.Id("TI_RefreshButton");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");


        readonly By CopyButton = By.Id("TI_CopyButton");

        readonly By PauseButton = By.Id("TI_PauseButton");
        readonly By RestartButton = By.Id("TI_RestartButton");
        readonly By moreoptions = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By ActivateButton = By.Id("TI_ActivateButton");

        readonly By CancelledStatus = By.XPath("//*[@id='CWField_statusid']/option[@value='7']");



        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By AttachmentsLeftMenuLink = By.XPath("//*[@id='CWNavItem_Attachments']");
        readonly By BrokerageEpisodeOffersLeftMenuLink = By.XPath("//*[@id='CWNavItem_BrokerageEpisodeOffers']");
        readonly By BrokerageEpisodeEscalationsLeftMenuLink = By.XPath("//*[@id='CWNavItem_BrokerageEpisodeEscalation']");


        readonly By BrokerageEpisodePauseRecordsLeftMenuLink = By.Id("CWNavItem_BrokerageEpisodePauseRecords");


        readonly By ServiceProvisionLeftMenuLink = By.Id("CWNavItem_ServiceProvisions");



        #endregion

        #region Fields

        readonly By Case_FieldHeader = By.Id("CWLabelHolder_caseid");
        readonly By Case_LinkField = By.XPath("//*[@id='CWField_caseid_Link']");
        readonly By Case_LookUpButton = By.Id("CWLookupBtn_caseid");
        readonly By Case_RemoveButton = By.Id("CWClearLookup_caseid");
        readonly By Case_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_caseid']/label/span");

        readonly By person_FieldHeader = By.Id("CWLabelHolder_personid");
        readonly By person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By person_LookUpButton = By.Id("CWLookupBtn_personid");
        readonly By person_RemoveButton = By.Id("CWClearLookup_personid");
        readonly By person_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_personid']/label/span");

        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        readonly By Status_Field = By.Id("CWField_statusid");
        readonly By Status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergicreactionStatusid']/label/span");

        readonly By RequestReceivedDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_requestreceiveddatetime']/label");
        readonly By RequestReceivedDateTime_DateField = By.Id("CWField_requestreceiveddatetime");
        readonly By RequestReceivedDateTime_TimeField = By.Id("CWField_requestreceiveddatetime_Time");
        readonly By RequestReceivedDateTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_requestreceiveddatetime']/label/span");

        readonly By SourceOfRequest_FieldHeader = By.Id("CWLabelHolder_brokeragerequestsourceid");
        readonly By SourceOfRequest_LinkField = By.XPath("//*[@id='CWField_brokeragerequestsourceid_Link']");
        readonly By SourceOfRequest_LookUpButton = By.Id("CWLookupBtn_brokeragerequestsourceid");
        readonly By SourceOfRequest_RemoveButton = By.Id("CWClearLookup_brokeragerequestsourceid");
        readonly By SourceOfRequest_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokeragerequestsourceid']/label/span");

        readonly By Priority_FieldHeader = By.Id("CWLabelHolder_brokerageepisodepriorityid");
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_brokerageepisodepriorityid_Link']");
        readonly By Priority_LookUpButton = By.Id("CWLookupBtn_brokerageepisodepriorityid");
        readonly By Priority_RemoveButton = By.Id("CWClearLookup_brokerageepisodepriorityid");
        readonly By Priority_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokerageepisodepriorityid']/label/span"); 

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By ResponsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleUser_LookUpButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By ResponsibleUser_RemoveButton = By.Id("CWClearLookup_responsibleuserid");

        readonly By TargetDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_targetdatetime']/label");
        readonly By TargetDateTime_DateField = By.Id("CWField_targetdatetime");
        readonly By TargetDateTime_TimeField = By.Id("CWField_targetdatetime_Time");

        readonly By TrackingStatus_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageepisodetrackingstatusid']/label");
        readonly By TrackingStatus_LinkField = By.XPath("//*[@id='CWField_brokerageepisodetrackingstatusid_Link']");
        readonly By TrackingStatus_Field = By.Id("CWField_brokerageepisodetrackingstatusid");
        readonly By TrackingStatus_LookUpButton = By.Id("CWLookupBtn_brokerageepisodetrackingstatusid");
        readonly By TrackingStatus_RemoveButton = By.Id("CWClearLookup_brokerageepisodetrackingstatusid");

        readonly By SourcedDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_sourceddatetime']/label");
        readonly By SourcedDateTime_DateField = By.Id("CWField_sourceddatetime");
        readonly By SourcedDateTime_TimeField = By.Id("CWField_sourceddatetime_Time");

        readonly By RelatedAssessment_FieldHeader = By.XPath("//*[@id='CWLabelHolder_relatedassessmentid']/label");
        readonly By RelatedAssessment_LinkField = By.XPath("//*[@id='CWField_relatedassessmentid_Link']");
        readonly By RelatedAssessment_LookUpButton = By.Id("CWLookupBtn_relatedassessmentid");
        readonly By RelatedAssessment_RemoveButton = By.Id("CWClearLookup_relatedassessmentid");






        readonly By DateTimeApproved_FieldHeader = By.XPath("//*[@id='CWLabelHolder_approveddatetime']/label");
        readonly By DateTimeApproved_DateField = By.Id("CWField_approveddatetime");
        readonly By DateTimeApproved_TimeField = By.Id("CWField_approveddatetime_Time");
        readonly By DateTimeApproved_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_approveddatetime']/div/div/label/span");

        readonly By Approver_FieldHeader = By.XPath("//*[@id='CWLabelHolder_approverid']/label");
        readonly By Approver_LinkField = By.XPath("//*[@id='CWField_approverid_Link']");
        readonly By Approver_LookUpButton = By.Id("CWLookupBtn_approverid");
        readonly By Approver_RemoveButton = By.Id("CWClearLookup_approverid");
        readonly By Approver_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_approverid']/label/span");

        readonly By ApproverComments_FieldHeader = By.XPath("//*[@id='CWLabelHolder_approvalcomments']/label");
        readonly By ApproverComments_Field = By.XPath("//*[@id='CWField_approvalcomments']");
        readonly By ApproverComments_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_approvalcomments']/label/span");

        readonly By DateTimeApprovalRejected_FieldHeader = By.XPath("//*[@id='CWLabelHolder_approvalrejecteddatetime']/label");
        readonly By DateTimeApprovalRejected_DateField = By.Id("CWField_approvalrejecteddatetime");
        readonly By DateTimeApprovalRejected_TimeField = By.Id("CWField_approvalrejecteddatetime_Time");
        readonly By DateTimeApprovalRejected_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_approvalrejecteddatetime']/div/div/label/span");






        readonly By BrokerageResponsibleForCommunications_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageresponsibleforcontact']/label");
        readonly By BrokerageResponsibleForCommunications_YesRadioButton = By.XPath("//*[@id='CWField_brokerageresponsibleforcontact_1']");
        readonly By BrokerageResponsibleForCommunications_NoRadioButton = By.XPath("//*[@id='CWField_brokerageresponsibleforcontact_0']");

        readonly By ContactRegisteredInCareDirector_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactregisteredincaredirector']/label");
        readonly By ContactRegisteredInCareDirector_YesRadioButton = By.XPath("//*[@id='CWField_contactregisteredincaredirector_1']");
        readonly By ContactRegisteredInCareDirector_NoRadioButton = By.XPath("//*[@id='CWField_contactregisteredincaredirector_0']");

        readonly By ContactPerson_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactpersonid']/label");
        readonly By ContactPerson_LinkField = By.XPath("//*[@id='CWField_contactpersonid_Link']");
        readonly By ContactPerson_LookUpButton = By.Id("CWLookupBtn_contactpersonid");
        readonly By ContactPerson_RemoveButton = By.Id("CWClearLookup_contactpersonid");

        readonly By ContactName_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactname']/label");
        readonly By ContactName_Field = By.XPath("//*[@id='CWField_contactname']");

        readonly By RelationshipToCitizen_FieldHeader = By.XPath("//*[@id='CWLabelHolder_relationshiptocitizen']/label");
        readonly By RelationshipToCitizen_Field = By.XPath("//*[@id='CWField_relationshiptocitizen']");

        readonly By ContactAddress_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactaddress']/label");
        readonly By ContactAddress_Field = By.XPath("//*[@id='CWField_contactaddress']");

        readonly By ContactPhoneNumber_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactphonenumber']/label");
        readonly By ContactPhoneNumber_Field = By.XPath("//*[@id='CWField_contactphonenumber']");





        readonly By ReferralType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokeragereferraltypeid']/label");
        readonly By ReferralType_Field = By.XPath("//*[@id='CWField_brokeragereferraltypeid']");

        readonly By InternalReferrer_FieldHeader = By.XPath("//*[@id='CWLabelHolder_internalreferredbyid']/label");
        readonly By InternalReferrer_LinkField = By.XPath("//*[@id='CWField_internalreferredbyid_Link']");
        readonly By InternalReferrer_LookUpButton = By.Id("CWLookupBtn_internalreferredbyid");
        readonly By InternalReferrer_RemoveButton = By.Id("CWClearLookup_internalreferredbyid");

        readonly By ExistingCarePackage_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageexistingcarepackageid']/label");
        readonly By ExistingCarePackage_LinkField = By.XPath("//*[@id='CWField_brokerageexistingcarepackageid_Link']");
        readonly By ExistingCarePackage_LookUpButton = By.Id("CWLookupBtn_brokerageexistingcarepackageid");
        readonly By ExistingCarePackage_RemoveButton = By.Id("CWClearLookup_brokerageexistingcarepackageid");

        readonly By Section117EligibleNeeds_FieldHeader = By.XPath("//*[@id='CWLabelHolder_section117eligibleneeds']/label");
        readonly By Section117EligibleNeeds_YesRadioButton = By.XPath("//*[@id='CWField_section117eligibleneeds_1']");
        readonly By Section117EligibleNeeds_NoRadioButton = By.XPath("//*[@id='CWField_section117eligibleneeds_0']");

        readonly By TemporaryCare_FieldHeader = By.XPath("//*[@id='CWLabelHolder_temporarycare']/label");
        readonly By TemporaryCare_YesRadioButton = By.XPath("//*[@id='CWField_temporarycare_1']");
        readonly By TemporaryCare_NoRadioButton = By.XPath("//*[@id='CWField_temporarycare_0']");

        readonly By TypeOFCarePackage_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokeragecarepackagetypeid']/label");
        readonly By TypeOFCarePackage_LinkField = By.XPath("//*[@id='CWField_brokeragecarepackagetypeid_Link']");
        readonly By TypeOFCarePackage_LookUpButton = By.Id("CWLookupBtn_brokeragecarepackagetypeid");
        readonly By TypeOFCarePackage_RemoveButton = By.Id("CWClearLookup_brokeragecarepackagetypeid");

        readonly By NumberOfProvidersContacted_FieldHeader = By.XPath("//*[@id='CWLabelHolder_numberofproviderscontacted']/label");
        readonly By NumberOfProvidersContacted_Field = By.Id("CWField_numberofproviderscontacted");

        readonly By NumberOfOffersRecieved_FieldHeader = By.XPath("//*[@id='CWLabelHolder_numberofoffersreceived']/label");
        readonly By NumberOfOffersRecieved_Field = By.Id("CWField_numberofoffersreceived");

        readonly By ScheduleRequired_FieldHeader = By.XPath("//*[@id='CWLabelHolder_schedulerequired']/label");
        readonly By ScheduleRequired_YesRadioButton = By.XPath("//*[@id='CWField_schedulerequired_1']");
        readonly By ScheduleRequired_NoRadioButton = By.XPath("//*[@id='CWField_schedulerequired_0']");

        readonly By NumberOfCarersPerVisit_FieldHeader = By.XPath("//*[@id='CWLabelHolder_numberofcarerspervisit']/label");
        readonly By NumberOfCarersPerVisit_Field = By.XPath("//*[@id='CWField_numberofcarerspervisit']");

        readonly By DeferredToCommissioning_FieldHeader = By.XPath("//*[@id='CWLabelHolder_deferredtocommissioning']/label");
        readonly By DeferredToCommissioning_YesRadioButton = By.XPath("//*[@id='CWField_deferredtocommissioning_1']");
        readonly By DeferredToCommissioning_NoRadioButton = By.XPath("//*[@id='CWField_deferredtocommissioning_0']");

        readonly By Notes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label");
        readonly By Notes_Field = By.Id("CWField_notes");

        readonly By ServiceElement1_FieldHeader = By.XPath("//*[@id='CWLabelHolder_serviceelement1id']/label");
        readonly By ServiceElement1_LinkField = By.XPath("//*[@id='CWField_serviceelement1id_Link']");
        readonly By ServiceElement1_LookUpButton = By.Id("CWLookupBtn_serviceelement1id");
        readonly By ServiceElement1_RemoveButton = By.Id("CWClearLookup_serviceelement1id");

        readonly By ServiceElement1_Field = By.Id("CWField_serviceelement1id");

        readonly By ServiceElement1_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_serviceelement1id']/label/span");


        readonly By ServiceElement2_FieldHeader = By.XPath("//*[@id='CWLabelHolder_serviceelement2id']/label");
        readonly By ServiceElement2_LinkField = By.XPath("//*[@id='CWField_serviceelement2id_Link']");
        readonly By ServiceElement2_LookUpButton = By.Id("CWLookupBtn_serviceelement2id");
        readonly By ServiceElement2_RemoveButton = By.Id("CWClearLookup_serviceelement2id");

        readonly By ServiceElement2_Field = By.Id("CWField_serviceelement2id");
        readonly By ServiceElement2_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_serviceelement2id']/label/span");


        readonly By PlannedStartDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_plannedstartdate']/label");
        readonly By PlannedStartDate_Field = By.Id("CWField_plannedstartdate");
        readonly By PlannedStartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_plannedstartdate']/label/span");

        readonly By PlannedEndDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_plannedenddate']/label");
        readonly By PlannedEndDate_Field = By.Id("CWField_plannedenddate");

        readonly By FinanceClientCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_financeclientcategoryid']/label");
        readonly By FinanceClientCategory_LinkField = By.XPath("//*[@id='CWField_financeclientcategoryid_Link']");
        readonly By FinanceClientCategory_LookUpButton = By.Id("CWLookupBtn_financeclientcategoryid");
        readonly By FinanceClientCategory_RemoveButton = By.Id("CWClearLookup_financeclientcategoryid");
        readonly By FinanceClientCategory_Field = By.Id("CWField_financeclientcategoryid");

        readonly By ContractType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contracttypeid']/label");
        readonly By ContractType_Field = By.Id("CWField_contracttypeid");

        readonly By DateTimeDeferred_FieldHeader = By.XPath("//*[@id='CWLabelHolder_deferreddatetime']/label");
        readonly By DateTimeDeferred_DateField = By.Id("CWField_deferreddatetime");
        readonly By DateTimeDeferred_TimeField = By.Id("CWField_deferreddatetime_Time");






        readonly By MondayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutesmonday']/label");
        readonly By MondayHealthMinutes_Field = By.Id("CWField_healthminutesmonday");

        readonly By TuesdayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutestuesday']/label");
        readonly By TuesdayHealthMinutes_Field = By.Id("CWField_healthminutestuesday");
                    
        readonly By WednesdayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminuteswednesday']/label");
        readonly By WednesdayHealthMinutes_Field = By.Id("CWField_healthminuteswednesday");
                    
        readonly By ThursdayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutesthursday']/label");
        readonly By ThursdayHealthMinutes_Field = By.Id("CWField_healthminutesthursday");
                    
        readonly By FridayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutesfriday']/label");
        readonly By FridayHealthMinutes_Field = By.Id("CWField_healthminutesfriday");
                    
        readonly By SaturdayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutessaturday']/label");
        readonly By SaturdayHealthMinutes_Field = By.Id("CWField_healthminutessaturday");
                    
        readonly By SundayHealthMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutessunday']/label");
        readonly By SundayHealthMinutes_Field = By.Id("CWField_healthminutessunday");

        readonly By TotalHealthMinutesPerWeek_FieldHeader = By.XPath("//*[@id='CWLabelHolder_healthminutestotal']/label");
        readonly By TotalHealthMinutesPerWeek_Field = By.Id("CWField_healthminutestotal");





        readonly By MondaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutesmonday']/label");
        readonly By MondaySocialCareMinutes_Field = By.Id("CWField_socialcareminutesmonday");
                          
        readonly By TuesdaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutestuesday']/label");
        readonly By TuesdaySocialCareMinutes_Field = By.Id("CWField_socialcareminutestuesday");
                          
        readonly By WednesdaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminuteswednesday']/label");
        readonly By WednesdaySocialCareMinutes_Field = By.Id("CWField_socialcareminuteswednesday");
                          
        readonly By ThursdaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutesthursday']/label");
        readonly By ThursdaySocialCareMinutes_Field = By.Id("CWField_socialcareminutesthursday");
                          
        readonly By FridaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutesfriday']/label");
        readonly By FridaySocialCareMinutes_Field = By.Id("CWField_socialcareminutesfriday");
                          
        readonly By SaturdaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutessaturday']/label");
        readonly By SaturdaySocialCareMinutes_Field = By.Id("CWField_socialcareminutessaturday");
                          
        readonly By SundaySocialCareMinutes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutessunday']/label");
        readonly By SundaySocialCareMinutes_Field = By.Id("CWField_socialcareminutessunday");
                          
        readonly By TotalSocialCareMinutesPerWeek_FieldHeader = By.XPath("//*[@id='CWLabelHolder_socialcareminutestotal']/label");
        readonly By TotalSocialCareMinutesPerWeek_Field = By.Id("CWField_socialcareminutestotal");





        readonly By Monday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitmonday']/label");
        readonly By Monday_Field = By.Id("CWField_minutespervisitmonday");

        readonly By Tuesday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisittuesday']/label");
        readonly By Tuesday_Field = By.Id("CWField_minutespervisittuesday");

        readonly By Wednesday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitwednesday']/label");
        readonly By Wednesday_Field = By.Id("CWField_minutespervisitwednesday");

        readonly By Thursday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitthursday']/label");
        readonly By Thursday_Field = By.Id("CWField_minutespervisitthursday");

        readonly By Friday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitfriday']/label");
        readonly By Friday_Field = By.Id("CWField_minutespervisitfriday");

        readonly By Saturday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitsaturday']/label");
        readonly By Saturday_Field = By.Id("CWField_minutespervisitsaturday");

        readonly By Sunday_FieldHeader = By.XPath("//*[@id='CWLabelHolder_minutespervisitsunday']/label");
        readonly By Sunday_Field = By.Id("CWField_minutespervisitsunday");

        readonly By TotalPerWeek_FieldHeader = By.XPath("//*[@id='CWLabelHolder_totalminutesperweek']/label");
        readonly By TotalMinutesPerWeek_Field = By.Id("CWField_totalminutesperweek");







        readonly By FundingArrangementType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokeragefundingarrangementtypeid']/label");
        readonly By FundingArrangementType_LinkField = By.XPath("//*[@id='CWField_brokeragefundingarrangementtypeid_Link']");
        readonly By FundingArrangementType_LookUpButton = By.Id("CWLookupBtn_brokeragefundingarrangementtypeid");
        readonly By FundingArrangementType_RemoveButton = By.Id("CWClearLookup_brokeragefundingarrangementtypeid");

        readonly By RequestedLocalAuthorityFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_requestedlafundingperweek']/label");
        readonly By RequestedLocalAuthorityFunding_Field = By.Id("CWField_requestedlafundingperweek");
        readonly By RequestedLocalAuthorityFunding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_requestedlafundingperweek']/div/label/span");

        readonly By AgreedLocalAuthorityFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_agreedlafundingperweek']/label");
        readonly By AgreedLocalAuthorityFunding_Field = By.Id("CWField_agreedlafundingperweek");
        readonly By AgreedLocalAuthorityFunding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_agreedlafundingperweek']/div/label/span");

        readonly By ThirdPartyTopUpFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_thirdpartytopupfundingperweek']/label");
        readonly By ThirdPartyTopUpFunding_Field = By.Id("CWField_thirdpartytopupfundingperweek");
        readonly By ThirdPartyTopUpFunding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_thirdpartytopupfundingperweek']/div/label/span");

        readonly By ContinuingHealthcareFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_continuinghealthcarefundingperweek']/label");
        readonly By ContinuingHealthcareFunding_Field = By.Id("CWField_continuinghealthcarefundingperweek");
        readonly By ContinuingHealthcareFunding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_continuinghealthcarefundingperweek']/div/label/span");

        readonly By FundedNursingCare_FieldHeader = By.XPath("//*[@id='CWLabelHolder_fundednursingcareperweek']/label");
        readonly By FundedNursingCare_Field = By.Id("CWField_fundednursingcareperweek");
        readonly By FundedNursingCare_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_fundednursingcareperweek']/div/label/span");

        readonly By OtherFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_otherfundingperweek']/label");
        readonly By OtherFunding_Field = By.Id("CWField_otherfundingperweek");
        readonly By OtherFunding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_otherfundingperweek']/div/label/span");

        readonly By TotalFunding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_totalfundingperweek']/label");
        readonly By TotalFunding_Field = By.Id("CWField_totalfundingperweek");








        readonly By RequestRejectionDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_requestrejectiondatetime']/label");
        readonly By RequestRejectionDateTime_DateField = By.Id("CWField_requestrejectiondatetime");
        readonly By RequestRejectionDateTime_TimeField = By.Id("CWField_requestrejectiondatetime_Time");

        readonly By RequestRejectionReason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageepisoderejectionreasonid']/label");
        readonly By RequestRejectionReason_LinkField = By.XPath("//*[@id='CWField_brokerageepisoderejectionreasonid_Link']");
        readonly By RequestRejectionReason_LookUpButton = By.Id("CWLookupBtn_brokerageepisoderejectionreasonid");
        readonly By RequestRejectionReason_RemoveButton = By.Id("CWClearLookup_brokerageepisoderejectionreasonid");





        readonly By CancellationDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_cancellationdatetime']/label");
        readonly By CancellationDateTime_DateField = By.Id("CWField_cancellationdatetime");
        readonly By CancellationDateTime_TimeField = By.Id("CWField_cancellationdatetime_Time");

        readonly By CancellationReason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageepisodecancellationreasonid']/label");
        readonly By CancellationReason_LinkField = By.XPath("//*[@id='CWField_brokerageepisodecancellationreasonid_Link']");
        readonly By CancellationReason_LookUpButton = By.Id("CWLookupBtn_brokerageepisodecancellationreasonid");
        readonly By CancellationReason_RemoveButton = By.Id("CWClearLookup_brokerageepisodecancellationreasonid");




        #endregion

        readonly By PauseDate_Field = By.Id("CWPauseDate");
        readonly By PauseTime_Field = By.Id("CWPauseTime");
        readonly By PasueReasonLookupButton = By.Id("CWLookupBtn_CWReasonId");
        readonly By Save_Button = By.Id("CWSave");

        readonly By cancel_Button = By.Id("CWCancel");


        readonly By otherReason_Field = By.Id("CWField_cwotherreason");
        readonly By RestartDate_Field = By.Id("CWRestartDate");

        public CaseBrokerageEpisodeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }


        public CaseBrokerageEpisodeRecordPage ClickPasueEpisodeSaveButton()
        {
            WaitForElementToBeClickable(Save_Button);
            Click(Save_Button);
            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickPasueEpisodeCancelButton()
        {
            WaitForElementToBeClickable(cancel_Button);
            Click(cancel_Button);
            return this;
        }



        public CaseBrokerageEpisodeRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickAdditionalItemsButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickCopyButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(CopyButton);
            Click(CopyButton);
            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ClickActivateButton()
        {

            WaitForElementVisible(ActivateButton);
            Click(ActivateButton);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ClickRefreshButton()
        {

            WaitForElementVisible(Refresh_Button);
            Click(Refresh_Button);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ClickPasueEpisodeButton()
        {
            WaitForElementVisible(moreoptions);
            Click(moreoptions);
            WaitForElementVisible(PauseButton);
            Click(PauseButton);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ClickRestartEpisodeButton()
        {
            WaitForElementVisible(moreoptions);
            Click(moreoptions);
            WaitForElementVisible(RestartButton);
            Click(RestartButton);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ValidateCopyButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CopyButton);
            }
            else
            {
                WaitForElementNotVisible(CopyButton, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateBrokerageOffersLeftMenuVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            if(ExpectVisible)
            {
                WaitForElementVisible(BrokerageEpisodeOffersLeftMenuLink);
            }else
            {
                WaitForElementNotVisible(BrokerageEpisodeOffersLeftMenuLink, 3);
            }

            Click(LeftMenuButton);

            return this;
        }



        public CaseBrokerageEpisodeRecordPage NavigateToBrokerageOffersSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);
            
            WaitForElementVisible(BrokerageEpisodeOffersLeftMenuLink);
            Click(BrokerageEpisodeOffersLeftMenuLink);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage NavigateToServiceProvisionSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            MoveToElementInPage(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(ServiceProvisionLeftMenuLink);
            MoveToElementInPage(ServiceProvisionLeftMenuLink);
            Click(ServiceProvisionLeftMenuLink);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage NavigateToBrokerageEscalationsSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementVisible(BrokerageEpisodeEscalationsLeftMenuLink);
            Click(BrokerageEpisodeEscalationsLeftMenuLink);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage NavigateToAttachmentsSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementVisible(AttachmentsLeftMenuLink);
            Click(AttachmentsLeftMenuLink);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage NavigateToBrokeragePauseEpisodeSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementVisible(BrokerageEpisodePauseRecordsLeftMenuLink);
            Click(BrokerageEpisodePauseRecordsLeftMenuLink);

            return this;
        }




        public CaseBrokerageEpisodeRecordPage WaitForCaseBrokerageEpisodeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementToBeClickable(Back_Button);

            WaitForElement(Case_FieldHeader);
            WaitForElement(person_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(RequestReceivedDateTime_FieldHeader);
            WaitForElement(SourceOfRequest_FieldHeader);
            WaitForElement(SourceOfRequest_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(TargetDateTime_FieldHeader);
            WaitForElement(TrackingStatus_FieldHeader);
            WaitForElement(SourcedDateTime_FieldHeader);
            WaitForElement(RelatedAssessment_FieldHeader);

            WaitForElement(BrokerageResponsibleForCommunications_FieldHeader);
            WaitForElement(ContactRegisteredInCareDirector_FieldHeader);
            WaitForElement(ContactPerson_FieldHeader);

            WaitForElement(ReferralType_FieldHeader);
            WaitForElement(ExistingCarePackage_FieldHeader);
            WaitForElement(Section117EligibleNeeds_FieldHeader);
            WaitForElement(TemporaryCare_FieldHeader);
            WaitForElement(TypeOFCarePackage_FieldHeader);
            WaitForElement(NumberOfProvidersContacted_FieldHeader);
            WaitForElement(NumberOfOffersRecieved_FieldHeader);
            WaitForElement(ScheduleRequired_FieldHeader);
            WaitForElement(DeferredToCommissioning_FieldHeader);
            WaitForElement(Notes_FieldHeader);
            WaitForElement(ServiceElement1_FieldHeader);
            WaitForElement(ServiceElement2_FieldHeader);
            WaitForElement(PlannedStartDate_FieldHeader);
            WaitForElement(PlannedEndDate_FieldHeader);
            WaitForElement(FinanceClientCategory_FieldHeader);
            WaitForElement(ContractType_FieldHeader);

            WaitForElement(FundingArrangementType_FieldHeader);
            WaitForElement(RequestedLocalAuthorityFunding_FieldHeader);
            WaitForElement(AgreedLocalAuthorityFunding_FieldHeader);
            WaitForElement(ThirdPartyTopUpFunding_FieldHeader);
            WaitForElement(ContinuingHealthcareFunding_FieldHeader);
            WaitForElement(FundedNursingCare_FieldHeader);
            WaitForElement(OtherFunding_FieldHeader);
            WaitForElement(TotalFunding_FieldHeader);
            
            WaitForElement(RequestRejectionDateTime_FieldHeader);
            WaitForElement(RequestRejectionReason_FieldHeader);

            WaitForElement(CancellationDateTime_FieldHeader);
            WaitForElement(CancellationReason_FieldHeader);

            

            return this;
        }

        public CaseBrokerageEpisodeRecordPage WaitForPasueBrokerageEpisodeRecordLookupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pasueEpisodeIFrame);
            SwitchToIframe(pasueEpisodeIFrame);

                      

            return this;
        }

       
        public CaseBrokerageEpisodeRecordPage WaitForCaseBrokerageEpisodeRecordPageToLoad(string PageTitle)
        {
            WaitForCaseBrokerageEpisodeRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Episode:\r\n" + PageTitle);

            return this;
        }


       



        public CaseBrokerageEpisodeRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateCaseFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Case_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Case_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePersonFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(person_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(person_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateStatusFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Status_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Status_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestReceivedDateTimeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequestReceivedDateTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(RequestReceivedDateTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSourceOfRequestFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SourceOfRequest_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(SourceOfRequest_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePriorityFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Priority_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Priority_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactPersonFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ContactPerson_FieldHeader);
                WaitForElementVisible(ContactPerson_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(ContactPerson_FieldHeader, 3);
                WaitForElementNotVisible(ContactPerson_LookUpButton, 3);
                WaitForElementNotVisible(ContactPerson_RemoveButton, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestedLocalAuthorityFundingFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequestedLocalAuthorityFunding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(RequestedLocalAuthorityFunding_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateAgreedLocalAuthorityFundingFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AgreedLocalAuthorityFunding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(AgreedLocalAuthorityFunding_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThirdPartyTopUpFundingFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ThirdPartyTopUpFunding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ThirdPartyTopUpFunding_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContinuingHealthcareFundingFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ContinuingHealthcareFunding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ContinuingHealthcareFunding_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFundedNursingCareFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(FundedNursingCare_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(FundedNursingCare_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateOtherFundingFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(OtherFunding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(OtherFunding_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement1FieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceElement1_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ServiceElement1_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement2FieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceElement2_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ServiceElement2_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePlannedStartDateFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(PlannedStartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(PlannedStartDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovedFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(DateTimeApproved_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(DateTimeApproved_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Approver_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Approver_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverCommentsFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ApproverComments_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ApproverComments_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovalRejectedFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(DateTimeApprovalRejected_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(DateTimeApprovalRejected_FieldErrorLabel, 3);
            }

            return this;
        }







        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovedVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(DateTimeApproved_FieldHeader);
                WaitForElementVisible(DateTimeApproved_DateField);
                WaitForElementVisible(DateTimeApproved_TimeField);
            }
            else
            {
                WaitForElementNotVisible(DateTimeApproved_FieldHeader, 3);
                WaitForElementNotVisible(DateTimeApproved_DateField, 3);
                WaitForElementNotVisible(DateTimeApproved_TimeField, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Approver_FieldHeader);
                WaitForElementVisible(Approver_LinkField);
                WaitForElementVisible(Approver_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(Approver_FieldHeader, 3);
                WaitForElementNotVisible(Approver_LinkField, 3);
                WaitForElementNotVisible(Approver_LookUpButton, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverCommentsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ApproverComments_FieldHeader);
                WaitForElementVisible(ApproverComments_Field);
            }
            else
            {
                WaitForElementNotVisible(ApproverComments_FieldHeader, 3);
                WaitForElementNotVisible(ApproverComments_Field, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovalRejectedVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(DateTimeApprovalRejected_FieldHeader);
                WaitForElementVisible(DateTimeApprovalRejected_DateField);
                WaitForElementVisible(DateTimeApprovalRejected_TimeField);
            }
            else
            {
                WaitForElementNotVisible(DateTimeApprovalRejected_FieldHeader, 3);
                WaitForElementNotVisible(DateTimeApprovalRejected_DateField, 3);
                WaitForElementNotVisible(DateTimeApprovalRejected_TimeField, 3);
            }

            return this;
        }







        public CaseBrokerageEpisodeRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePersonFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(person_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateCaseFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Case_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateStatusFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestReceivedDateTimeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RequestReceivedDateTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSourceOfRequestFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(SourceOfRequest_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePriorityFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Priority_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestedLocalAuthorityFundingFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RequestedLocalAuthorityFunding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateAgreedLocalAuthorityFundingFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AgreedLocalAuthorityFunding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThirdPartyTopUpFundingFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ThirdPartyTopUpFunding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContinuingHealthcareFundingFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ContinuingHealthcareFunding_FieldErrorLabel, ExpectedText);

            return this;

        }
        public CaseBrokerageEpisodeRecordPage ValidateFundedNursingCareFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(FundedNursingCare_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateOtherFundingFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(OtherFunding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement1FieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ServiceElement1_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement2FieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ServiceElement2_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePlannedStartDateFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(PlannedStartDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovedFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(DateTimeApproved_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Approver_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverCommentsFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ApproverComments_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovalRejectedFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(DateTimeApprovalRejected_FieldErrorLabel, ExpectedText);

            return this;
        }








        public CaseBrokerageEpisodeRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElement(Status_Field);
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage SelectReferralType(string TextToSelect)
        {
            WaitForElement(ReferralType_Field);
            SelectPicklistElementByText(ReferralType_Field, TextToSelect);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage SelectContractType(string TextToSelect)
        {
            WaitForElement(ContractType_Field);
            SelectPicklistElementByText(ContractType_Field, TextToSelect);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertRequestReceivedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(RequestReceivedDateTime_DateField);
            SendKeys(RequestReceivedDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(RequestReceivedDateTime_DateField, Keys.Tab);
            SendKeys(RequestReceivedDateTime_TimeField, TimeToInsert);


            return this;
        }


        public CaseBrokerageEpisodeRecordPage InsertPauseDateTime(string DateToInsert)
        {
            WaitForElement(PauseDate_Field);            
            MoveToElementInPage(PauseDate_Field);
            Click(PauseDate_Field);
            SendKeys(PauseDate_Field, DateToInsert);         

            return this;
        }

        public CaseBrokerageEpisodeRecordPage InsertPauseTime(string DateToInsert)
        {
            WaitForElement(PauseTime_Field);
            MoveToElementInPage(PauseTime_Field);
            Click(PauseTime_Field);
            SendKeys(PauseTime_Field, DateToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertRestartDateTime(string DateToInsert)
        {
            WaitForElement(RestartDate_Field);
            MoveToElementInPage(RestartDate_Field);
            WaitForElementToBeClickable(RestartDate_Field);
            Click(RestartDate_Field);
            SendKeys(RestartDate_Field, DateToInsert);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage InsertOtherReason(string otherReason)
        {
            WaitForElement(otherReason_Field);
            SendKeys(otherReason_Field, otherReason);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTargetDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(TargetDateTime_DateField);
            SendKeys(TargetDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(TargetDateTime_TimeField, Keys.Tab);
            SendKeys(TargetDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSourcedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(SourcedDateTime_DateField);
            SendKeys(SourcedDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(SourcedDateTime_DateField, Keys.Tab);
            SendKeys(SourcedDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertNumberOfProvidersContacted(string TextToInsert)
        {
            WaitForElement(NumberOfProvidersContacted_Field);
            SendKeys(NumberOfProvidersContacted_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertNumberOfOffersRecieved(string TextToInsert)
        {
            WaitForElement(NumberOfOffersRecieved_Field);
            SendKeys(NumberOfOffersRecieved_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertNotes(string TextToInsert)
        {
            WaitForElement(Notes_Field);
            SendKeys(Notes_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertNumberOfCarersPerVisit(string TextToInsert)
        {
            WaitForElement(NumberOfCarersPerVisit_Field);
            SendKeys(NumberOfCarersPerVisit_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertPlannedStartDate(string TextToInsert)
        {
            WaitForElement(PlannedStartDate_Field);
            SendKeys(PlannedStartDate_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertPlannedEndDate(string TextToInsert)
        {
            WaitForElement(PlannedEndDate_Field);
            SendKeys(PlannedEndDate_Field, TextToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertDateTimeDeferred(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(DateTimeDeferred_DateField);
            SendKeys(DateTimeDeferred_DateField, DateToInsert);
            SendKeysWithoutClearing(DateTimeDeferred_DateField, Keys.Tab);
            SendKeys(DateTimeDeferred_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertMondayHealthMinutes(string TextToInsert)
        {
            WaitForElement(MondayHealthMinutes_Field);
            SendKeys(MondayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(MondayHealthMinutes_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTuesdayHealthMinutes(string TextToInsert)
        {
            WaitForElement(TuesdayHealthMinutes_Field);
            SendKeys(TuesdayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(TuesdayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertWednesdayHealthMinutes(string TextToInsert)
        {
            WaitForElement(WednesdayHealthMinutes_Field);
            SendKeys(WednesdayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(WednesdayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertThursdayHealthMinutes(string TextToInsert)
        {
            WaitForElement(ThursdayHealthMinutes_Field);
            SendKeys(ThursdayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(ThursdayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertFridayHealthMinutes(string TextToInsert)
        {
            WaitForElement(FridayHealthMinutes_Field);
            SendKeys(FridayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(FridayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSaturdayHealthMinutes(string TextToInsert)
        {
            WaitForElement(SaturdayHealthMinutes_Field);
            SendKeys(SaturdayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(SaturdayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSundayHealthMinutes(string TextToInsert)
        {
            WaitForElement(SundayHealthMinutes_Field);
            SendKeys(SundayHealthMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(SundayHealthMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTotalHealthMinutesPerWeek(string TextToInsert)
        {
            WaitForElement(TotalHealthMinutesPerWeek_Field);
            SendKeys(TotalHealthMinutesPerWeek_Field, TextToInsert);
            SendKeysWithoutClearing(TotalHealthMinutesPerWeek_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertRequestedLocalAuthorityFunding(string TextToInsert)
        {
            WaitForElement(RequestedLocalAuthorityFunding_Field);
            SendKeys(RequestedLocalAuthorityFunding_Field, TextToInsert);
            SendKeysWithoutClearing(RequestedLocalAuthorityFunding_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertAgreedLocalAuthorityFunding(string TextToInsert)
        {
            WaitForElement(AgreedLocalAuthorityFunding_Field);
            SendKeys(AgreedLocalAuthorityFunding_Field, TextToInsert);
            SendKeysWithoutClearing(AgreedLocalAuthorityFunding_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertThirdPartyTopUpFunding(string TextToInsert)
        {
            WaitForElement(ThirdPartyTopUpFunding_Field);
            SendKeys(ThirdPartyTopUpFunding_Field, TextToInsert);
            SendKeysWithoutClearing(ThirdPartyTopUpFunding_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertContinuingHealthcareFunding(string TextToInsert)
        {
            WaitForElement(ContinuingHealthcareFunding_Field);
            SendKeys(ContinuingHealthcareFunding_Field, TextToInsert);
            SendKeysWithoutClearing(ContinuingHealthcareFunding_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertFundedNursingCare(string TextToInsert)
        {
            WaitForElement(FundedNursingCare_Field);
            SendKeys(FundedNursingCare_Field, TextToInsert);
            SendKeysWithoutClearing(FundedNursingCare_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertOtherFunding(string TextToInsert)
        {
            WaitForElement(OtherFunding_Field);
            SendKeys(OtherFunding_Field, TextToInsert);
            SendKeysWithoutClearing(OtherFunding_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTotalFunding(string TextToInsert)
        {
            WaitForElement(TotalFunding_Field);
            SendKeys(TotalFunding_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertRequestRejectionDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(RequestRejectionDateTime_DateField);
            SendKeys(RequestRejectionDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(RequestRejectionDateTime_DateField, Keys.Tab);
            SendKeys(RequestRejectionDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertCancellationDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(CancellationDateTime_DateField);
            SendKeys(CancellationDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(CancellationDateTime_DateField, Keys.Tab);
            SendKeys(CancellationDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertContactName(string TextToInsert)
        {
            WaitForElement(ContactName_Field);
            SendKeys(ContactName_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertRelationshipToCitizen(string TextToInsert)
        {
            WaitForElement(RelationshipToCitizen_Field);
            SendKeys(RelationshipToCitizen_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertContactAddress(string TextToInsert)
        {
            WaitForElement(ContactAddress_Field);
            SendKeys(ContactAddress_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertContactPhoneNumber(string TextToInsert)
        {
            WaitForElement(ContactPhoneNumber_Field);
            SendKeys(ContactPhoneNumber_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertDateTimeApprovedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(DateTimeApproved_DateField);
            SendKeys(DateTimeApproved_DateField, DateToInsert);
            SendKeysWithoutClearing(DateTimeApproved_DateField, Keys.Tab);
            SendKeys(DateTimeApproved_TimeField, TimeToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertApproverComments(string TextToInsert)
        {
            WaitForElement(ApproverComments_Field);
            SendKeys(ApproverComments_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertDateTimeApprovalRejectedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(DateTimeApprovalRejected_DateField);
            SendKeys(DateTimeApprovalRejected_DateField, DateToInsert);
            SendKeysWithoutClearing(DateTimeApprovalRejected_DateField, Keys.Tab);
            SendKeys(DateTimeApprovalRejected_TimeField, TimeToInsert);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertMondayMinutes(string TextToInsert)
        {
            WaitForElement(Monday_Field);
            SendKeys(Monday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTuesdayMinutes(string TextToInsert)
        {
            WaitForElement(Tuesday_Field);
            SendKeys(Tuesday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertWednesdayMinutes(string TextToInsert)
        {
            WaitForElement(Wednesday_Field);
            SendKeys(Wednesday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertThursdayMinutes(string TextToInsert)
        {
            WaitForElement(Thursday_Field);
            SendKeys(Thursday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertFridayMinutes(string TextToInsert)
        {
            WaitForElement(Friday_Field);
            SendKeys(Friday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSaturdayMinutes(string TextToInsert)
        {
            WaitForElement(Saturday_Field);
            SendKeys(Saturday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSundayMinutes(string TextToInsert)
        {
            WaitForElement(Sunday_Field);
            SendKeys(Sunday_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTotalMinutesPerWeek(string TextToInsert)
        {
            WaitForElement(TotalMinutesPerWeek_Field);
            SendKeys(TotalMinutesPerWeek_Field, TextToInsert);
            SendKeysWithoutClearing(Monday_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertMondaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(MondaySocialCareMinutes_Field);
            SendKeys(MondaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(MondaySocialCareMinutes_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTuesdaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(TuesdaySocialCareMinutes_Field);
            SendKeys(TuesdaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(TuesdaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertWednesdaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(WednesdaySocialCareMinutes_Field);
            SendKeys(WednesdaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(WednesdaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertThursdaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(ThursdaySocialCareMinutes_Field);
            SendKeys(ThursdaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(ThursdaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertFridaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(FridaySocialCareMinutes_Field);
            SendKeys(FridaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(FridaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSaturdaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(SaturdaySocialCareMinutes_Field);
            SendKeys(SaturdaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(SaturdaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertSundaySocialCareMinutes(string TextToInsert)
        {
            WaitForElement(SundaySocialCareMinutes_Field);
            SendKeys(SundaySocialCareMinutes_Field, TextToInsert);
            SendKeysWithoutClearing(SundaySocialCareMinutes_Field, Keys.Tab);


            return this;
        }
        public CaseBrokerageEpisodeRecordPage InsertTotalSocialCareMinutesPerWeek(string TextToInsert)
        {
            WaitForElement(TotalSocialCareMinutesPerWeek_Field);
            SendKeys(TotalSocialCareMinutesPerWeek_Field, TextToInsert);
            SendKeysWithoutClearing(TotalSocialCareMinutesPerWeek_Field, Keys.Tab);


            return this;
        }



        public CaseBrokerageEpisodeRecordPage ClickPasueReasonLookUpButton()
        {
            WaitForElementToBeClickable(PasueReasonLookupButton);
            Click(PasueReasonLookupButton);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ClickCaseLookUpButton()
        {
            WaitForElementToBeClickable(Case_LookUpButton);
            Click(Case_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickCaseRemoveButton()
        {
            WaitForElementToBeClickable(Case_RemoveButton);
            Click(Case_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickPersonLookUpButton()
        {
            WaitForElementToBeClickable(person_LookUpButton);
            Click(person_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickPersonRemoveButton()
        {
            WaitForElementToBeClickable(person_RemoveButton);
            Click(person_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickSourceOfRequestLookUpButton()
        {
            WaitForElementToBeClickable(SourceOfRequest_LookUpButton);
            Click(SourceOfRequest_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickSourceOfRequestRemoveButton()
        {
            WaitForElementToBeClickable(SourceOfRequest_RemoveButton);
            Click(SourceOfRequest_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickPriorityLookUpButton()
        {
            WaitForElementToBeClickable(Priority_LookUpButton);
            Click(Priority_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickPriorityRemoveButton()
        {
            WaitForElementToBeClickable(Priority_RemoveButton);
            Click(Priority_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickResponsibleUserLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_LookUpButton);
            Click(ResponsibleUser_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickResponsibleUserRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_RemoveButton);
            Click(ResponsibleUser_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickTrackingStatusLookUpButton()
        {
            WaitForElementToBeClickable(TrackingStatus_LookUpButton);
            Click(TrackingStatus_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickTrackingStatusRemoveButton()
        {
            WaitForElementToBeClickable(TrackingStatus_RemoveButton);
            Click(TrackingStatus_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickRelatedAssessmentLookUpButton()
        {
            WaitForElementToBeClickable(RelatedAssessment_LookUpButton);
            Click(RelatedAssessment_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickRelatedAssessmentRemoveButton()
        {
            WaitForElementToBeClickable(RelatedAssessment_RemoveButton);
            Click(RelatedAssessment_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickContactPersonLookUpButton()
        {
            WaitForElementToBeClickable(ContactPerson_LookUpButton);
            Click(ContactPerson_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickContactPersonRemoveButton()
        {
            WaitForElementToBeClickable(ContactPerson_RemoveButton);
            Click(ContactPerson_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickExistingCarePackageLookUpButton()
        {
            WaitForElementToBeClickable(ExistingCarePackage_LookUpButton);
            Click(ExistingCarePackage_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickExistingCarePackageRemoveButton()
        {
            WaitForElementToBeClickable(ExistingCarePackage_RemoveButton);
            Click(ExistingCarePackage_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickInternalReferrerLookUpButton()
        {
            WaitForElementToBeClickable(InternalReferrer_LookUpButton);
            Click(InternalReferrer_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickInternalReferrerRemoveButton()
        {
            WaitForElementToBeClickable(InternalReferrer_RemoveButton);
            Click(InternalReferrer_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickTypeOFCarePackageLookUpButton()
        {
            WaitForElementToBeClickable(TypeOFCarePackage_LookUpButton);
            Click(TypeOFCarePackage_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickTypeOFCarePackageRemoveButton()
        {
            WaitForElementToBeClickable(TypeOFCarePackage_RemoveButton);
            Click(TypeOFCarePackage_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickServiceElement1LookUpButton()
        {
            WaitForElementToBeClickable(ServiceElement1_LookUpButton);
            MoveToElementInPage(ServiceElement1_LookUpButton);
            Click(ServiceElement1_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickServiceElement1RemoveButton()
        {
            WaitForElementToBeClickable(ServiceElement1_RemoveButton);
            Click(ServiceElement1_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickServiceElement2LookUpButton()
        {
            WaitForElementToBeClickable(ServiceElement2_LookUpButton);
            Click(ServiceElement2_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickServiceElement2RemoveButton()
        {
            WaitForElementToBeClickable(ServiceElement2_RemoveButton);
            Click(ServiceElement2_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickFinanceClientCategoryLookUpButton()
        {
            WaitForElementToBeClickable(FinanceClientCategory_LookUpButton);
            Click(FinanceClientCategory_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickFinanceClientCategoryRemoveButton()
        {
            WaitForElementToBeClickable(FinanceClientCategory_RemoveButton);
            Click(FinanceClientCategory_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickFundingArrangementTypeLookUpButton()
        {
            WaitForElementToBeClickable(FundingArrangementType_LookUpButton);
            Click(FundingArrangementType_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickFundingArrangementTypeRemoveButton()
        {
            WaitForElementToBeClickable(FundingArrangementType_RemoveButton);
            Click(FundingArrangementType_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickRequestRejectionReasonLookUpButton()
        {
            WaitForElementToBeClickable(RequestRejectionReason_LookUpButton);
            Click(RequestRejectionReason_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickRequestRejectionReasonRemoveButton()
        {
            WaitForElementToBeClickable(RequestRejectionReason_RemoveButton);
            Click(RequestRejectionReason_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickCancellationReasonLookUpButton()
        {
            WaitForElementToBeClickable(CancellationReason_LookUpButton);
            Click(CancellationReason_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickCancellationReasonRemoveButton()
        {
            WaitForElementToBeClickable(CancellationReason_RemoveButton);
            Click(CancellationReason_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickContactRegisteredInCareDirectorYesRadioButton()
        {
            WaitForElementToBeClickable(ContactRegisteredInCareDirector_YesRadioButton);
            Click(ContactRegisteredInCareDirector_YesRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickContactRegisteredInCareDirectorNoRadioButton()
        {
            WaitForElementToBeClickable(ContactRegisteredInCareDirector_NoRadioButton);
            Click(ContactRegisteredInCareDirector_NoRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickScheduleRequiredYesRadioButton()
        {
            WaitForElementToBeClickable(ScheduleRequired_YesRadioButton);
            Click(ScheduleRequired_YesRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickScheduleRequiredNoRadioButton()
        {
            WaitForElementToBeClickable(ScheduleRequired_NoRadioButton);
            Click(ScheduleRequired_NoRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickDeferredToCommissioningYesRadioButton()
        {
            WaitForElementToBeClickable(DeferredToCommissioning_YesRadioButton);
            Click(DeferredToCommissioning_YesRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickDeferredToCommissioningNoRadioButton()
        {
            WaitForElementToBeClickable(DeferredToCommissioning_NoRadioButton);
            Click(DeferredToCommissioning_NoRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickApproverLookUpButton()
        {
            WaitForElementToBeClickable(Approver_LookUpButton);
            Click(Approver_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickApproverRemoveButton()
        {
            WaitForElementToBeClickable(Approver_RemoveButton);
            Click(Approver_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ClickServiceElement1LinkFieldText()
        {
            WaitForElementToBeClickable(ServiceElement1_LinkField);
            Click(ServiceElement1_LinkField);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ValidatePasueEpisodeButtonVisbility(bool ExpectVisible)
        {
            WaitForElementVisible(moreoptions);
            Click(moreoptions);
            
            if (ExpectVisible)
            {
                WaitForElementVisible(PauseButton);
            }
            else
            {
                WaitForElementNotVisible(PauseButton, 3);
            }

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateRefeshPageButtonVisbility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Refresh_Button);
            }
            else
            {
                WaitForElementNotVisible(Refresh_Button, 3);
            }

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateStatusOptionDisabled(string OptionText, bool ExpectDisabled)
        {
            ValidatePicklistOptionDisabled(Status_Field, OptionText, ExpectDisabled);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ValidateCaseLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Case_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(person_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSourceOfRequestLinkFieldText(string ExpectedText)
        {
            ValidateElementText(SourceOfRequest_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePriorityLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTrackingStatusLinkFieldText(string ExpectedText)
        {
            ValidateElementText(TrackingStatus_LinkField, ExpectedText);

            return this;
        }

       
        public CaseBrokerageEpisodeRecordPage ValidateRelatedAssessmentLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RelatedAssessment_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactPersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ContactPerson_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateExistingCarePackageLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ExistingCarePackage_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateInternalReferrerLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(InternalReferrer_LinkField);
            ValidateElementText(InternalReferrer_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTypeOFCarePackageLinkFieldText(string ExpectedText)
        {
            ValidateElementText(TypeOFCarePackage_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement1LinkFieldText(string ExpectedText)
        {
            ValidateElementText(ServiceElement1_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement2LinkFieldText(string ExpectedText)
        {
            ValidateElementText(ServiceElement2_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFinanceClientCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(FinanceClientCategory_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFundingArrangementTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(FundingArrangementType_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestRejectionReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RequestRejectionReason_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateCancellationReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(CancellationReason_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Approver_LinkField, ExpectedText);

            return this;
        }






       
        public CaseBrokerageEpisodeRecordPage ValidateNumberOfOffersRecievedFieldDisabled(bool ExpectDisabled)
        {
            if(ExpectDisabled)
                ValidateElementDisabled(NumberOfOffersRecieved_Field);
            else
                ValidateElementNotDisabled(NumberOfOffersRecieved_Field);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTotalHealthMinutesPerWeekFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(TotalHealthMinutesPerWeek_Field);
            else
                ValidateElementEnabled(TotalHealthMinutesPerWeek_Field);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Status_Field);
            MoveToElementInPage(Status_Field);
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateReferralTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ReferralType_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContractTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ContractType_Field, ExpectedText);

            return this;
        }





        public CaseBrokerageEpisodeRecordPage ValidateRequestReceivedDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(RequestReceivedDateTime_DateField, ExpectedDate);
            ValidateElementValue(RequestReceivedDateTime_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTargetDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(TargetDateTime_DateField, ExpectedDate);
            ValidateElementValue(TargetDateTime_TimeField, ExpectedTime);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateSourcedDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(SourcedDateTime_DateField, ExpectedDate);
            ValidateElementValue(SourcedDateTime_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestRejectionDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(RequestRejectionDateTime_DateField, ExpectedDate);
            ValidateElementValue(RequestRejectionDateTime_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateCancellationDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(CancellationDateTime_DateField, ExpectedDate);
            ValidateElementValue(CancellationDateTime_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeDeferred(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeDeferred_DateField, ExpectedDate);
            ValidateElementValue(DateTimeDeferred_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApproved(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeApproved_DateField, ExpectedDate);
            ValidateElementValue(DateTimeApproved_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDateTimeApprovalRejected(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeApprovalRejected_DateField, ExpectedDate);
            ValidateElementValue(DateTimeApprovalRejected_TimeField, ExpectedTime);

            return this;
        }





        public CaseBrokerageEpisodeRecordPage ValidateNumberOfProvidersContacted(string ExpectedText)
        {
            ValidateElementValue(NumberOfProvidersContacted_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateNumberOfOffersRecieved(string ExpectedText)
        {
            ValidateElementValue(NumberOfOffersRecieved_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateNotesFieldText(string ExpectedText)
        {
            ValidateElementValue(Notes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateNumberOfCarersPerVisitFieldText(string ExpectedText)
        {
            ValidateElementValue(NumberOfCarersPerVisit_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePlannedStartDate(string ExpectedText)
        {
            ValidateElementValue(PlannedStartDate_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePlannedEndDate(string ExpectedText)
        {
            ValidateElementValue(PlannedEndDate_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateMondayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(MondayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTuesdayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(TuesdayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateWednesdayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(WednesdayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThursdayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(ThursdayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFridayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(FridayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSaturdayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(SaturdayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSundayHealthMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(SundayHealthMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTotalHealthMinutesPerWeekFieldText(string ExpectedText)
        {
            ValidateElementValue(TotalHealthMinutesPerWeek_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRequestedLocalAuthorityFunding(string ExpectedText)
        {
            ValidateElementValue(RequestedLocalAuthorityFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateAgreedLocalAuthorityFunding(string ExpectedText)
        {
            ValidateElementValue(AgreedLocalAuthorityFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThirdPartyTopUpFunding(string ExpectedText)
        {
            ValidateElementValue(ThirdPartyTopUpFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContinuingHealthcareFunding(string ExpectedText)
        {
            ValidateElementValue(ContinuingHealthcareFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFundedNursingCare(string ExpectedText)
        {
            ValidateElementValue(FundedNursingCare_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateOtherFunding(string ExpectedText)
        {
            ValidateElementValue(OtherFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTotalFunding(string ExpectedText)
        {
            ValidateElementValue(TotalFunding_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactName(string ExpectedText)
        {
            ValidateElementValue(ContactName_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateRelationshipToCitizen(string ExpectedText)
        {
            ValidateElementValue(RelationshipToCitizen_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactAddress(string ExpectedText)
        {
            ValidateElementValue(ContactAddress_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactPhoneNumber(string ExpectedText)
        {
            ValidateElementValue(ContactPhoneNumber_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateApproverComments(string ExpectedText)
        {
            ValidateElementValue(ApproverComments_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateMondayFieldText(string ExpectedText)
        {
            ValidateElementValue(Monday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTuesdayFieldText(string ExpectedText)
        {
            ValidateElementValue(Tuesday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateWednesdayFieldText(string ExpectedText)
        {
            ValidateElementValue(Wednesday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThursdayFieldText(string ExpectedText)
        {
            ValidateElementValue(Thursday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFridayFieldText(string ExpectedText)
        {
            ValidateElementValue(Friday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSaturdayFieldText(string ExpectedText)
        {
            ValidateElementValue(Saturday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSundayFieldText(string ExpectedText)
        {
            ValidateElementValue(Sunday_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTotalMinutesPerWeekFieldText(string ExpectedText)
        {
            ValidateElementValue(TotalMinutesPerWeek_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateMondaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(MondaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTuesdaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(TuesdaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateWednesdaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(WednesdaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateThursdaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(ThursdaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateFridaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(FridaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSaturdaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(SaturdaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSundaySocialCareMinutesFieldText(string ExpectedText)
        {
            ValidateElementValue(SundaySocialCareMinutes_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTotalSocialCareMinutesPerWeekFieldText(string ExpectedText)
        {
            ValidateElementValue(TotalSocialCareMinutesPerWeek_Field, ExpectedText);

            return this;
        }




        public CaseBrokerageEpisodeRecordPage ValidateBrokerageResponsibleForCommunications(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(BrokerageResponsibleForCommunications_YesRadioButton);
                ValidateElementNotChecked(BrokerageResponsibleForCommunications_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(BrokerageResponsibleForCommunications_YesRadioButton);
                ValidateElementChecked(BrokerageResponsibleForCommunications_YesRadioButton);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateContactRegisteredInCareDirector(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ContactRegisteredInCareDirector_YesRadioButton);
                ValidateElementNotChecked(ContactRegisteredInCareDirector_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContactRegisteredInCareDirector_YesRadioButton);
                ValidateElementChecked(ContactRegisteredInCareDirector_NoRadioButton);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateSection117EligibleNeeds(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(Section117EligibleNeeds_YesRadioButton);
                ValidateElementNotChecked(Section117EligibleNeeds_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Section117EligibleNeeds_YesRadioButton);
                ValidateElementChecked(Section117EligibleNeeds_YesRadioButton);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateTemporaryCare(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(TemporaryCare_YesRadioButton);
                ValidateElementNotChecked(TemporaryCare_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(TemporaryCare_YesRadioButton);
                ValidateElementChecked(TemporaryCare_YesRadioButton);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateScheduleRequired(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ScheduleRequired_YesRadioButton);
                ValidateElementNotChecked(ScheduleRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ScheduleRequired_YesRadioButton);
                ValidateElementChecked(ScheduleRequired_NoRadioButton);
            }

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateDeferredToCommissioning(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(DeferredToCommissioning_YesRadioButton);
                ValidateElementNotChecked(DeferredToCommissioning_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(DeferredToCommissioning_YesRadioButton);
                ValidateElementChecked(DeferredToCommissioning_NoRadioButton);
            }

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateServiceElement1FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ServiceElement1_Field);
            else
                ValidateElementEnabled(ServiceElement1_Field);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidateServiceElement2FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ServiceElement2_Field);
            else
                ValidateElementEnabled(ServiceElement2_Field);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidatePlannedStartDateDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(PlannedStartDate_Field);
            else
                ValidateElementEnabled(PlannedStartDate_Field);

            return this;
        }
        public CaseBrokerageEpisodeRecordPage ValidatePlannedEndDateDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(PlannedEndDate_Field);
            else
                ValidateElementEnabled(PlannedEndDate_Field);

            return this;
        }


        public CaseBrokerageEpisodeRecordPage ValidateFinanceClientCategoryDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(FinanceClientCategory_Field);
            else
                ValidateElementEnabled(FinanceClientCategory_Field);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateTemperoryCare_YesRadioButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(TemporaryCare_YesRadioButton);
            else
                ValidateElementEnabled(TemporaryCare_YesRadioButton);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateTemperoryCare_NoRadioButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(TemporaryCare_NoRadioButton);
            else
                ValidateElementEnabled(TemporaryCare_NoRadioButton);

            return this;
        }

        public CaseBrokerageEpisodeRecordPage ValidateContractTypeDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ContractType_Field);
            else
                ValidateElementEnabled(ContractType_Field);

            return this;
        }

    }
}
