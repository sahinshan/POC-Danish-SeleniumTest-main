using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ApprovedCareTypesPage : CommonMethods
    {

        public ApprovedCareTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        readonly By relatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By approvedCareTypePageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Approved Care Types']");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By ApprovedCareTypesRow(string ServiceProvidedID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[2]");
        By ApprovedCareTypeRowCheckBox(string ServiceProvidedID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[1]/input");
        By ApprovedCareTypeRecordCell(string ServiceProvidedID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ServiceProvidedID + "']/td[" + CellPosition + "]");



        public ApprovedCareTypesPage WaitForApprovedCareTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(relatedRecordsPanelIFrame);            
            SwitchToIframe(relatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(approvedCareTypePageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ApprovedCareTypesPage ClickBackButton()
        {
            MoveToElementInPage(backButton);
            WaitForElementToBeClickable(backButton);            
            Click(backButton);

            return this;
        }

        public ApprovedCareTypesPage ClickRefreshButton()
        {
            MoveToElementInPage(refreshButton);
            WaitForElementToBeClickable(refreshButton);            
            Click(refreshButton);
            return this;
        }


        public ApprovedCareTypesPage SearchApprovedCareTypeRecord(string SearchQuery)
        {
            MoveToElementInPage(quickSearchTextBox);
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            return this;
        }


        public ApprovedCareTypesPage OpenApprovedCareTypeRecord(string ApprovedCareTypeId)
        {
            MoveToElementInPage(ApprovedCareTypesRow(ApprovedCareTypeId));
            WaitForElementToBeClickable(ApprovedCareTypesRow(ApprovedCareTypeId));            
            Click(ApprovedCareTypesRow(ApprovedCareTypeId));

            return this;
        }

        public ApprovedCareTypesPage SelectApprovedCareTypeRecord(string ApprovedCareTypeId)
        {
            MoveToElementInPage(ApprovedCareTypeRowCheckBox(ApprovedCareTypeId));
            WaitForElement(ApprovedCareTypeRowCheckBox(ApprovedCareTypeId));            
            Click(ApprovedCareTypeRowCheckBox(ApprovedCareTypeId));

            return this;
        }

        public ApprovedCareTypesPage DeleteApprovedCareTypeRecord()
        {
            MoveToElementInPage(DeleteRecordButton);
            WaitForElementToBeClickable(DeleteRecordButton);            
            Click(DeleteRecordButton);
            return this;
        }

        public ApprovedCareTypesPage SelectAvailableViewByText(string PicklistText)
        {
            MoveToElementInPage(viewsPicklist);            
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            return this;
        }

        public ApprovedCareTypesPage ClickNewRecordButton()
        {
            MoveToElementInPage(NewRecordButton);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ApprovedCareTypesPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(ApprovedCareTypesRow(RecordId));
            MoveToElementInPage(ApprovedCareTypesRow(RecordId));
            Assert.IsTrue(GetElementVisibility(ApprovedCareTypesRow(RecordId)));

            return this;
        }

        public ApprovedCareTypesPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(ApprovedCareTypesRow(RecordId), 7);
            Assert.IsFalse(GetElementVisibility(ApprovedCareTypesRow(RecordId)));

            return this;
        }

        public ApprovedCareTypesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(ApprovedCareTypeRecordCell(RecordID, CellPosition));
            ValidateElementText(ApprovedCareTypeRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}
