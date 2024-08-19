using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonHealthImmunisationRecordPage : CommonMethods
    {
        public PersonHealthImmunisationRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personimmunisation&')]");

        readonly By pageHeader = By.XPath("//h1[@title='Person Immunisation: New']");      

        
        
        
        readonly By ImmunisationType_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By PersonResponse_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[2]");
        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Headers
        readonly By immunisationtypeid_CWLookupButton = By.Id("CWLookupBtn_immunisationtypeid");
        readonly By personid_CWLookupButton = By.Id("CWLookupBtn_personid");
        readonly By personresponseid_Field = By.Id("CWField_personresponseid");
        readonly By dategiven_Field = By.Id("CWField_dategiven");
        readonly By relateddate_Field = By.Id("CWField_relateddate");
        readonly By uptodateon_Field = By.Id("CWField_uptodateon");
        readonly By agegivenyears_Field = By.Id("CWField_agegivenyears");
        readonly By agegivenmonths_Field = By.Id("CWField_agegivenmonths");
        readonly By immunisationsuptodateid_Field = By.Id("CWField_immunisationsuptodateid");
        readonly By responsibleuserid_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By ownerid_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        
       
        #endregion




        public PersonHealthImmunisationRecordPage WaitForPersonHealthImmunisationRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Immunisation:\r\nNew");

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }     

       

        public PersonHealthImmunisationRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonHealthImmunisationRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonHealthImmunisationRecordPage ValidateImmunisationFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(ImmunisationType_FieldErrorArea, ExpectedMessage);

            return this;
        }


        public PersonHealthImmunisationRecordPage ValidatePersonResponseFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(PersonResponse_FieldErrorArea, ExpectedMessage);

            return this;
        }

      

        public PersonHealthImmunisationRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }      



        public PersonHealthImmunisationRecordPage ValidateNoNotificationErrorMessageVisibile(bool ExpectedText)
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
               

        public PersonHealthImmunisationRecordPage InsertGivenDate(String GivenDateToInsert)
        {
            SendKeys(dategiven_Field, GivenDateToInsert);

            return this;
        }

       
        public PersonHealthImmunisationRecordPage InsertRelatedDate(string RelatedDateToInsert)
        {
            SendKeys(relateddate_Field, RelatedDateToInsert);

            return this;
        }

        public PersonHealthImmunisationRecordPage InsertUpToDateOn(string UpToDateOnToInsert)
        {
            SendKeys(uptodateon_Field, UpToDateOnToInsert);

            return this;
        }


        public PersonHealthImmunisationRecordPage ClickImmunisationTypeIdLookupButton()
        {
            Click(immunisationtypeid_CWLookupButton);

            return this;
        }

        public PersonHealthImmunisationRecordPage ClickResponsibleUserIdLookupButton()
        {
            Click(responsibleuserid_LookupButton);

            return this;
        }

        public PersonHealthImmunisationRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ownerid_LookupButton);

            return this;
        }

        public PersonHealthImmunisationRecordPage ClickPersonIdLookupButton()
        {
            Click(personid_CWLookupButton);

            return this;
        }


        public PersonHealthImmunisationRecordPage SelectPersonResponseId(String OptionToSelect)
        {
            WaitForElementVisible(personresponseid_Field);
            SelectPicklistElementByText(personresponseid_Field, OptionToSelect);

            return this;
        }

        public PersonHealthImmunisationRecordPage SelectImmunisationUptoDate(String OptionToSelect)
        {
            WaitForElementVisible(immunisationsuptodateid_Field);
            SelectPicklistElementByText(immunisationsuptodateid_Field, OptionToSelect);

            return this;
        }












    }
}
