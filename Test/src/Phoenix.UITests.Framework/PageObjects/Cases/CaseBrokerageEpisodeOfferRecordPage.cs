using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodeOfferRecordPage : CommonMethods
    {
        public CaseBrokerageEpisodeOfferRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageoffer')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By UndoAcceptance = By.Id("TI_UndoAcceptance");

        readonly By undo_AcceptanceButton = By.Id("TI_UndoAcceptance");


        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By AttachmentsLeftMenuLink = By.XPath("//*[@id='CWNavItem_Attachments']");



        #endregion

        #region Fields

        readonly By BrokerageEpisode_FieldHeader = By.Id("CWLabelHolder_brokerageepisodeid");
        readonly By BrokerageEpisode_LinkField = By.XPath("//*[@id='CWField_brokerageepisodeid_Link']");
        readonly By BrokerageEpisode_LookUpButton = By.Id("CWLookupBtn_brokerageepisodeid");
        readonly By BrokerageEpisode_RemoveButton = By.Id("CWClearLookup_brokerageepisodeid");
        readonly By BrokerageEpisode_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_brokerageepisodeid']/label/span");

        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        readonly By Status_Field = By.Id("CWField_statusid");
        readonly By Status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergicreactionStatusid']/label/span");

        readonly By ReceivedDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_receiveddatetime']/label");
        readonly By ReceivedDateTime_DateField = By.Id("CWField_receiveddatetime");
        readonly By ReceivedDateTime_TimeField = By.Id("CWField_receiveddatetime_Time");
        readonly By ReceivedDateTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_receiveddatetime']/div/div/label/span");

        readonly By SourcedDateTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_sourceddatetime']/label");
        readonly By SourcedDateTime_DateField = By.Id("CWField_sourceddatetime");
        readonly By SourcedDateTime_TimeField = By.Id("CWField_sourceddatetime_Time");
        readonly By SourcedDateTime_Mandatory = By.XPath("//*[@id='CWLabelHolder_sourceddatetime']/label/span[@class = 'mandatory']");
        readonly By SourcedDateTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_sourceddatetime']/label/span");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By ProviderRegisteredInCareDirector_FieldHeader = By.XPath("//*[@id='CWLabelHolder_providerregisteredincaredirector']/label");
        readonly By ProviderRegisteredInCareDirector_YesRadioButton = By.XPath("//*[@id='CWField_providerregisteredincaredirector_1']");
        readonly By ProviderRegisteredInCareDirector_NoRadioButton = By.XPath("//*[@id='CWField_providerregisteredincaredirector_0']");

        readonly By Provider_FieldHeader = By.Id("CWLabelHolder_providerid");
        readonly By Provider_LinkField = By.XPath("//*[@id='CWField_providerid_Link']");
        readonly By Provider_LookUpButton = By.Id("CWLookupBtn_providerid");
        readonly By Provider_RemoveButton = By.Id("CWClearLookup_providerid");
        readonly By Provider_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_providerid']/label/span");

        readonly By ExternalProvider_FieldHeader = By.Id("CWLabelHolder_externalprovider");
        readonly By ExternalProvider_Field = By.XPath("//*[@id='CWField_externalprovider']");
        readonly By ExternalProvider_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_externalprovider']/label/span");

        readonly By CostPerWeek_FieldHeader = By.XPath("//*[@id='CWLabelHolder_costperweek']/label");
        readonly By CostPerWeek_Field = By.XPath("//*[@id='CWField_costperweek']");
        readonly By CostPerWeek_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_costperweek']/div/label/span");

        readonly By RateUnit_FieldHeader = By.XPath("//*[@id='CWLabelHolder_rateunitid']/label");
        readonly By RateUnit_LinkField = By.XPath("//*[@id='CWField_rateunitid_Link']");
        readonly By RateUnit_Mandatory = By.XPath("//*[@id='CWLabelHolder_rateunitid']/label/span[@class = 'mandatory']");
        readonly By RateUnit_LookUpButton = By.Id("CWLookupBtn_rateunitid");
        readonly By RateUnit_RemoveButton = By.Id("CWClearLookup_rateunitid");

        readonly By ServiceProvided_FieldHeader = By.XPath("//*[@id='CWLabelHolder_serviceprovidedid']/label");
        readonly By ServiceProvided_LinkField = By.XPath("//*[@id='CWField_serviceprovidedid_Link']");
        readonly By ServiceProvided_MandatoryField = By.XPath("//*[@id='CWLabelHolder_serviceprovidedid']/label/span[@class ='mandatory']");
        readonly By ServiceProvided_LookUpButton = By.Id("CWLookupBtn_serviceprovidedid");
        readonly By ServiceProvided_RemoveButton = By.Id("CWClearLookup_serviceprovidedid");

        readonly By RejectionReason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageofferrejectionreasonid']/label");
        readonly By RejectionReason_LinkField = By.XPath("//*[@id='CWField_brokerageofferrejectionreasonid_Link']");
        readonly By RejectionReason_LookUpButton = By.Id("CWLookupBtn_brokerageofferrejectionreasonid");
        readonly By RejectionReason_RemoveButton = By.Id("CWClearLookup_brokerageofferrejectionreasonid");

        readonly By CancellationReason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_brokerageoffercancellationreasonid']/label");
        readonly By CancellationReason_LinkField = By.XPath("//*[@id='CWField_brokerageoffercancellationreasonid_Link']");
        readonly By CancellationReason_LookUpButton = By.Id("CWLookupBtn_brokerageoffercancellationreasonid");
        readonly By CancellationReason_RemoveButton = By.Id("CWClearLookup_brokerageoffercancellationreasonid");

        readonly By AwaitingCommunicationFrom_FieldHeader = By.XPath("//*[@id='CWLabelHolder_awaitingcommunicationfromid']/label");
        readonly By AwaitingCommunicationFrom_LookUpButton = By.Id("CWLookupBtn_awaitingcommunicationfromid");
        By AwaitingCommunicationFrom_OptionText(string OptionId) => By.XPath("//*[@id='MS_awaitingcommunicationfromid_" + OptionId + "']");
        By AwaitingCommunicationFrom_OptionRemoveButton(string OptionId) => By.XPath("//*[@id='MS_awaitingcommunicationfromid_" + OptionId + "']/a");



        #endregion


        public CaseBrokerageEpisodeOfferRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ClickUndoAccptanceButton()
        {
            WaitForElement(undo_AcceptanceButton);
            Click(undo_AcceptanceButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);            

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ClickUndoAcceptanceButton()
        {

            WaitForElementVisible(UndoAcceptance);
            Click(UndoAcceptance);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage NavigateToAttachmentsSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementVisible(AttachmentsLeftMenuLink);
            Click(AttachmentsLeftMenuLink);

            return this;
        }




        public CaseBrokerageEpisodeOfferRecordPage WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 25);

            WaitForElement(pageHeader);

            WaitForElement(Status_FieldHeader);
            WaitForElement(ReceivedDateTime_FieldHeader);
            WaitForElement(BrokerageEpisode_FieldHeader);
            WaitForElement(BrokerageEpisode_FieldHeader);
            WaitForElement(Provider_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(RateUnit_FieldHeader);
            WaitForElement(ServiceProvided_FieldHeader);
            WaitForElement(SourcedDateTime_FieldHeader);
            WaitForElement(RejectionReason_FieldHeader);

            WaitForElement(CancellationReason_FieldHeader);

            WaitForElement(CostPerWeek_FieldHeader);
            WaitForElement(AwaitingCommunicationFrom_FieldHeader);
            WaitForElement(ProviderRegisteredInCareDirector_FieldHeader);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage WaitForCaseBrokerageEpisodeOfferRecordPageToLoad(string PageTitle)
        {
            WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Offer:\r\n" + PageTitle);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled(string PageTitle)
        {
            WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Offer:\r\n" + PageTitle);

            ValidateElementDisabled(BrokerageEpisode_LookUpButton);
            ValidateElementDisabled(Status_Field);
            ValidateElementDisabled(ReceivedDateTime_DateField);
            ValidateElementDisabled(ReceivedDateTime_TimeField);
            ValidateElementDisabled(SourcedDateTime_DateField);
            ValidateElementDisabled(SourcedDateTime_TimeField);
            ValidateElementDisabled(ResponsibleTeam_LookUpButton);
            ValidateElementDisabled(ProviderRegisteredInCareDirector_YesRadioButton);
            ValidateElementDisabled(ProviderRegisteredInCareDirector_NoRadioButton);
            ValidateElementDisabled(Provider_LookUpButton);
            ValidateElementDisabled(CostPerWeek_Field);
            ValidateElementDisabled(RateUnit_LookUpButton);
            
            ValidateElementDisabled(AwaitingCommunicationFrom_LookUpButton);

            return this;
        }


        


        public CaseBrokerageEpisodeOfferRecordPage ValidateSourcedDateTimeFieldMandatoryVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SourcedDateTime_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(SourcedDateTime_Mandatory, 3);
            }

            return this;
        }


        public CaseBrokerageEpisodeOfferRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public CaseBrokerageEpisodeOfferRecordPage ValidateStatusFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Status_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Status_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateReceivedDateTimeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReceivedDateTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ReceivedDateTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateBrokerageEpisodeFieldErrorLabelVisibility(bool ExpectVisible)
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
        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Provider_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Provider_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateExternalProviderFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ExternalProvider_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ExternalProvider_FieldErrorLabel, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
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
        public CaseBrokerageEpisodeOfferRecordPage ValidateSourcedDateTimeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SourcedDateTime_FieldHeader);
                WaitForElementVisible(SourcedDateTime_DateField);
                WaitForElementVisible(SourcedDateTime_TimeField);
            }
            else
            {
                WaitForElementNotVisible(SourcedDateTime_FieldHeader, 3);
                WaitForElementNotVisible(SourcedDateTime_DateField, 3);
                WaitForElementNotVisible(SourcedDateTime_TimeField, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateRateUnitFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RateUnit_FieldHeader);
                WaitForElementVisible(RateUnit_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(RateUnit_FieldHeader, 3);
                WaitForElementNotVisible(RateUnit_LinkField, 3);
                WaitForElementNotVisible(RateUnit_LookUpButton, 3);
                WaitForElementNotVisible(RateUnit_RemoveButton, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateServiceProvidedFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceProvided_FieldHeader);
                WaitForElementVisible(ServiceProvided_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(ServiceProvided_FieldHeader, 3);
                WaitForElementNotVisible(ServiceProvided_LinkField, 3);
                WaitForElementNotVisible(ServiceProvided_LookUpButton, 3);
                WaitForElementNotVisible(ServiceProvided_RemoveButton, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateCostPerWeekFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CostPerWeek_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(CostPerWeek_FieldErrorLabel, 3);
            }

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateExternalProviderFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ExternalProvider_FieldHeader);
                WaitForElementVisible(ExternalProvider_Field);
            }
            else
            {
                WaitForElementNotVisible(ExternalProvider_FieldHeader, 3);
                WaitForElementNotVisible(ExternalProvider_Field, 3);
            }

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Provider_FieldHeader);
                WaitForElementVisible(Provider_LinkField);
                WaitForElementVisible(Provider_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(Provider_FieldHeader, 3);
                WaitForElementNotVisible(Provider_LinkField, 3);
                WaitForElementNotVisible(Provider_LookUpButton, 3);
            }

            return this;
        }






        public CaseBrokerageEpisodeOfferRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateStatusFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateReceivedDateTimeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ReceivedDateTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateBrokerageEpisodeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(BrokerageEpisode_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Provider_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateExternalProviderFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ExternalProvider_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateCostPerWeekFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CostPerWeek_FieldErrorLabel, ExpectedText);

            return this;
        }







        public CaseBrokerageEpisodeOfferRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElement(Status_Field);
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }



        public CaseBrokerageEpisodeOfferRecordPage INsertCostPerWeek(string TextToInsert)
        {
            WaitForElement(CostPerWeek_Field);
            SendKeys(CostPerWeek_Field, TextToInsert);
            SendKeysWithoutClearing(CostPerWeek_Field, Keys.Tab);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage InsertReceivedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(ReceivedDateTime_DateField);
            SendKeys(ReceivedDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(ReceivedDateTime_DateField, Keys.Tab);
            SendKeys(ReceivedDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage InsertSourcedDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(SourcedDateTime_DateField);
            SendKeys(SourcedDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(SourcedDateTime_DateField, Keys.Tab);
            SendKeys(SourcedDateTime_TimeField, TimeToInsert);


            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage InsertExternalProvider(string TextToInsert)
        {
            WaitForElement(ExternalProvider_Field);
            SendKeys(ExternalProvider_Field, TextToInsert);
            SendKeysWithoutClearing(ExternalProvider_Field, Keys.Tab);

            return this;
        }







        public CaseBrokerageEpisodeOfferRecordPage ClickBrokerageEpisodeLookUpButton()
        {
            WaitForElementToBeClickable(BrokerageEpisode_LookUpButton);
            Click(BrokerageEpisode_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickBrokerageEpisodeRemoveButton()
        {
            WaitForElementToBeClickable(BrokerageEpisode_RemoveButton);
            Click(BrokerageEpisode_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickProviderLookUpButton()
        {
            WaitForElementToBeClickable(Provider_LookUpButton);
            Click(Provider_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickProviderRemoveButton()
        {
            WaitForElementToBeClickable(Provider_RemoveButton);
            Click(Provider_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickRateUnitLookUpButton()
        {
            WaitForElementToBeClickable(RateUnit_LookUpButton);
            Click(RateUnit_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickRateUnitRemoveButton()
        {
            WaitForElementToBeClickable(RateUnit_RemoveButton);
            Click(RateUnit_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickServiceProvidedLookUpButton()
        {
            WaitForElementToBeClickable(ServiceProvided_LookUpButton);
            Click(ServiceProvided_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickServiceProvidedRemoveButton()
        {
            WaitForElementToBeClickable(ServiceProvided_RemoveButton);
            Click(ServiceProvided_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickRejectionReasonLookUpButton()
        {
            WaitForElementToBeClickable(RejectionReason_LookUpButton);
            Click(RejectionReason_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickRejectionReasonRemoveButton()
        {
            WaitForElementToBeClickable(RejectionReason_RemoveButton);
            Click(RejectionReason_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickCancellationReasonLookUpButton()
        {
            WaitForElementToBeClickable(CancellationReason_LookUpButton);
            Click(CancellationReason_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickCancellationReasonRemoveButton()
        {
            WaitForElementToBeClickable(CancellationReason_RemoveButton);
            Click(CancellationReason_RemoveButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickAwaitingCommunicationFromLookUpButton()
        {
            WaitForElementToBeClickable(AwaitingCommunicationFrom_LookUpButton);
            Click(AwaitingCommunicationFrom_LookUpButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickAwaitingCommunicationFromOptionRemoveButton(string OptionId)
        {
            WaitForElementToBeClickable(AwaitingCommunicationFrom_OptionRemoveButton(OptionId));
            Click(AwaitingCommunicationFrom_OptionRemoveButton(OptionId));

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickProviderRegisteredInCareDirectorYesRadioButton()
        {
            WaitForElementToBeClickable(ProviderRegisteredInCareDirector_YesRadioButton);
            Click(ProviderRegisteredInCareDirector_YesRadioButton);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ClickProviderRegisteredInCareDirectorNoRadioButton()
        {
            WaitForElementToBeClickable(ProviderRegisteredInCareDirector_NoRadioButton);
            Click(ProviderRegisteredInCareDirector_NoRadioButton);

            return this;
        }







        public CaseBrokerageEpisodeOfferRecordPage ValidateBrokerageEpisodeLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(BrokerageEpisode_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Provider_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateRateUnitLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RateUnit_LinkField, ExpectedText);

            return this;
        }


        public CaseBrokerageEpisodeOfferRecordPage ValidateRateUnitTextVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RateUnit_LinkField);
                
            }
            else
            {
                WaitForElementNotVisible(RateUnit_LinkField, 3);
                
            }

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateServiceProvidedLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ServiceProvided_LinkField, ExpectedText);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateServiceProvidedTextVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceProvided_LinkField);

            }
            else
            {
                WaitForElementNotVisible(ServiceProvided_LinkField, 3);

            }

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateRejectionReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RejectionReason_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateCancellationReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(CancellationReason_LinkField, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateAwaitingCommunicationFromOptionText(string OptionId, string ExpectedText)
        {
            ValidateElementText(AwaitingCommunicationFrom_OptionText(OptionId), ExpectedText);

            return this;
        }




        public CaseBrokerageEpisodeOfferRecordPage ValidateCostPerWeekValue(string ExpectedValue)
        {
            ValidateElementValue(CostPerWeek_Field, ExpectedValue);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateExternalProviderValue(string ExpectedValue)
        {
            ValidateElementValue(ExternalProvider_Field, ExpectedValue);

            return this;
        }






        public CaseBrokerageEpisodeOfferRecordPage ValidateStatusOptionDisabled(string OptionText, bool ExpectDisabled)
        {
            ValidatePicklistOptionDisabled(Status_Field, OptionText, ExpectDisabled);

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderRegisteredInCareDirectorOptionsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled) 
            { 
                ValidateElementDisabled(ProviderRegisteredInCareDirector_YesRadioButton);
                ValidateElementDisabled(ProviderRegisteredInCareDirector_NoRadioButton);
            }
            else
            {
                ValidateElementNotDisabled(ProviderRegisteredInCareDirector_YesRadioButton);
                ValidateElementNotDisabled(ProviderRegisteredInCareDirector_NoRadioButton);
            }

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderLookupButtondDisabled(bool ExpectDisabled)
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
        public CaseBrokerageEpisodeOfferRecordPage ValidateExternalProviderFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(ExternalProvider_Field);
            }
            else
            {
                ValidateElementNotDisabled(ExternalProvider_Field);
            }

            return this;
        }





        public CaseBrokerageEpisodeOfferRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }


        





        public CaseBrokerageEpisodeOfferRecordPage ValidateReceivedDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(ReceivedDateTime_DateField, ExpectedDate);
            ValidateElementValue(ReceivedDateTime_TimeField, ExpectedTime);

            return this;
        }
        public CaseBrokerageEpisodeOfferRecordPage ValidateSourcedDateTime(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(SourcedDateTime_DateField, ExpectedDate);
            ValidateElementValue(SourcedDateTime_TimeField, ExpectedTime);

            return this;
        }


        public CaseBrokerageEpisodeOfferRecordPage ValidateRateUnitMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RateUnit_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(RateUnit_Mandatory, 3);
            }

            return this;
        }


        public CaseBrokerageEpisodeOfferRecordPage ValidateServiceProvidedMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceProvided_MandatoryField);
            }
            else
            {
                WaitForElementNotVisible(ServiceProvided_MandatoryField, 3);
            }

            return this;
        }








        public CaseBrokerageEpisodeOfferRecordPage ValidateProviderRegisteredInCareDirector(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ProviderRegisteredInCareDirector_YesRadioButton);
                ValidateElementNotChecked(ProviderRegisteredInCareDirector_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ProviderRegisteredInCareDirector_YesRadioButton);
                ValidateElementChecked(ProviderRegisteredInCareDirector_NoRadioButton);
            }

            return this;
        }

        public CaseBrokerageEpisodeOfferRecordPage ValidateServiceProvidedLookUp(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ServiceProvided_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(ServiceProvided_LookUpButton, 3);
            }

            return this;
        }


        public CaseBrokerageEpisodeOfferRecordPage InsertCostPerWeek(string TextToInsert)
        {
            WaitForElement(CostPerWeek_Field);
            SendKeys(CostPerWeek_Field, TextToInsert);

            return this;
        }


    }
}
