using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePointOfContactRecordPage : CommonMethods
    {

        public WebSitePointOfContactRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitepointofcontact&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Address_FieldName = By.XPath("//*[@id='CWLabelHolder_address']/label[text()='Address']");
        readonly By Phone_FieldName = By.XPath("//*[@id='CWLabelHolder_phone']/label[text()='Phone']");
        readonly By Email_FieldName = By.XPath("//*[@id='CWLabelHolder_email']/label[text()='Email']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By ResponsibleTeam_FieldName = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By Status_FieldName = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        readonly By Address_Field = By.XPath("//*[@id='CWField_address']");
        readonly By Phone_Field = By.XPath("//*[@id='CWField_phone']");
        readonly By Email_Field = By.XPath("//*[@id='CWField_email']");

        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");
        readonly By ResponsibleTeam_FieldLink = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_ErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By Status_Picklist = By.XPath("//*[@id='CWField_statusid']");
        readonly By Status_ErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");




        public WebSitePointOfContactRecordPage WaitForWebSitePointOfContactRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            WaitForElement(Name_FieldName);
            WaitForElement(Address_FieldName);
            WaitForElement(Phone_FieldName);
            WaitForElement(Email_FieldName);
            WaitForElement(Website_FieldName);
            WaitForElement(ResponsibleTeam_FieldName);
            WaitForElement(Status_FieldName);

            return this;
        }


        public WebSitePointOfContactRecordPage ValidateNameFieldValue(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateAddressFieldValue(string ExpectedText)
        {
            ValidateElementValue(Address_Field, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidatePhoneFieldValue(string ExpectedText)
        {
            ValidateElementValue(Phone_Field, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateEmailFieldValue(string ExpectedText)
        {
            ValidateElementValue(Email_Field, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldLink, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Picklist, ExpectedText);

            return this;
        }



        public WebSitePointOfContactRecordPage ValidateNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Name_ErrorLabel);
            else
                WaitForElementNotVisible(Name_ErrorLabel, 3);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_ErrorLabel);
            else
                WaitForElementNotVisible(ResponsibleTeam_ErrorLabel, 3);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateStatusFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Status_ErrorLabel);
            else
                WaitForElementNotVisible(Status_ErrorLabel, 3);

            return this;
        }



        public WebSitePointOfContactRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Name_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSitePointOfContactRecordPage ValidateStatusFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_ErrorLabel, ExpectedText);

            return this;
        }




        public WebSitePointOfContactRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public WebSitePointOfContactRecordPage InsertAddress(string TextToInsert)
        {
            SendKeys(Address_Field, TextToInsert);

            return this;
        }
        public WebSitePointOfContactRecordPage InsertPhone(string TextToInsert)
        {
            SendKeys(Phone_Field, TextToInsert);

            return this;
        }
        public WebSitePointOfContactRecordPage InsertEmail(string TextToInsert)
        {
            SendKeys(Email_Field, TextToInsert);

            return this;
        }
        public WebSitePointOfContactRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Picklist, TextToSelect);

            return this;
        }




        public WebSitePointOfContactRecordPage TapResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public WebSitePointOfContactRecordPage TapResponsibleTeamRemoveButton()
        {
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
       



        public WebSitePointOfContactRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePointOfContactRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePointOfContactRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
