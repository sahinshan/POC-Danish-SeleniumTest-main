using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EducationYearsPage : CommonMethods
    {
        public EducationYearsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personattendededucationestablishment&')]");
        readonly By CWNavItem_PersonMHALegalStatusFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Education Years']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public EducationYearsPage WaitForEducationYearsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonMHALegalStatusFrame);
            SwitchToIframe(CWNavItem_PersonMHALegalStatusFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public EducationYearsPage SearchPersonMHALegalStatusRecord(string SearchQuery, string PersonID)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public EducationYearsPage SearchPersonMHALegalStatusRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public EducationYearsPage OpenPersonMHALegalStatusRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public EducationYearsPage SelectPersonMHALegalStatusRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public EducationYearsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
    }
}
