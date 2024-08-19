using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonHealthProfessionalsRecordPage : CommonMethods
    {
        public PersonHealthProfessionalsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personhealthprofessional&')]");

        readonly By pageHeader = By.XPath("//h1[@title='Person Health Professional: New']");      

        
        
        readonly By StartDate_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])");        
        readonly By ProfessionalType_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");       
       
        #region Headers

        readonly By ProfessionType_LookupButton = By.Id("CWLookupBtn_professiontypeid");
        readonly By Providerid_LookupButton = By.Id("CWLookupBtn_providerid");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By ProfessionalUser_LookupButton = By.Id("CWLookupBtn_professionaluserid");
        readonly By Professional_LookupButton = By.Id("CWLookupBtn_professionalid");
        readonly By FreetextName_Field = By.Id("CWField_freetextname");
        readonly By Phone_Field = By.Id("CWField_phone");
        readonly By Notes_Field = By.Id("CWField_notes");       
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By SaveButton = By.Id("TI_SaveButton");

        #endregion




        public PersonHealthProfessionalsRecordPage WaitForPersonHealthProfessionalsRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Health Professional:\r\nNew");

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }     

       

       

       

        public PersonHealthProfessionalsRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }      


       

        public PersonHealthProfessionalsRecordPage ValidateStartDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(StartDate_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ValidateEndDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(StartDate_FieldErrorArea, ExpectedMessage);

            return this;
        }



        public PersonHealthProfessionalsRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }      



        public PersonHealthProfessionalsRecordPage ValidateNoNotificationErrorMessageVisibile(bool ExpectedText)
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


        public PersonHealthProfessionalsRecordPage ValidateTitleFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(ProfessionalType_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public PersonHealthProfessionalsRecordPage InsertStartDate(String StartDateToInsert)
        {
            SendKeys(StartDate_Field, StartDateToInsert);

            return this;
        }

       
        public PersonHealthProfessionalsRecordPage InsertEndDate(string EndDateToInsert)
        {
            SendKeys(EndDate_Field, EndDateToInsert);

            return this;
        }

        public PersonHealthProfessionalsRecordPage InsertFreeTextName(string FreeTextNameToInsert)
        {
            SendKeys(FreetextName_Field, FreeTextNameToInsert);

            return this;
        }

        public PersonHealthProfessionalsRecordPage InsertPhoneNo(string PhoneNoToInsert)
        {
            SendKeys(Phone_Field, PhoneNoToInsert);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickProfessionaTypeLookupButton()
        {
            Click(ProfessionType_LookupButton);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickProvideridLookupButton()
        {
            Click(Providerid_LookupButton);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickProfessionalUserLookupButton()
        {
            Click(ProfessionalUser_LookupButton);

            return this;
        }

        public PersonHealthProfessionalsRecordPage ClickProfessionalLookupButton()
        {
            Click(Professional_LookupButton);

            return this;
        }










    }
}
