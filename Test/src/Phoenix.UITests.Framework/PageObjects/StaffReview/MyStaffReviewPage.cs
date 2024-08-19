using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects.StaffReview
{
    public class MyStaffReviewPage : CommonMethods
    {
        public MyStaffReviewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By cwContent_IFrame = By.Id("CWContentIFrame");

        readonly By systemViewsOption = By.XPath("//select[@id='CWViewSelector']");

        #region Advanced Search Area

        private static string SearchAllRecords_CheckboxId = "CWIgnoreViewFilter";
        readonly By SearchAllRecords_Checkbox = By.Id(SearchAllRecords_CheckboxId);
        readonly By ReviewType_LookupButton = By.Id("CWLookupBtn_staffreview_reviewtypeid");
        readonly By Role_LookupButton = By.Id("CWLookupBtn_staffreview_roleid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_staffreview_ownerid");
        readonly By useRange_Checkbox = By.Id("CWField_staffreview_duedate_range");
        readonly By DueDateFrom_Field = By.Id("CWField_staffreview_duedate_from");
        readonly By DueDateTo_Field = By.Id("CWField_staffreview_duedate_to");
        readonly By Search_Button = By.Id("CWSearchButton");
        readonly By ClearFilters_Button = By.Id("CWClearFiltersButton");

        #endregion

        readonly By staffReviewText = By.XPath("//h1[text()='Staff Reviews']");

        By recordRowAndCell(int rowNumber, int cellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + rowNumber + "]/td[" + cellPosition + "]");

        readonly By reviewType_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[2]");
        readonly By regardingUserId_Status = By.Id("//table[@id='CWGrid']/tbody/tr[1]/td[3]");
        readonly By responsibleTeam_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[5]");
        readonly By businesUnit_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[6]");
        readonly By reviewedBy_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[7]");
        readonly By nextReviewDate_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[8]");
        readonly By outCome_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[9]");
        readonly By createdDate_Status = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[13]");

        By ReviewType_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordId + "']/td[2]");
        By RegardingUserId_Status(string RecordId) => By.Id("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[3]");
        By ResponsibleTeam_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[5]");
        By BusinesUnit_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[6]");
        By ReviewedBy_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[7]");
        By NextReviewDate_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[8]");
        By OutCome_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[9]");
        By CreatedDate_Status(string RecordId) => By.XPath("//table[@id='CWGrid']/tbody/tr['" + RecordId + "']/td[13]");


        readonly By sortByRagarding = By.XPath("//a/span[text()='Regarding']");

        readonly By sortByCreatedOn = By.XPath("//a/span[text()='Created On']");
        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");

        public MyStaffReviewPage WaitForStaffReviewPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(cwContent_IFrame);
            SwitchToIframe(cwContent_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(systemViewsOption);
            WaitForElement(ClearFilters_Button);
            WaitForElement(Search_Button);

            return this;
        }

        public MyStaffReviewPage ClickSearchAllRecordsCheckbox()
        {
            WaitForElementToBeClickable(SearchAllRecords_Checkbox);
            Click(SearchAllRecords_Checkbox);

            return this;
        }

        public MyStaffReviewPage ClickUseRangeCheckbox()
        {
            WaitForElementToBeClickable(useRange_Checkbox);
            Click(useRange_Checkbox);

            return this;
        }

        public MyStaffReviewPage InsertDueDateFrom(string TextToInsert)
        {
            WaitForElementToBeClickable(DueDateFrom_Field);
            SendKeys(DueDateFrom_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public MyStaffReviewPage InsertDueDateTo(string TextToInsert)
        {
            WaitForElementToBeClickable(DueDateTo_Field);
            SendKeys(DueDateTo_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public MyStaffReviewPage ClickCreateRecordButton()
        {
            WaitForElementToBeClickable(createNewRecord_Button);
            Click(createNewRecord_Button);

            return this;
        }

        public MyStaffReviewPage SelectSystemViewsOption(string selectedText)
        {
            System.Threading.Thread.Sleep(3000);
            WaitForElement(systemViewsOption);
            SelectPicklistElementByText(systemViewsOption, selectedText);
            System.Threading.Thread.Sleep(3000);
            return this;
        }

        public MyStaffReviewPage ClickSortByRagarding()
        {
            WaitForElement(sortByRagarding);
            Click(sortByRagarding);
            System.Threading.Thread.Sleep(2000);
            return this;
        }

        public MyStaffReviewPage ClickSortByCreatedOn()
        {
            MoveToElementInPage(sortByCreatedOn);
            WaitForElementToBeClickable(sortByCreatedOn);
            Click(sortByCreatedOn);
            System.Threading.Thread.Sleep(2000);

            return this;
        }

        public MyStaffReviewPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public MyStaffReviewPage ValidateRegardindUserIdField(string verifytext)
        {
            WaitForElement(regardingUserId_Status);
            ValidateElementText(regardingUserId_Status, verifytext);
            System.Threading.Thread.Sleep(2000);

            return this;
        }

        public MyStaffReviewPage ValidateOutComeStatus(string verifytext)
        {
            WaitForElement(outCome_Status);
            ValidateElementText(outCome_Status, verifytext);
            System.Threading.Thread.Sleep(2000);

            return this;
        }

        public MyStaffReviewPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public MyStaffReviewPage ValidateCreatedDateStatus(string ExpectedValue)
        {
            WaitForElement(createdDate_Status);
            ValidateElementValue(createdDate_Status, ExpectedValue);
            
            return this;
        }

        public MyStaffReviewPage ValidateStaffReviewDisplayed(string ExpectedText)
        {
            WaitForElementVisible(staffReviewText);
            ValidateElementValueNotEqual(staffReviewText, ExpectedText);
            return this;
        }

        public MyStaffReviewPage ValidateFilterByUsingTeam(string ExpectedText)
        {
            WaitForElementVisible(responsibleTeam_Status);
            ValidateElementValueNotEqual(responsibleTeam_Status, ExpectedText);
            return this;
        }

        public MyStaffReviewPage ValidateFilterByOutCome(string ExpectedText)
        {
            WaitForElementVisible(outCome_Status);
            ValidateElementValueNotEqual(outCome_Status, ExpectedText);
            return this;
        }

        public MyStaffReviewPage validateRecordAllFieldStatus(string RecordID, string reviewTypeText, string TeamExpectedText, string businesUnitText, string reviewedByText, string nextReviewDateText, string outComeText)
        {

            WaitForElementVisible(ReviewType_Status(RecordID));
            ValidateElementText(ReviewType_Status(RecordID), reviewTypeText);
            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(ResponsibleTeam_Status(RecordID));
            ValidateElementText(ResponsibleTeam_Status(RecordID), TeamExpectedText);
            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(BusinesUnit_Status(RecordID));
            ValidateElementText(BusinesUnit_Status(RecordID), businesUnitText);
            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(ReviewedBy_Status(RecordID));
            ValidateElementText(ReviewedBy_Status(RecordID), reviewedByText);
            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(NextReviewDate_Status(RecordID));
            ValidateElementText(NextReviewDate_Status(RecordID), nextReviewDateText);
            System.Threading.Thread.Sleep(2000);

            WaitForElementVisible(OutCome_Status(RecordID));
            ValidateElementText(OutCome_Status(RecordID), outComeText);
            
            return this;
        }
        
        public MyStaffReviewPage ValidateCreateRecordButtonNotDisplay()
        {
            ValidateElementDoNotExist(createNewRecord_Button);

            return this;
        }

        public MyStaffReviewPage ValidateCellText(int rowNumber, int cellPosition, string expectedText)
        {
            WaitForElement(recordRowAndCell(rowNumber, cellPosition));
            ValidateElementText(recordRowAndCell(rowNumber, cellPosition), expectedText);

            return this;
        }


        #region Advanced Search

        public MyStaffReviewPage ClickReviewTypeLookupButton()
        {
            WaitForElementToBeClickable(ReviewType_LookupButton);
            Click(ReviewType_LookupButton);

            return this;
        }

        public MyStaffReviewPage ClickRoleLookupButton()
        {
            WaitForElementToBeClickable(Role_LookupButton);
            Click(Role_LookupButton);

            return this;
        }

        public MyStaffReviewPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public MyStaffReviewPage ClickSearchButton()
        {
            WaitForElementToBeClickable(Search_Button);
            Click(Search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public MyStaffReviewPage ClickClearFiltersButton()
        {
            WaitForElementToBeClickable(ClearFilters_Button);
            Click(ClearFilters_Button);

            return this;
        }


        #endregion
    }
}
