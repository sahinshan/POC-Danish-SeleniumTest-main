using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class BookingTypeClashActionRecordPage : CommonMethods
    {

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By cpbookingtypeclashaction_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingtypeclashaction&')]");

        readonly By pageHeader = By.XPath("//h1[text()='Booking Type Clash Action: ']");

        readonly By BackButton = By.Id("BackButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By RunOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By BookingType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_cpbookingtypeid']/label[text()='Booking Type']");
        readonly By BookingType_LinkField = By.Id("CWField_cpbookingtypeid_Link");
        readonly By BookingType_Mandatory = By.XPath("//*[@id='CWLabelHolder_cpbookingtypeid']/label/span[@class='mandatory']");
        readonly By BookingType_LookUpButton = By.Id("CWLookupBtn_cpbookingtypeid");
        readonly By BookingType_RemoveButton = By.Id("CWClearLookup_cpbookingtypeid");

        readonly By BookingTypeClass_FieldHeader = By.XPath("//*[@id='CWLabelHolder_bookingtypeclassid']/label[text()='Booking Type Class']");
        readonly By BookingTypeClass_Mandatory = By.XPath("//*[@id='CWLabelHolder_bookingtypeclassid']/label/span[@class='mandatory']");
        readonly By BookingTypeClass_PickList = By.Id("CWField_bookingtypeclassid");


        readonly By Global_FieldHeader = By.XPath("//*[@id='CWLabelHolder_defaultdoublebookingactionid']/label[text()='Global']");
        readonly By Global_Mandatory = By.XPath("//*[@id='CWLabelHolder_defaultdoublebookingactionid']/label/span[@class='mandatory']");
        readonly By Global_PickList = By.Id("CWField_defaultdoublebookingactionid");


        readonly By ThisBooking_FieldHeader = By.XPath("//*[@id='CWLabelHolder_doublebookingactionid']/label[text()='This Booking']");
        readonly By ThisBooking_Mandatory = By.XPath("//*[@id='CWLabelHolder_doublebookingactionid']/label/span[@class='mandatory']");
        readonly By ThisBooking_PickList = By.Id("CWField_doublebookingactionid");


        public BookingTypeClashActionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public BookingTypeClashActionRecordPage WaitForBookingTypeClashActionRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(cpbookingtypeclashaction_Iframe);
            SwitchToIframe(cpbookingtypeclashaction_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);

            return this;
        }

        public BookingTypeClashActionRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public BookingTypeClashActionRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public BookingTypeClashActionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public BookingTypeClashActionRecordPage SelectBookingTypeClass(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingTypeClass_PickList);
            ScrollToElement(BookingTypeClass_PickList);
            SelectPicklistElementByText(BookingTypeClass_PickList, TextToSelect);

            return this;
        }

        public BookingTypeClashActionRecordPage SelectGlobal(string TextToSelect)
        {
            WaitForElementToBeClickable(Global_PickList);
            ScrollToElement(Global_PickList);
            SelectPicklistElementByText(Global_PickList, TextToSelect);

            return this;
        }

        public BookingTypeClashActionRecordPage SelectThisBooking(string TextToSelect)
        {
            WaitForElementToBeClickable(ThisBooking_PickList);
            ScrollToElement(ThisBooking_PickList);
            SelectPicklistElementByText(ThisBooking_PickList, TextToSelect);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(BookingType_FieldHeader);
            if (IsMandatory)
                WaitForElementVisible(BookingType_Mandatory);
            else
                WaitForElementNotVisible(BookingType_Mandatory, 5);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeClassFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(BookingTypeClass_FieldHeader);
            if (IsMandatory)
                WaitForElementVisible(BookingTypeClass_Mandatory);
            else
                WaitForElementNotVisible(BookingTypeClass_Mandatory, 5);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateGlobalFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(Global_FieldHeader);
            if (IsMandatory)
                WaitForElementVisible(Global_Mandatory);
            else
                WaitForElementNotVisible(Global_Mandatory, 5);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateThisBookingFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(ThisBooking_FieldHeader);
            if (IsMandatory)
                WaitForElementVisible(ThisBooking_Mandatory);
            else
                WaitForElementNotVisible(ThisBooking_Mandatory, 5);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeClassSelectedText(string ExpectedText)
        {
            WaitForElementVisible(BookingTypeClass_PickList);
            ScrollToElement(BookingTypeClass_PickList);
            ValidatePicklistSelectedText(BookingTypeClass_PickList, ExpectedText);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateGlobalSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Global_PickList);
            ScrollToElement(Global_PickList);
            ValidatePicklistSelectedText(Global_PickList, ExpectedText);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateThisBookingSelectedText(string ExpectedText)
        {
            WaitForElementVisible(ThisBooking_PickList);
            ScrollToElement(ThisBooking_PickList);
            ValidatePicklistSelectedText(ThisBooking_PickList, ExpectedText);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(BookingType_LookUpButton);
            ScrollToElement(BookingType_LookUpButton);

            if (IsDisabled)
                WaitForElementToBeDisable(BookingType_LookUpButton);
            else
                ValidateElementNotDisabled(BookingType_LookUpButton);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeClassPickListIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(BookingTypeClass_PickList);
            ScrollToElement(BookingTypeClass_PickList);

            if (IsDisabled)
                WaitForElementToBeDisable(BookingTypeClass_PickList);
            else
                ValidateElementNotDisabled(BookingTypeClass_PickList);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateGlobalPickListIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Global_PickList);
            ScrollToElement(Global_PickList);

            if (IsDisabled)
                WaitForElementToBeDisable(Global_PickList);
            else
                ValidateElementNotDisabled(Global_PickList);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateThisBookingPickListIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ThisBooking_PickList);
            ScrollToElement(ThisBooking_PickList);

            if (IsDisabled)
                WaitForElementToBeDisable(ThisBooking_PickList);
            else
                ValidateElementNotDisabled(ThisBooking_PickList);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeLinkText(string ExpectedText)
        {
            WaitForElementVisible(BookingType_FieldHeader);
            WaitForElementVisible(BookingType_LinkField);
            ScrollToElement(BookingType_LinkField);
            ValidateElementText(BookingType_LinkField, ExpectedText);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateBookingTypeRemoveButtonVisibility(bool IsVisible)
        {
            WaitForElementVisible(BookingType_LinkField);
            ScrollToElement(BookingType_LinkField);

            if (IsVisible)
                WaitForElementVisible(BookingType_RemoveButton);
            else
                WaitForElementNotVisible(BookingType_RemoveButton, 3);

            return this;
        }

        public BookingTypeClashActionRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(RunOnDemandWorkflowButton);

            return this;
        }

        public BookingTypeClashActionRecordPage ClickThisBookingDropDown()
        {
            WaitForElementToBeClickable(ThisBooking_PickList);
            ScrollToElement(ThisBooking_PickList);
            Click(ThisBooking_PickList);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateThisBookingDropDownText(string ExpectedText)
        {
            WaitForElementVisible(ThisBooking_PickList);
            ScrollToElement(ThisBooking_PickList);
            ValidatePicklistContainsElementByText(ThisBooking_PickList, ExpectedText);

            return this;
        }

        public BookingTypeClashActionRecordPage ValidateDeleteRecordButtonIsPresent(bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(DeleteRecordButton);
            else
                WaitForElementNotVisible(DeleteRecordButton, 3);

            return this;
        }

    }
}
