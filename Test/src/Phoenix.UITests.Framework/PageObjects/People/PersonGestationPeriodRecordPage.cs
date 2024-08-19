using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonGestationPeriodRecordPage : CommonMethods
    {
        public PersonGestationPeriodRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=persongestationperiod&id')]");
        readonly By iframe_PersonGestationRecord = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=persongestationperiod&pid')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        readonly By mother_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personid']/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By child_FieldTitle = By.XPath("//*[@id='CWLabelHolder_childid']/label");
        readonly By startDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By number_FieldTitle = By.XPath("//*[@id='CWLabelHolder_totaldaysorweeks']/label");
        readonly By endDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By daysWeeks_FieldTitle = By.XPath("//*[@id='CWLabelHolder_gestationperiodtypeid']/label");
        readonly By endReason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_gestationendreasonid']/label");
        readonly By notes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_notes']/label");

        readonly By notificationHolderMessage = By.Id("CWNotificationHolder_DataForm");
        readonly By notificationMessage_StartField = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");
        readonly By notificationMessage_NumberField = By.XPath("//*[@id='CWControlHolder_totaldaysorweeks']/label/span");
        readonly By notificationMessage_DaysWeeks = By.XPath("//*[@id='CWControlHolder_gestationperiodtypeid']/label/span");
     

        readonly By motherField_LookUpButton = By.Id("CWLookupBtn_personid");
        readonly By childField_LookUpButton = By.Id("CWLookupBtn_childid");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By number_Field = By.Id("CWField_totaldaysorweeks");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By daysWeeks_Field = By.Id("CWField_gestationperiodtypeid");
        readonly By endReason_LookUpField = By.Id("CWLookupBtn_gestationendreasonid");
        readonly By notes_Field = By.Id("CWField_notes");


        

        public PersonGestationPeriodRecordPage WaitForPersonGestationRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_PersonGestationRecord);
            this.SwitchToIframe(iframe_PersonGestationRecord);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(mother_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(child_FieldTitle);
            this.WaitForElement(startDate_FieldTitle);
            this.WaitForElement(number_FieldTitle);
            this.WaitForElement(endDate_FieldTitle);
            this.WaitForElement(daysWeeks_FieldTitle);
            this.WaitForElement(endReason_FieldTitle);
            this.WaitForElement(notes_FieldTitle);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Gestation Period:\r\n" + TaskTitle);

           

            return this;
        }

        public PersonGestationPeriodRecordPage WaitForPersonGestationPeriodRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(mother_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(child_FieldTitle);
            this.WaitForElement(startDate_FieldTitle);
            this.WaitForElement(number_FieldTitle);
            this.WaitForElement(endDate_FieldTitle);
            this.WaitForElement(daysWeeks_FieldTitle);
            this.WaitForElement(endReason_FieldTitle);
            this.WaitForElement(notes_FieldTitle);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Gestation Period:\r\n" + TaskTitle);



            return this;
        }
        public PersonGestationPeriodRecordPage ClickChildLookupButton()
        {
            WaitForElementToBeClickable(childField_LookUpButton);
            Click(childField_LookUpButton);

            return this;
        }

        public PersonGestationPeriodRecordPage ClickMotherLookupButton()
        {
            WaitForElementToBeClickable(motherField_LookUpButton);
            Click(motherField_LookUpButton);

            return this;
        }


        public PersonGestationPeriodRecordPage ClickEndReasonLookupButton()
        {
            WaitForElementToBeClickable(endReason_LookUpField);
            Click(endReason_LookUpField);

            return this;
        }
        public PersonGestationPeriodRecordPage SelectDaysWeeks(String OptionToSelect)
        {
            WaitForElementVisible(daysWeeks_Field);
            SelectPicklistElementByValue(daysWeeks_Field, OptionToSelect);

            return this;
        }
        public PersonGestationPeriodRecordPage InsertStartDate(string SearchQuery)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, SearchQuery);


            return this;
        }

       

        public PersonGestationPeriodRecordPage InsertEndDate(string SearchQuery)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, SearchQuery);


            return this;
        }
        public PersonGestationPeriodRecordPage InsertNotes(String TextToInsert)
        {
            WaitForElement(notes_Field);
            SendKeys(notes_Field, TextToInsert);
            return this;
        }

        public PersonGestationPeriodRecordPage InsertNumber(String TextToInsert)
        {
            WaitForElement(number_Field);
            SendKeys(number_Field, TextToInsert);
            return this;
        }


        public PersonGestationPeriodRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
       
       
        
        public PersonGestationPeriodRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
       
        public PersonGestationPeriodRecordPage ValidateStartDate(String ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            ValidateElementTextContainsText(startDate_Field, ExpectedText);
            return this;
        }

        public PersonGestationPeriodRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public PersonGestationPeriodRecordPage ValidateNotificationMessage(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(notificationHolderMessage);

            }
            else
            {
                WaitForElementNotVisible(notificationHolderMessage, 5);
            }
            return this;
        }

        public PersonGestationPeriodRecordPage ValidateStartFieldNotificationMessage(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(notificationMessage_StartField);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage_StartField, 5);
            }
            return this;
        }

        public PersonGestationPeriodRecordPage ValidateNumberFieldNotificationMessage(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(notificationMessage_NumberField);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage_NumberField, 5);
            }
            return this;
        }

        public PersonGestationPeriodRecordPage ValidateDaysWeeksFieldNotificationMessage(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(notificationMessage_DaysWeeks);

            }
            else
            {
                WaitForElementNotVisible(notificationMessage_DaysWeeks, 5);
            }
            return this;
        }

       

    }
}
