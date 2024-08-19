using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserEmergencyContactsRecordPage : CommonMethods
    {
        public SystemUserEmergencyContactsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseremergencycontacts&')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By menu_Button = By.Id("CWNavGroup_Menu");
        readonly By audit_Button = By.Id("CWNavItem_AuditHistory");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");


        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        #region Fields

        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_Mandatoryfield = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label/span[@class ='mandatory']");
        readonly By systemUser_LookUpButtonDisabled = By.XPath("//*[@id='CWLookupBtn_systemuserid'][@disabled='disabled']");
        readonly By systemUser_LoopUpButton = By.Id("CWLookupBtn_systemuserid");


        readonly By title_Field = By.Id("CWField_titleid_cwname");
        readonly By titleid_FieldLookup = By.Id("CWLookupBtn_titleid");
        readonly By firstName_Field = By.Id("CWField_firstname");
        readonly By firstName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_firstname']/label/span[@class = 'mandatory']");
        readonly By lastName_Field = By.Id("CWField_lastname");
        readonly By lastName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_lastname']/label/span[@class = 'mandatory']");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By startDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class = 'mandatory']");
        readonly By nextOfKin_MandatoryField = By.XPath("//*[@id='CWLabelHolder_nextofkin']/label/span[@class = 'mandatory']");
        readonly By nextOfKin_YesOption = By.Id("CWField_nextofkin_1");
        readonly By nextOfKin_NoOption = By.Id("CWField_nextofkin_0");
        readonly By contactTelephonePrimary_Field = By.Id("CWField_contacttelephoneprimary");
        readonly By contactTelephonePrimaary_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contacttelephoneprimary']/label/span[@class = 'mandatory']");
        readonly By contactTelephoneOther1_Field = By.Id("CWField_contacttelephoneother1");
        readonly By contactTelephoneOther2_Field = By.Id("CWField_contacttelephoneother2");
        readonly By contactTelephoneOther3_Field = By.Id("CWField_contacttelephoneother3");
        readonly By endDate_Field = By.Id("CWField_enddate");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By contactTelephonePrimary_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_contacttelephoneprimary']/label/span");
        readonly By contactTelephoneOther1_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_contacttelephoneother1']/label/span");
        readonly By contactTelephoneOther2_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_contacttelephoneother2']/label/span");
        readonly By contactTelephoneOther3_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_contacttelephoneother3']/label/span");
        readonly By firstName_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_firstname']/label/span");
        readonly By lastName_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_lastname']/label/span");
        #endregion



        public SystemUserEmergencyContactsRecordPage WaitForSystemUserEmergencyContactsRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(audit_MenuLeftSubMenu);
            Click(audit_MenuLeftSubMenu);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateSystemUser_MandatoryField()
        {
            ValidateElementEnabled(systemUser_Mandatoryfield);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateSystemUserEmergencyContactsRecordPageTitle(string PageTitle)
        {


            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateSystemUser_Editable(bool ExpectedText)
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

        public SystemUserEmergencyContactsRecordPage ValidateSystemUserLookUp_Disabled(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElementToBeClickable(systemUser_LookUpButtonDisabled);
                ValidateElementDisabled(systemUser_LookUpButtonDisabled);

            }
            else
            {
                WaitForElement(systemUser_LookUpButtonDisabled, 5);

            }
            return this;
        }




        public SystemUserEmergencyContactsRecordPage ValidateSystemUser_LinkField(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ValidateElementTextContainsText(systemUser_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ClickSystemUserLoopUpButton()
        {
            WaitForElement(systemUser_LoopUpButton);
            Click(systemUser_LoopUpButton);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateTitle_Field()
        {
            ValidateElementEnabled(title_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateTitle_Field_Text(string ExpectedText)
        {
            WaitForElement(title_Field);
            ValidateElementTextContainsText(title_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateFirstName_Field()
        {
            ValidateElementEnabled(firstName_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateFirstName_Field_Text(string ExpectedText)
        {
            WaitForElement(firstName_Field);
            ValidateElementTextContainsText(firstName_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateFirstName_MandatoryField()
        {
            ValidateElementEnabled(firstName_MandatoryField);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateLastName_Field()
        {
            ValidateElementEnabled(lastName_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateLastName_Field_Text(string ExpectedText)
        {
            WaitForElement(lastName_Field);
            ValidateElementTextContainsText(lastName_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateLastName_MandatoryField()
        {
            ValidateElementEnabled(lastName_MandatoryField);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateStartDate_Field()
        {
            ValidateElementEnabled(startDate_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateStartDate_Field_Text(string ExpectedText)
        {
            WaitForElement(startDate_Field);
            ValidateElementValue(startDate_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateStartDate_MandatoryField()
        {
            ValidateElementEnabled(startDate_MandatoryField);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateNextOfKin_MandatoryField()
        {
            ValidateElementEnabled(nextOfKin_MandatoryField);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateNextOfKin_NoOption()
        {
            ValidateElementChecked(nextOfKin_NoOption);
            return this;

        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelehonePrimary_Field()
        {
            ValidateElementEnabled(contactTelephonePrimary_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateContactTelehonePrimary_Field_Text(string ExpectedText)
        {
            WaitForElement(contactTelephonePrimary_Field);
            ValidateElementTextContainsText(contactTelephonePrimary_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelehonePrimary_MandatoryField()
        {
            ValidateElementEnabled(contactTelephonePrimaary_MandatoryField);


            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther1_Field()
        {
            ValidateElementEnabled(contactTelephoneOther1_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther1_Field_Text(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther1_Field);
            ValidateElementTextContainsText(contactTelephoneOther1_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther2_Field()
        {
            ValidateElementEnabled(contactTelephoneOther2_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther2_Field_Text(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther2_Field);
            ValidateElementTextContainsText(contactTelephoneOther2_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther3_Field()
        {
            ValidateElementEnabled(contactTelephoneOther3_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateContactTelehoneOther3_Field_Text(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther3_Field);
            ValidateElementTextContainsText(contactTelephoneOther3_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateEndDate_Field()
        {
            ValidateElementEnabled(endDate_Field);


            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateEndDate_Field_Text(string ExpectedText)
        {
            WaitForElement(endDate_Field);
            ValidateElementTextContainsText(endDate_Field, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            WaitForElement(notificationMessage);
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelephonePrimaryFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(contactTelephonePrimary_Field_NottificationMessage);
            ValidateElementText(contactTelephonePrimary_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelephoneOther1FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther1_Field_NottificationMessage);
            ValidateElementText(contactTelephoneOther1_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateContactTelephoneOther2FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther2_Field_NottificationMessage);
            ValidateElementText(contactTelephoneOther2_Field_NottificationMessage, ExpectedText);

            return this;
        }
        public SystemUserEmergencyContactsRecordPage ValidateContactTelephoneOther3FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(contactTelephoneOther3_Field_NottificationMessage);
            ValidateElementText(contactTelephoneOther3_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateFirstNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(firstName_Field_NottificationMessage);
            ValidateElementText(firstName_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ValidateLastNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(lastName_Field_NottificationMessage);
            ValidateElementText(lastName_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ClickTitleLookupButton()
        {
            WaitForElement(titleid_FieldLookup);
            Click(titleid_FieldLookup);

            return this;
        }


        public SystemUserEmergencyContactsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            MoveToElementInPage(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserEmergencyContactsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }



        public SystemUserEmergencyContactsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemUserEmergencyContactsRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        public SystemUserEmergencyContactsRecordPage NavigateToMenuSubPage_Aduit()
        {


            WaitForElement(menu_Button);
            Click(menu_Button);

            WaitForElement(audit_Button);
            Click(audit_Button);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertFirstName(string TextToInsert)
        {

            SendKeys(firstName_Field, TextToInsert);
          
            return this;
        }


        public SystemUserEmergencyContactsRecordPage InsertLastName(string TextToInsert)
        {

            SendKeys(lastName_Field, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertStartDate(string TextToInsert)
        {

            SendKeys(startDate_Field, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertContactTelephonePrimary(string TextToInsert)
        {

            SendKeys(contactTelephonePrimary_Field, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertContactTelephoneOther1(string TextToInsert)
        {

            SendKeys(contactTelephoneOther1_Field, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertContactTelephoneOther2(string TextToInsert)
        {

            SendKeys(contactTelephoneOther2_Field, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage InsertContactTelephoneOther3(string TextToInsert)
        {

            SendKeys(contactTelephoneOther3_Field, TextToInsert);

            return this;
        }


        public SystemUserEmergencyContactsRecordPage InsertEndDate(string TextToInsert)
        {

            SendKeys(endDate_Field, TextToInsert);

            return this;
        }



        public SystemUserEmergencyContactsRecordPage ClickNextOfKin_YesOption()
        {
            WaitForElement(nextOfKin_YesOption);
            Click(nextOfKin_YesOption);

            return this;
        }

        public SystemUserEmergencyContactsRecordPage ClickNextOfKin_NoOption()
        {
            WaitForElement(nextOfKin_NoOption);
            Click(nextOfKin_NoOption);

            return this;
        }

    }
}

