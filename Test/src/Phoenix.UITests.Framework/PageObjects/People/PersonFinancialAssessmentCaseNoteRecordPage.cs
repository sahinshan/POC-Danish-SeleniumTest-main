using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFinancialAssessmentCaseNoteRecordPage : CommonMethods
    {
        public PersonFinancialAssessmentCaseNoteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']");
        readonly By activitiesLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Activities']/a");
        readonly By caseNotestLeftSubMenuItem = By.Id("CWNavItem_PersonPhysicalObservationCaseNote");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessmentcasenote&')]");
        readonly By CWNavItem_PhysicalObservationFrame = By.Id("CWUrlPanel_IFrame");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By cloneButton = By.Id("TI_CloneRecordButton");
        readonly By DropDownButton = By.Id("CWToolbarMenu");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By GeneratePhysicalObservationChartButton = By.Id("TI_HealthChartButton");


        public PersonFinancialAssessmentCaseNoteRecordPage WaitForPersonFinancialAssessmentCaseNoteRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(personRecordIFrame);
            this.SwitchToIframe(personRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);


            return this;
        }
    
        
        public PersonFinancialAssessmentCaseNoteRecordPage ClickCloneButton()
        {
            WaitForElement(DropDownButton);
            Click(DropDownButton);
            WaitForElement(cloneButton);
            Click(cloneButton);

            return this;
        }
        


    }
}
