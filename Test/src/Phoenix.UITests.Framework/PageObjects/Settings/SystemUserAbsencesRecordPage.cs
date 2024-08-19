
using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserAbsencesRecordPage : CommonMethods
    {
        public SystemUserAbsencesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=openendedabsence&')]");
        
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By assignRecord_Button = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElements_Button = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By Staff_LookUpButton = By.XPath("//button[@id='CWLookupBtn_systemuserid']");
        readonly By Provider_LookUpButton = By.XPath("//button[@id='CWLookupBtn_locationid']");
        readonly By BookingType_LookUpButton = By.XPath("//button[@title='Lookup Booking Type']");
        readonly By Contract_LookUpButton = By.XPath("//button[@title='Lookup Contract']");

        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditLinkButton = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Field Labels

        readonly By Staff_FieldLabel = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label");
        readonly By Location_FieldLabel = By.XPath("//*[@id='CWLabelHolder_locationid']/label");
        readonly By BookingType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_cpbookingtypeid']/label");
        readonly By Contract_FieldLabel = By.XPath("//*[@id='CWLabelHolder_contractid']/label");
        readonly By StartDateNTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdateandtime']/label");
        readonly By EndDateNTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddateandtime']/label");

        #endregion

        #region Fields

        readonly By OpenEndedAbsenceStartDate_Field = By.XPath("//*[@id='CWField_startdateandtime']");
        readonly By OpenEndedAbsenceStartTime_Field = By.XPath("//*[@id='CWField_startdateandtime_Time']");
        readonly By OpenEndedAbsenceEndDate_Field = By.XPath("//*[@id='CWField_enddateandtime']");
        readonly By OpenEndedAbsenceEndTime_Field = By.XPath("//*[@id='CWField_enddateandtime_Time']");
        readonly By Comments_Field = By.XPath("//*[@id='CWField_comments']"); 

        #endregion


        public SystemUserAbsencesRecordPage WaitForSystemUserAbsencesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(Staff_FieldLabel);
            WaitForElementVisible(Location_FieldLabel);
            WaitForElementVisible(BookingType_FieldLabel);
            WaitForElementVisible(Contract_FieldLabel);
            WaitForElementVisible(StartDateNTime_FieldLabel);
            WaitForElementVisible(EndDateNTime_FieldLabel);
           

            return this;
        }

        

        public SystemUserAbsencesRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemUserAbsencesRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        public SystemUserAbsencesRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserAbsencesRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(save_Button);
            WaitForElementVisible(saveAndClose_Button);
            WaitForElementVisible(assignRecord_Button);
            WaitForElementVisible(delete_Button);            
            WaitForElementVisible(additionalToolbarElements_Button);

            return this;
        }

        public SystemUserAbsencesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

    
      
        public SystemUserAbsencesRecordPage InsertOpenEndedAbsenceStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(OpenEndedAbsenceStartDate_Field);
            SendKeys(OpenEndedAbsenceStartDate_Field, TextToInsert + Keys.Tab);
            return this;
        }
        public SystemUserAbsencesRecordPage InsertOpenEndedAbsenceEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(OpenEndedAbsenceEndDate_Field);
            SendKeys(OpenEndedAbsenceEndDate_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserAbsencesRecordPage InsertOpenEndedAbsenceStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(OpenEndedAbsenceStartTime_Field);
            SendKeys(OpenEndedAbsenceStartTime_Field, TextToInsert + Keys.Tab);
            SendKeysWithoutClearing(OpenEndedAbsenceStartTime_Field, Keys.Tab);

            return this;
        }

        public SystemUserAbsencesRecordPage InsertOpenEndedAbsenceEndTime(string TextToInsert)
        {
            WaitForElementToBeClickable(OpenEndedAbsenceEndTime_Field);
            SendKeys(OpenEndedAbsenceEndTime_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserAbsencesRecordPage ValidateStaffLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Staff_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Staff_LookUpButton);
            }

            return this;
        }

        public SystemUserAbsencesRecordPage ValidateProviderLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Provider_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Provider_LookUpButton);
            }

            return this;
        }

        public SystemUserAbsencesRecordPage ValidateBookingTypeLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(BookingType_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(BookingType_LookUpButton);
            }

            return this;
        }

        public SystemUserAbsencesRecordPage ValidateContractLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Contract_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Contract_LookUpButton);
            }

            return this;
        }

        public SystemUserAbsencesRecordPage ValidatePlannedStartDateDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(OpenEndedAbsenceStartDate_Field);
            else
                ValidateElementEnabled(OpenEndedAbsenceStartDate_Field);

            return this;
        }

        public SystemUserAbsencesRecordPage ValidatePlannedStartDateTimeDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(OpenEndedAbsenceStartTime_Field);
            else
                ValidateElementEnabled(OpenEndedAbsenceStartTime_Field);

            return this;
        }

        public SystemUserAbsencesRecordPage ValidatePlannedEndDateDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(OpenEndedAbsenceEndDate_Field);
            else
                ValidateElementEnabled(OpenEndedAbsenceEndDate_Field);

            return this;
        }

        public SystemUserAbsencesRecordPage ValidatePlannedEndDateTimeDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(OpenEndedAbsenceEndTime_Field);
            else
                ValidateElementEnabled(OpenEndedAbsenceEndTime_Field);

            return this;
        }

        public SystemUserAbsencesRecordPage ValidateCommentsFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Comments_Field);
            else
                ValidateElementEnabled(Comments_Field);

            return this;
        }
    }
}
