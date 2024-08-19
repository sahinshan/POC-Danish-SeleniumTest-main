using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class OrganisationalRiskCategoryPage : CommonMethods
    {
        public OrganisationalRiskCategoryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By OrganisationalRiskCategoriesPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Organisational Risk Categories']");

        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowFromValue(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By recordRowToValue(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        By RiskCategoryRow(string riskCategory) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][@title = '"+riskCategory+"']");        
        By RiskRowCheckBox(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public OrganisationalRiskCategoryPage WaitForRiskCategoriesManagementPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElement(OrganisationalRiskCategoriesPageHeader);

            WaitForElement(NewRecordButton);
            WaitForElement(DeleteRecordButton);
            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);            

            return this;
        }

        public OrganisationalRiskCategoryPage SearchRiskCategoryRecord(string SearchQuery, string RiskCategory)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(RiskCategoryRow(RiskCategory));

            return this;
        }

        public OrganisationalRiskCategoryPage TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public OrganisationalRiskCategoryPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }



        public OrganisationalRiskCategoryPage SearchRiskCategoryRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }
        public OrganisationalRiskCategoryPage OpenRiskCategoryRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId), 7);
            ScrollToElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public OrganisationalRiskCategoryPage OpenRiskCategoryRecordUsingName(string RiskCategory)
        {
            WaitForElement(RiskCategoryRow(RiskCategory));
            driver.FindElement(RiskCategoryRow(RiskCategory)).Click();

            return this;
        }

        public OrganisationalRiskCategoryPage SelectRiskCategoryRecord(string RiskId)
        {
            WaitForElement(RiskRowCheckBox(RiskId));
            Click(RiskRowCheckBox(RiskId));

            return this;
        }

        public OrganisationalRiskCategoryPage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElement(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public OrganisationalRiskCategoryPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }


        public OrganisationalRiskCategoryPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public OrganisationalRiskCategoryPage ValidateRiskCategoryNameIsAvailable(string riskCategoryId, string expectedRiskCategory)
        {
            WaitForElement(recordRow(riskCategoryId));
            ValidateElementByTitle(recordRow(riskCategoryId), expectedRiskCategory);

            return this;
        }

        public OrganisationalRiskCategoryPage ValidateRiskCategoryNameIsAvailable(string expectedRiskCategory)
        {
            WaitForElement(RiskCategoryRow(expectedRiskCategory));
            ValidateElementByTitle(RiskCategoryRow(expectedRiskCategory), expectedRiskCategory);

            return this;
        }

        public OrganisationalRiskCategoryPage ValidateRecord(string riskCategoryId, string expectedRiskCategory, string valueFrom, string valueTo)
        {
            WaitForElement(recordRow(riskCategoryId));
            WaitForElement(recordRowFromValue(riskCategoryId));
            WaitForElement(recordRowToValue(riskCategoryId));
            
            ValidateElementByTitle(recordRow(riskCategoryId), expectedRiskCategory);
            ValidateElementByTitle(recordRowFromValue(riskCategoryId), valueFrom);
            ValidateElementByTitle(recordRowToValue(riskCategoryId), valueTo);

            return this;
        }
    }
}
