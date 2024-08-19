using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceGLCodingsPage : CommonMethods
    {
        public ServiceGLCodingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceElement1Frame = By.Id("iframe_serviceelement1");
        readonly By cwDialog_ServiceElement1Frame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceelement1')]");
        readonly By relatedRecordIframe = By.Id("CWUrlPanel_IFrame");



        readonly By ServiceGLCodingsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By backButton = By.Id("BackButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By viewSelectorPicklist = By.Id("CWViewSelector");


        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        By resultsPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By resultsPageHeaderElementSortOrded(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");



        public ServiceGLCodingsPage WaitForServiceGLCodingsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ServiceElement1Frame);
            SwitchToIframe(iframe_ServiceElement1Frame);

            WaitForElement(cwDialog_ServiceElement1Frame);
            SwitchToIframe(cwDialog_ServiceElement1Frame);

            WaitForElement(relatedRecordIframe);
            SwitchToIframe(relatedRecordIframe);



            return this;
        }


        public ServiceGLCodingsPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ServiceGLCodingsPage TapSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }



        public ServiceGLCodingsPage SelectServiceGLCodingRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceGLCodingsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServiceGLCodingsPage ClickNewRecordButton()
        {            
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);

            Click(newRecordButton);

            return this;
        }

        public ServiceGLCodingsPage SelectView(string TextToSelect)
        {
            WaitForElementToBeClickable(viewSelectorPicklist);
            MoveToElementInPage(viewSelectorPicklist);
            SelectPicklistElementByText(viewSelectorPicklist, TextToSelect);

            return this;
        }


        public ServiceGLCodingsPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);

            Click(saveAndCloseButton);

            return this;
        }


        public ServiceGLCodingsPage ClickBackButton()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ServiceElement1Frame);
            SwitchToIframe(iframe_ServiceElement1Frame);

            WaitForElement(cwDialog_ServiceElement1Frame);
            SwitchToIframe(cwDialog_ServiceElement1Frame);

            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceGLCodingsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));

            Click(RecordIdentifier(RecordID));

            return this;
        }

        public ServiceGLCodingsPage ValidateServiceGLCodingRecordIsNotPresent(string RecordID)
        {
            bool ActualVisible = GetElementVisibility(RecordIdentifier(RecordID));
            Assert.IsFalse(ActualVisible);

            return this;
        }

        public ServiceGLCodingsPage ValidateServiceGLCodingRecordIsPresent(string RecordID)
        {
            bool ActualVisible = GetElementVisibility(RecordIdentifier(RecordID));
            Assert.IsTrue(ActualVisible);

            return this;
        }

        public ServiceGLCodingsPage ValidateSelectedViewSelectorPicklistText(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelectorPicklist);
            MoveToElementInPage(viewSelectorPicklist);
            ValidatePicklistSelectedText(viewSelectorPicklist, ExpectedText);

            return this;
        }

        public ServiceGLCodingsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(resultsPageHeaderElement(CellPosition));
            WaitForElementVisible(resultsPageHeaderElement(CellPosition));
            ValidateElementText(resultsPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }
        public ServiceGLCodingsPage ValidateHeaderCellSortOrdedAscending(int CellPosition)
        {
            WaitForElement(resultsPageHeaderElementSortOrded(CellPosition));
            ScrollToElement(resultsPageHeaderElementSortOrded(CellPosition));
            WaitForElementVisible(resultsPageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(resultsPageHeaderElementSortOrded(CellPosition), "class", "sortasc");

            return this;
        }

    }
}
