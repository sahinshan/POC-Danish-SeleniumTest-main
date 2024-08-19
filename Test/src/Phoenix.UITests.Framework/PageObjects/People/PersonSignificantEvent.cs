using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonSignificantEvent : CommonMethods
    {
        public PersonSignificantEvent(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
       // readonly By CWNavItem_PersonChronologyFrame = By.Id("CWNavItem_PersonChronologyFrame");
        //readonly By includeEvent_ChronologyFrame = By.Id("CWIncludedEvents");

        readonly By NewSignificantEventIFrame = By.Id("iframe_CW_NewSignificantEvent");

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
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
       

       // readonly By EventCatogery_Field = By.Id("CWLookupBtn_significanteventcategoryid");
        readonly By EventDate_Field = By.Id("CWField_eventdate");
        readonly By ResponsibleTeam_Field = By.Id("CWLookupBtn_ownerid");
        readonly By EventCatogery_Field = By.Id("CWField_significanteventcategoryid_cwname");





       

       

        public PersonSignificantEvent WaitForPersonNewSignificantEventPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(NewSignificantEventIFrame);
            SwitchToIframe(NewSignificantEventIFrame);

          

            // WaitForElement(pageHeader);

            //  WaitForElement(NewRecordButton);
            // WaitForElement(PrintButton);

            return this;
        }

        public PersonSignificantEvent WaitForSignificantEventRecordPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CW_ViewSignificantEvent);
            SwitchToIframe(CW_ViewSignificantEvent);

          //  WaitForElement(IncludeEvent);
            //SwitchToIframe(IncludeEvent);


            // WaitForElement(pageHeader);

            //  WaitForElement(NewRecordButton);
            // WaitForElement(PrintButton);

            return this;
        }

       

        public PersonSignificantEvent OpenSignificantEventRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonSignificantEvent SelectPersonChronologyRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }    

      

        public PersonSignificantEvent ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonSignificantEvent ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }
             

       
      
        public PersonSignificantEvent ValidateDateFromFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(DateFrom_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonSignificantEvent ValidateDateToFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(DateTo_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonSignificantEvent ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }

        public PersonSignificantEvent ValidateNotificationHolderMessage(string ExpectedMessage)
        {
            WaitForElementVisible(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }



        public PersonSignificantEvent ValidateNoNotificationErrorMessageVisibile(bool ExpectedText)
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


        public PersonSignificantEvent ValidateTitleFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(Title_FieldErrorArea, ExpectedMessage);

            return this;
        }

      

        public PersonSignificantEvent InsertEventDate(string EventDateToInsert)
        {
            SendKeys(EventDate_Field, EventDateToInsert);

            return this;
        }

        public PersonSignificantEvent InsertEventCatogery(string EventCatogery)
        {
            SendKeys(EventCatogery_Field, EventCatogery);

            return this;
        }




        public PersonSignificantEvent ClickEventCatogeryLookup()
        {
            
            Click(EventCatogery_Field);

            return this;
        }

        public PersonSignificantEvent ClickResponsibleTeamLookup()
        {

            Click(ResponsibleTeam_Field);

            return this;
        }

       

        public PersonSignificantEvent ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);

            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }


    }
}
