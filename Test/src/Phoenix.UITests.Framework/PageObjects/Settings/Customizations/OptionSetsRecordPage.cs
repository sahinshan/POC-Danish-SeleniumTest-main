using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class OptionSetsRecordPage : CommonMethods
    {
        public OptionSetsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=optionset&')]");

        #region Options Toolbar

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_grpRelated']/button[@title = 'Menu']");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']");
        readonly By OptionsetValues_SubMenuItem = By.XPath("//*[@id='CWNavItem_optionsetvalue' and @title='Optionset Values']");

        readonly By Name_FieldValue = By.Id("CWField_name");

        #endregion

        public OptionSetsRecordPage WaitForOptionSetsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(pageHeader);
            WaitForElement(DetailsTab);

            return this;
        }

        public OptionSetsRecordPage NavigateToOptionSetValuesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            MoveToElementInPage(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(OptionsetValues_SubMenuItem);
            MoveToElementInPage(OptionsetValues_SubMenuItem);
            Click(OptionsetValues_SubMenuItem);

            return this;
        }

        public OptionSetsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public OptionSetsRecordPage ValidateOptionSetTextValue(string Name)
        {
            WaitForElement(Name_FieldValue);
            MoveToElementInPage(Name_FieldValue);
            ValidateElementValue(Name_FieldValue, Name);

            return this;
        }

    }
}
