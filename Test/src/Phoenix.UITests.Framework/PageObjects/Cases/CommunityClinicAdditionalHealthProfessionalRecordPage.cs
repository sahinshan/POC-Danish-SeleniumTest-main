using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Cases
{
    public class CommunityClinicAdditionalHealthProfessionalRecordPage : CommonMethods
    {
        public CommunityClinicAdditionalHealthProfessionalRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=healthappointmentadditionalprofessional')]");

        readonly By pageHeader = By.XPath("//h1[text() = 'Community/Clinic Additional Health Professional: ']");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");

        readonly By communityClinicHealthAppointmentName_Field = By.Id("CWField_healthappointmentid_cwname");
        readonly By communityClinicHealthAppointmentNameField_Value = By.Id("CWField_healthappointmentid_Link");
        readonly By communityClinicHealthAppointmentNameField_LookupButton = By.Id("CWLookupBtn_healthappointmentid");
        readonly By healthProfessional_Field = By.Id("CWField_healthprofessionalid_cwname");
        readonly By healthProfessionalField_Value = By.Id("CWField_healthprofessionalid_Link");
        readonly By healthProfessionalField_LookupButton = By.Id("CWLookupBtn_healthprofessionalid");
        readonly By professionalRemainingForFullDuration_YesButton = By.Id("CWField_professionalremainingforfullduration_1");
        readonly By professionalRemainingForFullDuration_NoButton = By.Id("CWField_professionalremainingforfullduration_0");
        readonly By responsibleUserField_Value = By.Id("CWField_ownerid_Link");
        readonly By responsibleUserField_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By startTime_Field = By.Id("CWField_starttime");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By endTime_Field = By.Id("CWField_endtime");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By healthProfessionalFieldRequiredMessage = By.XPath("//li[@id = 'CWControlHolder_healthprofessionalid']/label/span");

        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);
            this.WaitForElement(backButton);            


            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage TapSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage TapSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage TapBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }
        
        public CommunityClinicAdditionalHealthProfessionalRecordPage ClickCommunityClinicHealthAppointmentFieldLookupButton()
        {
            WaitForElement(communityClinicHealthAppointmentNameField_LookupButton);
            Click(communityClinicHealthAppointmentNameField_LookupButton);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ClickHealthProfessionalFieldLookupButton()
        {
            WaitForElement(healthProfessionalField_LookupButton);
            Click(healthProfessionalField_LookupButton);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ClickResponsibleTeamFieldLookupButton()
        {
            WaitForElement(responsibleUserField_LookupButton);
            Click(responsibleUserField_LookupButton);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage SelectProfessionalRemainingForFullDurationOption(bool optionToSelect)
        {
            WaitForElement(professionalRemainingForFullDuration_YesButton);
            WaitForElement(professionalRemainingForFullDuration_NoButton);
            if (optionToSelect)
            {
                Click(professionalRemainingForFullDuration_YesButton);
            }
            else
            {
                Click(professionalRemainingForFullDuration_NoButton);
            }

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertStartTime(string TimeToInsert)
        {
            WaitForElement(startTime_Field);
            SendKeys(startTime_Field, TimeToInsert);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage InsertEndTime(string TextToInsert)
        {
            WaitForElement(endTime_Field);
            SendKeys(endTime_Field, TextToInsert);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalRemainingForFullDurationOption(bool optionSelected)
        {
            WaitForElement(professionalRemainingForFullDuration_YesButton);
            WaitForElement(professionalRemainingForFullDuration_NoButton);
            if (optionSelected)
            {
                ValidateElementChecked(professionalRemainingForFullDuration_YesButton);
                ValidateElementNotChecked(professionalRemainingForFullDuration_NoButton);
            }
            else
            {
                ValidateElementNotChecked(professionalRemainingForFullDuration_YesButton);
                ValidateElementChecked(professionalRemainingForFullDuration_NoButton);
            }

            return this;
        }
        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateCommunityClinicHealthAppointmentFieldValue(string expectedText)
        {
            WaitForElement(communityClinicHealthAppointmentNameField_Value);
            string actualFieldValue = GetElementByAttributeValue(communityClinicHealthAppointmentNameField_Value, "title");
            Assert.AreEqual(expectedText, actualFieldValue);
            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateHealthProfessionalFieldValue(string expectedText)
        {
            WaitForElement(healthProfessionalField_Value);
            string actualFieldValue = GetElementByAttributeValue(healthProfessionalField_Value, "title");
            Assert.AreEqual(expectedText, actualFieldValue);
            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateResponsibleUserFieldValue(string expectedText)
        {
            WaitForElement(responsibleUserField_Value);
            string actualFieldValue = GetElementByAttributeValue(responsibleUserField_Value, "title");
            Assert.AreEqual(expectedText, actualFieldValue);
            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartDate(string expectedText)
        {
            WaitForElement(startDate_Field);

            string actualDateValue = GetElementValueByJavascript("CWField_startdate");
            Assert.AreEqual(expectedText, actualDateValue);

            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartTime(string expectedText)
        {
            WaitForElement(startTime_Field);

            string actualTimeValue = GetElementValueByJavascript("CWField_starttime");
            Assert.AreEqual(expectedText, actualTimeValue);

            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndDate(string expectedText)
        {
            WaitForElement(endDate_Field);

            string actualDateValue = GetElementValueByJavascript("CWField_startdate");
            Assert.AreEqual(expectedText, actualDateValue);

            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndTime(string expectedText)
        {
            WaitForElement(endTime_Field);

            string actualTimeValue = GetElementValueByJavascript("CWField_endtime");
            Assert.AreEqual(expectedText, actualTimeValue);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartDateFieldDisabled()
        {
            WaitForElement(startDate_Field);
            ValidateElementDisabled(startDate_Field);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateStartTimeFieldDisabled()
        {
            WaitForElement(startTime_Field);
            ValidateElementDisabled(startTime_Field);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndDateFieldDisabled()
        {
            WaitForElement(endDate_Field);
            ValidateElementDisabled(endDate_Field);
            return this;
        }


        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateEndTimeFieldDisabled()
        {
            WaitForElement(endTime_Field);
            ValidateElementDisabled(endTime_Field);
            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateNotificationMessage(string expectedText)
        {
            WaitForElement(notificationMessage);
            string actualNotificationMessage = GetElementText(notificationMessage);
            Assert.AreEqual(expectedText, actualNotificationMessage);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionalRecordPage ValidateProfessionalFieldRequiredErrorMessage(string expectedText)
        {
            WaitForElement(healthProfessionalFieldRequiredMessage);            
            string actualNotificationMessage = GetElementByAttributeValue(healthProfessionalFieldRequiredMessage, "title");
            Assert.AreEqual(expectedText, actualNotificationMessage);

            return this;
        }
    }
}
