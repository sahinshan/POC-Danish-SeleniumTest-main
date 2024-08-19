
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Cases.Health;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Regular Care Tasks Sub Tab
    /// </summary>
    public class CareDiaryPage : CommonMethods
    {
        public CareDiaryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CareDiaryIFrame = By.Id("CWFrame");
        readonly By CareDiary_Header = By.XPath("//div[@id='CWWrapper']//div/h1[text()='Care Diary']");
        readonly By CareDiary_RecordCount = By.XPath("//div[@id='CWPagingFooter']//span[@class='result-count']");
        readonly By CareDiary_RecordSelector = By.Id("CWViewSelector");
        By StatusField(string startdate,string status) => By.XPath("//td[text()='" +startdate+ "']/following-sibling::td[text()='"+status+"']");
        By CareDiaryRecord(string startdate) => By.XPath("//td[text()='" + startdate + "']");





        public CareDiaryPage WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWSubTabsPanel_IFrame);
            SwitchToIframe(CWSubTabsPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CareDiaryIFrame);
            SwitchToIframe(CareDiaryIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(CareDiary_Header);

            return this;
        }

        public CareDiaryPage SelectRecordPicklist(string TextToSelect)
        {
            WaitForElementVisible(CareDiary_RecordSelector);
            SelectPicklistElementByText(CareDiary_RecordSelector, TextToSelect);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        //method to validate Carediary's created
        public CareDiaryPage ValidateCareDiaryRecordCount(string ExpectedText)
        {
            WaitForElementVisible(CareDiary_RecordCount);
           // ScrollToElement(CareDiary_RecordCount);
            var elementText = GetElementText(CareDiary_RecordCount);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        public CareDiaryPage ValidateCareDiaryStatus(string startdate,string status,string ExpectedText)
        {
            WaitForElementVisible(CareDiary_RecordCount);
            ValidateElementText(StatusField(startdate, status), ExpectedText);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public CareDiaryPage ClickCareDiaryRecord(string startdate)
        {
            WaitForElementToBeClickable(CareDiaryRecord(startdate));
            Click(CareDiaryRecord(startdate));
            return this;
        }


    }
}
