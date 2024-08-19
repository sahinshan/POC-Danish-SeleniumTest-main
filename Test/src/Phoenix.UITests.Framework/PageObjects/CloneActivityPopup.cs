using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CloneActivityPopup : CommonMethods
    {
        public CloneActivityPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_PersonCloneActivity");

        readonly By popupHeader = By.XPath("//h1[@id='CWHeaderText']");

        readonly By notificationMessage = By.XPath("//*[@id='CWNotificationMessage_PersonCloneActivity']");
        readonly By closeNotificationMessageLink = By.XPath("//*[@id='CWNotificationHolder_PersonCloneActivity']/a");

        

        readonly By businessObjectType = By.Id("CWBusinessObjectType");
        readonly By retainStatus = By.Id("CWRetainStatus");

        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");

        By checkboxElement(string ElementID) => By.XPath("//*[@id='CHK_" + ElementID + "']");
        By personIDCell(string ElementID) => By.XPath("//*[@id='" + ElementID + "']/td[4]");
        By personNumber(string personnumber) => By.XPath("//td[@title='"+personnumber +"']/parent::tr/td/input"); 
        readonly By cloneButton = By.Id("CloneButton");
        readonly By closeButton = By.Id("CancelButton");



        public CloneActivityPopup WaitForCloneActivityPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(businessObjectType);
            WaitForElement(retainStatus);
            WaitForElement(gridResultsArea);

            WaitForElement(gridResultsArea);

            return this;
        }

        public CloneActivityPopup SelectBusinessObjectTypeText(string TextToSelect)
        {
            WaitForElementToBeClickable(businessObjectType);
            SelectPicklistElementByText(businessObjectType, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneActivityPopup SelectRetainStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(retainStatus);
            SelectPicklistElementByText(retainStatus, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneActivityPopup SelectRecord(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(checkboxElement(RecordID));
            Click(checkboxElement(RecordID));

            return this;
        }

        public CloneActivityPopup SelectRecordbypersonID(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(personNumber(RecordID));
            Click(personNumber(RecordID));

            return this;
        }

        public CloneActivityPopup ClickCloneButton()
        {
            Click(cloneButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneActivityPopup ClickCloseButton()
        {
            Click(closeButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CloneActivityPopup ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
                WaitForElementVisible(closeNotificationMessageLink);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 7);
                WaitForElementNotVisible(closeNotificationMessageLink, 7);
            }

            return this;
        }

        public CloneActivityPopup ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }


    }
}
