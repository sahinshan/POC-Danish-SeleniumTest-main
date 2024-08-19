using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SecurityProfilesPage : CommonMethods
    {
        public SecurityProfilesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By CWRelatedRecordPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By gridContent = By.XPath("//div[@id='CWWrapper']//div[@id='CWGridWrapper']/div[@class='grid-holder']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        By recordSecurityProfile_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3]");

        By recordCell(string CellText) => By.XPath("//*[@id='CWGridHolder']/table/tbody/tr/td[text()='" + CellText + "']");

        

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public SecurityProfilesPage WaitForSecurityProfilesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            this.ValidateElementText(pageHeader, "Security Profiles");

            return this;
        }

        public SecurityProfilesPage WaitForSystemUserSecurityProfilesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(recordIFrame);
            this.SwitchToIframe(recordIFrame);

            this.WaitForElement(CWRelatedRecordPanelIFrame);
            this.SwitchToIframe(CWRelatedRecordPanelIFrame);

            this.WaitForElement(pageHeader);
            this.WaitForElement(gridContent);

            this.ValidateElementText(pageHeader, "User Security Profiles");

            return this;
        }

        public SecurityProfilesPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public SecurityProfilesPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        
        public SecurityProfilesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public SecurityProfilesPage ValidateSecurityProfileCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordSecurityProfile_cell(RecordID), ExpectedText);

            return this;
        }

        public SecurityProfilesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordMessage, 5);
            }
            return this;
        }

        public SecurityProfilesPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }

        public SecurityProfilesPage ValidateTextNotPresentInResultsGrid(string CellText)
        {
            WaitForElementNotVisible(recordCell(CellText), 3);

            return this;
        }


    }
}
