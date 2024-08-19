using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonFormActionsOutcomesPageFrame : CommonMethods
    {
        public PersonFormActionsOutcomesPageFrame(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newCaseFormIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]");
        readonly By CWIFrame_PersonFormOutcomes = By.Id("CWIFrame_PersonFormOutcomes");

        readonly By newButton = By.Id("TI_NewRecordButton");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Form Actions/Outcomes']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordStatusCell(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[5]");
        

        public PersonFormActionsOutcomesPageFrame WaitForPersonFormActionsOutcomesPageFrameToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newCaseFormIFrame);
            SwitchToIframe(newCaseFormIFrame);

            WaitForElement(CWIFrame_PersonFormOutcomes);
            SwitchToIframe(CWIFrame_PersonFormOutcomes);

            WaitForElement(pageHeader);

            return this;
        }

        /// <summary>
        /// Tap on the New+ button
        /// </summary>
        public CaseFormPage TapNewButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            driver.FindElement(newButton).Click();

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage OpenRecord(string RecordID)
        {
            this.WaitForElement(RecordIdentifier(RecordID));
            this.Click(RecordIdentifier(RecordID));

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public PersonFormActionsOutcomesPageFrame ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            this.WaitForElement(RecordStatusCell(RecordID));
            this.ValidateElementText(RecordStatusCell(RecordID), ExpectedText);

            return this;
        }
    }
}
