using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonChronologyRecordPage : CommonMethods
    {
        public PersonChronologyRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_PersonChronologyFrame = By.Id("CWUrlPanel_IFrame");
        readonly By includeEvent_ChronologyFrame = By.Id("CWIncludedEvents");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Chronology Details: Chronology Child in Need']");

        readonly By NewRecordButton = By.XPath("//button[@title='Create New Chronology']");
        readonly By PrintButton = By.XPath("//button[@title='Print']");
        readonly By SaveButton = By.XPath("//*[@id='CWToolbarButtons']/button[1]");
        readonly By ViewSavedRecordButton = By.XPath("//*[@id='CWToolbarButtons']/button[2]");
        readonly By AdditionalEventButton = By.XPath("//*[@id='CWToolbarButtons']/button[3]");
        readonly By Title_Field = By.Id("CWTitle");
        readonly By StartDate_Field = By.Id("CWStartDate");
        readonly By EndDate_Field = By.Id("CWEndDate");
        readonly By category_CheckBox = By.Id("CWCheckAll");
        readonly By DateFrom_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[2]");
        readonly By DateTo_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[3]");
        readonly By Title_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By NotificationMessage = By.Id("CWNotificationMessage_PersonChronology");
        readonly By NotificationHolderMessage = By.Id("CWNotificationHolder_PersonChronology");
        readonly By ClickHereToHide = By.XPath("//*[@id='CWNotificationHolder_PersonChronology']/a");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        readonly By IncludeEvent = By.Id("CWIncludedEvents");
        readonly By CW_ViewSignificantEvent = By.Id("iframe_CW_ViewSignificantEvent");
        readonly By eventdate_Field = By.Id("CWField_eventdate");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");       
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By ExcludedEventsIFrame = By.Id("CWExcludedEvents");
        readonly By ExcludeEventButton = By.XPath("//*[@id='CWToolbarButtons']/button[@title='Include in Chronology']");
        readonly By SaveSnapshotButton = By.XPath("//*[@id='CWToolbarButtons']/button[@title='Save Snapshot']");
        readonly By ToolBarMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By ViewSnapShotButton = By.XPath("//*[@id='CWToolbarMenu']/div/div[1]/a");
        readonly By ViewSnapshot_PrintButton = By.Id("btnPrint");
        readonly By AddANewSignificantEventButton = By.XPath("//*[@id='CWToolbarMenu']/div/div[2]/a");


        public PersonChronologyRecordPage WaitForIncludeEventRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            WaitForElement(IncludeEvent);
            SwitchToIframe(IncludeEvent);

            return this;
        }

        public PersonChronologyRecordPage WaitForExcludeChronologyRecordsToLoad()
        {

            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            WaitForElement(ExcludedEventsIFrame);
            SwitchToIframe(ExcludedEventsIFrame);

            return this;
        }

        public PersonChronologyRecordPage WaitForPersonChronologyRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            return this;
        }

        public PersonChronologyRecordPage WaitForSignificantEventRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CW_ViewSignificantEvent);
            SwitchToIframe(CW_ViewSignificantEvent);

            return this;
        }

        public PersonChronologyRecordPage WaitForPersonChronologyNewRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            return this;
        }

        public PersonChronologyRecordPage OpenSignificantEventRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonChronologyRecordPage SelectPersonChronologyRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElementViaJavascript(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonChronologyRecordPage TapPrintButton()
        {
            WaitForElementToBeClickable(PrintButton);
            Click(PrintButton);

            return this;
        }

        public PersonChronologyRecordPage TapViewSnapShotPrintButton()
        {
            WaitForElementToBeClickable(ViewSnapshot_PrintButton);
            Click(ViewSnapshot_PrintButton);

            return this;
        }

        public PersonChronologyRecordPage ClickViewSavedSnapShotButton()
        {
            WaitForElementToBeClickable(ViewSnapShotButton);
            Click(ViewSnapShotButton);

            return this;
        }

        public PersonChronologyRecordPage ClickAddANewSignificantEventButton()
        {
            WaitForElementToBeClickable(AddANewSignificantEventButton);
            Click(AddANewSignificantEventButton);

            return this;
        }
        public PersonChronologyRecordPage ClickToolBarMenuButton()
        {
            WaitForElementToBeClickable(ToolBarMenuButton);
            Click(ToolBarMenuButton);

            return this;
        }

        public PersonChronologyRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonChronologyRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonChronologyRecordPage ClickViewSaveRecordButton()
        {
            WaitForElementToBeClickable(ViewSavedRecordButton);
            Click(ViewSavedRecordButton);

            return this;
        }

        public PersonChronologyRecordPage ClickAdditionalEventButton()
        {
            WaitForElementToBeClickable(AdditionalEventButton);
            Click(AdditionalEventButton);

            return this;
        }

        public PersonChronologyRecordPage ClickSaveSnapshotButton()
        {
            WaitForElementToBeClickable(SaveSnapshotButton);
            Click(SaveSnapshotButton);

            return this;
        }

        public PersonChronologyRecordPage ClickExcludeEventButton()
        {
            WaitForElement(ExcludeEventButton);
            System.Threading.Thread.Sleep(2000);
            Click(ExcludeEventButton);
            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public PersonChronologyRecordPage ValidateDateFromFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(DateFrom_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonChronologyRecordPage ValidateDateToFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(DateTo_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonChronologyRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }

        public PersonChronologyRecordPage ValidateNotificationHolderMessage(string ExpectedMessage)
        {
            WaitForElementVisible(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }

        public PersonChronologyRecordPage ValidateNoNotificationErrorMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NotificationMessage);

            }
            else
            {
                WaitForElementNotVisible(NotificationMessage, 5);
            }
            return this;
        }

        public PersonChronologyRecordPage ValidateTitleFieldErrorMessage(string ExpectedMessage)
        {
            WaitForElementVisible(Title_FieldErrorArea);
            ValidateElementText(Title_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonChronologyRecordPage InsertStartDate(string StartDateToInsert)
        {
            SendKeys(StartDate_Field, StartDateToInsert);

            return this;
        }

        public PersonChronologyRecordPage InsertEventDate(string EventDateToInsert)
        {
            SendKeys(eventdate_Field, EventDateToInsert);

            return this;
        }

        public PersonChronologyRecordPage InsertEndDate(string EndDateToInsert)
        {
            SendKeys(EndDate_Field, EndDateToInsert);

            return this;
        }

        public PersonChronologyRecordPage InsertTitle(string TileToInsert)
        {
            SendKeys(Title_Field, TileToInsert);

            return this;
        }

        public PersonChronologyRecordPage TapCategorie()
        {
            WaitForElementToBeClickable(category_CheckBox);
            Click(category_CheckBox);

            return this;
        }

        public PersonChronologyRecordPage TapClickHereToHideMessage()
        {
            WaitForElementToBeClickable(ClickHereToHide);
            Click(ClickHereToHide);

            return this;
        }

        public PersonChronologyRecordPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
           
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }
        
    }
}
