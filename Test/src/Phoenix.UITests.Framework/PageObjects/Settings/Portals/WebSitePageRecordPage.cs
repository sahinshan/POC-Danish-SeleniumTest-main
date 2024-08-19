using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePageRecordPage : CommonMethods
    {

        public WebSitePageRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitepage&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By DesignerTab = By.XPath("//*[@id='CWNavGroup_Designer']/a[text()='Designer']");
        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a[text()='Details']");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By label_FieldName = By.XPath("//*[@id='CWLabelHolder_labelid']/label[text()='Label']");
        readonly By ParentPage_FieldName = By.XPath("//*[@id='CWLabelHolder_parentpageid']/label[text()='Parent Page']");
        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By LayoutJson_FieldName = By.XPath("//*[@id='CWLabelHolder_layoutjson']/label[text()='Layout Json']");
        readonly By IsSecure_FieldName = By.XPath("//*[@id='CWLabelHolder_issecure']/label[text()='Is Secure']");


        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");

        readonly By SitemapOrBreadCrumbText_FieldLink = By.Id("CWField_sitemaporbreadcrumbtextid_Link");
        readonly By SitemapOrBreadCrumbText_RemoveButton = By.Id("CWClearLookup_sitemaporbreadcrumbtextid");
        readonly By SitemapOrBreadCrumbText_LookupButton = By.Id("CWLookupBtn_sitemaporbreadcrumbtextid");

        readonly By ParentPage_FieldLink = By.Id("CWField_parentpageid_Link");
        readonly By ParentPage_RemoveButton = By.Id("CWClearLookup_parentpageid");
        readonly By ParentPage_LookupButton = By.Id("CWLookupBtn_parentpageid");

        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");
        
        readonly By IsSecureYesOption_RadioButton = By.XPath("//*[@id='CWField_issecure_1']");
        readonly By IsSecureNoOption_RadioButton = By.XPath("//*[@id='CWField_issecure_0']");



        public WebSitePageRecordPage WaitForWebSitePageRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            //this.WaitForElement(DesignerTab);
            this.WaitForElement(DetailsTab);

            return this;
        }

        public WebSitePageRecordPage ClickDesignerTab()
        {
            Click(DesignerTab);

            return this;
        }

        public WebSitePageRecordPage ClickDetailsTab()
        {
            Click(DetailsTab);

            return this;
        }


        public WebSitePageRecordPage ValidateNameFieldText(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }
        public WebSitePageRecordPage ValidateSitemapOrBreadCrumbTextFieldLink(string ExpectedText)
        {
            ValidateElementText(SitemapOrBreadCrumbText_FieldLink, ExpectedText);

            return this;
        }
        public WebSitePageRecordPage ValidateParentPageFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ParentPage_FieldLink, ExpectedText);

            return this;
        }
        public WebSitePageRecordPage ValidateWebsiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSitePageRecordPage ValidateIsSecureYesRadioButtonChecked()
        {
            ValidateElementChecked(IsSecureYesOption_RadioButton);

            return this;
        }
        public WebSitePageRecordPage ValidateIsSecureNoRadioButtonChecked()
        {
            ValidateElementChecked(IsSecureNoOption_RadioButton);

            return this;
        }




        public WebSitePageRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public WebSitePageRecordPage TapSitemapOrBreadCrumbTextLookupButton()
        {
            Click(SitemapOrBreadCrumbText_LookupButton);

            return this;
        }
        public WebSitePageRecordPage TapParentPageLookupButton()
        {
            Click(ParentPage_LookupButton);

            return this;
        }
        public WebSitePageRecordPage TapParentPageRemoveButton()
        {
            Click(ParentPage_RemoveButton);

            return this;
        }
        public WebSitePageRecordPage TapWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public WebSitePageRecordPage TapWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
        public WebSitePageRecordPage TapIsSecureYesOption()
        {
            Click(IsSecureYesOption_RadioButton);

            return this;
        }
        public WebSitePageRecordPage TapIsSecureNoOption()
        {
            Click(IsSecureNoOption_RadioButton);

            return this;
        }



        public WebSitePageRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePageRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSitePageRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }

    }
}
