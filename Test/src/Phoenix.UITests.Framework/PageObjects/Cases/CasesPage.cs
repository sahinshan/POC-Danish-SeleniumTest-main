using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CasesPage : CommonMethods
    {
        public CasesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By relatedRecordIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Cases']");


        #region Advanced Search Filter Panel

        readonly By DoNotUseViewFilter_CheckBox = By.Id("CWIgnoreViewFilter");

        readonly By CaseNo_Field = By.Id("CWField_case_casenumber");
        readonly By NHSNo_Field = By.Id("CWField_person_nhsnumber_personid");
        readonly By FirstName_Field = By.Id("CWField_person_firstname_personid");
        readonly By LastName_Field = By.Id("CWField_person_lastname_personid");
        readonly By PersonID_Field = By.Id("CWField_person_personnumber_personid");
        readonly By DOB_Field = By.Id("CWField_person_dateofbirth_personid");
        readonly By StatedGender_picklist = By.Id("CWField_person_genderid_personid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_case_ownerid");
        readonly By CaseStatus_LookupButton = By.Id("CWLookupBtn_case_casestatusid");

        readonly By ClearFilters_Button = By.Id("CWClearFiltersButton");
        readonly By Search_Button = By.Id("CWSearchButton");

        #endregion


        By CaseRow(string CaseID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + CaseID + "']/td[2]");

        By CaseRowCheckBox(string CaseID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + CaseID + "']/td[1]/input");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]/*/*");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By MailMergeButton = By.Id("TI_MailMergeButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By topBannerMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By PinToMeButton = By.Id("TI_PinToMeButton");
        readonly By UnpinFromMeButton = By.Id("TI_UnpinFromMeButton");
        readonly By PinToAnotherButton = By.Id("TI_PinToAnotherButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");

        readonly By searchResult_DropDown = By.XPath("//select[@id='CWViewSelector']");

        readonly By myPinnedPeople_Option = By.XPath("//select[@id='CWViewSelector']/optgroup/option[text()='My Pinned Cases']");


        public CasesPage WaitForCasesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Cases")
                throw new Exception("Page title do not equals: \"Cases\" ");

            return this;
        }

        public CasesPage SearchByCaseNumber(string SearchQuery, string CaseID)
        {
            InsertCaseNo(SearchQuery);

            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CaseRow(CaseID));

            return this;
        }

        
        public CasesPage SearchByCaseNumber(string SearchQuery)
        {            
            InsertCaseNo(SearchQuery);

            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public CasesPage SelectCaseRecord(string CaseId)
        {
            WaitForElement(CaseRowCheckBox(CaseId));
            Click(CaseRowCheckBox(CaseId));

            return this;
        }

        public CasesPage TapTopBannerMenuButton()
        {
            Click(topBannerMenuButton);

            return this;
        }

        public CasesPage ValidateBulkEditButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BulkEditButton);
            else
                WaitForElementNotVisible(BulkEditButton, 5);


            return this;
        }

        public CaseRecordPage OpenCaseRecord(string CaseId, string CaseName)
        {
            this.WaitForElementToBeClickable(CaseRow(CaseId));
            this.Click(CaseRow(CaseId));

            return new CaseRecordPage(this.driver, this.Wait, this.appURL);
        }

        public CasesPage WaitForPersonCasesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(relatedRecordIFrame);
            SwitchToIframe(relatedRecordIFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Cases");

            return this;
        }

        public CasesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CasesPage ClickAssignRecordButton()
        {
            Click(AssignRecordButton);

            return this;
        }

        public CasesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public CasesPage ClickExportToExcel()
        {
            Click(ExportToExcelButton);

            return this;
        }

        public CasesPage SelectPersonCaseRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CasesPage ClickAdditionalItemsMenuButton()
        {
            this.Click(additionalItemsMenuButton);

            return this;
        }

        public CasesPage ClickPinToMe()
        {
            this.Click(PinToMeButton);

            return this;
        }

        public CasesPage SelectSearchResultsDropDown(string ElementTextToBeSelect)
        {
            WaitForElement(searchResult_DropDown);
            SelectPicklistElementByText(searchResult_DropDown, ElementTextToBeSelect);
            return this;
        }

        public CasesPage ValidateMyPinnedcasesOption(string ExpectedText)
        {
            WaitForElement(myPinnedPeople_Option);
            ValidateElementText(myPinnedPeople_Option, ExpectedText);
            return this;
        }

        public CasesPage ValidateCaesRecord(string RecordId,string ExpectedValue)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            ValidateElementValue(recordRowCheckBox(RecordId), ExpectedValue);

            return this;
        }

        public CasesPage ValidateHeaderRecordCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(headerCell(CellPosition));
            ValidateElementText(headerCell(CellPosition), ExpectedText);

            return this;
        }


        #region Advanced Search Filter Panel

        public CasesPage ClickDoNotUseViewFilterCheckBox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilter_CheckBox);
            Click(DoNotUseViewFilter_CheckBox);

            return this;
        }

        public CasesPage InsertCaseNo(string TextToInsert)
        {
            WaitForElementToBeClickable(CaseNo_Field);
            SendKeys(CaseNo_Field, TextToInsert);

            return this;
        }

        public CasesPage InsertNHSNo(string TextToInsert)
        {
            WaitForElementToBeClickable(NHSNo_Field);
            SendKeys(NHSNo_Field, TextToInsert);

            return this;
        }

        public CasesPage InsertFirstName(string TextToInsert)
        {
            WaitForElementToBeClickable(FirstName_Field);
            SendKeys(FirstName_Field, TextToInsert);

            return this;
        }

        public CasesPage InsertLastName(string TextToInsert)
        {
            WaitForElementToBeClickable(LastName_Field);
            SendKeys(LastName_Field, TextToInsert);

            return this;
        }

        public CasesPage InsertPersonID(string TextToInsert)
        {
            WaitForElementToBeClickable(PersonID_Field);
            SendKeys(PersonID_Field, TextToInsert);

            return this;
        }

        public CasesPage InsertDOB(string TextToInsert)
        {
            WaitForElementToBeClickable(DOB_Field);
            SendKeys(DOB_Field, TextToInsert);

            return this;
        }

        public CasesPage SelectStatedGender(string TextToSelect)
        {
            WaitForElementToBeClickable(StatedGender_picklist);
            SelectPicklistElementByText(StatedGender_picklist, TextToSelect);

            return this;
        }

        public CasesPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public CasesPage ClickCaseStatusLookupButton()
        {
            WaitForElementToBeClickable(CaseStatus_LookupButton);
            Click(CaseStatus_LookupButton);

            return this;
        }

        public CasesPage ClickClearFiltersButton()
        {
            WaitForElementToBeClickable(ClearFilters_Button);
            Click(ClearFilters_Button);

            return this;
        }

        public CasesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(Search_Button);
            Click(Search_Button);

            return this;
        }

        #endregion

    }
}
