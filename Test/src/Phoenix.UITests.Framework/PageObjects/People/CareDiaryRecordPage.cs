
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
    public class CareDiaryRecordPage : CommonMethods
    {
        public CareDiaryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpregularcaretaskdiary&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By LinkedRecord_LookUpBtn = By.Id("CWLookupBtn_linkedrecordid");
        readonly By LinkedRecord_TextFld = By.XPath("//div/a[@id='CWField_linkedrecordid_Link']");

        

        public CareDiaryRecordPage WaitForPersonCarePlansSubPage_CareDiaryRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);
            
            return this;
        }

        public CareDiaryRecordPage CareDiaryRecordPage_ValidateLinkedRecordFld(string expectedText)
        {
            ValidateElementDisabled(LinkedRecord_LookUpBtn);
            ValidateElementText(LinkedRecord_TextFld, expectedText);
            
            return this;
        }

    }
}
