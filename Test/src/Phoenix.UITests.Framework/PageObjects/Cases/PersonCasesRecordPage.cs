
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCasesRecordPage : CommonMethods
    {
        public PersonCasesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ProviderRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        //Reactivate case LookUP
        readonly By ReactiveIframe = By.Id("iframe_InpatientCaseReactivation");
        readonly By CaseStatusId_Field = By.Id("CWCaseStatusId");
        readonly By CWCaseReopenReasonId_LookupButton = By.Id("CWLookupBtn_CWCaseReopenReasonId");
        readonly By OkButton = By.Id("CWReactivate");
        

        readonly By pageHeader = By.XPath("//h1[@title='Case: New']");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By moreOptionsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By ActivateButton = By.Id("TI_ActivateButton");
        readonly By dataFormList_Field = By.Id("dataFormList");
        readonly By ChooseDataFormButton = By.Id("btnChooseDataForm");
        readonly By NotificationMessage_DataForm = By.Id("CWNotificationMessage_DataForm");
        readonly By DetailsButton = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RelatedItemsButton = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By HealthButton = By.XPath("//*[@id='CWNavSubGroup_Health']/a");

        readonly By CaseFormButton = By.Id("CWNavItem_CaseForm");
        readonly By DichargeOption_Disabled = By.XPath("/html/body/form/div[6]/div[2]/div[1]/div/div/fieldset/div[2]/div/div[2]/ul/li[12]/select/option[5]");
        readonly By AwaitingAdmission_Disabled = By.XPath("/html/body/form/div[6]/div[2]/div[1]/div/div/fieldset/div[2]/div/div[2]/ul/li[12]/select/option[2]");
        readonly By PersonDiagnosesButton = By.Id("CWNavItem_PersonDiagnoses");
        readonly By ConsultantEpisodesLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_InpatientConsultantEpisodes']");
        readonly By LeaveAWOLButton = By.Id("CWNavItem_InpatientLeaveAwol");
        readonly By SeclusionsButton = By.Id("CWNavItem_InpatientSeclusion");
        readonly By HealthAppointment = By.Id("CWNavItem_HealthAppointment");

        #region General Fields

        readonly By initialcontactid_LookupButton = By.Id("CWLookupBtn_initialcontactid");
        readonly By contactreceiveddate_Field = By.Id("CWField_contactreceiveddatetime");
        readonly By contactreceivedtime_Field = By.Id("CWField_contactreceiveddatetime_Time");
        readonly By contactreceivedbyid_LookupButton = By.Id("CWLookupBtn_contactreceivedbyid");
        readonly By reasonforadmission_LookupButton = By.Id("CWLookupBtn_contactreasonid");
        readonly By inpatientcasestatusid_Field = By.Id("CWField_inpatientcasestatusid");

        #endregion

        #region Source Infermation Fields

        readonly By contactsourceid_LookupButton = By.Id("CWLookupBtn_contactsourceid");
        readonly By inpatientadmissionsourceid_LookupButton = By.Id("CWLookupBtn_inpatientadmissionsourceid");

        #endregion

        #region AwaitingAdmission Fields
        readonly By AwaitingAdmission_Section = By.Id("CWSection_AwaitingAdmission");
        readonly By decisiontoadmitagreeddate_Field = By.Id("CWField_decisiontoadmitagreeddatetime");
        readonly By decisiontoadmitagreeddatetime_Field = By.Id("CWField_decisiontoadmitagreeddatetime_Time");
        readonly By intendedadmissiondate_Field = By.Id("CWField_intendedadmissiondate");
        #endregion

        #region Presenting Situation Fields
        readonly By presentingneeddetails_Field = By.Id("CWField_presentingneeddetails");
        readonly By inpatientadmissionmethodid_LookupButton = By.Id("CWLookupBtn_inpatientadmissionmethodid");
        #endregion

        #region Inpatient Management Fields
        readonly By currentconsultantid_LookupButton = By.Id("CWLookupBtn_currentconsultantid");
        readonly By admissiondatetime_Field = By.Id("CWField_admissiondatetime");
        readonly By admissiontime_Field = By.Id("CWField_admissiondatetime_Time");
        readonly By waitingtimestartdate_Field = By.Id("CWField_waitingtimestartdate");
        #endregion

        #region Bed Occupancy Fields
        readonly By providerid_LookupButton = By.Id("CWLookupBtn_providerid");
        readonly By inpatientwardid_LookupButton = By.Id("CWLookupBtn_inpatientwardid");
        readonly By inpatientBayid_LookupButton = By.Id("CWLookupBtn_inpatientbayid");
        readonly By inpatientBedid_LookupButton = By.Id("CWLookupBtn_inpatientbedid");
        readonly By inpatientresponsiblewardid_LookupButton = By.Id("CWLookupBtn_inpatientresponsiblewardid");
        readonly By WardId_Field = By.Id("CWField_inpatientwardid_cwname");
        readonly By BayId_Field = By.Id("CWField_inpatientbayid_cwname");
        readonly By BedId_Field = By.Id("CWField_inpatientbedid_cwname");
        #endregion

        #region Referral to Treatment Fields

        readonly By RTTReferral_Field = By.Id("CWField_rttreferralid");

        #endregion

        #region Discharge/Closer Fields
        readonly By Discharge_Title = By.Id("CWSection_DischargeClosure");
        readonly By actualdischargedate_Field = By.Id("CWField_actualdischargedatetime");
        readonly By actualdischargedatetim_Field = By.Id("CWField_actualdischargedatetime_Time");
        readonly By caseclosurereasonid_LookupButton = By.Id("CWLookupBtn_caseclosurereasonid");
        readonly By actualdischargedestinationid_LookupButton = By.Id("CWLookupBtn_actualdischargedestinationid");
        readonly By followupteamid_LookupButton = By.Id("CWLookupBtn_followupteamid");
        readonly By caserejectedreasonid_LookUpButton = By.Id("CWLookupBtn_caserejectedreasonid");
        #endregion



        public PersonCasesRecordPage WaitForPersonCasesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            

            
            return this;
        }

        public PersonCasesRecordPage WaitForReactiveRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            WaitForElement(ReactiveIframe);
            SwitchToIframe(ReactiveIframe);           


            return this;
        }

        public PersonCasesRecordPage SelectCaseStatus(String OptionToSelect)
        {
            WaitForElementVisible(CaseStatusId_Field);
            SelectPicklistElementByText(CaseStatusId_Field, OptionToSelect);

            return this;
        }

        public PersonCasesRecordPage ClickReopenReasonLookUp()
        {
            WaitForElement(CWCaseReopenReasonId_LookupButton);
            Click(CWCaseReopenReasonId_LookupButton);

            return this;
        }

       

        public PersonCasesRecordPage ClickOKButton()
        {
            WaitForElement(OkButton);
            Click(OkButton);

            return this;
        }


        public PersonCasesRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }
        public PersonCasesRecordPage ClickActivateButton()
        {
            Click(moreOptionsButton);
            WaitForElement(ActivateButton);
            Click(ActivateButton);

            return this;
        }
        public PersonCasesRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage_DataForm, ExpectedMessage);

            return this;
        }

        public PersonCasesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }
        public PersonCasesRecordPage InsertOutlineNeedForAdmission(string outline)
        {
            SendKeys(presentingneeddetails_Field, outline);

            return this;
        }
        public PersonCasesRecordPage InsertContactReceivedDate(string DateToInsert)
        {
            SendKeys(contactreceiveddate_Field,DateToInsert);
            SendKeysWithoutClearing(contactreceiveddate_Field, Keys.Tab);

            return this;
        }

        public PersonCasesRecordPage InsertDecisionAdmitDate(string DateToInsert)
        {
            SendKeys(decisiontoadmitagreeddate_Field, DateToInsert);
            SendKeysWithoutClearing(decisiontoadmitagreeddate_Field, Keys.Tab);

            return this;
        }

        public PersonCasesRecordPage InsertIntendedAdmissionDate(string DateToInsert)
        {
            SendKeys(intendedadmissiondate_Field, DateToInsert);

            return this;
        }

        public PersonCasesRecordPage InserDecisionAdmitTime(string TimeToInsert)
        {
            SendKeys(decisiontoadmitagreeddatetime_Field, TimeToInsert);

            return this;
        }

        public PersonCasesRecordPage InserContactReceivedTime(string TimeToInsert)
        {
            SendKeys(contactreceivedtime_Field, TimeToInsert);

            return this;
        }

        public PersonCasesRecordPage SelectInpatientStatusId(String OptionToSelect)
        {
            WaitForElementVisible(inpatientcasestatusid_Field);
            SelectPicklistElementByText(inpatientcasestatusid_Field, OptionToSelect);

            return this;
        }

        public PersonCasesRecordPage SelectDateForm(String OptionToSelect)
        {
            WaitForElementVisible(dataFormList_Field);
            SelectPicklistElementByText(dataFormList_Field, OptionToSelect);
            Click(ChooseDataFormButton);

            return this;
        }

        public PersonCasesRecordPage ClickContactReceivedLookupButton()
        {
            Click(contactreceivedbyid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickInitialContactLookupButton()
        {
            Click(initialcontactid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickReasonForAdmissionLookupButton()
        {
            Click(reasonforadmission_LookupButton);
           

            return this;
        }

        public PersonCasesRecordPage ClickContactSourceLookupButton()
        {
            Click(contactsourceid_LookupButton);

            return this;
        }

        public PersonCasesRecordPage ClickAdmissionSourceLookupButton()
        {
            Click(inpatientadmissionsourceid_LookupButton);

            return this;
        }

        public PersonCasesRecordPage ClickAdmissionMethodLookupButton()
        {
            Click(inpatientadmissionmethodid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickCurrentConsultantLookupButton()
        {
            Click(currentconsultantid_LookupButton);

            return this;
        }

        public PersonCasesRecordPage ClickActualDischargeMethodLookupButton()
        {
            Click(caseclosurereasonid_LookupButton);

            return this;
        }

        public PersonCasesRecordPage ClickReasonNotAcceptedLookupButton()
        {
            Click(caserejectedreasonid_LookUpButton);

            return this;
        }

        public PersonCasesRecordPage ClickActualDischargeDestinationLookupButton()
        {
            Click(actualdischargedestinationid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage InsertAdmissionDate(string dateToInsert)
        {
            SendKeys(admissiondatetime_Field, dateToInsert);
            SendKeysWithoutClearing(admissiondatetime_Field, Keys.Tab);

            return this;
        }
        public PersonCasesRecordPage InsertDischargeDate(string dateToInsert)
        {
            SendKeys(actualdischargedate_Field, dateToInsert);
            SendKeysWithoutClearing(actualdischargedate_Field, Keys.Tab);

            return this;
        }

        public PersonCasesRecordPage InsertDischargeTime(string timeToInsert)
        {
            SendKeys(actualdischargedatetim_Field, timeToInsert);

            return this;
        }
        public PersonCasesRecordPage InsertAdmissionTime(string timeToInsert)
        {
            SendKeys(admissiontime_Field, timeToInsert);

            return this;
        }

        public PersonCasesRecordPage InsertWaitingTime(string timeToInsert)
        {
            SendKeys(waitingtimestartdate_Field, timeToInsert);

            return this;
        }

        public PersonCasesRecordPage ClickHospitalLookupButton()
        {
            Click(providerid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickWardLookupButton()
        {
            Click(inpatientwardid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickBayLookupButton()
        {
            Click(inpatientBayid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickBedLookupButton()
        {
            Click(inpatientBedid_LookupButton);

            return this;
        }
        public PersonCasesRecordPage ClickResponsibleWardLookupButton()
        {
            Click(inpatientresponsiblewardid_LookupButton);

            return this;
        }

        public PersonCasesRecordPage SelectRTTReferralFieldValue(string TextToSelect)
        {
            MoveToElementInPage(RTTReferral_Field);
            SelectPicklistElementByText(RTTReferral_Field, TextToSelect);

            return this;
        }

        public PersonCasesRecordPage ValidateRTTReferralFieldValue(string ExpectedText)
        {
            MoveToElementInPage(RTTReferral_Field);
            ValidatePicklistSelectedText(RTTReferral_Field, ExpectedText);

            return this;
        }

        public PersonCasesRecordPage ValidateAwaitingAdmission_SectionVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(AwaitingAdmission_Section);
            }
            else
            {
                WaitForElementNotVisible(AwaitingAdmission_Section, 5);
            }
            return this;
        }

        public PersonCasesRecordPage ValidateWard_FieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(WardId_Field);
            }
            else
            {
                WaitForElementNotVisible(WardId_Field, 5);
            }
            return this;
        }

        public PersonCasesRecordPage ValidateBay_FieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(BayId_Field);
            }
            else
            {
                WaitForElementNotVisible(BayId_Field, 5);
            }
            return this;
        }
        public PersonCasesRecordPage ValidateBed_FieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(BedId_Field);
            }
            else
            {
                WaitForElementNotVisible(BedId_Field, 5);
            }
            return this;
        }

      

        public PersonCasesRecordPage NavigateToDetailsPage()
        {
            Click(DetailsButton);

           
            return this;
        }

        public PersonCasesRecordPage NavigateToFormsCasePage()
        {
            Click(MenuButton);
            WaitForElementVisible(RelatedItemsButton);
            Click(RelatedItemsButton);

            WaitForElementVisible(CaseFormButton);
            Click(CaseFormButton);


            return this;
        }

        public PersonCasesRecordPage ValidateDischargeOptionIsDisabled()
        {
           
            ValidateElementDisabled(DichargeOption_Disabled);
            


            return this;
        }

        public PersonCasesRecordPage ValidateAwaitingAdmissionOptionIsDisabled()
        {

            ValidateElementDisabled(AwaitingAdmission_Disabled);



            return this;
        }

        public PersonCasesRecordPage NavigateToDiagnosisPage()
        {
            Click(MenuButton);
            WaitForElementVisible(HealthButton);
            Click(HealthButton);

            WaitForElementVisible(PersonDiagnosesButton);
            Click(PersonDiagnosesButton);


            return this;
        }

        public PersonCasesRecordPage NavigateToConsultantEpisodesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthButton);
            Click(HealthButton);

            WaitForElementToBeClickable(ConsultantEpisodesLeftSubMenuItem);
            Click(ConsultantEpisodesLeftSubMenuItem);

            return new PersonCasesRecordPage(this.driver, this.Wait, this.appURL);
        }

        public PersonCasesRecordPage NavigateToLeaveAWOLPage()
        {
            Click(MenuButton);
            WaitForElementVisible(HealthButton);
            Click(HealthButton);

            WaitForElementVisible(LeaveAWOLButton);
            Click(LeaveAWOLButton);


            return this;
        }

        public PersonCasesRecordPage NavigateToSeclusionsPage()
        {
            Click(MenuButton);
            WaitForElementVisible(HealthButton);
            Click(HealthButton);

            WaitForElementVisible(SeclusionsButton);
            Click(SeclusionsButton);


            return this;
        }

        public PersonCasesRecordPage ValidateLeaveAWOLNotVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementNotVisible(LeaveAWOLButton, 5);
            }

            return this;
        }

        public PersonCasesRecordPage NavigateToHealthAppointmentsPage()
        {
            Click(MenuButton);
            WaitForElement(HealthButton);
            WaitForElementVisible(HealthButton);
            Click(HealthButton);

            WaitForElementVisible(HealthAppointment);
            WaitForElementVisible(HealthAppointment);
            Click(HealthAppointment);


            return this;
        }



    }
}
