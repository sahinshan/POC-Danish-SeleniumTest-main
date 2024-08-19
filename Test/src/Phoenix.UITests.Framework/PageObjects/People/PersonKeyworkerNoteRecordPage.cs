using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonKeyworkerNoteRecordPage : CommonMethods
    {
        public PersonKeyworkerNoteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonkeyworkernote&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Keyworker Note: ']");
        readonly By SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By SaveBtn = By.Id("TI_SaveButton");

        readonly By MenuButton = By.XPath("//*[@id ='CWNavGroup_Menu']/button");

        readonly By RelatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By RelatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By AttachmentsMenuItem = By.Id("CWNavItem_CPKeyworkerNotesAttachment");


        #region General
        readonly By occurred = By.XPath("//input[@id = 'CWField_ocurred']");
        readonly By occurredTime = By.Id("CWField_ocurred_Time");

        readonly By PersonLookupFieldLink = By.Id("CWField_personid_Link");

        readonly By TotalTimeSpentWithClientMinutesFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_totaltimespentwithclient']/label[text() = 'Total Time Spent With Person (Minutes)']");
        readonly By TotalTimeSpentWithClientMinutesField = By.Id("CWField_totaltimespentwithclient");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_totaltimespentwithclient'][@class = 'formerror']/span");

        readonly By DateAndTimeOccurredFieldLabel = By.Id("CWLabelHolder_ocurred");
        readonly By DateAndTimeOccurred_DateField = By.Id("CWField_ocurred");
        readonly By DateAndTimeOccurred_TimeField = By.Id("CWField_ocurred_Time");
        readonly By DateAndTimeOccurred_DatePicker = By.Id("CWField_ocurred_DatePicker");
        readonly By DateAndTimeOccurred_TimePicker = By.Id("CWField_ocurred_Time_TimePicker");

        #endregion

        #region Keyworker Notes

        readonly By KeyworkerNotesTextAreaField = By.Id("CWField_keyworkernote");

        #endregion

        #region Resident Voice

        readonly By ResidentVoiceSection = By.Id("CWSection_ResidentVoice");
        readonly By ResidentVoiceIframe = By.Id("CWIFrame_ResidentVoice");
        readonly By SectionHeader = By.XPath("//*[@id = 'CWToolbar']//h1[text() = 'Attachments (For Keyworker Notes)']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordIdentifier(int RowPosition, string RecordID) => By.XPath("//tr[" + RowPosition + "][@id='" + RecordID + "']");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']//a/span[text() = '" + ColumnName + "']");

        By gridPageHeaderElement(int HeaderCellPosition, string HeaderCellText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text() = '" + HeaderCellText + "']");
        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");


        #endregion


        public PersonKeyworkerNoteRecordPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            return this;
        }

        #region general

        public PersonKeyworkerNoteRecordPage InsertTotalTimeSpentWithClientMinutes(String TextToInsert)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            SendKeys(TotalTimeSpentWithClientMinutesField, TextToInsert + Keys.Tab);


            return this;
        }


        public PersonKeyworkerNoteRecordPage SetDateOccurred(String dateoccured)
        {
            ScrollToElement(occurred);
            WaitForElementVisible(occurred);
            SendKeys(occurred, dateoccured);


            return this;
        }

        //click DateAndTimeOccurred_DatePicker
        public PersonKeyworkerNoteRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElement(DateAndTimeOccurred_DatePicker);
            ScrollToElement(DateAndTimeOccurred_DatePicker);
            Click(DateAndTimeOccurred_DatePicker);

            return this;
        }

        public PersonKeyworkerNoteRecordPage SetTimeOccurred(String timeoccured)
        {
            WaitForElement(occurredTime);
            System.Threading.Thread.Sleep(1000);
            ScrollToElement(occurredTime);
            WaitForElementVisible(occurredTime);
            Click(occurredTime);
            ClearText(occurredTime);
            System.Threading.Thread.Sleep(1000);
            SendKeys(occurredTime, timeoccured);


            return this;
        }

        //click DateAndTimeOccurred_TimePicker
        public PersonKeyworkerNoteRecordPage ClickDateAndTimeOccurredTimePicker()
        {
            WaitForElement(DateAndTimeOccurred_TimePicker);
            ScrollToElement(DateAndTimeOccurred_TimePicker);
            Click(DateAndTimeOccurred_TimePicker);

            return this;
        }

        //verify personlookupfieldlinktext
        public PersonKeyworkerNoteRecordPage VerifyPersonLookupFieldLinkText(string ExpectedText)
        {
            ScrollToElement(PersonLookupFieldLink);
            WaitForElementVisible(PersonLookupFieldLink);
            ValidateElementByTitle(PersonLookupFieldLink, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield is present or not
        public PersonKeyworkerNoteRecordPage VerifyTotalTimeSpentWithClientMinutesFieldIsDisplayed(bool ExpectedPresent)
        {
            if(ExpectedPresent)
            {
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldLabel); 
                ScrollToElement(TotalTimeSpentWithClientMinutesField);
                WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            }
            else
            {
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesField, 2);
            }

            return this;
        }

        //verify totaltimespentwithclientminutesfield
        public PersonKeyworkerNoteRecordPage VerifyTotalTimeSpentWithClientMinutesFieldText(string ExpectedText)
        {
            ScrollToElement(TotalTimeSpentWithClientMinutesField);
            WaitForElementVisible(TotalTimeSpentWithClientMinutesField);
            ValidateElementValue(TotalTimeSpentWithClientMinutesField, ExpectedText);

            return this;
        }

        //verify totaltimespentwithclientminutesfield error
        public PersonKeyworkerNoteRecordPage VerifyTotalTimeSpentWithClientMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ScrollToElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurredfieldlabel and dateandtimeoccurredfield is displayed or not displayed
        public PersonKeyworkerNoteRecordPage VerifyDateAndTimeOccurredFieldsAreDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(DateAndTimeOccurredFieldLabel);
                ScrollToElement(DateAndTimeOccurredFieldLabel);
                WaitForElementVisible(DateAndTimeOccurred_DateField);
                ScrollToElement(DateAndTimeOccurred_TimeField);
                WaitForElementVisible(DateAndTimeOccurred_TimeField);
            }
            else
            {
                WaitForElementNotVisible(DateAndTimeOccurred_DateField, 2);
                WaitForElementNotVisible(DateAndTimeOccurred_TimeField, 2);
            }

            return this;
        }

        //verify dateandtimeoccurred_datefield
        public PersonKeyworkerNoteRecordPage VerifyDateAndTimeOccurredDateFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_DateField);
            WaitForElementVisible(DateAndTimeOccurred_DateField);
            ValidateElementValue(DateAndTimeOccurred_DateField, ExpectedText);

            return this;
        }

        //verify dateandtimeoccurred_timefield
        public PersonKeyworkerNoteRecordPage VerifyDateAndTimeOccurredTimeFieldText(string ExpectedText)
        {
            ScrollToElement(DateAndTimeOccurred_TimeField);
            WaitForElementVisible(DateAndTimeOccurred_TimeField);
            ValidateElementValue(DateAndTimeOccurred_TimeField, ExpectedText);

            return this;
        }

        //insert keyworkernotestext
        public PersonKeyworkerNoteRecordPage InsertKeyworkerNotesText(String TextToInsert)
        {
            ScrollToElement(KeyworkerNotesTextAreaField);
            WaitForElementVisible(KeyworkerNotesTextAreaField);
            SendKeys(KeyworkerNotesTextAreaField, TextToInsert);

            return this;
        }

        //verify keyworkernotestext field is displayed or not displayed
        public PersonKeyworkerNoteRecordPage VerifyKeyworkerNotesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(KeyworkerNotesTextAreaField);
            }
            else
            {
                WaitForElementNotVisible(KeyworkerNotesTextAreaField, 2);
            }

            return this;
        }

        //verify keyworkernotestext field text
        public PersonKeyworkerNoteRecordPage VerifyKeyworkerNotesTextAreaFieldText(string ExpectedText)
        {
            ScrollToElement(KeyworkerNotesTextAreaField);
            WaitForElementVisible(KeyworkerNotesTextAreaField);
            ValidateElementValue(KeyworkerNotesTextAreaField, ExpectedText);

            return this;
        }

        #endregion

        #region Resident Voice section

        public PersonKeyworkerNoteRecordPage WaitForResidentVoiceSectionToLoad()
        {
            WaitForElement(ResidentVoiceSection);
            SwitchToIframe(ResidentVoiceIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(SectionHeader);
            ScrollToElement(SectionHeader);
            WaitForElement(NewRecordButton);
            WaitForElement(DeleteRecordButton);

            return this;
        }

        public PersonKeyworkerNoteRecordPage VerifyResidentVoiceSectionIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                ScrollToElement(ResidentVoiceSection);
                WaitForElementVisible(ResidentVoiceSection);
            }
            else
            {
                WaitForElementNotVisible(ResidentVoiceSection, 2);
            }

            return this;
        }

        public PersonKeyworkerNoteRecordPage ClickNewRecordButton()
        {
            ScrollToElement(NewRecordButton);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        #endregion

        #region Options Toolbar

        public PersonKeyworkerNoteRecordPage ClickSaveAndClose()
        {
            WaitForElementToBeClickable(SaveNCloseBtn);
            Click(SaveNCloseBtn);

            return this;
        }

        public PersonKeyworkerNoteRecordPage ClickSave()
        {
            WaitForElementToBeClickable(SaveBtn);
            Click(SaveBtn);

            return this;
        }


        public PersonKeyworkerNoteRecordPage NavigateToAttachmentsPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(RelatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(RelatedItemsLeftSubMenu);
                Click(RelatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(AttachmentsMenuItem);
            Click(AttachmentsMenuItem);

            return this;
        }


        //For Keyworker Attachment records in the Keyworker Attachments sub-grid
        public PersonKeyworkerNoteRecordPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public PersonKeyworkerNoteRecordPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonKeyworkerNoteRecordPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                ScrollToElement(RecordIdentifier(RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public PersonKeyworkerNoteRecordPage ValidateRecordIsPresent(int RowPosition, string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RowPosition, RecordID));
                ScrollToElement(RecordIdentifier(RowPosition, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RowPosition, RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RowPosition, RecordID)));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateRecordIsPresent(int RowPosition, Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RowPosition, RecordID.ToString(), ExpectedVisible);

        }

        public PersonKeyworkerNoteRecordPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateHeaderCellText(string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(ExpectedText));
            ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

            return this;
        }

        public PersonKeyworkerNoteRecordPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ClickColumnHeader(int CellPosition, string HeaderCellText)
        {
            WaitForElementToBeClickable(gridPageHeaderElement(CellPosition, HeaderCellText));
            ScrollToElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            Click(gridPageHeaderElement(CellPosition, HeaderCellText));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateHeaderIsVisible(int CellPosition, string ExpectedText, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(gridPageHeaderElement(CellPosition, ExpectedText));
                WaitForElementVisible(gridPageHeaderElement(CellPosition, ExpectedText));
            }
            else
            {
                WaitForElementNotVisible(gridPageHeaderElement(CellPosition, ExpectedText), 3);
            }

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateColumnIsSortedByDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateColumnsSortOrderDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));

            return this;
        }

        public PersonKeyworkerNoteRecordPage ValidateColumnSortOrderAscending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));

            return this;
        }

        #endregion
    }
}
