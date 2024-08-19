using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteFeedbackRecordPage : CommonMethods
    {

        public WebSiteFeedbackRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitefeedback&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Email_FieldName = By.XPath("//*[@id='CWLabelHolder_email']/label[text()='Email']");
        readonly By WebsiteFeedbackType_FieldName = By.XPath("//*[@id='CWLabelHolder_websitefeedbacktypeid']/label[text()='Feedback Type']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By ResponsibleTeam_FieldName = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By Message_FieldName = By.XPath("//*[@id='CWLabelHolder_message']/label[text()='Feedback']");
        


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Email_Field = By.XPath("//*[@id='CWField_email']");
        readonly By WebsiteFeedbackType_FieldLink = By.Id("CWField_websitefeedbacktypeid_Link");
        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By ResponsibleTeam_FieldLink = By.Id("CWField_ownerid_Link");
        readonly By Message_Field = By.XPath("//*[@id='CWField_message']");
        




        public WebSiteFeedbackRecordPage WaitForWebSiteFeedbackRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElementNotVisible(saveButton, 3);
            this.WaitForElementNotVisible(saveAndCloseButton, 3);

            this.WaitForElement(deleteButton);

            WaitForElement(Name_FieldName);
            WaitForElement(Email_FieldName);
            WaitForElement(WebsiteFeedbackType_FieldName);
            WaitForElement(Website_FieldName);
            WaitForElement(ResponsibleTeam_FieldName);
            WaitForElement(Message_FieldName);

            return this;
        }


        public WebSiteFeedbackRecordPage ValidateNameFieldValue(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSiteFeedbackRecordPage ValidateEmailFieldValue(string ExpectedText)
        {
            ValidateElementValue(Email_Field, ExpectedText);

            return this;
        }
        public WebSiteFeedbackRecordPage ValidateWebSiteFeedbackTypeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(WebsiteFeedbackType_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteFeedbackRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteFeedbackRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteFeedbackRecordPage ValidateMessage(string ExpectedText)
        {
            ValidateElementValue(Message_Field, ExpectedText);

            return this;
        }


        public WebSiteFeedbackRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
