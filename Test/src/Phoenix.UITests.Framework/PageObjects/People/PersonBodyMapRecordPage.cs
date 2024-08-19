using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonBodyMapRecordPage : CommonMethods
    {
        public PersonBodyMapRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personbodymap&')]"); 

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Body Map: ']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By responsibleUserLinkField = By.XPath("//a[@id='CWField_responsibleuserid_Link']");
        readonly By dateOfEvent = By.Id("CWField_dateofevent");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public PersonBodyMapRecordPage WaitForPersonBodyMapRecordPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public PersonBodyMapRecordPage ValidateResponsibleUser(string verifytext)
        {
            ValidateElementText(responsibleUserLinkField, verifytext);
            return this;
        }
        public PersonBodyMapRecordPage ValidateDateOfEvent(string ExpectedValue)
        {
            WaitForElement(dateOfEvent);
            ValidateElementValue(dateOfEvent, ExpectedValue);
            return this;
        }

        public PersonBodyMapRecordPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }
    }
}
