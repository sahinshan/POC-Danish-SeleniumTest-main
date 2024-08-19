using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonHealthDisabilityImpairmentsRecordPage : CommonMethods
    {
        public PersonHealthDisabilityImpairmentsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=persondisabilityimpairments&')]");

        readonly By pageHeader = By.XPath("//h1[@title='Disability/Impairment: New']");      

        
        
        readonly By StartDate_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])");        
        readonly By ProfessionalType_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Headers
        readonly By disabilityimpairmenttype_Field = By.Id("CWField_disabilityimpairmenttypeid");
        readonly By disabilitytype_CWLookupButton = By.Id("CWLookupBtn_disabilitytypeid");
        readonly By primaryorsecondary_Field = By.Id("CWField_primaryorsecondaryid");
        readonly By diagnosisdate_Field = By.Id("CWField_diagnosisdate");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By cvireceiveddate_Field = By.Id("CWField_cvireceiveddate");
        readonly By reviewdate_Field = By.Id("CWField_reviewdate");
        readonly By ownerid_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By registereddisabilitynumber_Field = By.Id("CWField_registereddisabilitynumber");
        readonly By disabilityseverityid_Field = By.Id("CWField_disabilityseverityid");
        readonly By notifieddate_Field = By.Id("CWField_notifieddate");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By onsetdate_Field = By.Id("CWField_onsetdate");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By impairmenttypeid_LookupButton = By.Id("CWLookupBtn_impairmenttypeid");







        #endregion




        public PersonHealthDisabilityImpairmentsRecordPage WaitForPersonHealthDisabilityImpairmentsRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Disability/Impairment:\r\nNew");

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }     

       

        public PersonHealthDisabilityImpairmentsRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }      


       

        public PersonHealthDisabilityImpairmentsRecordPage ValidateStartDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(StartDate_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage ValidateEndDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(StartDate_FieldErrorArea, ExpectedMessage);

            return this;
        }



        public PersonHealthDisabilityImpairmentsRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }      



        public PersonHealthDisabilityImpairmentsRecordPage ValidateNoNotificationErrorMessageVisibile(bool ExpectedText)
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


        public PersonHealthDisabilityImpairmentsRecordPage ValidateTitleFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(ProfessionalType_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage InsertStartDate(String StartDateToInsert)
        {
            SendKeys(StartDate_Field, StartDateToInsert);

            return this;
        }

       
        public PersonHealthDisabilityImpairmentsRecordPage InsertEndDate(string EndDateToInsert)
        {
            SendKeys(EndDate_Field, EndDateToInsert);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage InsertOnSetDate(string OnsetDateToInsert)
        {
            SendKeys(onsetdate_Field, OnsetDateToInsert);

            return this;
        }

       

        public PersonHealthDisabilityImpairmentsRecordPage ClickDisabilityTypeLookupButton()
        {
            Click(disabilitytype_CWLookupButton);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage ClickImpairmentTypeLookupButton()
        {
            Click(impairmenttypeid_LookupButton);

            return this;
        }


        public PersonHealthDisabilityImpairmentsRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ownerid_LookupButton);

            return this;
        }

        public PersonHealthDisabilityImpairmentsRecordPage SelectDisabilityImpairment(String OptionToSelect)
        {
            WaitForElementVisible(disabilityimpairmenttype_Field);
            SelectPicklistElementByText(disabilityimpairmenttype_Field, OptionToSelect);

            return this;
        }












    }
}
