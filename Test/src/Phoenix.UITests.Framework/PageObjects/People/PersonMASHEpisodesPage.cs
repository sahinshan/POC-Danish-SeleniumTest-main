using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMASHEpisodesPage : CommonMethods
    {
        public PersonMASHEpisodesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_MASHEpisodesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='MASH Episodes']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By BulkCreateButton = By.Id("TI_UploadMultipleButton");






        public PersonMASHEpisodesPage WaitForPersonMASHEpisodesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_MASHEpisodesFrame);
            SwitchToIframe(CWNavItem_MASHEpisodesFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public PersonMASHEpisodesPage SearchPersonMASHEpisodeRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public PersonMASHEpisodesPage SearchPersonMASHEpisodeRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonMASHEpisodesPage OpenPersonMASHEpisodeRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonMASHEpisodesPage SelectPersonMASHEpisodeRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public PersonMASHEpisodesPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public PersonMASHEpisodesPage ClickBulkEditButton()
        {
            Click(BulkEditButton);

            return this;
        }

        public PersonMASHEpisodesPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonMASHEpisodesPage ClickBulkCreateButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(BulkCreateButton);
            Click(BulkCreateButton);

            return this;
        }

        public PersonMASHEpisodesPage ClickQuickSearchButton()
        {
            Click(quickSearchButton);

            return this;
        }
    }
}
