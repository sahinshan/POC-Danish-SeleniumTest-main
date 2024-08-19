using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CloningContractServicePopup : CommonMethods
    {
        public CloningContractServicePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservice')]");
        By CWDialogIframe(string RecordId) => By.XPath("//*[@id='iframe_CWDialog_" + RecordId + "']");

        readonly By popupIframe = By.Id("iframe_CloneContractService");
        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderTitle']");

        readonly By cwCloneMessage = By.XPath("//*[@id = 'CWCloneMessage_Container']");

        readonly By EstablishmentLookupButton = By.XPath("//*[@id='CWLookupBtn_EstablishmentLookup']");
        readonly By EstablishmentLinkField = By.XPath("//*[@id='EstablishmentLookup_Link']");
        readonly By ContractSchemeLookupButton = By.XPath("//*[@id='CWLookupBtn_ContractSchemeLookup']");
        By ContractSchemeLink(Guid ContractSchemeId) => By.XPath("//*[@id='CWField_ContractSchemeLookup_List']/li[@id = 'MS_ContractSchemeLookup_" + ContractSchemeId + "'][@lkpid = '" + ContractSchemeId + "']");
        readonly By ContractSchemeLinkField = By.XPath("//*[@id='CWField_ContractSchemeLookup_List']");
        readonly By ServiceLookupButton = By.XPath("//*[@id='CWLookupBtn_ServiceLookup']");
        readonly By ServiceLinkField = By.XPath("//*[@id='ServiceLookup_Link']");
        readonly By ServiceDetailLookupButton = By.XPath("//*[@id='CWLookupBtn_ServiceDetailLookup']");
        By ServiceDetailLink(Guid? ServiceDetailId) => By.XPath("//*[@id='CWField_ServiceDetailLookup_List']/li[@id = 'MS_ServiceDetailLookup_"+ ServiceDetailId + "'][@lkpid = '"+ ServiceDetailId + "']");
        readonly By ServiceDetailLinkField = By.XPath("//*[@id='CWField_ServiceDetailLookup_List']");
        readonly By BatchGroupingLookupButton = By.XPath("//*[@id='CWLookupBtn_BatchGroupingLookup']");
        readonly By BatchGroupingLinkField = By.XPath("//*[@id='BatchGroupingLookup_Link']");
        readonly By CopyRates_YesRadioButton = By.XPath("//*[@id='rdCopyRecordsYes']");
        readonly By CopyRates_NoRadioButton = By.XPath("//*[@id='rdCopyRecordsNo']");


        readonly By confirmationMessage = By.XPath("//*[@id='CWCloneConfirmationMsg']");

        readonly By notificationMessageArea = By.XPath("//*[@id='CWNotificationHolder_CloneContractService']");
        readonly By cloneNotificationMessage = By.XPath("//*[@id='CWNotificationMessage_CloneContractService']");
        readonly By messageHideLink = By.XPath("//*[@id='CWNotificationHolder_CloneContractService']/a");

        readonly By cloneButton = By.Id("CWCopy");
        readonly By cancelButton = By.Id("CWCancel");
        readonly By OpenNewRecordButton = By.XPath("//button[text() = 'Open New Record']");



        public CloningContractServicePopup WaitForPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(popupHeader);

            WaitForElement(cloneButton);
            WaitForElement(cancelButton);

            return this;
        }

        public CloningContractServicePopup WaitForPopupToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIframe(RecordId));
            SwitchToIframe(CWDialogIframe(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(popupHeader);

            WaitForElement(cloneButton);
            WaitForElement(cancelButton);

            return this;
        }

        //CloneButton is visible or not visible
        public CloningContractServicePopup ValidateCloneButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(cloneButton);
                WaitForElementVisible(cloneButton);
            }
            else
                WaitForElementNotVisible(cloneButton, 3);

            return this;
        }

        public CloningContractServicePopup ClickCloneButton()
        {
            ScrollToElement(cloneButton);
            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        //CancelButton is visible or not visible
        public CloningContractServicePopup ValidateCancelButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(cancelButton);
                WaitForElementVisible(cancelButton);
            }
            else
                WaitForElementNotVisible(cancelButton, 3);

            return this;
        }

        public CloningContractServicePopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloningContractServicePopup ClickOpenNewRecordButton()
        {
            WaitForElementToBeClickable(OpenNewRecordButton);
            Click(OpenNewRecordButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        //OpenNewRecordButton is visible or not visible
        public CloningContractServicePopup ValidateOpenNewRecordButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(OpenNewRecordButton);
                WaitForElementVisible(OpenNewRecordButton);
            }                
            else
                WaitForElementNotVisible(OpenNewRecordButton, 3);

            return this;
        }

        public CloningContractServicePopup ValidateCloneMessage(string ExpectedText)
        {
            WaitForElement(cwCloneMessage);
            ScrollToElement(cwCloneMessage);
            ValidateElementText(cwCloneMessage, ExpectedText);

            return this;
        }

        public CloningContractServicePopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(confirmationMessage);

            }
            else
            {
                WaitForElementNotVisible(confirmationMessage, 7);
            }

            return this;
        }

        public CloningContractServicePopup ValidateNotificationMessageText(string ExpectedText)
        {
            WaitForElement(confirmationMessage);
            ScrollToElement(confirmationMessage);
            ValidateElementText(confirmationMessage, ExpectedText);

            return this;
        }


        public CloningContractServicePopup ValidateCloneNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(notificationMessageArea);
                WaitForElementVisible(notificationMessageArea);
                WaitForElementVisible(cloneNotificationMessage);
                WaitForElementVisible(messageHideLink);

            }
            else
            {
                WaitForElementNotVisible(notificationMessageArea, 7);
                WaitForElementNotVisible(cloneNotificationMessage, 7);
                WaitForElementNotVisible(messageHideLink, 7);
            }

            return this;
        }


        public CloningContractServicePopup ValidateCloneNotificationMessageText(string ExpectedText)
        {
            WaitForElement(cloneNotificationMessage);
            ScrollToElement(cloneNotificationMessage);
            ValidateElementTextContainsText(cloneNotificationMessage, ExpectedText);

            return this;
        }


        public CloningContractServicePopup ClickHideMessageLink()
        {
            ScrollToElement(messageHideLink);
            WaitForElementToBeClickable(messageHideLink);
            Click(messageHideLink);

            return this;
        }

        //Establishment Lookup is visible or not visible
        public CloningContractServicePopup ValidateEstablishmentLookupVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EstablishmentLookupButton);
            else
                WaitForElementNotVisible(EstablishmentLookupButton, 7);

            return this;
        }

        //Establishment Lookup Button is disabled or not disabled
        public CloningContractServicePopup ValidateEstablishmentLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(EstablishmentLookupButton);
            else
                ValidateElementNotDisabled(EstablishmentLookupButton);

            return this;
        }

        //Click Establishment Lookup Button
        public CloningContractServicePopup ClickEstablishmentLookupButton()
        {
            WaitForElement(EstablishmentLookupButton);
            ScrollToElement(EstablishmentLookupButton);
            Click(EstablishmentLookupButton);

            return this;
        }

        //Verify EstablishmentLinkField Text
        public CloningContractServicePopup ValidateEstablishmentLinkText(string ExpectedText)
        {
            WaitForElement(EstablishmentLinkField);
            ScrollToElement(EstablishmentLinkField);
            ValidateElementText(EstablishmentLinkField, ExpectedText);

            return this;
        }

        //Contract Scheme Lookup is visible or not visible
        public CloningContractServicePopup ValidateContractSchemeLookupVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ContractSchemeLookupButton);
            else
                WaitForElementNotVisible(ContractSchemeLookupButton, 7);

            return this;
        }

        //Contract Scheme Lookup Button is disabled or not disabled
        public CloningContractServicePopup ValidateContractSchemeLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ContractSchemeLookupButton);
            else
                ValidateElementNotDisabled(ContractSchemeLookupButton);

            return this;
        }

        //Click Contract Scheme Lookup Button
        public CloningContractServicePopup ClickContractSchemeLookupButton()
        {
            WaitForElement(ContractSchemeLookupButton);
            ScrollToElement(ContractSchemeLookupButton);
            Click(ContractSchemeLookupButton);

            return this;
        }

        //Verify ContractSchemeLinkField Text
        public CloningContractServicePopup ValidateContractSchemeLinkText(Guid ContractSchemeId, string ExpectedText)
        {
            WaitForElement(ContractSchemeLink(ContractSchemeId));
            ScrollToElement(ContractSchemeLink(ContractSchemeId));
            ValidateElementTextContainsText(ContractSchemeLink(ContractSchemeId), ExpectedText);

            return this;
        }

        //Validate ContractSchemeId is available in ContractSchemeLinkField
        public CloningContractServicePopup ValidateContractSchemeInContractSchemeLinkField(Guid ContractSchemeId, bool ExpectedVisible)
        {
            WaitForElement(ContractSchemeLinkField);
            ScrollToElement(ContractSchemeLinkField);

            if(ExpectedVisible)
                WaitForElementVisible(ContractSchemeLink(ContractSchemeId));
            else
                WaitForElementNotVisible(ContractSchemeLink(ContractSchemeId), 3);

            return this;
        }

        public CloningContractServicePopup ValidateContractSchemeText(string ExpectedText, bool ExpectedVisible)
        {
            WaitForElement(ContractSchemeLinkField);
            ScrollToElement(ContractSchemeLinkField);

            var actualContractSchemeText = GetElementText(ContractSchemeLinkField);

            if(ExpectedVisible)
                Assert.IsTrue(actualContractSchemeText.Contains(ExpectedText));
            else
                Assert.IsFalse(actualContractSchemeText.Contains(ExpectedText));

            return this;
        }


        //ServiceLookupButton is visible or not visible
        public CloningContractServicePopup ValidateServiceLookupVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceLookupButton);
            else
                WaitForElementNotVisible(ServiceLookupButton, 7);

            return this;
        }

        //ServiceLookupButton is disabled or not disabled
        public CloningContractServicePopup ValidateServiceLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ServiceLookupButton);
            else
                ValidateElementNotDisabled(ServiceLookupButton);

            return this;
        }

        //Click ServiceLookupButton
        public CloningContractServicePopup ClickServiceLookupButton()
        {
            WaitForElement(ServiceLookupButton);
            ScrollToElement(ServiceLookupButton);
            Click(ServiceLookupButton);

            return this;
        }

        //Verify ServiceLinkField Text
        public CloningContractServicePopup ValidateServiceLinkText(string ExpectedText)
        {
            WaitForElement(ServiceLinkField);
            ScrollToElement(ServiceLinkField);
            ValidateElementText(ServiceLinkField, ExpectedText);

            return this;
        }

        //ServiceDetailLookupButton is visible or not visible
        public CloningContractServicePopup ValidateServiceDetailLookupVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceDetailLookupButton);
            else
                WaitForElementNotVisible(ServiceDetailLookupButton, 7);

            return this;
        }

        //ServiceDetailLookupButton is disabled or not disabled
        public CloningContractServicePopup ValidateServiceDetailLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ServiceDetailLookupButton);
            else
                ValidateElementNotDisabled(ServiceDetailLookupButton);

            return this;
        }

        //Click ServiceDetailLookupButton
        public CloningContractServicePopup ClickServiceDetailLookupButton()
        {
            WaitForElement(ServiceDetailLookupButton);
            ScrollToElement(ServiceDetailLookupButton);
            Click(ServiceDetailLookupButton);

            return this;
        }

        //Verify ServiceDetailLink Text
        public CloningContractServicePopup ValidateServiceDetailLinkText(Guid? ServiceDetailId, string ExpectedText)
        {
            WaitForElement(ServiceDetailLink(ServiceDetailId));
            ScrollToElement(ServiceDetailLink(ServiceDetailId));
            ValidateElementTextContainsText(ServiceDetailLink(ServiceDetailId), ExpectedText);

            return this;
        }

        //Verify ServiceDetailLinkField Text
        public CloningContractServicePopup ValidateServiceDetailLinkFieldText(string ExpectedText)
        {
            WaitForElement(ServiceDetailLinkField);
            ScrollToElement(ServiceDetailLinkField);
            ValidateElementText(ServiceDetailLinkField, ExpectedText);

            return this;
        }

        //click BatchGroupingLookupButton
        public CloningContractServicePopup ClickBatchGroupingLookupButton()
        {
            WaitForElement(BatchGroupingLookupButton);
            ScrollToElement(BatchGroupingLookupButton);
            Click(BatchGroupingLookupButton);

            return this;
        }

        //BatchGroupingLookupButton is visible or not visible
        public CloningContractServicePopup ValidateBatchGroupingLookupVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BatchGroupingLookupButton);
            else
                WaitForElementNotVisible(BatchGroupingLookupButton, 7);

            return this;
        }

        //BatchGroupingLookupButton is disabled or not disabled
        public CloningContractServicePopup ValidateBatchGroupingLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(BatchGroupingLookupButton);
            else
                ValidateElementNotDisabled(BatchGroupingLookupButton);

            return this;
        }

        //Verify BatchGroupingLinkField Text
        public CloningContractServicePopup ValidateBatchGroupingLinkText(string ExpectedText)
        {
            WaitForElement(BatchGroupingLinkField);
            ScrollToElement(BatchGroupingLinkField);
            ValidateElementText(BatchGroupingLinkField, ExpectedText);

            return this;
        }

        //CopyRates radio buttons are visible or not visible
        public CloningContractServicePopup ValidateCopyRatesRadioButtonsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(CopyRates_YesRadioButton);
                WaitForElementVisible(CopyRates_YesRadioButton);
                WaitForElementVisible(CopyRates_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(CopyRates_YesRadioButton, 7);
                WaitForElementNotVisible(CopyRates_NoRadioButton, 7);
            }

            return this;
        }

        //CopyRates radio buttons are disabled or not disabled
        public CloningContractServicePopup ValidateCopyRatesRadioButtonsDisabled(bool ExpectDisabled)
        {
            ScrollToElement(CopyRates_YesRadioButton);
            if (ExpectDisabled)
            {
                ValidateElementDisabled(CopyRates_YesRadioButton);
                ValidateElementDisabled(CopyRates_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(CopyRates_YesRadioButton);
                ValidateElementNotDisabled(CopyRates_NoRadioButton);
            }

            return this;
        }

        //Click CopyRates Yes radio button
        public CloningContractServicePopup ClickCopyRatesYesRadioButton()
        {
            ScrollToElement(CopyRates_YesRadioButton);
            WaitForElementToBeClickable(CopyRates_YesRadioButton);
            Click(CopyRates_YesRadioButton);

            return this;
        }

        //Click CopyRates No radio button
        public CloningContractServicePopup ClickCopyRatesNoRadioButton()
        {
            ScrollToElement(CopyRates_NoRadioButton);
            WaitForElementToBeClickable(CopyRates_NoRadioButton);
            Click(CopyRates_NoRadioButton);

            return this;
        }

        //Method to verify if CopyRates Yes Radio button is checked and CopyRates No radio button is not checked or if CopyRates No Radio button is checked and Copy Rates Yes Radio button is not checked
        public CloningContractServicePopup ValidateCopyRatesYesRadioButtonChecked(bool YesChecked)
        {
            if (YesChecked)
            {
                ValidateElementChecked(CopyRates_YesRadioButton);
                ValidateElementNotChecked(CopyRates_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CopyRates_YesRadioButton);
                ValidateElementChecked(CopyRates_NoRadioButton);
            }

            return this;
        }


    }
}
