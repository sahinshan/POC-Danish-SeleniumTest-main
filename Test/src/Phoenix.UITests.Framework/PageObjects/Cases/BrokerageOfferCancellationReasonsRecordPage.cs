using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class BrokerageOfferCancellationReasonsRecordPage : CommonMethods
    {
        public BrokerageOfferCancellationReasonsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By brokerageOfferCancellation = By.Id("iframe_brokerageoffercancellationreason");
        readonly By CWNavItem_BrokerageOfferCancellationReasons = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageoffercancellationreason')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

      
        readonly By back_Button = By.XPath("/html/body/form/div[3]/div/div/button[@onclick='CW.DataForm.Close(); return false;']");
        readonly By defaultForEpisodeCancallation_YesOption = By.Id("CWField_defaultforepisodecancellation_1");
        readonly By defaultForEpisodeCancallation_NoOption = By.Id("CWField_defaultforepisodecancellation_0");
        readonly By save_Button = By.Id("TI_SaveButton");



        public BrokerageOfferCancellationReasonsRecordPage WaitForBrokerageOfferCancellationReasonsRecordPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(brokerageOfferCancellation);
            SwitchToIframe(brokerageOfferCancellation);

            WaitForElement(CWNavItem_BrokerageOfferCancellationReasons);
            SwitchToIframe(CWNavItem_BrokerageOfferCancellationReasons);

           

           

            return this;
        }


        public BrokerageOfferCancellationReasonsRecordPage ClickDefaultForEpisodeCancellation_YesOption()
        {

            WaitForElementVisible(defaultForEpisodeCancallation_YesOption);
            Click(defaultForEpisodeCancallation_YesOption);

            return this;
        }


        public BrokerageOfferCancellationReasonsRecordPage ClickDefaultForEpisodeCancellation_NoOption()
        {

            WaitForElementVisible(defaultForEpisodeCancallation_NoOption);
            Click(defaultForEpisodeCancallation_NoOption);

            return this;
        }


        public BrokerageOfferCancellationReasonsRecordPage ClickSaveButton()
        {

            WaitForElementVisible(save_Button);
            Click(save_Button);

            return this;
        }

















        public BrokerageOfferCancellationReasonsRecordPage ClickBackButton()
        {

            WaitForElementVisible(back_Button);
            Click(back_Button);

            return this;
        }

        
       



    }
}
