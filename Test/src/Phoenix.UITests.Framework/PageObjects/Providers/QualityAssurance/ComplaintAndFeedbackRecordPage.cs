using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ComplaintAndFeedbackRecordPage : CommonMethods
    {

        public ComplaintAndFeedbackRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=providercomplaintfeedback')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By RestrictAccessButton = By.XPath("//*[@id='TI_RestrictAccessButton']");

        readonly By Complaintfeedbackdate = By.XPath("//*[@id='CWField_complaintfeedbackdate']");
        readonly By ComplaintfeedbackdateDatePicker = By.XPath("//*[@id='CWField_complaintfeedbackdate_DatePicker']");
        readonly By MadebyidLink = By.XPath("//*[@id='CWField_madebyid_Link']");
        readonly By MadebyidClearButton = By.XPath("//*[@id='CWClearLookup_madebyid']");
        readonly By MadebyidLookupButton = By.XPath("//*[@id='CWLookupBtn_madebyid']");
        readonly By ProvidercomplaintfeedbacktypeidLink = By.XPath("//*[@id='CWField_providercomplaintfeedbacktypeid_Link']");
        readonly By ProvidercomplaintfeedbacktypeidClearButton = By.XPath("//*[@id='CWClearLookup_providercomplaintfeedbacktypeid']");
        readonly By ProvidercomplaintfeedbacktypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_providercomplaintfeedbacktypeid']");
        readonly By ProvidercomplaintstageidLink = By.XPath("//*[@id='CWField_providercomplaintstageid_Link']");
        readonly By ProvidercomplaintstageidClearButton = By.XPath("//*[@id='CWClearLookup_providercomplaintstageid']");
        readonly By ProvidercomplaintstageidLookupButton = By.XPath("//*[@id='CWLookupBtn_providercomplaintstageid']");
        readonly By ProvidercomplaintoutcomeidLink = By.XPath("//*[@id='CWField_providercomplaintoutcomeid_Link']");
        readonly By ProvidercomplaintoutcomeidClearButton = By.XPath("//*[@id='CWClearLookup_providercomplaintoutcomeid']");
        readonly By ProvidercomplaintoutcomeidLookupButton = By.XPath("//*[@id='CWLookupBtn_providercomplaintoutcomeid']");
        readonly By Complaintfeedbackdetails = By.XPath("//*[@id='CWField_complaintfeedbackdetails']");
        readonly By ResponsibleuseridLink = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleuseridClearButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By ResponsibleuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By ProvideridLink = By.XPath("//*[@id='CWField_providerid_Link']");
        readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
        readonly By Freetextmadeby = By.XPath("//*[@id='CWField_freetextmadeby']");
        readonly By ProvidercomplaintnatureidLink = By.XPath("//*[@id='CWField_providercomplaintnatureid_Link']");
        readonly By ProvidercomplaintnatureidClearButton = By.XPath("//*[@id='CWClearLookup_providercomplaintnatureid']");
        readonly By ProvidercomplaintnatureidLookupButton = By.XPath("//*[@id='CWLookupBtn_providercomplaintnatureid']");
        readonly By Resolutionduedate = By.XPath("//*[@id='CWField_resolutionduedate']");
        readonly By ResolutionduedateDatePicker = By.XPath("//*[@id='CWField_resolutionduedate_DatePicker']");
        readonly By Outcomedate = By.XPath("//*[@id='CWField_outcomedate']");
        readonly By OutcomedateDatePicker = By.XPath("//*[@id='CWField_outcomedate_DatePicker']");
        readonly By Investigationdetails = By.XPath("//*[@id='CWField_investigationdetails']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");


        public ComplaintAndFeedbackRecordPage WaitForPageToLoad(string Title = null)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(BackButton);
            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            WaitForElement(pageHeader);

            WaitForElement(Complaintfeedbackdate);

            WaitForElement(MadebyidLookupButton);

            if (!string.IsNullOrEmpty(Title))
                ValidateElementText(pageHeader, "Complaint & Feedback:\r\n" + Title);

            return this;
        }

        public ComplaintAndFeedbackRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(BackButton);
            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            WaitForElement(pageHeader);

            WaitForElement(Complaintfeedbackdate);

            WaitForElement(MadebyidLookupButton);

            return this;
        }



        public ComplaintAndFeedbackRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickRestrictAccessButton()
        {
            WaitForElementToBeClickable(RestrictAccessButton);
            Click(RestrictAccessButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateComplaintFeedbackDateText(string ExpectedText)
        {
            ValidateElementValue(Complaintfeedbackdate, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnComplaintFeedbackDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Complaintfeedbackdate);
            SendKeys(Complaintfeedbackdate, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickComplaintFeedbackDateDatePicker()
        {
            WaitForElementToBeClickable(ComplaintfeedbackdateDatePicker);
            Click(ComplaintfeedbackdateDatePicker);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickMadeByLinkField()
        {
            WaitForElementToBeClickable(MadebyidLink);
            Click(MadebyidLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateMadeByLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(MadebyidLink);
            ValidateElementText(MadebyidLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickMadeByClearButton()
        {
            WaitForElementToBeClickable(MadebyidClearButton);
            Click(MadebyidClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickMadeByLookupButton()
        {
            WaitForElementToBeClickable(MadebyidLookupButton);
            Click(MadebyidLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProviderComplaintFeedbackTypeLink()
        {
            WaitForElementToBeClickable(ProvidercomplaintfeedbacktypeidLink);
            Click(ProvidercomplaintfeedbacktypeidLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateProviderComplaintFeedbackTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvidercomplaintfeedbacktypeidLink);
            ValidateElementText(ProvidercomplaintfeedbacktypeidLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProviderComplaintFeedbackTypeClearButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintfeedbacktypeidClearButton);
            Click(ProvidercomplaintfeedbacktypeidClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProviderComplaintFeedbackTypeLookupButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintfeedbacktypeidLookupButton);
            Click(ProvidercomplaintfeedbacktypeidLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickStageLink()
        {
            WaitForElementToBeClickable(ProvidercomplaintstageidLink);
            Click(ProvidercomplaintstageidLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateStageLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvidercomplaintstageidLink);
            ValidateElementText(ProvidercomplaintstageidLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickStageClearButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintstageidClearButton);
            Click(ProvidercomplaintstageidClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickStageLookupButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintstageidLookupButton);
            Click(ProvidercomplaintstageidLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProvidercomplaintoutcomeidLink()
        {
            WaitForElementToBeClickable(ProvidercomplaintoutcomeidLink);
            Click(ProvidercomplaintoutcomeidLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateComplaintOutcomeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvidercomplaintoutcomeidLink);
            ValidateElementText(ProvidercomplaintoutcomeidLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickComplaintOutcomeClearButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintoutcomeidClearButton);
            Click(ProvidercomplaintoutcomeidClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickComplaintOutcomeLookupButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintoutcomeidLookupButton);
            Click(ProvidercomplaintoutcomeidLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateComplaintFeedbackDetailsText(string ExpectedText)
        {
            ValidateElementText(Complaintfeedbackdetails, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnComplaintFeedbackDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(Complaintfeedbackdetails);
            SendKeys(Complaintfeedbackdetails, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResponsibleUserLink()
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            Click(ResponsibleuseridLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            ValidateElementText(ResponsibleuseridLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResponsibleUserClearButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridClearButton);
            Click(ResponsibleuseridClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridLookupButton);
            Click(ResponsibleuseridLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProviderLink()
        {
            WaitForElementToBeClickable(ProvideridLink);
            Click(ProvideridLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateProviderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvideridLink);
            ValidateElementText(ProvideridLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickProviderLookupButton()
        {
            WaitForElementToBeClickable(ProvideridLookupButton);
            Click(ProvideridLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateFreeTextMadeByText(string ExpectedText)
        {
            ValidateElementValue(Freetextmadeby, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnFreeTextMadeBy(string TextToInsert)
        {
            WaitForElementToBeClickable(Freetextmadeby);
            SendKeys(Freetextmadeby, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickNatureLink()
        {
            WaitForElementToBeClickable(ProvidercomplaintnatureidLink);
            Click(ProvidercomplaintnatureidLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateNatureLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvidercomplaintnatureidLink);
            ValidateElementText(ProvidercomplaintnatureidLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickNatureClearButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintnatureidClearButton);
            Click(ProvidercomplaintnatureidClearButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickNatureLookupButton()
        {
            WaitForElementToBeClickable(ProvidercomplaintnatureidLookupButton);
            Click(ProvidercomplaintnatureidLookupButton);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateResolutionDueDateText(string ExpectedText)
        {
            ValidateElementValue(Resolutionduedate, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnResolutionDueDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Resolutionduedate);
            SendKeys(Resolutionduedate, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResolutionDueDateDatePicker()
        {
            WaitForElementToBeClickable(ResolutionduedateDatePicker);
            Click(ResolutionduedateDatePicker);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateOutcomeDateText(string ExpectedText)
        {
            ValidateElementValue(Outcomedate, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnOutcomeDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Outcomedate);
            SendKeys(Outcomedate, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickOutcomeDateDatePicker()
        {
            WaitForElementToBeClickable(OutcomedateDatePicker);
            Click(OutcomedateDatePicker);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateInvestigationDetailsText(string ExpectedText)
        {
            ValidateElementText(Investigationdetails, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage InsertTextOnInvestigationDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(Investigationdetails);
            SendKeys(Investigationdetails, TextToInsert);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ComplaintAndFeedbackRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

    }
}
