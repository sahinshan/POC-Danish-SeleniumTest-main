using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSitePageRecordDesignerSubPage : CommonMethods
    {

        public WebSitePageRecordDesignerSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitepage&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Website Page Designer']");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By addWidgetButton = By.Id("TI_AddWidgetButton");


        By Widget_Title(string WidgetPosition) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div/span");
        By Widget_SettingsButton(string WidgetPosition) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div/div/button[@title='Settings']");
        By Widget_DeleteButton(string WidgetPosition) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div/div/button[@title='Remove Widget']");

        



        public WebSitePageRecordDesignerSubPage WaitForWebSitePageRecordDesignerSubPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(CWHTMLResourcePanel_IFrame);
            this.SwitchToIframe(CWHTMLResourcePanel_IFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(addWidgetButton);

            return this;
        }

        public WebSitePageRecordDesignerSubPage ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public WebSitePageRecordDesignerSubPage ClickAddWidgetButton()
        {
            this.Click(addWidgetButton);

            return this;
        }

        public WebSitePageRecordDesignerSubPage ValidateWidgetTitle(string WidgetPosition, string ExpectedTitle)
        {
            ValidateElementText(Widget_Title(WidgetPosition), ExpectedTitle);

            return this;
        }

        public WebSitePageRecordDesignerSubPage ClickWidgetSettingsButton(string WidgetPosition)
        {
            this.Click(Widget_SettingsButton(WidgetPosition));

            return this;
        }

        public WebSitePageRecordDesignerSubPage ClickWidgetDeleteButton(string WidgetPosition)
        {
            this.Click(Widget_DeleteButton(WidgetPosition));

            return this;
        }

    }
}
