using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BayOrRoomspage : CommonMethods
    {
        public BayOrRoomspage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By BayOrRoomsIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By InpatientWardIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientward&')]");
        readonly By BayOrRoomspageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Bay/Rooms']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

        By PersonRow(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[2]");
        By PersonRowCheckBox(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");        
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");       
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");




        public BayOrRoomspage WaitForBayOrRoomspageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(InpatientWardIFrame);
            SwitchToIframe(InpatientWardIFrame);

            WaitForElement(BayOrRoomsIFrame);
            SwitchToIframe(BayOrRoomsIFrame);

           // WaitForElement(BayOrRoomspageHeader);

           // ValidateElementText(BayOrRoomspageHeader, "Bay/Rooms");

            return this;
        }

        public BayOrRoomspage SearchBayRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            

            return this;
        }

        public BayOrRoomspage SearchPersonRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ProviderRecordPage OpenBayRecord(string PersonId)
        {
            WaitForElement(PersonRow(PersonId));
            driver.FindElement(PersonRow(PersonId)).Click();

            return new ProviderRecordPage(this.driver, this.Wait, this.appURL);
        }

        public BayOrRoomspage SelectPersonRecord(string PersonId)
        {
            WaitForElement(PersonRowCheckBox(PersonId));
            Click(PersonRowCheckBox(PersonId));

            return this;
        }
       

        public BayOrRoomspage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
       

        public BayOrRoomspage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public BayOrRoomspage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

    }
}
