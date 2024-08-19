
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemSettingRecordPage : CommonMethods
    {
        public SystemSettingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemsetting&')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By menu_Button = By.Id("CWNavGroup_Menu");
        readonly By audit_Button = By.Id("CWNavItem_AuditHistory");

        readonly By name_Field = By.Id("CWField_name");
        readonly By settingvalue_Field = By.Id("CWField_settingvalue");


        #region Fields

        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_LoopUpButton = By.Id("CWLookupBtn_systemuserid");
       
        readonly By propertyname_Field = By.Id("CWField_propertyname");
        readonly By propertyno_Field = By.Id("CWField_addressline1");
        readonly By street_Field = By.Id("CWField_addressline2");
        readonly By villageDistrict_Field = By.Id("CWField_addressline3");
        readonly By townCity_Field = By.Id("CWField_addressline4");
        readonly By postcode_Field = By.Id("CWField_postcode");

        readonly By county_Field = By.Id("CWField_addressline5");
        readonly By country_Field = By.Id("CWField_country");
        readonly By addressType_Field = By.Id("CWField_addresstypeid");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By endDate_Field = By.Id("CWField_enddate");

        readonly By addressSearch_Button = By.Id("CWFieldButton_AddressSearch");


        readonly By addressType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_addresstypeid']/label/span[@class='mandatory']");
        readonly By startDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class ='mandatory']");

        #endregion

        #region FieldLabel
        readonly By propertyname_FieldLabel = By.Id("CWLabelHolder_propertyname");
        readonly By propertyno_FieldLabel = By.Id("CWLabelHolder_addressline1");
        readonly By street_FieldLabel = By.Id("CWLabelHolder_addressline2");
        readonly By villageDistrict_FieldLabel = By.Id("CWLabelHolder_addressline3");
        readonly By townCity_FieldLabel = By.Id("CWLabelHolder_addressline4");
        readonly By postcode_FieldLabel = By.Id("CWLabelHolder_postcode");

        readonly By county_FieldLabel = By.Id("CWLabelHolder_addressline5");
        readonly By country_FieldLabel = By.Id("CWLabelHolder_country");
        readonly By addressType_FieldLabel = By.Id("CWLabelHolder_addresstypeid");
        readonly By startDate_FieldLabel = By.Id("CWLabelHolder_startdate");
        readonly By endDate_FieldLabel = By.Id("CWLabelHolder_enddate");

        readonly By addressSearch_ButtonLabel = By.Id("CWControlHolder_AddressSearch");

        #endregion



        public SystemSettingRecordPage WaitForSystemSettingRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
           
            return this;
        }        

      
        public SystemSettingRecordPage ValidateSettingValueFieldText(string ExpectedText)
        {
            WaitForElement(settingvalue_Field);
            ValidateElementValue(settingvalue_Field, ExpectedText);

            return this;
        }

        public SystemSettingRecordPage InsertSettingValue(string Text)
        {
            WaitForElement(settingvalue_Field);
            SendKeys(settingvalue_Field, Text);

            return this;
        }


        public SystemSettingRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemSettingRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

        public SystemSettingRecordPage ValidateEndDate(string ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);


            return this;
        }

        public SystemSettingRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemSettingRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        public SystemSettingRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            WaitForElement(name_Field);
            ValidateElementValue(name_Field, ExpectedValue);

            return this;
        }

    }
}
