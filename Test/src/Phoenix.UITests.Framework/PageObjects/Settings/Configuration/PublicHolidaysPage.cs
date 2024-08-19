using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PublicHolidaysPage : CommonMethods
    {
        public PublicHolidaysPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }




        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By PublicHolidaysPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");




        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
        readonly By name_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");





        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRow_NameCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");






        public PublicHolidaysPage WaitForPublicHolidaysPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(PublicHolidaysPageHeader);

            WaitForElement(name_Header);

            return this;
        }

        public PublicHolidaysPage SearchRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_NameCell(recordID));

            return this;
        }

        public PublicHolidaysPage SearchRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PublicHolidaysPage OpenRecord(string recordId)
        {
            WaitForElement(recordRow_NameCell(recordId));
            Click(recordRow_NameCell(recordId));

            return this;
        }

        public PublicHolidaysPage SelectRecord(string recordId)
        {
            WaitForElement(recordRowCheckBox(recordId));
            Click(recordRowCheckBox(recordId));

            return this;
        }

        public PublicHolidaysPage ClickNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PublicHolidaysPage ValidateNameCell(string recordID, string ExpectedText)
        {
            ValidateElementText(recordRow_NameCell(recordID), ExpectedText);
            return this;
        }

        //validate if public holidays record is available
        public PublicHolidaysPage ValidatePublicHolidaysRecordIsAvailable(string recordID, bool ExpectedAvailable)
        {
            if(ExpectedAvailable)
            {
                WaitForElement(recordRow_NameCell(recordID));
            }
            else
            {
                WaitForElementNotVisible(recordRow_NameCell(recordID), 80);
            }
            
            return this;
        }

        public PublicHolidaysPage ValidatePublicHolidaysRecordIsAvailable(Guid recordID, bool ExpectedAvailable)
        {
            return ValidatePublicHolidaysRecordIsAvailable(recordID.ToString(), ExpectedAvailable);
        }

    }
}
