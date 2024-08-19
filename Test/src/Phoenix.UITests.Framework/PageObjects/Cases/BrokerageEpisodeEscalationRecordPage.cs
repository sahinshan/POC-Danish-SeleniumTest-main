using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class BrokerageEpisodeEscalationRecordPage : CommonMethods
    {
        public BrokerageEpisodeEscalationRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisodeescalation')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By BrokerageEpisode_FieldHeader = By.Id("CWLabelHolder_brokerageepisodeid");
        readonly By BrokerageEpisode_LinkField = By.XPath("//*[@id='CWField_brokerageepisodeid_Link']");
        readonly By BrokerageEpisode_LookUpButton = By.Id("CWLookupBtn_brokerageepisodeid");
        readonly By BrokerageEpisode_RemoveButton = By.Id("CWClearLookup_brokerageepisodeid");
        readonly By BrokerageEpisode_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokerageepisodeid']/label/span");

        readonly By EscalatedTo_FieldHeader = By.Id("CWLabelHolder_escalatedtoid");
        readonly By EscalatedTo_LinkField = By.XPath("//*[@id='CWField_escalatedtoid_Link']");
        readonly By EscalatedTo_LookUpButton = By.Id("CWLookupBtn_escalatedtoid");
        readonly By EscalatedTo_RemoveButton = By.Id("CWClearLookup_escalatedtoid");
        readonly By EscalatedTo_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_escalatedtoid']/label/span");

        readonly By EscalationDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_escalationdatetime']/label");
        readonly By EscalationDateTime_DateField = By.Id("CWField_escalationdatetime");
        readonly By EscalationDateTime_TimeField = By.Id("CWField_escalationdatetime_Time");
        readonly By EscalationDateTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_escalationdatetime']/label/span");

        readonly By EscalationDetails_FieldHeader = By.XPath("//*[@id='CWLabelHolder_escalationdetails']/label");
        readonly By EscalationDetails_Field = By.XPath("//*[@id='CWField_escalationdetails']");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By ResolutionDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_resolutiondatetime']/label");
        readonly By ResolutionDateTime_DateField = By.Id("CWField_resolutiondatetime");
        readonly By ResolutionDateTime_TimeField = By.Id("CWField_resolutiondatetime_Time");

        readonly By ResolutionDetails_FieldHeader = By.XPath("//*[@id='CWLabelHolder_resolutiondetails']/label");
        readonly By ResolutionDetails_Field = By.XPath("//*[@id='CWField_resolutiondetails']");
        readonly By ResolutionDetails_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_resolutiondetails']/label/span");

        #endregion


        public BrokerageEpisodeEscalationRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);            
            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }






        public BrokerageEpisodeEscalationRecordPage WaitForBrokerageEpisodeEscalationRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(BrokerageEpisode_FieldHeader);
            WaitForElement(EscalationDateTime_FieldHeader);
            WaitForElement(EscalatedTo_FieldHeader);
            WaitForElement(EscalationDetails_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResolutionDateTime_FieldHeader);
            WaitForElement(ResolutionDetails_FieldHeader);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage WaitForBrokerageEpisodeEscalationRecordPageToLoad(string PageTitle)
        {
            WaitForBrokerageEpisodeEscalationRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Episode: " + PageTitle);

            return this;
        }


       



        public BrokerageEpisodeEscalationRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public BrokerageEpisodeEscalationRecordPage ValidateBrokerageEpisodeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(BrokerageEpisode_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(BrokerageEpisode_FieldErrorLabel, 3);
            }

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateEscalationDateTimeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EscalationDateTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EscalationDateTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResolutionDetailsFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResolutionDetails_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResolutionDetails_FieldErrorLabel, 3);
            }

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateEscalatedToFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EscalatedTo_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EscalatedTo_FieldErrorLabel, 3);
            }

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
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






        public BrokerageEpisodeEscalationRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateBrokerageEpisodeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(BrokerageEpisode_FieldErrorLabel, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateEscalationDateTimeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EscalationDateTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResolutionDetailsFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResolutionDetails_FieldErrorLabel, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateEscalatedToFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EscalatedTo_FieldErrorLabel, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }










        public BrokerageEpisodeEscalationRecordPage InsertEscalationDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(EscalationDateTime_DateField);
            SendKeys(EscalationDateTime_DateField, DateToInsert);
            WaitForElementToBeClickable(EscalationDateTime_TimeField);
            MoveToElementInPage(EscalationDateTime_TimeField);
            Click(EscalationDateTime_TimeField);
            SendKeys(EscalationDateTime_TimeField, TimeToInsert);


            return this;
        }
        public BrokerageEpisodeEscalationRecordPage InsertResolutionDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(ResolutionDateTime_DateField);
            SendKeys(ResolutionDateTime_DateField, DateToInsert);
            WaitForElementToBeClickable(ResolutionDateTime_TimeField);
            MoveToElementInPage(ResolutionDateTime_TimeField);
            Click(ResolutionDateTime_TimeField);
            SendKeys(ResolutionDateTime_TimeField, TimeToInsert);


            return this;
        }
        public BrokerageEpisodeEscalationRecordPage InsertEscalationDetails(string TextToInsert)
        {
            WaitForElement(EscalationDetails_Field);
            SendKeys(EscalationDetails_Field, TextToInsert);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage InsertResolutionDetails(string TextToInsert)
        {
            WaitForElement(ResolutionDetails_Field);
            SendKeys(ResolutionDetails_Field, TextToInsert);

            return this;
        }



        
        public BrokerageEpisodeEscalationRecordPage ClickBrokerageEpisodeLookUpButton()
        {
            WaitForElementToBeClickable(BrokerageEpisode_LookUpButton);
            Click(BrokerageEpisode_LookUpButton);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickBrokerageEpisodeRemoveButton()
        {
            WaitForElementToBeClickable(BrokerageEpisode_RemoveButton);
            Click(BrokerageEpisode_RemoveButton);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickEscalatedToLookUpButton()
        {
            WaitForElementToBeClickable(EscalatedTo_LookUpButton);
            Click(EscalatedTo_LookUpButton);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickEscalatedToRemoveButton()
        {
            WaitForElementToBeClickable(EscalatedTo_RemoveButton);
            Click(EscalatedTo_RemoveButton);

            return this;
        }
        
        public BrokerageEpisodeEscalationRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        






       
        public BrokerageEpisodeEscalationRecordPage ValidateBrokerageEpisodeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(BrokerageEpisode_LinkField, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateEscalatedToLinkFieldText(string ExpectedText)
        {
            ValidateElementText(EscalatedTo_LinkField, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }








        





        public BrokerageEpisodeEscalationRecordPage ValidateEscalationDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(EscalationDateTime_DateField, ExpectedDate);
            ValidateElementValue(EscalationDateTime_TimeField, ExpectedTime);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResolutionDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(ResolutionDateTime_DateField, ExpectedDate);
            ValidateElementValue(ResolutionDateTime_TimeField, ExpectedTime);

            return this;
        }
        





        
        public BrokerageEpisodeEscalationRecordPage ValidateEscalationDetails(string ExpectedText)
        {
            ValidateElementValue(EscalationDetails_Field, ExpectedText);

            return this;
        }
        public BrokerageEpisodeEscalationRecordPage ValidateResolutionDetails(string ExpectedText)
        {
            ValidateElementValue(ResolutionDetails_Field, ExpectedText);

            return this;
        }
       

    }
}
