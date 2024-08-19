using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServicePermissionsPage : CommonMethods
    {
        public ServicePermissionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceElement1 = By.Id("iframe_serviceelement1");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src,'type=serviceelement1&')]");
        readonly By iframe_UrlPanel = By.Id("CWUrlPanel_IFrame");

        readonly By ServicePermissionsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text() = 'Service Permissions']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        
        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By recordRowCell(int RowPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RowPosition + "]/td[2]");

        readonly By TeamHeaderElementSortOrded = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='teamid_cwname']/a/span[2]");
        readonly By ServiceElement1HeaderElementSortOrded = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='serviceelement1id_cwname']/a/span[2]");



        public ServicePermissionsPage WaitForServicePermissionPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ServiceElement1);
            SwitchToIframe(iframe_ServiceElement1);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(iframe_UrlPanel);
            SwitchToIframe(iframe_UrlPanel);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServicePermissionsPageHeader);
            WaitForElementVisible(viewSelector);

            return this;
        }

        public ServicePermissionsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public ServicePermissionsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            MoveToElementInPage(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);
            return this;
        }

        public ServicePermissionsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            MoveToElementInPage(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ServicePermissionsPage TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);

            return this;
        }


        public ServicePermissionsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(searchButton);

            return this;
        }

        public ServicePermissionsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServicePermissionsPage SelectServicePermissionRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServicePermissionsPage ValidateRecordCellContent(int RowPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RowPosition));
            MoveToElementInPage(recordRowCell(RowPosition));
            ValidateElementText(recordRowCell(RowPosition), ExpectedText);

            return this;
        }

        public ServicePermissionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public ServicePermissionsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ServicePermissionsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }


        public ServicePermissionsPage ValidateRecordIsVisible(string RecordID, bool ExpectedVisibility)
        {
            bool ActualVisibility;
            if (ExpectedVisibility)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                MoveToElementInPage(RecordIdentifier(RecordID));
                ActualVisibility = GetElementVisibility(RecordIdentifier(RecordID));
                Assert.IsTrue(ActualVisibility);
            }
            else
            {
                WaitForElementNotVisible(RecordIdentifier(RecordID), 10);
                ActualVisibility = GetElementVisibility(RecordIdentifier(RecordID));
                Assert.IsFalse(ActualVisibility);
            }

            return this;
        }

        public ServicePermissionsPage ValidateTeamHeaderCellSortOrdedAscending()
        {
            WaitForElementVisible(TeamHeaderElementSortOrded);
            ValidateElementAttribute(TeamHeaderElementSortOrded, "class", "sortasc");

            return this;
        }

        public ServicePermissionsPage ValidateServiceElement1HeaderCellSortOrdedAscending()
        {
            WaitForElementVisible(ServiceElement1HeaderElementSortOrded);
            ValidateElementAttribute(ServiceElement1HeaderElementSortOrded, "class", "sortasc");

            return this;
        }

    }
}
