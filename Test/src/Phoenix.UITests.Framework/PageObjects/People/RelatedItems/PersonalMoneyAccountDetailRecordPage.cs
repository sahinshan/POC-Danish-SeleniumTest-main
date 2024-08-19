using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonalMoneyAccountDetailRecordPage : CommonMethods
    {
        public PersonalMoneyAccountDetailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personAllegationIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersonalmoneyaccountdetail&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");

        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By attachmentsSubMenuButton = By.XPath("//*[@id='CWNavItem_Attachments']");

        readonly By CareproviderpersonalmoneyaccountidLink = By.XPath("//*[@id='CWField_careproviderpersonalmoneyaccountid_Link']");
        readonly By CareproviderpersonalmoneyaccountidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpersonalmoneyaccountid']");
        readonly By Date = By.XPath("//*[@id='CWField_date']");
        readonly By DateDatePicker = By.XPath("//*[@id='CWField_date_DatePicker']");
        readonly By Entrytypeid_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_entrytypeid']/label/span");
        readonly By EntrytypeidLink = By.XPath("//*[@id='CWField_entrytypeid_Link']");
        readonly By EntrytypeidClearButton = By.XPath("//*[@id='CWClearLookup_entrytypeid']");
        readonly By EntrytypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_entrytypeid']");
        readonly By Amount_FieldLabel = By.XPath("//*[@id='CWLabelHolder_amount']/label");
        readonly By Amount_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_amount']/div/label/span");
        readonly By Amount = By.XPath("//*[@id='CWField_amount']");
        readonly By Cashtakenbyid_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_cashtakenbyid']/label/span");
        readonly By CashtakenbyidLookupButton = By.XPath("//*[@id='CWLookupBtn_cashtakenbyid']");
        readonly By Cancellation_1 = By.XPath("//*[@id='CWField_cancellation_1']");
        readonly By Cancellation_0 = By.XPath("//*[@id='CWField_cancellation_0']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Accountdetailnumber = By.XPath("//*[@id='CWField_accountdetailnumber']");
        readonly By Reference = By.XPath("//*[@id='CWField_reference']");
        readonly By Runningbalance = By.XPath("//*[@id='CWField_runningbalance']");
        readonly By Runningbalance_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_runningbalance']/div/label/span");
        readonly By Observedbyid_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_observedbyid']/label/span");
        readonly By ObservedbyidLookupButton = By.XPath("//*[@id='CWLookupBtn_observedbyid']");
        readonly By ObservedbyidClearButton = By.XPath("//*[@id='CWClearLookup_observedbyid']");


        public PersonalMoneyAccountDetailRecordPage WaitForPersonalMoneyAccountDetailRecordPageToLoad(bool AssignRecordButtonVisible = true, bool RunOnDemandWorkflowVisible = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(personAllegationIFrame);
            SwitchToIframe(personAllegationIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(BackButton);

            if (AssignRecordButtonVisible)
                WaitForElementVisible(AssignRecordButton);
            else
                WaitForElementNotVisible(AssignRecordButton, 3);

            if (RunOnDemandWorkflowVisible)
                WaitForElementVisible(RunOnDemandWorkflow);
            else
                WaitForElementNotVisible(RunOnDemandWorkflow, 3);
            WaitForElementVisible(CareproviderpersonalmoneyaccountidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickRunOnDemandWorkflow()
        {
            WaitForElementToBeClickable(RunOnDemandWorkflow);
            Click(RunOnDemandWorkflow);

            return this;
        }


        public PersonalMoneyAccountDetailRecordPage NavigateToAttachmentsPage()
        {
            WaitForElementToBeClickable(menuButton);
            Click(menuButton);

            WaitForElementToBeClickable(attachmentsSubMenuButton);
            Click(attachmentsSubMenuButton);

            return this;
        }



        public PersonalMoneyAccountDetailRecordPage ClickPersonalMoneyAccountLink()
        {
            WaitForElementToBeClickable(CareproviderpersonalmoneyaccountidLink);
            Click(CareproviderpersonalmoneyaccountidLink);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidatePersonalMoneyAccountLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderpersonalmoneyaccountidLink);
            ValidateElementText(CareproviderpersonalmoneyaccountidLink, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickPersonalMoneyAccountLookupButton()
        {
            WaitForElementToBeClickable(CareproviderpersonalmoneyaccountidLookupButton);
            Click(CareproviderpersonalmoneyaccountidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateDateText(string ExpectedText)
        {
            ValidateElementValue(Date, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage InsertTextOnDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Date);
            SendKeys(Date, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickDateDatePicker()
        {
            WaitForElementToBeClickable(DateDatePicker);
            Click(DateDatePicker);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickEntryTypeLink()
        {
            WaitForElementToBeClickable(EntrytypeidLink);
            Click(EntrytypeidLink);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateEntryTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(EntrytypeidLink);
            ValidateElementText(EntrytypeidLink, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickEntryTypeClearButton()
        {
            WaitForElementToBeClickable(EntrytypeidClearButton);
            Click(EntrytypeidClearButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickEntryTypeLookupButton()
        {
            WaitForElementToBeClickable(EntrytypeidLookupButton);
            Click(EntrytypeidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateEntryTypeFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Entrytypeid_FieldErrorLabel);
            ValidateElementText(Entrytypeid_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateEntryTypeFieldErrorLabelNotVisible()
        {
            WaitForElementNotVisible(Entrytypeid_FieldErrorLabel, 3);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateEntryTypeLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(EntrytypeidLookupButton);
            else
                ValidateElementNotDisabled(EntrytypeidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateAmountFieldLabelToolTip(string ExpectedTooltip)
        {
            ValidateElementAttribute(Amount_FieldLabel, "title", ExpectedTooltip);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateAmountFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Amount_FieldErrorLabel);
            ValidateElementText(Amount_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateAmountFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(Amount_FieldErrorLabel);
            else
                WaitForElementNotVisible(Amount_FieldErrorLabel, 3);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateAmountText(string ExpectedText)
        {
            ValidateElementValue(Amount, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage InsertTextOnAmount(string TextToInsert)
        {
            WaitForElementToBeClickable(Amount);
            SendKeys(Amount, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateAmountFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Amount);
            else
                ValidateElementNotDisabled(Amount);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickCashTakenByLookupButton()
        {
            WaitForElementToBeClickable(CashtakenbyidLookupButton);
            Click(CashtakenbyidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCashTakenByFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Cashtakenbyid_FieldErrorLabel);
            ValidateElementText(Cashtakenbyid_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCashTakenByFieldErrorLabelNotVisible()
        {
            WaitForElementNotVisible(Cashtakenbyid_FieldErrorLabel, 3);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickCancellation_YesRadioButton()
        {
            WaitForElementToBeClickable(Cancellation_1);
            Click(Cancellation_1);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCancellation_YesRadioButtonChecked()
        {
            WaitForElement(Cancellation_1);
            ValidateElementChecked(Cancellation_1);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCancellation_YesRadioButtonNotChecked()
        {
            WaitForElement(Cancellation_1);
            ValidateElementNotChecked(Cancellation_1);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickCancellation_NoRadioButton()
        {
            WaitForElementToBeClickable(Cancellation_0);
            Click(Cancellation_0);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCancellation_NoRadioButtonChecked()
        {
            WaitForElement(Cancellation_0);
            ValidateElementChecked(Cancellation_0);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateCancellation_NoRadioButtonNotChecked()
        {
            WaitForElement(Cancellation_0);
            ValidateElementNotChecked(Cancellation_0);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateIDText(string ExpectedText)
        {
            ValidateElementValue(Accountdetailnumber, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage InsertTextOnID(string TextToInsert)
        {
            WaitForElementToBeClickable(Accountdetailnumber);
            SendKeys(Accountdetailnumber, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateReferenceText(string ExpectedText)
        {
            ValidateElementValue(Reference, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage InsertTextOnReference(string TextToInsert)
        {
            WaitForElementToBeClickable(Reference);
            SendKeys(Reference, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateRunningBalanceText(string ExpectedText)
        {
            ValidateElementValue(Runningbalance, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateRunningBalanceFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Runningbalance_FieldErrorLabel);
            ValidateElementText(Runningbalance_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage InsertTextOnRunningBalance(string TextToInsert)
        {
            WaitForElementToBeClickable(Runningbalance);
            SendKeys(Runningbalance, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateRunningBalanceFieldDisabled(bool ExpectDisabled)
        {
            if(ExpectDisabled)
                ValidateElementDisabled(Runningbalance);
            else
                ValidateElementNotDisabled(Runningbalance);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickObservedByLookupButton()
        {
            WaitForElementToBeClickable(ObservedbyidLookupButton);
            Click(ObservedbyidLookupButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ClickObservedByClearButton()
        {
            WaitForElementToBeClickable(ObservedbyidClearButton);
            Click(ObservedbyidClearButton);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateObservedByFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(Observedbyid_FieldErrorLabel);
            ValidateElementText(Observedbyid_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailRecordPage ValidateObservedByFieldErrorLabelNotVIislbe()
        {
            WaitForElementNotVisible(Observedbyid_FieldErrorLabel, 3);

            return this;
        }
    }
}
