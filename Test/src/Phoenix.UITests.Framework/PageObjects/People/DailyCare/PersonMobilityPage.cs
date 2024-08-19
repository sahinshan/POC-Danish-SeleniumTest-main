using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMobilityPage : CommonMethods
    {
        public PersonMobilityPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); 
        readonly By CWNavItem_MobilityFrame = By.Id("CWUrlPanel_IFrame"); 

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Mobility']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");
        By GridHeaderCell(string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']//*[text()='"+ExpectedText+"']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public PersonMobilityPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_MobilityFrame);
            SwitchToIframe(CWNavItem_MobilityFrame);

            WaitForElement(pageHeader);

            return this;
        }
     
        public PersonMobilityPage ClickNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonMobilityPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonMobilityPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        //validate if record row is displayed or not displayed
        public PersonMobilityPage ValidateRecordIsDisplayed(string RecordId, bool ExpectedVisibilty = true)
        {
            if(ExpectedVisibilty)
                WaitForElementVisible(recordRow(RecordId));
            else
                WaitForElementNotVisible(recordRow(RecordId), 3);

            return this;
        }



        public PersonMobilityPage OpenRecord(Guid RecordId)
        {
            return OpenRecord(RecordId.ToString());
        }

        public PersonMobilityPage ValidateRecordIsDisplayed(Guid RecordId, bool ExpectedVisibilty = true)
        {
            return ValidateRecordIsDisplayed(RecordId.ToString(), ExpectedVisibilty);
        }

        public PersonMobilityPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));

            return this;
        }

        public PersonMobilityPage ValidateHeaderCellNotPresent(string ExpectedText)
        {
            WaitForElementNotVisible(GridHeaderCell(ExpectedText), 3);

            return this;
        }
    }
}
