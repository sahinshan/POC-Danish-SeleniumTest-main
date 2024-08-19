using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CloneAttachmentsPopup : CommonMethods
    {
        public CloneAttachmentsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CloneAttachments");

        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderText']");

        readonly By notificationMessage = By.XPath("//*[@id='CWNotificationMessage_PersonCloneAttachment']");



        readonly By businessObjectTypeField = By.Id("CWBusinessObjectType");
        readonly By startDateField = By.Id("CWField_startdate");
        readonly By startDateErrorLabel = By.XPath("//label[@for='CWField_startdate'][@class='formerror']/span");

        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");

        By checkboxElement(string ElementID) => By.XPath("//*[@id='CHK_" + ElementID + "']");
        By personIDCell(string ElementID) => By.XPath("//*[@id='" + ElementID + "']/td[2]");

        readonly By cloneButton = By.Id("CloneButton");
        readonly By closeButton = By.Id("CloseButton");



        public CloneAttachmentsPopup WaitForCloneAttachmentsPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(businessObjectTypeField);
            WaitForElement(startDateField);
            WaitForElement(gridResultsArea);

            WaitForElement(gridResultsArea);

            return this;
        }

        public CloneAttachmentsPopup SelectBusinessObjectTypeText(string TextToSelect)
        {
            WaitForElementToBeClickable(businessObjectTypeField);
            SelectPicklistElementByText(businessObjectTypeField, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneAttachmentsPopup InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(startDateField);
            SendKeys(startDateField, TextToInsert);

            return this;
        }

        public CloneAttachmentsPopup SelectRecord(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(checkboxElement(RecordID));
            Click(checkboxElement(RecordID));

            return this;
        }

        public CloneAttachmentsPopup ClickCloneButton()
        {
            Click(cloneButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneAttachmentsPopup ClickCloseButton()
        {
            Click(closeButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneAttachmentsPopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 7);
            }

            return this;
        }

        public CloneAttachmentsPopup ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public CloneAttachmentsPopup ValidateStartDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(startDateErrorLabel);

            }
            else
            {
                WaitForElementNotVisible(startDateErrorLabel, 7);
            }

            return this;
        }

        public CloneAttachmentsPopup ValidateStartDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(startDateErrorLabel, ExpectedText);

            return this;
        }


    }
}
