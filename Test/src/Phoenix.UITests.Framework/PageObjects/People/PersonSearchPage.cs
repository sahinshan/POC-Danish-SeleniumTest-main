
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonSearchPage : CommonMethods
    {
        public PersonSearchPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");


        readonly By BackButton = By.XPath("//*[@id='CWHeader']/descendant::button[@title='Back']");
        readonly By AddNewRecordButton = By.XPath("//*[@id='CWNewButton']");

        readonly By NotificationMessage = By.XPath("//*[@id='CWNotificationMessage_PersonSearch']");
        readonly By NotificationHideLink = By.XPath("//*[@id='CWNotificationHolder_PersonSearch']/a");

        readonly By FirstNameField = By.XPath("//*[@id='CWField_FirstName']");
        readonly By MiddleNameField = By.XPath("//*[@id='CWField_MiddleName']");
        readonly By LastNameField = By.XPath("//*[@id='CWField_LastName']");
        readonly By StatedGenderField = By.XPath("//*[@id='CWField_GenderId']");
        readonly By NHSNoField = By.XPath("//*[@id='CWField_NHSNumber']");
        readonly By DOBField = By.XPath("//*[@id='CWField_DateOfBirth']");

        readonly By UseDateOfBirthRangeCheckbox = By.XPath("//*[@id='CWField_UseBirthDateRange']");
        readonly By DateOfBirthFromField = By.XPath("//*[@id='CWField_DateOfBirthFrom']");
        readonly By DateOfBirthToField = By.XPath("//*[@id='CWField_DateOfBirthTo']");
        readonly By IdField = By.XPath("//*[@id='CWField_PersonNumber']");
        readonly By LegacyIdField = By.XPath("//*[@id='CWField_LegacyId']");
        readonly By UniquePupilNoField = By.XPath("//*[@id='CWField_UniquePupilNumber']");
        readonly By NationalInsuranceNumberField = By.XPath("//*[@id='CWField_NationalInsuranceNumber']");
        readonly By PropertyNameField = By.XPath("//*[@id='CWField_PropertyName']");
        readonly By PropertyNoField = By.XPath("//*[@id='CWField_AddressLine1']");
        readonly By StreetField = By.XPath("//*[@id='CWField_AddressLine2']");
        readonly By VillageDistrictField = By.XPath("//*[@id='CWField_AddressLine3']");
        readonly By TownCityField = By.XPath("//*[@id='CWField_AddressLine4']");
        readonly By CountyField = By.XPath("//*[@id='CWField_AddressLine5']");
        readonly By PostCodeField = By.XPath("//*[@id='CWField_Postcode']");
        readonly By UseSoundsLikeCheckbox = By.XPath("//*[@id='CWField_UseSoundsLike']");
        readonly By IncludeInactiveCheckbox = By.XPath("//*[@id='CWField_ShowHideInactivePersons']");


        readonly By SearchButton = By.XPath("//*[@id='CWSearchButton']");
        readonly By PDSSearchButton = By.XPath("//*[@id='CWPDSSearchButton']");
        readonly By ClearFiltersButton = By.XPath("//*[@id='CWClearFiltersButton']");

        readonly By GridHeaderRow = By.XPath("//tr[@id='CWGridHeaderRow']/th");

        #region Results Area

        By tableHeaderCellLink(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]//a");

        readonly By selectAllReacordsCheckbox = By.XPath("//*[@id='cwgridheaderselector']");
        By recordCheckbox(string RecordID) => By.XPath("//*[@id='CHK_" + RecordID.ToString() + "']");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//*[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By cellPosition (int CellPosition) => By.XPath("//tr[@id='CWGridHeaderRow']/th[" + CellPosition + "]");
        By cellIconPosition(string RecordID, int CellPosition) => By.XPath("//*[@id='" + RecordID + "']/td[" + CellPosition + "]//img");
        By cellPositionText(string RecordID, int CellPosition) => By.XPath("//*[@id='" + RecordID + "']/td[" + CellPosition + "]");

        readonly By columnsList = By.XPath("//table[@id = 'CWGridHeader']/thead/tr[@id = 'CWGridHeaderRow']/th/descendant::span[@class = 'th-text']");

        By colHeader(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th["+cellPosition+"]//a/span");        
        #endregion


        public PersonSearchPage WaitForPersonSearchPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(BackButton);
            WaitForElement(AddNewRecordButton);

            WaitForElement(FirstNameField);
            WaitForElement(LastNameField);

            WaitForElementVisible(SearchButton);            
            WaitForElementVisible(ClearFiltersButton);

            return this;
        }

        public PersonSearchPage InsertFirstName(string TextToInsert)
        {
            SendKeys(FirstNameField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertMiddleName(string TextToInsert)
        {
            SendKeys(MiddleNameField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertLastName(string TextToInsert)
        {
            SendKeys(LastNameField, TextToInsert);
            return this;
        }
        public PersonSearchPage SelectStatedGender(string TextToSelect)
        {
            SelectPicklistElementByText(StatedGenderField, TextToSelect);
            return this;
        }
        public PersonSearchPage InsertNHSNo(string TextToInsert)
        {
            SendKeys(NHSNoField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertDOB(string TextToInsert)
        {
            SendKeys(DOBField, TextToInsert);
            return this;
        }

        public PersonSearchPage ClickUseDateOfBirthRangeCheckbox()
        {
            Click(UseDateOfBirthRangeCheckbox);
            return this;
        }
        public PersonSearchPage InsertDateOfBirthFrom(string TextToInsert)
        {
            SendKeys(DateOfBirthFromField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertDateOfBirthTo(string TextToInsert)
        {
            SendKeys(DateOfBirthToField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertId(string TextToInsert)
        {
            SendKeys(IdField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertLegacyId(string TextToInsert)
        {
            SendKeys(LegacyIdField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertUniquePupilNo(string TextToInsert)
        {
            SendKeys(UniquePupilNoField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertNationalInsuranceNumber(string TextToInsert)
        {
            SendKeys(NationalInsuranceNumberField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertPropertyName(string TextToInsert)
        {
            SendKeys(PropertyNameField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertPropertyNo(string TextToInsert)
        {
            SendKeys(PropertyNoField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertStreet(string TextToInsert)
        {
            SendKeys(StreetField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertVillageDistrict(string TextToInsert)
        {
            SendKeys(VillageDistrictField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertTownCity(string TextToInsert)
        {
            SendKeys(TownCityField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertCounty(string TextToInsert)
        {
            SendKeys(CountyField, TextToInsert);
            return this;
        }
        public PersonSearchPage InsertPostCode(string TextToInsert)
        {
            SendKeys(PostCodeField, TextToInsert);
            return this;
        }
        public PersonSearchPage ClickUseSoundsLikeCheckbox()
        {
            Click(UseSoundsLikeCheckbox);
            return this;
        }
        public PersonSearchPage ClickIncludeInactiveCheckbox()
        {
            WaitForElementToBeClickable(IncludeInactiveCheckbox);
            MoveToElementInPage(IncludeInactiveCheckbox);
            Click(IncludeInactiveCheckbox);
            return this;
        }


        public PersonSearchPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(AddNewRecordButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public PersonSearchPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public PersonSearchPage ClickSearchButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementToBeClickable(SearchButton);
            Click(SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 15);
            return this;
        }
        public PersonSearchPage ClickPDSSearchButton()
        {
            Click(PDSSearchButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public PersonSearchPage ClickClearFiltersButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(ClearFiltersButton);
            ScrollToElement(ClearFiltersButton);
            Click(ClearFiltersButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public PersonSearchPage ClickOnRecord(string RecordID)
        {
            this.Click(cellPositionText(RecordID, 2));

            return this;
        }


        public PersonSearchPage WaitForResultsAreaToLoad()
        {
            WaitForElement(selectAllReacordsCheckbox);

            return this;
        }

        public PersonSearchPage WaitForRecordVisible(string RecordID)
        {
            WaitForElement(recordCheckbox(RecordID));

            return this;
        }

        public PersonSearchPage VerifyColumnCellText(string RecordID, int CellPosition, string ExpeectedText)
        {
            ScrollToElement(cellPosition(CellPosition));
            ValidateElementText(cellPosition(CellPosition), ExpeectedText);

            return this;
        }

        public PersonSearchPage VerifyCellText(string RecordID, int CellPosition, string ExpeectedText)
        {
            ScrollToElement(cellPositionText(RecordID, CellPosition));
            ValidateElementAttribute(cellPositionText(RecordID, CellPosition), "title", ExpeectedText);

            return this;
        }

        public PersonSearchPage VerifyColumnIcon(string RecordID, int CellPosition, string IconPath)
        {
            ScrollToElement(cellPositionText(RecordID, CellPosition));
            ValidateElementAttribute(cellIconPosition(RecordID, CellPosition), "src", IconPath);

            return this;
        }

        public PersonSearchPage ValidateShouldNotDisplayCellImage(string RecordID, int CellPosition)
        {
            ValidateElementDoNotExist(cellIconPosition(RecordID, CellPosition));

            return this;
        }

        public PersonSearchPage VerifyColumnCellText_Icon_Tooltip(string RecordID,string ColumnName, string Icon, string Tooltip)
        {
            int CellPosition = GetColumnIndex(ColumnName);
            ValidateElementText(cellPosition(CellPosition), ColumnName);

            if (Icon == "")
                ValidateElementDoNotExist(cellIconPosition(RecordID, CellPosition));
            else
                ValidateElementAttribute(cellIconPosition(RecordID, CellPosition), "src", Icon);

            ValidateElementAttribute(cellPositionText(RecordID, CellPosition), "title", Tooltip);

            return this;
        }

        public PersonSearchPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordCheckbox(RecordID), 3);

            return this;
        }

        public PersonSearchPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }

        public PersonSearchPage ClickTableHeaderCellLink(int CellPosition)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(tableHeaderCellLink(CellPosition));

            Click(tableHeaderCellLink(CellPosition));

            return this;
        }

        /// <summary>
        /// Validate that a record is only displayed once in the results page
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        public PersonSearchPage ValidateRecordIsUnique(string RecordID)
        {
            int totalRecords = CountElements(recordCheckbox(RecordID));
            if (totalRecords != 1)
                throw new Exception(string.Format("Record {0} should be displayed once but is actually displayed {1}", RecordID, totalRecords));

            return this;
        }

        public PersonSearchPage ValidateRecordCellText(string RecordID, string columnName, string ExpectedText)
        {
            int columnIndex = GetColumnIndex(columnName);
            
            WaitForElementVisible(recordCell(RecordID, columnIndex));            
            ValidateElementText(recordCell(RecordID, columnIndex), ExpectedText);

            return this;
        }

        public PersonSearchPage ValidateNotificationHideLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationHideLink);
            else
                WaitForElementNotVisible(NotificationHideLink, 3);

            return this;
        }

        public PersonSearchPage ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public PersonSearchPage ClickNotificationHideLink()
        {
            Click(NotificationHideLink);

            return this;
        }

        public PersonSearchPage ValidateIncludeInactiveFilterVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(IncludeInactiveCheckbox);
            else
                WaitForElementNotVisible(IncludeInactiveCheckbox, 5);

            return this;
        }

        public int GetColumnIndex(string columnName)
        {
            int columnIndex = 0;
            var columnCollections = GetWebElements(GridHeaderRow);

            foreach (var columnNames in columnCollections)
            {
                columnIndex++;
                ScrollToElement(cellPosition(columnIndex));
                if (columnNames.Text.Contains(columnName))
                    break;
            }
            return columnIndex;
        }
    }
}
