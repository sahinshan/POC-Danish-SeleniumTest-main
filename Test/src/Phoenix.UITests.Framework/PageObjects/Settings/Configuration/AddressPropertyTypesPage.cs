using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AddressPropertyTypesPage : CommonMethods
    {
        public AddressPropertyTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_addresspropertytype = By.Id("iframe_addresspropertytype");

        readonly By AddressPropertyTypesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");




        public AddressPropertyTypesPage WaitForAddressPropertyTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_addresspropertytype);
            SwitchToIframe(iframe_addresspropertytype);

            WaitForElement(AddressPropertyTypesPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public AddressPropertyTypesPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewsPicklist, TextToSelect);

            return this;
        }

        public AddressPropertyTypesPage SearchAddressPropertyTypeRecord(string SearchQuery, string RecordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_CreatedOnCell(RecordID));

            return this;
        }

        public AddressPropertyTypesPage SearchAddressPropertyTypeRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public AddressPropertyTypesPage OpenAddressPropertyTypeRecord(string RecordId)
        {
            WaitForElement(recordRow_CreatedOnCell(RecordId));
            Click(recordRow_CreatedOnCell(RecordId));

            return this;
        }

        public AddressPropertyTypesPage SelectAddressPropertyTypeRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public AddressPropertyTypesPage ValidateRecordVisible(string recordID)
        {
            WaitForElementVisible(recordRowCheckBox(recordID));
            WaitForElementVisible(recordRow_CreatedOnCell(recordID));
            return this;
        }
        


    }
}
