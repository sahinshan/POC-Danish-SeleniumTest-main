using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class SystemUserLanguageRecordPage : CommonMethods
    {
        public SystemUserLanguageRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuserlanguage')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
     

        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By SystemUser_FieldHeader = By.Id("CWLabelHolder_systemuserid");
        readonly By SystemUser_LinkField = By.XPath("//*[@id='CWField_systemuserid_Link']");
        readonly By SystemUser_LookUpButton = By.Id("CWLookupBtn_systemuserid");
        readonly By SystemUser_RemoveButton = By.Id("CWClearLookup_systemuserid");
        readonly By SystemUser_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_systemuserid']/label/span");

        readonly By Language_FieldHeader = By.Id("CWLabelHolder_languageid");
        readonly By Language_LinkField = By.XPath("//*[@id='CWField_languageid_Link']");
        readonly By Language_LookUpButton = By.Id("CWLookupBtn_languageid");
        readonly By Language_RemoveButton = By.Id("CWClearLookup_languageid");
        readonly By Language_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_languageid']/label/span");

        readonly By StartDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_DateField = By.Id("CWField_startdate");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");

        readonly By Fluency_FieldHeader = By.XPath("//*[@id='CWLabelHolder_fluencyid']/label");
        readonly By Fluency_LinkField = By.XPath("//*[@id='CWField_fluencyid_Link']");
        readonly By Fluency_Field = By.Id("CWField_fluencyid");
        readonly By Fluency_LookUpButton = By.Id("CWLookupBtn_fluencyid");
        readonly By Fluency_RemoveButton = By.Id("CWClearLookup_fluencyid");


        #endregion


        public SystemUserLanguageRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public SystemUserLanguageRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public SystemUserLanguageRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public SystemUserLanguageRecordPage ClickAdditionalItemsButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            return this;
        }
        public SystemUserLanguageRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }





        public SystemUserLanguageRecordPage WaitForSystemUserLanguageRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(SystemUser_FieldHeader);
            WaitForElement(Language_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(Fluency_FieldHeader);

            return this;
        }

        public SystemUserLanguageRecordPage WaitForSystemUserLanguageRecordPageToLoad(string PageTitle)
        {
            WaitForSystemUserLanguageRecordPageToLoad();

            WaitForElementToContainText(pageHeader, "System User Language:\r\n" + PageTitle);

            return this;
        }



        public SystemUserLanguageRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public SystemUserLanguageRecordPage ValidateSystemUserFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SystemUser_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(SystemUser_FieldErrorLabel, 3);
            }

            return this;
        }
        public SystemUserLanguageRecordPage ValidateLanguageFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Language_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Language_FieldErrorLabel, 3);
            }

            return this;
        }
        public SystemUserLanguageRecordPage ValidateStartDateFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }


        public SystemUserLanguageRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public SystemUserLanguageRecordPage ValidateLanguageFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Language_FieldErrorLabel, ExpectedText);

            return this;
        }
        public SystemUserLanguageRecordPage ValidateStartDateFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public SystemUserLanguageRecordPage ValidateSystemUserFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(SystemUser_FieldErrorLabel, ExpectedText);

            return this;
        }




        public SystemUserLanguageRecordPage InsertStartDate(string DateToInsert)
        {
            WaitForElement(StartDate_DateField);
            SendKeys(StartDate_DateField, DateToInsert);


            return this;
        }



        public SystemUserLanguageRecordPage ClickSystemUserLookUpButton()
        {
            WaitForElementToBeClickable(SystemUser_LookUpButton);
            Click(SystemUser_LookUpButton);

            return this;
        }
        public SystemUserLanguageRecordPage ClickSystemUserRemoveButton()
        {
            WaitForElementToBeClickable(SystemUser_RemoveButton);
            Click(SystemUser_RemoveButton);

            return this;
        }
        public SystemUserLanguageRecordPage ClickLanguageLookUpButton()
        {
            WaitForElementToBeClickable(Language_LookUpButton);
            Click(Language_LookUpButton);

            return this;
        }
        public SystemUserLanguageRecordPage ClickLanguageRemoveButton()
        {
            WaitForElementToBeClickable(Language_RemoveButton);
            Click(Language_RemoveButton);

            return this;
        }


        public SystemUserLanguageRecordPage ClickFluencyLookUpButton()
        {
            WaitForElementToBeClickable(Fluency_LookUpButton);
            Click(Fluency_LookUpButton);

            return this;
        }
        public SystemUserLanguageRecordPage ClickFluencyRemoveButton()
        {
            WaitForElementToBeClickable(Fluency_RemoveButton);
            Click(Fluency_RemoveButton);

            return this;
        }





        public SystemUserLanguageRecordPage ValidateSystemUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(SystemUser_LinkField, ExpectedText);

            return this;
        }
        public SystemUserLanguageRecordPage ValidateLanguageLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Language_LinkField, ExpectedText);

            return this;
        }
        public SystemUserLanguageRecordPage ValidateFluencyLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Fluency_LinkField, ExpectedText);

            return this;
        }

       
        public SystemUserLanguageRecordPage ValidateStartDate(string ExpectedDate)
        {
            ValidateElementValue(StartDate_DateField, ExpectedDate);

            return this;
        }





        public SystemUserLanguageRecordPage ValidateSystemUserLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(SystemUser_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(SystemUser_LookUpButton);
            }

            return this;
        }

        public SystemUserLanguageRecordPage ValidateLanguageLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Language_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Language_LookUpButton);
            }

            return this;
        }

        public SystemUserLanguageRecordPage ValidateStartDateFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(StartDate_DateField);
            }
            else
            {
                ValidateElementNotDisabled(StartDate_DateField);
            }

            return this;
        }

        public SystemUserLanguageRecordPage ValidateFluencyLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Fluency_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Fluency_LookUpButton);
            }

            return this;
        }



    }
}
