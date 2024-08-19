using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class BookingTypeRecordPage : CommonMethods
    {
        public BookingTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By availabilityTypes_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingtype&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By Name_Field = By.Id("CWField_name");
        readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

        readonly By DefaultStartTime_Field = By.Id("CWField_defaultstarttime");
        readonly By Duration_Field = By.Id("CWField_duration");
        readonly By BookingTypeShortCode_Field = By.Id("CWField_bookingtypeshortcode");

        readonly By BookingTypeClass_Picklist = By.Id("CWField_diarybookingsvalidityid");
        readonly By Working_ContractedTime_Picklist = By.Id("CWField_bookingcountclassid");

        readonly By ValidFromDate_Field = By.Id("CWField_validfromdate");
        readonly By ValidToDate_Field = By.Id("CWField_validtodate");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        public BookingTypeRecordPage WaitForBookingTypesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(availabilityTypes_Iframe);
            SwitchToIframe(availabilityTypes_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElementVisible(pageHeader);

            WaitForElement(Name_Field);

            return this;
        }


        public BookingTypeRecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);
            SendKeysWithoutClearing(Name_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage SelectBookingTypeClass(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingTypeClass_Picklist);
            MoveToElementInPage(BookingTypeClass_Picklist);
            SelectPicklistElementByText(BookingTypeClass_Picklist, TextToSelect);

            return this;
        }

        public BookingTypeRecordPage InsertDefaultStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(DefaultStartTime_Field);
            MoveToElementInPage(DefaultStartTime_Field);
            SendKeys(DefaultStartTime_Field, TextToInsert);
            SendKeysWithoutClearing(DefaultStartTime_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage InsertDuration(string TextToInsert)
        {
            WaitForElementToBeClickable(Duration_Field);
            MoveToElementInPage(Duration_Field);
            SendKeys(Duration_Field, TextToInsert);
            SendKeysWithoutClearing(Duration_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage InsertBookingTypeShortCode(string TextToInsert)
        {
            WaitForElementToBeClickable(BookingTypeShortCode_Field);
            MoveToElementInPage(BookingTypeShortCode_Field);
            SendKeys(BookingTypeShortCode_Field, TextToInsert);
            SendKeysWithoutClearing(BookingTypeShortCode_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage InsertValidFromDate(string TextToInsert)
        {
            WaitForElementToBeClickable(ValidFromDate_Field);
            MoveToElementInPage(ValidFromDate_Field);
            SendKeys(ValidFromDate_Field, TextToInsert);
            SendKeysWithoutClearing(ValidFromDate_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage InsertValidToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(ValidToDate_Field);
            MoveToElementInPage(ValidToDate_Field);
            SendKeys(ValidToDate_Field, TextToInsert);
            SendKeysWithoutClearing(ValidToDate_Field, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public BookingTypeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public BookingTypeRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public BookingTypeRecordPage ClickDeleteButton()
        {
            MoveToElementInPage(deleteButton);
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public BookingTypeRecordPage ClickBackButton()
        {
            MoveToElementInPage(backButton);
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public BookingTypeRecordPage ValidateNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(NotificationMessage);
            MoveToElementInPage(NotificationMessage);
            ValidateElementTextContainsText(NotificationMessage, ExpectedText);
            return this;
        }

    }
}
