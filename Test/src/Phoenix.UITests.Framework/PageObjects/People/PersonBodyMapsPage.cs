using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonBodyMapsPage : CommonMethods
    {
        public PersonBodyMapsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); 
        readonly By CWNavItem_BodyMapsFrame = By.Id("CWUrlPanel_IFrame"); 

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Body Maps']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public PersonBodyMapsPage WaitForPersonBodyMapsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_BodyMapsFrame);
            SwitchToIframe(CWNavItem_BodyMapsFrame);

            WaitForElement(pageHeader);

            return this;
        }
     


        public PersonBodyMapsPage ClickNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

       
    }
}
