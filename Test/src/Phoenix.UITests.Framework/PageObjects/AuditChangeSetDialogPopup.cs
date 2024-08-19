using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AuditChangeSetDialogPopup : CommonMethods
    {
        public AuditChangeSetDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By iframe_CWAuditChangeSetDialog = By.Id("iframe_CWAuditChangeSetDialog");



        #region Header

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1");

        readonly By recordLink = By.XPath("//*[@id='linkDiv']/a");


        readonly By operationLabel = By.XPath("//*[@id='CWHeader']/div[1]/strong[contains(text(),'Operation:')]");
        readonly By changedByLabel = By.XPath("//*[@id='CWHeader']/div[2]/strong[contains(text(),'User:')]");
        readonly By changedOnLabel = By.XPath("//*[@id='CWHeader']/div[3]/strong[contains(text(),'Date:')]");


        readonly By operationField = By.XPath("//*[@id='CWHeader']/div[1]");
        readonly By changedByField = By.XPath("//*[@id='CWHeader']/div[2]");
        readonly By changedOnField = By.XPath("//*[@id='CWHeader']/div[3]");



        readonly By adminAuditDetails_operationLabel = By.XPath("//*[@id='CWHeader']/div/strong[contains(text(),'Operation:')]");
        readonly By adminAuditDetails_userLabel = By.XPath("//*[@id='CWHeader']/div/strong[contains(text(),'User:')]");
        readonly By adminAuditDetails_dateLabel = By.XPath("//*[@id='CWHeader']/div/strong[contains(text(),'Date:')]");

        readonly By adminAuditDetails_operationField = By.XPath("//*[@id='CWHeader']/div[2]");
        readonly By adminAuditDetails_userField = By.XPath("//*[@id='CWHeader']/div[3]");
        readonly By adminAuditDetails_changedOnField = By.XPath("//*[@id='CWHeader']/div[4]");
        readonly By adminAuditDetails_CommentsField = By.XPath("//*[@id='CWHeader']/div[5]");


        #endregion

        #region Results Area

        readonly By fieldNameHeader = By.XPath("//*[@id='CWChangesTable']//*[contains(text(),'Field Name')]");
        readonly By newValueHeader = By.XPath("//*[@id='CWChangesTable']//*[contains(text(),'New Value')]");
        readonly By oldValueHeader = By.XPath("//*[@id='CWChangesTable']//*[contains(text(),'Old Value')]");



        By fieldNameCell(string AuditChangesetID) => By.XPath("//*[@id='" + AuditChangesetID + "']/td[1]");
        By newValueCell(string AuditChangesetID) => By.XPath("//*[@id='" + AuditChangesetID + "']/td[2]");
        By oldvalueCell(string AuditChangesetID) => By.XPath("//*[@id='" + AuditChangesetID + "']/td[3]");

        By fieldNameCell(int RowPosition) => By.XPath("//tr[" + RowPosition + "]/td[1]/p");
        By newValueCell(int RowPosition) => By.XPath("//tr[" + RowPosition + "]/td[2]/p");
        By oldvalueCell(int RowPosition) => By.XPath("//tr[" + RowPosition + "]/td[3]/p");



        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        #endregion


        readonly By closeBUtton = By.Id("CloseButton");



        public AuditChangeSetDialogPopup WaitForAuditChangeSetDialogPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(iframe_CWAuditChangeSetDialog);
            SwitchToIframe(iframe_CWAuditChangeSetDialog);

            WaitForElement(popupHeader);

            ValidateElementText(popupHeader, "Audit Detail");

            WaitForElement(operationLabel);
            WaitForElement(changedByLabel);
            WaitForElement(changedOnLabel);

            //WaitForElement(fieldNameHeader);
            //WaitForElement(newValueHeader);
            //WaitForElement(oldValueHeader);

            WaitForElement(closeBUtton);

            return this;
        }

        public AuditChangeSetDialogPopup WaitForAdminAuditChangeSetDialogPopupToLoad()
        {

            WaitForElement(iframe_CWAuditChangeSetDialog);
            SwitchToIframe(iframe_CWAuditChangeSetDialog);

            WaitForElement(popupHeader);

            ValidateElementText(popupHeader, "Audit Detail");

            WaitForElement(adminAuditDetails_operationLabel);
            WaitForElement(adminAuditDetails_userLabel);
            WaitForElement(adminAuditDetails_dateLabel);


            WaitForElement(closeBUtton);

            return this;
        }

        public AuditChangeSetDialogPopup TapCloseButton()
        {
            Click(closeBUtton);

            return this;
        }



        public AuditChangeSetDialogPopup ValidateOperation(string ExpectedText)
        {
            ValidateElementText(operationField, "Operation: " + ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateChangedBy(string ExpectedText)
        {
            ValidateElementText(changedByField, "User: " + ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateChangedOn(string ExpectedText)
        {
            ValidateElementText(changedOnField, "Date: " + ExpectedText);

            return this;
        }

        public AuditChangeSetDialogPopup ValidateAdminAuditDetailOperation(string ExpectedText)
        {
            ValidateElementText(adminAuditDetails_operationField, "Operation: " + ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateAdminAuditDetailUser(string ExpectedText)
        {
            ValidateElementText(adminAuditDetails_userField, "User: " + ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateAdminAuditDetailDate(string ExpectedText)
        {
            ValidateElementText(adminAuditDetails_changedOnField, "Date: " + ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateAdminAuditDetailComments(string ExpectedText)
        {
            ValidateElementText(adminAuditDetails_CommentsField, "Comments: " + ExpectedText);

            return this;
        }


        public AuditChangeSetDialogPopup ValidateFieldNameCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(fieldNameCell(RecordID), ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateNewValueCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(newValueCell(RecordID), ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateOldValueCellText(string RecordID, string ExpectedText)
        {
            ValidateElementText(oldvalueCell(RecordID), ExpectedText);

            return this;
        }


        public AuditChangeSetDialogPopup ValidateFieldNameCellText(int RowId, string ExpectedText)
        {
            ValidateElementText(fieldNameCell(RowId), ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateNewValueCellText(int RowId, string ExpectedText)
        {
            ValidateElementText(newValueCell(RowId), ExpectedText);

            return this;
        }
        public AuditChangeSetDialogPopup ValidateOldValueCellText(int RowId, string ExpectedText)
        {
            ValidateElementText(oldvalueCell(RowId), ExpectedText);

            return this;
        }



        public AuditChangeSetDialogPopup ValidateNoRecordsMessageVisible()
        {
            WaitForElement(noRecordsMessage);
            WaitForElement(noResultsMessage);

            return this;
        }

        public AuditChangeSetDialogPopup ValidateRecordLinkText(string ExpectedText)
        {
            ValidateElementText(recordLink, ExpectedText);

            return this;
        }

        public AuditChangeSetDialogPopup ClickRecordLink()
        {
            Click(recordLink);

            return this;
        }


    }
}
