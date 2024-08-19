using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteSettingRecordPage : CommonMethods
    {

        public WebSiteSettingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitesetting&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Description_FieldName = By.XPath("//*[@id='CWLabelHolder_description']/label[text()='Description']");
        readonly By SettingsValue_FieldName = By.XPath("//*[@id='CWLabelHolder_settingvalue']/label[text()='Setting Value']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By IsEncrypted_FieldName = By.XPath("//*[@id='CWLabelHolder_isencrypted']/label[text()='Is Encrypted']");
        readonly By EncryptedValue_FieldName = By.XPath("//*[@id='CWLabelHolder_encryptedvalue']/label[text()='Encrypted Value']");


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Description_Field = By.XPath("//*[@id='CWField_description']");
        readonly By SettingValue_Field = By.XPath("//*[@id='CWField_settingvalue']");

        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");

        readonly By IsEncryptedYesOption_RadioButton = By.XPath("//*[@id='CWField_isencrypted_1']");
        readonly By IsEncryptedNoOption_RadioButton = By.XPath("//*[@id='CWField_isencrypted_0']");
        readonly By EncryptedValue_Field = By.XPath("//input[@id='CWField_encryptedvalue']");
        
        readonly By changeEncryptedValue_Link = By.XPath("//a[@id='CWField_encryptedvalue']");

        

        public WebSiteSettingRecordPage WaitForWebSiteSettingRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            this.WaitForElement(Name_FieldName);
            this.WaitForElement(Description_FieldName);
            this.WaitForElement(SettingsValue_FieldName);
            this.WaitForElement(Website_FieldName);
            this.WaitForElement(IsEncrypted_FieldName);
            this.WaitForElement(EncryptedValue_FieldName);

            return this;
        }


        public WebSiteSettingRecordPage ValidateNameFieldText(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSiteSettingRecordPage ValidateDescriptionFieldText(string ExpectedText)
        {
            ValidateElementValue(Description_Field, ExpectedText);

            return this;
        }
        public WebSiteSettingRecordPage ValidateSettingValueFieldText(string ExpectedText)
        {
            ValidateElementValue(SettingValue_Field, ExpectedText);

            return this;
        }
        public WebSiteSettingRecordPage ValidateEncryptedValueFieldText(string ExpectedText)
        {
            ValidateElementValue(EncryptedValue_Field, ExpectedText);

            return this;
        }
        public WebSiteSettingRecordPage ValidateWebsiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        


        public WebSiteSettingRecordPage ValidateIsEncryptedYesRadioButtonChecked()
        {
            ValidateElementChecked(IsEncryptedYesOption_RadioButton);

            return this;
        }
        public WebSiteSettingRecordPage ValidateIsEncryptedNoRadioButtonChecked()
        {
            ValidateElementChecked(IsEncryptedNoOption_RadioButton);

            return this;
        }




        public WebSiteSettingRecordPage ValidateSettingValueFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SettingValue_Field);
            else
                WaitForElementNotVisible(SettingValue_Field, 3);
            
            return this;
        }
        public WebSiteSettingRecordPage ValidateChangeEncryptedValueLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(changeEncryptedValue_Link);
            else
                WaitForElementNotVisible(changeEncryptedValue_Link, 3);

            return this;
        }
        public WebSiteSettingRecordPage ValidateEncryptedValueFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EncryptedValue_Field);
            else
                WaitForElementNotVisible(EncryptedValue_Field, 3);

            return this;
        }





        public WebSiteSettingRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public WebSiteSettingRecordPage InsertDescription(string TextToInsert)
        {
            SendKeys(Description_Field, TextToInsert);

            return this;
        }
        public WebSiteSettingRecordPage InsertSettingValue(string TextToInsert)
        {
            SendKeys(SettingValue_Field, TextToInsert);

            return this;
        }
        public WebSiteSettingRecordPage InsertEncryptedValue(string TextToInsert)
        {
            SendKeys(EncryptedValue_Field, TextToInsert);

            return this;
        }




        public WebSiteSettingRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public WebSiteSettingRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
        public WebSiteSettingRecordPage TapIsEncryptedYesOption()
        {
            Click(IsEncryptedYesOption_RadioButton);

            return this;
        }
        public WebSiteSettingRecordPage TapIsEncryptedNoOption()
        {
            Click(IsEncryptedNoOption_RadioButton);

            return this;
        }
        public WebSiteSettingRecordPage TapChangeEncryptedValueLink()
        {
            Click(changeEncryptedValue_Link);

            return this;
        }



        public WebSiteSettingRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSettingRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSettingRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
