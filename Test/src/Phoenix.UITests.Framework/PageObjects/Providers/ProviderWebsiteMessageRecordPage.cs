using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderWebsiteMessageRecordPage : CommonMethods
    {

        public ProviderWebsiteMessageRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitemessage&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        #region Field Titles

        readonly By fromFieldLabel = By.XPath("//*[@id='CWLabelHolder_fromid']/label");
        readonly By toFieldLabel = By.XPath("//*[@id='CWLabelHolder_toid']/label");
        readonly By regardingFieldLabel = By.XPath("//*[@id='CWLabelHolder_regardingid']/label");
        readonly By readFieldLabel = By.XPath("//*[@id='CWLabelHolder_read']/label");
        readonly By responsibleTeamFieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By messageFieldLabel = By.XPath("//*[@id='CWLabelHolder_message']/label");

        #endregion

        #region Fields

        readonly By fromLinkField = By.XPath("//*[@id='CWField_fromid_Link']");
        readonly By fromLookupButton = By.XPath("//*[@id='CWLookupBtn_fromid']");
        readonly By fromRemoveButton = By.XPath("//*[@id='CWClearLookup_fromid']");

        readonly By toLinkField = By.XPath("//*[@id='CWField_toid_Link']");
        readonly By toLookupButton = By.XPath("//*[@id='CWLookupBtn_toid']");
        readonly By toRemoveButton = By.XPath("//*[@id='CWClearLookup_toid']");

        readonly By regardingLinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By regardingLookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By regardingRemoveButton = By.XPath("//*[@id='CWClearLookup_regardingid']");

        readonly By readFieldYesRadioButton = By.XPath("//*[@id='CWField_read_1']");
        readonly By readFieldNoRadioButton = By.XPath("//*[@id='CWField_read_0']");

        readonly By responsibleTeamLinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By messageField = By.XPath("//*[@id='CWField_message']");

        #endregion




        public ProviderWebsiteMessageRecordPage WaitForProviderWebsiteMessageRecordPageToLoad(string WebsiteMessageTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(fromFieldLabel);
            WaitForElement(toFieldLabel);
            WaitForElement(regardingFieldLabel);
            WaitForElement(readFieldLabel);
            WaitForElement(responsibleTeamFieldLabel);
            WaitForElement(messageFieldLabel);

            return this;
        }


        public ProviderWebsiteMessageRecordPage InsertMessage(string Message)
        {
            SendKeys(messageField, Message);

            return this;
        }


        public ProviderWebsiteMessageRecordPage ClickFromLookupButton()
        {
            Click(fromLookupButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickToLookupButton()
        {
            Click(toLookupButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickRegardingLookupButton()
        {
            Click(regardingLookupButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeamLookupButton);

            return this;
        }


        public ProviderWebsiteMessageRecordPage ClickReadFieldYesRadioButton()
        {
            Click(readFieldYesRadioButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickReadFieldNoRadioButton()
        {
            Click(readFieldNoRadioButton);

            return this;
        }



        public ProviderWebsiteMessageRecordPage ValidateMessageText(string ExpectedText)
        {
            ValidateElementText(messageField, ExpectedText);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ValidateFromLinkFieldText(string ExpectedText)
        {
            ValidateElementText(fromLinkField, ExpectedText);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ValidateToLinkFieldText(string ExpectedText)
        {
            ValidateElementText(toLinkField, ExpectedText);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            ValidateElementText(regardingLinkField, ExpectedText);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeamLinkField, ExpectedText);

            return this;
        }


        public ProviderWebsiteMessageRecordPage ValidateReadYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(readFieldYesRadioButton);
            else
                ValidateElementNotChecked(readFieldYesRadioButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ValidateReadNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(readFieldNoRadioButton);
            else
                ValidateElementNotChecked(readFieldNoRadioButton);

            return this;
        }


        public ProviderWebsiteMessageRecordPage ClickBackButton()
        {
            this.Click(backButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }
        public ProviderWebsiteMessageRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
