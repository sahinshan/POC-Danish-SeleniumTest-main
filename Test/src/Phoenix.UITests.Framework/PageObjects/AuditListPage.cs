using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AuditListPage : CommonMethods
    {
        public AuditListPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        By businessRecordIFrame(string BusinessObjectName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + BusinessObjectName + "&')]");        
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        By referenceDataPageIFrame(string referenceDataPage) => By.Id(referenceDataPage);

        readonly By pageHeader = By.XPath("//*[@id='CWHeader']/div/h1[text()='Audit List']");
        
        #region Search Area

        readonly By userLabel = By.Id("CWUserIdLabel");
        readonly By operationLabel = By.Id("CWField_OperationLabel");
        readonly By recordTypeLabel = By.Id("CWRecordTypeIdLabel");
        readonly By yearLabel = By.Id("CWField_YearLabel");
        readonly By dateFromLabel = By.Id("CWField_DateFromLabel");
        readonly By dateToLabel = By.Id("CWField_DateToLabel");

        readonly By userRemoveButton = By.Id("CWClearLookup_CWUserId");
        readonly By userLookupButton = By.Id("CWLookupBtn_CWUserId");
        readonly By operationPicklist = By.Id("CWField_Operation");
        readonly By recordTypeLookupButton = By.Id("CWLookupBtn_CWRecordTypeId");
        readonly By yearPicklist = By.Id("CWField_Year");
        readonly By dateFromField = By.Id("CWField_DateFrom");
        readonly By dateToField = By.Id("CWField_DateTo");

        readonly By searchButton = By.Id("CWSearchButton");
        readonly By clearFiltersButton = By.Id("CWClearFiltersButton");

        #endregion

        #region Results Area

        readonly By recordTypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Record Type']");
        readonly By titleHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Record Type']");
        readonly By operationHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Operation']");
        readonly By userHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='User']");
        readonly By dateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Date']");
        readonly By commentsHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Comments']");
        readonly By applicationHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Application']");
        readonly By reasonErrorManagemenHeader = By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text()='Reason']");

        By operationTitle(string titleName) => By.XPath("//*[@title='" + titleName + "']");

        By auditRecordCheckbox(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By operationCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[2]");
        By userCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[3]");
        By dateCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[4]");
        By commentsCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[5]");
        By applicationCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[6]");
        By reasonErrorManagemenCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[7]");


        By AdminAuditList_recordTypeCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[3]");
        By AdminAuditList_TitleCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[4]");
        By AdminAuditList_OperationCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[5]");
        By AdminAuditList_UserCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[6]");
        By AdminAuditList_DateCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[7]");
        By AdminAuditList_CommentsCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[8]");
        By AdminAuditList_ApplicationCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[9]");
        By AdminAuditList_ReasonCell(string AuditRecordID) => By.XPath("//*[@id='" + AuditRecordID + "']/td[10]");


        By AdminAuditList_ReasonCell(int RowNumber, int CellPosition) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + RowNumber + "]/td[" + CellPosition + "]");

        #endregion

        By FirstPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]/a");
        By PreviousPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]/a");
        By PageNumber_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        By NextPage_Button = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]/a");
       


        public AuditListPage WaitForAuditListPageToLoad(string BusinessObjectName = "person")
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(businessRecordIFrame(BusinessObjectName));
            SwitchToIframe(businessRecordIFrame(BusinessObjectName));

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(pageHeader);

            WaitForElement(userLabel);
            WaitForElement(operationLabel);
            WaitForElement(yearLabel);
            WaitForElement(dateFromLabel);
            WaitForElement(dateToLabel);

            WaitForElement(operationHeader);
            WaitForElement(userHeader);
            WaitForElement(dateHeader);
            WaitForElement(commentsHeader);
            WaitForElement(applicationHeader);
            WaitForElement(reasonErrorManagemenHeader);

            return this;
        }

        public AuditListPage WaitForAuditListPageToLoadFromReferenceDataPage(string BusinessObjectName = "person", string referenceDataPage = "")
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(businessRecordIFrame(BusinessObjectName));
            SwitchToIframe(businessRecordIFrame(BusinessObjectName));

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(pageHeader);

            WaitForElement(userLabel);
            WaitForElement(operationLabel);
            WaitForElement(yearLabel);
            WaitForElement(dateFromLabel);
            WaitForElement(dateToLabel);

            WaitForElement(operationHeader);
            WaitForElement(userHeader);
            WaitForElement(dateHeader);
            WaitForElement(commentsHeader);
            WaitForElement(applicationHeader);
            WaitForElement(reasonErrorManagemenHeader);

            return this;
        }

        public AuditListPage WaitForAdminAuditListPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(pageHeader);

            WaitForElement(userLabel);
            WaitForElement(operationLabel);
            WaitForElement(recordTypeLabel);
            WaitForElement(yearLabel);
            WaitForElement(dateFromLabel);
            WaitForElement(dateToLabel);

            WaitForElement(recordTypeHeader);
            WaitForElement(titleHeader);
            WaitForElement(operationHeader);
            WaitForElement(userHeader);
            WaitForElement(dateHeader);
            WaitForElement(commentsHeader);
            WaitForElement(applicationHeader);
            WaitForElement(reasonErrorManagemenHeader);

            return this;
        }
        
        public AuditListPage WaitForAlertAndHazardAuditListPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);


            return this;
        }

        public AuditListPage TapUserLookupButton()
        {
            Click(userLookupButton);

            return this;
        }

        public AuditListPage TapUserRemoveButton()
        {
            Click(userRemoveButton);

            return this;
        }

        public AuditListPage TapRecordTypeLookupButton()
        {
            Click(recordTypeLookupButton);

            return this;
        }

        public AuditListPage SelectOperation(string PicklistText)
        {
            SelectPicklistElementByText(operationPicklist, PicklistText);

            return this;
        }

        public AuditListPage SelectYear(string PicklistText)
        {
            SelectPicklistElementByText(yearPicklist, PicklistText);

            return this;
        }

        public AuditListPage InsertDateFrom(string TextToInsert)
        {
            SendKeys(dateFromField, TextToInsert);

            return this;
        }

        public AuditListPage InsertDateTo(string TextToInsert)
        {
            SendKeys(dateToField, TextToInsert);

            return this;
        }

        public AuditListPage ClickSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AuditListPage ClickClearFiltersButton()
        {
            Click(clearFiltersButton);

            return this;
        }

        public AuditListPage ValidateAuditRecordOperationCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(operationCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAuditRecordUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(userCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAuditRecordDateCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(dateCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAuditRecordCommentsCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(commentsCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAuditRecordApplicationCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(applicationCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAuditRecordReasonCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(reasonErrorManagemenCell(RecordID), ExpectedText);

            return this;
        }

        public AuditListPage ValidateAdminAuditListRecordTypeCellText(string RecordID, string ExpectedText)
        {
            WaitForElement(AdminAuditList_recordTypeCell(RecordID));
            ValidateElementText(AdminAuditList_recordTypeCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditListTitleCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_TitleCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditListOperationCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_OperationCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditListUserCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_UserCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditListDateCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_DateCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditCommentsCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_CommentsCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditApplicationCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_ApplicationCell(RecordID), ExpectedText);

            return this;
        }
        
        public AuditListPage ValidateAdminAuditReasonCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(AdminAuditList_ReasonCell(RecordID), ExpectedText);

            return this;
        }

        public AuditListPage ClickOnAuditRecord(string RecordID)
        {
            WaitForElementToBeClickable(operationCell(RecordID));

            Click(operationCell(RecordID));

            return this;
        }
        
        public AuditListPage ClickOnAdminAuditRecord(string RecordID)
        {
            WaitForElementToBeClickable(AdminAuditList_OperationCell(RecordID));

            Click(AdminAuditList_OperationCell(RecordID));

            return this;
        }

        public AuditListPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementToBeClickable(auditRecordCheckbox(RecordID));

            return this;
        }

        public AuditListPage ClickOnAuditRecordText(string Text)
        {
            WaitForElementToBeClickable(operationTitle(Text));
            ScrollToElement(operationTitle(Text));
            Click(operationTitle(Text));

            return this;
        }

        public AuditListPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(auditRecordCheckbox(RecordID), 3);

            return this;
        }

        public AuditListPage ClickFirstPageButton()
        {
            WaitForElementToBeClickable(FirstPage_Button);

            Click(FirstPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        
        public AuditListPage ClickPreviousPageButton()
        {
            WaitForElementToBeClickable(PreviousPage_Button);

            Click(PreviousPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        
        public AuditListPage ValidateCurrentPageInfo(string ExpectedText)
        {
            ValidateElementText(PageNumber_Button, ExpectedText);

            return this;
        }
        
        public AuditListPage ClickNextPageButton()
        {
            WaitForElementToBeClickable(NextPage_Button);

            Click(NextPage_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AuditListPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public AuditListPage ValidateRecordCellText(String operationalCell,String ExpectedText)
        {
           
            ValidateElementText(operationalCell, ExpectedText);

            return this;
        }

        private void ValidateElementText(string operationalCell, string expectedText)
        {
            throw new NotImplementedException();
        }

        public AuditListPage ValidateCellText(int RowNumber, int CellPosition, string expectedText)
        {
            ValidateElementText(AdminAuditList_ReasonCell(RowNumber, CellPosition), expectedText);

            return this;
        }

    }
}
