
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserAliasesRecordPage : CommonMethods
    {
        public SystemUserAliasesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseralias&')]");

        #endregion IFrame



        #region Fields

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Alias: ']");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");


        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_LoopUpButton = By.Id("CWLookupBtn_systemuserid");

        readonly By aliasType_Field = By.Id("CWField_systemuseraliastypeid_cwname");
        readonly By aliasType_LookUpButton = By.Id("CWLookupBtn_systemuseraliastypeid");
        readonly By firstName_Field = By.Id("CWField_firstname");
        readonly By middleName_Field = By.Id("CWField_middlename");
        readonly By lastName_Field = By.Id("CWField_lastname");

        readonly By preferredName_YesRadioButton = By.Id("CWField_preferredname_1");
        readonly By preferredName_NoRadioButton = By.Id("CWField_preferredname_0");


        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion

        #region MandatoryFields

        readonly By systemUser_MandatoryField = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label/span[@class ='mandatory']");
        readonly By preferredName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_preferredname']/label/span[@class ='mandatory']");
        readonly By lastName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_lastname']/label/span[@class ='mandatory']");

        #endregion MandatoryFields
        

        #region ErrorMessages

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By lastName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_lastname']/label/span");

        #endregion ErrorMessages


        public SystemUserAliasesRecordPage WaitForSystemUserAliasesRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
           
            return this;
        }        

       

        public SystemUserAliasesRecordPage ValidateSystemUserAliasesRecordPageTitle(string PageTitle)
        {


            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }

        public SystemUserAliasesRecordPage WaitForSystemUserAliasesRecordPageTitleToLoad(string PageTitle)
        {
            WaitForSystemUserAliasesRecordPageToLoad();

            WaitForElementToContainText(pageHeader, "System User Alias:  " + PageTitle);

            return this;
        }

      

        public SystemUserAliasesRecordPage ValidateToolTipTextForSystemUser(string ExpectedText)
        {
            

            ValidateElementToolTip(systemUser_Field, ExpectedText);
           
            return this;
        }

      
        public SystemUserAliasesRecordPage ValidateSystemUser_LinkField(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ValidateElementTextContainsText(systemUser_LinkField, ExpectedText);

            return this;
        }

        public SystemUserAliasesRecordPage ValidateSystemUser_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElementToBeClickable(systemUser_LinkField);
                ValidateElementEnabled(systemUser_Field);

            }
            else
            {
                WaitForElement(systemUser_LinkField, 5);
                
            }
            return this;
        }

        public SystemUserAliasesRecordPage ValidateFirstName_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(firstName_Field);
                ValidateElementEnabled(firstName_Field);

            }
            else
            {
                WaitForElement(firstName_Field, 5);

            }
            return this;
        }

        public SystemUserAliasesRecordPage ValidateMiddleName_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(middleName_Field);
                ValidateElementEnabled(middleName_Field);

            }
            else
            {
                WaitForElement(middleName_Field, 5);

            }
            return this;
        }


        public SystemUserAliasesRecordPage ValidateLastName_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(lastName_Field);
                ValidateElementEnabled(lastName_Field);

            }
            else
            {
                WaitForElement(lastName_Field, 5);

            }
            return this;
        }

        public SystemUserAliasesRecordPage ValidateAliasType_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(aliasType_Field);
                ValidateElementEnabled(aliasType_Field);

            }
            else
            {
                WaitForElement(aliasType_Field, 5);

            }
            return this;
        }

        public SystemUserAliasesRecordPage ValidatePreferredYesOption_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(preferredName_YesRadioButton);
                ValidateElementEnabled(preferredName_YesRadioButton);

            }
            else
            {
                WaitForElement(preferredName_YesRadioButton, 5);

            }
            return this;
        }

        public SystemUserAliasesRecordPage ValidatePreferredNoOption_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElement(preferredName_NoRadioButton);
                ValidateElementEnabled(preferredName_NoRadioButton);

            }
            else
            {
                WaitForElement(preferredName_NoRadioButton, 5);

            }
            return this;
        }

        public SystemUserAliasesRecordPage ClickSystemUserLoopUpButton()
        {
            WaitForElement(systemUser_LoopUpButton);
            Click(systemUser_LoopUpButton);

            return this;
        }

        public SystemUserAliasesRecordPage ClickPreferredName_YesRadioButton()
        {
            WaitForElement(preferredName_YesRadioButton);
            Click(preferredName_YesRadioButton);

            return this;
        }



        public SystemUserAliasesRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserAliasesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

        public SystemUserAliasesRecordPage ValidateAvailableFields()
        {
           
            ValidateElementEnabled(systemUser_Field);
            ValidateElementEnabled(aliasType_Field);
            ValidateElementEnabled(firstName_Field);
            ValidateElementEnabled(middleName_Field);
            ValidateElementEnabled(lastName_Field);
            ValidateElementEnabled(preferredName_YesRadioButton);
            ValidateElementEnabled(preferredName_NoRadioButton);

            return this;
        }


        public SystemUserAliasesRecordPage ValidateFirstNameFieldValue(string ExpectedText)
        {

           
            ValidateElementEnabled(firstName_Field);
            ValidateElementTextContainsText(firstName_Field, ExpectedText);

            return this;
        }


        public SystemUserAliasesRecordPage ValidateFirstNameText(string ExpectedText)
        {


            WaitForElement(firstName_Field);
            ValidateElementValue(firstName_Field,ExpectedText);

            return this;
        }


        public SystemUserAliasesRecordPage ValidateMiddleNameFieldValue(string ExpectedText)
        {


            ValidateElementEnabled(middleName_Field);
            ValidateElementTextContainsText(middleName_Field, ExpectedText);

            return this;
        }


        public SystemUserAliasesRecordPage ValidateSystemUserMandatoryField()
        {


            WaitForElement(systemUser_MandatoryField);
            ValidateElementEnabled(systemUser_MandatoryField);

            return this;
        }

        public SystemUserAliasesRecordPage ValidateLastNameMandatoryField()
        {


            WaitForElement(lastName_MandatoryField);
            ValidateElementEnabled(lastName_MandatoryField);

            return this;
        }

        public SystemUserAliasesRecordPage ValidatePreferredNameMandatoryField()
        {


            WaitForElement(preferredName_MandatoryField);
            ValidateElementEnabled(preferredName_MandatoryField);

            return this;
        }


        public SystemUserAliasesRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public SystemUserAliasesRecordPage ValidateLastNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(lastName_ErrorLabel, ExpectedText);

            return this;
        }

        public SystemUserAliasesRecordPage InsertFirstName(string TextToInsert)
        {
            WaitForElement(firstName_Field);
            SendKeys(firstName_Field, TextToInsert);

            return this;
        }

        public SystemUserAliasesRecordPage InsertMiddleName(string TextToInsert)
        {
            WaitForElement(middleName_Field);
            SendKeys(middleName_Field, TextToInsert);

            return this;
        }

        public SystemUserAliasesRecordPage InsertLastName(string TextToInsert)
        {
            WaitForElement(lastName_Field);
            SendKeys(lastName_Field, TextToInsert);

            return this;
        }

        public SystemUserAliasesRecordPage ClickAliasTypeLookUpButton()
        {
            WaitForElement(aliasType_LookUpButton);
            Click(aliasType_LookUpButton);

            return this;
        }

        public SystemUserAliasesRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }
    }
}
