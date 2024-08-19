
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Care Plans Sub Tab
    /// </summary>
    public class PersonCarePlansSubPage_AssessmentsTab : CommonMethods
    {
        public PersonCarePlansSubPage_AssessmentsTab(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWFrame_ = By.Id("CWFrame");
        readonly By listViewFrame = By.XPath("//iframe[contains(@src,'viewgroup=system&')]");
        readonly By CarePlanType_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[2]/a[@title='Care Plan Type']");
        readonly By Person_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[3]/a[@title='Person']");
        readonly By StartDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[4]/a[@title='Start Date']");
        readonly By Status_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[5]/a[@title='Status']");
        readonly By ReviewDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[6]/a[@title='Review Date']");
        readonly By ReviewFrequency_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[7]/a[@title='Review Frequency']");
        readonly By ResponsibleTeam_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[8]/a[@title='Responsible Team']");

        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By mailMergeButton = By.Id("TI_MailMergeButton");

        #endregion

        #region Records

        By record_CheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordRow(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");


        #endregion




        public PersonCarePlansSubPage_AssessmentsTab WaitForPersonCarePlansSubPage_AssessmentsTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWSubTabsPanel_IFrame);
            SwitchToIframe(CWSubTabsPanel_IFrame);

            WaitForElement(CWFrame_);
            SwitchToIframe(CWFrame_);

            WaitForElement(listViewFrame);
            SwitchToIframe(listViewFrame);

           

            return this;
        }

     
    }
}
