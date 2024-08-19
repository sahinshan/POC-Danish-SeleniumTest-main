using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteAnnouncementRecordPage : CommonMethods
    {

        public WebSiteAnnouncementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteannouncement&')]");
        
        //contents rich textbox is inside his own iframe
        readonly By contentsIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_contents']");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Contents_FieldName = By.XPath("//*[@id='CWLabelHolder_contents']/label[text()='Contents']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By Status_FieldName = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By ExpiresOn_FieldName = By.XPath("//*[@id='CWLabelHolder_expireson']/label[text()='Expires On']");
        readonly By PublishedOn_FieldName = By.XPath("//*[@id='CWLabelHolder_publishedon']/label[text()='Published On']");


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        By Contents_Field(string LineNumber) => By.XPath("/html/body/p[" + LineNumber + "]");

        readonly By textarea_Field = By.XPath("//textarea[@id='CWField_contents']");
        readonly string textarea_FieldName = "CWField_contents";



        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");

        readonly By Status_Picklist = By.XPath("//*[@id='CWField_statusid']");
        readonly By Status_ErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");
        readonly By ExpiresOn_DateField = By.XPath("//*[@id='CWField_expireson']");
        readonly By ExpiresOn_TimeField = By.XPath("//*[@id='CWField_expireson_Time']");
        readonly By PublishedOn_DateField = By.XPath("//*[@id='CWField_publishedon']");
        readonly By PublishedOn_TimeField = By.XPath("//*[@id='CWField_publishedon_Time']");




        public WebSiteAnnouncementRecordPage WaitForWebSiteAnnouncementRecordPageToLoad()
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
            this.WaitForElement(Contents_FieldName);
            this.WaitForElement(Website_FieldName);
            this.WaitForElement(Status_FieldName);
            this.WaitForElement(ExpiresOn_FieldName);
            this.WaitForElement(PublishedOn_FieldName);

            return this;
        }

        public WebSiteAnnouncementRecordPage LoadContentsRichTextBox()
        {
            WaitForElement(contentsIframe);
            SwitchToIframe(contentsIframe);

            return this;
        }


        public WebSiteAnnouncementRecordPage ValidateNameFieldText(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateContentsFieldText(string LineNumber, string ExpectedText)
        {
            ValidateElementText(Contents_Field(LineNumber), ExpectedText);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateStatusValueFieldText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Picklist, ExpectedText);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateExpiresOnValueFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(ExpiresOn_DateField, ExpectedDate);
            ValidateElementValue(ExpiresOn_TimeField, ExpectedTime);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidatePublishedOnValueFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(PublishedOn_DateField, ExpectedDate);
            ValidateElementValue(PublishedOn_TimeField, ExpectedTime);

            return this;
        }



        public WebSiteAnnouncementRecordPage ValidateNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Name_ErrorLabel);
            else
                WaitForElementNotVisible(Name_ErrorLabel, 3);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateStatusFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Status_ErrorLabel);
            else
                WaitForElementNotVisible(Status_ErrorLabel, 3);

            return this;
        }



        public WebSiteAnnouncementRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Name_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteAnnouncementRecordPage ValidateStatusFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_ErrorLabel, ExpectedText);

            return this;
        }




        public WebSiteAnnouncementRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public WebSiteAnnouncementRecordPage InsertContent(string LineNumber, string TextToInsert)
        {
            SetElementDisplayStyleToInline(textarea_FieldName);
            SetElementVisibilityStyleToVisible(textarea_FieldName);
            SendKeys(textarea_Field, TextToInsert);

            return this;
        }
        public WebSiteAnnouncementRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Picklist, TextToSelect);

            return this;
        }
        public WebSiteAnnouncementRecordPage InsertExpiresOn(string Date, string Time)
        {
            SendKeys(ExpiresOn_DateField, Date);
            SendKeysWithoutClearing(ExpiresOn_DateField, Keys.Tab);
            SendKeys(ExpiresOn_TimeField, Time);

            return this;
        }
        public WebSiteAnnouncementRecordPage InsertPublishedOn(string Date, string Time)
        {
            SendKeys(PublishedOn_DateField, Date);
            SendKeysWithoutClearing(PublishedOn_DateField, Keys.Tab);
            SendKeys(PublishedOn_TimeField, Time);

            return this;
        }




        public WebSiteAnnouncementRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public WebSiteAnnouncementRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
       



        public WebSiteAnnouncementRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteAnnouncementRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteAnnouncementRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
