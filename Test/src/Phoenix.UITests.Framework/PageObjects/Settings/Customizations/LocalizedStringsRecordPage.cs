using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class LocalizedStringsRecordPage : CommonMethods
    {
        public LocalizedStringsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=localizedstring&')]");

        #region Options Toolbar

        readonly By pageHeader = By.XPath("//h1[contains(@title,'Localized String:')]");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_grpRelated']/a");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']");
        readonly By LocalizedStringValues_SubMenuItem = By.XPath("//a[@id='CWNavItem_localizedstringvalue']");

        readonly By Name_FieldValue = By.Id("CWField_name");

        #endregion

        public LocalizedStringsRecordPage WaitForLocalizedStringsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);
            WaitForElement(DetailsTab);

            return this;
        }

        public LocalizedStringsRecordPage NavigateToLocalizedStringValuesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            MoveToElementInPage(MenuButton);
            Click(MenuButton);

            WaitForElement(RelatedItems_LeftMenu);
            MoveToElementInPage(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(LocalizedStringValues_SubMenuItem);
            MoveToElementInPage(LocalizedStringValues_SubMenuItem);
            Click(LocalizedStringValues_SubMenuItem);

            return this;
        }

        public LocalizedStringsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public LocalizedStringsRecordPage ValidateOptionSetTextValue(string Name)
        {
            WaitForElement(Name_FieldValue);
            MoveToElementInPage(Name_FieldValue);
            ValidateElementValue(Name_FieldValue, Name);

            return this;
        }

    }
}
