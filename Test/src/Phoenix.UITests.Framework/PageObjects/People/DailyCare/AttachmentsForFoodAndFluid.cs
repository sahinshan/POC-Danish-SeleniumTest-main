using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class AttachmentsForFoodAndFluid : CommonMethods
    {
        public AttachmentsForFoodAndFluid(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonfoodandfluid&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By PersonsVoiceSection = By.Id("CWSection_ResidentVoice");
        readonly By FoodandFluidAttachmentIframe = By.Id("CWIFrame_FoodandFluidAttachment");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Attachments (For Food and Fluid)']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");

        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']//a/span[text() = '" + ColumnName + "']");
        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");


        public AttachmentsForFoodAndFluid WaitForPageToLoad(bool isSubGrid = false)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(cwIFrame);
            SwitchToIframe(cwIFrame);
            
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(PersonsVoiceSection);

            if (isSubGrid)
            {             
                WaitForElement(FoodandFluidAttachmentIframe);
                SwitchToIframe(FoodandFluidAttachmentIframe);

                WaitForElementNotVisible("CWRefreshPanel", 7);
            }
            else
            {
                WaitForElement(CWUrlPanel_IFrame);
                SwitchToIframe(CWUrlPanel_IFrame);
                
                WaitForElementNotVisible("CWRefreshPanel", 7);

                WaitForElementVisible(viewSelector);
                WaitForElementVisible(searchField);
                WaitForElementVisible(searchButton);

            }

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);

            return this;
        }


        public AttachmentsForFoodAndFluid ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public AttachmentsForFoodAndFluid ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public AttachmentsForFoodAndFluid SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public AttachmentsForFoodAndFluid SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public AttachmentsForFoodAndFluid ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public AttachmentsForFoodAndFluid SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public AttachmentsForFoodAndFluid ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public AttachmentsForFoodAndFluid ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public AttachmentsForFoodAndFluid OpenRecord(string RecordID)
        {
            ScrollToElement(recordCell(RecordID, 2));
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public AttachmentsForFoodAndFluid OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public AttachmentsForFoodAndFluid ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(recordIdentifier(RecordID));
                ScrollToElement(recordIdentifier(RecordID));
            }
            else
                WaitForElementNotVisible(recordIdentifier(RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(recordIdentifier(RecordID)));

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public AttachmentsForFoodAndFluid ValidateRecordIsPresent(int RowPosition, string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(recordPosition(RowPosition, RecordID));
                ScrollToElement(recordPosition(RowPosition, RecordID));
            }
            else
                WaitForElementNotVisible(recordPosition(RowPosition, RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(recordPosition(RowPosition, RecordID)));

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateRecordIsPresent(int RowPosition, Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RowPosition, RecordID.ToString(), ExpectedVisible);

        }

        public AttachmentsForFoodAndFluid ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateHeaderCellText(string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(ExpectedText));
            ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

            return this;
        }

        public AttachmentsForFoodAndFluid ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public AttachmentsForFoodAndFluid ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public AttachmentsForFoodAndFluid ClickColumnHeader(int CellPosition, string HeaderCellText)
        {
            WaitForElementToBeClickable(GridHeaderCell(CellPosition, HeaderCellText));
            ScrollToElement(GridHeaderCell(CellPosition, HeaderCellText));
            Click(GridHeaderCell(CellPosition, HeaderCellText));

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateHeaderIsVisible(int CellPosition, string ExpectedText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(GridHeaderCell(CellPosition, ExpectedText));
                WaitForElementVisible(GridHeaderCell(CellPosition, ExpectedText));
            }
            else
            {
                WaitForElementNotVisible(GridHeaderCell(CellPosition, ExpectedText), 3);
            }

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateColumnIsSortedByDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateColumnsSortOrderDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));

            return this;
        }

        public AttachmentsForFoodAndFluid ValidateColumnSortOrderAscending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));

            return this;
        }

    }
}