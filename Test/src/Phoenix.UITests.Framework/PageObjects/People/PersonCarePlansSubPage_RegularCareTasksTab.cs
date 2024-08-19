
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Regular Care Tasks Sub Tab
    /// </summary>
    public class PersonCarePlansSubPage_RegularCareTasksTab : CommonMethods
    {
        public PersonCarePlansSubPage_RegularCareTasksTab(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=person'
        readonly By iframe_RegularCare = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=regularcaretask&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=regularcare'
        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWFrame_ = By.Id("CWFrame");
        readonly By listViewFrame = By.XPath("//iframe[contains(@src,'dataviewid=048e2a4e-c770-ed11-a354-0050569231cf')]");
        readonly By InactivelistViewFrame = By.XPath("//iframe[contains(@src,'dataviewid=57961332-1c97-ed11-a355-0050569231cf')]");
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
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Records

        By record_CheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordRow(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By record(string CareTask) => By.XPath("//table[@id='CWGrid']//tbody/tr/td[2][@title='" + CareTask + "']");
        readonly By ActivateButton = By.XPath("//*[@id='TI_ActivateButton']");

        #endregion




        public PersonCarePlansSubPage_RegularCareTasksTab WaitForPersonCarePlansSubPage_RegularCareTasksTabToLoad()
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


            // WaitForElement(InactivelistViewFrame);
            //SwitchToIframe(InactivelistViewFrame);


            //WaitForElement(newRecordButton);
            //WaitForElement(exportToExcelButton);
            // WaitForElement(mailMergeButton);


            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab WaitForPersonCarePlansSubPage_EditRegularCareTasksTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_RegularCare);
            SwitchToIframe(iframe_RegularCare);

          
            return this;
        }


        public PersonCarePlansSubPage_RegularCareTasksTab WaitForPersonCarePlansSubPage_RegularCareTasksInactiveTabToLoad()
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

            WaitForElement(InactivelistViewFrame);
            SwitchToIframe(InactivelistViewFrame);

            return this;
        }
        public PersonCarePlansSubPage_RegularCareTasksTab SelectCarePlanRecord(string RecordID)
        {
            Click(record_CheckBox(RecordID));

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ClickOnCarePlanRecord(string RecordID)
        {
            Click(recordRow(RecordID));

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ClickMailMergeButton()
        {
            Click(mailMergeButton);

            return this;
        }
        public PersonCarePlansSubPage_RegularCareTasksTab ClickCreateNewRecord()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ClickDeleteRecord()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateCarePlanTypeHeader(String ExpectedText)
        {
            WaitForElementVisible(CarePlanType_Header);
            ValidateElementText(CarePlanType_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidatePersonHeader(String ExpectedText)
        {
            ValidateElementText(Person_Header, ExpectedText);
            return this;
        }
        public PersonCarePlansSubPage_RegularCareTasksTab ValidateStartDateHeader(String ExpectedText)
        {
            ValidateElementText(StartDate_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateStatusHeader(String ExpectedText)
        {
            ValidateElementText(Status_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateReviewDateHeader(String ExpectedText)
        {
            ValidateElementText(ReviewDate_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateReviewFrequencyHeader(String ExpectedText)
        {
            ValidateElementText(ReviewFrequency_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateResponsibleTeamHeader(String ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElement(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab ClickRecordCellText(string RecordID, int CellPosition)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            System.Threading.Thread.Sleep(2000);
            Click(recordCell(RecordID, CellPosition));

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab test()
        {

            ScrollToElement(InactivelistViewFrame);

            return this;
        }
    }
}
