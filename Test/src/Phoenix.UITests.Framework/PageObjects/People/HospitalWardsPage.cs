using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HospitalWardsPage : CommonMethods
    {
        public HospitalWardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By HospitalWardsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Hospital Wards']");

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

        public HospitalWardsPage WaitForHospitalWardsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementVisible(HospitalWardsPageHeader);

            ValidateElementText(HospitalWardsPageHeader, "Hospital Wards");

            return this;
        }

        public HospitalWardsPage SearchPersonRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(PersonRow(PersonID));

            return this;
        }

        public HospitalWardsPage SearchWardRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public HospitalWardsPage OpenWardRecord(string PersonId)
        {
            WaitForElementToBeClickable(PersonRow(PersonId));
            Click(PersonRow(PersonId));

            return this;
        }

        public HospitalWardsPage SelectPersonRecord(string PersonId)
        {
            WaitForElementToBeClickable(PersonRowCheckBox(PersonId));
            Click(PersonRowCheckBox(PersonId));

            return this;
        }

        public HospitalWardsPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public HospitalWardsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public HospitalWardsPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

    }
}
