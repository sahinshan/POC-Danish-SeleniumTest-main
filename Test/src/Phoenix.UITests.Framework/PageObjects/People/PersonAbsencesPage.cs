using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAbsencesPage : CommonMethods
    {
        public PersonAbsencesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By contractIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontract&')]");

        readonly By personFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWNavItem_PersonAbsenceFrame = By.Id("iframe_cppersoncontractcppersonabsence");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Absences']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public PersonAbsencesPage WaitForPersonAbsencesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(contractIFrame);
            SwitchToIframe(contractIFrame);

            WaitForElement(personFrame);
            SwitchToIframe(personFrame);

            WaitForElement(CWNavItem_PersonAbsenceFrame);
            SwitchToIframe(CWNavItem_PersonAbsenceFrame);

            WaitForElement(pageHeader);

            return this;
        }

     

        public PersonAbsencesPage OpenAbsenceRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonAbsencesPage SelectAbsenceRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

       
    }
}
