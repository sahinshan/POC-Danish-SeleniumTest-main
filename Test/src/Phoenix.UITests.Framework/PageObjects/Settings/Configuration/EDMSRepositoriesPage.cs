using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EDMSRepositoriesPage : CommonMethods
    {
        public EDMSRepositoriesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By EDMSRepositoriesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");




        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
        readonly By name_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");
        




        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

        By recordRow_NameCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");






        public EDMSRepositoriesPage WaitForEDMSRepositoriesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(EDMSRepositoriesPageHeader);

            WaitForElement(name_Header);

            return this;
        }

        public EDMSRepositoriesPage SearchRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_NameCell(recordID));

            return this;
        }

        public EDMSRepositoriesPage SearchRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public EDMSRepositoriesPage OpenRecord(string recordId)
        {
            WaitForElement(recordRow_NameCell(recordId));
            Click(recordRow_NameCell(recordId));

            return this;
        }

        public EDMSRepositoriesPage SelectRecord(string recordId)
        {
            WaitForElement(recordRowCheckBox(recordId));
            Click(recordRowCheckBox(recordId));

            return this;
        }

        public EDMSRepositoriesPage TapNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }


        public EDMSRepositoriesPage ValidateNameCell(string recordID, string ExpectedText)
        {
            ValidateElementText(recordRow_NameCell(recordID), ExpectedText);
            return this;
        }

    }
}
