using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageOfferCommunicationRecordPage : CommonMethods
    {
        public CaseBrokerageOfferCommunicationRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageoffercommunication')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Fields

        readonly By BrokerageOffer_FieldHeader = By.Id("CWLabelHolder_brokerageofferid");
        readonly By BrokerageOffer_LinkField = By.XPath("//*[@id='CWField_brokerageofferid_Link']");
        readonly By BrokerageOffer_LookUpButton = By.Id("CWLookupBtn_brokerageofferid");
        readonly By BrokerageOffer_RemoveButton = By.Id("CWClearLookup_brokerageofferid");
        readonly By BrokerageOffer_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokerageofferid']/label/span");

        readonly By CommunicationDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_communicationdatetime']/label");
        readonly By CommunicationDateTime_DateField = By.Id("CWField_communicationdatetime");
        readonly By CommunicationDateTime_TimeField = By.Id("CWField_communicationdatetime_Time");
        readonly By CommunicationDateTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_communicationdatetime']/div/div/label/span");

        readonly By CommunicationWith_FieldHeader = By.Id("CWLabelHolder_brokeragecommunicationwithid");
        readonly By CommunicationWith_LinkField = By.XPath("//*[@id='CWField_brokeragecommunicationwithid_Link']");
        readonly By CommunicationWith_LookUpButton = By.Id("CWLookupBtn_brokeragecommunicationwithid");
        readonly By CommunicationWith_RemoveButton = By.Id("CWClearLookup_brokeragecommunicationwithid");
        readonly By CommunicationWith_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokeragecommunicationwithid']/label/span");

        readonly By ContactMethod_FieldHeader = By.XPath("//*[@id='CWLabelHolder_contactmethodid']/label");
        readonly By ContactMethod_LinkField = By.XPath("//*[@id='CWField_contactmethodid_Link']");
        readonly By ContactMethod_LookUpButton = By.Id("CWLookupBtn_contactmethodid");
        readonly By ContactMethod_RemoveButton = By.Id("CWClearLookup_contactmethodid");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By Subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label");
        readonly By Subject_Field = By.XPath("//*[@id='CWField_subject']");

        readonly By CommunicationDetails_FieldHeader = By.XPath("//*[@id='CWLabelHolder_communicationdetails']/label");
        readonly By CommunicationDetails_Field = By.XPath("//*[@id='CWField_communicationdetails']");

        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_outcomeid']/label");
        readonly By Outcome_LinkField = By.XPath("//*[@id='CWField_outcomeid_Link']");
        readonly By Outcome_LookUpButton = By.Id("CWLookupBtn_outcomeid");
        readonly By Outcome_RemoveButton = By.Id("CWClearLookup_outcomeid");




        #endregion


        public CaseBrokerageOfferCommunicationRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }








        public CaseBrokerageOfferCommunicationRecordPage WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(CommunicationDateTime_FieldHeader);
            WaitForElement(BrokerageOffer_FieldHeader);
            WaitForElement(BrokerageOffer_FieldHeader);
            WaitForElement(CommunicationWith_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ContactMethod_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(Subject_FieldHeader);
            WaitForElement(CommunicationDetails_FieldHeader);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage WaitForCaseBrokerageOfferCommunicationRecordPageToLoad(string PageTitle)
        {
            WaitForCaseBrokerageOfferCommunicationRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Offer: " + PageTitle);

            return this;
        }

        public CaseBrokerageOfferCommunicationRecordPage WaitForCaseBrokerageOfferCommunicationRecordPageToLoadDisabled(string PageTitle)
        {
            WaitForCaseBrokerageOfferCommunicationRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Offer: " + PageTitle);

            ValidateElementDisabled(BrokerageOffer_LookUpButton);
            ValidateElementDisabled(CommunicationDateTime_DateField);
            ValidateElementDisabled(CommunicationDateTime_TimeField);
            ValidateElementDisabled(ResponsibleTeam_LookUpButton);
            ValidateElementDisabled(CommunicationWith_LookUpButton);
            ValidateElementDisabled(Subject_Field);
            ValidateElementDisabled(CommunicationDetails_Field);
            ValidateElementDisabled(ContactMethod_LookUpButton);

            return this;
        }






        public CaseBrokerageOfferCommunicationRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationDateTimeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CommunicationDateTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(CommunicationDateTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateBrokerageOfferFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(BrokerageOffer_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(BrokerageOffer_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationWithFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CommunicationWith_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(CommunicationWith_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }


        public CaseBrokerageOfferCommunicationRecordPage ValidateContactMethodFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ContactMethod_FieldHeader);
                WaitForElementVisible(ContactMethod_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(ContactMethod_FieldHeader, 3);
                WaitForElementNotVisible(ContactMethod_LinkField, 3);
                WaitForElementNotVisible(ContactMethod_LookUpButton, 3);
                WaitForElementNotVisible(ContactMethod_RemoveButton, 3);
            }

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateOutcomeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Outcome_FieldHeader);
                WaitForElementVisible(Outcome_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(Outcome_FieldHeader, 3);
                WaitForElementNotVisible(Outcome_LinkField, 3);
                WaitForElementNotVisible(Outcome_LookUpButton, 3);
                WaitForElementNotVisible(Outcome_RemoveButton, 3);
            }

            return this;
        }






        public CaseBrokerageOfferCommunicationRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationDateTimeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CommunicationDateTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateBrokerageOfferFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(BrokerageOffer_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationWithFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CommunicationWith_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
       
        
        







        public CaseBrokerageOfferCommunicationRecordPage InsertSubject(string TextToInsert)
        {
            WaitForElement(Subject_Field);
            SendKeys(Subject_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage InsertCommunicationDetails(string TextToInsert)
        {
            WaitForElement(CommunicationDetails_Field);
            SendKeys(CommunicationDetails_Field, TextToInsert);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage InsertCommunicationDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(CommunicationDateTime_DateField);
            SendKeys(CommunicationDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(CommunicationDateTime_DateField, Keys.Tab);
            SendKeys(CommunicationDateTime_TimeField, TimeToInsert);

            return this;
        }
        





        
        public CaseBrokerageOfferCommunicationRecordPage ClickBrokerageOfferLookUpButton()
        {
            WaitForElementToBeClickable(BrokerageOffer_LookUpButton);
            Click(BrokerageOffer_LookUpButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickBrokerageOfferRemoveButton()
        {
            WaitForElementToBeClickable(BrokerageOffer_RemoveButton);
            Click(BrokerageOffer_RemoveButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickCommunicationWithLookUpButton()
        {
            WaitForElementToBeClickable(CommunicationWith_LookUpButton);
            Click(CommunicationWith_LookUpButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickCommunicationWithRemoveButton()
        {
            WaitForElementToBeClickable(CommunicationWith_RemoveButton);
            Click(CommunicationWith_RemoveButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickContactMethodLookUpButton()
        {
            WaitForElementToBeClickable(ContactMethod_LookUpButton);
            Click(ContactMethod_LookUpButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickContactMethodRemoveButton()
        {
            WaitForElementToBeClickable(ContactMethod_RemoveButton);
            Click(ContactMethod_RemoveButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickOutcomeLookUpButton()
        {
            WaitForElementToBeClickable(Outcome_LookUpButton);
            Click(Outcome_LookUpButton);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ClickOutcomeRemoveButton()
        {
            WaitForElementToBeClickable(Outcome_RemoveButton);
            Click(Outcome_RemoveButton);

            return this;
        }
        






        
        public CaseBrokerageOfferCommunicationRecordPage ValidateBrokerageOfferLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(BrokerageOffer_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationWithLinkFieldText(string ExpectedText)
        {
            ValidateElementText(CommunicationWith_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateContactMethodLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ContactMethod_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateOutcomeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        



        public CaseBrokerageOfferCommunicationRecordPage ValidateSubjectValue(string ExpectedValue)
        {
            ValidateElementValue(Subject_Field, ExpectedValue);

            return this;
        }
        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationDetailsText(string ExpectedValue)
        {
            ValidateElementText(CommunicationDetails_Field, ExpectedValue);

            return this;
        }






        public CaseBrokerageOfferCommunicationRecordPage ValidateCommunicationDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(CommunicationDateTime_DateField, ExpectedDate);
            ValidateElementValue(CommunicationDateTime_TimeField, ExpectedTime);

            return this;
        }

        

    }
}
