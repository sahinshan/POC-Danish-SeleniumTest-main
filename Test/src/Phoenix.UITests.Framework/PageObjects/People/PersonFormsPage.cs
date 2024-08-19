using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    
    public class PersonFormsPage : CommonMethods
    {
        public PersonFormsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src, 'editpage.aspx')]");
        readonly By personFormFrame = By.Id("CWUrlPanel_IFrame");

        readonly By newButton = By.Id("TI_NewRecordButton");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Forms (Person)']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        



        public PersonFormsPage WaitForPersonFormsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(personFormFrame);
            SwitchToIframe(personFormFrame);

            WaitForElementVisible(pageHeader);

            return this;
        }

        
        public PersonFormsPage TapNewButton()
        {
            WaitForElementToBeClickable(newButton);
            Click(newButton);

            return this;
        }

        public PersonFormsPage OpenRecord(string RecordID)
        {
            WaitForElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }
    }
}
