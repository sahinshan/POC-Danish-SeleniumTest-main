using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteContactRecordPage : CommonMethods
    {

        public WebSiteContactRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitecontact&')]");
        
        //contents rich textbox is inside his how iframe
        readonly By contentsIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_message']");



        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Email_FieldName = By.XPath("//*[@id='CWLabelHolder_emailaddress']/label[text()='Email']");
        readonly By ResponsibleTeam_FieldName = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By PointOfContact_FieldName = By.XPath("//*[@id='CWLabelHolder_websitepointofcontactid']/label[text()='Point of Contact']");

        readonly By Subject_FieldName = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By Message_FieldName = By.XPath("//*[@id='CWLabelHolder_message']/label[text()='Message']");
        
        


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        readonly By Email_Field = By.XPath("//*[@id='CWField_emailaddress']");
        readonly By Email_ErrorLabel = By.XPath("//*[@id='CWControlHolder_emailaddress']/label/span");
        readonly By ResponsibleTeam_FieldLink = By.Id("CWField_ownerid_Link");
        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");
        readonly By PointOfContact_FieldLink = By.XPath("//*[@id='CWField_websitepointofcontactid_Link']");
        readonly By PointOfContact_RemoveButton = By.XPath("//*[@id='CWClearLookup_websitepointofcontactid']");
        readonly By PointOfContact_LookupButton = By.XPath("//*[@id='CWLookupBtn_websitepointofcontactid']");
        readonly By PointOfContact_ErrorLabel = By.XPath("//*[@id='CWControlHolder_websitepointofcontactid']/label/span");

        readonly By Subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By Subject_ErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By textarea_Field = By.XPath("//textarea[@id='CWField_message']");
        readonly string textarea_FieldName = "CWField_message";

        By Message_Field(string LineNumber) => By.XPath("//html/body/p[" + LineNumber + "]");

        public WebSiteContactRecordPage WaitForWebSiteContactRecordPageToLoad()
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
            WaitForElement(Email_FieldName);
            WaitForElement(ResponsibleTeam_FieldName);
            WaitForElement(Website_FieldName);
            WaitForElement(PointOfContact_FieldName);
            WaitForElement(Subject_FieldName);
            WaitForElement(Message_FieldName);

            return this;
        }

        public WebSiteContactRecordPage LoadMessageRichTextBox()
        {
            WaitForElement(contentsIframe);
            SwitchToIframe(contentsIframe);

            return this;
        }

        public WebSiteContactRecordPage ValidateNameFieldValue(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateEmailFieldValue(string ExpectedText)
        {
            ValidateElementValue(Email_Field, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldLink, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidatePointOfContactLinkText(string ExpectedText)
        {
            ValidateElementText(PointOfContact_FieldLink, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateSubjectFieldValue(string ExpectedText)
        {
            ValidateElementValue(Subject_Field, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateMessageFieldText(string LineNumber, string ExpectedText)
        {
            WaitForElementVisible(Message_Field(LineNumber));
            ValidateElementText(Message_Field(LineNumber), ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Name_ErrorLabel);
            else
                WaitForElementNotVisible(Name_ErrorLabel, 3);

            return this;
        }

        public WebSiteContactRecordPage ValidateEmailFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Email_ErrorLabel);
            else
                WaitForElementNotVisible(Email_ErrorLabel, 3);

            return this;
        }

        public WebSiteContactRecordPage ValidatePointOfContactFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PointOfContact_ErrorLabel);
            else
                WaitForElementNotVisible(PointOfContact_ErrorLabel, 3);

            return this;
        }

        public WebSiteContactRecordPage ValidateSubjectFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Subject_ErrorLabel);
            else
                WaitForElementNotVisible(Subject_ErrorLabel, 3);

            return this;
        }

        public WebSiteContactRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Name_ErrorLabel, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateEmailFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Email_ErrorLabel, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidatePointOfContactFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(PointOfContact_ErrorLabel, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage ValidateSubjectFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Subject_ErrorLabel, ExpectedText);

            return this;
        }

        public WebSiteContactRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public WebSiteContactRecordPage InsertEmail(string TextToInsert)
        {
            SendKeys(Email_Field, TextToInsert);

            return this;
        }

        public WebSiteContactRecordPage InsertSubject(string TextToInsert)
        {
            SendKeys(Subject_Field, TextToInsert);

            return this;
        }

        public WebSiteContactRecordPage InsertMessage(string TextToInsert)
        {
            SetElementDisplayStyleToInline(textarea_FieldName);
            SetElementVisibilityStyleToVisible(textarea_FieldName);
            SendKeys(textarea_Field, TextToInsert);

            return this;
        }

        public WebSiteContactRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }

        public WebSiteContactRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }

        public WebSiteContactRecordPage TapPointOfContactLookupButton()
        {
            this.Click(PointOfContact_LookupButton);

            return this;
        }

        public WebSiteContactRecordPage TapPointOfContactRemoveButton()
        {
            this.Click(PointOfContact_RemoveButton);

            return this;
        }

        public WebSiteContactRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteContactRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteContactRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
