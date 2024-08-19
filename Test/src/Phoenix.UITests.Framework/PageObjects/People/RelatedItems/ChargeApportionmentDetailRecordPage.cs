using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ChargeApportionmentDetailRecordPage : CommonMethods
    {
        public ChargeApportionmentDetailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_CareproviderChargeApportionmentDetail = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderchargeapportionmentdetail')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.Id("BackButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");

        #region Genereal

        readonly By GeneralSectionTitle = By.XPath("//*[@id='CWSection_General']//span[text()='General']");

        readonly By ChargeApportionment_LabelField = By.XPath("//*[@id='CWLabelHolder_chargeapportionmentid']/label[text()='Charge Apportionment']");
        readonly By ChargeApportionment_MandatoryField = By.XPath("//*[@id='CWLabelHolder_chargeapportionmentid']/label/span[@class='mandatory']");
        readonly By ChargeApportionment_LinkText = By.Id("CWField_chargeapportionmentid_Link");
        readonly By ChargeApportionment_LookupButton = By.Id("CWLookupBtn_chargeapportionmentid");

        readonly By Priority_LabelField = By.XPath("//*[@id='CWLabelHolder_priority']/label[text()='Priority']");
        readonly By Priority_MandatoryField = By.XPath("//*[@id='CWLabelHolder_priority']/label/span[@class='mandatory']");
        readonly By Priority_Field = By.Id("CWField_priority");

        readonly By Balance_LabelField = By.XPath("//*[@id='CWLabelHolder_balance']/label[text()='Balance?']");
        readonly By Balance_MandatoryField = By.XPath("//*[@id='CWLabelHolder_balance']/label/span[@class='mandatory']");
        readonly By Balance_YesOption = By.Id("CWField_balance_1");
        readonly By Balance_NoOption = By.Id("CWField_balance_0");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By Payer_LabelField = By.XPath("//*[@id='CWLabelHolder_payerid']/label[text()='Payer']");
        readonly By Payer_MandatoryField = By.XPath("//*[@id='CWLabelHolder_payerid']/label/span[@class='mandatory']");
        readonly By Payer_LinkText = By.Id("CWField_payerid_Link");
        readonly By Payer_LookupButton = By.Id("CWLookupBtn_payerid");
        readonly By Payer_RemoveButton = By.Id("CWClearLookup_payerid");

        readonly By Amount_LabelField = By.XPath("//*[@id='CWLabelHolder_amount']/label[text()='Amount']");
        readonly By Amount_MandatoryField = By.XPath("//*[@id='CWLabelHolder_amount']/label/span[@class='mandatory']");
        readonly By Amount_Field = By.Id("CWField_amount");

        readonly By Percentage_LabelField = By.XPath("//*[@id='CWLabelHolder_percentage']/label[text()='Percentage']");
        readonly By Percentage_MandatoryField = By.XPath("//*[@id='CWLabelHolder_percentage']/label/span[@class='mandatory']");
        readonly By Percentage_Field = By.Id("CWField_percentage");

        #endregion

        public ChargeApportionmentDetailRecordPage WaitForChargeApportionmentDetailRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog_CareproviderChargeApportionmentDetail);
            SwitchToIframe(iframe_CWDialog_CareproviderChargeApportionmentDetail);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);

            return this;
        }
        public ChargeApportionmentDetailRecordPage WaitForChargeApportionmentDetailRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(iframe_CWDialog_CareproviderChargeApportionmentDetail);
            SwitchToIframe(iframe_CWDialog_CareproviderChargeApportionmentDetail);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateAllFieldsOfGeneralSection()
        {
            WaitForElementVisible(GeneralSectionTitle);
            ScrollToElement(GeneralSectionTitle);

            WaitForElementVisible(ChargeApportionment_LabelField);
            WaitForElementVisible(ChargeApportionment_LinkText);
            WaitForElementVisible(ChargeApportionment_LookupButton);

            WaitForElementVisible(Priority_LabelField);
            WaitForElementVisible(Priority_Field);

            WaitForElementVisible(Balance_LabelField);
            WaitForElementVisible(Balance_YesOption);
            WaitForElementVisible(Balance_NoOption);

            WaitForElementVisible(ResponsibleTeam_LabelField);
            WaitForElementVisible(ResponsibleTeam_LinkText);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            WaitForElementVisible(Payer_LabelField);
            WaitForElementVisible(Payer_LookupButton);

            WaitForElementVisible(Amount_LabelField);
            WaitForElementVisible(Amount_Field);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Charge Apportionment Detail:\r\n" + ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditSubMenuLink);
            Click(auditSubMenuLink);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            ScrollToElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage InsertPriority(string Priority)
        {
            WaitForElementToBeClickable(Priority_Field);
            SendKeys(Priority_Field, Priority + Keys.Tab);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ClickPayerLookupButton()
        {
            WaitForElementToBeClickable(Payer_LookupButton);
            ScrollToElement(Payer_LookupButton);
            Click(Payer_LookupButton);

            return this;
        }

        public ChargeApportionmentDetailRecordPage InsertAmount(string Amount)
        {
            WaitForElementToBeClickable(Amount_Field);
            SendKeys(Amount_Field, Amount + Keys.Tab);

            return this;
        }

        public ChargeApportionmentDetailRecordPage InsertPercentage(string Percentage)
        {
            WaitForElementToBeClickable(Percentage_Field);
            SendKeys(Percentage_Field, Percentage + Keys.Tab);

            return this;
        }

        public ChargeApportionmentDetailRecordPage SelectBalanceOption(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(Balance_YesOption);
                Click(Balance_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(Balance_NoOption);
                Click(Balance_NoOption);
            }

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateChargeApportionmentMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ChargeApportionment_LabelField);
            ScrollToElement(ChargeApportionment_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ChargeApportionment_MandatoryField);
            else
                WaitForElementNotVisible(ChargeApportionment_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePriorityMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Priority_LabelField);
            ScrollToElement(Priority_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Priority_MandatoryField);
            else
                WaitForElementNotVisible(Priority_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateBalanceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Balance_LabelField);
            ScrollToElement(Balance_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Balance_MandatoryField);
            else
                WaitForElementNotVisible(Balance_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            ScrollToElement(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePayerMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Payer_LabelField);
            ScrollToElement(Payer_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Payer_MandatoryField);
            else
                WaitForElementNotVisible(Payer_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateAmountMandatoryFieldVisibility(bool ExpectVisible)
        {

            if (ExpectVisible)
            {
                WaitForElementVisible(Amount_LabelField);
                ScrollToElement(Amount_LabelField);

            }
            else
            {
                WaitForElementNotVisible(Amount_MandatoryField, 3);
            }

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePercentageMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Percentage_LabelField);
            ScrollToElement(Percentage_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Percentage_MandatoryField);
            else
                WaitForElementNotVisible(Percentage_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateChargeApportionmentLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ChargeApportionment_LinkText);
            ScrollToElement(ChargeApportionment_LinkText);
            ValidateElementText(ChargeApportionment_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkText);
            ScrollToElement(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePayerLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Payer_LinkText);
            ScrollToElement(Payer_LinkText);
            ValidateElementText(Payer_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePriorityFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(Priority_Field);
            ScrollToElement(Priority_Field);
            ValidateElementValue(Priority_Field, ExpectedValue);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateAmountFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(Amount_Field);
            ScrollToElement(Amount_Field);
            ValidateElementValue(Amount_Field, ExpectedValue);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePercentageFieldValue(string ExpectedValue)
        {
            WaitForElementToBeClickable(Percentage_Field);
            ScrollToElement(Percentage_Field);
            ValidateElementValue(Percentage_Field, ExpectedValue);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateBalance_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(Balance_YesOption);
            WaitForElementVisible(Balance_NoOption);
            ScrollToElement(Balance_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(Balance_YesOption);
                ValidateElementNotChecked(Balance_NoOption);
            }
            else
            {
                ValidateElementChecked(Balance_NoOption);
                ValidateElementNotChecked(Balance_YesOption);
            }

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateRangeOfPriorityField(string expectedRange)
        {
            WaitForElementVisible(Priority_Field);
            ValidateElementAttribute(Priority_Field, "range", expectedRange);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateRangeOfAmountField(string expectedRange)
        {
            WaitForElementVisible(Amount_Field);
            ValidateElementAttribute(Amount_Field, "range", expectedRange);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidatePercentageOfAmountField(string expectedRange)
        {
            WaitForElementVisible(Percentage_Field);
            ValidateElementAttribute(Percentage_Field, "range", expectedRange);

            return this;
        }

        public ChargeApportionmentDetailRecordPage ValidateToobarButtonIsPresent()
        {
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        //validate responsible team lookup button is disabled
        public ChargeApportionmentDetailRecordPage ValidateResponsibleTeamLookupButtonIsDisabled()
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            ValidateElementAttribute(ResponsibleTeam_LookupButton, "disabled", "true");

            return this;
        }

        //validate charge appointment lookup button is disabled
        public ChargeApportionmentDetailRecordPage ValidateChargeApportionmentLookupButtonIsDisabled()
        {
            WaitForElementVisible(ChargeApportionment_LookupButton);
            ValidateElementAttribute(ChargeApportionment_LookupButton, "disabled", "true");

            return this;
        }

    }
}
