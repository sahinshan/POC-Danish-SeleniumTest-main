using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUsersPage : CommonMethods
    {
        public SystemUsersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By newRecord_Button = By.Id("TI_NewRecordButton");

        readonly By FirstName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/*/*");
        readonly By LastName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/*/*");
        readonly By Name_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/*/*");
        readonly By BusinessUnit_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/*/*");
        readonly By UserName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/*/*");
        readonly By WorkEmail_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/*/*");



        readonly By viewSelector = By.Id("CWViewSelector");

        #region Advanced Search Area

        readonly By DoNotUseViewFilter_CheckBox = By.Id("CWIgnoreViewFilter");

        readonly By Name_Field = By.Id("CWField_systemuser_fullname");
        readonly By UserName_Field = By.Id("CWField_systemuser_username");
        readonly By EmploymentStatus_LookupButton = By.Id("CWLookupBtn_systemuser_employmentstatusid");
        readonly By DefaultTeam_LookupButton = By.Id("CWLookupBtn_systemuser_defaultteamid");
        readonly By Search_Button = By.Id("CWSearchButton");
        readonly By ClearFilters_Button = By.Id("CWClearFiltersButton");

        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int cellId) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + cellId + "]");
        By recordFirstName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3]");
        By recordLastName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[4]");
        By recordName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[5]");
        By recordBusinessUnit_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6]");
        By recordUserName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[7]");
        By recordWorkEmail_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[8]");


        public SystemUsersPage WaitForSystemUsersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            this.WaitForElementVisible(pageHeader);
            this.ValidateElementText(pageHeader, "System Users");

            WaitForElementVisible(viewSelector);

            return this;
        }

        public SystemUsersPage WaitForResultsGridToLoad()
        {
            this.WaitForElement(FirstName_FieldHeader);
            this.WaitForElement(LastName_FieldHeader);
            this.WaitForElement(Name_FieldHeader);
            this.WaitForElement(BusinessUnit_FieldHeader);
            this.WaitForElement(UserName_FieldHeader);
            this.WaitForElement(WorkEmail_FieldHeader);

            return this;
        }

        public SystemUsersPage ValidateFirstNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordFirstName_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateLastNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordLastName_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordName_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateBusinessUnitCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordBusinessUnit_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateUserNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordUserName_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateWorkEmailCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordWorkEmail_cell(RecordID), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 30);

            return this;
        }

        public SystemUsersPage ValidateRecordNotPresent(Guid RecordID)
        {
            return ValidateRecordNotPresent(RecordID.ToString());
        }

        public SystemUsersPage ValidateRecordCellText(string RecordID, int CellId, string ExpectedText)
        {
            WaitForElement(recordCell(RecordID, CellId), 7);
            ScrollToElement(recordCell(RecordID, CellId));
            ValidateElementText(recordCell(RecordID, CellId), ExpectedText);

            return this;
        }

        public SystemUsersPage ValidateRecordCellText(Guid RecordID, int CellId, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellId, ExpectedText);
        }

        public SystemUsersPage ClickNewRecordButton()
        {

            WaitForElementToBeClickable(newRecord_Button);
            ScrollToElement(newRecord_Button);
            Click(newRecord_Button);

            return this;
        }

        public SystemUsersPage OpenRecord(string RecordID)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(recordRow(RecordID));
            ScrollToElement(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public SystemUsersPage OpenRecord(Guid RecordID)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(recordRow(RecordID.ToString()));
            ScrollToElement(recordRow(RecordID.ToString()));
            this.Click(recordRow(RecordID.ToString()));

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public SystemUsersPage SelectSystemView(string TextToSelect)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(viewSelector);

            SelectPicklistElementByText(viewSelector, TextToSelect);

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public SystemUsersPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }

        public SystemUsersPage ValidateRecordIsPresent(Guid RecordID)
        {
            return ValidateRecordIsPresent(RecordID.ToString());
        }

        #region Advanced Search

        public SystemUsersPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public SystemUsersPage InsertUserName(string TextToInsert)
        {
            WaitForElementToBeClickable(UserName_Field);
            SendKeys(UserName_Field, TextToInsert);

            return this;
        }

        public SystemUsersPage ClickEmploymentStatusLookupButton()
        {
            WaitForElementToBeClickable(EmploymentStatus_LookupButton);
            Click(EmploymentStatus_LookupButton);

            return this;
        }

        public SystemUsersPage ClickDefaultTeamLookupButton()
        {
            WaitForElementToBeClickable(DefaultTeam_LookupButton);
            Click(DefaultTeam_LookupButton);

            return this;
        }

        public SystemUsersPage ClickSearchButton()
        {
            WaitForElementToBeClickable(Search_Button);
            Click(Search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public SystemUsersPage ClickClearFiltersButton()
        {
            WaitForElementToBeClickable(ClearFilters_Button);
            Click(ClearFilters_Button);

            return this;
        }

        public SystemUsersPage ClickDoNotUseViewFilterCheckbox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilter_CheckBox);
            string elementChecked = GetElementByAttributeValue(DoNotUseViewFilter_CheckBox, "checked");
            if (elementChecked != "true")
                Click(DoNotUseViewFilter_CheckBox);

            return this;
        }

        public SystemUsersPage UncheckDoNotUseViewFilterCheckbox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilter_CheckBox);
            string elementChecked = GetElementByAttributeValue(DoNotUseViewFilter_CheckBox, "checked");
            if (elementChecked == "true")
                Click(DoNotUseViewFilter_CheckBox);

            return this;
        }


        #endregion

    }
}
