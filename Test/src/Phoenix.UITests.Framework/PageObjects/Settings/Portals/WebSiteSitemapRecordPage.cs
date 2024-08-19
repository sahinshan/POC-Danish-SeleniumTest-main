using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteSitemapRecordPage : CommonMethods
    {

        public WebSiteSitemapRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitesitemap&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By designerTab = By.XPath("//*[@id='CWNavGroup_Designer']/a");
        readonly By detailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By Type_FieldName = By.XPath("//*[@id='CWLabelHolder_typeid']/label[text()='Type']");


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");

        readonly By Type_Picklist = By.XPath("//*[@id='CWField_typeid']");
        readonly By Type_ErrorLabel = By.XPath("//*[@id='CWControlHolder_typeid']/label/span");




        public WebSiteSitemapRecordPage WaitForWebSiteSitemapRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(backButton);
            this.WaitForElement(detailsTab);

            return this;
        }

        public WebSiteSitemapRecordPage TapDesignerTab()
        {
            Click(designerTab);

            return this;
        }

        public WebSiteSitemapRecordPage TapDetailsTab()
        {
            Click(detailsTab);

            return this;
        }

        public WebSiteSitemapRecordPage WaitForDetailsTabToLoad()
        {
            this.WaitForElement(Name_FieldName);
            this.WaitForElement(Website_FieldName);
            this.WaitForElement(Type_FieldName);
            this.WaitForElement(backButton);

            return this;
        }

        public WebSiteSitemapRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }


        public WebSiteSitemapRecordPage ValidateNameFieldText(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordPage ValidateTypeValueFieldText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Type_Picklist, ExpectedText);

            return this;
        }



        public WebSiteSitemapRecordPage ValidateNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Name_ErrorLabel);
            else
                WaitForElementNotVisible(Name_ErrorLabel, 3);

            return this;
        }
        public WebSiteSitemapRecordPage ValidateTypeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Type_ErrorLabel);
            else
                WaitForElementNotVisible(Type_ErrorLabel, 3);

            return this;
        }



        public WebSiteSitemapRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Name_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordPage ValidateTypeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Type_ErrorLabel, ExpectedText);

            return this;
        }




        public WebSiteSitemapRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public WebSiteSitemapRecordPage SelectType(string TextToSelect)
        {
            SelectPicklistElementByText(Type_Picklist, TextToSelect);

            return this;
        }





        public WebSiteSitemapRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public WebSiteSitemapRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
       



        public WebSiteSitemapRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSitemapRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteSitemapRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }
        

    }
}
