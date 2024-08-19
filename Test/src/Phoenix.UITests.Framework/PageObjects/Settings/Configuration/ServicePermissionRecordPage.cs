using OpenQA.Selenium;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServicePermissionRecordPage : CommonMethods
    {
        public ServicePermissionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialog_ServicePermissionFrame = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src,'type=servicepermission&')]");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");


        readonly By ServicePermissionRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By shareRecord_Button = By.Id("TI_ShareRecordButton");
        readonly By assignRecord_Button = By.Id("TI_AssignRecordButton");
        readonly By back_Button = By.XPath("//button[@title = 'Back']");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By ServiceElement1FieldLabel = By.Id("CWLabelHolder_serviceelement1id");
        readonly By ServiceElement1FieldLink = By.Id("CWField_serviceelement1id_Link");
        readonly By ServiceElement1FieldLookupButton = By.Id("CWLookupBtn_serviceelement1id");

        readonly By TeamFieldLabel = By.Id("CWLabelHolder_teamid");
        readonly By TeamFieldLink = By.Id("CWField_teamid_Link");
        readonly By TeamFieldLookupButton = By.Id("CWLookupBtn_teamid");
        readonly By TeamFieldErrorMessage = By.XPath("//*[@id = 'CWControlHolder_teamid']//span");

        readonly By ResponsibleTeamFieldLabel = By.Id("CWLabelHolder_ownerid");
        readonly By ResponsibleTeamFieldLink = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeamFieldLookupButton = By.Id("CWLookupBtn_ownerid");
        

        public ServicePermissionRecordPage WaitForServicePermissionRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialog_ServicePermissionFrame);
            SwitchToIframe(cwDialog_ServicePermissionFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(ServicePermissionRecordPageHeader);
            WaitForElement(ServiceElement1FieldLabel);
            WaitForElement(ServiceElement1FieldLink);
            WaitForElement(ServiceElement1FieldLookupButton);
            WaitForElement(TeamFieldLabel);
            WaitForElement(ResponsibleTeamFieldLabel);

            return this;
        }


        public ServicePermissionRecordPage ClickSaveButton()
        {
            MoveToElementInPage(save_Button);
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);

            return this;
        }

        public ServicePermissionRecordPage ClickBackButton()
        {
            MoveToElementInPage(back_Button);
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);

            return this;
        }

        public ServicePermissionRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServicePermissionRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementVisible(shareRecord_Button);
            WaitForElementVisible(assignRecord_Button);
            
            return this;
        }

        public ServicePermissionRecordPage ValidateRecordTitle(string PageTitle)
        {

            MoveToElementInPage(ServicePermissionRecordPageHeader);
            ValidateElementTextContainsText(ServicePermissionRecordPageHeader, "Service Permission:\r\n" + PageTitle);

            return this;
        }

        public ServicePermissionRecordPage ValidateServiceElement1FieldVisible()
        {
            Assert.IsTrue(GetElementVisibility(ServiceElement1FieldLabel));
            Assert.IsTrue(GetElementVisibility(ServiceElement1FieldLookupButton));            

            return this;
        }

        public ServicePermissionRecordPage ValidateTeamFieldVisible()
        {
            Assert.IsTrue(GetElementVisibility(TeamFieldLabel));
            Assert.IsTrue(GetElementVisibility(TeamFieldLookupButton));            

            return this;
        }

        public ServicePermissionRecordPage ValidateResponsibleTeamFieldVisible()
        {
            Assert.IsTrue(GetElementVisibility(ResponsibleTeamFieldLabel));
            Assert.IsTrue(GetElementVisibility(ResponsibleTeamFieldLookupButton));            

            return this;
        }

        public ServicePermissionRecordPage ClickTeamLookUpButton()
        {
            WaitForElementToBeClickable(TeamFieldLookupButton);
            MoveToElementInPage(TeamFieldLookupButton);
            Click(TeamFieldLookupButton);

            return this;
        }

        public ServicePermissionRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamFieldLookupButton);
            MoveToElementInPage(ResponsibleTeamFieldLookupButton);
            Click(ResponsibleTeamFieldLookupButton);

            return this;
        }

        public ServicePermissionRecordPage ValidateServiceElement1FieldDisabled(bool ExpectedStatus)
        {
            WaitForElementVisible(ServiceElement1FieldLookupButton);
            MoveToElementInPage(ServiceElement1FieldLookupButton);
            if (ExpectedStatus)
            {
                ValidateElementDisabled(ServiceElement1FieldLookupButton);
            }
            else
            {
                ValidateElementNotDisabled(ServiceElement1FieldLookupButton);
            }
            return this;
        }

        public ServicePermissionRecordPage ValidateTeamFieldDisabled(bool ExpectedStatus)
        {
            WaitForElementVisible(TeamFieldLookupButton);
            MoveToElementInPage(TeamFieldLookupButton);
            if (ExpectedStatus)
            {
                ValidateElementDisabled(TeamFieldLookupButton);
            }
            else
            {
                ValidateElementNotDisabled(TeamFieldLookupButton);
            }
            return this;
        }

        public ServicePermissionRecordPage ValidateResponsibleTeamFieldDisabled(bool ExpectedStatus)
        {
            WaitForElementVisible(ResponsibleTeamFieldLookupButton);
            MoveToElementInPage(ResponsibleTeamFieldLookupButton);
            if (ExpectedStatus)
            {
                ValidateElementDisabled(ResponsibleTeamFieldLookupButton);
            }
            else
            {
                ValidateElementNotDisabled(ResponsibleTeamFieldLookupButton);
            }
            return this;
        }

        public ServicePermissionRecordPage ValidateServiceElement1FieldLinkText(String ExpectedString)
        {
            WaitForElementVisible(ServiceElement1FieldLink);
            MoveToElementInPage(ServiceElement1FieldLink);
            ValidateElementAttribute(ServiceElement1FieldLink, "title", ExpectedString);            

            return this;
        }

        public ServicePermissionRecordPage ValidateTeamFieldLinkText(String ExpectedString)
        {
            WaitForElementVisible(TeamFieldLink);
            MoveToElementInPage(TeamFieldLink);
            ValidateElementAttribute(TeamFieldLink, "title", ExpectedString);

            return this;
        }

        public ServicePermissionRecordPage ValidateResponsibleTeamFieldLinkText(String ExpectedString)
        {
            WaitForElementVisible(ResponsibleTeamFieldLink);
            MoveToElementInPage(ResponsibleTeamFieldLink);
            ValidateElementAttribute(ResponsibleTeamFieldLink, "title", ExpectedString);

            return this;
        }

        public ServicePermissionRecordPage ValidateNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(NotificationMessage);
            MoveToElementInPage(NotificationMessage);
            Assert.AreEqual(ExpectedText, GetElementText(NotificationMessage));

            return this;
        }

        public ServicePermissionRecordPage ValidateTeamFieldErrorNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(TeamFieldErrorMessage);
            MoveToElementInPage(TeamFieldErrorMessage);
            ValidateElementByTitle(TeamFieldErrorMessage, ExpectedText);

            return this;
        }
    }
}
