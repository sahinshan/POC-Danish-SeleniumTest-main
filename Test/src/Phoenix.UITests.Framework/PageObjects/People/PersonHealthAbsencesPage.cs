using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthAbsencesPage : CommonMethods
    {
        public PersonHealthAbsencesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWPersonHealthAbsenceFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Absences']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']");
        readonly By PersonContractsLeftSubMenuItem = By.Id("CWNavItem_ContractsAndFunding");
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        By PersonAbsenceRowCheckBox(string PersonAbsenceID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonAbsenceID + "']/td[1]/input/parent::td");
        By PersonAbsenceNameRowCheckBox(string PersonAbsenceID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonAbsenceID + "']/td[1]/input/parent::td/parent::tr/td[2]");




        public PersonHealthAbsencesPage WaitForPersonHealthAbsencesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWPersonHealthAbsenceFrame);
            SwitchToIframe(CWPersonHealthAbsenceFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Person Absences");
         

            return this;
        }

        public PersonHealthAbsencesPage NavigateToPersonContractsPage()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            Click(MenuButton);

            WaitForElement(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElement(PersonContractsLeftSubMenuItem);
            Click(PersonContractsLeftSubMenuItem);

            return this;
        }



        public PersonHealthAbsencesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonHealthAbsencesPage ClickRefreshPage()
        {            
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);
            return this;
        }

        public PersonHealthAbsencesPage OpenPersonAbsenceRecord(string PersonAbsenceId)
        {
            WaitForElementToBeClickable(PersonAbsenceRowCheckBox(PersonAbsenceId));
            Click(PersonAbsenceNameRowCheckBox(PersonAbsenceId));

            return this;
        }

    }
}